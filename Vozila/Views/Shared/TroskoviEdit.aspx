<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Troskovi>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <% ViewBag.Message = "Ažuriranje Troškova"; %> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"> </script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"> </script>

    <% using (Html.BeginForm())
       {%>
        <%: Html.ValidationSummary(true) %>
        <table>
            <% Html.HiddenFor(model => model.id_tr);
               Html.HiddenFor(model => model.id_rez); %>
            <tr>
                <td class="editor-label">Opis</td>
                <td class="editor-field">
                    <%: Html.EditorFor(model => model.Naziv) %>
                    <%: Html.ValidationMessageFor(model => model.Naziv) %>
                </td>
            </tr>
            <tr>
                <td class="editor-label">Iznos</td>
                <td class="editor-field">
                    <%: Html.EditorFor(model => model.Iznos) %>
                    <%: Html.ValidationMessageFor(model => model.Iznos) %>
                </td>
            </tr>
            <tr>
                <td class="editor-label">Valuta</td>
                <td class="editor-field">
                    <%: Html.DropDownList("id_val", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.id_val) %>
                </td>
            </tr>

        </table>
        <p>
            <input type="submit" value="spremiti" />
        </p>
    <% } %>

    <
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "ZapisnikPage", new {id = ViewBag.id_rez}) %>
</asp:Content>