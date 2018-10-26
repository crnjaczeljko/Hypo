<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Automobil>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Da li želite brisati?</h3>
<br/>
<table>
<tr>
    <td class="col1">Naziv</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.Naziv) %>
    </td>
    </tr>
    <tr>
    <td class="col1">Lokacije</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
    </td>
        </tr>
    <tr>
    <td class="col1">RegBr</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.RegBr) %>
    </td>
        </tr>
    <tr>
    <td class="col1">Tip</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.Tip) %>
    </td>
        </tr>
    <tr>
    <td class="col1">God_Proiz</td>
    <td class="col2">
        <%: Html.DisplayFor(model => model.God_Proiz) %>
    </td>
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
