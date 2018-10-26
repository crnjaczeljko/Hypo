<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Lokacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ažuriranja lokacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <% using (Html.BeginForm())
       { %>
    <%: Html.ValidationSummary(true) %>
    <%: Html.HiddenFor(model => model.id_lok) %>
    <table>
        <tr>

            <td class="col1">
                <%: Html.LabelFor(model => model.Naziv) %>
            </td>
            <td class="col2">
                <%: Html.EditorFor(model => model.Naziv) %>
                <%: Html.ValidationMessageFor(model => model.Naziv) %>
            </td>
        </tr>
        <tr>
            <td class="col1">Odgovorna osoba
            </td>
            <td class="col2">
                <%: Html.DropDownList("id_odgOsoba", String.Empty) %>
                <%: Html.ValidationMessageFor(model => model.id_odgOsoba) %>
            </td>
        </tr>
               <tr>
            <td class="col1">Nadređena osoba obavijest
            </td>
            <td class="col2">
                <%: Html.DropDownList("id_nadOsoba", String.Empty) %>
                <%: Html.ValidationMessageFor(model => model.id_nadOsoba) %>
            </td>
        </tr>
               <tr>
            <td class="col1">Rb
            </td>
            <td class="col2">
                <%: Html.EditorFor(model => model.rb) %>
                <%: Html.ValidationMessageFor(model => model.rb) %>
            </td>
        </tr>
    </table>
    <p>
        <input type="submit" value="Spremiti" />
    </p>
    <% } %>

    <div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>