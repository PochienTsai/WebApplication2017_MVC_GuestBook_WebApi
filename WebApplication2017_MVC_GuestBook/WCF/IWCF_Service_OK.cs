using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//**************************************
//*** MVC裡面，必須手動將這個DLL檔，「加入參考」
using System.ServiceModel.Web;    // 搭配 [WebInvoke] 或是 [WebGet]
//**************************************
using System.Text;

namespace WebApplication2017_MVC_GuestBook.WCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IWCF_Service_OK"。
    [ServiceContract]
    public interface IWCF_Service_OK
    {
        // 重點一！！
        // *** (1) 透過 Post來做
        //[WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        // *** (2) 透過 Get來做
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        string GetDateTime();


        // 參考資料：（微軟官方，中文文件）
        // https://docs.microsoft.com/zh-tw/dotnet/framework/wcf/feature-details/creating-wcf-ajax-services-without-aspnet
        // https://docs.microsoft.com/zh-tw/dotnet/framework/wcf/samples/ajax-service-with-json-and-xml-sample
    }
}
