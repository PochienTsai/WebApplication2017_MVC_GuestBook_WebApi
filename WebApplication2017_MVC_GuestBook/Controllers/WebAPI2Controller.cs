using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace WebApplication2017_MVC_GuestBook.Controllers
{
    //=== 微軟 VS 2017提供的訊息（當您加入 WebAPI控制器以後）===
    //Visual Studio has added the full set of dependencies for ASP.NET Web API 2 to project 'WebApplication2017_MVC_GuestBook'. 

    //The Global.asax.cs file in the project may require additional changes to enable ASP.NET Web API.
    //     **********************
    //1. Add the following namespace references:
    //    using System.Web.Http;
    //    using System.Web.Routing;

    //2. If the code does not already define an Application_Start method, add the following method:
    //    protected void Application_Start()
    //    {
    //    }

    //3. Add the following lines to the beginning of the Application_Start method:
    //    GlobalConfiguration.Configure(WebApiConfig.Register);


    public class WebAPI2Controller : ApiController   //*** 重點！！
    {
        //參考來源：  https://docs.microsoft.com/zh-tw/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api
               //[System.Web.Http.HttpGet] //明確指定動作的HTTP方法
        //[AcceptVerbs("GET","POST")] //允許多個HTTP方法的動作
        public string GetHello()
        {
            return "Hello! The World.....";
        }

        public string GetHelloWorld(int id)
        {
            return "Hello! The World....." + id.ToString();
        }
        public string GetComputeIT(int a, int b)
        {
            //須設定一個新的路由
            int result = a + b;
            return result.ToString();
        }
    }
}
