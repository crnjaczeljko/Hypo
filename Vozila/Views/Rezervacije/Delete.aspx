<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Rezervacije</legend>

    <div class="display-label">Zaposlenici</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici.Ime) %>
    </div>

    <div class="display-label">Kontakt_Tel</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Kontakt_Tel) %>
    </div>

    <div class="display-label">Automobil</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Automobil.Naziv) %>
    </div>

    <div class="display-label">relacija</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.relacija) %>
    </div>

    <div class="display-label">datum_kreiranja</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_kreiranja) %>
    </div>

    <div class="display-label">Status</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Status) %>
    </div>

    <div class="display-label">Lokacije</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
    </div>

    <div class="display-label">datum_polaska</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_polaska) %>
    </div>

    <div class="display-label">datum_dolaska</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_dolaska) %>
    </div>

    <div class="display-label">odobreno</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.odobreno) %>
    </div>

    <div class="display-label">datum_zakljucenja</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_zakljucenja) %>
    </div>

    <div class="display-label">datum_odobrenja</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_odobrenja) %>
    </div>

    <div class="display-label">broj_putnika</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.broj_putnika) %>
    </div>

    <div class="display-label">Opis</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Opis) %>
    </div>

    <div class="display-label">TipRezervacije</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.TipRezervacije.Naziv) %>
    </div>

    <div class="display-label">Komentar</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Komentar) %>
    </div>

    <div class="display-label">Mjesta</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Mjesta.Naziv) %>
    </div>

    <div class="display-label">Zaposlenici1</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici1.Ime) %>
    </div>

    <div class="display-label">Zaposlenici2</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici2.Ime) %>
    </div>

    <div class="display-label">Zaposlenici3</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici3.Ime) %>
    </div>

    <div class="display-label">Zaposlenici4</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici4.Ime) %>
    </div>

    <div class="display-label">Zaposlenici5</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici5.Ime) %>
    </div>

    <div class="display-label">Zaposlenici6</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zaposlenici6.Ime) %>
    </div>

    <div class="display-label">Lokacije2</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Lokacije2.Naziv) %>
    </div>

    <div class="display-label">Lokacije1</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Lokacije1.Naziv) %>
    </div>

    <div class="display-label">Poc_KM</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Poc_KM) %>
    </div>

    <div class="display-label">Zav_KM</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zav_KM) %>
    </div>

    <div class="display-label">Troskovi</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Troskovi) %>
    </div>

    <div class="display-label">Zapisnik</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Zapisnik) %>
    </div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
