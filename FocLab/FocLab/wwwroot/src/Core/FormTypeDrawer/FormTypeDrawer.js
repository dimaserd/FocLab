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
})(UserInterfaceType || (UserInterfaceType = {}));

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

var TabFormDrawImplementation = /** @class */ (function () {
    function TabFormDrawImplementation(model) {
        this._datePickerPropNames = [];
        this._selectClass = 'form-draw-select';
        this._model = model;
    }
    TabFormDrawImplementation.prototype.BeforeFormDrawing = function () {
        //TODO Init calendar or some scripts
    };
    TabFormDrawImplementation.prototype.AfterFormDrawing = function () {
        //Красивые селекты
        $("." + this._selectClass).selectpicker('refresh');
        //Инициация календарей
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
    TabFormDrawImplementation.prototype.RenderDropDownList = function (typeDescription, selectList) {
        var rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);
        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);
        var _class = this._selectClass + " form-control m-input m-bootstrap-select m_selectpicker";
        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription.PropertyName), selectList);
        return "<div class=\"form-group m-form__group row\">\n                    <label class=\"col-xl-3 col-lg-3 col-form-label\">" + typeDescription.PropertyDisplayName + ":</label>\n                    <div class=\"col-xl-9 col-lg-9\">\n                        " + select + "\n                    </div>\n                </div>";
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





var FormDrawFactory = /** @class */ (function () {
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

var HtmlDrawHelper = /** @class */ (function () {
    function HtmlDrawHelper() {
    }
    HtmlDrawHelper.RenderSelect = function (className, propName, selectList) {
        var select = "<select class=\"" + className + "\" name=\"" + propName + "\">";
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
            //Заплатка для выпадающего списка 
            //TODO Вылечить это
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

var ValueProviderHelper = /** @class */ (function () {
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

var FormTypeDataGetter = /** @class */ (function () {
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
                    initData[prop.PropertyName] = initData[prop.PropertyName].toLowerCase() == "true";
                    break;
            }
        }
        return initData;
    };
    return FormTypeDataGetter;
}());

var FormTypeDrawer = /** @class */ (function () {
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
    FormTypeDrawer.prototype.TextBoxFor = function (propertyName) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextBox(prop);
    };
    FormTypeDrawer.prototype.DatePickerFor = function (propertyName) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDatePicker(prop);
    };
    FormTypeDrawer.prototype.TextAreaFor = function (propertyName) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextArea(prop);
    };
    FormTypeDrawer.prototype.DropDownFor = function (propertyName, selectList) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDropDownList(prop, selectList);
    };
    FormTypeDrawer.prototype.HiddenFor = function (propertyName) {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderHidden(prop);
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

var TryForm = /** @class */ (function () {
    function TryForm() {
    }
    TryForm.UnWrapModel = function (model, drawer) {
        var html = "";
        for (var i = 0; i < model.Blocks.length; i++) {
            var block = model.Blocks[i];
            switch (block.InterfaceType) {
                case UserInterfaceType.TextBox:
                    html += drawer.TextBoxFor(block.PropertyName);
                    break;
                case UserInterfaceType.TextArea:
                    html += drawer.TextAreaFor(block.PropertyName);
                    break;
                case UserInterfaceType.DropDownList:
                    html += drawer.DropDownFor(block.PropertyName, block.SelectList);
                    break;
                case UserInterfaceType.Hidden:
                    html += drawer.HiddenFor(block.PropertyName);
                    break;
                case UserInterfaceType.DatePicker:
                    html += drawer.DatePickerFor(block.PropertyName);
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
    TryForm.GetForms = function () {
        var elems = document.getElementsByClassName("generic-user-interface");
        for (var i = 0; i < elems.length; i++) {
            var elem = elems[i];
            TryForm.GetForm(elem);
        }
    };
    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    TryForm.GetDataForFormByModelPrefix = function (modelPrefix) {
        var model = TryForm._genericInterfaces.find(function (x) { return x.Prefix == modelPrefix; });
        if (model == null) {
        }
        return TryForm.GetDataForForm(model);
    };
    /**
     * Получить объект данных с первой попавшейся формы на странице
     */
    TryForm.GetDataForFirstForm = function () {
        if (TryForm._genericInterfaces.length == 0) {
            TryForm.ThrowError("На странице не объявлено ни одной формы");
        }
        var model = TryForm._genericInterfaces[0];
        return TryForm.GetDataForForm(model);
    };
    /**
     * Получить данные с формы приведенные к описанному типу данных
     * @param buildModel Тип данных
     */
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
        TryForm.AddBuildModel(buildModel);
        var drawImpl = FormDrawFactory.GetImplementation(buildModel, formDrawKey);
        var drawer = new FormTypeDrawer(drawImpl, buildModel.TypeDescription);
        drawer.BeforeFormDrawing();
        elem.innerHTML = TryForm.UnWrapModel(buildModel, drawer);
        drawer.AfterFormDrawing();
    };
    TryForm._genericInterfaces = [];
    return TryForm;
}());
TryForm.GetForms();
