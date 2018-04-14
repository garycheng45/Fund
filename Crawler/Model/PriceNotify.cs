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
        public PriceNotify()
        {
            _NotifyConfigObj = new NotifyConfigModel();
            _NotifyObj = new NotifyDataModel();
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
                string html = wb.GetHtmlContent();
                if(nd.NotifyType == Enum.DBEnum.NotifyType.Fund)
                {
                    IFundCrawler crawer = CrawlerCreateFactory.CreateFundCrawler(nd.Resource);
                    var fundDatas =  crawer.ParseFund(html);
                    var crd = ConvertFundDataToCrawerData(fundDatas, nd.Name);
                    _CrawlerData.Add(crd);
                }
            }
        }

        private CrawlerResultData ConvertFundDataToCrawerData(List<FundData> funds, string name)
        {
            CrawlerResultData crawlerData = new CrawlerResultData();
            FundData fund = funds.OrderByDescending(x => x.Time).FirstOrDefault();
            return new CrawlerResultData()
            {
                Name = name,
                Value = fund.Price
            };
        }
        //public async Task DoCrawerAsync()
        //{
        //    foreach (var nd in _NotifyData)
        //    {
        //        await Crawer(nd);
        //        Console.WriteLine("crawering " + nd.Name);
        //    }
            
        //}

        //private Task Crawer(NotifyData notifyData)
        //{
        //    WebBase wb = new WebBase();
        //    wb.SetURL(notifyData.Url);
        //    string html = wb.GetHtmlContent();
            
        //    return t;
        //}

        public void StartPriceCompare()
        {

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
