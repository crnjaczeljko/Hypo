<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>ViewPage1</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="ALL">Svi</asp:ListItem>
            <asp:ListItem Value="ON">Aktivni</asp:ListItem>
            <asp:ListItem Value="Off">Isključeno</asp:ListItem>
        </asp:RadioButtonList>
        
    </div>
    </form>
</body>
</html>
