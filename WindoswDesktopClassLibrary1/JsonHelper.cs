using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//*********************************自己加寫（宣告）的NameSpace
using System.Data;    // DataTable會用到。
using System.Text;    // StringBuilder會用到。
using System.Text.RegularExpressions;  // Regex會用到

using System.Web.Script.Serialization;    //** 自己宣告、加入。JSON會用到。
                                                          //** 需自己動手「加入參考」System.Web.Extension.dll檔
//*********************************


namespace WindoswDesktopClassLibrary1
{
    /// <summary>
    /// JsonHelper 的摘要描述 -- 以字串相連的方式來做JSON。
    /// 本範例引用自 http://www.roelvanlisdonk.nl/?p=2654
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// Date time format, used to convert a datetime to a string.
        /// By default "yyyy-MM-dd HH:mm:ss".
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// Initializes properties.
        /// </summary>
        public JsonHelper()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";   //日期格式在此修改！！
        }


        /// <summary>
        /// Converts a DataSet to a JSON string.  把DataSet轉成JSON字串。
        /// </summary>
        /// <param name="dataSet">The dataset to convert.</param>
        /// <exception cref="System.ArgumentException">Thrown when parameter [dataSet] is null.</exception>
        /// <returns>
        /// - JSON in the format [[[Table0Row0Column0,Table0Row0Column1],[Table0Row1Column0,Table0Row1Column1]],[[Table1Row0Column0,Table1Row0Column1],[Table1Row1Column0,Table1Row1Column1]]]
        /// - Empty string, when dataset contains no tables.
        /// - Empty string, when all tables contain no rows.
        /// </returns>
        public string ToJson(DataSet dataSet)
        {
            if (dataSet == null) { throw new ArgumentNullException("dataSet"); }

            StringBuilder result = new StringBuilder(string.Empty);
            if (dataSet.Tables.Count > 0)
            {
                result.Append("[");
                foreach (DataTable table in dataSet.Tables)   {
                    result.Append(ToJson(table));
                }
                result.Append("]");
            }
            return result.ToString();
        }

        /// <summary>
        /// Converts a DataTable to a JSON string.  把DataTable轉成JSON字串。
        /// </summary>
        /// <param name="table">The dataset to convert.</param>
        /// <exception cref="System.ArgumentException">Thrown when parameter [table] is null.</exception>
        /// <returns>
        /// - JSON in the format JSON in the format [[Row0Column0,Row0Column1],[Row1Column0,Row1Column1]]
        /// - Empty string, when datatable contains no rows.
        /// </returns>
        public string ToJson(DataTable table)
        {
            if (table == null) { throw new ArgumentNullException("table"); }

            StringBuilder result = new StringBuilder(string.Empty);
            if (table.Rows.Count > 0)
            {
                int i = 1;

                result.Append("[");
                foreach (DataRow row in table.Rows)
                {
                    if (i == table.Rows.Count)
                    {   //--這一段我稍做修改，不然無法通過JSON格式的驗證。
                        result.Append(ToJson(row));
                    }
                    else   {
                        result.Append(ToJson(row) + ",");
                    }
                    i++;
                }
                result.Append("]");
            }
            return result.ToString();
        }

        /// <summary>
        /// Converts a DataRow to a JSON string.  把DataRow轉成JSON字串。
        /// </summary>
        /// <param name="row">The data row to convert.</param>
        /// <exception cref="System.ArgumentException">Thrown when parameter [row] is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when property [DateTimeFormat] is null, empty or contains only whitespaces.</exception>
        /// <returns>
        /// - JSON in the format [Row0Column0,Row0Column1].
        /// - Empty string, when datarow contains no columns.
        /// </returns>
        public string ToJson(DataRow row)
        {
            if (row == null) { throw new ArgumentNullException("row"); }
            if (string.IsNullOrWhiteSpace(DateTimeFormat)) { throw new ArgumentNullException("DateTimeFormat"); }

            StringBuilder result = new StringBuilder(string.Empty);
            if (row.ItemArray.Count() > 0)   // DataRow的.ItemArray屬性。http://msdn.microsoft.com/zh-tw/library/system.data.datarow.itemarray.aspx
            {
                var serializer = new JavaScriptSerializer();  //使用 System.Web.Script.Serialization命名空間。
                string json = serializer.Serialize(row.ItemArray);

                // Replace Date(...) by a string in the format found in the property [DateTimeFormat].
                // 將JSON日期時間，轉成DateTime格式。
                var matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                var regex = new Regex(@"\\/Date\((-?\d+)\)\\/");
                json = regex.Replace(json, matchEvaluator);

                result.Append(json);
            }
            return result.ToString();
        }


        //============================================================
        //== 沿用 JSON_2.aspx、JSON控制器的Index_JSON2動作的程式碼做出來。
        //============================================================
        //     自己添加的功能，可產生「欄位名稱」與「值」
        //     資料來源 http://www.codeproject.com/Tips/624888/Converting-DataTable-to-JSON-Format-in-Csharp-and
        public string ToJson2Column(DataTable table)
        {
            if (table == null) { throw new ArgumentNullException("table"); }

            StringBuilder result = new StringBuilder(string.Empty);
            if (table.Rows.Count > 0)
            {   //資料來源 http://www.codeproject.com/Tips/624888/Converting-DataTable-to-JSON-Format-in-Csharp-and
                List<Dictionary<string, object>> result_rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;

                foreach (DataRow drow in table.Rows)
                {
                    row = new Dictionary<string, object>();    //一筆記錄
                    foreach (DataColumn col in table.Columns)
                    {
                        row.Add(col.ColumnName.Trim(), drow[col]);   //加入一個欄位 與 值
                    }
                    result_rows.Add(row);
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();    //搭配命名空間System.Web.Script.Serialization
                string json = serializer.Serialize(result_rows);

                //將JSON日期時間，轉成DateTime格式。
                var matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                var regex = new Regex(@"\\/Date\((-?\d+)\)\\/");
                json = regex.Replace(json, matchEvaluator);

                return json;
            }
            else   {
                return "***查無資料！***";
            }

        }

        //********************************************************************************************

        /// <summary>
        /// Converts a JSON string to a object array.   把 JSON轉成物件陣列。
        /// </summary>
        /// <param name="input">The input.</param>
        /// <exception cref="System.ArgumentException">Thrown when input is null.</exception>
        /// <returns></returns>
        public object[] FromJson(string input)
        {
            if (input == null) { throw new ArgumentNullException("input"); }
            var serializer = new JavaScriptSerializer();
            object[] result = serializer.Deserialize(input, typeof(object[])) as object[];

            return result;
        }

        //********************************************************************************************

        /// <summary>
        /// JSON 日期與時間 格式轉換。
        /// Replace JSON dates, like "\/Date(1330740183000)\/" to a string in the format found in the property [DateTimeFormat].
        /// </summary>
        /// <param name="match">When null, throws exception</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException, when property [DateTimeFormat] is null, empty or contains only white spaces.</exception>
        /// <returns>A string in the format found in the property [DateTimeFormat]</returns>
        public string ConvertJsonDateToDateString(Match match)
        {
            if (match == null) { throw new ArgumentNullException("match"); }
            if (string.IsNullOrWhiteSpace(DateTimeFormat)) { throw new ArgumentNullException("DateTimeFormat"); }

            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1); // Epoch date, used by the JavaScriptSerializer to represent starting point of datetime in JSON.
            dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString(DateTimeFormat);
            return result;
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
