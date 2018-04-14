using Crawler.SystemConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model.dao.Json
{
    static class ReadFile
    {
        public static string ReadJsonFile(string fileName, string path)
        {
            path = Path.Combine(path, fileName);

            string json = File.ReadAllText(path, Encoding.Default);
            
            return json;
        }
    }
}
