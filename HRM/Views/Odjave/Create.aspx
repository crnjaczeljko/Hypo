<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Odjave>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Create</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Odjave</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_iniciranja) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_iniciranja) %>
            <%: Html.ValidationMessageFor(model => model.datum_iniciranja) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_zavrsetka) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_zavrsetka) %>
            <%: Html.ValidationMessageFor(model => model.datum_zavrsetka) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_zaposlenik, "Zaposlenici1") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_zaposlenik", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_zaposlenik) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_voditelj, "Zaposlenici") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_voditelj", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_voditelj) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_od, "OD") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_od", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_od) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_rm, "RadnaMjesta") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_rm", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_rm) %>
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
