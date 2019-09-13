var CSharpType;
(function (CSharpType) {
    CSharpType[CSharpType["String"] = "String"] = "String";
    CSharpType[CSharpType["Int"] = "Int32"] = "Int";
    CSharpType[CSharpType["Decimal"] = "Decimal"] = "Decimal";
    CSharpType[CSharpType["Boolean"] = "Boolean"] = "Boolean";
    CSharpType[CSharpType["DateTime"] = "DateTime"] = "DateTime";
})(CSharpType || (CSharpType = {}));

var UserInterfaceType;
(function (UserInterfaceType) {
    UserInterfaceType[UserInterfaceType["TextBox"] = "TextBox"] = "TextBox";
    UserInterfaceType[UserInterfaceType["TextArea"] = "TextArea"] = "TextArea";
    UserInterfaceType[UserInterfaceType["DropDownList"] = "DropDownList"] = "DropDownList";
    UserInterfaceType[UserInterfaceType["Hidden"] = "Hidden"] = "Hidden";
    UserInterfaceType[UserInterfaceType["DatePicker"] = "DatePicker"] = "DatePicker";
    UserInterfaceType[UserInterfaceType["MultipleDropDownList"] = "MultipleDropDownList"] = "MultipleDropDownList";
})(UserInterfaceType || (UserInterfaceType = {}));

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
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.RenderTextBox = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                <input autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" type=\"text\" value=\"" + value + "\">";
        if (!wrap) {
            return html;
        }
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.RenderTextArea = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var styles = "style=\"margin-top: 0px; margin-bottom: 0px; height: 79px;\"";
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n            <textarea autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" rows=\"3\" " + styles + ">" + value + "</textarea>";
        if (!wrap) {
            return html;
        }
        return this.WrapInForm(typeDescription, html);
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
        return this.WrapInForm(typeDescription, html);
    };
    FormDrawImplementation.prototype.WrapInForm = function (prop, html) {
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(prop.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, false, wrap);
    };
    FormDrawImplementation.prototype.RenderMultipleDropDownList = function (typeDescription, selectList, wrap) {
        return this.RenderGenericDropList(typeDescription, selectList, true, wrap);
    };
    return FormDrawImplementation;
}());

var TabFormDrawImplementation = (function () {
    function TabFormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
    }
    TabFormDrawImplementation.prototype.BeforeFormDrawing = function () {
    };
    TabFormDrawImplementation.prototype.AfterFormDrawing = function () {
        $("." + this._selectClass).selectpicker('refresh');
        for (var i = 0; i < this._datePickerPropNames.length; i++) {
            var datePickerPropName = this._datePickerPropNames[i];
            var propName = "" + this._model.Prefix + datePickerPropName;
            FormDrawImplementation.InitCalendarForPrefixedProperty(propName);
        }
    };
    TabFormDrawImplementation.prototype.RenderTextBox = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">\n                        " + typeDescription.PropertyDisplayName + ":\n                    </label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        <div class=\"input-group\">\n                            <input type=\"text\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyName) + "\" class=\"form-control m-input\" placeholder=\"\" value=\"" + value + "\">\n                        </div>\n                    </div>\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderTextArea = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">" + typeDescription.PropertyDisplayName + "</label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        <textarea class=\"form-control m-input\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyName) + "\" rows=\"3\">" + value + "</textarea>\n                    </div>\n\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderGenericDropList = function (typeDescription, selectList, isMultiple) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var dict = isMultiple ? new Dictionary([{ key: "multiple", value: "" }]) : null;
        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription.PropertyName), selectList, dict);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">" + typeDescription.PropertyDisplayName + ":</label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        " + select + "\n                    </div>\n                </div>";
    };
    TabFormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList) {
        return this.RenderGenericDropList(typeDescription, selectList, false);
    };
    TabFormDrawImplementation.prototype.RenderMultipleDropDownList = function (typeDescription, selectList) {
        return this.RenderGenericDropList(typeDescription, selectList, true);
    };
    TabFormDrawImplementation.prototype.RenderHidden = function (typeDescription) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        return "<input type=\"hidden\" name=\"" + this.GetPropertyValueName(typeDescription.PropertyName) + "\" value=\"" + value + "\">";
    };
    TabFormDrawImplementation.prototype.RenderDatePicker = function (typeDescription) {
        this._datePickerPropNames.push(typeDescription.PropertyName);
        return this.RenderTextBox(typeDescription);
    };
    TabFormDrawImplementation.prototype.GetPropertyValueName = function (propName) {
        return "" + this._model.Prefix + propName;
    };
    return TabFormDrawImplementation;
}());





