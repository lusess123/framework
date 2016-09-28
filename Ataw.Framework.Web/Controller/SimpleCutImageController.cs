using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Ataw.Framework.Core;

namespace Ataw.Framework.Web
{
    /// <summary>
    /// 简单的图片裁剪
    /// </summary>
    public class SimpleCutImageController : AtawBaseController
    {
        //上传头像按钮事件
        public string CutImage(string imgUrl, string location)
        {
            string[] zbs = location.Split(',');
            int x1 = Int32.Parse(zbs[0]);
            int y1 = Int32.Parse(zbs[1]);
            int x2 = Int32.Parse(zbs[2]);
            int y2 = Int32.Parse(zbs[3]);

            string path = Server.MapPath(imgUrl);
            System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(path);
            int height = Convert.ToInt32(sourceImage.Height);//获取原始图像的高
            int width = Convert.ToInt32(sourceImage.Width);//获取原始图像的宽度
            string ext = System.IO.Path.GetExtension(path).ToLower();//获取图片后缀
            x1 = x1 * (width / 320);//根据实际尺寸缩放
            x2 = x2 * (width / 320);

            y1 = y1 * (height / 240);
            y2 = y2 * (height / 240);
            string tick = "/Images/" + DateTime.Now.Ticks + ext;
            string newImg = Server.MapPath(tick);
            DrawImage(path, newImg, ext, x1, y1, x2 - x1, y2 - y1);

            return tick;
        }

        //裁剪图片
        protected void DrawImage(string srcImage, string destImage, string ext, int x, int y, int width, int height)
        {
            using (System.Drawing.Image sourceImage = System.Drawing.Image.FromFile(srcImage))
            {
                using (System.Drawing.Image templateImage = new System.Drawing.Bitmap(100, 100))//这里设置要显示图片的长宽
                {
                    using (System.Drawing.Graphics templateGraphics = System.Drawing.Graphics.FromImage(templateImage))
                    {
                        templateGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        templateGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        templateGraphics.DrawImage(sourceImage, new System.Drawing.Rectangle(0, 0, 100, 100), new System.Drawing.Rectangle(x, y, width, height), System.Drawing.GraphicsUnit.Pixel);
                        templateImage.Save(destImage, GetImageExt(ext));
                    }
                }
            }
        }

        //根据图片后缀名获取裁剪格式
        public ImageFormat GetImageExt(string ext)
        {
            switch (ext)
            {
                case ".jpg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case ".png":
                    return System.Drawing.Imaging.ImageFormat.Png;
                case ".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                default:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
            }
        }

        /// <summary>
        /// 原始图片路径
        /// </summary>
        /// <param name="imgUrl">原始图片路径</param>
        /// <param name="h">裁剪的高度</param>
        /// <param name="w">裁剪的宽度</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="picWidth"></param>
        /// <param name="picHeight"></param>
        /// <returns></returns>
        public string CutImageJcrop(string imgUrl, int h, int w, int x, int y, int picWidth, int picHeight, int newWidth, int newHeight, string fileStorageName, string ResourceInfoList, string UploaderName)
        {
            ResourceInfo info = null;
            if (!ResourceInfoList.IsEmpty())
            {
                List<ResourceInfo> _res = FastToObject<List<ResourceInfo>>(ResourceInfoList);
                info = _res[0];
            }
            string oldTitle = Path.GetFileNameWithoutExtension(info.PhysicalPath);
            string _title = "{0}-{1}".AkFormat(newWidth, newHeight);
            string _newPath = FileUtil.ChangeFileName(info.PhysicalPath, "{0}_{1}".AkFormat(oldTitle, _title));
            //判断传进来的值是否等于0，如果等于0则直接取截图的大小
            if (newWidth == 0 || newHeight == 0) {
                newWidth = w;
                newHeight = h;
            }
            CutImageForCustom(info.PhysicalPath, _newPath, w, h, x, y, picWidth, picHeight, newWidth, newHeight);
            info.ExtendList.Add(_title, _title);
            string _cropPath = _newPath;
            if (!UploaderName.IsEmpty())
            {
                var _cuts = FileManagementUtil.GetImageCutsByUploadName(UploaderName);
                foreach (ImageCut cut in _cuts)
                {
                    if (cut.ImageSizeWidth != newWidth && cut.ImageSizeHeight != newHeight)
                    {
                        string newFile = FileUtil.ChangeFileName(_cropPath, cut.GetNewFileTitle(oldTitle));
                        ImageUtil.SavePictureInFile(_cropPath, Path.GetFileName(_cropPath), cut.ImageSizeWidth, cut.ImageSizeHeight, newFile,cut.Quality);
                        string _ff = "{0}-{1}".AkFormat(cut.ImageSizeWidth, cut.ImageSizeHeight);
                        info.ExtendList.Add(_ff, _ff);
                    }
                }
            }

            return ReturnJson(info);
            // return tick;
        }

        public class ImageInfo
        {
            public string FileName { get; set; }
            public string FileExtension { get; set; }
            public string HttpPath { get; set; }
        }

        public ViewResult Demo()
        {
            return View();
        }

        /// <summary>
        /// 根据客户端的裁剪尺寸进行裁剪
        /// </summary>
        /// <param name="srcImage">原图</param>
        /// <param name="destImage">裁剪后要保存的图片</param>
        /// <param name="cutWidth"></param>
        /// <param name="cutHeight"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">原图宽度</param>
        /// <param name="height">原图高度</param>
        /// <param name="newWidth">新图片宽度</param>
        /// <param name="newHeight">新图片高度</param>
        public void CutImageForCustom(string srcImage, string destImage, int cutWidth, int cutHeight, int x, int y, int width, int height, int newWidth, int newHeight)
        {
            if (!System.IO.File.Exists(destImage))
            {
                //从文件获取原始图片，并使用流中嵌入的颜色管理信息
                Image initImage = Image.FromFile(srcImage);
                //按模版大小生成最终图片
                System.Drawing.Image templateImage = new System.Drawing.Bitmap(cutWidth * initImage.Width / width, cutHeight * initImage.Width / width);
                System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                templateG.Clear(Color.White);



                int a = x * initImage.Width / width, b = y * initImage.Height / height, c = cutWidth * initImage.Width / width, d = cutHeight * initImage.Height / height;

                templateG.DrawImage(initImage, new Rectangle(0, 0, cutWidth * initImage.Width / width, cutHeight * initImage.Width / width), a, b, c, d, GraphicsUnit.Pixel);            //关键质量控制


                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                        break;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);

                MemoryStream stream = new MemoryStream();
                //保存缩略图
                templateImage.Save(stream, ici, ep);
                Image newImg = Image.FromStream(stream);
                Bitmap bmp = new Bitmap(newImg, newWidth, newHeight); //建立图片
                bmp.Save(destImage);
                // bmp.
                //释放资源
                templateG.Dispose();
                templateImage.Dispose();
                initImage.Dispose();
                newImg.Dispose();
                bmp.Dispose();
            }
        }

    }
}
