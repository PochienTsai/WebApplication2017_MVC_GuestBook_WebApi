using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2017_MVC_GuestBook.Models;  // 自己動手寫上命名空間 -- 「專案名稱.Models」。
//*********************************自己加寫（宣告）的NameSpace
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.Web.Script.Serialization;   //** 自己宣告、加入。JSON會用到。JavaScriptSerializer

using Newtonsoft.Json;   //** 自己宣告、加入。JSON.NET會用到。
//*********************************


namespace WebApplication2017_MVC_GuestBook.Controllers
{
    public class JSONController : Controller
    {
        // GET: JSON
        // 線上格式化JSON的網站  http://jsoneditoronline.org/
        // JSON格式進行驗證的網站  http://zaach.github.io/jsonlint/



        //*************************************   連結 MVC_UserDB 資料庫  ********************************* (start)
        #region
        private MVC_UserDBContext _db = new MVC_UserDBContext();
        // 如果沒寫上方的命名空間 --「專案名稱.Models」，就得寫成下面這樣，加註「Models.」字樣。
        // private Models.MVC_UserDBContext _db = new Models.MVC_UserDBContext();

        // 資料庫一旦開啟連線，用完就得要關閉連線與釋放資源。https://msdn.microsoft.com/zh-tw/library/system.web.mvc.controller_methods(v=vs.118).aspx
        protected override void Dispose(bool disposing)
        {   // 有開啟DB連結，就得動手關掉、Dispose這個資源。https://msdn.microsoft.com/zh-tw/library/system.idisposable.dispose(v=vs.110).aspx
            // 或是 官方網站的教材（程式碼）https://github.com/aspnet/Docs/blob/master/aspnet/mvc/overview/getting-started/introduction/sample/MvcMovie/MvcMovie/Controllers/MoviesController.cs
            if (disposing)
            {
                _db.Dispose();  //***這裡需要自己修改，例如 _db字樣
            }
            base.Dispose(disposing);
            // 資料庫一旦開啟連線，用完就得要關閉連線與釋放資源。
            // The base "Controller" class already implements the "IDisposable" interface, so this code simply adds an "override" to the 
            // "Dispose(bool)" method to explicitly dispose the context instance. 
            // ( "Dispose(bool)"方法標示為 virtual，所以可以用override覆寫。https://msdn.microsoft.com/zh-tw/library/dd492699(v=vs.118).aspx )

            // "Controller" class  https://msdn.microsoft.com/zh-tw/library/system.web.mvc.controller(v=vs.118).aspx
        }

        //// 如果找不到動作（Action）或是輸入錯誤的動作名稱，一律跳回首頁
        //// Controller的 HandleUnknownAction方法為 virtual，所以可用override覆寫。https://msdn.microsoft.com/zh-tw/library/dd492730(v=vs.118).aspx

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    Response.Redirect("http://公司首頁(網址)/");  // 自訂結果 -- 找不到動作就跳回首頁
        //    base.HandleUnknownAction(actionName);
        //}
        #endregion
        //*************************************   連結 MVC_UserDB 資料庫  ********************************* (end)


        /// <summary>
        /// LINQ & JSON。搭配 /Models/UserTable.cs。  缺點：輸出的「日期格式」怪怪的。
        /// 資料來源  https://stackoverflow.com/questions/6126151/can-i-get-javascriptserializer-to-serialize-a-linq-result-hierarchically
        /// </summary>
        /// <returns></returns>
        public ActionResult Index_JSON1()
        {
            var data = from u in _db.UserTables
                       select new
                       {
                           UserId = u.UserId,
                           UserName = u.UserName,
                           UserMobilePhone = u.UserMobilePhone
                       };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string result = serializer.Serialize(data);

            return Content(result);
        }


        /// <summary>
        /// ADO.NET -- DataSet & JSON。缺點：輸出的「日期格式」怪怪的。
        /// </summary>
        /// <returns></returns>
        public ActionResult Index_JSON2()
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MVC_UserDB"].ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT [UserId],[UserName],[UserSex],[UserBirthDay],[UserMobilePhone] FROM [UserTable]", Conn);

            DataSet ds = new DataSet();

