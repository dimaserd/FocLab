var TabFormDrawImplementation = (function () {
    function TabFormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
        this._drawHelper = new HtmlSelectDrawHelper(CrocoAppCore.Application.FormDataHelper.NullValue);
    }
    TabFormDrawImplementation.prototype.BeforeFormDrawing = function () {
    };
    TabFormDrawImplementation.prototype.AfterFormDrawing = function () {
        $("." + this._selectClass).selectpicker('refresh');
        for (var i = 0; i < this._datePickerPropNames.length; i++) {
            var datePickerPropName = this._datePickerPropNames[i];
            FormDrawImplementation.InitCalendarForPrefixedProperty(this._model.Prefix, datePickerPropName);
        }
    };
    TabFormDrawImplementation.prototype.RenderTextBox = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">\n                        " + typeDescription.PropertyDescription.PropertyDisplayName + ":\n                    </label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        <div class=\"input-group\">\n                            <input type=\"text\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyDescription.PropertyName) + "\" class=\"form-control m-input\" placeholder=\"\" value=\"" + value + "\">\n                        </div>\n                    </div>\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderTextArea = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">" + typeDescription.PropertyDescription.PropertyDisplayName + "</label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        <textarea class=\"form-control m-input\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyDescription.PropertyName) + "\" rows=\"3\">" + value + "</textarea>\n                    </div>\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderGenericDropList = function (typeDescription, selectList, isMultiple) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        this._drawHelper.ProccessSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var dict = isMultiple ? new Map().set("multiple", "") : null;
        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription.PropertyDescription.PropertyName), selectList, dict);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">" + typeDescription.PropertyDescription.PropertyDisplayName + ":</label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        " + select + "\n                    </div>\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList) {
        return this.RenderGenericDropList(typeDescription, selectList, false);
    };
    TabFormDrawImplementation.prototype.RenderMultipleDropDownList = function (typeDescription, selectList) {
        return this.RenderGenericDropList(typeDescription, selectList, true);
    };
    TabFormDrawImplementation.prototype.RenderHidden = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<input type=\"hidden\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyDescription.PropertyName) + "\" value=\"" + value + "\">";
    };
    TabFormDrawImplementation.prototype.RenderDatePicker = function (typeDescription) {
        this._datePickerPropNames.push(typeDescription.PropertyDescription.PropertyName);
        return this.RenderTextBox(typeDescription);
    };
    TabFormDrawImplementation.prototype.GetPropertyValueName = function (propName) {
        return "" + this._model.Prefix + propName;
    };
    return TabFormDrawImplementation;
}());
