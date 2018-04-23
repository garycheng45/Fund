using Crawler.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.SystemConfig
{
    public class SystemInfo
    {
        
        static Settings setting = new Settings();   
        //public static string root = AppDomain.CurrentDomain.BaseDirectory;
        //public static string root = @"D:\practice\Fund\Crawler\Env\";
        public static string root = setting.Root;
        public static string etc = Path.Combine(root, "etc") + Path.DirectorySeparatorChar;
        public static string data = Path.Combine(root, "data") + Path.DirectorySeparatorChar;
        public static string notifyConfig = Path.Combine(data, "NotifyConfig") + Path.DirectorySeparatorChar;
        public static string notifyData = Path.Combine(data, "NotifyData") + Path.DirectorySeparatorChar;
        public static string member = Path.Combine(data, "Member") + Path.DirectorySeparatorChar;
    }
}
