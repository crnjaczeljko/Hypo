<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Lokacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Lokacije</legend>

    <div class="display-label">Naziv</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Naziv) %>
    </div>

    <div class="display-label">Zaposlenici</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici.Ime) %>
    </div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.id_lok }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
