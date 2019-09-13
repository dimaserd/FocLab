var FormDrawImplementation = (function () {
    function FormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
    }
    FormDrawImplementation.prototype.BeforeFormDrawing = function () {
    };
    FormDrawImplementation.prototype.AfterFormDrawing = function () {
        $("." + this._selectClass).selectpicker('refresh');
        for (var i = 0; i < this._datePickerPropNames.length; i++) {
            var datePickerPropName = this._datePickerPropNames[i];
            var propName = "" + this._model.Prefix + datePickerPropName;
            FormDrawImplementation.InitCalendarForPrefixedProperty(propName);
        }
    };
    FormDrawImplementation.InitCalendarForPrefixedProperty = function (prefixedPropName) {
        Utils.SetDatePicker("input[name='" + prefixedPropName + "']");
    };
    FormDrawImplementation.prototype.RenderDatePicker = function (typeDescription, wrap) {
        this._datePickerPropNames.push(typeDescription.PropertyName);
        return this.RenderTextBox(typeDescription, wrap);
    };
    FormDrawImplementation.prototype.RenderHidden = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var html = "<input type=\"hidden\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" value=\"" + value + "\">";
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + "\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderTextBox = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                <input autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" type=\"text\" value=\"" + value + "\">";
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderTextArea = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var styles = "style=\"margin-top: 0px; margin-bottom: 0px; height: 79px;\"";
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n            <textarea autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" rows=\"3\" " + styles + ">" + value + "</textarea>";
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                      " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderGenericDropList = function (typeDescription, selectList, isMultiple, wrap) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var dict = isMultiple ? new Dictionary([{ key: "multiple", value: "" }]) : null;
        var select = HtmlDrawHelper.RenderSelect(_class, FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix), selectList, dict);
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>" + select;
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n            </div>";
    };
    FormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, false, wrap);
    };
    FormDrawImplementation.prototype.RenderMultipleDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, true, wrap);
    };
    return FormDrawImplementation;
}());
