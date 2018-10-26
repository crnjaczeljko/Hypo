<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Lokacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Brisanje lokacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h3>Da li želite izbrisati?</h3>
<br/>
<table>
    <tr>
    <td class="col1">Naziv</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.Naziv) %>
    </td>
    </tr><tr>
    <td class="col1">Odgovorna osoba</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.Zaposlenici.ImePrezime) %> </td>
        </tr>
   
</table>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Brisanje" /> 
        
    </p>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>
