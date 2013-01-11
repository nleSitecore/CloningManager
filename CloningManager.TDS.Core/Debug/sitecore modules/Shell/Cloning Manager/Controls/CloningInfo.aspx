<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloningInfo.aspx.cs" Inherits="Sitecore.SharedSource.CloningManager.Controls.CloningInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cloning Manager</title>
    <link href="/sitecore/shell/themes/standard/default/Dialogs.css" rel="stylesheet" />

<script src="~/sitecore/shell/controls/Gecko.js" type="text/javascript"></script>
<script src="~/sitecore/shell/controls/Sitecore.js" type="text/javascript"></script>
<script src="~/sitecore/shell/controls/lib/prototype/prototype.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function onClose() {            
            window.close();
        }
        function onCancel() 
      {
          if (confirm("Are you sure you want to cancel?")) {
              window.close();
             } 
    window.event.cancelBubble = true; return false; } </script> <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;"><table border="0" cellpadding="0" cellspacing="0" style="background:white;" width="100%">
	<tbody><tr>
		<td valign="top"><img style="margin:4px 8px 4px 8px;" src="/~/icon/Core3/32x32/views.png" border="0" alt="" width="32px" height="32px"></td>
		<td width="100%" valign="top"><div style="padding:2px 16px 0px 0px;"><div style="color:black;padding:0px 0px 4px 0px;font:bold 9pt tahoma;">Clone Manager</div>
        <div style="color:#333333;">Manage and view cloned Items of the selected Item</div></div></td>
	</tr>
	<tr>
		<td colspan="2"><div style="background:#dbdbdb;"><img src="/sitecore/images/blank.gif" border="0" alt="" width="1" height="1" align=""></div></td>
	</tr>
</tbody></table>
</div>
    <asp:Literal runat="server" ID="ltInfo"></asp:Literal>
        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="pl" 
            onitemdatabound="ListView1_ItemDataBound">
        <LayoutTemplate>
        <table>
        <thead style="background-color:#777; font:10pt tahoma;">
        <td>
        Auto Adopt
        </td>
        <td>Item Path</td>
        <td>Inherit from Version</td>
        <td>Fields in Clone changed</td>
        <//thead>   
        <asp:PlaceHolder ID="pl" runat="server"></asp:PlaceHolder>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
        <tr style="background-color:#CCC;">
        <td>
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" /><asp:Literal runat="server" ID="ItemID" Visible="false" Text='<%#Eval("ID").ToString() %>'></asp:Literal></td>
            <td><asp:HyperLink ID="HyperLink1" runat="server"><%#Eval("Paths.FullPath") %></asp:HyperLink></td>            
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>               
                 <td><asp:Label ID="lbChangedFields" runat="server" Text=""></asp:Label></td>   
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr style="background-color:white;">
        <td>
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" /><asp:Literal runat="server" ID="ItemID" Visible="false" Text='<%#Eval("ID").ToString() %>'></asp:Literal></td>
            <td><asp:HyperLink ID="HyperLink1" runat="server"><%#Eval("Paths.FullPath") %></asp:HyperLink></td>            
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>               
                <td><asp:Label ID="lbChangedFields" runat="server" Text=""></asp:Label></td>               
            </tr>
        </AlternatingItemTemplate>
        
        </asp:ListView>
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclientclick="onCancel();"/>
            <asp:Button ID="btnDone" runat="server" Text="Done" 
            onclientclick="onClose();" Visible="false"/>
    
    </form>
</body>
</html>
