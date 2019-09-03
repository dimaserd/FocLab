var FormDrawImplementation = /** @class */ (function () {
    function FormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
    }
    FormDrawImplementation.prototype.BeforeFormDrawing = function () {
        //TODO Init calendar or some scripts
    };
    FormDrawImplementation.prototype.AfterFormDrawing = function () {
        //Красивые селекты
        $("." + this._selectClass).selectpicker('refresh');
        //Инициация календарей
        for (var i = 0; i < this._datePickerPropNames.length; i++) {
            var datePickerPropName = this._datePickerPropNames[i];
            var propName = "" + this._model.Prefix + datePickerPropName;
            FormDrawImplementation.InitCalendarForPrefixedProperty(propName);
        }
    };
    FormDrawImplementation.InitCalendarForPrefixedProperty = function (prefixedPropName) {
        Utils.SetDatePicker("input[name='" + prefixedPropName + "']");
    };
    FormDrawImplementation.prototype.GetPropertyValueName = function (typeDescription) {
        return "" + this._model.Prefix + typeDescription.PropertyName;
    };
    FormDrawImplementation.prototype.RenderDatePicker = function (typeDescription) {
        this._datePickerPropNames.push(typeDescription.PropertyName);
        return this.RenderTextBox(typeDescription);
    };
    FormDrawImplementation.prototype.RenderHidden = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<input type=\"hidden\" name=\"" + this.GetPropertyValueName(typeDescription) + "\" value=\"" + value + "\">";
    };
    FormDrawImplementation.prototype.RenderTextBox = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var t = "<div class=\"form-group m-form__group\">\n                <label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                <input autocomplete=\"false\" class=\"form-control m-input\" name=\"" + this.GetPropertyValueName(typeDescription) + "\" type=\"text\" value=\"" + value + "\">\n            </div>";
        return t;
    };
    FormDrawImplementation.prototype.RenderTextArea = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var styles = "style=\"margin-top: 0px; margin-bottom: 0px; height: 79px;\"";
        var t = "<div class=\"form-group m-form__group\">\n                <label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                <textarea autocomplete=\"false\" class=\"form-control m-input\" name=\"" + this.GetPropertyValueName(typeDescription) + "\" rows=\"3\" " + styles + ">" + value + "</textarea>\n            </div>";
        return t;
    };
    FormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription), selectList);
        return "<div class=\"form-group m-form__group\">\n                <label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                " + select + "   \n            </div>";
    };
    return FormDrawImplementation;
}());
