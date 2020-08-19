using System;
using System.Drawing;

namespace ImageCache {
    interface IImageCache
    {
        string cachedImagePath(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background);
    }
}
