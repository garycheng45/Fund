using Crawler.Enum.DBEnum;
using Crawler.Properties;
using Crawler.SystemConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Mail
{
    public class SmtpConfig
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }

    public class SendEmail
    {
        string _MailConfigPath = SystemInfo.etc + "MailServerConfig.json";
        SmtpConfig _Config;

        public SendEmail()
        {
            _Config = new SmtpConfig();
            ReadSmtpConfig();
        }

        /// <summary>
        /// 寄送到價通知
        /// </summary>
        /// <param name="type">通知類型 基金 匯率</param>
        /// <param name="notifyString">通知資訊</param>
        /// <param name="userEmail">使用者email</param>
        /// <param name="userName">使用者名稱</param>
        /// <returns></returns>
        public bool SendNotifyMail(NotifyType type, IEnumerable<string> notifyString, string userEmail, string userName = "")
        {
            var client = CreateEmailObj();
            StringBuilder sb = new StringBuilder();
            string subject = "";
            if (type == NotifyType.Fund)
            {
                subject = "基金到價通知";
            }
            else if (type == NotifyType.Rate)
            {
                subject = "匯率到價通知";
            }

            var from = CreateMailAddress(_Config.Account, "博");

            var to = CreateMailAddress(userEmail);

            MailMessage msg = new MailMessage(from, to);

            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            string bodyTitle = "{0}您好:" + Environment.NewLine + Environment.NewLine;
            if (userName == "")
                msg.Body = string.Format(bodyTitle, "使用者");
            else
                msg.Body = string.Format(bodyTitle, userName);

            msg.Body += GetEmailBody(notifyString);
            msg.BodyEncoding = Encoding.UTF8;

            msg.IsBodyHtml = false;

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        private string GetEmailBody(IEnumerable<string> msg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var m in msg)
            {
                sb.Append(m + Environment.NewLine);
                
            }
            return sb.ToString();
        }

        private MailAddress CreateMailAddress(string account, string name = "")
        {
            if (name == "")
                name = account;

            MailAddress ma = new MailAddress(account, name);
            return ma;
        }

        private void ReadSmtpConfig()
        {
            string json = File.ReadAllText(_MailConfigPath, Encoding.Default);
            _Config = JsonConvert.DeserializeObject<SmtpConfig>(json);
            
        }

        private SmtpClient CreateEmailObj()
        {
            SmtpClient client = new SmtpClient(_Config.Smtp);
            client.Port = _Config.Port;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_Config.Account, _Config.Password);
            return client;
        }
    }
}
