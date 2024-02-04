using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2017_MVC_GuestBook.Controllers
{
    //********************************************
    // 測試 JavaScript的幾種作法
    //
    // http://kevintsengtw.blogspot.com/2012/09/aspnet-mvc-javascript-alert.html
    // https://blog.miniasp.com/post/2014/11/10/ASPNET-MVC-5-Microsoft-jQuery-Unobtrusive-Ajax-lost-and-found.aspx
    //https://stackoverflow.com/questions/19541336/can-i-return-javascript-from-mvc-controller-to-view-via-ajax-request
    //********************************************

    public class JavaScriptController : Controller
    {
        // GET: JavaScript
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JavaScript_Test1()
        {
            string urlStr = Url.Action("Index");
            string jsCode = $"<script>alert(\"找不到資料！\"); location.href=\" {urlStr} \";</script>";
            return Content(jsCode);
        }


        public ActionResult JavaScript_Test2()
        {
            string urlStr = Url.Action("Index");
            string jsCode = $"alert('找不到資料！'); location.href=' {urlStr} ';";  // 注意！！沒有 <script></script>

            //***********************************
            TempData["jsMessage"] = jsCode;    // 檢視畫面(View)有搭配、對應的程式碼
            //***********************************
            return View();
        }


        // *** JavaScriptResult ***，第一種作法。
        //==== 兩個畫面（View）彼此搭配 =================================== (start)
        public ActionResult JavaScript_Test3()
        {
            //string urlStr = Url.Action("Index");
            //string jsCode = $"alert('找不到資料！'); location.href=' {urlStr} ';";  // 注意！！沒有 <script></script>

            //return JavaScript(jsCode);  // 檢視畫面(View)有搭配、對應的程式碼
            //// *** 缺點：***
            //// 畫面上直接顯示 純文字（字串），沒有效果！！
            //// 解決方法 -- https://dotblogs.com.tw/brooke/2016/09/09/182829
            //// 重要觀念 https://stackoverflow.com/questions/1677325/working-example-for-javascriptresult-in-asp-net-mvc
            
            return View();            
        }

        public ActionResult JavaScriptMessage()
        {
            string urlStr = Url.Action("Index");
            string jsCode = $"alert('找不到資料！')";  // 注意！！沒有 <script></script>

            return JavaScript(jsCode);  // 檢視畫面(View)有搭配、對應的程式碼
            // 解決方法 -- https://dotblogs.com.tw/brooke/2016/09/09/182829
            // 重要觀念 https://stackoverflow.com/questions/1677325/working-example-for-javascriptresult-in-asp-net-mvc
        }
        //==== 兩個畫面（View）彼此搭配 =================================== (end)



        // *** JavaScriptResult ***，第二種作法。
        //==== 兩個畫面（View）彼此搭配 =================================== (start)
        public ActionResult JavaScript_Test4()
        {
            return View();  // 檢視畫面(View)有搭配、對應的程式碼
        }

        public ActionResult JavaScriptMessage2()
        {
            string urlStr = Url.Action("Index");
            string jsCode = $"alert('找不到資料！')";  // 注意！！沒有 <script></script>

            return JavaScript(jsCode);  // 檢視畫面(View)有搭配、對應的程式碼
            // 可以在頁面上使用 jQuery的  .getScript()方法，向服務器獲取js代碼，然後執行js代碼
            // https://hk.saowen.com/a/c41d508d7a3bb56b3f97713d1fe49a9956529e713384e3c08c7bf3580a78823d
        }
        //==== 兩個畫面（View）彼此搭配 =================================== (end)






    }
}