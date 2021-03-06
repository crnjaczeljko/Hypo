﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Rezervacije>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista odobrenih Rezervacija
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <% Html.RenderPartial("ViewSrch1");%>
    </div>

    <table id="hor-minimalist-b">
        <tr>
            <th></th>
            <th></th>
            <th></th>
            <th></th>
            <th>Zaposlenik</th>
            <th>Kreiran zahtjev</th>
            <th>Automobil</th>
            <th>Reg Oznaka</th>
            <th>Polazak</th>
            <th>Dolazak</th>
            <th>Mjesto polaska</th>
            <th>Odredište</th>
            <th>Tip putovanja</th>
            <th>Broj putnika</th>
            <th>Status</th>
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
                <a href='<%: Url.Action("Details", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="../../Content/Images/Zoom-icon.png" alt="Edit"
                        style="border-style: none" />
                </a>
            </td>
            <td>
                <a href='<%: Url.Action("OtkazatiPage", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="../../Content/Images/delete.png" alt="Otkazati"
                        style="border-style: none" />
                </a>
            </td>
            <td>
                <a href='<%: Url.Action("PotvrdaEditPage", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="../../Content/Images/accept.png" alt="Izmjena Vozila"
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
                <%: Html.DisplayFor(modelItem => item.Automobil.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Automobil.RegBr) %>
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
                <%: Html.DisplayFor(modelItem => item.TipRezervacije.Naziv) %>
            </td>
            <td style="text-align: center">
                <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
            </td>

            <td style="text-align: center">
                <% int? st = item.Status;
                   if (st == 0)
                   { %>
                <img src="/Content/images/0.png" alt="Odobrenje" style="border-style: none" />
                <% }
                       if (st == 1)
                       { %>
                <img src="/Content/images/1.png" alt="Odobreno" style="border-style: none" />
                <% }
                       if (st == 2)
                       { %>
                <img src="/Content/images/2.png" alt="Odbijeno" style="border-style: none" />
                <% }
                       if (st == 3)
                       { %>
                <img src="/Content/images/3.png" alt="Zapisnik" style="border-style: none" />
                <% }
                       if (st == 4)
                       { %>
                <img src="/Content/images/4.png" alt="Zakljuceno" style="border-style: none" />
                <% }
                       Html.DisplayFor(modelItem => st); %>
            </td>
        </tr>
        <% } %>
    </table>
    <div align="center">
        <% if (Model.HasPreviousPage)
           { %>
        <%: Html.ActionLink("<<", "OdobrenoList", new {page = 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <%: Html.ActionLink("< Prev", "OdobrenoList", new
                    {
                        page = Model.PageNumber - 1,
                        sortOrder = ViewBag.CurrentSort,
                        currentFilter = ViewBag.CurrentFilter
                    }) %>
        <% }
           else
           { %>
            << &nbsp;< Prev
        <% } %>
        -  Page <%: (Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) %>
        of <%: Model.PageCount %>   -
        <% if (Model.HasNextPage)
           { %>
        <%: Html.ActionLink("Next>", "OdobrenoList", new {page = Model.PageNumber + 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <%: Html.ActionLink(">>", "OdobrenoList", new {page = Model.PageCount, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <% }
           else
           { %>
            Next> &nbsp;>>
        <% } %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
    <hr />
    <%: Html.ActionLink("Ispis", "ReportPage") %>
      
</asp:Content>