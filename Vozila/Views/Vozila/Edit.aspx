<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Automobil>" %>

<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPager" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Azuriranje vozila
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">

        <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
        <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

        <% using (Html.BeginForm())
           { %>
        <%: Html.ValidationSummary(true) %>
        <table>

            <%: Html.HiddenFor(model => model.id_auto) %>
            <tr>
                <td class="col1">Naziv</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.Naziv) %>
                    <%: Html.ValidationMessageFor(model => model.Naziv) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Lokacija</td>
                <td class="col2">
                    <%: Html.DropDownList("id_lok", String.Empty) %>
                    <%: Html.ValidationMessageFor(model => model.id_lok) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Reg. br</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.RegBr) %>
                    <%: Html.ValidationMessageFor(model => model.RegBr) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Tip</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.Tip) %>
                    <%: Html.ValidationMessageFor(model => model.Tip) %>
                </td>
            </tr>
            <tr>
                <td class="col1">God. proizv-</td>
                <td class="col2">
                    <%: Html.EditorFor(model => model.God_Proiz) %>
                    <%: Html.ValidationMessageFor(model => model.God_Proiz) %>
                </td>
            </tr>
            <tr>
                <td class="col1">Servis</td>
                <td class="col2">
                    <%: Html.CheckBoxFor(model => model.Servis) %>
                    <%: Html.ValidationMessageFor(model => model.Servis) %>
                </td>
            </tr>
        </table>
        <p>
            <input type="submit" value="Spremiti" />
        </p>

        <% } %>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>