<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Control.Help.Enabled) %>
    <%: Html.EditorFor(m => m.Control.Help.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Control.Help.LogEnabled)%>
        <%: Html.EditorFor(m => m.Control.Help.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Help.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Help.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Help.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Help.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Help.HelpMessage)%>
        <%: Html.EditorFor(m => m.Control.Help.HelpMessage)%>
    </div>
</div>

<hr />

<div>
    <%: Html.LabelFor(m => m.Control.Files.Enabled) %>
    <%: Html.EditorFor(m => m.Control.Files.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Control.Files.LogEnabled)%>
        <%: Html.EditorFor(m => m.Control.Files.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Files.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Files.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.MessagePerServer)%>
        <%: Html.EditorFor(m => m.Control.Files.MessagePerServer)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.MessageFile)%>
        <%: Html.EditorFor(m => m.Control.Files.MessageFile)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.MessageFilesFound)%>
        <%: Html.EditorFor(m => m.Control.Files.MessageFilesFound)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.MessageNoFilesFound)%>
        <%: Html.EditorFor(m => m.Control.Files.MessageNoFilesFound)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Files.HelpMessage)%>
        <%: Html.EditorFor(m => m.Control.Files.HelpMessage)%>
    </div>
</div>

<hr />

<div>
    <%: Html.LabelFor(m => m.Control.Seen.Enabled) %>
    <%: Html.EditorFor(m => m.Control.Seen.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Control.Seen.LogEnabled)%>
        <%: Html.EditorFor(m => m.Control.Seen.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Seen.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Seen.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Seen.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Seen.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Seen.TextMessage)%>
        <%: Html.EditorFor(m => m.Control.Seen.TextMessage)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Seen.HelpMessage)%>
        <%: Html.EditorFor(m => m.Control.Seen.HelpMessage)%>
    </div>
</div>

<hr />

<div>
    <%: Html.LabelFor(m => m.Control.Stick.Enabled) %>
    <%: Html.EditorFor(m => m.Control.Stick.Enabled)%>
</div>
<div class="whenEnabled">
    <div>
        <%: Html.LabelFor(m => m.Control.Stick.LogEnabled)%>
        <%: Html.EditorFor(m => m.Control.Stick.LogEnabled)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Stick.PermittedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Stick.PermittedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Stick.DeniedServerGroups)%>
        <%: Html.EditorFor(m => m.Control.Stick.DeniedServerGroups)%>
    </div>

    <div>
        <%: Html.LabelFor(m => m.Control.Stick.HelpMessage)%>
        <%: Html.EditorFor(m => m.Control.Stick.HelpMessage)%>
    </div>
</div>