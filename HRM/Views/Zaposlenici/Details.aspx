<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Zaposlenici>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Zaposlenici</legend>

    <div class="display-label">Ime</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Ime) %>
    </div>

    <div class="display-label">Prezime</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Prezime) %>
    </div>

    <div class="display-label">ImePrezime</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.ImePrezime) %>
    </div>

    <div class="display-label">OD2</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.OD2.Naziv) %>
    </div>

    <div class="display-label">RadnaMjesta</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.RadnaMjesta.Naziv) %>
    </div>

    <div class="display-label">PripadnostUBihGrupi</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.PripadnostUBihGrupi.naziv) %>
    </div>

    <div class="display-label">jmbg</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.jmbg) %>
    </div>

    <div class="display-label">datum_pocetka_rada</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_pocetka_rada) %>
    </div>

    <div class="display-label">privremeno</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.privremeno) %>
    </div>

    <div class="display-label">datum_prestanka</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.datum_prestanka) %>
    </div>

    <div class="display-label">hrm_broj</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.hrm_broj) %>
    </div>

    <div class="display-label">tip_zaposlenja</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.tip_zaposlenja) %>
    </div>

    <div class="display-label">email</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.email) %>
    </div>

    <div class="display-label">ad</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.ad) %>
    </div>

    <div class="display-label">sysdate</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.sysdate) %>
    </div>

    <div class="display-label">Napomena</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.Napomena) %>
    </div>

    <div class="display-label">NotesEmail</div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.NotesEmail) %>
    </div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.id_zaposlenici }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>
