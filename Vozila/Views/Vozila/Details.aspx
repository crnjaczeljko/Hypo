<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Automobil>" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Automobil</legend>

    <div class="display-label">Naziv</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Naziv) %>
    </div>

    <div class="display-label">Lokacije</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
    </div>

    <div class="display-label">RegBr</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.RegBr) %>
    </div>

    <div class="display-label">Tip</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Tip) %>
    </div>

    <div class="display-label">God_Proiz</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.God_Proiz) %>
    </div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.id_auto }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
