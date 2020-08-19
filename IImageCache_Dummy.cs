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

        public string cachedImagePath(ref ImageProperties desired_image)
        {
            // stub implementaiton
            // Here is where we *would* check if an image matching the arguments had already been generated and cached, and return the path to that image
            return "";
        }

        public void saveImageToCache(Image image, ref ImageProperties image_properties)
        {
            string cache_image_name = "test";
            string cache_image_path = CacheFolder + cache_image_name + "." + image_properties.Format.ToString();
            image.Save(cache_image_path, image_properties.Format);
        }

        public string CacheFolder { get; set; }
    }
}
