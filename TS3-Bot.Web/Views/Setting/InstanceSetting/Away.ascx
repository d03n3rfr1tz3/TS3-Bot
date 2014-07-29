<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Away.Enabled) %>
    <%: Html.EditorFor(m => m.Away.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Away.LogEnabled) %>
        <%: Html.EditorFor(m => m.Away.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Away.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Away.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Away.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Away.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Away.Channel)%>
        <%: Html.EditorFor(m => m.Away.Channel)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Away.MuteHeadphoneChannel)%>
        <%: Html.EditorFor(m => m.Away.MuteHeadphoneChannel)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Away.MuteMicrophoneChannel)%>
        <%: Html.EditorFor(m => m.Away.MuteMicrophoneChannel)%>
    </div>
</div>