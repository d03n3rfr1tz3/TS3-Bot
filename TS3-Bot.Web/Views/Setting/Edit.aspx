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
    
        <div>
            <%: Html.EditorFor(m => m.TeamSpeak) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Away) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.BadNickname) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Control) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Event) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Global) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Idle) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Message) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Record) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Sticky) %>
        </div>

        <div>
            <%: Html.EditorFor(m => m.Vote) %>
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    <% } %>

</asp:Content>