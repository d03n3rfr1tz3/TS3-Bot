<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Sticky.Enabled) %>
    <%: Html.EditorFor(m => m.Sticky.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Sticky.LogEnabled)%>
        <%: Html.EditorFor(m => m.Sticky.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Sticky.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Sticky.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Sticky.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Sticky.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Sticky.Channel)%>
        <%: Html.EditorFor(m => m.Sticky.Channel)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Sticky.StickTime)%>
        <%: Html.EditorFor(m => m.Sticky.StickTime)%>
    </div>
</div>