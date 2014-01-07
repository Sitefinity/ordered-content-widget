var designerApp = angular.module('DesignerApp', ['ngResource', 'ui.sortable'
   ]).factory('DynamicTypes', ['$resource',
        function ($resource) {
            return $resource('/restapi/orderedcontent/dynamictypes', {}, {
                query: { method: 'GET', isArray: true }
            });
        }
    ]).factory('DynamicContents', ['$resource',
        function ($resource) {
            return $resource('/restapi/orderedcontent/dynamiccontents/:id', {}, {
                query: { method: 'GET', isArray: false }
            });
        }
    ]).factory('Page', ['$resource',
        function ($resource) {
            return $resource('/restapi/orderedcontent/page/:id', {}, {
                get: { method: 'GET' }
            });
        }
    ]).factory('WidgetTemplate', ['$resource',
        function ($resource) {
            return $resource('/restapi/orderedcontent/widgettemplates', {}, {
                query: { method: 'GET', isArray: true }
            });
        }
    ]).controller('DesignerCtrl', ['$scope', 'DynamicTypes', 'DynamicContents', 'Page', 'WidgetTemplate',
        function ($scope, DynamicTypes, DynamicContents, Page, WidgetTemplate) {

            var pageSelector = null,
                filterSelector = null,
                listTemplateControl = null,
                singleItemTemplateControl = null,
                pagerPageSize = 20,
                pagerMaxPages = 10;

            $scope.controlData = {};
            $scope.controlDataLoaded = false;
            $scope.contentTypesLoaded = false;

            /**
             * Array with all the items that are currently visible in the selector.
             * Currently visible items depend on the selected content type, filters and paging.
             */
            $scope.allItems = [];

            /*
             * The virtual count of all the items that could be displayed given the current
             * filter expressions.
            **/
            $scope.allItemsVirtualCount = 0;

            $scope.allItemsPages = [];

            $scope.currentPage = 1;

            $scope.currentPageSegment = 1;

            $scope.totalPageSegments = 0;

            $scope.changePage = function (page) {

                var typeId = $scope.selectedDynamicType.Id;
                var skip = page * pagerPageSize - pagerPageSize;

                var queryData = filterSelector.get_queryData();
                if (queryData.QueryItems.length > 0) {
                    queryData = JSON.stringify(queryData);
                } else {
                    queryData = null;
                }

                DynamicContents.query({ id: typeId, queryData: queryData, skip: skip, take: pagerPageSize }, function (data) {
                    $scope.allItems = data.Items;
                    $scope.allItemsVirtualCount = data.VirtualCount;
                    $scope.currentPage = page;
                });

                $scope.isSelectAllChecked = false;
            };

            $scope.changePageSegment = function (add) {
                $scope.currentPageSegment = $scope.currentPageSegment + add;
                configurePager();
                $scope.isSelectAllChecked = false;
            }

            // Array with all the items that have been selected
            $scope.selectedItems = [];

            // the mode in which list operates. paging|limit|none
            $scope.listMode = "none"

            // Determines whether the paging will be used in master view.
            $scope.allowPaging = false;

            // Determines whether the master list should be limited to a finite number of items
            $scope.useLimit = false;

            // Determines the page size, if paging is enabled.
            $scope.itemsPerPage = 20;

            // Determines the limit of items to show if use limit mode
            $scope.limitCount = 20;

            // Determines the list sort mode
            $scope.sortMode = 'manual';

            // Determines whether page selector should be shown
            $scope.showPageSelector = false;

            $scope.showDesigner = function () {

                if ($scope.showPageSelector) return false;


                return true;
            };

            $scope.detailPageMode = 'auto';

            $scope.detailsPage = null;

            $scope.showFilters = function () {
                $("#filtersWrp").toggleClass("sfExpandedSection");
                dialogBase.resizeToContent();
            };

            $scope.applyFilter = function () {

                filterSelector.applyChanges();
                var queryData = filterSelector.get_queryData();
                queryData = JSON.stringify(queryData);

                var typeId = $scope.selectedDynamicType.Id;

                DynamicContents.query({ id: typeId, queryData: queryData }, function (data) {
                    $scope.allItems = data.Items;
                    $scope.allItemsVirtualCount = data.VirtualCount;
                });

            };

            $scope.mainFieldStartsWith = null;

            $scope.isSelectAllChecked = false;

            $scope.resizeMainDlg = function () {
                if (dialogBase)
                    dialogBase.resizeToContent();
            };

            /*
             * Finds the index of an item for a given id
             * within a specified collection.
            **/
            var findIndex = function (comparer, collection) {
                for (var i = 0; i < collection.length; i++) {
                    if (comparer(collection[i])) {
                        return i;
                    }
                }
                return -1;
            };

            var selectType = function (clrType) {
                if (clrType) {
                    $scope.selectedDynamicType = $scope.dynamicTypes[findIndex(function (item) {
                        return item.ClrType === clrType;
                    }, $scope.dynamicTypes)];
                }
            };

            var loadDesignerData = function () {
                selectType($scope.controlData.DynamicContentTypeName);
                if ($scope.selectedDynamicType.Id) {
                    DynamicContents.query({ id: $scope.selectedDynamicType.Id, SelectedContentIds: $scope.controlData.SelectedItems }, function (data) {
                        $scope.selectedItems = data.Items;
                        dialogBase.resizeToContent();
                    });
                }
            };

            // Selects an item
            var selectItem = function (id) {
                var item = $scope.allItems[findIndex(function (item) {
                    return item.Id === id;
                }, $scope.allItems)];
                $scope.selectedItems.push(item);
            };

            // Unselects an item
            var unselectItem = function (id) {
                $scope.selectedItems.splice(findIndex(function (item) {
                    return item.Id === id;
                }, $scope.selectedItems), 1);
                $scope.isSelectAllChecked = false;
            };

            var pageSelected = function (page) {
                $scope.$apply(function () {
                    $scope.detailsPage = page;
                    $scope.showPageSelector = false;
                });
            }

            var isItemSelected = function (item) {
                for (var i = 0; i < $scope.selectedItems.length; i++) {
                    if (item.Id === $scope.selectedItems[i].Id) return true;
                }
                return false;
            };

            /**
             * Configures the pager for the all items grid. The pager is configured to show
             * at most 10 pages at the moment. Should there be more pages, pager will introduce
             * segments. So, for example if there are 34 pages, that means that pager will consist
             * of 34 pages divided in 4 segments, where first three segments will each have 10 pages,
             * while the last one will have 4 pages. Aside from the page navigation, when appropriate
             * pager will render segment navigation as well (if more than one segment is present).
             */
            var configurePager = function () {
                var pages = [];
                var totalPages = Math.ceil($scope.allItemsVirtualCount / pagerPageSize);
                var firstPage = $scope.currentPageSegment * pagerMaxPages - pagerMaxPages;
                var lastPage = ($scope.currentPageSegment * pagerMaxPages) > totalPages ? totalPages : $scope.currentPageSegment * pagerMaxPages;
                for (var i = firstPage; i < lastPage ; i++) {
                    pages.push({
                        Number: i + 1
                    });
                }
                $scope.allItemsPages = pages;
                $scope.totalPageSegments = Math.ceil(totalPages / pagerMaxPages);
            };

            var initializeTemplateControls = function () {

                // TODO: refactor master and template definitions into a function
                var masterDefinition = $scope.controlData.ControlDefinition.Views.DynamicContentMasterView;
                var detailDefinition = $scope.controlData.ControlDefinition.Views.DynamicContentDetailView;

                WidgetTemplate.query({ dynamicTypeId: $scope.selectedDynamicType.Id, IsMaster: true }, function (data) {
                    listTemplateControl.get_viewsList().clearListItems();
                    for (var i = 0; i < data.length; i++) {
                        listTemplateControl.get_viewsList().addListItem(data[i].TemplateId, data[i].TemplateName);
                    }
                    listTemplateControl.get_viewsList().set_value(masterDefinition.TemplateKey);
                });

                WidgetTemplate.query({ dynamicTypeId: $scope.selectedDynamicType.Id, IsMaster: false }, function (data) {
                    singleItemTemplateControl.get_viewsList().clearListItems();
                    for (var i = 0; i < data.length; i++) {
                        singleItemTemplateControl.get_viewsList().addListItem(data[i].TemplateId, data[i].TemplateName);
                    }
                    singleItemTemplateControl.get_viewsList().set_value(detailDefinition.TemplateKey);
                });
            };

            $scope.selectedDynamicType = "";

            $scope.selectAll = function () {
                for (var i = 0; i < $scope.allItems.length; i++) {
                    if (isItemSelected($scope.allItems[i])) {
                        continue;
                    }
                    $scope.selectedItems.push($scope.allItems[i]);
                }
                $scope.isSelectAllChecked = true;
            };

            $scope.unselectAll = function () {
                $scope.selectedItems = [];
                $scope.isSelectAllChecked = false;
            };

            $scope.dynamicTypes = [];

            /*
             * Toggles the item into selected or unselected state
             * depending on it's current state.
            **/
            $scope.toggleSelect = function (id) {
                $scope.isSelected(id) ? unselectItem(id) : selectItem(id);
            };

            $scope.load = function (controlData, _pageSelector, _filterSelector, _listTemplateControl, _singleItemTemplateControl) {

                DynamicTypes.query(function (data) {
                    $scope.dynamicTypes = data;
                    $scope.contentTypesLoaded = true;
                });

                if (!pageSelector) {
                    pageSelector = _pageSelector;
                    pageSelector.add_doneClientSelection(pageSelected);
                }

                if (!filterSelector) {
                   filterSelector = _filterSelector;
                }

                if (!listTemplateControl) {
                    listTemplateControl = _listTemplateControl;
                }

                if (!singleItemTemplateControl) {
                    singleItemTemplateControl = _singleItemTemplateControl;
                }

                $scope.controlData = controlData;
                var masterDefinition = controlData.ControlDefinition.Views.DynamicContentMasterView;
                if (masterDefinition.AllowPaging) {
                    $scope.listMode = 'paging'
                    $scope.itemsPerPage = masterDefinition.ItemsPerPage;
                } else if (masterDefinition.ItemsPerPage !== 0) {
                    $scope.listMode = 'limit';
                    $scope.limitCount = masterDefinition.ItemsPerPage;
                } else {
                    $scope.listMode = 'none';
                    $scope.itemsPerPage = 20;
                    $scope.limitCount = 20;
                }

                if (controlData.SortMode !== 'None') {
                    $scope.sortMode = controlData.SortMode;
                } else {
                    $scope.sortMode = 'Manual';
                }

                if (masterDefinition.DetailsPageId && masterDefinition.DetailsPageId !== '00000000-0000-0000-0000-000000000000') {
                    Page.get({ id: masterDefinition.DetailsPageId }, function (data) {
                        $scope.detailsPage = {
                            Id: masterDefinition.DetailsPageId,
                            Title: data.Title
                        };
                    });
                    $scope.detailPageMode = 'custom';
                }

                $scope.controlDataLoaded = true;
            };

            /*
             * Checks whether the item with specified id
             * has been selected. Returns true if selected;
             * otherwise false.
            **/
            $scope.isSelected = function (id) {
                for (var i = 0; i < $scope.selectedItems.length; i++) {
                    if ($scope.selectedItems[i].Id === id) return true;
                }
                return false;
            };

            $scope.$watch('selectedDynamicType', function () {
                var typeId = $scope.selectedDynamicType.Id;
                if (!(typeId && typeId.length > 0)) return;

                $scope.selectedItems = [];

                DynamicContents.query({ id: typeId }, function (data) {
                    $scope.allItems = data.Items;
                    $scope.allItemsVirtualCount = data.VirtualCount;
                    initializeTemplateControls();
                    $scope.isSelectAllChecked = false;
                });

                // TODO: this is awful, but oh well...
                $('[data-filter-id]').each(function () {

                    var filterId = $(this).attr('data-filter-id');
                    if (filterId === 'date-selector') return;

                    if ($scope.selectedDynamicType.SupportedTaxonomyIds.indexOf(filterId) === -1) {
                        $(this).closest('li').hide();
                    } else {
                        $(this).closest('li').show();
                    }

                });

            }, true);

            $scope.$watch('mainFieldStartsWith', function (newValue, oldValue) {
                if (newValue !== oldValue) {

                    var typeId = $scope.selectedDynamicType.Id;

                    var queryData = filterSelector.get_queryData();
                    if (queryData.QueryItems.length > 0) {
                        queryData = JSON.stringify(queryData);
                    } else {
                        queryData = null;
                    }

                    DynamicContents.query({ id: typeId, queryData: queryData, mainFieldStartsWith: newValue }, function (data) {
                        $scope.allItems = data.Items;
                        $scope.allItemsVirtualCount = data.VirtualCount;
                    });
                }
            });

            $scope.$watch('[controlDataLoaded, contentTypesLoaded]', function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if ($scope.controlDataLoaded && $scope.contentTypesLoaded) {
                        loadDesignerData();
                    }
                }
            }, true);

            /**
             * Watch over the all items property. Following things will happen if
             * the allItemms collection changes:
             * <ul>
             *  <li>
             *      Pager will be reconfigured through configurePager()
             *  </li>
             * </ul>
             *
             * @param newValue the new value of the all items collection; used for comparison
             * @param oldValue the old value of the all items collection; used for comparison
             */
            $scope.$watch('allItems', function (newValue, oldValue) {
                configurePager();
            });

        }]);

