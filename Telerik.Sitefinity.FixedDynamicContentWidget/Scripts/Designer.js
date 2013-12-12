﻿var designerApp = angular.module('DesignerApp', ['ngResource', 'ui.sortable']);

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

var designerCtrl = designerApp.controller('DesignerCtrl', ['$scope', 'DynamicTypes', 'DynamicContents', 
    function ($scope, DynamicTypes, DynamicContents) {

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
            DynamicContents.query({ id : $scope.selectedDynamicType.Id,  SelectedContentIds: $scope.controlData.SelectedItems }, function(data) {
                $scope.selectedItems = data.Items;
                $scope.selectedItemsVirtualCount = data.VirtualCount;
            });
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

        $scope.load = function (controlData) {
            $scope.controlData = controlData;
            var masterDefinition = controlData.ControlDefinition.Views.DynamicContentMasterView;
            if(masterDefinition.AllowPaging) {
                $scope.listMode = 'paging'
            } else if(masterDefinition.UseLimit) {
                $scope.listMode = 'limit';
            } else {
                $scope.listMode = 'none';
            }

            $scope.itemsPerPage = masterDefinition.ItemsPerPage;
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
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {

        $('#tabstrip').kendoTabStrip();
        $('#all-selected-tabstrip').kendoTabStrip();

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
        if (ctrl.listMode == 'paging') {
            masterDefinition.AllowPaging = true;
            masterDefinition.UseLimit = false;
        } else if (ctrl.listMode == 'limit') {
            masterDefinition.AllowPaging = false;
            masterDefinition.UseLimit = true;
        } else {
            masterDefinition.AllowPaging = false;
            masterDefinition.UseLimit = false;
        }

        masterDefinition.ItemsPerPage = ctrl.itemsPerPage;
    },

    // forces the designer to refresh the UI from the control data
    refreshUI: function () {
        var controlData = this.get_controlData();

        var ctrl = angular.element($("[ng-controller='DesignerCtrl']")).scope();
        ctrl.load(controlData);
    }
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.registerClass('Telerik.Sitefinity.FixedDynamicContentWidget.Designer', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
