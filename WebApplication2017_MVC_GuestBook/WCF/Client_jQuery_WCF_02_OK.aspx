<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client_jQuery_WCF_02_OK.aspx.cs" Inherits="WebApplication2017_MVC_GuestBook.WCF.Client_jQuery_WCF_02_OK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>從WCF傳回日期與時間（無須 後置程式碼）</title>

<script src="../Scripts/jquery-1.10.2.js"></script>
<script type="text/javascript">
    $(document).ready(function () {

            $("#Button1").click(function() {
                $.ajax({
                    type: "POST",     // 需要跟 WCF「介面」的設定，彼此搭配！
                    contentType: "application/json; charset=utf-8",
                    //*****************************************************************
                    url: "/WCF/WCF_Service_OK.svc/GetDateTime",
                    // 執行WCF服務（Server端），自己複製這段網址。 localhost最後的數字（Port，通訊埠）會變，請自行修正
                    //*****************************************************************
                    dataType: "json",
                    success: function (msg) {
                        $("#output1").text(msg.d);
                    },
                    error: function () {
                        alert('例外狀況，有問題～～  localhost最後的數字（Port，通訊埠）會變，您改了嗎？？？');    
                    }
                });
            });  // End of Button1.Click
    });

    // 參考資料：
    //  https://dotblogs.com.tw/kirkchen/2010/08/18/17282
    //  https://www.codeproject.com/Articles/132809/Calling-WCF-Services-using-jQuery
    //
    // 如果要使用 ASP.NET AJAX，請參閱 https://docs.microsoft.com/zh-tw/dotnet/framework/wcf/feature-details/create-an-ajax-wcf-asp-net-client

</script>

</head>
<body>
    <form id="form1" runat="server">
    <div><h3>從WCF #2 傳回日期與時間</h3>  ==需搭配WCF二號範例，而且要修改「設定檔」==<br /><br />
    
    <input id="Button1" type="button" 
            value="傳統HTML按鈕。==需搭配WCF二號範例，而且要修改「設定檔」==" />
    <br /><br />

    <!-- *********************** -->
    <div id="output1"></div>
    <!-- *********************** -->

     <br /><br /><br />
     參考資料：<br />
      https://dotblogs.com.tw/kirkchen/2010/08/18/17282<br />
      https://www.codeproject.com/Articles/132809/Calling-WCF-Services-using-jQuery<br />
    </div>
    </form>
</body>
</html>
