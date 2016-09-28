using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Ataw.Framework.Web
{
    #region 图片操作类
    public class ImageClass
    {
        /// <summary>
        /// 图片微缩图处理
        /// </summary>
        /// <param name="path">目标图片</param>
        /// <param name="width">宽度</param>
        public static void SavePictureInFile(HttpPostedFileBase file, int width, string fileName, string path)
        {
            string fileExt = System.IO.Path.GetExtension(file.FileName);
            string filePath = file.FileName;

            ImageCodecInfo ici = GetImageCodecInfoFromExtension(fileExt);
            //根据图片的磁盘绝对路径获取 源图片 的Image对象
            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

            int oldWidth = img.Width;
            double rate = (double)width / (double)oldWidth;
            double newHeight = img.Height * rate;
            int height = (int)newHeight;
            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(width, height);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight)
                        , GraphicsUnit.Pixel);

            #region 加水印
            if (width > 500)
            {
                // 加文字水印
                //Font f = new Font("Verdana", (float)(32 * rate));//文字字体             
                //Brush b = Brushes.DarkGray;//文字颜色         
                //string addText = "江南第一杯";//要加的文字           
                //g.DrawString(addText, f, b, 10, 10);

                // 添加图片水印
                string watermarkImage = path + "/logo.png";
                if (File.Exists(watermarkImage))
                {
                    System.Drawing.Image copyImage = System.Drawing.Image.FromFile(watermarkImage);
                    g.DrawImage(copyImage, new Rectangle(bmp.Width - copyImage.Width, bmp.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                }
            }
            #endregion

            ImageFormat format = img.RawFormat;
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(i => i.FormatID == format.Guid);
            EncoderParameter param = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = param;
            img.Dispose();

            string name = string.Format("{0}_{1}.{2}", fileName, width, fileExt.Substring(1));
            bmp.Save(Path.Combine(path, name), ici, parameters);
            img.Dispose();
            g.Dispose();
            bmp.Dispose();
        }

       /// <summary>
       /// 压缩图片
       /// </summary>
       /// <param name="file">上传控件</param>
       /// <param name="width">宽度</param>
       /// <param name="fileName">文件名</param>
       /// <param name="path">保存路径</param>
        public static void SavePictureInFileHaveNoSize(HttpPostedFileBase file, int width, string fileName, string path)
        {
            string fileExt = System.IO.Path.GetExtension(file.FileName);
            string filePath = file.FileName;

            ImageCodecInfo ici = GetImageCodecInfoFromExtension(fileExt);
            //根据图片的磁盘绝对路径获取 源图片 的Image对象
            System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);

            int oldWidth = img.Width;
            double rate = (double)width / (double)oldWidth;
            double newHeight = img.Height * rate;
            int height = (int)newHeight;
            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(width, height);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight)
                        , GraphicsUnit.Pixel);

            ImageFormat format = img.RawFormat;
            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().SingleOrDefault(i => i.FormatID == format.Guid);
            EncoderParameter param = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = param;
            img.Dispose();

            bmp.Save(Path.Combine(path, fileName), ici, parameters);
            img.Dispose();
            g.Dispose();
            bmp.Dispose();
        }
      

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="width">压缩的图片宽度</param>
        /// <param name="size">压缩的图片高度</param>
        /// <param name="fileName">文件名</param>
        /// <param name="path">保存的路径</param>
        public static void SavePictureInFile(HttpPostedFileBase file, int width, int height, string fileName, string path)
        {
            string fileExt = System.IO.Path.GetExtension(file.FileName);
            string filePath = file.FileName;

            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(file.InputStream);
            ImageCodecInfo ici = GetImageCodecInfoFromExtension(fileExt);
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImageQuality);

            Bitmap objNewBitMap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            //从指定的 Image 对象创建新 Graphics 对象
            Graphics objGraphics = Graphics.FromImage(objNewBitMap);
            //清除整个绘图面并以透明背景色填充
            // objGraphics.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制 原图片 对象
            objGraphics.DrawImage(originalImage, new Rectangle(0, 0, width, height));
            objNewBitMap.Save(Path.Combine(path, fileName), ici, parameters);

        }


        /// <summary>
        /// 根据图片后缀获取图片的信息
        /// </summary>
        /// <param name="fileExt">图片后缀</param>
        /// <returns>ImageCodecInfo</returns>
        protected static ImageCodecInfo GetImageCodecInfoFromExtension(string fileExt)
        {
            fileExt = fileExt.TrimStart(".".ToCharArray()).ToLower().Trim();
            switch (fileExt)
            {
                case "jpg":
                case "jpeg":
                    return GetImageCodecInfoFromMimeType("image/jpeg");
                case "png":
                    return GetImageCodecInfoFromMimeType("image/png");
                case "gif":
                    //use png codec for gif to preserve transparency
                    //return GetImageCodecInfoFromMimeType("image/gif");
                    return GetImageCodecInfoFromMimeType("image/png");
                default:
                    return GetImageCodecInfoFromMimeType("image/jpeg");
            }
        }


        /// <summary>
        /// 获取图片的信息（ImageCodecInfo）
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>ImageCodecInfo</returns>
        protected static ImageCodecInfo GetImageCodecInfoFromMimeType(string mimeType)
        {
            var info = ImageCodecInfo.GetImageEncoders();
            foreach (var ici in info)
                if (ici.MimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
                    return ici;
            return null;
        }


        /// <summary>
        ///  图片质量
        /// </summary>
        public static long ImageQuality
        {
            get
            {
                return 100L;
            }
        }


        /// <SUMMARY>
        /// 生成缩略图//带压缩图片不压缩22k压缩2k
        /// </SUMMARY>
        /// <PARAM name="originalImagePath" />原始路径
        /// <PARAM name="thumbnailPath" />生成缩略图路径
        /// <PARAM name="width" />缩略图的宽
        /// <PARAM name="height" />缩略图的高
        //是否压缩图片质量
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, bool Ys)
        {
            //获取原始图片  
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            //缩略图画布宽高  
            int towidth = width;
            int toheight = height;
            //原始图片写入画布坐标和宽高(用来设置裁减溢出部分)  
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            //原始图片画布,设置写入缩略图画布坐标和宽高(用来原始图片整体宽高缩放)  
            int bg_x = 0;
            int bg_y = 0;
            int bg_w = towidth;
            int bg_h = toheight;
            //倍数变量  
            double multiple = 0;
            //获取宽长的或是高长与缩略图的倍数  
            if (originalImage.Width >= originalImage.Height)
                multiple = (double)originalImage.Width / (double)width;
            else
                multiple = (double)originalImage.Height / (double)height;
            //上传的图片的宽和高小等于缩略图  
            if (ow <= width && oh <= height)
            {
                //缩略图按原始宽高  
                bg_w = originalImage.Width;
                bg_h = originalImage.Height;
                //空白部分用背景色填充  
                bg_x = Convert.ToInt32(((double)towidth - (double)ow) / 2);
                bg_y = Convert.ToInt32(((double)toheight - (double)oh) / 2);
            }
            //上传的图片的宽和高大于缩略图  
            else
            {
                //宽高按比例缩放  
                bg_w = Convert.ToInt32((double)originalImage.Width / multiple);
                bg_h = Convert.ToInt32((double)originalImage.Height / multiple);
                //空白部分用背景色填充  
                bg_y = Convert.ToInt32(((double)height - (double)bg_h) / 2);
                bg_x = Convert.ToInt32(((double)width - (double)bg_w) / 2);
            }
            //新建一个bmp图片,并设置缩略图大小.  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并设置背景色  
            g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFF"));
            //在指定位置并且按指定大小绘制原图片的指定部分  
            //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
            g.DrawImage(originalImage, new System.Drawing.Rectangle(bg_x, bg_y, bg_w, bg_h), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

            if (Ys)
            {

                System.Drawing.Imaging.ImageCodecInfo encoder = GetImageCodecInfoFromMimeType("image/jpeg");
                try
                {
                    if (encoder != null)
                    {
                        System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                        //设置 jpeg 质量为 60
                        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)60);
                        bitmap.Save(thumbnailPath, encoder, encoderParams);
                        encoderParams.Dispose();

                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }

            }
            else
            {

                try
                {
                    //获取图片类型  
                    string fileExtension = System.IO.Path.GetExtension(originalImagePath).ToLower();
                    //按原图片类型保存缩略图片,不按原格式图片会出现模糊,锯齿等问题.  
                    switch (fileExtension)
                    {
                        case ".gif": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif); break;
                        case ".jpg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        case ".bmp": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp); break;
                        case ".png": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png); break;
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }

            }

        }

    }
    #endregion
}
