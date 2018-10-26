<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Rezervacije</legend>

        <%: Html.HiddenFor(model => model.id_rez) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_zaposlenik, "Zaposlenici") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_zaposlenik", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_zaposlenik) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Kontakt_Tel) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Kontakt_Tel) %>
            <%: Html.ValidationMessageFor(model => model.Kontakt_Tel) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_auto, "Automobil") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_auto", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_auto) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.relacija) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.relacija) %>
            <%: Html.ValidationMessageFor(model => model.relacija) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_kreiranja) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_kreiranja) %>
            <%: Html.ValidationMessageFor(model => model.datum_kreiranja) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Status) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Status) %>
            <%: Html.ValidationMessageFor(model => model.Status) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_polLok, "Lokacije") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_polLok", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_polLok) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_polaska) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_polaska) %>
            <%: Html.ValidationMessageFor(model => model.datum_polaska) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_dolaska) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_dolaska) %>
            <%: Html.ValidationMessageFor(model => model.datum_dolaska) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.odobreno) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.odobreno) %>
            <%: Html.ValidationMessageFor(model => model.odobreno) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_zakljucenja) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_zakljucenja) %>
            <%: Html.ValidationMessageFor(model => model.datum_zakljucenja) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.datum_odobrenja) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.datum_odobrenja) %>
            <%: Html.ValidationMessageFor(model => model.datum_odobrenja) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.broj_putnika) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.broj_putnika) %>
            <%: Html.ValidationMessageFor(model => model.broj_putnika) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Opis) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Opis) %>
            <%: Html.ValidationMessageFor(model => model.Opis) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_tiprez, "TipRezervacije") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_tiprez", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_tiprez) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Komentar) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Komentar) %>
            <%: Html.ValidationMessageFor(model => model.Komentar) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_grad, "Mjesta") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_grad", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_grad) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik1, "Zaposlenici1") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik1", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik1) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik2, "Zaposlenici2") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik2", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik2) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik3, "Zaposlenici3") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik3", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik3) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik4, "Zaposlenici4") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik4", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik4) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik5, "Zaposlenici5") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik5", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik5) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_Putnik6, "Zaposlenici6") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_Putnik6", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik6) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_lok_zaduzenje, "Lokacije2") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_lok_zaduzenje", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_lok_zaduzenje) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.id_lok_razduzenje, "Lokacije1") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("id_lok_razduzenje", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_lok_razduzenje) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Poc_KM) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Poc_KM) %>
            <%: Html.ValidationMessageFor(model => model.Poc_KM) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Zav_KM) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Zav_KM) %>
            <%: Html.ValidationMessageFor(model => model.Zav_KM) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Troskovi) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Troskovi) %>
            <%: Html.ValidationMessageFor(model => model.Troskovi) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Zapisnik) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Zapisnik) %>
            <%: Html.ValidationMessageFor(model => model.Zapisnik) %>
        </div>

        <p>
            <input type="submit" value="Save" />
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
