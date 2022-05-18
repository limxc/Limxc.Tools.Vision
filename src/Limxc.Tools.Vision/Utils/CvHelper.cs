using System;
using Limxc.Tools.Vision.Extensions;
using OpenCvSharp;

namespace Limxc.Tools.Vision.Utils
{
    public static class CvHelper
    {
        public static string ToGray(string base64Image)
        {
            using (var mat = base64Image.FromBase64())
            using (var gray = new Mat())
            {
                Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
                return gray.ToBase64(); 
            }
        }
    }
}