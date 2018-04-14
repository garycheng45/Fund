using Crawler.Enum.DBEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model.DataModel
{
    /// <summary>
    /// 爬蟲資料來源設定
    /// </summary>
    public class NotifyData
    {
        private string _Name;
        /// <summary>
        /// 資料名稱
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _NotifyType;

        public int NotifyType
        {
            get { return _NotifyType; }
            set { _NotifyType = value; }
        }


        private DataResource _Resource;
        /// <summary>
        /// 資料來源
        /// </summary>
        public DataResource Resource
        {
            get { return _Resource; }
            set { _Resource = value; }
        }
        
        private string _Url;
        /// <summary>
        /// 資料源網址
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
    }
}
