using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//***************************************
using WebApplication2017_MVC_GuestBook.ServiceReference2;   // 加入服務參考後，呼叫遠端（Server端）WCF
using WebApplication2017_MVC_GuestBook.ServiceReference3;   // 加入服務參考後，呼叫遠端（Server端）WCF
//***************************************


namespace WebApplication2017_MVC_GuestBook.WCF
{
    public partial class Client_WebForm_WF_02_OK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Service123Client client = new Service123Client();
            //// 使用 'client' 變數來呼叫服務上的作業。
            //Label1.Text = client.DoWork();

            //// 永遠關閉用戶端。
            //client.Close();

            WCF_Service_OKClient client = new WCF_Service_OKClient();
            // 使用 'client' 變數來呼叫服務上的作業。
            Label1.Text = client.GetDateTime();

            // 永遠關閉用戶端。
            client.Close();
        }
    }
}