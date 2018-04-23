using Crawler.Craw;
using Crawler.Craw.Interface;
using Crawler.Craw.Model;
using Crawler.Model.dao;
using Crawler.Model.dao.Json;
using Crawler.Model.DataModel;
using Crawler.Properties;
using Crawler.WebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model
{
    class MemberAndNotify
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<string> NotifyString { get; set; }
    }
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

        public IEnumerable<MemberAndNotify> PriceNotifyProcess()
        {
            DoDataPrepare();
            foreach (var item in StartPriceCompare())
            {
                yield return item;
            }
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
        
        public IEnumerable<MemberAndNotify> StartPriceCompare()
        {
            List<MemberAndNotify> retList = new List<MemberAndNotify>();
            
            foreach (var member in _NotifyConfigDatas)
            {
                MemberAndNotify mn = new MemberAndNotify();
                mn.UserName = member.Account;
                mn.UserEmail = member.Email;
                List<string> retMsg = new List<string>();
                foreach (var nc in member.NotifyConfig)
                {                    
                    var cd = _CrawlerData.Where(x => x.Name == nc.Name).FirstOrDefault();
                    if(nc.Change == Enum.DBEnum.Change.Rise)
                    {
                        if(cd.Value >= nc.Value)
                        {
                            retMsg.Add(string.Format(_NotifyMessage, nc.Name, cd.Value, "高於", nc.Value));
                            Console.WriteLine(string.Format(_NotifyMessage, nc.Name, cd.Value, "高於", nc.Value));
                        }
                    }
                    else if(nc.Change == Enum.DBEnum.Change.Fall)
                    {
                        if(cd.Value <= nc.Value)
                        {
                            retMsg.Add(string.Format(_NotifyMessage, nc.Name, cd.Value, "低於", nc.Value));
                            Console.WriteLine(string.Format(_NotifyMessage, nc.Name, cd.Value, "低於", nc.Value));
                        }
                    }
                    
                }
                if (retMsg.Count > 0)
                {
                    mn.NotifyString = retMsg;
                    yield return mn;
                }                        
                else
                    continue;
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
