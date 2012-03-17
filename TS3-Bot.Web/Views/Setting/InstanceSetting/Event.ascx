<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Event.Enabled) %>
    <%: Html.EditorFor(m => m.Event.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Event.LogEnabled)%>
        <%: Html.EditorFor(m => m.Event.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Event.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Event.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Event.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Event.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Event.Events)%>
        <%: Html.EditorFor(m => m.Event.Events)%>
    </div>
</div>