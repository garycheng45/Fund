using Crawler.Craw.Interface;
using Crawler.Enum.DBEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Craw
{
    static class CrawlerCreateFactory
    {
        public static IFundCrawler CreateFundCrawler(DataResource resource)
        {
            IFundCrawler crawler = null;
            switch (resource)
            {
                case DataResource.BankOfTaiwan:
                    crawler = new BankOfTaiwan();
                    break;
                case DataResource.FundRich:
                    crawler = new FundRich();
                    break;
                case DataResource.FundYes:
                    break;
                default:
                    break;
            }
            return crawler;
        }
    }
}
