<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.TeamSpeak.Host) %>
    <%: Html.EditorFor(m => m.TeamSpeak.Host) %>
</div>

<div>
    <%: Html.LabelFor(m => m.TeamSpeak.QueryPort) %>
    <%: Html.EditorFor(m => m.TeamSpeak.QueryPort) %>
</div>

<div>
    <%: Html.LabelFor(m => m.TeamSpeak.Instance) %>
    <%: Html.EditorFor(m => m.TeamSpeak.Instance) %>
</div>

<div>
    <%: Html.LabelFor(m => m.TeamSpeak.Username) %>
    <%: Html.EditorFor(m => m.TeamSpeak.Username) %>
</div>

<div>
    <%: Html.LabelFor(m => m.TeamSpeak.Password) %>
    <%: Html.EditorFor(m => m.TeamSpeak.Password) %>
</div>