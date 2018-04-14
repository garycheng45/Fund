using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model.DataModel
{
    class Member
    {
        public Member()
        {
            _NotifyConfig = new List<NotifyConfig>();
        }
        private string _Account;

        public string Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        private List<NotifyConfig> _NotifyConfig;
        /// <summary>
        /// 使用者設定的到價通知
        /// </summary>
        public List<NotifyConfig> NotifyConfig
        {
            get { return _NotifyConfig; }
            set { _NotifyConfig = value; }
        }

    }
}
