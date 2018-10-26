<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Automobil>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kreiranje novog vozila
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
  </tr>
  <tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.id_lok, "Lokacije") %>
        </td>
        <td class="col2">
            <%: Html.DropDownList("id_lok", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_lok) %>
        </td>
          </tr>
  <tr>
        <td class="col1">Reg. oznaka
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.RegBr) %>
            <%: Html.ValidationMessageFor(model => model.RegBr) %>
        </td>
          </tr>
  <tr>
        <td class="col1">
            <%: Html.LabelFor(model => model.Tip) %>
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Tip) %>
            <%: Html.ValidationMessageFor(model => model.Tip) %>
        </td>
          </tr>
  <tr>
        <td class="col1">God. Proizvodnje
        </td>
        <td class="col2">
            <%: Html.EditorFor(model => model.God_Proiz) %>
            <%: Html.ValidationMessageFor(model => model.God_Proiz) %>
        </td>
          </tr>
  <tr>
    
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
