using System;
using System.Drawing;
using System.IO;
using ImageCache;

class ImageProvider
{
    public ImageProvider(IImageCache cache)
    {
        _image_cache = cache;

        // set default max permissible size to 4k resolution
        MaxResolutionWidth = 3840;
        MaxResolutionHeight = 2160;
    }

    public uint MaxResolutionWidth { get; set; }
    public uint MaxResolutionHeight { get; set; }


    public Image getImage(string path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark = "", Color background_color = default(Color))
    {
        if (!File.Exists(path)) {
            throw new FileNotFoundException();
        }
        if (!dimensionsAreValid(desired_image_size)) {
            throw new ArgumentOutOfRangeException("desired_image_size: The requested image dimensions are out of bounds");
        }

        // if the requested image exactly matches the original image simply fetch it with no changes
        if (!imageChangesRequired(path, desired_image_size, format, watermark, background_color)) {
            return new Bitmap(path);
        }

        // if the requested image exactly matches one stored in the cache, return the cached image
        string cached_path = _image_cache.cachedImagePath(path, desired_image_size, format, watermark, background_color);
        if (File.Exists(cached_path)) {
            return new Bitmap(cached_path);
        }

        // the requested image is new, so generate & cache it for future requests
        return generateNewCachedImage(path, desired_image_size, format, watermark, background_color);
    }

    private bool dimensionsAreValid(System.Drawing.Size size)
    {
        bool width_valid = (size.Width > 0 && size.Width <= MaxResolutionWidth);
        bool height_valid = (size.Height > 0 && size.Height <= MaxResolutionHeight);
        return width_valid && height_valid;
    }

    private Font getMaximumSizeFont(System.Drawing.Graphics graphics, Size available_space, string text, Font font)
    {
        SizeF font_space = graphics.MeasureString(text, font);
        float max_height_scaling = available_space.Height / font_space.Height;
        float max_width_scaling = available_space.Width / font_space.Width;
        float scaling = Math.Min(max_height_scaling, max_width_scaling);
        float scaled_font_size = font.Size * scaling;
        return new Font(font.FontFamily, scaled_font_size, font.Style, GraphicsUnit.Pixel);
    }

    private bool imageChangesRequired(string path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background_color)
    {
        using (var file_stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var image = Image.FromStream(file_stream, false, false))
            {
                bool format_changed = format != image.RawFormat;
                bool size_changed = desired_image_size != image.Size;
                bool watermark_required = watermark.Length > 0;
                bool colour_change_required = background_color != default(Color);
                return format_changed || size_changed || watermark_required || colour_change_required;
            }
        }
    }

    private Image generateNewCachedImage(string path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background_color)
    {
        var modified_image = new Bitmap(desired_image_size.Width, desired_image_size.Height);
        using (var graphics = Graphics.FromImage(modified_image)) {

            // paint the replacement background colour
            if (background_color != default(Color)) {
                graphics.Clear(background_color);
            }

            // paint the original image
            Image original_image = new Bitmap(path);
            graphics.DrawImage(original_image, 0, 0, desired_image_size.Width, desired_image_size.Height);

            // paint the watermark
            if (watermark.Length > 0) {
                var color = Color.FromArgb(60, 0, 0, 0);
                var brush = new SolidBrush(color);
                var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular, GraphicsUnit.Pixel);

                var available_text_area = new Size(desired_image_size.Width * 9 / 10, desired_image_size.Height * 9 / 10);
                var scaled_font = getMaximumSizeFont(graphics, available_text_area, watermark, font);
                var font_space = graphics.MeasureString(watermark, scaled_font);
                var text_start_point = new Point((desired_image_size.Width - (int)font_space.Width) / 2, (desired_image_size.Height - (int)font_space.Height) / 2);
                graphics.DrawString(watermark, scaled_font, brush, text_start_point);
            }

            // save finished image to the cache
            _image_cache.saveImageToCache(modified_image, path, desired_image_size, format, watermark, background_color);
        }
        return modified_image;
    }

    private IImageCache _image_cache;
}