<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Troskovi>>" %>
<%@ Import Namespace="Vozila.Models" %>

<table id="hor-minimalist-b">
    <tr>
        <th></th>
        <th></th>
        <th>Naziv</th>
        <th>Iznos</th>
        <th>Valuta</th>
        <th>Iznos KM</th>
    </tr>

    <% foreach (Troskovi item in Model)
       { %>
        <tr>
            <td>
                <a href='<%: Url.Action("TroskoviEdit", new {id = item.id_tr}) %>' style="border-style: none">
                    <img src="/Content/images/Edit.png" alt="Edit" style="border-style: none" />
                </a>
            </td>
            <td>
                <a href='<%: Url.Action("TroskoviDelete", new {id = item.id_tr}) %>'>
                    <img src="/Content/images/delete.png" alt="Delete" style="border-style: none" />
                </a>
            </td>
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
        <th></th>
        <th></th>
        <th></th>
        <th></th>
        <th>Ukupno</th>
        <th><% if (ViewBag.Zbroj == null)
               {
                   ViewBag.Zbroj = 0;
               }%>
               <%:ViewBag.Zbroj %></th>
    </tr>
</table>