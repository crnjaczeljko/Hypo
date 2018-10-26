<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Rezervacije>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewBag.Message = "Otvorene rezervacije" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <% Html.RenderPartial("ViewSrch1");%>
    </div>

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
            <th>Status</th>
            <th>Automobil</th>
            <th>Reg.br</th>
            <th>Tip puta</th>
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
                <a href='<%: Url.Action("ZapisnikPage", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="/Content/images/Edit.png" alt="Edit" style="border-style: none" />
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
                <%: Html.DisplayFor(modelItem => item.Status) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Automobil.Naziv) %>
            </td>
                        <td>
                <%: Html.DisplayFor(modelItem => item.Automobil.RegBr) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.TipRezervacije.Naziv) %>
            </td>
        </tr>
        <% } %>
    </table>

    <div align="center">
        <% if (Model.HasPreviousPage)
           { %>
        <%: Html.ActionLink("<<", "ZapisnikList", new {page = 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <%: Html.ActionLink("< Prev", "ZapisnikList", new
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
        <%: Html.ActionLink("Next>", "ZapisnikList", new {page = Model.PageNumber + 1, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <%: Html.ActionLink(">>", "ZapisnikList", new {page = Model.PageCount, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter}) %>
        <% }
           else
           { %>
            Next> &nbsp;>>
        <% } %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
      <hr />
                                  <% if (Page.User.Identity.Name == "HYPO\\zeljkoc"
                                         || Page.User.Identity.Name == "HYPO\\nadad"
                                         || Page.User.Identity.Name == "HYPO\\damirs"
                                         || Page.User.Identity.Name == "HYPO\\dariom"
                                         || Page.User.Identity.Name == "HYPO\\igorp"
                                         || Page.User.Identity.Name == "HYPO\\igorj"
                                         || Page.User.Identity.Name == "HYPO\\suhretaz"
                                         || Page.User.Identity.Name == "HYPO\\sonjas"
                                         || Page.User.Identity.Name == "HYPO\\ilfadm"
                                         || Page.User.Identity.Name == "HYPO\\merisas"
                                         || Page.User.Identity.Name == "HYPO\\snjezanav"
                                         )
                                     { %> 
    <%: Html.ActionLink("Obavijest za zakljucenje", "ObavijestZakljuciti") %>
    <% } %>
</asp:Content>