var FormDrawFactory = (function () {
    function FormDrawFactory() {
    }
    FormDrawFactory.GetImplementation = function (buildModel, key) {
        var func = FormDrawFactory.DictionaryImplementations.getByKey(key);
        if (func == null) {
            return new FormDrawImplementation(buildModel);
        }
        return func(buildModel);
    };
    FormDrawFactory.DictionaryImplementations = new Dictionary([
        { key: "Default", value: function (x) { return new FormDrawImplementation(x); } },
        { key: "Tab", value: function (x) { return new TabFormDrawImplementation(x); } }
    ]);
    return FormDrawFactory;
}());

var FormDrawHelper = (function () {
    function FormDrawHelper() {
    }
    FormDrawHelper.GetPropertyValueName = function (propertyName, modelPrefix) {
        return "" + modelPrefix + propertyName;
    };
    FormDrawHelper.GetPropertySelector = function (propertyName, modelPrefix) {
        var prefixedPropName = FormDrawHelper.GetPropertyValueName(propertyName, modelPrefix);
        return "input[name='" + prefixedPropName + "']";
    };
    FormDrawHelper.GetOuterFormElement = function (propertyName, modelPrefix) {
        return document.querySelector("[" + FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\"][" + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"]");
    };
    FormDrawHelper.GetOuterFormAttributes = function (propertyName, modelPrefix) {
        return FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\" " + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"";
    };
    FormDrawHelper.FormPropertyName = "form-property-name";
    FormDrawHelper.FormModelPrefix = "form-model-prefix";
    return FormDrawHelper;
}());

var HtmlDrawHelper = (function () {
    function HtmlDrawHelper() {
    }
    HtmlDrawHelper.RenderAttributesString = function (attrs) {
        var result = "";
        if (attrs == null) {
            return result;
        }
        for (var i = 0; i < attrs._keys.length; i++) {
            var key = attrs._keys[i];
            var res = attrs.getByKey(key);
            if (res == null || res === "") {
                result += " " + key;
            }
            else {
                result += " " + key + "=\"" + res + "\"";
            }
        }
        return result;
    };
    HtmlDrawHelper.RenderSelect = function (className, propName, selectList, attrs) {
        var attrStr = HtmlDrawHelper.RenderAttributesString(attrs);
        console.log("RenderSelect", attrStr);
        var select = "<select" + attrStr + " class=\"" + className + "\" name=\"" + propName + "\">";
        for (var i = 0; i < selectList.length; i++) {
            var item = selectList[i];
            var selected = item.Selected ? " selected=\"selected\"" : '';
            select += "<option" + selected + " value=\"" + item.Value + "\">" + item.Text + "</option>";
        }
        select += "</select>";
        return select;
    };
    HtmlDrawHelper.ProceesSelectValues = function (typeDescription, rawValue, selectList) {
        if (rawValue != null) {
            selectList.forEach(function (x) { return x.Selected = false; });
            var item = typeDescription.TypeName == CSharpType.Boolean.toString() ?
                selectList.find(function (x) { return x.Value.toLowerCase() == rawValue.toLowerCase(); }) :
                selectList.find(function (x) { return x.Value == rawValue; });
            if (item != null) {
                item.Selected = true;
            }
        }
        return selectList;
    };
    return HtmlDrawHelper;
}());

var ValueProviderHelper = (function () {
    function ValueProviderHelper() {
    }
    ValueProviderHelper.GetStringValueFromValueProvider = function (prop, valueProvider) {
        var value = ValueProviderHelper.GetRawValueFromValueProvider(prop, valueProvider);
        return value == null ? "" : value;
    };
    ValueProviderHelper.GetRawValueFromValueProvider = function (prop, valueProvider) {
        if (valueProvider == null) {
            return null;
        }
        if (!prop.IsEnumerable) {
            var value = valueProvider.Singles.find(function (x) { return x.PropertyName == prop.PropertyName; });
            return (value == null) ? null : value.Value;
        }
        return "";
    };
    return ValueProviderHelper;
}());

