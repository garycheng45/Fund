
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.WebCore
{
    public class WebBase : IDisposable
    {
        RestClient _client;
        public WebBase()
        {
            _client = new RestClient();
        }

        public void Dispose()
        {
            _client.BaseUrl = null;
            _client = null;
        }

        /// <summary>
        /// 設定新的URI
        /// </summary>
        /// <param name="uri"></param>
        public void SetURL(string uri)
        {
            _client.BaseUrl = new Uri(uri);
        }

        /// <summary>
        /// 取得設定的URI
        /// </summary>
        /// <returns></returns>
        public string GetURL()
        {
            return _client.BaseUrl.ToString();
        }

        /// <summary>
        /// 取得URI回傳網頁html 
        /// </summary>
        /// <returns></returns>
        public string GetHtmlContent()
        {
            if(_client.BaseUrl == null)
            {
                return "";
            }

            var req = new RestRequest(Method.GET);
            req.AddHeader("cache-control", "no-cache");
            //req.AddHeader("token", "");
            IRestResponse resp = _client.Execute(req);
            if (resp == null)
                return "";
            return resp.Content;
        }


        public void temp()
        {
            
            var client = new RestClient("http://fund.bot.com.tw/w/wb/wb02a.djhtm?customershowall=0&a=TLZ64-0532");
            //RestClient a = new RestClient()
            
            var request = new RestRequest(Method.GET);
            //request.AddHeader("postman-token", "48f062b4-24f1-9613-e58b-6eeb65cb31d8");
            request.AddHeader("cache-control", "no-cache");
            
            ////IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }
    }
}
