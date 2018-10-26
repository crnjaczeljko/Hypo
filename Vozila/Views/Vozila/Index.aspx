<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<PagedList.IPagedList<Vozila.Models.Automobil>>" %>

<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPager" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pregled vozila
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="blue">
        <% using (Html.BeginForm())
           {%>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                     <td style="border-style: none">Pretraga po nazivu, reg. br: <%:Html.TextBox("SearchString")%> 
                   </td>
<%--                <td style="border-style: none">&nbsp;&nbsp;Datum Od: <%:Html.TextBox("datumOd", string.Empty, new { style = "width:150px", @class = "datepicker" })%>
                 &nbsp;&nbsp;Datum do: <%:Html.TextBox("datumDo", string.Empty, new { style = "width:150px", @class = "datepicker" })  %>
                </td>--%>
                <td style="border-style: none">
                    <input type="submit" value="Pretraga" />
                </td>               
            </tr>
        </table>
        <% }%>
    </div>
<hr/>

    <table id="hor-minimalist-b">
        <tr>
            <th style="width: 30px"></th>
            <th style="width: 30px"></th>
            <th>Naziv</th>
            <th>Lokacije</th>
            <th>RegBr</th>
            <th>Tip</th>
            <th>God.Proiz</th>
            <th>Status</th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <a href='<%: Url.Action("Edit", new {id = item.id_auto}) %>' style="border-style: none">
                    <img src="/Content/images/Edit.png" alt="Edit" style="border-style: none" />
                </a>
            </td>
            <td>
                <a href='<%: Url.Action("Delete", new {id = item.id_auto}) %>'>
                    <img src="/Content/images/delete.png" alt="Delete" style="border-style: none" />
                </a>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Lokacije.Naziv) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.RegBr) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.Tip) %>
            </td>
            <td>
                <%: Html.DisplayFor(modelItem => item.God_Proiz) %>
            </td>
            <td>
                <% var st = item.Servis;
                   if (st == null || st == false)
                   { %>
                <img src="<%: Url.Content("~/Content/Images/1.png") %>" alt="OK" style="border-style: none" />
                <% }
                       if (st == true)
                       { %>
                <img src="<%: Url.Content("~/Content/Images/service.png") %>" alt="servis" style="border-style: none" />
                <% }%>
            </td>
        </tr>
        <% } %>
    </table>
    <% Html.RenderPartial("PageListView", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">

    <%: Html.ActionLink("Novo vozilo", "Create") %>
</asp:Content>