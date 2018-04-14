using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Enum.DBEnum
{
    /// <summary>
    /// 提醒類別
    /// </summary>
    public enum NotifyType
    {
        /// <summary>
        /// 基金
        /// </summary>
        Fund,
        /// <summary>
        /// 匯率
        /// </summary>
        Rate
    }

    /// <summary>
    /// 爬蟲資料來源
    /// </summary>
    public enum DataResource
    {
        /// <summary>
        /// 台銀
        /// </summary>
        BankOfTaiwan,
        /// <summary>
        /// 基富通
        /// </summary>
        FundRich,
        /// <summary>
        /// 鉅亨網
        /// </summary>
        FundYes
    }

    /// <summary>
    /// 漲跌變化
    /// </summary>
    public enum Change
    {
        /// <summary>
        /// 漲
        /// </summary>
        Rise,
        /// <summary>
        /// 跌
        /// </summary>
        Fall
    }
}
