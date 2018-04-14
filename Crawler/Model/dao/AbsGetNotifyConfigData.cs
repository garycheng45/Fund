using Crawler.Model.dao.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model.DataModel;

namespace Crawler.Model.dao
{
    abstract class AbsGetNotifyConfigData : INotifyConfig
    {
        public abstract List<Member> GetMemberNotifyConfig();

        /// <summary>
        /// 將所有使用者所設定的資料名稱進行distinct並回傳
        /// </summary>
        /// <param name="datas">所有使用者設定資料</param>
        /// <returns>distinct後的設定名稱</returns>
        protected List<string> GetDistinctNotifyName(List<Member> datas)
        {
            List<string> retdata = new List<string>();

            foreach (var data in datas)
            {
                var names = data.NotifyConfig.Select(x => x.Name).Distinct();
                retdata.AddRange(names);
            }
            retdata = retdata.Distinct().ToList();
            return retdata;
        }
    }
}
