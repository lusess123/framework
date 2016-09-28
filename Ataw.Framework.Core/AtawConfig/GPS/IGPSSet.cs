using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ataw.Framework.Core
{
    public interface IGPSSet
    {
       /// <summary>
       /// 获取Sim卡集合
       /// </summary>
       /// <param name="simNum">Sim卡号</param>
       /// <returns></returns>
        IEnumerable<string> SIMCard(string simNum);

      /// <summary>
      /// 获取终端号集合
      /// </summary>
      /// <param name="terminalNum">终端号</param>
      /// <returns></returns>
        IEnumerable<string> GPSSet(string terminalNum);


        /// <summary>
        /// 更新sim卡状态，  状态参考枚举GPSSIMStatus
        /// </summary>
        /// <param name="FSimCardNum"></param>
        /// <param name="status"></param>
        void UpdateSimCardStatus(string FSimCardNum,int status);
        /// <summary>
        /// 更新gps终端状态， 状态参考枚举GPSSetStatus
        /// </summary>
        /// <param name="FTerminalNum"></param>
        /// <param name="status"></param>
        void UpdateGPSSetStatus(string FTerminalNum, int status);
    }
}
