var designerApp = angular.module('DesignerApp', ['ngResource', 'ui.sortable']);

designerApp.factory('DynamicTypes', ['$resource',
    function ($resource) {
        return $resource('/restapi/fixeddynamiccontent/dynamictypes', {}, {
            query: { method: 'GET', isArray: true }
        });
    }
]);

designerApp.factory('DynamicContents', ['$resource',
    function ($resource) {
        return $resource('/restapi/fixeddynamiccontent/dynamiccontents/:id', {}, {
            query: { method: 'GET', isArray: false }
        });
    }
]);

designerApp.factory('Page', ['$resource',
    function ($resource) {
        return $resource('/restapi/fixeddynamiccontent/page/:id', {}, {
            get: { method: 'GET' }
        });
    }
]);


var designerCtrl = designerApp.controller('DesignerCtrl', ['$scope', 'DynamicTypes', 'DynamicContents', 'Page', 
    function ($scope, DynamicTypes, DynamicContents, Page) {

        var pageSelector = null,
            filterSelector = null;

        $scope.controlData = {};
        $scope.controlDataLoaded = false;
        $scope.contentTypesLoaded = false;

        // Array with all the items that are currently visible in the selector
        $scope.allItems = [];

        /*
         * The virtual count of all the items that could be displayed given the current
         * filter expressions.
        **/
        $scope.allItemsVirtualCount = 0;

        // Array with all the items that have been selected
        $scope.selectedItems = [];

        /*
         * The virtual count of all the items that have been selected and could be displayed
         * given the paging constraints.
         *
        **/
        $scope.selectedItemsVirtualCount = 0;

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

        $scope.showFilters = false;

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

        /*
         * Finds the index of an item for a given id
         * within a specified collection.
        **/
        var findIndex = function (comparer, collection) {
            for (var i = 0; i < collection.length; i++) {
                if(comparer(collection[i])) {
                    return i;
                }
            }
            return -1;
        };

        var selectType = function (clrType) {
            if (clrType) {
                $scope.selectedDynamicType = $scope.dynamicTypes[findIndex(function(item) {
                    return item.ClrType === clrType;
                }, $scope.dynamicTypes)];
            }
        };

        var loadDesignerData = function () {
            selectType($scope.controlData.DynamicContentTypeName);
            if ($scope.selectedDynamicType.Id) {
                DynamicContents.query({ id: $scope.selectedDynamicType.Id, SelectedContentIds: $scope.controlData.SelectedItems }, function (data) {
                    $scope.selectedItems = data.Items;
                    $scope.selectedItemsVirtualCount = data.VirtualCount;
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
            $scope.selectedItemsVirtualCount++;
        };

        // Unselects an item
        var unselectItem = function (id) {
            $scope.selectedItems.splice(findIndex(function (item) {
                return item.Id === id;
            }, $scope.selectedItems), 1);
            $scope.selectedItemsVirtualCount--;
        };

        var pageSelected = function (page) {
            $scope.$apply(function () {
                $scope.detailsPage = page;
                $scope.showPageSelector = false;
            });
        }

        $scope.selectedDynamicType = "";

        $scope.dynamicTypes = DynamicTypes.query(function () {
            $scope.contentTypesLoaded = true;
        });

        /*
         * Toggles the item into selected or unselected state
         * depending on it's current state.
        **/
        $scope.toggleSelect = function (id) {
            $scope.isSelected(id) ? unselectItem(id) : selectItem(id);
        };

        $scope.load = function (controlData, _pageSelector, _filterSelector) {

            if (!pageSelector) {
                pageSelector = _pageSelector;
                pageSelector.add_doneClientSelection(pageSelected);
            }

            if (!filterSelector) {
                filterSelector = _filterSelector;
            }

            $scope.controlData = controlData;
            var masterDefinition = controlData.ControlDefinition.Views.DynamicContentMasterView;
            if(masterDefinition.AllowPaging) {
                $scope.listMode = 'paging'
                $scope.itemsPerPage = masterDefinition.ItemsPerPage;
            } else if(masterDefinition.ItemsPerPage !== 0) {
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
                        Id : masterDefinition.DetailsPageId,
                        Title : data.Title
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

            DynamicContents.query({ id: typeId }, function (data) {
                $scope.allItems = data.Items;
                $scope.allItemsVirtualCount = data.VirtualCount;
            });

        }, true);

        $scope.$watch('[controlDataLoaded, contentTypesLoaded]', function (newValue, oldValue) {
            if (newValue !== oldValue) {
                if ($scope.controlDataLoaded && $scope.contentTypesLoaded) {
                    loadDesignerData();
                }
            }
        }, true);
}]);

Type.registerNamespace("Telerik.Sitefinity.FixedDynamicContentWidget");

Telerik.Sitefinity.FixedDynamicContentWidget.Designer = function (element) {
    Telerik.Sitefinity.FixedDynamicContentWidget.Designer.initializeBase(this, [element]);
    this._listTemplateControl = null;
    this._singleItemTemplateControl = null;
    this._pageSelector = null;
    this._filterSelector = null;
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {

        $('#tabstrip').kendoTabStrip();
        $('#all-selected-tabstrip').kendoTabStrip();

        var controlData = this.get_controlData();

        this.get_listTemplateControl().set_parentDesigner(this);
        this.get_listTemplateControl().set_currentView(controlData.ControlDefinition.Views.DynamicContentMasterView);

        this.get_singleItemTemplateControl().set_parentDesigner(this);
        this.get_singleItemTemplateControl().set_currentView(controlData.ControlDefinition.Views.DynamicContentDetailView);

        this._filterSelector.set_queryData(new Telerik.Sitefinity.Web.UI.QueryData());

        Telerik.Sitefinity.FixedDynamicContentWidget.Designer.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.FixedDynamicContentWidget.Designer.callBaseMethod(this, 'dispose');
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
        ctrl.load(controlData, this.get_pageSelector(), this.get_filterSelector());
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

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.registerClass('Telerik.Sitefinity.FixedDynamicContentWidget.Designer', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
