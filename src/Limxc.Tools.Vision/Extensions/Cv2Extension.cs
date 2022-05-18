using System;
using OpenCvSharp;

namespace Limxc.Tools.Vision.Extensions
{
    public static class Cv2Extension
    {
        public static Mat FromBase64(this string image)
        {
            var bytes = Convert.FromBase64String(image);

            return Cv2.ImDecode(bytes, ImreadModes.Unchanged);
        }

        public static string ToBase64(this Mat mat, string ext = ".png")
        {
            if (Cv2.ImEncode(ext, mat, out var rst))
                return Convert.ToBase64String(rst);

            return string.Empty;
        }
    }
}