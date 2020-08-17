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

    public static void getImage(string path, System.Drawing.Size size, ImageFormat format, string watermark = "", Color background = default(Color)) {
        if (!File.Exists(path) || !dimensionsAreValid(size)) {
            return;
        }

        string cached_path = cachedImagePath(path, size, format, watermark, background);
        if (File.Exists(cached_path)) {
            // use cached image file
        }
        else {
            // generate new cached image file
            if (background != default(Color)) {
            // do the background replace
            }

            if (watermark.Length > 0) {
                // do the watermark
            }
        }

        // return imagefile;
    }
}