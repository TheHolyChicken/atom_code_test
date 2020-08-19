using System;
using System.Drawing;

namespace ImageCache {
    interface IImageCache
    {
        string cachedImagePath(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background);
        void saveImageToCache(Image image, string image_source_path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background_color);
    }
}
