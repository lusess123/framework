using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public interface IGroupBuilder
    {
        /// <summary>
        /// 根据组织id获取车组
        /// </summary>
        /// <param name="FcontrolUnitID">组织id</param>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        IList<TreeCodeTableModel> GetGroup(string FcontrolUnitID,string UserId);


        /// <summary>
        /// 根据组织id获取车组
        /// </summary>
        /// <param name="FcontrolUnitID">组织id</param>
        /// <returns></returns>
        IList<TreeCodeTableModel> GetGroup(string FcontrolUnitID);

        /// <summary>
        /// 根据条件搜索车辆信息
        /// </summary>
        /// <param name="FcontrolUnitID">组织id</param>
        /// <param name="GroupId">车组id</param>
        /// <param name="Name">车牌号</param>
        /// <param name="GPS">GPS终端号</param>
        /// <returns></returns>
        IList<MonitorVehicle> GetVehicles(string FcontrolUnitID, string GroupId, string Name, string GPS);


        /// <summary>
        /// 根据条件搜索车辆信息(终端号可为空)  状态从枚举取
        /// </summary>
        /// <param name="FcontrolUnitID">组织id</param>
        /// <param name="Name">车牌号</param>
        ///  <param name="AuditStatus">审核状态</param>
        ///   <param name="ServiceStatus">服务状态</param>
        /// <param name="pageSize">条数</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        IList<MonitorVehicle> GetVehiclesForMRP(string FcontrolUnitID,string Name, string AuditStatus,string ServiceStatus, int pageSize, int pageIndex, out int total);




        /// <summary>
        /// 根据条件搜索车辆信息(终端号不可为空)
        /// </summary>
        /// <param name="FcontrolUnitID">组织id</param>
        /// <param name="GroupId">车组id</param>
        /// <param name="Name">车牌号</param>
        /// <param name="GPS">GPS终端号</param>
        /// <param name="pageSize">条数</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        IList<MonitorVehicle> GetVehicles(string FcontrolUnitID, string GroupId, string Name, string GPS, int pageSize, int pageIndex, out int total);

        /// <summary>
        /// 根据车牌号修改
        /// </summary>
        /// <param name="FID"></param>
        /// <param name="FGpsTerminalNo"></param>
        /// <param name="FSimCardNo"></param>
        /// <returns></returns>
        void UpdateVehicle(string FID, string FGpsTerminalNo, string FSimCardNo);

        /// <summary>
        /// 根据父级节点获取其下所有子节点
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        string[] GetGroupIDsByPID(string ParentID);


        /// <summary>
        /// 审核/冲销GPS服务申请单时更改VM_Vehicle
        /// </summary>
        /// <param name="FID">车辆fid</param>
        /// <param name="FGpsTerminalNo">gps终端号</param>
        /// <param name="FSimCardNo">sim</param>
        /// <param name="FControlUnitID">组织id</param>
        /// <param name="AuditStatus">审核状态</param>
        /// <param name="ServiceStatus">服务状态</param>
        void UpdateVehicle(string FID, string FGpsTerminalNo, string FSimCardNo, string FControlUnitID, string AuditStatus, string ServiceStatus);

        /// <summary>
        /// 根据组织获取所有可以查看车辆
        /// </summary>
        /// <param name="FcontrolUnitID"></param>
        /// <returns>返回(终端号,车辆类型|终端号,车辆类型)集合</returns>
        string GetRightVehicles(string FcontrolUnitID);
    }


    /// <summary>
    /// 车辆信息
    /// </summary>
    public class MonitorVehicle
    {
        /// <summary>
        /// 车辆id
        /// </summary>
        public string FID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string FLicenseID { get; set; }

        /// <summary>
        /// GPS终端号
        /// </summary>
        public string FGpsTerminalNo { get; set; }

        /// <summary>
        /// SIM卡号
        /// </summary>
        public string FSimCardNo { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public string CarState { get; set; }
    }
}
