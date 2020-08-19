using System;
using System.Drawing;
using System.IO;

namespace ImageCache {
    class IImageCache_Dummy : IImageCache {

        public IImageCache_Dummy(string cache_folder)
        {
            CacheFolder = cache_folder;
            Directory.CreateDirectory(CacheFolder);
        }

        public string cachedImagePath(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background)
        {
            // stub implementaiton
            // Here is where we *would* check if an image matching the arguments had already been generated and cached, and return the path to that image
            return "";
        }

        public void saveImageToCache(Image image, string image_source_path, System.Drawing.Size desired_image_size, System.Drawing.Imaging.ImageFormat format, string watermark, Color background_color)
        {
            string cache_image_name = "test";
            string cache_image_path = CacheFolder + cache_image_name + "." + format.ToString();
            image.Save(cache_image_path, format);
        }

        public string CacheFolder { get; set; }
    }
}
