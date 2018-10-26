<%@ Page Title="Kreiranje nove rezervacije" Language="C#" MasterPageFile="~/Views/Shared/Site.master" 
Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>
<%@ Import Namespace="Vozila.Controllers" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
    //<![CDATA[
         function OnNameValidation(s, e) {
             if (e.value == null)
                 e.isValid = false;
             var name = String(e.value);
             if (name == "")
                 e.isValid = false;
             if (name.length > 50) {
                 e.isValid = false;
                 e.errorText = "Must be under 50 characters";
             }
         }
         function OnAgeValidation(s, e) {
             if (e.value == null || e.value == "")
                 return;
             var age = Number(e.value);
             if (isNaN(age) || age < 18 || age > 100)
                 e.isValid = false;
         }
    // ]]> 
    </script>
    <% Html.BeginForm("Create", "Home", FormMethod.Post, new { id = "validationForm", @class = "edit_form" }); %>
        <div class="line">
            <% 
                Html.DevExpress().Label(
                    settings => {
                        settings.ControlStyle.CssClass = "label";
                        settings.Text = "Name:";
                        settings.AssociatedControlName = "Name";
                    }
                )
                .Render();
            %>
            <% 
                Html.DevExpress().TextBox(
                    settings => {
                        settings.Name = "Name";
                        settings.ControlStyle.CssClass = "editor";
                        settings.Properties.ValidationSettings.Assign(EditorsDemosHelper.NameValidationSettings);
                        settings.Properties.ClientSideEvents.Validation = "OnNameValidation";
                    }
                )
                .Bind(Model.relacija)
                .Render();
            %>
        </div>
        <div class="line">
            <% 
                Html.DevExpress().Label(
                    settings => {
                        settings.ControlStyle.CssClass = "label";
                        settings.Text = "Age:";
                        settings.AssociatedControlName = "Age";
                    }
                )
                .Render();
            %>
            <% 
                Html.DevExpress().TextBox(
                    settings => {
                        settings.Name = "Age";
                        settings.ControlStyle.CssClass = "editor";
                        settings.Properties.ValidationSettings.Assign(EditorsDemosHelper.AgeValidationSettings);
                        settings.Properties.ClientSideEvents.Validation = "OnAgeValidation";
                    }
                )
                .Bind(Model.broj_putnika)
                .Render();
            %>
        </div>
        <div class="line">
            <% 
                Html.DevExpress().Label(
                    settings => {
                        settings.ControlStyle.CssClass = "label";
                        settings.Text = "Email:";
                        settings.AssociatedControlName = "Email";
                    }
                )
                .Render();
            %>
            <% 
                Html.DevExpress().TextBox(
                    settings => {
                        settings.Name = "Email";
                        settings.ControlStyle.CssClass = "editor";
                        settings.Properties.ValidationSettings.Assign(EditorsDemosHelper.EmailValidationSettings);
                    }
                )
                .Bind(Model.Kontakt_Tel)
                .Render();
            %>
        </div>
        <div class="line">
           Datum polaska
            <% 
                Html.DevExpress().DateEdit(
                    settings => {
                        settings.Name = "ArrivalDate";
                    settings.ControlStyle.CssClass = "editor";
                        settings.Properties.ValidationSettings.Assign(EditorsDemosHelper.ArrivalDateValidationSettings);                        
                    }
                )
                .Bind(Model.datum_polaska)
                .Render();
            %>
        </div>
        <div class="line">
            <% 
                Html.DevExpress().Label(
                    settings => {
                        settings.ControlStyle.CssClass = "label";
                    }
                )
                .Render();
            %>
            <% 
                Html.DevExpress().Button(
                    settings => {
                        settings.Name = "btnUpdate";
                        settings.ControlStyle.CssClass = "button";
                        settings.Text = "Update";
                        settings.UseSubmitBehavior = true;
                    }
                )
                .Render();
            %>
            <% 
                Html.DevExpress().Button(
                    settings => {
                        settings.Name = "btnClear";
                        settings.ControlStyle.CssClass = "button";
                        settings.Text = "Clear";
                        settings.ClientSideEvents.Click = "function(s, e){ ASPxClientEdit.ClearEditorsInContainer(); }";
                    }
                )
                .Render();
            %>
        </div>
    <% Html.EndForm(); %>

</asp:Content>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Links">
       <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
        <title></title>
    <style type="text/css">
        .style1
        {
            width: 248px;
        }
    </style>
</asp:Content>

