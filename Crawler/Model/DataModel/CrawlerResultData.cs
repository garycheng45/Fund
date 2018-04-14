using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model.DataModel
{
    public class CrawlerResultData
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private double _Value;

        public double Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

    }
}
