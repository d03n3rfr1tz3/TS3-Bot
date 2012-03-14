<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<InstanceSettings>>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<table>
    <tr>
        <th></th>
        <th>
            FilePath
        </th>
        <th>
            Enabled
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id = item.Id }) %>
        </td>
        <td>
            <%: Html.DisplayFor(m => item.Id) %>
        </td>
        <td>
            <%: Html.DisplayFor(m => item.Enabled) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>