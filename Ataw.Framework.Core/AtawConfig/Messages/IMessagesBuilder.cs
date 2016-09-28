using System;
using System.Collections.Generic;
 

namespace Ataw.Framework.Core 
{ 
    public interface IMessagesBuilder  //站内信息的接口
    {
        /// <summary>
        /// 添加  ReadingStatus 0未读 1已读
        /// </summary>
        bool InsertMessage(string title, string messageBody, string url, int ReadingStatus, string ReceiverID, MessageTypeEnum Mtype, string FSourceModuleID,string FControlUnitID,string CreaterID);
         //删除
        int deleteMessage(string FID);
        //更新阅读状态
        bool UpdateReadingStatus(string FID, int ReadingStatus,string OperaterID);
    }
}
