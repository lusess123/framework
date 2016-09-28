using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ataw.Framework.Core
{

    public class WhModel
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    /// <summary>
    /// Bitmap operations
    /// </summary>
    public class Callback
    {
        /// <summary>
        /// create a new BitmapCutter.Core.API.BitmapOPS instance
        /// </summary>
        public Callback() { }

        public static string Src(string src)
        {
            if (src[0] == '/')
                src = src.Substring(1, src.Length - 1);
            src = src.Replace("/", "\\");
            return Path.Combine(AtawAppContext.Current.MapPath, src);
        }


        /// <summary>
        /// rotate bitmap with any angle
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public WhModel RotateBitmap(string src, float angle)
        {
            src = Src(src);

            // HttpContext context = HttpContext.Current;
            // float angle = float.Parse(context.Request["angle"]);
            Image oldImage = Bitmap.FromFile(src);
            using (oldImage)
            {
                using (Bitmap newBmp = Helper.RotateImage(oldImage, angle))
                {
                    oldImage.Dispose();
                    int nw = newBmp.Width;
                    int nh = newBmp.Height;
                    newBmp.Save(src);
                    return new WhModel() { width = nw, height = nh };
                }
                //newBmp.Dispose();
            }
            //  return "{msg:'success',size:{width:" + nw.ToString() + ",height:" + nh.ToString() + "}}";


        }

        public string GenerateBitmap(string src, string zoom, int x, int y, int width, int height)
        {
            src = Src(src);

            FileInfo fi = new FileInfo(src);
            string ext = fi.Extension;

            string newfileName = string.Format("portraits/{0}{1}", Guid.NewGuid().ToString("N"), ".png");
            string pyNewFileName = Src(newfileName);
            string dFileName = Path.GetDirectoryName(pyNewFileName);
            if (!Directory.Exists(dFileName))
            {
                Directory.CreateDirectory(dFileName);
            }
            //Image.GetThumbnailImageAbort abort = null;         
            Bitmap oldBitmap = new Bitmap(src);
            // HttpContext context = HttpContext.Current;
            Cutter cut = new Cutter(
                zoom.Value<double>(),
                -x,
                -y,
                width,
                height,
                oldBitmap.Width,
                oldBitmap.Height);

            //using ()
            //{
            Bitmap bmp = Helper.GenerateBitmap(oldBitmap, cut);
            using (bmp)
            {
                oldBitmap.Dispose();
                string temp = Path.Combine(AtawAppContext.Current.MapPath, newfileName);
                //oldBitmap.Dispose();
                bmp.Save(temp, ImageFormat.Png);
            }

            return newfileName;
            //}
        }
    }
}
