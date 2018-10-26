<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

    <div class="blue">
        <% using (Html.BeginForm())
           {%>
        <table style="border: 0px none #FFFFFF;">
            <tr style="border-style: none">
                     <td style="border-style: none">Pretraga po zaposleniku: <%:Html.TextBox("SearchString")%> 
                   </td>
                <td>Automobil : <%: Html.DropDownList("id_auto",String.Empty) %> </td>
                 <td>Lokacija : <%: Html.DropDownList("id_lok",String.Empty) %> </td>
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