Type.registerNamespace("Telerik.Sitefinity.OrderedContentWidget");

Telerik.Sitefinity.OrderedContentWidget.Designer = function (element) {
    Telerik.Sitefinity.OrderedContentWidget.Designer.initializeBase(this, [element]);
    this._listTemplateControl = null;
    this._singleItemTemplateControl = null;
    this._pageSelector = null;
    this._filterSelector = null;
};

Telerik.Sitefinity.OrderedContentWidget.Designer.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {

        $('#tabstrip').kendoTabStrip({
            animation: false,
            activate: function () {
                dialogBase.resizeToContent();
            }
        });
        $('#all-selected-tabstrip').kendoTabStrip({ animation: false });

        var controlData = this.get_controlData();

        this.get_listTemplateControl().set_parentDesigner(this);
        this.get_listTemplateControl().set_currentView(controlData.ControlDefinition.Views.DynamicContentMasterView);

        this.get_singleItemTemplateControl().set_parentDesigner(this);
        this.get_singleItemTemplateControl().set_currentView(controlData.ControlDefinition.Views.DynamicContentDetailView);

        this._filterSelector.set_queryData(new Telerik.Sitefinity.Web.UI.QueryData());

        Telerik.Sitefinity.OrderedContentWidget.Designer.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.OrderedContentWidget.Designer.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    // forces the designer to apply the changes on UI to the cotnrol data
    applyChanges: function () {
        var controlData = this.get_controlData();
       
        var ctrl = angular.element($("[ng-controller='DesignerCtrl']")).scope();
        controlData.DynamicContentTypeName = ctrl.selectedDynamicType.ClrType;
        
        var selectedItems = [];
        if (ctrl.selectedItems.length > 0) {
            controlData.SelectedItems = JSON.stringify(ctrl.selectedItems);
            for (i = 0; i < ctrl.selectedItems.length; i++) {
                selectedItems.push(ctrl.selectedItems[i].Id);
            }

            controlData.SelectedItems = JSON.stringify(selectedItems);
        } else {
            controlData.SelectedItems = [];
        }

        var masterDefinition = controlData.ControlDefinition.Views.DynamicContentMasterView;
        var detailDefinition = controlData.ControlDefinition.Views.DynamicContentDetailView;
        
        switch(ctrl.listMode) {
            case 'paging':
                masterDefinition.AllowPaging = true;
                masterDefinition.ItemsPerPage = ctrl.itemsPerPage;
                break;
            case 'limit':
                masterDefinition.AllowPaging = false;
                masterDefinition.ItemsPerPage = ctrl.limitCount;
                break;
            case 'none':
                masterDefinition.AllowPaging = false;
                masterDefinition.ItemsPerPage = 0;
                break;
        }

        masterDefinition.TemplateKey = this.get_listTemplateControl().get_currentView().TemplateKey;
        detailDefinition.TemplateKey = this.get_singleItemTemplateControl().get_currentView().TemplateKey;

        if (ctrl.detailPageMode === 'custom') {
            masterDefinition.DetailsPageId = ctrl.detailsPage.Id;
        } else {
            masterDefinition.DetailsPageId = null;
        }

        controlData.SortMode = ctrl.sortMode;
    },

    // forces the designer to refresh the UI from the control data
    refreshUI: function () {
        var controlData = this.get_controlData();
        var masterDefinition = controlData.ControlDefinition.Views.DynamicContentMasterView;
        var detailDefinition = controlData.ControlDefinition.Views.DynamicContentDetailView;

        this.get_listTemplateControl()._getFieldControl('TemplateKey').set_value(masterDefinition.TemplateKey);
        this.get_singleItemTemplateControl()._getFieldControl('TemplateKey').set_value(detailDefinition.TemplateKey);

        var ctrl = angular.element($("[ng-controller='DesignerCtrl']")).scope();
        ctrl.load(controlData, this.get_pageSelector(), this.get_filterSelector(), this.get_listTemplateControl(), this.get_singleItemTemplateControl());
    },

    get_listTemplateControl: function() {
        return this._listTemplateControl;
    },

    set_listTemplateControl: function(value) {
        this._listTemplateControl = value;
    },

    get_singleItemTemplateControl: function() {
        return this._singleItemTemplateControl;
    },
    
    set_singleItemTemplateControl: function(value) {
        this._singleItemTemplateControl = value;
    },

    get_pageSelector: function () {
        return this._pageSelector;
    },

    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_filterSelector: function () {
        return this._filterSelector;
    },

    set_filterSelector: function (value) {
        this._filterSelector = value;
    }

};

Telerik.Sitefinity.OrderedContentWidget.Designer.registerClass('Telerik.Sitefinity.OrderedContentWidget.Designer', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
