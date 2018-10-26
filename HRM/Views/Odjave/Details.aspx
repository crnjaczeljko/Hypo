<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Odjave>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Odjave</legend>

    <div class="display-label">datum_iniciranja</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_iniciranja) %>
    </div>

    <div class="display-label">datum_zavrsetka</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_zavrsetka) %>
    </div>

    <div class="display-label">Zaposlenici1</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici1.Ime) %>
    </div>

    <div class="display-label">Zaposlenici</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici.Ime) %>
    </div>

    <div class="display-label">OD</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.OD.Naziv) %>
    </div>

    <div class="display-label">RadnaMjesta</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.RadnaMjesta.Naziv) %>
    </div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.OdjaveId }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
