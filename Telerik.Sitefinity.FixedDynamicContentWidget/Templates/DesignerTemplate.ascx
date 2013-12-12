<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls" Assembly="Telerik.Sitefinity" %>

<sf:JavaScriptEmbedControl runat="server" ID="JavaScriptEmbedControl3" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" />
<sf:JavaScriptEmbedControl runat="server" ID="angularLink" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.4/angular.min.js" />
<sf:JavaScriptEmbedControl runat="server" ID="angularResourceLink" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.4/angular-resource.js" />
<sf:JavaScriptEmbedControl runat="server" ID="JavaScriptEmbedControl2" ScriptEmbedPosition="Head" Url="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js" />
<sf:JavaScriptEmbedControl runat="server" ID="JavaScriptEmbedControl1" ScriptEmbedPosition="Head" Url="http://cdnjs.cloudflare.com/ajax/libs/angular-ui/0.4.0/angular-ui.min.js" />

<sf:ResourceLinks ID="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="KendoWeb" />
    <sf:ResourceFile JavaScriptLibrary="jQuery" />
    <sf:ResourceFile Name="Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css" Static="True" />
</sf:ResourceLinks>

<div ng-app="DesignerApp" ng-controller="DesignerCtrl">

    <div id="tabstrip">
        <ul>
            <li class="k-state-active">
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Content %>" />
            </li>
            <li>
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ListSettings %>" />
            </li>
            <li>
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SingleItemSettings %>" />
            </li>
        </ul>
        <div>
            <strong>
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ContentType %>" />
            </strong>
            <br />
            <select ng-model="selectedDynamicType" ng-options="t.Title for t in dynamicTypes">
                <option value="">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:OrderedContentResources, ChooseContentType %>" />
                </option>
            </select>

            <p>
                <strong>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:OrderedContentResources, WidgetHelp1 %>" />
                </strong>
                <br />
                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:OrderedContentResources, WidgetHelp2 %>" />
            </p>

            <div id="all-selected-tabstrip">
                <ul>
                    <li class="k-state-active">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, All %>" />
                        ({{allItemsVirtualCount}})
                    </li>
                    <li>
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Selected %>" />
                    </li>
                </ul>
                <div>
                    <table style="width: 500px;">
                        <thead>
                            <tr>
                                <th></th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Title %>" />
                                </th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ModifiedOn %>" />
                                </th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, By %>" />
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="item in allItems">
                                <td ng-click="toggleSelect(item.Id)"><input type="checkbox" /></td>
                                <td>{{ item.Title }}</td>
                                <td>{{ item.LastModified | date:'mediumDate' }}</td>
                                <td>{{ item.Author }}</td>
                                <td><a href="{{ item.CanonicalUrl }}">View</a></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div>
                    <table style="width: 500px;">
                        <thead>
                            <tr>
                                <th></th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Title %>" />
                                </th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ModifiedOn %>" />
                                </th>
                                <th>
                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, By %>" />
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody ui-sortable ng-model="selectedItems">
                            <tr ng-repeat="item in selectedItems" style="cursor: move;">
                                <td ng-click="toggleSelect(item.Id)"><input type="checkbox" /></td>
                                <td>{{ item.Title }}</td>
                                <td>{{ item.LastModified | date:'mediumDate' }}</td>
                                <td>{{ item.Author }}</td>
                                <td>
                                    <a href="{{ item.CanonicalUrl }}">
                                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, By %>" />
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

        </div>
        <div>
            2
        </div>
        <div>
            3
        </div>
    </div>

</div>
