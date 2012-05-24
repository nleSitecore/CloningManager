<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloningInfo.aspx.cs" Inherits="Sitecore.SharedSource.CloningManager.Controls.CloningInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Literal runat="server" ID="ltInfo"></asp:Literal>
        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="pl">
        <LayoutTemplate>
        <div>
        <asp:PlaceHolder ID="pl" runat="server"></asp:PlaceHolder>
        </div>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="CheckBox1" runat="server" /><asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink><br />
        </ItemTemplate>
        
        </asp:ListView>
    </div>
    </form>
</body>
</html>
