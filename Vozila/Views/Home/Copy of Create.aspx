<%@ Page Title="Kreiranje nove rezervacije" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>
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



<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <% Html.EnableClientValidation(); %> 
    <% Html.BeginForm("Create", "Home", FormMethod.Post, new { id = "validationForm", @class = "edit_form" }); %>

    <table style="border-style: none; border-width: thin">
        <tr>
        <td style="border-style: none; border-width: thin; width: 353px">
            <table style="width: 100%">
                 <tr>
        <td class="editor-label">
           Zaposlenik
        </td>
        <td class="style1">
            <%: ViewBag.User %>
        </td>
        </tr><tr>
                             
        <td class="editor-label">
           Tip putovanja
        </td>
        <td class="style1">
            <%: Html.DropDownList("id_tiprez", String.Empty)%>
            <%: Html.ValidationMessageFor(model => model.id_tiprez) %>
        </td> 
        </tr><tr>
        <td class="editor-label">
            Relacija
        </td>
        <td class="style1">
            <%: Html.EditorFor(model => model.relacija) %>
            <%: Html.ValidationMessageFor(model => model.relacija) %>
        </td>
             </tr><tr>
        <td class="editor-label">
            Mjesto polaska</td>
        <td class="style1">
                        <%: Html.DropDownList("id_polLok", String.Empty)%>
            <%: Html.ValidationMessageFor(model => model.id_polLok) %>
            </td>
             </tr>
             <tr>
        <td class="editor-label">
            Polazak
        </td>
        <td class="style1">
            <table >
                <tr>
                    <td>
                                        <%:@Html.DevExpress().DateEdit(
                settings =>
                    {
                        settings.Name = "datpol1";
                        settings.Width = 90;
                        if (Model.datum_kreiranja != null) settings.Date = Model.datum_polaska.Value;
                    }
                           ).GetHtml()
 %>
                    </td>
                    <td>
                                     <%:@Html.DevExpress().TimeEdit(
                settings =>
                    {
                        settings.Name = "datpol2";
                        settings.Width = 60;
                        if (Model.datum_kreiranja != null) settings.DateTime = Model.datum_polaska.Value;
                    }
                           ).GetHtml()
 %>
                    </td>
                </tr>
            </table>
            <%: Html.ValidationMessageFor(model => model.datum_polaska) %>
            
        </td>
             </tr>
             <tr>
        <td class="editor-label">
            Dolazak</td>
        <td class="style1">
            <table >
                <tr>
                    <td>
                <%:@Html.DevExpress().DateEdit(
                settings =>
                    {
                        settings.Name = "datdol1";
                        settings.Width = 90;
                        if (Model.datum_dolaska != null) settings.Date = Model.datum_dolaska.Value;
                    }
                           ).GetHtml()
 %>
                    </td>
                    <td>
            <%:@Html.DevExpress().TimeEdit(
                settings =>
                    {
                        settings.Name = "datdol2";
                        settings.Width = 60;
                        if (Model.datum_dolaska != null) settings.DateTime = Model.datum_dolaska.Value;
                    }
                           ).GetHtml()
 %>
                    </td>
                </tr>
            </table>
            <%: Html.ValidationMessageFor(model => model.datum_dolaska) %>
        </td>
             </tr><tr>
        <td class="key">
            Broj putnika
        </td>
        <td class="style1">
                        <% 
                Html.DevExpress().TextBox(
                      settings => {
                        settings.Name = "BrojPutnika";
                        settings.ControlStyle.CssClass = "editor";
                        settings.Properties.ValidationSettings.Assign(EditorsDemosHelper.NameValidationSettings);
                        settings.Properties.ClientSideEvents.Validation = "OnNameValidation";
                    }
                )
                .Bind(Model.broj_putnika)
                .Render();
            %>
            <%: Html.EditorFor(model => model.broj_putnika) %>
        </td>
             </tr><tr>

        <td class="editor-label">
            Kontakt Telefon
        </td>
        <td class="style1">
            <%: Html.EditorFor(model => model.Kontakt_Tel) %>
            <%: Html.ValidationMessageFor(model => model.Kontakt_Tel) %>
        </td>
             </tr><tr>
        <td class="editor-label">
            Opis
        </td>
        <td class="style1">
            <%: Html.EditorFor(model => model.Opis) %>
            <%: Html.ValidationMessageFor(model => model.Opis) %>
        </td>

             </tr><tr>
        <td class="editor-label">
            Mjesto Putovanja
        </td>
        <td class="style1">
            <%: Html.DropDownList("id_grad", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_grad) %>
        </td>
             </tr>
            </table>
        </td>
        <td style="border-style: none; border-width: thin; width: 37px"></td>
        <td style="border-style: none; border-width: thin; width: 407px; vertical-align: top;">
            <table>
                 <tr>
        <td class="editor-label" style="width: 110px">
            Putnik 1
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik1", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik1) %>
        </td>
             </tr><tr>
        <td class="editor-label" style="width: 110px">
            Putnik 2
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik2", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik2) %>
        </td>
             </tr><tr>
        <td class="editor-label" style="width: 110px">
            Putnik 3
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik3", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik3) %>
        </td>
             </tr><tr>
        <td class="editor-label" style="width: 110px">
           Putnik 4
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik4", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik4) %>
        </td>
             </tr><tr>
        <td class="editor-label" style="width: 110px">
             Putnik 5
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik5", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik5) %>
        </td>
             </tr><tr>
        <td class="editor-label" style="width: 110px">
           Putnik 6
        </td>
        <td class="editor-field" style="width: 204px">
            <%: Html.DropDownList("id_Putnik6", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.id_Putnik6) %>
        </td>
             </tr>
            </table>
        </td>
        </tr>
    </table>

        <p>
            <input type="submit" value="Poslati" />
        </p>
        
                   <% 
                       Html.DevExpress().Button(
                           settings =>
                           {
                               settings.Name = "btnUpdate";
                               settings.ControlStyle.CssClass = "button";
                               settings.Text = "Spremiti";
                               settings.UseSubmitBehavior = true;
                           }
                       )
                       .Render();
            %>

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

