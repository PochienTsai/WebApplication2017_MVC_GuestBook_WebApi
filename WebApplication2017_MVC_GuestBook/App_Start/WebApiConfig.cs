using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication2017_MVC_GuestBook
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}", // 注意!!第二個是"控制器"名稱
                defaults: new { id = RouteParameter.Optional }
            );
            //使用路由中的"api"的原因是避免與ASP.NET MVC的routing發生衝突
            //如此一來,你可以有"/控制器"移至(傳統的)MVC控制器
            //而新的"/api/控制器"移至Web API控制器
            //WebAPI的路由 : https://learn.microsoft.com/zh-tw/aspnet/web-api/overview/web-api-routing-and-actions/

            config.Routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "api/{controller}/{a}/{b}", // 注意!!第二個是"控制器"名稱
                defaults: new { id = RouteParameter.Optional }
                //a與b這兩個變數名稱,需與GetComputeIT(int a, int b)方法的輸入參數一模一樣
            );
        }
    }
}
