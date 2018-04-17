using Crawler.Craw;
using Crawler.Craw.Interface;
using Crawler.Craw.Model;
using Crawler.Model.dao;
using Crawler.Model.dao.Json;
using Crawler.Model.DataModel;
using Crawler.WebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model
{
    class PriceNotify
    {
        AbsGetNotifyConfigData _NotifyConfigObj;
        AbsGetNotifyData _NotifyObj;
        List<Member> _NotifyConfigDatas;
        List<NotifyData> _NotifyData;
        List<CrawlerResultData> _CrawlerData;
        /// <summary>
        /// Name 價錢: 目前價錢 高於(低於)設定價錢 設定價錢
        /// </summary>
        string _NotifyMessage = "{0} 價錢: {1} {2}設定價錢 {3}";
        public PriceNotify()
        {
            _NotifyConfigObj = new NotifyConfigModel();
            _NotifyObj = new NotifyDataModel();
            _CrawlerData = new List<CrawlerResultData>();
        }

        public void PriceNotifyProcess()
        {
            DoDataPrepare();
            StartPriceCompare();
        }

        public void DoDataPrepare()
        {
            _NotifyConfigDatas = GetNotifyConfig();
            _NotifyData = GetNotifyData();
            List<string> crawerNames = _NotifyConfigObj.GetDistinctNotifyName(_NotifyConfigDatas);
            _NotifyData = _NotifyObj.GetConfigNotify(_NotifyData, crawerNames);
            DoCrawer();
        }

        /// <summary>
        /// 根據設定將進行資料爬蟲
        /// </summary>
        public void DoCrawer()
        {
            WebBase wb = new WebBase();
            foreach (var nd in _NotifyData)
            {
                wb.SetURL(nd.Url);
                string html = wb.GetHtmlContent(nd.Url);
                if(nd.NotifyType == Enum.DBEnum.NotifyType.Fund)
                {
                    IFundCrawler crawer = CrawlerCreateFactory.CreateFundCrawler(nd.Resource);
                    var fundDatas =  crawer.ParseFund(html);
                    if (fundDatas.Count == 0)
                        continue;
                    var crd = ConvertFundDataToCrawerData(fundDatas, nd.Name);
                    _CrawlerData.Add(crd);
                }
            }
        }

        private CrawlerResultData ConvertFundDataToCrawerData(List<FundData> funds, string name)
        {
            CrawlerResultData crawlerData = new CrawlerResultData();
            FundData fund = funds.OrderByDescending(x => x.Time).FirstOrDefault();
            var ret = new CrawlerResultData()
            {
                Name = name,
                Value = fund.Price
            };
            return ret;
        }
        
        public void StartPriceCompare()
        {
            foreach (var member in _NotifyConfigDatas)
            {
                foreach (var nc in member.NotifyConfig)
                {
                    //var nd = _NotifyData.Where(x => x.Name == nc.Name).FirstOrDefault();
                    var cd = _CrawlerData.Where(x => x.Name == nc.Name).FirstOrDefault();
                    if(nc.Change == Enum.DBEnum.Change.Rise)
                    {
                        if(nc.Value >= cd.Value)
                        {
                            Console.WriteLine(string.Format(_NotifyMessage, nc.Name, cd.Value, "高於", nc.Value));
                            //Console.WriteLine(nc.Name + " value: " +cd.Value + " 價錢高於設定值: "　+ nc.Value);
                        }
                    }
                    else if(nc.Change == Enum.DBEnum.Change.Fall)
                    {
                        if(nc.Value <= cd.Value)
                        {
                            Console.WriteLine(string.Format(_NotifyMessage, nc.Name, cd.Value, "低於", nc.Value));
                        }
                    }
                }
            }
        }

        

        /// <summary>
        /// 取得NotifyConfig資料
        /// </summary>
        /// <returns></returns>
        private List<Member> GetNotifyConfig()
        {
            List<Member> notifyConfigDatas = _NotifyConfigObj.GetMemberNotifyConfig();
            return notifyConfigDatas;
        }

        /// <summary>
        /// 取得NotifyData資料
        /// </summary>
        /// <returns></returns>
        private List<NotifyData> GetNotifyData()
        {
            List<NotifyData> notifyDatas = _NotifyObj.GetNotifyData();
            return notifyDatas;
        }
    }
}
