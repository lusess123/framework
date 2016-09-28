using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Ataw.Framework.Core
{
    #region 图片操作类
    public class ImageUtil
    {


        /// <summary>
        /// 把图片转化为二进制
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] GetByteFromFile(string filepath)
        {

            int Length = 0;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Length = Convert.ToInt32(fs.Length);
                byte[] retBlock = new byte[Length];
                //申明一个byte数组
                BinaryReader br = new BinaryReader(fs);
                //申明一个读取二进制流的BinaryReader对象
                for (int i = 0; i < Length; i++)
                {   //循环数组大小那么多次
                    br.Read(retBlock, 0, Length); //第一个数组用0表示
                    //将数据读取出来放在数组中
                }
                return retBlock;

            }
            //return null;
        }

        #region 生成缩略图
        #region 给图片打背景 并且生产缩略图
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="Width">图片实际宽度</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="Height">图片实际高度</param>
        /// <param name="maxHeight">最大高度</param>
        /// <returns></returns>
        public static Size SquareResizeImage(int Width, int maxWidth, int Height, int maxHeight)
        {
            if (Width >= maxWidth && Height >= maxHeight) //图片宽度高度比最大宽度高度都大时
            {
                if (Width * maxHeight > Height * maxWidth)//图片宽度宽度大于宽度高度时
                {
                    int h = Convert.ToInt32((decimal)maxWidth / (decimal)Width * (decimal)Height);
                    return new Size(maxWidth, h);
                }
                else
                {
                    int w = Convert.ToInt32((decimal)maxHeight / (decimal)Height * (decimal)Width);
                    return new Size(w, maxHeight);
                }
            }
            if (Width > maxWidth && Height < maxHeight)//图片宽度比最大宽度大 高度比最大高度小时
            {
                int h = Convert.ToInt32((decimal)maxWidth / (decimal)Width * (decimal)Height);
                return new Size(maxWidth, h);
            }
            if (Width < maxWidth && Height > maxHeight)//图片宽度高度比最大宽度高度都小时
            {
                int w = Convert.ToInt32((decimal)maxHeight / (decimal)Height * (decimal)Width);
                return new Size(w, maxHeight);
            }
            else
            {
                return new Size(Width, Height);
            }
        }
        /// <summary>
        /// 上传图片生成缩列图并打上背景
        /// </summary>
        /// <param name="stream">原文件路径</param>
        /// <param name="filePath">保存图片路径</param>
        /// <param name="DefaultSrc">背景图片</param>
        /// <param name="maxLength">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void MakeSquareThumbnail(string originalImagePath, string filePath, string DefaultSrc, int maxLength, int maxHeight)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
            //System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            Size size = SquareResizeImage(image.Width, maxLength, image.Height, maxHeight);//生成图片大小

            Bitmap bitmap = new Bitmap(image);

            Bitmap squareBitmap = bitmap.Clone(new Rectangle(0, 0, image.Width, image.Height), bitmap.PixelFormat);//绘制
            Bitmap tbBitmap = new Bitmap(squareBitmap, size);
            try
            {
                #region 图片存入内存
                //				MemoryStream downStream = new MemoryStream();
                //				//处理JPG质量的函数
                //				int level = 95;
                //				ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                //				ImageCodecInfo ici = null;
                //				foreach (ImageCodecInfo codec in codecs)
                //				{
                //					if (codec.MimeType == "image/jpeg")
                //						ici = codec;
                //				}
                //				EncoderParameters ep = new EncoderParameters();
                //				ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)level);
                //               
                //				bitmap.Save(downStream, ici, ep);
                //				//bitMap.Save(downStream, ImageFormat.Jpeg);
                //				HttpResponse response = HttpContext.Current.Response;
                //				response.ClearContent();
                //				response.ContentType = "image/jpeg";
                //				response.BinaryWrite(downStream.ToArray());
                //				bitmap.Dispose();
                #endregion

                tbBitmap.Save(new Uri(filePath).LocalPath, ImageFormat.Jpeg);
                //     			tbBitmap.Save(filePath, ImageFormat.Jpeg);
                //				System.Drawing.Image image1 = System.Drawing.Image.FromFile(DefaultSrc); //背景图片
                System.Net.WebClient obj1 = new System.Net.WebClient();
                System.Drawing.Image image1 = System.Drawing.Image.FromStream(obj1.OpenRead(DefaultSrc));//背景图片  这种方法才可以直接写HTTP路径的图片
                obj1.Dispose();
                System.Drawing.Image copyImage = System.Drawing.Image.FromFile(filePath);
                Graphics g = Graphics.FromImage(image1);

              //  System.Drawing.Image copyImage = System.Drawing.Image.FromFile(filePath);
                //新建一个bmp图片
                //var image1 = new System.Drawing.Bitmap(maxLength, maxHeight);
                //var copyImage = image1;
                ////新建一个画板
                //Graphics g = System.Drawing.Graphics.FromImage(image1);
                ////设置高质量插值法
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                ////设置高质量,低速度呈现平滑程度
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                ////清空画布并以透明背景色填充
                //g.Clear(Color.Transparent);
               // var copyImage = bitmap2;

                //				if(copyImage.Width==maxLength && copyImage.Height==maxHeight)
                //				{
                //					g.DrawImage(copyImage, new Rectangle(0, 0, maxLength, maxHeight), 0, 0,copyImage.Width, copyImage.Height, GraphicsUnit.Pixel); 
                //				}
                if (copyImage.Width < maxLength || copyImage.Height < maxHeight)
                {
                    if (copyImage.Width == maxLength && copyImage.Height < maxHeight)
                    {
                        int h = Convert.ToInt32((decimal)maxLength / (decimal)image.Width * (decimal)image.Height);
                        g.DrawImage(copyImage, new Rectangle(0, (maxHeight - h) / 2, maxLength, h), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel); //左上右下
                    }
                    if (copyImage.Width < maxLength && copyImage.Height < maxHeight)
                    {
                        int a = (image1.Width - copyImage.Width) / 2;
                        int b = (image1.Height - copyImage.Height) / 2;
                        g.DrawImage(copyImage, new Rectangle(a, b, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                    }
                    if (copyImage.Width < maxLength && copyImage.Height == maxHeight)
                    {
                        int w = Convert.ToInt32((decimal)maxHeight / (decimal)image.Height * (decimal)image.Width);
                        g.DrawImage(copyImage, new Rectangle((maxLength - w) / 2, 0, w, maxHeight), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);

                    }
                    g.Dispose();
                    copyImage.Dispose();
                    //保存加水印过后的图片,删除原始图片 
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    image1.Save(filePath);
                    image1.Dispose();

                }
                else
                {
                    g.Dispose();
                    copyImage.Dispose();
                    image1.Dispose();
                }

            }
            finally
            {
                bitmap.Dispose();
                squareBitmap.Dispose();
                tbBitmap.Dispose();

            }

        }
        #endregion
        /// <summary>
        /// 生成缩略图保存到服务器端文件夹
        /// </summary>
        /// <param name="streamImage">源图文件流</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(Stream streamImage, string thumbnailPath, int height, int width, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(streamImage);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）
                    toheight = originalImage.Height * width / originalImage.Width;
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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


        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            var bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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
        #endregion


        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="filePath">原图片路径</param>
        /// <param name="fileName">原图片名称</param>
        /// <param name="size">压缩的大小</param>
        /// <param name="path">转后的图片要保存的路径</param>
        public static void SavePictureInFile(string filePath, string fileName, int width, int height, string path, int quality = 100)
        {
            if (!System.IO.File.Exists(path))
            {

              // MakeSquareThumbnail(filePath, path, @"D:\project\AtawCode\src\framework\Web\Ataw.WebSite\Content\bg.jpg", width, height);

             //  MakeThumbnail(filePath, path, width, height, "HW");

                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filePath);
                CutForCustom(originalImage, path, width, height, quality);



                //string fileExt = System.IO.Path.GetExtension(filePath);

                //Image originalImage = Image.FromFile(filePath);
                //ImageCodecInfo ici = GetImageCodecInfoFromExtension(fileExt);
                //EncoderParameters parameters = new EncoderParameters(1);
                //parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ImageQuality);
                //if (width == 0)
                //    width = originalImage.Width;
                //if (height == 0)
                //    height = originalImage.Height;
                //Bitmap objNewBitMap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                ////从指定的 Image 对象创建新 Graphics 对象
                //Graphics objGraphics = Graphics.FromImage(objNewBitMap);

                ////清除整个绘图面并以透明背景色填充
                //objGraphics.Clear(Color.Transparent);
                ////在指定位置并且按指定大小绘制 原图片 对象
                //objGraphics.DrawImage(originalImage, new Rectangle(0, 0, width, height));
                //objNewBitMap.Save(path, ici, parameters);
                //originalImage.Dispose();
                //objNewBitMap.Dispose();
                //objGraphics.Dispose();
            }
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


        /// <summary>
        /// 设置图片质量
        /// </summary>
        /// <param name="iSource"></param>
        /// <param name="outPath">保存路径</param>
        /// <param name="quality">压缩比例 100 为不压缩</param>
        /// <returns></returns>
        private static bool SetPicThumbnail(Image iSource, string outPath, int quality)
        {
            // System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //以下代码为保存图片时，设置压缩质量 
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = quality;//设置压缩的比例1-100 
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }


        /// <summary> 
        /// 指定长宽裁剪 
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸 
        /// </summary> 
        /// <remarks>吴剑 2010-11-15</remarks> 
        /// <param name="postedFile">原图HttpPostedFile对象</param> 
        /// <param name="fileSaveUrl">保存路径</param> 
        /// <param name="maxWidth">最大宽(单位:px)</param> 
        /// <param name="maxHeight">最大高(单位:px)</param> 
        /// <param name="quality">质量（范围0-100）</param> 
        public static void CutForCustom(System.Drawing.Image initImage, string fileSaveUrl, int maxWidth, int maxHeight, int quality)
        {
            //从文件获取原始图片，并使用流中嵌入的颜色管理信息 
            //System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存 
            if (initImage.Width < maxWidth && initImage.Height < maxHeight)
            {
                Bitmap _newBitmap = new Bitmap(maxWidth, maxHeight);
               // _newBitmap.MakeTransparent(Color.White);
               // _newBitmap.(Color.Transparent);
                Graphics _graphics = Graphics.FromImage(_newBitmap);
                _graphics.Clear(Color.White);
                //_graphics.DrawImage(Image.FromFile(@"F:\backImage.bmp"), 0, 0, 240, 320);
               // Image sourceImg = initImage;
                _graphics.DrawImage(initImage, (maxWidth - initImage.Width) / 2, (maxHeight - initImage.Height) / 2);
               // _graphics.DrawString("菜鸟先飞",new Font( FontFamily.GenericMonospace,12), Brushes.Yellow, 0, 0);
                //_graphics.Clear(Color.White);
                //_graphics.Save();
                _newBitmap.Save(fileSaveUrl, ImageFormat.Jpeg);
                _graphics.Dispose();
                _newBitmap.Dispose();
            }
            else
            {
                //模版的宽高比例 
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例 
                double initRate = (double)initImage.Width / initImage.Height;

                //原图与模版比例相等，直接缩放 
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片 
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                   // templateG.Clear(Color.White);
                    templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                    templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Png);
                }
                //原图与模版比例不等，裁剪后缩放 
                else
                {
                    //裁剪对象 
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //定位 
                    Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位 
                    Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位 

                    //宽为标准进行裁剪 
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化 
                        pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)Math.Floor(initImage.Width / templateRate));
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        //裁剪源定位 
                        fromR.X = 0;
                        fromR.Y = (int)Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int)Math.Floor(initImage.Width / templateRate);

                        //裁剪目标定位 
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int)Math.Floor(initImage.Width / templateRate);
                    }
                    //高为标准进行裁剪 
                    else
                    {
                        pickedImage = new System.Drawing.Bitmap((int)Math.Floor(initImage.Height * templateRate), initImage.Height);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        fromR.X = (int)Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
                        fromR.Y = 0;
                        fromR.Width = (int)Math.Floor(initImage.Height * templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int)Math.Floor(initImage.Height * templateRate);
                        toR.Height = initImage.Height;
                    }

                    //设置质量 
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //裁剪 
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                    //按模版大小生成最终图片 
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //关键质量控制 
                    //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff 
                    ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo i in icis)
                    {
                        if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                        {
                            ici = i;
                        }
                    }
                   // EncoderParameters ep = new EncoderParameters(1);
                   // ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                    //保存缩略图 
                   // templateImage.Save(fileSaveUrl, ici, ep);
                    SetPicThumbnail(templateImage, fileSaveUrl, quality);
                    //templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg); 

                    //释放资源 
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }

            //释放资源 
            initImage.Dispose();
        }

    }
    #endregion
}
