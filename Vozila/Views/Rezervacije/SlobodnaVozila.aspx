<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.procSlobodnaAuta_Result>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista Slobodnih vozila
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%: Url.Content("~/Content/themes/base/jquery.ui.all.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%: Url.Content("~/Scripts/jquery-ui.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: 'dd.mm.yy',
                showStatus: true,
                showWeeks: false,
                highlightWeek: true,
                numberOfMonths: 1,
                showAnim: "scale",
                showOptions: {
                    origin: ["top", "left"]
                }
            }
            );
        });
    </script>

    <div class="blue">
        <% using (Html.BeginForm())
           {
        %>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                <td style="border-style: none">Datum Od: <%: Html.TextBox("datumOd", DateTime.Now.Date, new {style = "width:150px", @class = "datepicker"}) %>
                        &nbsp;&nbsp;Datum do: <%: Html.TextBox("datumDo", DateTime.Now.Date.AddDays(1), new {style = "width:150px", @class = "datepicker"}) %>
                </td>
                <td style="border-style: none">
                    <input type="submit" value="Pretraga" />
                </td>
            </tr>
        </table>
        <% } %>
    </div>
    <table id="hor-minimalist-b">
        <tr>
            <th></th>
            <th>Automobil</th>
            <th>Reg. br</th>
            <th>Lokacija</th>
            <th>Status</th>
        </tr>

        <% foreach (procSlobodnaAuta_Result item in Model)
           { %>
        <tr>
            <td>
                <%: Html.DisplayFor(modelItem => item.Row) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.RegBr) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Lokacija) %>
            </td>
            <td>
                <% var st = item.Servis;
                   if (st == null || st == false)
                   { %>
                <img src="<%: Url.Content("~/Content/Images/1.png") %>" alt="OK" style="border-style: none" />
                <% }
                       if (st == true)
                       { %>
                <img src="<%: Url.Content("~/Content/Images/service.png") %>" alt="servis" style="border-style: none" />
                <% }%>
            </td>
        </tr>
        <% } %>
    </table>

    <hr>
    <div align="center">
        <% if (Model.HasPreviousPage)
           {%>
        <%: Html.ActionLink("<<", "SlobodnaVozila", new { page = 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter })%>
        <%: Html.ActionLink("< Prev", "SlobodnaVozila", new
                   {
                       page = Model.PageNumber - 1,
                       sortOrder = ViewBag.CurrentSort,
                       currentFilter = ViewBag.CurrentFilter
                   })%>
        <% }
           else
           { %>
            << &nbsp;< Prev
        <% } %>
        -  Page <%: (Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) %>
        of <%: Model.PageCount %>   -
        <% if (Model.HasNextPage)
           { %>
        <%: Html.ActionLink("Next>", "SlobodnaVozila", new {page = Model.PageNumber + 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <%: Html.ActionLink(">>", "SlobodnaVozila", new { page = Model.PageCount, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter })%>
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
    <%: Html.ActionLink("Nova rezervacija", "Create") %>
    <hr />
    <%: Html.ActionLink("Zahtjevi", "PotvrdaList") %>
    <hr />
    <%: Html.ActionLink("Nezakljuceno", "ZakljucitiList") %>
    <hr />
    <%: Html.ActionLink("Odobreno", "OdobrenoList") %>
    <hr />
    <%: Html.ActionLink("Odbijene", "OdbijenoList") %>
    <hr />
    <%: Html.ActionLink("Zatvorene", "ZatvorenoList") %>
    <hr />
    <%: Html.ActionLink("Pregled vozila", "PregledVozila") %>
</asp:Content>