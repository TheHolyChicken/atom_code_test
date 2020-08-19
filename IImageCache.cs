using System;
using System.Drawing;

namespace ImageCache {
    interface IImageCache
    {
        string cachedImagePath(ref ImageProperties desired_image);
        void saveImageToCache(Image image, ref ImageProperties image_properties);
    }
}
