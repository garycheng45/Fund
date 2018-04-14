using Crawler.Enum.DBEnum;

namespace Crawler.Model.DataModel
{
    /// <summary>
    /// 使用者設定的到價通知
    /// </summary>
    public class NotifyConfig
    {
        private string _Name;
        /// <summary>
        /// 通知項目名稱
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private Change _Change;
        /// <summary>
        /// 漲跌到價通知
        /// </summary>
        public Change Change
        {
            get { return _Change; }
            set { _Change = value; }
        }

        private decimal _Value;
        /// <summary>
        /// 通知項目的URL
        /// </summary>
        public decimal Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
