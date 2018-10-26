<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
 Inherits="System.Web.Mvc.ViewPage<IEnumerable<Vozila.Models.Rezervacije>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>ListPage</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th>
            id_zaposlenik
        </th>
        <th>
            Kontakt_Tel
        </th>
        <th>
            id_auto
        </th>
        <th>
            relacija
        </th>
        <th>
            datum_kreiranja
        </th>
        <th>
            Status
        </th>
        <th>
            id_polLok
        </th>
        <th>
            datum_polaska
        </th>
        <th>
            datum_dolaska
        </th>
        <th>
            odobreno
        </th>
        <th>
            datum_zakljucenja
        </th>
        <th>
            datum_odobrenja
        </th>
        <th>
            broj_putnika
        </th>
        <th>
            Opis
        </th>
        <th>
            id_tiprez
        </th>
        <th>
            Komentar
        </th>
        <th>
            id_grad
        </th>
        <th>
            id_Putnik1
        </th>
        <th>
            id_Putnik2
        </th>
        <th>
            id_Putnik3
        </th>
        <th>
            id_Putnik4
        </th>
        <th>
            id_Putnik5
        </th>
        <th>
            id_Putnik6
        </th>
        <th>
            id_lok_zaduzenje
        </th>
        <th>
            id_lok_razduzenje
        </th>
        <th>
            Poc_KM
        </th>
        <th>
            Zav_KM
        </th>
        <th>
            Troskovi
        </th>
        <th>
            Zapisnik
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_zaposlenik) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Kontakt_Tel) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_auto) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.relacija) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_kreiranja) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Status) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_polLok) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_polaska) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_dolaska) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.odobreno) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_zakljucenja) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_odobrenja) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Opis) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_tiprez) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Komentar) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_grad) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik1) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik2) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik3) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik4) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik5) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_Putnik6) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_lok_zaduzenje) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.id_lok_razduzenje) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Poc_KM) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Zav_KM) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Troskovi) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Zapisnik) %>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.id_rez }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.id_rez }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.id_rez }) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
