using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using Ataw.Framework.Core;
using System.Globalization;

namespace Ataw.Framework.Web
{
    public class EditorUploadJsonController : AtawBaseController
    {
        //文件保存目录路径
        private string savePath = "~/Upload/";
        //文件保存目录URL
        private string saveUrl = "Upload/";
        //定义允许上传的文件扩展名
        private string fileTypes = "gif,jpg,jpeg,png,bmp,ppt,xls,doc,docx,txt,rar,zip,docx,mp3,acc,wma,wav";
        //最大文件大小
        private int maxSize = 1000000000;


        public string UploadFileResult()
        {
            HttpPostedFileBase imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                return showError("请选择文件。");
            }

            string dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                //showError("上传目录不存在。");
            }

            string fileName = imgFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();
            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                return showError("上传文件大小超过限制。");
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return showError("上传文件扩展名是不允许的扩展名。");
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);
            saveUrl = WebCommonClass.GetStoreLocation() + saveUrl;
            string fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            return JsonConvert.SerializeObject(hash);
        }

        private string showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            return JsonConvert.SerializeObject(hash);
        }
    }
}
