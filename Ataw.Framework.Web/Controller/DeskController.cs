using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ataw.Framework.Core;
using Newtonsoft.Json;

namespace Ataw.Framework.Web
{
    public class DeskController : AtawBaseController
    {
        public string Right(string ds, string xml, string keyValue)
        {
            AtawDebug.AssertNotNullOrEmpty(xml, "亲 ,modulexml 注册名不可以为空的", this);
            ModuleConfig mc = xml.SingletonByPage<ModuleConfig>();
            mc.Mode = ModuleMode.None;
            IDeskMergeBuilder deskBuilder = AtawIocContext.Current.FetchInstance<IDeskMergeBuilder>("Core");
            var deskForms = deskBuilder.GetUserDeskFormList();
            List<BaseFormConfig> mForms = new List<BaseFormConfig>();
            foreach (var form in mc.Forms)
            {
                var dForm = deskForms.FirstOrDefault(a => a.FormName == form.Name);
                if (dForm != null)
                {
                    form.Order = dForm.Order.ToString();
                    //form.Width = dForm.
                    mForms.Add(form);
                }
                else
                {
                    mForms.Add(form);
                }
            }
            mc.Forms = mForms.OrderBy(a => a.Order.Value<int>()).ToList();

            AtawBasePageViewCreator pageCreator = ("Module" + "PageView").SingletonByPage<AtawBasePageViewCreator>();
            pageCreator.Initialize(mc, JsonConvert.DeserializeObject<DataSet>(ds ?? ""), keyValue, "", false);
            var apcv = pageCreator.Create();
            apcv.RegName = xml;

            apcv.ExtData = GetDeskData(deskBuilder);
            return ReturnJson(apcv);
        }

        private ModuleConfig SetDeskSetting(ModuleConfig config)
        {
            config.Forms = config.Forms.OrderBy(a => a.Order).ToList();
            return config;
        }

        private void TryThowIgro(Action a)
        {
            try
            {
                a();
            }
            catch (Exception ex)
            {
                AtawTrace.WriteFile(LogType.Desk,
                      "{0}{1}{2}".AkFormat(ex.Message, Environment.NewLine, ex.StackTrace)
                    );
                //---------------异常处理继续往下执行，以后这里需要记录日志
            }
        }

        private Dictionary<string, object> GetDeskData(IDeskMergeBuilder deskBuilder)
        {
            object o = new object();
            Dictionary<string, object> d = new Dictionary<string, object>();
            // deskBuilder = AtawIocContext.Current.FetchInstance<IDeskMergeBuilder>("Core");
            TryThowIgro(() =>
            {
                var emailData = deskBuilder.GetEmailListByCurrent();
                d.Add("emailData", emailData ?? o);
            });

            TryThowIgro(() =>
            {
                var meetingData = deskBuilder.GetMeetingListByCurrent();
                d.Add("meetingData", meetingData ?? o);
            });

            TryThowIgro(() =>
            {
                var noticeList = deskBuilder.GetNoticeListByCurrent();
                d.Add("noticeList", noticeList ?? o);
            });
            TryThowIgro(() =>
            {
                d.Add("merge", deskBuilder.GetMergeInfo() ?? o);
            });
            TryThowIgro(() =>
            {
                var moduleList = deskBuilder.GetModuleListByCurrent();
                d.Add("moduleList", moduleList ?? o);
            });


            return d;
        }

        public string GetToDoWork()
        {
            IMessage message = new MessageService();
            var items = message.GetToDoWork();
            return ReturnJson(items);
        }

        public string GetNewMessages(string latestTime)
        {
            var msgService = new MessageService();
            var rows = msgService.GetNewMessages(latestTime);
            return ReturnJson(rows);
        }

        public string GetMessageType()
        {
            var dict = ObjectUtil.GetEnumFields(typeof(MessageType));
            var result = new List<Object>();
            dict.ToList().ForEach(x =>
            {
                result.Add(new { Name = x.Value, Value = x.Key });
            });
            return ReturnJson(result);
        }

    }
}
