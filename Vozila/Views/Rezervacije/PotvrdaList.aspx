<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Rezervacije>>" %>

<%@ Import Namespace="Vozila.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: ViewBag.Message = "Lista rezervacija za potvrdu" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        function OnNameValidation(s, e) {
            if (e.value == null)
                e.isValid = false;
            var name = String(e.value);
            if (name == "")
                e.isValid = false;
            if (name.length> 50) {
                e.isValid = false;
                e.errorText = "Must be under 50 characters";
            }
        }

        function OnAgeValidation(s, e) {
            if (e.value == null || e.value == "")
                return;
            var age = Number(e.value);
            if (isNaN(age) || age < 18 || age> 100)
                e.isValid = false;
        }
        // ]]>
    </script>
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
            <th>Broj putnika</th>
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
                <a href='<%: Url.Action("PotvrdaPage", new {id = item.id_rez}) %>' style="border-style: none">
                    <img src="/Content/images/accept.png" alt="Potvrda" style="border-style: none" />
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
            <td style="text-align: center">
                <% var st = item.Status;
                   if (st == 0)
                   {%>
                <img src="/Content/images/0.png" alt="Odobrenje" style="border-style: none" />
                <% }
                       if (st == 1)
                       {%>
                <img src="/Content/images/1.png" alt="Odobreno" style="border-style: none" />
                <% }
                        if (st == 2)
                        {%>
                <img src="/Content/images/2.png" alt="Odbijeno" style="border-style: none" />
                <% }
                        if (st == 3)
                        {%>
                <img src="/Content/images/3.png" alt="Zapisnik" style="border-style: none" />
                <% }
                       if (st == 4)
                       {%>
                <img src="/Content/images/4.png" alt="Zaključeno" style="border-style: none" />
                <% }
                       Html.DisplayFor(modelItem => st); %>
            </td>
            <td style="text-align: center">
                <%: Html.DisplayFor(modelItem => item.broj_putnika) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.TipRezervacije.Naziv) %>
            </td>
        </tr>
        <% } %>
    </table>

    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>