var FormTypeAfterDrawnDrawer = (function () {
    function FormTypeAfterDrawnDrawer() {
    }
    FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty = function (propertyName, modelPrefix, innerHtml) {
        var elem = FormDrawHelper.GetOuterFormElement(propertyName, modelPrefix);
        console.log("SetInnerHtmlForProperty elem", elem);
        elem.innerHTML = innerHtml;
    };
    FormTypeAfterDrawnDrawer.SetSelectListForProperty = function (propertyName, modelPrefix, selectList) {
        var t = TryForm._genericInterfaces.find(function (x) { return x.Prefix === modelPrefix; });
        var prop = t.TypeDescription.Properties.find(function (x) { return x.PropertyName === propertyName; });
        var drawer = new FormDrawImplementation(t);
        var html = drawer.RenderDropDownList(prop, selectList, false);
        FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);
        drawer.AfterFormDrawing();
    };
    return FormTypeAfterDrawnDrawer;
}());

var FormTypeDataGetter = (function () {
    function FormTypeDataGetter(data) {
        if (!data.IsClass) {
            var mes = "Тип не являющийся классом не поддерживается";
            alert(mes);
            throw Error(mes);
        }
        this._typeDescription = data;
    }
    FormTypeDataGetter.prototype.BuildObject = function () {
        var data = {};
        for (var i = 0; i < this._typeDescription.Properties.length; i++) {
            var prop = this._typeDescription.Properties[i];
            data[prop.PropertyName] = "";
        }
        return data;
    };
    FormTypeDataGetter.prototype.GetData = function (modelPrefix) {
        var initData = FormDataHelper.CollectDataByPrefix(this.BuildObject(), modelPrefix);
        for (var i = 0; i < this._typeDescription.Properties.length; i++) {
            var prop = this._typeDescription.Properties[i];
            switch (prop.TypeName) {
                case CSharpType.Decimal.toString():
                    initData[prop.PropertyName] = Number(initData[prop.PropertyName].replace(/,/g, '.'));
                    break;
                case CSharpType.Boolean.toString():
                    initData[prop.PropertyName] = initData[prop.PropertyName].toLowerCase() === "true";
                    break;
            }
        }
        return initData;
    };
    return FormTypeDataGetter;
}());

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

