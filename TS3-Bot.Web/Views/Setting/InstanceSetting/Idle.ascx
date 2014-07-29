<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Idle.Enabled) %>
    <%: Html.EditorFor(m => m.Idle.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Idle.LogEnabled)%>
        <%: Html.EditorFor(m => m.Idle.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Idle.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Idle.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Idle.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Idle.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Idle.Channel)%>
        <%: Html.EditorFor(m => m.Idle.Channel)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Idle.IdleTime)%>
        <%: Html.EditorFor(m => m.Idle.IdleTime)%>
    </div>
</div>