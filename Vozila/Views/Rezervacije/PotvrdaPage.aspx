<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
         Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>
<%@ Import Namespace="Vozila.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Potvrda rezervacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    //<![CDATA[
        function OnNameValidation(s, e) {
            var frm = document.forms["validationForm"];
            var br = frm.broj_putnika.value;

//            if (e.value == null)
//                e.isValid = false;
            var name = String(e.value);
//            if (name == "")
//                e.isValid = false;

            if (name == "0" && s.name == "id_grad")
                e.isValid = false;
            if (br > 6 && s.name == "broj_putnika") {
                e.isValid = false;
                e.errorText = "Pogresan broj putnika";
            }
            if ((e.value == null || e.value == "") && s.name == "id_auto")
                e.isValid = false;

            if ((e.value == null || e.value == "") && s.name == "id_lok_zaduzenje")
                e.isValid = false;

            if ((e.value == null || e.value == "") && s.name == "id_lok_razduzenje")
                e.isValid = false;

            if (br == 1) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
            }
            if (br == 2) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik2")
                    e.isValid = false;
            }
            if (br == 3) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik2")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik3")
                    e.isValid = false;
            }
            if (br == 4) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik2")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik3")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik4")
                    e.isValid = false;
            }
            if (br == 5) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik2")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik3")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik4")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik5")
                    e.isValid = false;
            }
            if (br == 6) {
                if ((e.value == null || e.value == "") && s.name == "id_Putnik1")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik2")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik3")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik4")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik5")
                    e.isValid = false;
                if ((e.value == null || e.value == "") && s.name == "id_Putnik6")
                    e.isValid = false;
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

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"> </script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"> </script>

    <%--    <% using (Html.BeginForm())
       {%>
        <%: Html.ValidationSummary(true) %>--%>
    
    <table>
        <tr>
            <td style="vertical-align: top; width: 382px;">
                <h2>Zahtjev</h2>
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
                        <td class="col1">Kontakt telefon</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Kontakt_Tel) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Relacija - Mjesto polaska , odrediste/a i mjesto dolaska</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.relacija) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Mjesto polaska</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
                            <%: Html.ValidationMessageFor(model => model.id_polLok) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Mjesto putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Mjesta.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Polazak</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.datum_polaska) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Povratak</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.datum_dolaska) %>
                        </td>            
                    </tr>
                    <tr> 
                        <td class="col1"> Tip putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.TipRezervacije.Naziv) %>
                        </td>    
                    </tr>
                    <tr>
                        <td class="col1">Svrha putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Opis) %>
                        </td>     
                    </tr>
                </table>            
            </td>
            <td style="width: 51px"></td>
            <td style="vertical-align: top; width: 459px;">
                <% Html.EnableClientValidation(); %> 
                <% Html.BeginForm("PotvrdaPage", "Rezervacije", FormMethod.Post, new {id = "validationForm", @class = "edit_form"}); %>
                 
                <h2>Putnici</h2>
                <hr/>
                <table style="width: 100%">
                                        <tr>
                        <td class="col1">Polazak novi</td>
                        <td class="col2">
                            <table>
                                <tr>
                                    <td class="style2">
                                        <%
                                            Html.DevExpress().DateEdit(
                                                settings =>
                                                {
                                                    settings.Name = "Pol1";
                                                    settings.Width = 90;
                                                    settings.ControlStyle.CssClass = "editor";
                                                    settings.Properties.ValidationSettings.Assign(
                                                        EditorsDemosHelper.ArrivalDateValidationSettings);
                                                }
                                                )
                                                .Bind(Model.datum_polaska)
                                                .Render();%>
                                    </td>
                                    <td>
                                        <% Html.DevExpress().TimeEdit(
                                               settings =>
                                               {
                                                   settings.Name = "Pol2";
                                                   settings.Width = 60;
                                                   settings.ControlStyle.CssClass = "editor";
                                                   settings.Properties.ValidationSettings.Assign(
                                                       EditorsDemosHelper.NameValidationSettings);
                                               }
                                               )
                                               .Bind(Model.datum_polaska)
                                               .Render();
                                        %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Dolazak novi</td>
                        <td class="style2">
                            <table>
                                <tr>
                                    <td class="style2">
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
                        <td class="col1"> Broj putnika</td>
                        <td class="col2">
                            <% Html.DevExpress().TextBox(
                                   settings =>
                                       {
                                           settings.Name = "broj_putnika";
                                           settings.Width = 90;
                                           settings.ControlStyle.CssClass = "editor";
                                           settings.Properties.ValidationSettings.Assign(
                                               EditorsDemosHelper.NameValidationSettings);
                                           settings.Properties.ClientSideEvents.Validation =
                                               "OnNameValidation";
                                       }
                                   )
                                   .Bind(Model.broj_putnika)
                                   .Render(); %>
                        </td>         
                    </tr>
                    <tr>
                        <td class="col1">Putnik 1</td>
                        <td class="col2">
                            <%  Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik1";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik1Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik1)
                                    .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 2</td>
                        <td class="col2">
                             <% Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik2";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik2Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik2)
                                    .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 3</td>
                        <td class="col2">
                                  <% Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik3";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik3Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik3)
                                    .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 4</td>
                        <td class="col2">
                             <% Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik4";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik4Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik4)
                                    .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 5</td>
                        <td class="col2">
                                      <% Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik5";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik5Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik5)
                                    .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 6</td>
                        <td class="col2">
                                     <% Html.DevExpress().ComboBox(
                                    settings =>
                                        {
                                            settings.Name = "id_Putnik6";
                                            settings.Width = 180;
                                            settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                            settings.CallbackRouteValues = new {Controller = "ComboBox", Action = "CbPutnik6Partial"};
                                            settings.Properties.EnableCallbackMode = true;
                                            settings.Properties.CallbackPageSize = 15;
                                            settings.Properties.IncrementalFilteringMode =
                                                IncrementalFilteringMode.StartsWith;
                                            settings.Properties.TextField = "ImePrezime";
                                            settings.Properties.ValueField = "id_zaposlenici";
                                            settings.Properties.ValueType = Type.GetType("System.Int32");
                                            settings.Properties.ValidationSettings.Assign(
                                                EditorsDemosHelper.NameValidationSettings);
                                            settings.Properties.ClientSideEvents.Validation =
                                                "OnNameValidation";
                                        }
                                    )
                                    .BindList(ViewData["id_zaposlenik"])
                                    .Bind(Model.id_Putnik6)
                                    .Render(); %>
                        </td>
                    </tr>
                </table>
   
                <h2>Odobrenje</h2>
                <hr/>
                <table>
                    <tr>               
                        <td class="col1" style="width: 206px"> Dodijeljeni automobil</td>
                        <td class="col2" style="width: 210px">
                            <% Html.DevExpress().ComboBox(
                                   settings =>
                                       {
                                           settings.Name = "id_auto";
                                           settings.Width = 180;
                                           settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                           // settings.SelectedIndex = 0;
                                           settings.Properties.IncrementalFilteringMode =
                                               IncrementalFilteringMode.StartsWith;
                                           settings.Properties.TextField = "Lokacija";
                                           settings.Properties.ValueField = "id_auto";
                                           settings.Properties.ValidationSettings.Assign(
                                               EditorsDemosHelper.NameValidationSettings);
                                           settings.Properties.ClientSideEvents.Validation =
                                               "OnNameValidation";
                                       }
                                   )
                                   .BindList(ViewData["id_auto"])
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1" style="width: 206px"> Zaduzenje automobila</td>
                        <td class="col2" style="width: 210px">
                            <% Html.DevExpress().ComboBox(
                                   settings =>
                                       {
                                           settings.Name = "id_lok_zaduzenje";
                                           settings.Width = 180;
                                           settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                           // settings.SelectedIndex = 0;
                                           settings.Properties.IncrementalFilteringMode =
                                               IncrementalFilteringMode.StartsWith;
                                           settings.Properties.TextField = "Naziv";
                                           settings.Properties.ValueField = "id_lok";
                                           settings.Properties.ValidationSettings.Assign(
                                               EditorsDemosHelper.NameValidationSettings);
                                           settings.Properties.ClientSideEvents.Validation =
                                               "OnNameValidation";
                                       }
                                   )
                                   .BindList(ViewData["id_lokacija"])
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1" style="width: 206px">Razduzenje automobila</td>
                        <td class="col2" style="width: 210px">
                            <% Html.DevExpress().ComboBox(
                                   settings =>
                                       {
                                           settings.Name = "id_lok_razduzenje";
                                           settings.Width = 180;
                                           settings.Properties.DropDownStyle = DropDownStyle.DropDownList;
                                           //settings.SelectedIndex = 0;
                                           settings.Properties.IncrementalFilteringMode =
                                               IncrementalFilteringMode.StartsWith;
                                           settings.Properties.TextField = "Naziv";
                                           settings.Properties.ValueField = "id_lok";
                                           settings.Properties.ValidationSettings.Assign(
                                               EditorsDemosHelper.NameValidationSettings);
                                           settings.Properties.ClientSideEvents.Validation =
                                               "OnNameValidation";
                                       }
                                   )
                                   .BindList(ViewData["id_lokacija"])
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1" style="width: 206px">Komentar</td>
                        <td class="col2" style="width: 210px">
                            <%: Html.TextAreaFor(model => model.Komentar) %>
                            <%: Html.ValidationMessageFor(model => model.Komentar) %>
                        </td> 
                    </tr>
                </table>
                <hr/>  
                <table>
                    <tr>
                        <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnUpdate";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Potvrda";
                                            settings.UseSubmitBehavior = true;
                                        }
                                    )
                                    .Render(); %>
                        </td>
                        <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnCancel";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Odbiti";
                                            settings.UseSubmitBehavior = true;
                                            settings.CausesValidation = false;
                                        }
                                    )
                                    .Render(); %>
                        </td>
                         <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnVratiti";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Vratiti na doradu";
                                            settings.UseSubmitBehavior = true;
                                            settings.CausesValidation = false;
                                        }
                                    )
                                    .Render(); %>
                        </td>

                                                 <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnRefresh";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Osvje&#382;iti";
                                            settings.UseSubmitBehavior = true;
                                            settings.CausesValidation = false;
                                        }
                                    )
                                    .Render(); %>
                        </td>
                    </tr>
                </table>  
                <% Html.EndForm(); %>
            </td>
        </tr>
    </table>
  
    <%-- 
    <% } %>--%>

</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="Links">
    <%: Html.ActionLink("Natrag", "PotvrdaList") %>
</asp:Content>