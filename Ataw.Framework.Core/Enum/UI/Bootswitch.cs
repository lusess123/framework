using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ataw.Framework.Core
{
	/// <summary>
	/// 主题
	/// </summary>
	public enum Bootswitch
	{
		/// <summary>
		/// 象牙白（默认）
		/// </summary>
		_default = 0,
		/// <summary>
		/// 扁平
		/// </summary>
		[Description("扁平")]
		_flatly = 1,
		/// <summary>
		/// 阿米莉亚
		/// </summary>
		[Description("阿米莉亚")]
		_amelia = 2,
		/// <summary>
		/// 蔚蓝
		/// </summary>
		[Description("蔚蓝")]
		_cerulean = 3,
		/// <summary>
		/// 红橙
		/// </summary>
		[Description("红橙")]
		_united = 4,
		/// <summary>
		/// 半机械人
		/// </summary>
		[Description("半机械人")]
		_cyborg = 5,
		/// <summary>
		/// 超级英雄
		/// </summary>
		[Description("超级英雄")]
		_superhero = 6,
		/// <summary>
		/// 黑暗宇宙
		/// </summary>
		[Description("黑暗宇宙")]
		_cosmo = 7,
		/// <summary>
		/// 愉阅
		/// </summary>
		[Description("愉阅")]
		_readable = 8
	}
}
