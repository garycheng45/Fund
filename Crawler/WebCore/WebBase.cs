
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string GetHtmlContent1()
        {
            if(_client.BaseUrl == null)
            {
                return "";
            }

            var req = new RestRequest(Method.GET);
            req.AddHeader("cache-control", "no-cache");
            req.AddHeader("postman-token", "48f062b4-24f1-9613-e58b-6eeb65cd8");
            IRestResponse resp = _client.Execute(req);
            string a = resp.ContentEncoding;
            
            if (resp == null)
                return "";
            
            return resp.Content;
        }

        public string GetHtmlContent(string url)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(
              url);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "text/html; charset=utf-8";
            // Get the response.  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);
            StreamReader reader = new StreamReader(dataStream, ResponseEncoding(response.CharacterSet));
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
            return responseFromServer;
        }

        private Encoding ResponseEncoding(string charset)
        {
            Encoding encode;
            if(charset.ToUpper() == "ISO-8859-1")
            {
                encode = Encoding.UTF8;
            }
            else
            {
                encode = Encoding.GetEncoding(charset);
            }
            return encode;
        }


        public string temp()
        {

            //var client = new RestClient("http://fund.bot.com.tw/w/wb/wb02a.djhtm?customershowall=0&a=TLZ64-0532");
            var client = new RestClient("https://www.fundrich.com.tw/fund/019002.html?id=019002#淨值走勢");

            //RestClient a = new RestClient()

            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "48f062b4-24f1-9613-e58b-6eeb65cb31d8");
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }
        //public string temp()
        //{
        //    WebClient httpClient = new WebClient();
        //    httpClient.BaseAddress =
        //    httpClient.BaseAddress = new Uri("http://foobar.com/");
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Add(
        //                    new MediaTypeWithQualityHeaderValue("application/xml"));

        //}
    }
}
