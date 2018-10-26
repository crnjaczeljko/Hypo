<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Rezervacije>>" %>

<%@ Import Namespace="Vozila.Models" %>

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
            <th>Status</th>
            <th>Automobil</th>
            <th>Reg. oznaka</th>
            <th>Tip puta</th>
            <th>Broj putnika</th>
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
                <% if (item.Status == 0)
                   { %>
                <a href='<%: Url.Action("Edit", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="../../Content/Images/Edit.png" alt="Edit"
                        style="border-style: none" />
                </a>
                <% } %>

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
            <td style="text-align: center">
                <% int? st = item.Status;
                   if (st == 0)
                   {%>
                <img src="<%:Url.Content("~/Content/images/0.png")%>" alt="Odobrenje" style="border-style: none" />
                <% }
                       if (st == 1)
                       {%>
                <img src="<%:Url.Content("~/Content/images/1.png")%>" alt="Odobreno" style="border-style: none" />
                <% }
                       if (st == 2)
                       {%>
                <img src="<%:Url.Content("~/Content/images/2.png")%>" alt="Odbijeno" style="border-style: none" />
                <% }
                       if (st == 3)
                       {%>
                <img src="<%:Url.Content("~/Content/images/3.png")%>" alt="Zapisnik" style="border-style: none" />
                <% }
                       if (st == 4)
                       {%>
                <img src="<%:Url.Content("~/Content/images/4.png")%>" alt="Zaključeno" style="border-style: none" />
                
                <%}
                       Html.DisplayFor(modelItem => st); %>
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
            <td>
                <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
            </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Nova Rezervacija", "Create") %>
    <hr />
    <%: Html.ActionLink("Moje rezervacije", "Index") %>
    <hr />
    <%: Html.ActionLink("Odobrene rezervacije", "OdobrenoList") %>
    <hr />
    <%: Html.ActionLink("Za Zapisnik", "ZapisnikList") %>
</asp:Content>