
namespace Ataw.Framework.Core
{
    public interface IAtawRightBuilder
    {
        RegNameList<FunRightItem> CreateFunRight(string modelName);
        //-1未续费，-2没权限，1成功
        int MenuRightCheck(string modelName);
        //
        string GetUserNameByUserId(string userId);
        string GetDeptNameByDepId(string depId);
        string GetNickNameByUserId(string userId);
        string GetDepartmentIDByUserID(string userId);
        // #region 车组相关
        ////返回已授权的车组ID集合
        // string[] GetPermissionCarGroup(string userId);
        // /// <summary>
        // /// 新增车组时添加数据权限
        // /// </summary>
        // /// <param name="userid">userid</param>
        // /// <param name="FControlUnitID">组织id</param>
        // /// <param name="CarGroupID">车组fid</param>
        // /// <param name="IsDefault">true时全组织用户添加，false只对此user添加</param>
        // /// <returns></returns>
        // bool InsertCarGroupPermission(string userid, string FControlUnitID, string CarGroupID, bool IsDefault);
        // /// <summary>
        // /// 删除车组时去除数据权限
        // /// </summary>
        // /// <param name="CarGroupID"></param>
        // /// <returns></returns>
        // bool DeleteCarGroupPermission( string CarGroupID);

        // #endregion
        // //给gps项目
        // string[] GetList(string UserName, string PassWord, string FControlUnitID, string PFID);
        // string GetGPSVehicles(string FControlUnitID);
        // void DeleteGPSVehicles(string FControlUnitID);
        // string GetDepartmentList(string UserID, string FControlUnitID);
        // string GetProRight(string UserName, string Pass, string FControlUnitID, string PFID);
        // string GetProRight(string UserName, string FControlUnitID, string PFID);
        // string LoginUser(string UserName, string Pass, string FControlUnitID);
        // bool UpdateUser(string userId, string newPassword, string oldPassword);


        // //gps设备
        // bool UpdateGPSSetStatus(string FID, int status);
        // string GetGPSList(string FControlUnitID, int Status);
        // IList<TreeCodeTableModel> GetRightFControlUnit(string FcontrolUnitID);
        //  IList<TreeCodeTableModel> GetRightDeparment(string UserID);

    }
}
