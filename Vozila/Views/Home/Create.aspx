<%@ Page Title="Kreiranje nove rezervacije" Language="C#" MasterPageFile="~/Views/Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>

<%@ Import Namespace="Vozila.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        function OnNameValidation(s, e) {

            var frm = document.forms["validationForm"];
            var br = frm.BrojPutnika.value;

            if (e.value == null)
                e.isValid = false;
            var name = String(e.value);
            if (s.name == "BrojPutnika") {
                e.isValid = !isNaN(name);
                e.errorText = "Unesite broj";
                return;
            }

            if (s.name == "pon") {
                e.isValid = !isNaN(name);
                e.errorText = "Unesite broj";
                return;
            }


            if (name == "")
                e.isValid = false;
            if (name == "0" && s.name == "id_grad")
                e.isValid = false;
            if (br> 6 && s.name == "BrojPutnika") {
                e.isValid = false;
                e.errorText = "Pogresan broj putnika";
            }

            if (br == 1) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
            }
            if (br == 2) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik2")
                    e.isValid = false;
            }
            if (br == 3) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik2")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik3")
                    e.isValid = false;
            }
            if (br == 4) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik2")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik3")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik4")
                    e.isValid = false;
            }
            if (br == 5) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik2")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik3")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik4")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik5")
                    e.isValid = false;
            }
            if (br == 6) {
                if (e.value == "0" && s.name == "id_Putnik1")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik2")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik3")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik4")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik5")
                    e.isValid = false;
                if (e.value == "0" && s.name == "id_Putnik6")
                    e.isValid = false;
            }
        }

        function OnAgeValidation(s, e) {
            if (e.value == null || e.value == "")
                return;
            var age = Number(e.value);
            if (isNaN(age) || age < 18 || age> 100)
                e.isValid = false;
        }

        // ]]>
    </script>
    <% Html.EnableClientValidation(); %>
    <% Html.BeginForm("Create", "Home", FormMethod.Post, new { id = "validationForm", @class = "edit_form" }); %>

    <table style="border-style: none; border-width: thin">
        <tr>
            <td style="border-style: none; border-width: thin;" class="style4">
                <table style="width: 100%">
                    <tr>
                        <td class="col1">Zaposlenik</td>
                        <td class="col2">
                            <%: ViewBag.User %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Tip putovanja</td>
                        <td class="col2">
                            <%
                                Html.DevExpress().ComboBox(
                                    settings =>
                                    {
                                        settings.Name = "id_tiprez";
                                        settings.Width = 180;
                                        settings.SelectedIndex = 0;
                                        settings.Properties.IncrementalFilteringMode =
                                            IncrementalFilteringMode.StartsWith;
                                        settings.Properties.DropDownStyle = DropDownStyle.DropDown;
                                        settings.Properties.TextField = "Naziv";
                                        settings.Properties.ValueField = "id_tiprez";
                                        settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                                        settings.Properties.ValidationSettings.Assign(
                                            EditorsDemosHelper.NameValidationSettings);
                                        settings.Properties.ClientSideEvents.Validation = "OnNameValidation";
                                    }
                                    )
                                    .BindList(ViewData["TipRezervacije"])
                                    .Render();
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Relacija - Mjesto polaska , odrediste/a i mjesto dolaska</td>
                        <td class="col2">

                            <% Html.DevExpress().Memo(
                                   settings =>
                                   {
                                       settings.Name = "relacija";
                                       settings.Width = 180;
                                       settings.Height = 80;
                                       settings.ControlStyle.CssClass = "editor";
                                       settings.Properties.ValidationSettings.Assign(
                                           EditorsDemosHelper.NameValidationSettings);
                                       settings.Properties.ClientSideEvents.Validation =
                                           "OnNameValidation";
                                   }
                                   )
                                   .Bind(Model.relacija)
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Mjesto polaska - lokacija vozila</td>
                        <td class="col2">
                            <% Html.DevExpress().ComboBox(
                                   settings =>
                                   {
                                       settings.Name = "id_lok";
                                       settings.Width = 180;
                                       settings.SelectedIndex = 0;
                                       settings.Properties.IncrementalFilteringMode =
                                           IncrementalFilteringMode.StartsWith;
                                       settings.Properties.DropDownStyle = DropDownStyle.DropDown;
                                       settings.Properties.TextField = "Naziv";
                                       settings.Properties.ValueField = "id_lok";
                                       settings.Properties.ValidationSettings.Assign(
                                           EditorsDemosHelper.NameValidationSettings);
                                       settings.Properties.ClientSideEvents.Validation =
                                           "OnNameValidation";
                                   }
                                   )
                                   .BindList(ViewData["id_lok"])
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Polazak</td>
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
                                                .Render(); %>
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
                        <td class="col1">Dolazak</td>
                        <td class="col2">
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
                        <td class="col1">Broj putnika</td>
                        <td class="col2">
                            <% Html.DevExpress().TextBox(
                                   settings =>
                                   {
                                       settings.Name = "BrojPutnika";
                                       settings.Width = 90;
                                       settings.ControlStyle.CssClass = "editor";
                                       settings.Properties.ValidationSettings.Assign(
                                           EditorsDemosHelper.NameValidationSettings);
                                       settings.Properties.ClientSideEvents.Validation = "OnNameValidation";
                                   }
                                   )
                                   .Bind(Model.broj_putnika)
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Kontakt Telefon</td>
                        <td class="col2">
                            <% Html.DevExpress().TextBox(
                                   settings =>
                                   {
                                       settings.Name = "Kontakt_Tel";
                                       settings.Width = 180;
                                       settings.ControlStyle.CssClass = "editor";
                                       settings.Properties.ValidationSettings.Assign(
                                           EditorsDemosHelper.NameValidationSettings);
                                       settings.Properties.ClientSideEvents.Validation =
                                           "OnNameValidation";
                                   }
                                   )
                                   .Bind(Model.Kontakt_Tel)
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Svrha putovanja</td>
                        <td class="col2">
                            <% Html.DevExpress().Memo(
                                   settings =>
                                   {
                                       settings.Name = "Opis";
                                       settings.Width = 180;
                                       settings.ControlStyle.CssClass = "editor";
                                   }
                                   )
                                   .Bind(Model.Opis)
                                   .Render(); %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Mjesto Putovanja</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbMjestaPartial", ViewData["id_grad"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Ponavljanje rezervacije</td>
                        <td class="col2">
                            <% Html.DevExpress().SpinEdit(
                                   settings =>
                                   {
                                       settings.Name = "pon";
                                       settings.Width = 180;
                                       settings.ControlStyle.CssClass = "editor";
                                   }
                                   )
                                   .Bind(Model.Ponavljanje)
                                   .Render(); %>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="border-style: none; border-width: thin; width: 37px">&nbsp;</td>
            <td style="border-style: none; border-width: thin; vertical-align: top; width: 407px;">
                <table style="width: 100%">
                    <tr>
                        <td class="col1">Putnik 1</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik1Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 2</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik2Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 3</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik3Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 4</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik4Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 5</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik5Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Putnik 6</td>
                        <td class="col2">
                            <% Html.RenderPartial("CbPutnik6Partial", ViewData["id_zaposlenik"]);%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <hr />
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
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="head">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 255px;
        }

        .style2
        {
            width: 130px;
        }

        .style4
        {
            width: 422px;
        }
    </style>
</asp:Content>