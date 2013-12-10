<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls" Assembly="Telerik.Sitefinity" %>

<sf:JavaScriptEmbedControl runat="server" ID="angularLink" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.4/angular.min.js" />
<sf:JavaScriptEmbedControl runat="server" ID="angularResourceLink" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.4/angular-resource.js" />

<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="KendoWeb" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
</sf:ResourceLinks>

<div ng-app="DesignerApp" ng-controller="DesignerCtrl">

<div id="tabstrip">
    <ul>
        <li class="k-state-active">Content</li>
        <li>List Settings</li>
        <li>Single Item Settings</li>
    </ul>
    <div>
        <strong>Content type</strong>
        <br />
        <select ng-model="selectedDynamicType" ng-options="t.Title for t in dynamicTypes">
            <option value="">-- choose content type --</option>
        </select>
    </div>
    <div>
        2
    </div>
    <div>
        3
    </div>
</div>

</div>