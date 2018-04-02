using Crawler.Craw.Interface;
using Crawler.Craw.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Craw
{
    class BankOfTaiwan : IFundCrawler
    {
        string[] _Tags;
        public BankOfTaiwan()
        {
            _Tags = new string[] { "日期", "淨值", "漲/跌", "漲跌幅(%)" };
            
        }

        public List<FundData> ParseFund(string html)
        {
            List<FundData> data = new List<FundData>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var tmp1 = doc.DocumentNode.SelectNodes(@"html/body/div/table/tr");
            var valueTable = tmp1[3].SelectNodes(@"td/table");
            var tdvalues = valueTable[0].SelectNodes(@"tr/td");

            bool r = true;
            for (int i = 0; i < _Tags.Length; i++)
            {
                if (tdvalues[i].InnerText != _Tags[i])
                {
                    r = false;
                    break;
                }
            }

            if (r == false)
                return data;

            var parseRows = valueTable[0].SelectNodes(@"tr");
            InsertParseData(data, parseRows);

            Console.WriteLine("");
            return data;

        }

        /// <summary>
        /// 將分析資料插入物件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parseRows"></param>
        private void InsertParseData(List<FundData> data, HtmlNodeCollection parseRows)
        {
            int count = 0;
            foreach (var pr in parseRows)
            {
                if (count == 0)
                {
                    count++;
                    continue;
                }

                FundData p = new FundData();
                var pCols = pr.SelectNodes(@"td");

                DateTime tmpTime;
                DateTime.TryParse(pCols[0].InnerText, out tmpTime);
                p.Time = tmpTime;

                double tmpPrice;
                double.TryParse(pCols[1].InnerText, out tmpPrice);
                p.Price = tmpPrice;

                double tmpRiseValue;
                double.TryParse(pCols[2].InnerText, out tmpRiseValue);
                p.RiseValue = tmpRiseValue;

                double tmpRisePersent;
                double.TryParse(pCols[3].InnerText, out tmpRisePersent);
                p.RisePersent = tmpRisePersent;

                data.Add(p);
                count++;
            }
        }
    }
}
