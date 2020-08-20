using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace atom_code_test
{
    class Program
    {
        static List<ImageProperties> createTestImageRequests()
        {
            var images = new List<ImageProperties>();

            var image_with_bad_path = new ImageProperties("bad path"
                            , new System.Drawing.Size(537, 1024)
                            , System.Drawing.Imaging.ImageFormat.Png
                            , ""
                            , default(Color));

            var image_too_small = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                            , new System.Drawing.Size(0, -1)
                            , System.Drawing.Imaging.ImageFormat.Png
                            , ""
                            , default(Color));

            var image_too_big = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                            , new System.Drawing.Size(4000, 512)
                            , System.Drawing.Imaging.ImageFormat.Png
                            , ""
                            , default(Color));

            var image_exactly_matching_original = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                            , new System.Drawing.Size(537, 1024)
                            , System.Drawing.Imaging.ImageFormat.Png
                            , ""
                            , default(Color));

            var image_requiring_edit_1 = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                            , new System.Drawing.Size(268, 512)
                            , System.Drawing.Imaging.ImageFormat.Png
                            , "watermark"
                            , System.Drawing.Color.DarkBlue);

            var image_requiring_edit_2 = new ImageProperties("C:/work/atom_code_test/product_images/01_04_2019_001137.png"
                            , new System.Drawing.Size(2048, 1350)
                            , System.Drawing.Imaging.ImageFormat.Jpeg
                            , "watermark"
                            , System.Drawing.Color.Pink);

            images.Add(image_with_bad_path);
            images.Add(image_too_small);
            images.Add(image_too_big);
            images.Add(image_exactly_matching_original);
            images.Add(image_requiring_edit_1);
            images.Add(image_requiring_edit_1);
            images.Add(image_requiring_edit_2);
            images.Add(image_requiring_edit_2);

            return images;
        }

        static void Main(string[] args)
        {
            var cache_folder = "C:/work/atom_code_test/cache_images/";
            var cache = new ImageCache.IImageCache_SimpleFiles(cache_folder);
            var image_provider = new ImageProvider(cache);

            var test_images = createTestImageRequests();
            foreach(var image in test_images) {
                fetchImage(image, ref image_provider);
            }
        }

        static void fetchImage(ImageProperties image_properties, ref ImageProvider image_provider)
        {
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
