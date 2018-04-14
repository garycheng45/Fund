using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model.DataModel;
using System.IO;
using Crawler.SystemConfig;
using Newtonsoft.Json;

namespace Crawler.Model.dao.Json
{
    class NotifyDataModel : AbsGetNotifyData
    {
        public override List<NotifyData> GetNotifyData()
        {
            List<NotifyData> data = null;
            try
            {
                string json = ReadFile.ReadJsonFile("List.json", SystemInfo.notifyData);
                data = JsonConvert.DeserializeObject<List<NotifyData>>(json);
            }
            catch (Exception ex)
            {
                
            }
            return data;
//            data = Json
        }
    }
}
