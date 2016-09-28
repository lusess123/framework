
using System.Collections.Generic;
using System.Data;
namespace Ataw.Framework.Core
{
    public interface IAtawServiceBuilder
    {
        /// <summary>
        ///  取全局key的值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        string GetAtawConfigsValue(string Key);
        /// <summary>
        ///  根据控制单元和key取值
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="FControlUnitID"></param>
        /// <returns></returns>
        string GetAtawControlUnitsValue(string Key, string FControlUnitID);
        /// <summary>
        /// 组织机构顶级菜单同步
        /// </summary>
        /// <param name="FcontrolUnitID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        string SynchronismRightIDbyFcontrolUnitID(string FcontrolUnitID, string UserID);

        /// <summary>
        /// 获取部门和用户的树形结构
        /// </summary>
        /// <param name="controlUnitID"></param>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable GetDepartmentAndUser(string controlUnitID, string userID, int type);

          /// <summary>
        /// 返回控制单元及其下级
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
       string GetControlUnitList(string value);

        #region xml的相关操作
        
       
        IList<BaseConfigItemInfo> GetAtawConfigs();
        bool delBaseConfigXml(string keys);
        bool InsertBaseConfigXml(BaseConfigItemInfo model);
        IList<DatabaseConfigItem> GetAtawDatabaseConfig();
        //string GetAtawDatabaseValue(string Name);
        bool delDatabaseConfig(string names);
        bool InsertlDatabaseXml(DatabaseConfigItem model);
        IList<ControlUnitItemInfo> GetAtawControlUnits();
        IList<ControlUnitItemInfo> GetAtawControlUnits(string FControlUnitID);
        IList<ControlUnitConfigItemInfo> GetAtawControlUnitsConfigItemList(string FControlUnitID);
 
       // Ataw_ConfigInfo GetAtawControlUnitsValue_Ataw_ConfigInfo(string FControlUnitID, string ConfigKey);
        IList<ProductItemInfo> GetAtawControlUnitsProductList(string FControlUnitID);
       // string GetAtawControlUnitsProductValue(string FControlUnitID, SQLUtil.DatabaseType DatabaseType, string key);
        bool delFcontrolUnitIDXML(string FControlUnitID);
        bool InsertlFControlUnitIDXml(ControlUnitItemInfo model);
        #endregion

        #region 附件相关
        string UploadArchiveFiles(string folder, string SourceFID, string UserID, string FControlUnitID);
        string DeleteArchives(string FID, string path);
        string GetArchivesList(string FSourceFID);
        void RemoveArchives(string SourceFIDs);
        #endregion

        /// <summary>
        /// 易耗品的工作流返回ApplicantID,FCreateTime
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        string ConsumptionPurchaseForWF(string FID);
    }
}
