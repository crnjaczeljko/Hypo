<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<HRM.Models.Prijave>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Detalji prijave
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <td class="col1">Zaposlenici</td>
            <td class="col2Bold">
                <%: Html.DisplayFor(model => model.Zaposlenici.ImePrezime) %>
            </td>
        </tr>
        <tr>
            <td class="col1">OD</td>
            <td class="col2">
                <%: Html.DisplayFor(model => model.OD.Naziv) %>
            </td>
        </tr>
        <tr>
            <td class="col1">RadnaMjesta</td>
            <td class="col2">
                <%: Html.DisplayFor(model => model.RadnaMjesta.Naziv) %>
            </td>
        </tr>
        <tr>
            <td class="col1">Datum prijave</td>
            <td class="col2">
                <%: Html.DisplayFor(model => model.sysdate) %>
            </td>
        </tr>
        <tr>
            <td class="col1">Status</td>
            <td class="col2">
                             <% if (Model.potvrda_all != null && (bool)Model.potvrda_all)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
               
            </td>
        </tr>
        <tr>
            <td class="col1">Nadre&#273;ena osoba</td>
            <td class="col2">
                <%: Html.DisplayFor(model => model.Zaposlenici1.ImePrezime) %>
            </td>
        </tr>
                <tr>
            <td class="col1">Potvrda od n.o.</td>
            <td class="col2">
                                             <% if (Model.potvrda_vod != null && (bool)Model.potvrda_vod)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
            </td>
        </tr>
    </table>
    <p>

        <% Html.RenderPartial("PrijaveAppList", ViewData["Prijave"]);%>
        
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>