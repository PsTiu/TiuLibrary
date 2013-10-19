<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebFormApplication.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function CustomValidator_txtStr_ClientValidate(source, args) {
            args.IsValid = args.Value.length <= 6;
        }
    </script>
</head>
<body style="font-size:12px;">
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" />
        </p>
        <p>
            Name：
            <asp:TextBox runat="server" ID="txtName" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtName" runat="server" ValidationGroup="1"
                ErrorMessage="Name不能为空" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
        </p>
        <p>
            Number：
            <asp:TextBox runat="server" ID="txtNum" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtNum" runat="server" ValidationGroup="1"
                ErrorMessage="Number不能为空" ControlToValidate="txtNum" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator_txtNum" ControlToValidate="txtNum" ValidationGroup="1" Type="Integer"
                runat="server" ErrorMessage="Number必须是在0到10之间" MinimumValue="0" MaximumValue="10" Display="Dynamic"></asp:RangeValidator>
        </p>
        <p>
            String:
            <asp:TextBox runat="server" ID="txtStr" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtStr" runat="server" ValidationGroup="1"
                ErrorMessage="String不能为空" ControlToValidate="txtStr" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator_txtStr" runat="server" ControlToValidate="txtStr" ValidationGroup="1"
                ErrorMessage="String不能超过6个字" ClientValidationFunction="CustomValidator_txtStr_ClientValidate"
                onservervalidate="CustomValidator_txtStr_ServerValidate"></asp:CustomValidator>
        </p>
        <p>
            <asp:Button ID="btnSave1" runat="server" Text="按钮（验证）" onclick="btnSave_Click" ValidationGroup="1"/>
            <asp:Button ID="btnSave2" runat="server" Text="按钮（不验证）" onclick="btnSave_Click" />
            <asp:Label Text="" runat="server" ID="labMsg" />
        </p>
    </div>
    </form>
</body>
</html>
