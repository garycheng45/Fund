using Crawler.Model.dao.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model.DataModel;

namespace Crawler.Model.dao
{
    public abstract class AbsGetNotifyData : IGetNotifydata
    {
        public abstract List<NotifyData> GetNotifyData();

        /// <summary>
        /// 取出有被設定通知的資料源資訊
        /// </summary>
        /// <param name="settingDatas">所有資料源資訊</param>
        /// <param name="configNames">被設定通知的資料名稱</param>
        /// <returns></returns>
        public List<NotifyData> GetConfigNotify(List<NotifyData> settingDatas, List<string> configNames)
        {
            List<NotifyData> configNotify = new List<NotifyData>();
            
            foreach (var configName in configNames.Distinct())
            {
                var data = settingDatas.Where(x => x.Name == configName).FirstOrDefault();
                if (data != null)
                    configNotify.Add(data);
            }
            return configNotify;
        }
    }
}
