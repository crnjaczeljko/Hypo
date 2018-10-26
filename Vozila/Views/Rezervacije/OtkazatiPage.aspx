<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Zaključenje rezervacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"> </script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"> </script>

    <% Html.EnableClientValidation();%> 
    <%
       Html.BeginForm("ZakljucitiPage", "Rezervacije", FormMethod.Post,
                      new {id = "validationForm", @class = "edit_form"});
    %>
                 
    <table>
        <tr>
            <td style="vertical-align: top; width: 382px;"><h2>Opis</h2>
                <hr/>
                <table style="width: 100%">
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
                        <td class="col1">Polazno odredi&#353;te</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Krajnje odredi&#353;te</td>
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
                        <td class="col1"> Dodijeljeni automobil
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
            <td style="vertical-align: top; width: 459px;">
                <h2>Zapisnik</h2>
                <hr/>
                <table>
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
                        <td class="col1">Pocetna KM</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Poc_KM) %>
                        </td>
                    </tr><tr>
                             <td class="col1">Krajnja KM</td>
                             <td class="col2">
                                 <%: Html.DisplayFor(model => model.Zav_KM) %>
                             </td>
                         </tr>
                    <tr>
                        <td class="col1">Zakljucak</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zapisnik) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Troskovi putovanja</td>
                        <td><% Html.RenderPartial("TroskoviList", ViewData["TroskoviPage"]); %></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnUpdate";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Otkazati";
                                            settings.UseSubmitBehavior = true;
                                        }
                                    )
                                    .Render();%>
                        </td>
                        <td>
                            <%
                                Html.DevExpress().Button(
                                    settings =>
                                        {
                                            settings.Name = "btnCancel";
                                            settings.ControlStyle.CssClass = "button";
                                            settings.Text = "Odustati";
                                            settings.UseSubmitBehavior = true;
                                        }
                                    )
                                    .Render();%>

                        </td>
                    </tr>
                </table>  
                <% Html.EndForm();%>
            </td>
        </tr>
    </table>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Links" runat="server">
    <%:Html.ActionLink ("Natrag", "ZapisnikList")
%>
</asp:Content>