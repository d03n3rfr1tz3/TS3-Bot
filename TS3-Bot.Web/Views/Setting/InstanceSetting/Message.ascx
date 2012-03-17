<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Message.Enabled) %>
    <%: Html.EditorFor(m => m.Message.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Message.LogEnabled)%>
        <%: Html.EditorFor(m => m.Message.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Message.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Message.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Message.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Message.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Message.Messages)%>
        <%: Html.EditorFor(m => m.Message.Messages)%>
    </div>
</div>