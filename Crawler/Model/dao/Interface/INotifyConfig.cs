using Crawler.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Model.dao.Interface
{
    interface INotifyConfig
    {
        /// <summary>
        /// 取得使用者設定通知資料
        /// </summary>
        /// <returns></returns>
        List<Member> GetMemberNotifyConfig();
    }
}
