<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchPage.aspx.cs" Inherits="SearchPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:360px;color:Black">

        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Enter Seach Query"></asp:Label>
        <br />
        <asp:TextBox ID="SearchQuery" runat="server" Width="465px" Font-Bold="False"
            Height="40px" Font-Size="Medium"></asp:TextBox>
        <br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="61px"
            Width="177px">
            <asp:ListItem>Spell Correction</asp:ListItem>
            <asp:ListItem>Soundex</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="SearchButton" runat="server" onclick="Button1_Click"
            Text="Search" Height="35px" Width="97px" />
        <asp:Button ID="Button1" runat="server" Height="35px" onclick="Button1_Click1"
            Text="Clear" Width="97px" />
        <br />
        <br />
        Results<br />
        <asp:ListBox ID="ListOfResults" runat="server" Height="131px" Width="560px"
            onselectedindexchanged="ListOfResults_SelectedIndexChanged">
        </asp:ListBox>
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Go"
            Width="87px" />
        <br />
        Did you mean?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:ListBox ID="ListOfWords" runat="server" Height="101px" Width="554px">
        </asp:ListBox>

        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click"
            Text="Correct Search Query" Width="148px" />

    </div>
    </form>
</body>
</html>
