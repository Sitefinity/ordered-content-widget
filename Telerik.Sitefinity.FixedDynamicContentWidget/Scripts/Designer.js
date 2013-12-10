var designerApp = angular.module('DesignerApp', ['ngResource']);

designerApp.factory('DynamicTypes', ['$resource',
    function ($resource) {
        return $resource('/restapi/fixeddynamiccontent/dynamictypes', {}, {
            query: { method: 'GET', isArray: true }
        });
    }
]);

designerApp.controller('DesignerCtrl', ['$scope', 'DynamicTypes', function ($scope, DynamicTypes) {
    
    $scope.selectedDynamicType = "";

    $scope.dynamicTypes = DynamicTypes.query();
    
}]);

Type.registerNamespace("Telerik.Sitefinity.FixedDynamicContentWidget");

Telerik.Sitefinity.FixedDynamicContentWidget.Designer = function (element) {
    Telerik.Sitefinity.FixedDynamicContentWidget.Designer.initializeBase(this, [element]);
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {

        $('#tabstrip').kendoTabStrip();

        Telerik.Sitefinity.FixedDynamicContentWidget.Designer.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.FixedDynamicContentWidget.Designer.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    // forces the designer to apply the changes on UI to the cotnrol data
    applyChanges: function () {
        var controlData = this.get_controlData();

        //controlData.MaxItems = this.get_MaxItems().get_value();

        //controlData.TemplateKey = this.get_templateSelector().get_value();

    },

    // forces the designer to refresh the UI from the cotnrol data
    refreshUI: function () {
        var controlData = this.get_controlData();

        //if (controlData.MaxItems != 'undefined')
        //    this.get_MaxItems().set_value(controlData.MaxItems);

        //if (controlData.TemplateKey) {
        //    this.get_templateSelector().set_value(controlData.TemplateKey);
        //}
    }
};

Telerik.Sitefinity.FixedDynamicContentWidget.Designer.registerClass('Telerik.Sitefinity.FixedDynamicContentWidget.Designer', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
