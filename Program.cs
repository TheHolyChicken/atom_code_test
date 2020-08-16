using System;
using System.Drawing;

namespace atom_code_test
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageEditor.getImage("C:/work/atom_code_test/product_images/01_04_2019_001123.png", 537, 512, ImageEditor.ImageFormat.png, "watermark", default(Color));
            Console.WriteLine("Hello World!");
        }
    }
}
