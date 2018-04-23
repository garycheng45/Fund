using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Model.DataModel;
using Crawler.SystemConfig;
using System.IO;
using Newtonsoft.Json;

namespace Crawler.Model.dao.Json
{
    class NotifyConfigModel : AbsGetNotifyConfigData
    {
        /// <summary>
        /// 取得使用者設定通知資料
        /// </summary>
        /// <returns></returns>
        public override List<Member> GetMemberNotifyConfig()
        {
            List<Member> memberData = new List<Member>();
            
            var fileList = Directory.GetFiles(SystemInfo.notifyConfig);

            foreach (var file in fileList)
            {
                var member = new Member();
                var memberSetupFileName = Path.GetFileNameWithoutExtension(file);
                string memberJson = ReadFile.ReadJsonFile(Path.GetFileName(file), SystemInfo.member);
                try
                {
                    member = JsonConvert.DeserializeObject<Member>(memberJson);
                }
                catch (Exception)
                {

                    throw;
                }
                member.Account = Path.GetFileNameWithoutExtension(file);
                string json = ReadFile.ReadJsonFile(file, SystemInfo.notifyConfig);
                try
                {
                    member.NotifyConfig = JsonConvert.DeserializeObject<List<NotifyConfig>>(json);
                    memberData.Add(member);
                }
                catch (Exception ex)
                {
                    continue;
                    //throw;
                }                
            }
            return memberData;
        }
    }
}
