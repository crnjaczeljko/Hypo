<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Vozila.Models.Rezervacije>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ispravka pre&#273;ene kilometra&#382;e sa automobilom
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <table>
        <%: Html.HiddenFor(model => model.id_rez) %>
        <tr>
                 <td class="col1">Automobil</td>
        <td class="col2">
            <%: Html.LabelFor(model => model.Automobil.Naziv) %> 
              <%: Html.LabelFor(model => model.Automobil.RegBr) %>
        </td>  
        </tr>
        <tr>
           <td class="col1">Pocetna KM</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Poc_KM) %>
            </td>
        </tr>
        <tr>
                    <td class="col1"> Zavrsna KM</td>
        <td class="col2">
            <%: Html.EditorFor(model => model.Zav_KM) %>
            <%: Html.ValidationMessageFor(model => model.Zav_KM) %>
        </td>
        </tr>
    </table>
        <p>
            <input type="submit" value="Spremiti" />
        </p>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
      <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>
