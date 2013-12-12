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

var designerCtrl = designerApp.controller('DesignerCtrl', ['$scope', 'DynamicTypes', 'DynamicContents', 
    function ($scope, DynamicTypes, DynamicContents) {

        $scope.controlData = {};
        $scope.controlDataLoaded = false;
        $scope.contentTypesLoaded = false;

        $scope.allItems = [];
        $scope.allItemsVirtualCount = 0;

        $scope.selectedItems = [];
        $scope.selectedItemsVirtualCount = 0;

        var findType = function (clrType) {
            for (var i = 0; i < $scope.dynamicTypes.length; i++) {
                if ($scope.dynamicTypes[i].ClrType === clrType) {
                    return $scope.dynamicTypes[i];
                }
            }
        };

        var findItem = function (id) {
            for (var i = 0; i < $scope.allItems.length; i++) {
                if ($scope.allItems[i].Id === id) {
                    return $scope.allItems[i];
                }
            }
        };

        var selectType = function (clrType) {
            if (clrType) {
                $scope.selectedDynamicType = findType(clrType);
            }
        };

        var loadDesignerData = function () {
            selectType($scope.controlData.DynamicContentTypeName);
            DynamicContents.query({ id : $scope.selectedDynamicType.Id,  SelectedContentIds: $scope.controlData.SelectedItems }, function(data) {
                $scope.selectedItems = data.Items;
                $scope.selectedItemsVirtualCount = data.VirtualCount;
            });
        };

        $scope.selectedDynamicType = "";

        $scope.dynamicTypes = DynamicTypes.query(function () {
            $scope.contentTypesLoaded = true;
        });

        $scope.toggleSelect = function (id) {
            var item = findItem(id);
            $scope.selectedItems.push(item);
            $scope.selectedItemsVirtualCount++;
        };

        $scope.load = function (controlData) {
            $scope.controlData = controlData;
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
    },

    // forces the designer to refresh the UI from the control data
    refreshUI: function () {
        var controlData = this.get_controlData();

        var ctrl = angular.element($("[ng-controller='DesignerCtrl']")).scope();
        //ctrl.$apply(function () {
            ctrl.load(controlData);
        //});
    }
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.registerClass('Telerik.Sitefinity.FixedDynamicContentWidget.Designer', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
