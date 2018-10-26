<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Vozila.Models.Rezervacije>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Detalji rezervacije
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                        <td class="col1">Mjesto polaska - lokacija vozila</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Lokacije.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Krajnje odredi&#353;te
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Mjesta.Naziv) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Broj putnika</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.broj_putnika) %>
                        </td>         
                    </tr>
                    <tr> 
                        <td class="col1">Tip putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.TipRezervacije.Naziv) %>
                        </td>    
                    </tr>
                    <tr>
                        <td class="col1"> Svrha putovanja</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Opis) %>
                        </td>     
                    </tr>
                                        <tr>
                        <td class="col1">Kontakt broj</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Kontakt_Tel ) %>
                        </td>     
                    </tr>
                    <tr>
                        <td class="col1"> Putnik 1
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici1.ImePrezime) %>
                        </td>     
                    </tr>   
                    <tr>
                        <td class="col1"> Putnik 2</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici2.ImePrezime) %>
                        </td>     
                    </tr>  
                    <tr>
                        <td class="col1"> Putnik 3</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici3.ImePrezime) %>
                        </td>     
                    </tr>   
                    <tr>
                        <td class="col1"> Putnik 4</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici4.ImePrezime) %>
                        </td>     
                    </tr> 
                    <tr>
                        <td class="col1"> Putnik 5</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici5.ImePrezime) %>
                        </td>     
                    </tr>   
                    <tr>
                        <td class="col1"> Putnik 6</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zaposlenici6.ImePrezime) %>
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
                        <td class="col1"> Dodijeljeni automobil
                        </td>
                        <td class="col2" style="width: 210px">
                            <%: Html.DisplayFor(model => model.Automobil.Naziv) %>
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
                        <td class="col1">Po&#269;etna KM</td>
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
                        <td class="col1">Zaklju&#269;ak</td>
                        <td class="col2">
                            <%: Html.DisplayFor(model => model.Zapisnik) %>
                        </td>
                    </tr>
                    <tr>
                        <td class="col1">Tro&#353;kovi putovanja</td>
                        <td><% Html.RenderPartial("TroskoviList", ViewData["TroskoviPage"]); %></td>
                    </tr>
                </table>

                <% Html.EndForm(); %>
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Links" runat="server">
    <%: Html.ActionLink("Natrag", "Index") %>
</asp:Content>