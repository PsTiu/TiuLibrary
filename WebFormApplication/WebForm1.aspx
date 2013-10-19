<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebFormApplication.WebForm1" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnAddTxb" Text="AddTxb" runat="server" 
            onclick="btnAddTxb_Click" />
        <asp:Panel runat="server" ID="pnlTxb">
        </asp:Panel>
        <asp:Button ID="btnShowText" Text="ShowText" runat="server" 
            onclick="btnShowText_Click" />
        <asp:Label Text="" ID="labText" runat="server" />
    </div>
    </form>
</body>
</html>
