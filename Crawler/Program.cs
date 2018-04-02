using Crawler.Craw;
using Crawler.Craw.Interface;
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
            WebBase a = new WebBase();
            a.SetURL("http://fund.bot.com.tw/w/wb/wb02a.djhtm?customershowall=0&a=TLZ64-0532");
            //string html = a.GetHtmlContent();
            string html = File.ReadAllText(testFilePath);
            IFundCrawler craw = new FundRich();
            var obj = craw.ParseFund(html);
            Console.ReadLine();
        }
        
    }
}
