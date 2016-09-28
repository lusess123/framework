using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Ataw.Framework.Core;

namespace Ataw.Right.Interface
{
    public interface IRight
    {
        /// <summary>
        /// 根据角色编号获取用户编号列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<string> GetUserIdsByRoleId(string roleId);

        /// <summary>
        /// 根据角色标识获取用户编号列表
        /// </summary>
        /// <param name="roleSign"></param>
        /// <returns></returns>
        IEnumerable<string> GetUserIdsByRoleSign(string roleSign);

        IEnumerable<string> GetUserIdsByRoleSign(string roleSign, string fControlUnitId);

        /// <summary>
        /// 根据用户名获取昵称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetNickNameByUserId(string userId);
        /// <summary>
        /// 根据用户名获取角色标识
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        IDictionary<string, List<string>> GetRoleSignsByUserIds(IEnumerable<string> users);

        //string InsertDepartMent(string name);

        int GetGroupCount(string WhereCondition, string OrderByExpression);

        //void DeleteCache(GetCacheType CacheType);

        int GetUsersDetailsByQueryCount(string WhereCondition);

        IList<TreeCodeTableModel> GetRightControlUnits(string controlUnitID);

        /// <summary>
        /// 根据用户ID获取部门名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetDepartmentNameByUserId(string userId);
    }

    //[DescriptionAttribute("获取缓存类型")]
    //public enum GetCacheType
    //{
    //    [DescriptionAttribute("修改基础菜单、自定义菜单时更新")]
    //    Ataw_Menus_ButtonInfo_New,
    //    [DescriptionAttribute("修改角色授权用户授权时更新")]
    //    Ataw_RightsInfo_New,
    //    [DescriptionAttribute("修改角色授权用户授权时更新")]
    //    Ataw_RightsWithButtonInfo_New,
    //    [DescriptionAttribute("修改用户信息时更新")]
    //    AvatarPath_New,
    //    [DescriptionAttribute("修改组织，客商信息管理时更新")]
    //    Ataw_GroupInfo_New,
    //    [DescriptionAttribute("修改基础菜单时更新/换数据库连接串时更新")]
    //    Ataw_Menus_ButtonInfoList_New,
    //    Ataw_Menus_ButtonInfoList
    //}


}
