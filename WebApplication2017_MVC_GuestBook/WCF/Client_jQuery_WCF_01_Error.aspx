<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client_jQuery_WCF_01_Error.aspx.cs" Inherits="WebApplication2017_MVC_GuestBook.WCF.Client_jQuery_WCF_01_Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>錯誤示範 -- 從WCF傳回日期與時間（無須 後置程式碼）</title>

<script src="../Scripts/jquery-1.10.2.js"></script>
<script type="text/javascript">
    $(document).ready(function () {

            $("#Button1").click(function() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/WCF/Service123.svc/DoWork",    // 執行WCF服務（Server端），自己複製這段網址。 Port會變
                    dataType: "json",
                    success: function (msg) {
                        $("#output1").text(msg.d);
                    },
                    error: function () {
                        alert('例外狀況，有問題～～');    //本範例是錯誤示範！！
                    }
                });
            });  // End of Button1.Click
    });

    // 參考資料：
    //  https://dotblogs.com.tw/kirkchen/2010/08/18/17282
    //  https://www.codeproject.com/Articles/132809/Calling-WCF-Services-using-jQuery
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>錯誤示範 -- 從WCF傳回日期與時間<br /><br />
    
    <input id="Button1" type="button" value="傳統HTML按鈕。錯誤示範 -- 從WCF傳回日期與時間" />
    <br />WCF在「Server端」需要一些設定才能讓 Client端呼叫，有點小麻煩！！ 
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
