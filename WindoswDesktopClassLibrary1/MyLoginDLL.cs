using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//*****************************************************
using System.Security.Cryptography;   //-- MD5 / SHA1（雜湊）專用  https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography
using System.Text;                                //-- StringBuilder專用


//********************************************（命名空間）
namespace WindoswDesktopClassLibrary1
{
    //***** 別忘了設為 public，才能被引用！！
    public class MyLoginDLL
    {

        public bool LoginCheck()
        {
            if (HttpContext.Current.Session["Login"] != null && HttpContext.Current.Session["Login"].ToString() == "OK")
            {   // 正常登入成功，取得門票與鑰匙。  允許通行！
                return true;
            }
            else
            {   //return Content("<h3>登入失敗！！</h3>");
                return false;                
            }
        }


        /// <summary>
        ///  計算MD5雜湊值的字串，並傳回做為 32 個字元的十六進位格式字串的雜湊。
        ///  請搭配 System.Security.Cryptography命名空間。   http://msdn.microsoft.com/zh-tw/library/system.security.cryptography.md5.aspx  
        /// </summary>
        /// <param name="md5Hash">MD5.Create()。建立 MD5 雜湊演算法之預設實作的執行個體。</param>
        /// <param name="input">輸入值</param>
        /// <returns></returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)     //*** 固定的數學公式。 可以使用static（靜態）方法
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)   {
                sBuilder.Append(data[i].ToString("x2"));  //--變成十六進位
            }
            return sBuilder.ToString();
        }


        /// <summary>
        /// 雜湊大小 SHA1演算法是 160 位元。
        /// </summary>
        /// <param name="uInput">輸入值</param>
        /// <returns></returns>
        public static string GetSHA1Hash(string uInput)     //*** 固定的數學公式。 可以使用static（靜態）方法
        {
            ////** 方法一 ****************************************************************
            //SHA1 SHA1Hasher = SHA1.Create();
            ////-- SHA1必須搭配 System.Security.Cryptography命名空間

            //Byte[] data = SHA1Hasher.ComputeHash(Encoding.Default.GetBytes(uInput));
            ////-- SHA1的 .ComputeHash(Byte[]) 方法，計算指定位元組陣列的雜湊值。
            ////  （字串轉成Byte[]）  System.Text.Encoding.Default.GetBytes(uInput)

            //** 方法二 ****************************************************************
            Byte[] data;
            SHA1CryptoServiceProvider sha1SP = new SHA1CryptoServiceProvider();
            //-- 使用密碼編譯服務提供者 (CSP) 所提供之實作，計算輸入資料的 SHA1 雜湊值。
            //-- http://msdn.microsoft.com/zh-tw/library/system.security.cryptography.sha1cryptoserviceprovider.aspx
            data = sha1SP.ComputeHash(Encoding.Default.GetBytes(uInput));
            StringBuilder sBuilder = new StringBuilder();   //-- StringBuilder必須搭配 System.Text命名空間

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)   {
                sBuilder.Append(data[i].ToString("x2"));  //--變成十六進位
            }
            return sBuilder.ToString();
        }



    }
}
