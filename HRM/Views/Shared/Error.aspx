<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>


<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
 <% if (ViewBag.Message == null)
           {
               ViewBag.Message = "Greška";
           }%>
           <%:ViewBag.Message%>
               
    </h2>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>