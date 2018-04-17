using Crawler.Craw;
using Crawler.Craw.Interface;
using Crawler.Model;
using Crawler.Model.dao.Json;
using Crawler.WebCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        static string testFilePath = @"D:\practice\Fund\fundrich.txt";
        static void Main(string[] args)
        {
            PriceNotify pn = new PriceNotify();
            pn.PriceNotifyProcess();
            
            
            Console.WriteLine("end");
            Console.ReadLine();
        }


        private static void HTTPTest2(string url)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(
              url);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            //request.ContentType = "text/html; charset=utf-8";
            // Get the response.  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.  
            Console.WriteLine(response.StatusDescription);
            string charset = response.CharacterSet;
            Console.WriteLine("charset: " + charset + " Encoding: " + Encoding.GetEncoding(charset));
            
            
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream, Encoding.GetEncoding(charset));

            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.  
            reader.Close();
            response.Close();
        }

        private static void TestDataRead()
        {
            var a = new NotifyDataModel();
            var b = a.GetNotifyData();

            var c = new NotifyConfigModel();
            var d = c.GetMemberNotifyConfig();
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
            string html = a.temp();
            //a.SetURL("http://fund.bot.com.tw/w/wb/wb02a.djhtm?customershowall=0&a=TLZ64-0532");
            //string html = a.GetHtmlContent();
            //string html = File.ReadAllText(testFilePath);
            IFundCrawler craw = new FundRich();
            var obj = craw.ParseFund(html);
        }

    }
}
