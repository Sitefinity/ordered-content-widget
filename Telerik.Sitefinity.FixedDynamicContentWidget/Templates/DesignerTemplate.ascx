<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="KendoWeb" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
</sf:ResourceLinks>

<div id="tabstrip">
    <ul>
        <li class="k-state-active">Content</li>
        <li>List Settings</li>
        <li>Single Item Settings</li>
    </ul>
    <div>
        1
    </div>
    <div>
        2
    </div>
    <div>
        3
    </div>
</div>

