<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Prijave>" %>

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
        <legend>Prijave</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_zaposlenik, "Zaposlenici") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_zaposlenik", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_zaposlenik) %>
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

        <div class="editor-label">
            <%: Html.LabelFor(model => model.sysdate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.sysdate) %>
            <%: Html.ValidationMessageFor(model => model.sysdate) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.potvrda_em_ad) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.potvrda_em_ad) %>
            <%: Html.ValidationMessageFor(model => model.potvrda_em_ad) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.potvrda_bmc) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.potvrda_bmc) %>
            <%: Html.ValidationMessageFor(model => model.potvrda_bmc) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.potvrda_vod) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.potvrda_vod) %>
            <%: Html.ValidationMessageFor(model => model.potvrda_vod) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.potvrda_ostale_app) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.potvrda_ostale_app) %>
            <%: Html.ValidationMessageFor(model => model.potvrda_ostale_app) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.potvrda_all) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.potvrda_all) %>
            <%: Html.ValidationMessageFor(model => model.potvrda_all) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_voditelj, "Zaposlenici1") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_voditelj", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_voditelj) %>
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
