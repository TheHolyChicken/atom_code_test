using System;
using System.IO;
using System.Drawing;

namespace atom_code_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache_folder = "C:/work/atom_code_test/cache_images/";
            var cache = new ImageCache.IImageCache_Dummy(cache_folder);
            var image_provider = new ImageProvider(cache);

            var image_properties = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                , new System.Drawing.Size(537, 512)
                , System.Drawing.Imaging.ImageFormat.Png
                , "watermark"
                , System.Drawing.Color.DarkBlue);

            try {
                image_provider.getImage(ref image_properties);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"{e}");
            }
        }
    }
}
