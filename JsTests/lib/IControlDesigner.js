/// <reference name="MicrosoftAjax.js"/>
/// <reference path="jquery-1.3.2-vsdoc2.js" />
Type._registerScript("IControlDesigner.js");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");


Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner = function() { };
Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner.Prototype = {
    /// <summary>
    /// used for the base control designers (news/blogs/etc), not the views
    /// <summary>
    
    // gets the reference to the propertyEditor control
    get_propertyEditor: function() { },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function(value) { },
    ///get the control data to be visualized and edited by the designer
    get_controlData: function() { 
    },
    ///forces the designer to refresh the UI from the cotnrol Data
    refreshUI: function() { },
    ///forces the designer to apply the changes on UI to the cotnrol Data
    applyChanges: function() {}

};

Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner.registerInterface("Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner");