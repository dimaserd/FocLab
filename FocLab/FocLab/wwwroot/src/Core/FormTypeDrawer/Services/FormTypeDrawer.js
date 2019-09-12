var FormTypeDrawer = (function () {
    function FormTypeDrawer(formDrawer, typeDescription) {
        this._formDrawer = formDrawer;
        this._typeDescription = typeDescription;
    }
    FormTypeDrawer.prototype.BeforeFormDrawing = function () {
        this._formDrawer.BeforeFormDrawing();
    };
    FormTypeDrawer.prototype.AfterFormDrawing = function () {
        this._formDrawer.AfterFormDrawing();
    };
    FormTypeDrawer.prototype.TextBoxFor = function (propertyName, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextBox(prop, wrap);
    };
    FormTypeDrawer.prototype.DatePickerFor = function (propertyName, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDatePicker(prop, wrap);
    };
    FormTypeDrawer.prototype.TextAreaFor = function (propertyName, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextArea(prop, wrap);
    };
    FormTypeDrawer.prototype.DropDownFor = function (propertyName, selectList, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDropDownList(prop, selectList, wrap);
    };
    FormTypeDrawer.prototype.MultipleDropDownFor = function (propertyName, selectList, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderMultipleDropDownList(prop, selectList, wrap);
    };
    FormTypeDrawer.prototype.HiddenFor = function (propertyName, wrap) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderHidden(prop, wrap);
    };
    FormTypeDrawer.FindPropByName = function (type, propName) {
        for (var i = 0; i < type.Properties.length; i++) {
            var prop = type.Properties[i];
            if (prop.PropertyName == propName) {
                return prop;
            }
        }
        throw new Error("\u0421\u0432\u043E\u0439\u0441\u0442\u0432\u043E " + propName + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E");
    };
    return FormTypeDrawer;
}());
