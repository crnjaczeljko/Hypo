<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Zaposlenici>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ažuriranje
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <table>

        <%: Html.HiddenFor(model => model.id_zaposlenici) %>
        <tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.Ime) %>
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Ime) %>
            <%: Html.ValidationMessageFor(model => model.Ime) %>
        </td>
</tr><tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.Prezime) %>
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Prezime) %>
            <%: Html.ValidationMessageFor(model => model.Prezime) %>
        </td>
    </tr><tr>

        <td class="col1"> Org. dio</td>
        <td class="col2">
            <%: Html.DropDownList("id_od", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_od) %>
        </td>
        </tr><tr>
        <td class="col1">Radno mjesto</td>
        <td class="col2">
            <%: Html.DropDownList("id_rm", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_rm) %>
        </td>
            </tr><tr>
        <td class="col1">Radi u</td>
        <td class="col2">
            <%: Html.DropDownList("id_pripadnost", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_pripadnost) %>
        </td>
</tr><tr>
        <td class="col1">Jmbg</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.jmbg) %>
            <%: Html.ValidationMessageFor(model => model.jmbg) %>
        </td>
    </tr><tr>
        <td class="col1">Početak rada</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.datum_pocetka_rada) %>
            <%: Html.ValidationMessageFor(model => model.datum_pocetka_rada) %>
        </td>
        </tr><tr>
        <td class="col1">Datum prestanka</td>
        <td class="col2">
            <%: Html.DisplayFor(model => model.datum_prestanka) %>
                  </td>
</tr><tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.hrm_broj) %>
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.hrm_broj) %>
            <%: Html.ValidationMessageFor(model => model.hrm_broj) %>
        </td>
    </tr><tr>
        <td class="col1">Tip zaposlenja</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.tip_zaposlenja) %>
            <%: Html.ValidationMessageFor(model => model.tip_zaposlenja) %>
        </td>
        </tr><tr>
        <td class="col1">Napomena</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Napomena) %>
            <%: Html.ValidationMessageFor(model => model.Napomena) %>
        </td>
</tr>
    </table>
        <p>
            <input type="submit" value="Spremiti" />
        </p>
<% } %>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>
