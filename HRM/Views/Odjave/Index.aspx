<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<HRM.Models.Odjave>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table  id="hor-minimalist-b">
    <tr>
        <th class="auto-style1">
            datum_iniciranja
        </th>
        <th>
            datum_zavrsetka
        </th>
        <th>
            Zaposlenik
        </th>
        <th>
            OD
        </th>   
             <th>
            Voditelj
        </th>

        <th>
            RadnaMjesta
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td class="auto-style1">
            <%: Html.DisplayFor(modelItem => item.datum_iniciranja) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_zavrsetka) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Zaposlenici1.ImePrezime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.OD.Naziv) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.Zaposlenici.ImePrezime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.RadnaMjesta.Naziv) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width: 170px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
