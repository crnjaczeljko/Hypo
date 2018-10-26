<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.vPregledVozila>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Lista Rezervacija
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
           {%>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                <td style="border-style: none">Pretraga po zaposleniku: <%:Html.TextBox("SearchString")%>
                </td>
                <td style="border-style: none">Datum Od: <%:Html.TextBox("datumOd", string.Empty, new { style = "width:150px", @class = "datepicker" })%>
                 &nbsp;&nbsp;Datum do: <%:Html.TextBox("datumDo", string.Empty, new { style = "width:150px", @class = "datepicker" })  %>
                </td>
                <td style="border-style: none">
                    <input type="submit" value="Pretraga" />
                </td>
            </tr>
            <tr>
                <td>Automobil : <%: Html.DropDownList("id_auto",String.Empty) %> </td>
                <td>Lokacija : <%: Html.DropDownList("id_lok",String.Empty) %> </td>
                <td></td>
            </tr>
        </table>
        <% }%>
    </div>
    <hr />
    <table id="hor-minimalist-b">
        <tr>
            <th></th>
            <th></th>
            <th>Automobil</th>
            <th>Reg. br</th>
            <th>Djelatnik</th>
            <th>Destinacija</th>
            <th>Polazak</th>
            <th>Povratak</th>
            <th>Broj putnika</th>
            <th>Status</th>
        </tr>
        <% int i = 0;
           int pg = (Model.PageNumber - 1) * 10;%>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <% { i = i + 1; }%>
                <%: Html.Label((i+pg).ToString())%>
            </td>
            <td>
                <a href='<%:Url.Action("Details", new {id = item.id_rez})%>' style="border-style: none">
                    <img src="<%: Url.Content("~/Content/Images/Zoom-icon.png") %>" alt="Edit"
                        style="border-style: none" />
                </a>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.RegBr) %>
            </td>
            <td>
                <% if (item.ImePrezime != null)
                   { %>
                <%: Html.DisplayFor(modelItem => item.ImePrezime) %>
                <% } %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Destinacija) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.datum_polaska) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.datum_dolaska) %>
            </td>
            <td style="text-align: center">
                <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
            </td>
            <td style="text-align: center">
                <% var st = item.Status;
                   if (st == 0)
                   { %>
                <img src="<%: Url.Content("~/Content/Images/0.png") %>" alt="Odobrenje" style="border-style: none" />
                <% }
                   if (st == 1)
                   { %>
                <img src="<%: Url.Content("~/Content/Images/1.png") %>" alt="Odobreno" style="border-style: none" />
                <% }
                   if (st == 2)
                   { %>
                <img src="<%: Url.Content("~/Content/Images/2.png") %>" alt="Odbijeno" style="border-style: none" />
                <% }
                       if (st == 3)
                       { %>
                <img src="<%: Url.Content("~/Content/Images/3.png") %>" alt="Zapisnik" style="border-style: none" />
                <% }
                       if (st == 4)
                       { %>
                <img src="<%: Url.Content("~/Content/Images/4.png") %>" alt="Zakljuceno" style="border-style: none" />
                <% }
                       Html.DisplayFor(modelItem => st); %>
            </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Nova rezervacija", "Create") %>
    <hr />
    <%: Html.ActionLink("Slobodna vozila", "SlobodnaVozila") %>
    <hr />
    <%: Html.ActionLink("Zahtjevi", "PotvrdaList") %>
    <hr />
    <%: Html.ActionLink("Nezakljuceno", "ZakljucitiList") %>
    <hr />
    <%: Html.ActionLink("Odobreno", "OdobrenoList") %>
    <hr />
    <%: Html.ActionLink("Odbijene", "OdbijenoList") %>
    <hr />
    <%: Html.ActionLink("Otvorene", "ZapisnikList") %>
    <hr />
    <%: Html.ActionLink("Zatvorene", "ZatvorenoList") %>
    <hr />
    <%: Html.ActionLink("Pregled vozila", "PregledVozila") %>
</asp:Content>