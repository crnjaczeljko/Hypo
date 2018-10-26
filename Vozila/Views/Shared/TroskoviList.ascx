<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Vozila.Models.Troskovi>>" %>

<table id="hor-minimalist-b" width="100%">
    <tr>
        <th>Naziv</th>
        <th>Iznos</th>
        <th>Valuta</th>
        <th>Iznos KM</th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.Naziv) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Iznos) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Valute.Oznaka) %>
        </td>
                <td>
            <%: Html.DisplayFor(modelItem => item.IznosKM) %>
        </td>
    </tr>
<% } %>
 <tr>
        <th> </th>
        <th> </th>
        <th>Ukupno</th>
        <th><% if (ViewBag.Zbroj == null)
               {
                   ViewBag.Zbroj = 0;
               }%>
               <%:ViewBag.Zbroj %></th>
    </tr>
</table>
