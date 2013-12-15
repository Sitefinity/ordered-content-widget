<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" Assembly="Telerik.Sitefinity" %>

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

<telerik:radwindowmanager id="windowManager" runat="server" height="100%" width="100%"
    behaviors="None" skin="Sitefinity" showcontentduringload="false" visiblestatusbar="false">
    <windows>
        <telerik:RadWindow ID="widgetEditorDialog" runat="server" ReloadOnShow="true" Modal="true" />
    </windows>
</telerik:radwindowmanager>

<div ng-app="DesignerApp" ng-controller="DesignerCtrl">

    <div ng-show="showDesigner()">
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

                <div style="margin: 10px 0;">

                    <a href="#" ng-click="showFilters = !showFilters">
                        <strong>
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:OrderedContentResources, FilterItems %>" />
                        </strong>
                        <i>(<asp:Literal ID="Literal5" runat="server" Text="<%$Resources:OrderedContentResources, ByCategoryTag %>" />)</i>
                    </a>

                    <div ng-show="showFilters">
                        <div ng-non-bindable>
                            <asp:PlaceHolder ID="filterSelectorPlaceholder" runat="server">
                            </asp:PlaceHolder>
                        </div>
                        <a href="#" ng-click="applyFilter()">Apply filter</a>
                    </div>

                </div>

                <div ng-hide="sortMode != 'Manual'">
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
                                ({{ allItemsVirtualCount }})
                            </li>
                            <li>
                                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Selected %>" />
                                ({{ selectedItems.length }})
                            </li>
                        </ul>
                        <div>
                            <table style="width: 500px;">
                                <thead>
                                    <tr>
                                        <th>
                                            <input type="checkbox" ng-click="selectAll()" />
                                        </th>
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
                                <tbody ng-show="allItems.length > 0">
                                    <tr ng-repeat="item in allItems">
                                        <td>
                                            <input type="checkbox" ng-click="toggleSelect(item.Id)" ng-checked="isSelected(item.Id)" /></td>
                                        <td>{{ item.Title }}</td>
                                        <td>{{ item.LastModified | date:'mediumDate' }}</td>
                                        <td>{{ item.Author }}</td>
                                        <td>
                                            <a href="{{ item.CanonicalUrl }}">
                                                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, View %>" />
                                            </a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!-- empty screen -->
                            <div ng-hide="allItems.length > 0">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:OrderedContentResources, NoItemsMatchedFilter %>" />
                            </div>
                            <!-- pager -->
                            <ul>
                                <li>
                                    <a href="#" ng-if="currentPageSegment > 1" ng-click="changePageSegment(-1)">...</a>
                                </li>
                                <li ng-repeat="page in allItemsPages">
                                    <a href="#" ng-click="changePage(page.Number)" ng-if="page.Number !== currentPage">{{ page.Number }}</a>
                                    <strong ng-if="page.Number === currentPage">{{ page.Number }}</strong>
                                </li>
                                <li>
                                    <a href="#" ng-if="currentPageSegment < totalPageSegments" ng-click="changePageSegment(1)">...</a>
                                </li>
                            </ul>
                        </div>
                        <div>
                            <table style="width: 500px;">
                                <thead>
                                    <tr>
                                        <th>
                                            <input type="checkbox" ng-click="unselectAll()" ng-checked="selectedItems.length > 0" />
                                        </th>
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
                                <tbody ui-sortable ng-model="selectedItems" ng-show="selectedItems.length > 0">
                                    <tr ng-repeat="item in selectedItems" style="cursor: move;">
                                        <td>
                                            <input type="checkbox" ng-click="toggleSelect(item.Id)" ng-checked="isSelected(item.Id)" /></td>
                                        <td>{{ item.Title }}</td>
                                        <td>{{ item.LastModified | date:'mediumDate' }}</td>
                                        <td>{{ item.Author }}</td>
                                        <td>
                                            <a href="{{ item.CanonicalUrl }}">
                                                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, View %>" />
                                            </a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div ng-hide="selectedItems.length > 0">
                                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, NoItemsHaveBeenSelected %>" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <input type="radio" ng-model="listMode" value="paging" />
                <strong>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, AllowPaging %>" /></strong>
                <br />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, DivideTheListOnPagesUpTo %>"></asp:Literal>
                <input type="text" ng-model="itemsPerPage" ng-disabled="listMode !== 'paging'" />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ItemsPerPage %>"></asp:Literal>

                <br />

                <input type="radio" ng-model="listMode" value="limit" />
                <strong>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, UseLimit %>" /></strong>
                <br />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ShowOnlyLimitedNumberOfItems %>"></asp:Literal>
                <input type="text" ng-model="limitCount" name="master-mode" ng-disabled="listMode !== 'limit'" />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ItemsInTotal %>"></asp:Literal>

                <br />

                <input type="radio" ng-model="listMode" value="none" />
                <strong>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, NoLimitAndPaging %>" /></strong>
                <br />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ShowAllPublishedItemsAtOnce %>" />

                <br />
                <strong>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SortItems %>" />
                </strong>
                <br />
                <select ng-model="sortMode" ng-init="sortMode='Manual'">
                    <option value="NewestFirst">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, NewestFirst %>" /></option>
                    <option value="OldestFirst">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, OldestFirst %>" /></option>
                    <option value="AlphabetAsc">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ByTitleAz %>" /></option>
                    <option value="AlphabetDesc">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ByTitleZa %>" /></option>
                    <option value="Manual">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, AsManuallyOrdered %>" /></option>
                </select>

                <sf:CreateEditTemplateControl runat="server" ID="listTemplates" ClientIDMode="Predictable" Title="<%$Resources:OrderedContentResources,ListTemplate %>"></sf:CreateEditTemplateControl>

            </div>
            <div>

                <strong>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, OpenSingleItemIn %>" /></strong>
                <br />
                <input type="radio" ng-model="detailPageMode" value="auto" />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, AutoGeneratedPage %>" />
                <i>(<asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, WithTheSameLayoutAsTheListPage %>" />)</i>
                <br />
                <input type="radio" ng-model="detailPageMode" value="custom" />
                <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SelectedExistingPage %>" />...
            <div ng-hide="detailPageMode == 'auto'">
                <span>{{ detailsPage.Title }}</span>
                <a href="#" ng-click="showPageSelector = true">
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SelectPage %>" />
                </a>
            </div>

                <sf:CreateEditTemplateControl runat="server" ID="singleItemTemplates" ClientIDMode="Predictable" Title="<%$Resources:OrderedContentResources,SingleItemTemplate %>"></sf:CreateEditTemplateControl>
            </div>
        </div>
    </div>

    <div ng-show="showPageSelector">
        <sf:PageSelector id="pageSelector" runat="server"></sf:PageSelector>
    </div>



</div>


