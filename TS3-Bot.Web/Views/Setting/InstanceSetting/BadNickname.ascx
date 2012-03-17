<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.BadNickname.Enabled) %>
    <%: Html.EditorFor(m => m.BadNickname.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.BadNickname.LogEnabled)%>
        <%: Html.EditorFor(m => m.BadNickname.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.BadNickname.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.BadNickname.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.BadNickname.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.BadNickname.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.BadNickname.BadNicknames)%>
        <%: Html.EditorFor(m => m.BadNickname.BadNicknames)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.BadNickname.KickMessage)%>
        <%: Html.EditorFor(m => m.BadNickname.KickMessage)%>
    </div>
</div>