using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Craw.Model
{
    public class FundData
    {
        private DateTime _Time;

        /// <summary>
        /// 資料日期
        /// </summary>
        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        private decimal _Price;

        /// <summary>
        /// 價錢
        /// </summary>
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private double? _RiseValue;
        /// <summary>
        /// 漲跌幅
        /// </summary>
        public double RiseValue
        {
            get
            {
                if (_RiseValue == null)
                    return -999;
                return _RiseValue.Value;
            }
            set { _RiseValue = value; }
        }

        private double? _RisePersent;
        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public double RisePersent
        {
            get
            {
                if (_RisePersent == null)
                    return -999;
                return _RisePersent.Value;
            }
            set { _RisePersent = value; }
        }


    }
}
