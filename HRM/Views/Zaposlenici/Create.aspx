<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Zaposlenici>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Novi zaposlenik
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"> </script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"> </script>
    
                    <% Html.EnableClientValidation(); %>
                <% Html.BeginForm("Create", "Zaposlenici", FormMethod.Post, new {id = "validationForm", @class = "edit_form"}); %>
        <table>
            <tr>
                <td class="col1">Ime</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.Ime) %>
                       <%: Html.ValidationMessageFor(model => model.Ime) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Prezime</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.Prezime) %>
                    <%: Html.ValidationMessageFor(model => model.Prezime) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Sektor / Odjel</td>
                <td class="col2">
                    <%: Html.DropDownList("id_od", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.id_od) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Radno mjesto</td>
                <td class="col2">
                    <%: Html.DropDownList("id_rm", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.id_rm) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Zaposlen u</td>
                <td class="col2">
                    <%: Html.DropDownList("id_pripadnost", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.id_pripadnost) %>
                </td>
            </tr>
                        <tr>
                <td class="col1">Status zaposlenja</td>
                <td class="col2">
                    <%: Html.DropDownList("tip_zaposlenja", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.tip_zaposlenja) %>
                </td>
            </tr>
            <tr>
                <td class="col1">JMBG</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.jmbg) %>
                    <%: Html.ValidationMessageFor(model => model.jmbg) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Početak rada</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.datum_pocetka_rada) %>
                    <%: Html.ValidationMessageFor(model => model.datum_pocetka_rada) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Privremeni prestanak </td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.privremeno) %>
                    <%: Html.ValidationMessageFor(model => model.privremeno) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Datum prestanka</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.datum_prestanka) %>
                    <%: Html.ValidationMessageFor(model => model.datum_prestanka) %>
                </td>
            </tr>
            <tr>
                <td class="col1">HRM broj</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.hrm_broj) %>
                    <%: Html.ValidationMessageFor(model => model.hrm_broj) %>
                </td>
            </tr>

            <tr>
                <td class="col1">email</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.email) %>
                    <%: Html.ValidationMessageFor(model => model.email) %>
                </td>
            </tr>
             <tr>
                <td class="col1">Notes email</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.NotesEmail) %>
                    <%: Html.ValidationMessageFor(model => model.NotesEmail) %>
                </td>
            </tr>           
            <tr>
                <td class="col1">AD</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.ad) %>
                    <%: Html.ValidationMessageFor(model => model.ad) %>
                </td>
            </tr>
            <tr>
                <td class="col1">
                    <%: Html.LabelFor(model => model.Napomena) %>
                </td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.Napomena) %>
                    <%: Html.ValidationMessageFor(model => model.Napomena) %>
                </td>
            </tr>
 
        </table>
        <p>
            <input type="submit" value="Spremiti" />
            
                                        <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnUpdate";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Potvrda";
                                            settings.UseSubmitBehavior = true;
                                        }
                                    )
                                    .Render(); %>
        </p>
                <% Html.EndForm(); %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>