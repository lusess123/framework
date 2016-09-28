
using System.Collections;
using System.Collections.Generic;
namespace Ataw.Framework.Core
{
    public interface IDeskMergeBuilder
    {
        IDictionary<string, SingleMergeInfo> GetMergeInfo();
        IEnumerable<NoticeInfo> GetNoticeListByCurrent();
        IEnumerable<ModuleInfo> GetModuleListByCurrent();
        IEnumerable<MeetingInfo> GetMeetingListByCurrent();
        IEnumerable GetEmailListByCurrent();
        IEnumerable<UserOnlineInfo> GetUserOnlineListByCurrent();
        IEnumerable<DeskForm> GetUserDeskFormList();
    }
}
