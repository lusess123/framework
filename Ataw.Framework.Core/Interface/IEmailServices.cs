using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Ataw.Framework.Core;

namespace Ataw.Framework.Core.Interface
{
  public  interface IEmailServices
    {
        string Host { get;}
        string Port { get;}

        string FromUser { get; }

        string Password { get; }

        IEnumerable<string> ToUsers { get; set; }

        string StrSubject { get; set; }

        string StrBody { get; set; }

        void Send();
    }

    public abstract class BaseEmailServices : IEmailServices
    {
        public abstract string Host
        {
            get;
        }

        public abstract string Port
        {
            get;
        }

        public abstract string FromUser
        {
            get;
        }

        public abstract string Password
        {
            get;
        }

        public abstract IEnumerable<string> ToUsers
        {
            get;
            set;
        }

        public abstract string StrSubject
        {
            get;
            set;
        }

        public abstract string StrBody
        {
            get;
            set;
        }

        public void Send()
        {


            StringBuilder _log = new StringBuilder();
            ToUsers.ToList().ForEach((u) => {
                SmtpClient client = new SmtpClient(Host);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(FromUser, Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(FromUser, u, StrSubject, StrBody);
                message.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
                message.IsBodyHtml = true;
               
                try
                {
                    client.SendCompleted += client_SendCompleted;
                    client.Send(message);
                    _log.AppendLine();
                    _log.AppendLine( "form:{1}{0}to:{2}{0}title:{3}{0}content:{4}{0}".AkFormat(Environment.NewLine, FromUser, u,StrSubject, StrBody));
                   

                }
                catch (Exception ex)
                {
                    AtawTrace.WriteFile(LogType.EmailLog, JSON.Instance.ToJSON(this) +  ex.ExcepString());
                   // AtawDebug.ThrowAtawException(JSON.Instance.ToJSON(this) + ex.ExcepString(), this);
                }

            });
            if (!_log.ToString().IsEmpty())
            {
                string fileName = "AtawLogs\\{0}.txt".AkFormat("EmailSendLog");
                AtawTrace.WriteStringFile(fileName, _log.ToString());
            }
           
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
             
            //throw new NotImplementedException();
            AtawTrace.WriteFile(LogType.EmailLog, JSON.Instance.ToJSON(sender));
        }
    }
}
