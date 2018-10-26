<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Mobiteli.Models.Zaduzenje_Uredjaja>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>ViewPage1</title>
    <style type="text/css">
        .style1
        {
            width: 237px;
        }
        .style2
        {
            width: 157px;
        }
        .style3
        {
            width: 183px;
        }
    </style>
</head>
<body>
    <address>
        &nbsp;</address>
    <table frame="below" bgcolor="#009933" cellpadding="0" cellspacing="0">
        <tr>
            <th bgcolor="White" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <address>
                id_tel
                </address>
            </th>
            <th bgcolor="White" class="style2" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <address>
                id_ur
                </address>
            </th>
            <th bgcolor="White" class="style3" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <address>
                datum_zad
                </address>
            </th>
            <th bgcolor="White" class="style1" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <address>
                datum_razd
                </address>
            </th>
            <th bgcolor="White" 
                style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0">
                <address>
                </address>
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td bgcolor="White">
                <address style="width: 240px">
                <%: Html.DisplayFor(modelItem => item.id_tel) %>
                </address>
            </td>
            <td bgcolor="White" class="style2">
                <address style="width: 229px">
                <%: Html.DisplayFor(modelItem => item.id_ur) %>
                </address>
            </td>
            <td bgcolor="White" class="style3">
                <address>
                <%: Html.DisplayFor(modelItem => item.datum_zad) %>
                </address>
            </td>
            <td class="style1" bgcolor="White">
                <address>
                <%: Html.DisplayFor(modelItem => item.datum_razd) %>
                </address>
            </td>
            <td bgcolor="White">
                <address>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.id_zad }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.id_zad }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.id_zad }) %>
                </address>
                <address>
                    &nbsp;</address>
                <address>
                    &nbsp;</address>
                <address>
                    &nbsp;</address>
                <address>
                    &nbsp;</address>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
