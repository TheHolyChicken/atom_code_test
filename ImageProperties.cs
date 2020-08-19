using System.Drawing;


public struct ImageProperties
{
    public ImageProperties(string path, System.Drawing.Size size, System.Drawing.Imaging.ImageFormat format, string watermark = "", Color background_color = default(Color))
    {
        Path = path;
        Size = size;
        Format = format;
        Watermark = watermark;
        BackgroundColor = background_color;
    }

    public string Path { get; }
    public System.Drawing.Size Size { get; }
    public System.Drawing.Imaging.ImageFormat Format { get; }
    public string Watermark { get; }
    public Color BackgroundColor { get; }
}
