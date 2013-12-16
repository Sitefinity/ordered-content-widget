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
    <sf:ResourceFile Name="Styles/Tabstrip.css" />
    <sf:ResourceFile Name="Styles/Grid.css" />
</sf:ResourceLinks>

<sf:ResourceLinks ID="ResourceLinks1" runat="server" UseEmbeddedThemes="true"> 
    <sf:ResourceFile Name="Telerik.Sitefinity.OrderedContentWidget.Styles.styles.css" AssemblyInfo="Telerik.Sitefinity.OrderedContentWidget.Designer, Telerik.Sitefinity.OrderedContentWidget" Static="true" /> 
</sf:ResourceLinks> 


<telerik:radwindowmanager id="windowManager" runat="server" height="100%" width="100%"
    behaviors="None" skin="Sitefinity" showcontentduringload="false" visiblestatusbar="false">
    <windows>
        <telerik:RadWindow ID="widgetEditorDialog" runat="server" ReloadOnShow="true" Modal="true" />
    </windows>
</telerik:radwindowmanager>

<div ng-app="DesignerApp" ng-controller="DesignerCtrl">

    <div ng-show="showDesigner()" class="sfBasicDim">
        <div id="tabstrip" class="k-sitefinity">
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
            <div class="sfContentViews sfContentDim730">
                <div class="sfFormCtrl">
                    <label class="sfTxtLbl" for="field_selectedDynamicType">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ContentType %>" />
                    </label>
                    <select ng-model="selectedDynamicType" ng-options="t.Title for t in dynamicTypes" id="field_selectedDynamicType">
                        <option value="">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:OrderedContentResources, ChooseContentType %>" />
                        </option>
                    </select>
                </div>
                <div class="sfExpandableSection" id="filtersWrp">
                    <h3>
                        <a href="#" ng-click="showFilters()" class="sfMoreDetails">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:OrderedContentResources, FilterItems %>" />
                            <i class="sfNote">(<asp:Literal ID="Literal5" runat="server" Text="<%$Resources:OrderedContentResources, ByCategoryTag %>" />)</i>
                        </a>
                    </h3>
                    <div class="sfTargetList">
                        <div ng-non-bindable>
                            <asp:PlaceHolder ID="filterSelectorPlaceholder" runat="server">
                            </asp:PlaceHolder>
                        </div>
                        <a href="#" ng-click="applyFilter()" class="sfLinkBtn sfChange"><span class="sfLinkBtnIn">Apply filter</span></a>
                    </div>

                </div>

                <div ng-hide="sortMode != 'Manual'">
                    <h2>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:OrderedContentResources, WidgetHelp1 %>" />
                    </h2>
                    <p class="sfNote">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:OrderedContentResources, WidgetHelp2 %>" />
                    </p>

                    <div id="all-selected-tabstrip" class="k-sitefinity">
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
                        <asp:TextBox runat="server" placeholder="<%$Resources:OrderedContentResources, NarrowByTyping %>" ng-model="mainFieldStartsWith" CssClass="sfTxt sfMTop10 sfMBottom10"></asp:TextBox>
                        <div>
                            <div class="sfSelectorGridWrapper">
                                <div class="k-grid k-widget">
                                    <table ng-show="allItems.length > 0">
                                        <thead class="k-grid-header">
                                            <tr>
                                                <th class="k-header sfCheckBoxCol">
                                                    <input type="checkbox" ng-click="selectAll()" ng-checked="isSelectAllChecked" />
                                                </th>
                                                <th class="sfTitleCol k-header">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Title %>" />
                                                </th>
                                                <th class="k-header sfRegular">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ModifiedOn %>" />
                                                </th>
                                                <th class="k-header sfRegular">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, By %>" />
                                                </th>
                                                <th class="k-header sfView"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-repeat="item in allItems">
                                                <td class="sfCheckBoxCol">
                                                    <input type="checkbox" ng-click="toggleSelect(item.Id)" ng-checked="isSelected(item.Id)" /></td>
                                                <td class="sfTitleCol"><strong class="sfItemTitle">{{ item.Title }}</strong></td>
                                                <td class="sfRegular">{{ item.LastModified | date:'mediumDate' }}</td>
                                                <td class="sfRegular">{{ item.Author }}</td>
                                                <td class="sfView">
                                                    <a href="{{ item.CanonicalUrl }}" ng-show="{{ item.CanonicalUrl && item.CanonicalUrl.length > 0 }}" target="_blank" class="sfExternal">
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
                                </div>
                            </div>
                            <!-- pager -->
                            <div class="k-pager-wrap k-grid-pager k-widget">
                                <ul class="k-pager-numbers k-reset">
                                    <li>
                                        <a href="#" ng-if="currentPageSegment > 1" ng-click="changePageSegment(-1)" class="k-link">...</a>
                                    </li>
                                    <li ng-repeat="page in allItemsPages">
                                        <a href="#" ng-click="changePage(page.Number)" ng-if="page.Number !== currentPage" class="k-link">{{ page.Number }}</a>
                                        <strong ng-if="page.Number === currentPage">{{ page.Number }}</strong>
                                    </li>
                                    <li>
                                        <a href="#" ng-if="currentPageSegment < totalPageSegments" ng-click="changePageSegment(1)" class="k-link">...</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div>
                            <div class="sfSelectorGridWrapper sfDragNDropListWrp">
                                <div class="k-grid k-widget">
                                    <table ng-show="selectedItems.length > 0">
                                        <thead class="k-grid-header">
                                            <tr>
                                                <th class="k-header sfCheckBoxCol">
                                                    <%--<input type="checkbox" ng-click="unselectAll()" ng-checked="selectedItems.length > 0" />--%>
                                                </th>
                                                <th class="sfTitleCol k-header">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, Title %>" />
                                                </th>
                                                <th class="k-header sfRegular">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ModifiedOn %>" />
                                                </th>
                                                <th class="k-header sfRegular">
                                                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, By %>" />
                                                </th>
                                                <th class="k-header sfView"></th>
                                            </tr>
                                        </thead>
                                        <tbody ui-sortable ng-model="selectedItems">
                                            <tr ng-repeat="item in selectedItems" style="cursor: move;">
                                                <td class="sfCheckBoxCol">
                                                    <i class="sfDragPoint"></i>
                                                    <input type="checkbox" ng-click="toggleSelect(item.Id)" ng-checked="isSelected(item.Id)" /></td>
                                                <td class="sfTitleCol"><strong class="sfItemTitle">{{ item.Title }}</strong></td>
                                                <td class="sfRegular">{{ item.LastModified | date:'mediumDate' }}</td>
                                                <td class="sfRegular">{{ item.Author }}</td>
                                                <td class="sfView">
                                                    <a href="{{ item.CanonicalUrl }}" ng-show="{{ item.CanonicalUrl && item.CanonicalUrl.length > 0 }}" target="_blank" class="sfExternal">
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
                </div>
            </div>
            <div class="sfContentViews">
                <ul class="sfRadioList sfTitledList">
                    <li>
                        <input type="radio" ng-model="listMode" value="paging" id="field_ListMode_Paging" />
                        <label for="field_ListMode_Paging"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, AllowPaging %>" /></label>
                        <div class="sfCheckBox sfSetNumberPropery sfExpandedPropertyDetails">
                            <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, DivideTheListOnPagesUpTo %>"></asp:Literal>
                            <input type="text" ng-model="itemsPerPage" ng-disabled="listMode !== 'paging'" class="sfTxt" />
                            <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ItemsPerPage %>"></asp:Literal>
                        </div>
                    </li>
                    <li>
                        <input type="radio" ng-model="listMode" value="limit" id="field_ListMode_Limit" />
                        <label for="field_ListMode_Limit"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, UseLimit %>" /></label>
                        <div class="sfCheckBox sfSetNumberPropery sfExpandedPropertyDetails">
                            <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ShowOnlyLimitedNumberOfItems %>"></asp:Literal>
                            <input type="text" ng-model="limitCount" name="master-mode" ng-disabled="listMode !== 'limit'" class="sfTxt" />
                            <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ItemsInTotal %>"></asp:Literal>
                        </div>
                    </li>
                    <li>
                        <input type="radio" ng-model="listMode" value="none" id="field_ListMode_None" />
                        <label for="field_ListMode_None"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, NoLimitAndPaging %>" /></label>
                        <div class="sfExpandedPropertyDetails">
                            <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, ShowAllPublishedItemsAtOnce %>" />
                        </div>
                    </li>
                </ul>
                <div class="sfFormCtrl">
                    <label class="sfTxtLbl" for="field_SortMode">
                        <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SortItems %>" />
                    </label>
                    <select ng-model="sortMode" ng-init="sortMode='Manual'" id="field_SortMode">
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
                </div>
                <h2><asp:Literal runat="server" id="lListTemplate" Text="<%$Resources:OrderedContentResources,ListTemplate %>" /></h2>
                <sf:CreateEditTemplateControl runat="server" ID="listTemplates" ClientIDMode="Predictable"></sf:CreateEditTemplateControl>

            </div>
            <div class="sfContentViews">

                <h2>
                    <asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, OpenSingleItemIn %>" /></h2>
                <ul class="sfRadioList">
                    <li>
                        <input type="radio" ng-model="detailPageMode" value="auto" id="field_DetailPageMode_auto" />
                        <label for="field_DetailPageMode_auto"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, AutoGeneratedPage %>" /></label>
                        <i class="sfNote">(<asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, WithTheSameLayoutAsTheListPage %>" />)</i>
                    </li>
                    <li>
                        <input type="radio" ng-model="detailPageMode" value="custom" id="field_DetailPageMode_custom" />
                        <label for="field_DetailPageMode_custom"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SelectedExistingPage %>" />...</label>
                        <div ng-hide="detailPageMode == 'auto'" ng-show="resizeMainDlg()" class="sfExpandedPropertyDetails">
                            <span class="sfSelectedItem">{{ detailsPage.Title }}</span>
                            <a href="#" ng-click="showPageSelector = true" class="sfLinkBtn sfChange">
                                <span class="sfLinkBtnIn"><asp:Literal runat="server" Text="<%$Resources:OrderedContentResources, SelectPage %>" /></span>
                            </a>
                        </div>
                    </li>
                </ul>
                <h2><asp:Literal runat="server" id="lSingleItemTemplate" Text="<%$Resources:OrderedContentResources,SingleItemTemplate %>" /></h2>
                <sf:CreateEditTemplateControl runat="server" ID="singleItemTemplates" ClientIDMode="Predictable"></sf:CreateEditTemplateControl>
            </div>
        </div>
    </div>
    <div ng-show="showPageSelector" class="sfPageSelectorWrp">
        <sf:PageSelector id="pageSelector" runat="server"></sf:PageSelector>
    </div>
</div>
