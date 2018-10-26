<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Mobiteli.Models.vEronetPotrosnja>>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>PrintView</title>
</head>
<body>
    
                 <table style="width: 100%;">
                 <th >
                     <td style="border-style: none;">
                               <h1>&nbsp;&nbsp;@ViewBag.Title</h1>
                           </td>
           
                       <td id="logindisplay">
                 korisnik: <strong>@User.Identity.Name</strong>!
             </td>
                 </th>
             </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <th>
                id_tel
            </th>
            <th>
                telefon
            </th>
            <th>
                GODINA
            </th>
            <th>
                MJESEC
            </th>
            <th>
                SIFRA
            </th>
            <th>
                OPIS_STAVKE
            </th>
            <th>
                Iznos
            </th>
            <th>
                idlnk
            </th>
            <th></th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.id_tel) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.telefon) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.GODINA) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.MJESEC) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.SIFRA) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.OPIS_STAVKE) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Iznos) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.idlnk) %>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.id_potr }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.id_potr }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.id_potr }) %>
            </td>
        </tr>
    <% } %>
    
    </table>
</body>
</html>
