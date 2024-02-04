<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Client_WebForm_WF_02_OK.aspx.cs" Inherits="WebApplication2017_MVC_GuestBook.WCF.Client_WebForm_WF_02_OK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            使用傳統 Web Form（.NET -- Client端來呼叫WCF）<br /><br /><br />

            <asp:Button runat="server" ID="Button1" Text="使用傳統 Web Form（.NET -- Client端來呼叫WCF）"
                                 OnClick="Button1_Click" />
            <br /><hr /><br />

            <h3>
                  <asp:Label runat="server" ID="Label1" />
            </h3>

        </div>
    </form>
</body>
</html>
