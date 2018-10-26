<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Lokacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kreiranje nove lokacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <table>
    <tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.Naziv) %>
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Naziv) %>
            <%: Html.ValidationMessageFor(model => model.Naziv) %>
        </td>
</tr><tr>
        <td class="col1">
            Odgovorna osoba
        </td>
        <td class="col2">
            <%: Html.DropDownList("id_odgOsoba", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_odgOsoba) %>
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
