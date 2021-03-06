﻿using Crawler.Enum.DBEnum;
using Crawler.SystemConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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

        public bool SendNotifyMail(NotifyType type, IEnumerable<string> notifyString, string userEmail)
        {
            var client = CreateEmailObj();
            StringBuilder sb = new StringBuilder();
            string subject = "";
            if(type == NotifyType.Fund)
            {
                subject = "基金到價通知";
            }
            else if(type == NotifyType.Rate)
            {
                subject = "匯率到價通知";
            }

            var from = CreateMailAddress(_Config.Account, "博");

            var to = CreateMailAddress(_Config.Account);

            MailMessage msg = new MailMessage(from, to);

            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = GetEmailBody(notifyString);
            msg.BodyEncoding = Encoding.UTF8;

            msg.IsBodyHtml = false;

            try
            {
                client.Send(msg);
            }
            catch (Exception)
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
                sb.Append(m);
            }
            return sb.ToString();
        }

        private MailAddress CreateMailAddress(string account,string name = "")
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