            try  //==== 以下程式，只放「執行期間」的指令！====
            {
                //Conn.Open();  //---- 這一列註解掉，不用寫，DataAdapter會自動開啟
                myAdapter.Fill(ds, "UserTable");    // 這時候執行SQL指令。取出資料，放進 DataSet。
                //---- DataSet是由許多 DataTable組成的，我們目前只放進一個名為 test的 DataTable而已。

                //******************************************************************************(start)
                //資料來源 http://www.codeproject.com/Tips/624888/Converting-DataTable-to-JSON-Format-in-Csharp-and
                List<Dictionary<string, object>> result_rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;

                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    row = new Dictionary<string, object>();    // 一筆記錄
                    foreach (DataColumn col in ds.Tables[0].Columns)   {
                        row.Add(col.ColumnName.Trim(), drow[col]);   // 加入一個欄位 與 值
                    }
                    result_rows.Add(row);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();    // 搭配命名空間System.Web.Script.Serialization
                return Content(serializer.Serialize(result_rows));
                //******************************************************************************(end)
            }
            catch (Exception ex)   {
                return Content("<hr /> Exception Error Message----  " + ex.ToString());
            }
            finally
            {  //---- 不用寫，DataAdapter會自動關閉
                //    if (Conn.State == ConnectionState.Open)  {
                //          Conn.Close();
                //          Conn.Dispose();
                //    }  // 使用SqlDataAdapter的時候，不需要寫程式去控制Conn.Open()與 Conn.Close()。
            }
            //return View();
        }


        /// <summary>
        /// ADO.NET -- DataSet & JSON。缺點：輸出的「日期格式」怪怪的。
        /// </summary>
        /// <returns></returns>
        public ActionResult Index_JSON_DateTime()
        {
            string result = "";
            // 兩種 JSON的日期時間格式。
            string[] jsonDates = { "/Date(1242357713797+0800)/", "/Date(1027008000000)/" };

            foreach (string jsonDate in jsonDates)
            {
                //                         // **************  
                DateTime dtResult = JsonToDateTime(jsonDate);   
                // 別人寫的function   *********** 放在本程式（控制器）最末端。

                result += "<hr />原始格式：" + jsonDate.ToString() + "<br />";
                result += String.Format("DateTime: {0}", dtResult.ToString("yyyy-MM-dd hh:mm:ss ffffff"));
            }

            return Content(result);
        }

        
        /// <summary>
        /// ADO.NET -- DataSet & JSON。
        /// 搭配 DLL類別庫 (WindoswDesktopClassLibrary1)裡面的「JsonHelper.cs」。
        /// </summary>
        /// <returns></returns>
        public ActionResult JSON_3_AdoNet_JsonHelper()
        {
            string result = "";
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MVC_UserDB"].ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT [UserId],[UserName],[UserSex],[UserBirthDay],[UserMobilePhone] FROM [UserTable]", Conn);

            DataSet ds = new DataSet();

            try  //==== 以下程式，只放「執行期間」的指令！====
            {
                // Conn.Open();  //---- 這一列註解掉，不用寫，DataAdapter會自動開啟
                myAdapter.Fill(ds, "UserTable");    //這時候執行SQL指令。取出資料，放進 DataSet。
                //---- DataSet是由許多 DataTable組成的，我們目前只放進一個名為 UserTable的 DataTable而已。

                //*****************************************************************(start)
                //** 搭配 DLL類別庫 WindoswDesktopClassLibrary1 裡面的「JsonHelper.cs」
                //WindoswDesktopClassLibrary1.JsonHelper JH = new WindoswDesktopClassLibrary1.JsonHelper();

                // 產生三種結果（三種輸出）：
                //result += "<hr />**** DataSet ****<br />" + JH.ToJson(ds) + "<br /><br />";
                //result += "<hr />*** DataTable ***<br />" + JH.ToJson(ds.Tables[0]) + "<br /><br />";
                //result += "<hr />*** 自己添加的功能，可產生「欄位名稱」與「值」 ***<br />" + JH.ToJson2Column(ds.Tables[0]);
                //*****************************************************************(end)

                return Content(result);
            }
            catch (Exception ex)  {
                return Content("<hr /> Exception Error Message----  " + ex.ToString());
            }
            finally
            {   //---- 不用寫，DataAdapter會自動關閉
                //    if (Conn.State == ConnectionState.Open)  {
                //          Conn.Close();
                //          Conn.Dispose();
                //    }  //使用SqlDataAdapter的時候，不需要寫程式去控制Conn.Open()與 Conn.Close()。
            }
        }


        /// <summary>
        /// ADO.NET -- DataSet & JSON。
        /// 搭配 Newtonsoft.Json。
        /// </summary>
        /// <returns></returns>
        public ActionResult JSON_4_JsonNET()
        {
            string result = "";
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MVC_UserDB"].ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter("SELECT [UserId],[UserName],[UserSex],[UserBirthDay],[UserMobilePhone] FROM [UserTable]", Conn);

            DataSet ds = new DataSet();

            try  //==== 以下程式，只放「執行期間」的指令！====
            {
                //Conn.Open();  //---- 這一行註解掉，不用寫，DataAdapter會自動開啟
                myAdapter.Fill(ds, "UserTable");    //這時候執行SQL指令。取出資料，放進 DataSet。
                //---- DataSet是由許多 DataTable組成的，我們目前只放進一個名為 UserTable的 DataTable而已。

                //***********************************************************************(start)
                //資料來源： http://james.newtonking.com/json/help/index.html?topic=html/SerializeDataSet.htm
                // 跟上一個範例的差異在此：
                result += JsonConvert.SerializeObject(ds, Formatting.Indented);
                result  += JsonConvert.SerializeObject(ds, Formatting.None);
                //***********************************************************************(end)
                return Content(result);
            }
            catch (Exception ex)  {
                return Content("<hr /> Exception Error Message----  " + ex.ToString());
            }
            finally
            {  //---- 不用寫，DataAdapter會自動關閉
                //    if (Conn.State == ConnectionState.Open)  {
                //          Conn.Close();
                //          Conn.Dispose();
                //    }  //使用SqlDataAdapter的時候，不需要寫程式去控制Conn.Open()與 Conn.Close()。
            }
        }



        //**********************************************************************
        //*** 把JSON的日期格式轉回來DateTime *************************************
        //資料來源： http://www.cnblogs.com/coolcode/archive/2009/05/22/1487254.html
        public static DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }


    }
}