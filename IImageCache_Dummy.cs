using System;
using System.Drawing;

namespace ImageCache {
    class IImageCache_Dummy : IImageCache {
        public string cachedImagePath(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background)
        {
            // stub implementaiton
            // Here is where we *would* check if an image matching the arguments had already been generated and cached, and return the path to that image
            return "";
        }
    }
}
