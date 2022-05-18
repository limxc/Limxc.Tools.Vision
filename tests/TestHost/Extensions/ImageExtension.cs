using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace TestHost.Extensions;

public static class ImageExtension
{
    public static BitmapImage ToBitmapImage(this Bitmap bitmap)
    {
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            var result = new BitmapImage();
            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
    }

    public static BitmapImage ToBitmapImage(this MemoryStream stream)
    {
        stream.Position = 0;
        var result = new BitmapImage();
        result.BeginInit();
        result.CacheOption = BitmapCacheOption.OnLoad;
        result.StreamSource = stream;
        result.EndInit();
        result.Freeze();
        return result;
    }

    public static BitmapImage ToBitmapImage(this string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        using (var stream = new MemoryStream(bytes))
        {
            var result = new BitmapImage();
            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
    }

    public static string FromImageControl(this Image image)
    {
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
        using var ms = new MemoryStream();
        encoder.Save(ms);
        var base64 = Convert.ToBase64String(ms.ToArray());
        return base64;
    }
}