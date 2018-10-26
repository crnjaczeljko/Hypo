<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<style type="text/css">
    .style1
    {
        width: 325px;
    }
</style>

<table>
    <tr>
        <td class="style1">Datum od:&nbsp;
            <asp:TextBox ID="txtDatOd" runat="server"></asp:TextBox>
        </td>
        <td>
        <td class="style1">Datum do:&nbsp;
            <asp:TextBox ID="txtDatDo" runat="server"></asp:TextBox>
        </td>
    </tr>
</table>