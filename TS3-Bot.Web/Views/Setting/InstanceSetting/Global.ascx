<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DirkSarodnick.TS3_Bot.Core.Settings.InstanceSettings>" %>

<div>
    <%: Html.LabelFor(m => m.Global.BotNickname) %>
    <%: Html.EditorFor(m => m.Global.BotNickname)%>
</div>
<div>
    <%: Html.LabelFor(m => m.Global.Globalization) %>
    <%: Html.EditorFor(m => m.Global.Globalization)%>
</div>