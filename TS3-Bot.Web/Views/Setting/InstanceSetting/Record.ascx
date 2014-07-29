<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Record.Enabled) %>
    <%: Html.EditorFor(m => m.Record.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Record.LogEnabled)%>
        <%: Html.EditorFor(m => m.Record.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Record.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Record.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Record.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Record.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Record.Behavior)%>
        <%: Html.EditorFor(m => m.Record.Behavior)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Record.KickMessage)%>
        <%: Html.EditorFor(m => m.Record.KickMessage)%>
    </div>
</div>