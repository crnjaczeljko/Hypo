<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<HRM.Models.vPrijaveAplikacija>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pregled prijava aplikacija
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <% using (Html.BeginForm())
           {%>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                <td style="border-style: none" class="selected">&nbsp;&nbsp;Pretraga po zaposleniku, org.dijelu i voditelju: <%: Html.TextBox("SearchString")%>
                </td>
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
            <th>Datum</th>
            <th>Status</th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <a href='<%:Url.Action("Details", new {id = item.id_prijave})%>' style="border-style: none">
                    <img src="../../Images/edit.png" alt="Pregled" style="border-style: none" />
                </a>
            </td>
            <td><%: Html.DisplayFor(modelItem => item.ImePrezime) %></td>
            <td><%: Html.DisplayFor(modelItem => item.OD) %></td>
            <td><%: Html.DisplayFor(modelItem => item.Voditelj) %></td>
            <td><%: Html.DisplayFor(modelItem => item.RadnoMjesto) %></td>
            <td><%: Html.DisplayFor(modelItem => item.sysdate) %></td>
            <td><% if (item.potvrda_all != null && (bool)item.potvrda_all)
                   {%>
                <img src="../../images/green.png" alt="Edit" style="border-style: none" />
                <%  }
                   else
                   {%>
                <img src="../../images/red.png" alt="Edit" style="border-style: none" />
                <% } %> </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
</asp:Content>