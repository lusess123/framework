using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{//权限的key类型
    public enum RightKeyType
    {
        [Description("产品")]
        product = 0,
        [Description("模块")]
        module = 1,
        [Description("页面")]
        index = 2,
        [Description("按钮")]
        button = 3
    }

    public enum MenuRightType
    {
        /// <summary>
        /// 选择该选项，点击菜单局部刷新
        /// </summary>
        [Description("业务模块")]
        Business = 0,
        /// <summary>
        /// 系统基础设置模块
        /// </summary>
        [Description("基础资料")]
        Basic = 1,
        /// <summary>
        /// 系统权限模块
        /// </summary>
        [Description("权限系统")]
        Right = 2,
        /// <summary>
        /// 选择该选项，填写所属类别名称，在外部系统中权限验证及显示（如GPS）
        /// </summary>
        [Description("其他")]
        Other = 3,
        /// <summary>
        /// 选择该选项，不显示在左侧菜单结构，只做权限验证试用
        /// </summary>
        [Description("业务子系统")]
        ChildBusiness = 4
    }
}
