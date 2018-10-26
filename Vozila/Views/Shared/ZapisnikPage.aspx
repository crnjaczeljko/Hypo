<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>
<%@ Import Namespace="Vozila.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Zapisnik sa putovanja
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    //<![CDATA[
        function OnNameValidation(s, e) {

            if (e.value == null) {
                e.isValid = false;
                return;
            }
            var name = String(e.value);
            if (name == "") {
                e.isValid = false;
                return;
            }
            if (name == " ")
                e.isValid = false;

            if (name.length > 50) {
                e.isValid = false;
                e.errorText = "Must be under 50 characters";
                return;
            }

            if (s.name == "Poc_KM") {
                e.isValid = !isNaN(name);
                e.errorText = "Unesite broj";
            }
            if (s.name == "Zav_KM") {
                e.isValid = !isNaN(name);
                e.errorText = "Unesite broj";
            }
        }

        function IsNumeric(input) {
            return (input - 0) == input && input.length > 0;
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

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"> </script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"> </script>

    
    <table>
        <tr>
            <td style="vertical-align: top; width: 382px;"><h2>Opis</h2>
                <hr/>
                <table>

                    <%: Html.HiddenFor(model => model.id_rez) %>
                    <tr>
                        <td class="col1">Zaposlenik</td>
                        <td class="col2">
                            <%: Model.Zaposlenici.ImePrezime %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Datum zahtjeva</td>
                        <td class="col2">
                            <%: Model.datum_kreiranja %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Pravac putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.relacija) %>

                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Polazno odredište</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Krajnje odredište</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Mjesta.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"> Broj putnika</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.broj_putnika) %>
                        </td>         
                    </tr>
                    <tr> 
                        <td class="col1"> Tip putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.TipRezervacije.Naziv) %>
                        </td>    
                    </tr>
                    <tr>
                        <td class="col1" style="width: 206px"> Dodijeljeni automobil
                        </td>
                        <td class="col2" style="width: 210px">
                            <%: Html.DisplayFor(model => model.Automobil.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1"> Opis</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Opis) %>
                        </td>     
                    </tr>
                </table>
            </td>
            <td style="width: 51px"></td>
            <td style="vertical-align: top; width: 459px;"><h2>Zapisnik</h2>
                <hr/>
                <% Html.EnableClientValidation(); %> 
                <% Html.BeginForm("ZapisnikPage", "Rezervacije", FormMethod.Post, new {id = "validationForm", @class = "edit_form"}); %>
                <table>
                    <tr>
                        <td class="col1">Polazak</td>
                        <td class="col2">
                            <table >
                                <tr>
                                    <td class="style1">
                                        <%: @Html.DevExpress().DateEdit(
                                                settings =>
                                                    {
                                                        settings.Name = "datpol1";
                                                        settings.Width = 90;
                                                        settings.Properties.ValidationSettings.Assign(
                                                            EditorsDemosHelper.ArrivalDateValidationSettings);
                                                        if (Model.datum_polaska != null)
                                                            settings.Date =
                                                                Model.datum_polaska.Value;
                                                    }
                                                ).GetHtml() %>
                                    </td>
                                    <td>
                                        <%: @Html.DevExpress().TimeEdit(
                                                settings =>
                                                    {
                                                        settings.Name = "datpol2";
                                                        settings.Width = 60;
                                                        if (Model.datum_polaska != null)
                                                            settings.DateTime = Model.datum_polaska.Value;
                                                    }
                                                ).GetHtml() %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Povratak</td>
                        <td class="col2">
                            <table >
                                <tr>
                                    <td class="style1">
                                        <%: @Html.DevExpress().DateEdit(
                                                settings =>
                                                    {
                                                        settings.Name = "datdol1";
                                                        settings.Width = 90;
                                                        settings.Properties.ValidationSettings.Assign(
                                                            EditorsDemosHelper.ArrivalDateValidationSettings);
                                                        if (Model.datum_dolaska != null)
                                                            settings.Date =
                                                                Model.datum_dolaska.Value;
                                                    }
                                                ).GetHtml() %>
                                    </td>
                                    <td>
                                        <%: @Html.DevExpress().TimeEdit(
                                                settings =>
                                                    {
                                                        settings.Name = "datdol2";
                                                        settings.Width = 60;
                                                        if (Model.datum_dolaska != null)
                                                            settings.DateTime = Model.datum_dolaska.Value;
                                                    }
                                                ).GetHtml() %>
                                    </td>
                                </tr>
                            </table>
                        </td>            
                    </tr>
                    <tr>
                        <td class="col1">Početna KM</td>
                        <td class="col2">
                            <% Html.DevExpress().TextBox(
                                   settings =>
                                       {
                                           settings.Name = "Poc_KM";
                                           settings.Width = 90;
                                           settings.ControlStyle.CssClass = "editor";

                                           settings.Properties.ValidationSettings.Assign(
                                               EditorsDemosHelper.NameValidationSettings);
                                           settings.Properties.ClientSideEvents.Validation =
                                               "OnNameValidation";
                                       }
                                   )
                                   .Bind(Model.Poc_KM)
                                   .Render(); %>
                        </td>
                    </tr><tr>
                             <td class="col1">Krajnja KM</td>
                             <td class="col2">
                                 <% Html.DevExpress().TextBox(
                                        settings =>
                                            {
                                                settings.Name = "Zav_KM";
                                                settings.Width = 90;
                                                settings.ControlStyle.CssClass = "editor";
                                                settings.Properties.ValidationSettings.Assign(
                                                    EditorsDemosHelper.NameValidationSettings);
                                                settings.Properties.ClientSideEvents.Validation =
                                                    "OnNameValidation";
                                            }
                                        )
                                        .Bind(Model.Zav_KM)
                                        .Render(); %>
                             </td>
                         </tr>
                    <tr>
                        <td class="col1" style="width: 206px">Zaključak</td>
                        <td class="col2" style="width: 210px">
                            <%: Html.TextAreaFor(model => model.Zapisnik) %>
                            <%: Html.ValidationMessageFor(model => model.Zapisnik) %>
                        </td>
                    </tr>
                    <tr>
                            
                        <td style="vertical-align: top">
                            <hr/>Troškovi putovanja
                            <br/>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnTrosak";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Novi Trosak";
                                            settings.UseSubmitBehavior = true;
                                        }
                                    )
                                    .Render();
                            %>
                        </td>
                        <td><hr/><% Html.RenderPartial("TroskoviPage", ViewData["TroskoviPage"]); %></td>
                    </tr>
                </table>
                <hr/>    
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
            </td>
        </tr>
    </table>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "ZapisnikList") %>
</asp:Content>