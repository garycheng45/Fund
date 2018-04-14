using Crawler.Craw;
using Crawler.Craw.Interface;
using Crawler.Model.dao.Json;
using Crawler.WebCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        static string testFilePath = @"D:\practice\Fund\Crawler\fundrich.txt";
        static void Main(string[] args)
        {
            var a = new NotifyDataModel();
            var b = a.GetNotifyData();

            var c = new NotifyConfigModel();
            var d = c.GetMemberNotifyConfig();
            Console.ReadLine();
        }

        private static void TestLs()
        {
            string a = @"D:\practice\Fund\Crawler\Sample\Member";
            var list = Directory.GetFiles(a);
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }
        }

        static void TestCrawler()
        {
            WebBase a = new WebBase();
            a.SetURL("http://fund.bot.com.tw/w/wb/wb02a.djhtm?customershowall=0&a=TLZ64-0532");
            //string html = a.GetHtmlContent();
            string html = File.ReadAllText(testFilePath);
            IFundCrawler craw = new FundRich();
            var obj = craw.ParseFund(html);
        }

    }
}
