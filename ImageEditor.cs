using System;
using System.Drawing;
using System.IO;

class ImageEditor
{
    public enum ImageFormat {
        png,
        jpg
    }
    private static bool dimensionsAreValid(uint width, uint height) {
        const uint max_resolution_width = 3840;
        const uint max_resolution_height = 2160;

        bool width_valid = (width > 0 && width <= max_resolution_width);
        bool height_valid = (height > 0 && height <= max_resolution_height);
        return width_valid && height_valid;
    }

    private static string cachedImagePath(string path, uint width, uint height, ImageFormat format, string watermark, Color background) {
        // TODO: implement cache, and return a valid string pointing to the cached image if it exists
        return "";
    }

    public static void getImage(string path, uint width, uint height, ImageFormat format, string watermark = "", Color background = default(Color)) {
        if (!File.Exists(path) || !dimensionsAreValid(width, height)) {
            return;
        }

        string cached_path = cachedImagePath(path, width, height, format, watermark, background);
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