using Crawler.Craw.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Craw.Interface
{
    interface IFundCrawler
    {
        List<FundData> ParseFund(string html);
    }
}
