using Crawler.Craw.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Craw.Model;
using HtmlAgilityPack;

namespace Crawler.Craw
{
    class FundRich : IFundCrawler
    {
        string[] _Tags;
        public FundRich()
        {
            _Tags = new string[] { "日期", "淨值" };
        }
        public List<FundData> ParseFund(string html)
        {
            List<FundData> data = new List<FundData>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var divContainers = doc.DocumentNode.SelectNodes(@"//div[@class='container']");

            if (divContainers == null || divContainers.Count < 2)
                return data;

            var targetContainer = divContainers[1];

            var valueLayers = targetContainer.SelectNodes(@"div/div/div/div/div");
            if (valueLayers.Count < 6)
                return data;
            var valueLayer = valueLayers[5];
            var valueTables = valueLayer.SelectSingleNode(@".//tbody");
            var tdValues = valueTables.SelectNodes(@"tr/td");

            bool r = true;
            for(int i = 0; i < _Tags.Length; i++)
            {
                if (tdValues[i].InnerText != _Tags[i])
                {
                    r = false;
                    break;
                }
            }
            if (r == false)
                return data;

            var parseRows = valueTables.SelectNodes(@"tr");
            InsertParseData(data, parseRows);
            

            
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

                decimal tmpPrice;
                decimal.TryParse(pCols[1].InnerText, out tmpPrice);
                p.Price = tmpPrice;

                //double tmpRiseValue;
                //double.TryParse(pCols[2].InnerText, out tmpRiseValue);
                //p.RiseValue = tmpRiseValue;

                //double tmpRisePersent;
                //double.TryParse(pCols[3].InnerText, out tmpRisePersent);
                //p.RisePersent = tmpRisePersent;

                data.Add(p);
                count++;
            }
        }

    }
}
