using System;
using System.Drawing;
using System.IO;

class ImageEditor
{
    private static bool dimensionsAreValid(System.Drawing.Size size) {
        // max permissible size is 4k resolution
        const uint max_resolution_width = 3840;
        const uint max_resolution_height = 2160;

        bool width_valid = (size.Width > 0 && size.Width <= max_resolution_width);
        bool height_valid = (size.Height > 0 && size.Height <= max_resolution_height);
        return width_valid && height_valid;
    }

    private static string cachedImagePath(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background) {
        // TODO: implement cache, and return a valid string pointing to the cached image if it exists
        return "";
    }

    private static Font getMaximumSizeFont(System.Drawing.Graphics graphics, Size available_space, string text, Font font)
    {
        SizeF font_space = graphics.MeasureString(text, font);
        float max_height_scaling = available_space.Height / font_space.Height;
        float max_width_scaling = available_space.Width / font_space.Width;
        float scaling = Math.Min(max_height_scaling, max_width_scaling);
        float scaled_font_size = font.Size * scaling;
        return new Font(font.FontFamily, scaled_font_size, font.Style, GraphicsUnit.Pixel);
    }

    public static Image getImage(string path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark = "", Color background_color = default(Color)) {
        if (!File.Exists(path)) {
            throw new FileNotFoundException();
        }
        if (!dimensionsAreValid(desired_image_size)) {
            throw new ArgumentOutOfRangeException("desired_size: The requested image dimensions must be greater than 0 and smaller than 4k resolution");
        }

        Image adjusted_image;
        string cached_path = cachedImagePath(path, desired_image_size, format, watermark, background_color);
        if (File.Exists(cached_path)) {
            // use cached image file
            adjusted_image = new Bitmap(cached_path);
        }
        else {
            // generate new cached image file
            adjusted_image = new Bitmap(desired_image_size.Width, desired_image_size.Height);
            using (Graphics graphics = Graphics.FromImage(adjusted_image)) {

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
                string cache_image_folder = "C:/work/atom_code_test/cache_images/";
                string cache_image_name = "test";
                string cache_image_path = cache_image_folder + cache_image_name + "." + format.ToString();
                Directory.CreateDirectory(cache_image_folder);
                adjusted_image.Save(cache_image_path, format);
            }
        }

        // return imagefile;
        return adjusted_image;
    }
}