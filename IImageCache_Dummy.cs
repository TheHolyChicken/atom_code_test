using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ImageCache {
    class IImageCache_Dummy : IImageCache {

        public IImageCache_Dummy(string cache_folder)
        {
            CacheFolder = cache_folder;
            Directory.CreateDirectory(CacheFolder);
            stored_images_ = new Dictionary<ImageProperties, string>();
        }

        public string cachedImagePath(ref ImageProperties desired_image)
        {
            string cached_image_path;
            stored_images_.TryGetValue(desired_image, out cached_image_path);
            return cached_image_path;
        }

        public void saveImageToCache(Image image, ref ImageProperties image_properties)
        {
            string cache_image_name = stored_images_.Count.ToString();
            string cache_image_path = CacheFolder + cache_image_name + "." + image_properties.Format.ToString();
            image.Save(cache_image_path, image_properties.Format);
            stored_images_.TryAdd(image_properties, cache_image_path);
        }

        public string CacheFolder { get; }
        private Dictionary<ImageProperties, string> stored_images_;
    }
}
