using System.Data;
namespace Ataw.Framework.Core
{
    public class AtawBaseUploadOptionCreator : AtawDecodeOptionCreator
    {
        protected BaseUploadOptions fBaseUploadOptions;

        public override BaseOptions Create()
        {
            //var options = base.Create();
            //var UploadType = base.Config.ControlType;
            //var options = base.Create() as BaseUploadOptions;
            //UploadConfig uploadC = new UploadConfig();
            //string UploadInfo = FileManagementUtil.getFileUploadInfo(UploadType.ToString());
            //uploadC.FileLength = System.Convert.ToInt64(UploadInfo.Split(' ')[0]);
            //uploadC.FileExtension = UploadInfo.Split(' ')[1];
            //uploadC.FilePath = UploadInfo.Split(' ')[2];
            //if (UploadType.ToString() == "ImageUpload")
            //{
            //    uploadC.ImageSizeHeight = System.Convert.ToInt32(UploadInfo.Split(' ')[3]);
            //    uploadC.ImageSizeWidth = System.Convert.ToInt32(UploadInfo.Split(' ')[4]);
            //}
            //options.Upload = this.Config.Upload;
            // options.Upload = uploadC;
            base.Create();
            if (this.Config.Upload != null && !this.Config.Upload.UploadName.IsEmpty())
            {
                var config = FileManagementUtil.GetFileUploadConfig(this.Config.Upload.UploadName);
                fBaseUploadOptions.FileSize = config.MaxSize;
                fBaseUploadOptions.FileExtension = config.Extensions;
                fBaseUploadOptions.StorageName = this.Config.Upload.StorageName;
                fBaseUploadOptions.UploadName = this.Config.Upload.UploadName;
                fBaseUploadOptions.ImageSizeHeight = config.ImageSizeHeight;
                fBaseUploadOptions.ImageSizeWidth = config.ImageSizeWidth;
                fBaseUploadOptions.HasDocumentCenter = this.Config.Upload.HasDocumentCenter;

                //if (fBaseUploadOptions is AtawImageDetailOptions)
                //{
                   // var options = base.Create();
                    if (this.Config.Upload != null && this.Config.Upload.StorageName != null)
                    {
                        fBaseUploadOptions.StorageName = this.Config.Upload.StorageName;
                    }
                    if (!fBaseUploadOptions.StorageName.IsEmpty())
                    {
                        DataTable dt = this.PageView.Data.Tables[this.FormView.TableName];
                        if (dt != null && dt.Columns.Contains(this.Config.Name))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string _str = row[this.Config.Name].ToString();
                                ResourceArrange arrange = _str.SafeJsonObject<ResourceArrange>();
                                if (arrange != null)
                                {
                                    if (arrange.ResourceInfoList != null)
                                    {
                                        foreach (ResourceInfo info in arrange.ResourceInfoList)
                                        {
                                            string _fid = info.FileId;
                                            string _ext = info.ExtName;
                                            int _pathId = info.PathID;

                                            string _http = FileManagementUtil.GetFullPath(fBaseUploadOptions.StorageName, FilePathScheme.Http, _pathId, _fid, _ext);
                                            // string sConfig = this.fTextOptions.
                                            info.HttpPath = _http;
                                        }

                                        row[this.Config.Name] = AtawAppContext.Current.FastJson.ToJSON(arrange);


                                    }
                                }
                            }
                        }
                    }
                //}

                if (fBaseUploadOptions is SingleImageUploadOptions)
                {
                    var _op = (SingleImageUploadOptions)fBaseUploadOptions;
                    if (!config.ImageCutGroupName.IsEmpty())
                    {
                        var _list = FileManagementUtil.GetImageCutsByCutConfigName(config.ImageCutGroupName);

                        //var _cuts = FileManagementUtil.FileManagementConfig.ImageCutGroups;
                        //if (_cuts != null && _cuts.Count > 0)
                        //{
                        //    ImageCutGroup cut = _cuts.FirstOrDefault(a => a.Name == config.ImageCutName);
                        //    AtawDebug.Assert(cut != null && cut.ImageCutList.Count > 0, string.Format(ObjectUtil.SysCulture,
                        //        "名称为{0}的截图配置不存在,或者没有截图配置项", config.ImageCutName), this);
                        //var _cconfig = _list[0];
                        //_op.ImageSizeHeight = _cconfig.ImageSizeHeight;
                        //_op.ImageSizeWidth = _cconfig.ImageSizeWidth;
                        //如果ImageSizeHeight和ImageSizeWidth没有配置那么默认为0，如果为0那么将不限制你要裁剪的大小比例
                        if (config.ImageSizeHeight != null && !config.ImageSizeHeight.Equals("") && config.ImageSizeWidth != null && !config.ImageSizeWidth.Equals(""))
                        {
                            _op.ImageSizeHeight = config.ImageSizeHeight;
                            _op.ImageSizeWidth = config.ImageSizeWidth;
                        }
                        else {
                            _op.ImageSizeHeight = 0;
                            _op.ImageSizeWidth = 0;
                        }

                        //}
                    }
                    // ((SingleImageUploadOptions)fBaseUploadOptions).ImageSizeHeight = config.ImageSizeHeight;
                    //((SingleImageUploadOptions)fBaseUploadOptions).ImageSizeWidth = config.ImageSizeWidth;
                    _op.IsCut = this.Config.Upload.IsCut;
                }
            }
            string colName = this.Config.Name;
            string tableName = this.FormView.TableName;
            var ds = this.PageView.Data;
            //if (PageStyle == PageStyle.Detail || PageStyle == Core.PageStyle.List)
            //{
            //if (ds.Tables.Contains(tableName))
            //{
            //    foreach (DataRow row in ds.Tables[tableName].Rows)
            //    {
            //        string val = row[colName].ToString();
            //        if (!val.IsEmpty())
            //        {
            //            bool isError = false;
            //            ResourceInfo resourceInfo = null;
            //            try
            //            {
            //                resourceInfo = AtawAppContext.Current.FastJson.ToObject<ResourceInfo>(val);
            //            }
            //            catch
            //            {
            //                isError = true;
            //            }
            //            if (!isError)
            //            {
            //                string fileStorageName = this.Config.Upload.StorageName;
            //                string fullPath = FileManagementUtil.GetFullPath(fileStorageName, FilePathScheme.Http,
            //                    resourceInfo.PathID, resourceInfo.FileId, resourceInfo.ExtName);
            //                row[colName] = fullPath;
            //            }
            //        }
            //        //}
            //    }

            // }
            return fBaseUploadOptions;
        }
    }
}
