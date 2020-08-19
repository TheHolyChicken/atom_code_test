﻿using System;
using System.IO;
using System.Drawing;

namespace atom_code_test
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageCache.IImageCache cache = new ImageCache.IImageCache_Dummy();
            var image_editor = new ImageEditor(cache);

            try {
                image_editor.getImage("C:/work/atom_code_test/product_images/01_04_2019_001123.png"
                , new System.Drawing.Size(537, 512)
                , System.Drawing.Imaging.ImageFormat.Png
                , "watermark"
                , System.Drawing.Color.Aquamarine);
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
