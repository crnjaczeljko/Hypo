<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Rezervacije>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewBag.Message = "Lista rezervacija za zakljucenje" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table id="hor-minimalist-b">
        <tr>
            <th></th>
            <th></th>
            <th>Zaposlenik</th>
            <th>Kreirano</th>
            <th>Polazak</th>
            <th>Povratak</th>
            <th>Od</th>
            <th>Do</th>
            <th>Automobil</th>
            <th>Broj putnika</th>
            <th>Tip puta</th>
            <th>Pocet. KM</th>
            <th>Zavr. KM</th>
        </tr>
        <% int i = 0;
           int pg = (Model.PageNumber - 1) * 10;%>

        <% foreach (Rezervacije item in Model)
           { %>
        <tr>
            <td>
                <% { i = i + 1; }%>
                <%: Html.Label((i+pg).ToString())%>
            </td>
            <td>
                <a href='<%: Url.Action("ZakljucitiPage", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="../../Content/Images/accept.png" alt="Edit"
                        style="border-style: none" />
                </a>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Zaposlenici.ImePrezime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.datum_kreiranja) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.datum_polaska) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.datum_dolaska) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Lokacije.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Mjesta.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Automobil.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.TipRezervacije.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Poc_KM) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Zav_KM) %>
            </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>