using System.Linq;
using System.Net.Mail;
using System;
namespace Ataw.Framework.Core
{
    public static class EmailUtil
    {
        private static string SMTPHost;
        private static string SMTPPort;
        private static string SMTPUser;
        private static string SMTPPassword;
        static EmailUtil()
        {
            var appSettings = AtawAppContext.Current.ApplicationXml.AppSettings;
            if (appSettings != null)
            {
                SMTPHost = appSettings.FirstOrDefault(m => m.Key == "SMTPHost").Value;
                SMTPPort = appSettings.FirstOrDefault(m => m.Key == "SMTPPort").Value;
                SMTPUser = appSettings.FirstOrDefault(m => m.Key == "SMTPUser").Value;
                SMTPPassword = appSettings.FirstOrDefault(m => m.Key == "SMTPPassword").Value;
            }
        }
        public static void SendSMTPEMail(string strto, string strSubject, string strBody)
        {
            SmtpClient client = new SmtpClient(SMTPHost);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage(SMTPUser, strto, strSubject, strBody);
            message.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            message.IsBodyHtml = true;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                AtawDebug.ThrowAtawException(ex.Message, null);
            }
        }
    }
}
