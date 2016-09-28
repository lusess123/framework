using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ataw.Framework.Core
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string FID
        {
            get;
            set;
        }

        /// <summary>
        /// 发送人ID
        /// </summary>
        public string SendID
        {
            get;
            set;
        }

        /// <summary>
        /// 接收人ID，可能一个，多个或所有
        /// </summary>
        public string ReceiveID
        {
            get;
            set;
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantID
        {
            get;
            set;
        }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizationID
        {
            get;
            set;
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string ApplicationID
        {
            get;
            set;
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleID
        {
            get;
            set;
        }

        /// <summary>
        /// 内容ID，如来自订单的消息，那么此ID为订单的主键
        /// </summary>
        public string SourceID
        {
            get;
            set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// 消息类别
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 业务分类
        /// </summary>
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// 消息隐私，对谁可见
        /// </summary>
        public string Privacy
        {
            get;
            set;
        }

        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsHandled
        {
            get;
            set;
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority
        {
            get;
            set;
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

    }
}