using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebApplication2017_MVC_GuestBook.WCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼、svc 和組態檔中的類別名稱 "Service123"。
    // 注意: 若要啟動 WCF 測試用戶端以便測試此服務，請在 [方案總管] 中選取 Service123.svc 或 Service123.svc.cs，然後開始偵錯。
    public class Service123 : IService123
    {
        public string DoWork()
        {
            return "Hello, World";
        }

        public string DoWork2(int a, int b)
        {
            int result = a + b;
            return result.ToString();
        }

        //public string GetDateTime()
        //{
        //    return DateTime.Now.ToString();
        //}

    }
}
