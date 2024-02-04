using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//*****************************************************
using System.ServiceModel.Activation;   // 搭配 [AspNetCompatibilityRequirements]
//*****************************************************
using System.Text;

namespace WebApplication2017_MVC_GuestBook.WCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼、svc 和組態檔中的類別名稱 "WCF_Service_OK"。
    // 注意: 若要啟動 WCF 測試用戶端以便測試此服務，請在 [方案總管] 中選取 WCF_Service_OK.svc 或 WCF_Service_OK.svc.cs，然後開始偵錯。


    // 重點二！！ 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WCF_Service_OK : IWCF_Service_OK
    {
        public string GetDateTime()
        {
            return DateTime.Now.ToString();
        }


        // 重點三！！    別忘了，修改 Server端的 Web.Config設定檔。
    }
}
