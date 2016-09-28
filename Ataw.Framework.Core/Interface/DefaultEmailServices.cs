using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ataw.Framework.Core.Interface
{
       [CodePlug("DefaultEmailServices", BaseClass = typeof(IEmailServices),
    CreateDate = "2016-01-08", Author = "", Description = "默认的邮件发送插件")]
    public class DefaultEmailServices : BaseEmailServices
    {
        private string fHost;
        private string fPort;
        private string fFormUser;
        private string fPassword;
        private List<string> fToUsers;
        private string fStrSubject;
        private string fStrBody;

        public override string Host
        {
            get { return this.fHost; }
        }

        public override string Port
        {
            get { return fPort; }
        }

        public override string FromUser
        {
            get { return fFormUser; }
        }

        public override string Password
        {
            get { return fPassword; }
        }

        public override IEnumerable<string> ToUsers
        {
            get { return fToUsers; }
            set { fToUsers = value.ToList(); }
        }

        public override string StrSubject
        {
            get { return fStrSubject; }
            set { fStrSubject = value; }
        }

        public override string StrBody
        {
            get { return fStrBody; }
            set { fStrBody = value; }
        }

        private string fGetStr(string key,string defaultStr) {
            return key.AppKv<string>(defaultStr);
        }

        public DefaultEmailServices()
        {
            this.fHost = fGetStr("SMTPHost", "smtp.exmail.qq.com");
            this.fPort = fGetStr("SMTPPort", "25");
            this.fFormUser = fGetStr("SMTPUser", "TestMessage@ataw.cn");
            //this.fPassword = fGetStr("SMTPPassword", "");
            this.fStrSubject = fGetStr("SMTPSubject", "测试标题{0}".AkFormat(DateTime.Now.ToString()));
            this.fPassword = fGetStr("SMTPPassword", "QWEasd123456123");
            this.fStrBody = fGetStr("SMTPBody", "测试正文{0}".AkFormat(DateTime.Now.ToString()));
        
        }



    }
}
