/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("ControlDesignerBase.js", ["IControlDesigner.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.initializeBase(this, [element]);
    this._propertyEditor = null;
    this._loadDelegate = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.callBaseMethod(this, 'initialize');

        this._loadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.callBaseMethod(this, 'dispose');
    },

    /* ----------------------------- public methods ----------------------------- */

    // forces the designer to refresh the UI from the cotnrol Data
    refreshUI: function () {
        alert('This function has to be implemented on the concrete implementation of this class.');
    },

    // forces the designer to apply the changes on UI to the cotnrol Data
    applyChanges: function () {
        alert('This function has to be implemented on the concrete implementation of this class.');
    },

    /* ----------------------------- event handlers ----------------------------- */

    // Called when page has loaded with all of its components. At this moment property
    // editor already has the control data.
    _onLoad: function () {
        this.refreshUI();
    },

    /* ----------------------------- private methods ----------------------------- */


    /* ----------------------------- properties ----------------------------- */

    // gets the reference to the property editor cliend side component
    get_propertyEditor: function () {
        return this._propertyEditor;
    },

    // gets the reference to the property editor cliend side component
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    // gets the client side object which represents the control that we are designing
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    }
}

Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
