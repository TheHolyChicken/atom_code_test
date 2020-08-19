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


    public Image getImage(ref ImageProperties desired_image)
    {
        if (!File.Exists(desired_image.Path)) {
            throw new FileNotFoundException();
        }
        if (!dimensionsAreValid(desired_image.Size)) {
            throw new ArgumentOutOfRangeException("desired_image.Size: The requested image dimensions are out of bounds");
        }

        // if the requested image exactly matches the original image simply fetch it with no changes
        if (!imageChangesRequired(ref desired_image)) {
            return new Bitmap(desired_image.Path);
        }

        // if the requested image exactly matches one stored in the cache, return the cached image
        string cached_path = _image_cache.cachedImagePath(ref desired_image);
        if (File.Exists(cached_path)) {
            return new Bitmap(cached_path);
        }

        // the requested image is new, so generate & cache it for future requests
        return generateNewCachedImage(ref desired_image);
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

    private bool imageChangesRequired(ref ImageProperties desired_image)
    {
        using (var file_stream = new FileStream(desired_image.Path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var source_image = Image.FromStream(file_stream, false, false))
            {
                bool format_changed = desired_image.Format != source_image.RawFormat;
                bool size_changed = desired_image.Size != source_image.Size;
                bool watermark_required = desired_image.Watermark.Length > 0;
                bool colour_change_required = desired_image.BackgroundColor != default(Color);
                return format_changed || size_changed || watermark_required || colour_change_required;
            }
        }
    }

    private Image generateNewCachedImage(ref ImageProperties desired_image)
    {
        var modified_image = new Bitmap(desired_image.Size.Width, desired_image.Size.Height);
        using (var graphics = Graphics.FromImage(modified_image)) {

            // paint the replacement background colour
            if (desired_image.BackgroundColor != default(Color)) {
                graphics.Clear(desired_image.BackgroundColor);
            }

            // paint the original image
            Image original_image = new Bitmap(desired_image.Path);
            graphics.DrawImage(original_image, 0, 0, desired_image.Size.Width, desired_image.Size.Height);

            // paint the watermark
            if (desired_image.Watermark.Length > 0) {
                var color = Color.FromArgb(60, 0, 0, 0);
                var brush = new SolidBrush(color);
                var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular, GraphicsUnit.Pixel);

                var available_text_area = new Size(desired_image.Size.Width * 9 / 10, desired_image.Size.Height * 9 / 10);
                var scaled_font = getMaximumSizeFont(graphics, available_text_area, desired_image.Watermark, font);
                var font_space = graphics.MeasureString(desired_image.Watermark, scaled_font);
                var text_start_point = new Point((desired_image.Size.Width - (int)font_space.Width) / 2, (desired_image.Size.Height - (int)font_space.Height) / 2);
                graphics.DrawString(desired_image.Watermark, scaled_font, brush, text_start_point);
            }

            // save finished image to the cache
            _image_cache.saveImageToCache(modified_image, ref desired_image);
        }
        return modified_image;
    }

    private IImageCache _image_cache;
}