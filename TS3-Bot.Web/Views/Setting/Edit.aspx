<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<InstanceSettings>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
    
        <div><%: Html.Partial("InstanceSetting/TeamSpeak", Model) %></div>
        <div><%: Html.Partial("InstanceSetting/Away", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/BadNickname", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Control", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Event", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Global", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Idle", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Message", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Record", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Sticky", Model)%></div>
        <div><%: Html.Partial("InstanceSetting/Vote", Model)%></div>

        <p>
            <input type="submit" value="Save" />
        </p>
    <% } %>

</asp:Content>