var FormTypeDrawerModelBuilder = (function () {
    function FormTypeDrawerModelBuilder(model) {
        this._model = model;
    }
    FormTypeDrawerModelBuilder.prototype.SetMultipleDropDownListFor = function (propertyName, selectListItems) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.MultipleDropDownList;
        block.SelectList = selectListItems;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetDropDownListFor = function (propertyName, selectListItems) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.DropDownList;
        block.SelectList = selectListItems;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetTextAreaFor = function (propertyName) {
        var block = this.GetPropertyBlockByName(propertyName);
        if (block.InterfaceType != UserInterfaceType.TextBox) {
            throw new Error("\u0422\u043E\u043B\u044C\u043A\u043E \u044D\u043B\u0435\u043C\u0435\u043D\u0442\u044B \u0441 \u0442\u0438\u043F\u043E\u043C " + UserInterfaceType.TextBox + " \u043C\u043E\u0436\u043D\u043E \u043F\u0435\u0440\u0435\u043A\u043B\u044E\u0447\u0430\u0442\u044C \u043D\u0430 " + UserInterfaceType.TextArea);
        }
        block.InterfaceType = UserInterfaceType.TextArea;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.SetHiddenFor = function (propertyName) {
        var block = this.GetPropertyBlockByName(propertyName);
        block.InterfaceType = UserInterfaceType.Hidden;
        this.ResetBlock(block);
        return this;
    };
    FormTypeDrawerModelBuilder.prototype.ResetBlock = function (block) {
        var index = this._model.Blocks.findIndex(function (x) { return x.PropertyName == block.PropertyName; });
        this._model.Blocks[index] = block;
    };
    FormTypeDrawerModelBuilder.prototype.GetPropertyBlockByName = function (propertyName) {
        var block = this._model.Blocks.find(function (x) { return x.PropertyName == propertyName; });
        if (block == null) {
            throw new Error("\u0411\u043B\u043E\u043A \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043F\u043E \u0443\u043A\u0430\u0437\u0430\u043D\u043D\u043E\u043C\u0443 \u0438\u043C\u0435\u043D\u0438 " + propertyName);
        }
        return block;
    };
    return FormTypeDrawerModelBuilder;
}());

var TryForm = (function () {
    function TryForm() {
    }
    TryForm.UnWrapModel = function (model, drawer) {
        var html = "";
        for (var i = 0; i < model.Blocks.length; i++) {
            var block = model.Blocks[i];
            switch (block.InterfaceType) {
                case UserInterfaceType.TextBox:
                    html += drawer.TextBoxFor(block.PropertyName, true);
                    break;
                case UserInterfaceType.TextArea:
                    html += drawer.TextAreaFor(block.PropertyName, true);
                    break;
                case UserInterfaceType.DropDownList:
                    html += drawer.DropDownFor(block.PropertyName, block.SelectList, true);
                    break;
                case UserInterfaceType.Hidden:
                    html += drawer.HiddenFor(block.PropertyName, true);
                    break;
                case UserInterfaceType.DatePicker:
                    html += drawer.DatePickerFor(block.PropertyName, true);
                    break;
                case UserInterfaceType.MultipleDropDownList:
                    html += drawer.MultipleDropDownFor(block.PropertyName, block.SelectList, true);
                    break;
                default:
                    console.log("Данный блок не реализован", block);
                    throw new Error("Не реализовано");
            }
        }
        return html;
    };
    TryForm.ThrowError = function (mes) {
        alert(mes);
        throw Error(mes);
    };
    TryForm.SetBeforeDrawingHandler = function (modelPrefix, func) {
        TryForm._beforeDrawInterfaceHandlers.add(modelPrefix, func);
    };
    TryForm.SetAfterDrawingHandler = function (modelPrefix, func) {
        TryForm._afterDrawInterfaceHandlers.add(modelPrefix, func);
    };
    TryForm.GetForms = function () {
        var elems = document.getElementsByClassName("generic-user-interface");
        for (var i = 0; i < elems.length; i++) {
            var elem = elems[i];
            TryForm.GetForm(elem);
        }
    };
    TryForm.GetDataForFormByModelPrefix = function (modelPrefix) {
        var model = TryForm._genericInterfaces.find(function (x) { return x.Prefix === modelPrefix; });
        if (model == null) {
            throw new Error("Generic user interface model is not defined by prefix '" + modelPrefix + "'");
        }
        return TryForm.GetDataForForm(model);
    };
    TryForm.GetDataForFirstForm = function () {
        if (TryForm._genericInterfaces.length === 0) {
            TryForm.ThrowError("На странице не объявлено ни одной формы");
        }
        var model = TryForm._genericInterfaces[0];
        return TryForm.GetDataForForm(model);
    };
    TryForm.GetDataForForm = function (buildModel) {
        var getter = new FormTypeDataGetter(buildModel.TypeDescription);
        return getter.GetData(buildModel.Prefix);
    };
    TryForm.AddBuildModel = function (buildModel) {
        var elem = TryForm._genericInterfaces.find(function (x) { return x.Prefix == buildModel.Prefix; });
        if (elem != null) {
            TryForm.ThrowError("\u041D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u0443\u0436\u0435 \u043E\u0431\u044A\u044F\u0432\u043B\u0435\u043D\u0430 \u0444\u043E\u0440\u043C\u0430 \u0441 \u043F\u0440\u0435\u0444\u0438\u043A\u0441\u043E\u043C " + buildModel.Prefix);
        }
        TryForm._genericInterfaces.push(buildModel);
    };
    TryForm.GetForm = function (elem) {
        var id = elem.getAttribute("data-id");
        var formDrawKey = elem.getAttribute("data-form-draw");
        var buildModel = window[id];
        if (TryForm._beforeDrawInterfaceHandlers.containsKey(formDrawKey)) {
            var func = TryForm._beforeDrawInterfaceHandlers.getByKey(formDrawKey);
            buildModel = func(buildModel);
        }
        TryForm.AddBuildModel(buildModel);
        var drawImpl = FormDrawFactory.GetImplementation(buildModel, formDrawKey);
        var drawer = new FormTypeDrawer(drawImpl, buildModel.TypeDescription);
        drawer.BeforeFormDrawing();
        elem.innerHTML = TryForm.UnWrapModel(buildModel, drawer);
        drawer.AfterFormDrawing();
        if (TryForm._afterDrawInterfaceHandlers.containsKey(formDrawKey)) {
            var action = TryForm._afterDrawInterfaceHandlers.getByKey(formDrawKey);
            action(buildModel);
        }
    };
    TryForm._genericInterfaces = [];
    TryForm._beforeDrawInterfaceHandlers = new Dictionary();
    TryForm._afterDrawInterfaceHandlers = new Dictionary();
    return TryForm;
}());
TryForm.GetForms();