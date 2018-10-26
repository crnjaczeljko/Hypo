<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<HRM.Models.Zaposlenici>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Pregled zaposlenika
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
     <% using (Html.BeginForm())
    {%>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                <td style="border-style: none" class="selected">
                    &nbsp;&nbsp;Pretraga po zaposleniku, org.dijelu i voditelju: <%: Html.TextBox("SearchString")%>
                </td>
                <td> </td> 
                <td style="border-style: none">
                    <input type="submit" value="Pretraga" /> 
                </td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;Org dio : <%: Html.DropDownList("id_od",String.Empty) %> </td>
            </tr>
        </table>
  <% } %>
  </div>

<table id="hor-minimalist-b">
    <tr>
        <th></th>
        <th>Zaposlenik</th>
        <th>Org. dio</th>
        <th>Voditelj</th>
        <th>Radno mjesto</th> 
          <th>B/L</th>
        <th>Datum po&#269;etka</th>
        <th>Datum prestanka</th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
                <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.id_zaposlenici }) %>

                </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.ImePrezime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.OD2.Naziv) %>
        </td>
                <td>
            <%: Html.DisplayFor(modelItem => item.OD2.Zaposlenici.ImePrezime) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.RadnaMjesta.Naziv) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.PripadnostUBihGrupi.naziv) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_pocetka_rada) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.datum_prestanka) %>
        </td>
  
    </tr>
<% } %>

</table>
        <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <p>
    <%: Html.ActionLink("Novi zaposlenik", "Create") %>
</p>
</asp:Content>
