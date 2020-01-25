var FormDrawImplementation = (function () {
    function FormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
        this._htmlDrawHelper = new HtmlSelectDrawHelper(CrocoAppCore.Application.FormDataHelper.NullValue);
    }
    FormDrawImplementation.prototype.BeforeFormDrawing = function () {
    };
    FormDrawImplementation.prototype.AfterFormDrawing = function () {
        $("." + this._selectClass).selectpicker('refresh');
        for (var i = 0; i < this._datePickerPropNames.length; i++) {
            var datePickerPropName = this._datePickerPropNames[i];
            FormDrawImplementation.InitCalendarForPrefixedProperty(this._model.Prefix, datePickerPropName);
        }
    };
    FormDrawImplementation.prototype.GetPropertyName = function (propName) {
        return FormDrawHelper.GetPropertyValueName(propName, this._model.Prefix);
    };
    FormDrawImplementation.prototype.GetPropertyBlock = function (propertyName) {
        return this._model.Blocks.find(function (x) { return x.PropertyName === propertyName; });
    };
    FormDrawImplementation.GetElementIdForFakeCalendar = function (modelPrefix, propName) {
        var result = modelPrefix + "_" + propName + "FakeCalendar";
        return result.replace(new RegExp(/\./, 'g'), '_');
    };
    FormDrawImplementation.GetElementIdForRealCalendarBackProperty = function (modelPrefix, propName) {
        var result = modelPrefix + "_" + propName + "RealCalendar";
        return result.replace(new RegExp(/\./, 'g'), '_');
    };
    FormDrawImplementation.InitCalendarForPrefixedProperty = function (modelPrefix, propName) {
        var calandarElementId = FormDrawImplementation.GetElementIdForFakeCalendar(modelPrefix, propName);
        var backPropElementId = FormDrawImplementation.GetElementIdForRealCalendarBackProperty(modelPrefix, propName);
        DatePickerUtils.SetDatePicker(calandarElementId, backPropElementId);
    };
    FormDrawImplementation.prototype.GetPropValue = function (typeDescription) {
        return ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
    };
    FormDrawImplementation.prototype.RenderDatePicker = function (typeDescription, wrap) {
        var propName = typeDescription.PropertyDescription.PropertyName;
        var renderPropName = this.GetPropertyName(propName);
        this._datePickerPropNames.push(propName);
        var id = FormDrawImplementation.GetElementIdForFakeCalendar(this._model.Prefix, propName);
        var hiddenProps = new Map()
            .set("id", FormDrawImplementation.GetElementIdForRealCalendarBackProperty(this._model.Prefix, propName))
            .set(CrocoAppCore.Application.FormDataHelper.DataTypeAttributeName, CSharpType.String.toString());
        return this.RenderTextBoxInner(typeDescription, wrap, id, renderPropName + "Fake")
            + FormDrawHelper.GetInputTypeHidden(renderPropName, "", hiddenProps);
    };
    FormDrawImplementation.prototype.RenderHidden = function (typeDescription, wrap) {
        var value = this.GetPropValue(typeDescription);
        var html = FormDrawHelper.GetInputTypeHidden(FormDrawHelper.GetPropertyValueName(typeDescription.PropertyDescription.PropertyName, this._model.Prefix), value, null);
        return html;
    };
    FormDrawImplementation.prototype.RenderTextBox = function (typeDescription, wrap) {
        return this.RenderTextBoxInner(typeDescription, wrap, null, this.GetPropertyName(typeDescription.PropertyDescription.PropertyName));
    };
    FormDrawImplementation.prototype.RenderTextBoxInner = function (typeDescription, wrap, id, propName) {
        var _a;
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var idAttr = id == null ? "" : " id=\"" + id + "\"";
        var propBlock = this.GetPropertyBlock(typeDescription.PropertyDescription.PropertyName);
        var cSharpType = ((_a = propBlock.TextBoxData) === null || _a === void 0 ? void 0 : _a.IsInteger) ? CSharpType.Int : CSharpType.String;
        var dataTypeAttr = CrocoAppCore.Application.FormDataHelper.DataTypeAttributeName + "=" + cSharpType.toString();
        var typeAndStep = propBlock.TextBoxData.IsInteger ? "type=\"number\" step=\"" + propBlock.TextBoxData.IntStep + "\"" : "type=\"text\"";
        var html = "<label for=\"" + typeDescription.PropertyDescription.PropertyName + "\">" + propBlock.LabelText + "</label>\n                <input" + idAttr + " autocomplete=\"off\" class=\"form-control m-input\" name=\"" + propName + "\" " + dataTypeAttr + " " + typeAndStep + " value=\"" + value + "\" />";
        if (!wrap) {
            return html;
        }
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.RenderTextArea = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var styles = "style=\"margin-top: 0px; margin-bottom: 0px; height: 79px;\"";
        var propBlock = this.GetPropertyBlock(typeDescription.PropertyDescription.PropertyName);
        var html = "<label for=\"" + typeDescription.PropertyDescription.PropertyName + "\">" + propBlock.LabelText + "</label>\n            <textarea autocomplete=\"off\" class=\"form-control m-input\" name=\"" + this.GetPropertyName(typeDescription.PropertyDescription.PropertyName) + "\" rows=\"3\" " + styles + ">" + value + "</textarea>";
        if (!wrap) {
            return html;
        }
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.RenderGenericDropList = function (typeDescription, selectList, isMultiple, wrap) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        this._htmlDrawHelper.ProccessSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var dict = isMultiple ? new Map().set("multiple", "") : null;
        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyName(typeDescription.PropertyDescription.PropertyName), selectList, dict);
        var propBlock = this.GetPropertyBlock(typeDescription.PropertyDescription.PropertyName);
        var html = "<label for=\"" + typeDescription.PropertyDescription.PropertyName + "\">" + propBlock.LabelText + "</label>" + select;
        if (!wrap) {
            return html;
        }
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.WrapInForm = function (prop, html) {
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(prop.PropertyDescription.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, false, wrap);
    };
    FormDrawImplementation.prototype.RenderMultipleDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, true, wrap);
    };
    return FormDrawImplementation;
}());
