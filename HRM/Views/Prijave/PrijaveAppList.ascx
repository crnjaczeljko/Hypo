<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<HRM.Models.vAplikacijeDetalj>>" %>

<style type="text/css">
    .auto-style2
    {
        width: 130px;
    }
</style>

<table>
    <tr>
    <td>
        <hr>
<table id="hor-minimalist-b">
    <tr>
        <th>Aplikacija</th>
        <th class="auto-style2">Datum potvrde IT</th>
        <th>Status IT</th>
        <th>Potvrda Nadređ. osobe</th>
        <th>Status nad.os</th>
        <th>Status prijave</th>
        <th>Aktivna</th>
      </tr>

<% foreach (var item in Model) { %>
    <tr>
         <%: Html.HiddenFor(modelItem => item.id_prijave) %>
        <td>
            <%: Html.DisplayFor(modelItem => item.naziv) %>
        </td>
        <td class="auto-style2">
            <%: Html.DisplayFor(modelItem => item.dat_it_potvrde) %>
        </td>
        <td style="text-align: center">
             <% if (item.potvrda_it != null && (bool)item.potvrda_it)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.dat_voditelj) %>
        </td>
             <td style="text-align: center">
             <% if (item.potvrda_voditelj != null && (bool)item.potvrda_voditelj)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
        </td>
                     <td style="text-align: center">
             <% if (item.potvrde_all != null && (bool)item.potvrde_all)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
        </td>
                             <td style="text-align: center">
             <% if (item.aktivna != null && (bool)item.aktivna)
                {%>
                    <img src="../../images/green.png" alt="Edit" style="border-style: none" />
               <%  }
                else
                {%>
                    <img src="../../images/red.png" alt="Edit" style="border-style: none" />
               <% } %> 
        </td>
    </tr>
<% } %>

</table>

    </td>
        </tr>
    </table>
