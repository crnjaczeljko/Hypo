<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Vozila.Models.Lokacije>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pregled lokacija vozila
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table id="hor-minimalist-b">
        <tr>
            <th style="width: 30px"></th>
            <th style="width: 30px"></th>
            <th>Naziv</th>
            <th>Odgovorna osoba</th>
            <th>Nadređena osoba</th>
            <th>rb</th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <a href='<%: Url.Action("Edit", new {id = item.id_lok}) %>' style="border-style: none">
                    <img src="/Content/images/Edit.png" alt="Edit" style="border-style: none" />
                </a>
            </td>
            <td style="width: 30">
                <a href='<%: Url.Action("Delete", new {id = item.id_lok}) %>'>
                    <img src="/Content/images/delete.png" alt="Delete" style="border-style: none" />
                </a>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Zaposlenici.ImePrezime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Zaposlenici1.ImePrezime) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.rb) %>
            </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Nova lokacija", "Create") %>
</asp:Content>