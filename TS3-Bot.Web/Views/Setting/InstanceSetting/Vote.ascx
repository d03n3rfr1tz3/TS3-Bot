<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Vote.Enabled) %>
    <%: Html.EditorFor(m => m.Vote.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Vote.LogEnabled)%>
        <%: Html.EditorFor(m => m.Vote.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Vote.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Vote.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Vote.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Vote.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Vote.NeededCompliants)%>
        <%: Html.EditorFor(m => m.Vote.NeededCompliants)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Vote.Behavior)%>
        <%: Html.EditorFor(m => m.Vote.Behavior)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Vote.KickMessage)%>
        <%: Html.EditorFor(m => m.Vote.KickMessage)%>
    </div>
</div>