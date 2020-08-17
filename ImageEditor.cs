using System;
using System.Drawing;
using System.IO;

class ImageEditor
{
    public enum ImageFormat {
        png,
        jpg
    }
    private static bool dimensionsAreValid(System.Drawing.Size size) {
        // max permissible size is 4k resolution
        const uint max_resolution_width = 3840;
        const uint max_resolution_height = 2160;

        bool width_valid = (size.Width > 0 && size.Width <= max_resolution_width);
        bool height_valid = (size.Height > 0 && size.Height <= max_resolution_height);
        return width_valid && height_valid;
    }

    private static string cachedImagePath(string path, System.Drawing.Size size, ImageFormat format, string watermark, Color background) {
        // TODO: implement cache, and return a valid string pointing to the cached image if it exists
        return "";
    }

    public static Image getImage(string path, System.Drawing.Size desired_size, ImageFormat format, string watermark = "", Color background_color = default(Color)) {
        if (!File.Exists(path)) {
            throw new FileNotFoundException();
        }
        if (!dimensionsAreValid(desired_size)) {
            throw new ArgumentOutOfRangeException("desired_size: The requested image dimensions must be greater than 0 and smaller than 4k resolution");
        }

        Image adjusted_image;
        string cached_path = cachedImagePath(path, desired_size, format, watermark, background_color);
        if (File.Exists(cached_path)) {
            // use cached image file
            adjusted_image = new Bitmap(cached_path);
        }
        else {
            // generate new cached image file
            adjusted_image = new Bitmap(desired_size.Width, desired_size.Height);
            using (Graphics graphics = Graphics.FromImage(adjusted_image)) {

                // paint the replacement background colour
                if (background_color != default(Color)) {
                }

                // paint the original image

                // paint the watermark
                if (watermark.Length > 0) {
                }

                // save finished image to the cache
            }
        }

        // return imagefile;
        return adjusted_image;
    }
}