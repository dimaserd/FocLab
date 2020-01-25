var UserInterfaceType;
(function (UserInterfaceType) {
    UserInterfaceType[UserInterfaceType["CustomInput"] = "CustomInput"] = "CustomInput";
    UserInterfaceType[UserInterfaceType["TextBox"] = "TextBox"] = "TextBox";
    UserInterfaceType[UserInterfaceType["TextArea"] = "TextArea"] = "TextArea";
    UserInterfaceType[UserInterfaceType["DropDownList"] = "DropDownList"] = "DropDownList";
    UserInterfaceType[UserInterfaceType["Hidden"] = "Hidden"] = "Hidden";
    UserInterfaceType[UserInterfaceType["DatePicker"] = "DatePicker"] = "DatePicker";
    UserInterfaceType[UserInterfaceType["MultipleDropDownList"] = "MultipleDropDownList"] = "MultipleDropDownList";
})(UserInterfaceType || (UserInterfaceType = {}));









var FormDrawFactory = /** @class */ (function () {
    function FormDrawFactory(opts) {
        this._defaultImplementation = opts.DefaultImplementation;
        this._implementations = opts.Implementations;
    }
    FormDrawFactory.prototype.GetImplementation = function (buildModel, key) {
        var func = this._implementations.get(key);
        if (func == null) {
            return this._defaultImplementation(buildModel);
        }
        return func(buildModel);
    };
    return FormDrawFactory;
}());

var FormDrawHelper = /** @class */ (function () {
    function FormDrawHelper() {
    }
    FormDrawHelper.GetPropertyValueName = function (propertyName, modelPrefix) {
        return "" + modelPrefix + propertyName;
    };
    FormDrawHelper.GetOuterFormElement = function (propertyName, modelPrefix) {
        return document.querySelector("[" + FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\"][" + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"]");
    };
    FormDrawHelper.GetOuterFormAttributes = function (propertyName, modelPrefix) {
        return FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\" " + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"";
    };
    FormDrawHelper.GetInputTypeHidden = function (propName, value, otherProps) {
        if (otherProps === void 0) { otherProps = null; }
        var t = otherProps != null ? otherProps : new Map();
        t.set("value", value);
        t.set("name", propName);
        return HtmlDrawHelper.RenderInput("hidden", t);
    };
    FormDrawHelper.FormPropertyName = "form-property-name";
    FormDrawHelper.FormModelPrefix = "form-model-prefix";
    return FormDrawHelper;
}());

var HtmlDrawHelper = /** @class */ (function () {
    function HtmlDrawHelper() {
    }
    HtmlDrawHelper.RenderInput = function (type, attrs) {
        var attrString = HtmlDrawHelper.RenderAttributesString(attrs);
        return "<input type=" + type + " " + attrString + "/>";
    };
    HtmlDrawHelper.RenderAttributesString = function (attrs) {
        var result = "";
        if (attrs == null) {
            return result;
        }
        //Итерация по ключам в map
        for (var _i = 0, _a = Array.from(attrs.keys()); _i < _a.length; _i++) {
            var key = _a[_i];
            var res = attrs.get(key);
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
        var select = "<select" + attrStr + " class=\"" + className + "\" name=\"" + propName + "\">";
        for (var i = 0; i < selectList.length; i++) {
            var item = selectList[i];
            var selected = item.Selected ? " selected=\"selected\"" : '';
            select += "<option" + selected + " value=\"" + item.Value + "\">" + item.Text + "</option>";
        }
        select += "</select>";
        return select;
    };
    return HtmlDrawHelper;
}());

var HtmlSelectDrawHelper = /** @class */ (function () {
    function HtmlSelectDrawHelper(nullValue) {
        this.NullValue = nullValue;
    }
    HtmlSelectDrawHelper.prototype.ProccessSelectValues = function (typeDescription, rawValue, selectList) {
        var _this = this;
        selectList.forEach(function (x) {
            if (x.Value == null) {
                x.Value = _this.NullValue;
            }
        });
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
    };
    return HtmlSelectDrawHelper;
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
            var value = valueProvider.Singles.find(function (x) { return x.PropertyName == prop.PropertyDescription.PropertyName; });
            return (value == null) ? null : value.Value;
        }
        return "";
    };
    return ValueProviderHelper;
}());

var CrocoTypeDescriptionOverrider = /** @class */ (function () {
    function CrocoTypeDescriptionOverrider() {
    }
    CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName = function (model, propertyName) {
        var prop = model.Blocks.find(function (x) { return x.PropertyName === propertyName; });
        if (prop == null) {
            alert("\u0421\u0432\u043E\u0439\u0441\u0442\u0432\u043E '" + propertyName + "' \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E \u0432 \u043C\u043E\u0434\u0435\u043B\u0438 \u043E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u043E\u0433\u043E \u043F\u043E\u043B\u044C\u0437\u043E\u0432\u0430\u0442\u043B\u0435\u044C\u0441\u043A\u043E\u0433\u043E \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441\u0430");
            return;
        }
        return prop;
    };
    CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty = function (model, propertyName, interfaceType) {
        var prop = CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName);
        prop.InterfaceType = interfaceType;
    };
    CrocoTypeDescriptionOverrider.RemoveProperty = function (model, propertyName) {
        model.Blocks = model.Blocks.filter(function (x) { return x.PropertyName !== propertyName; });
    };
    /**
     * Установить текстовый лейбл для имени свойста
     * @param model модель для свойства которой нужно установить лейбл
     * @param propertyName  название свойства
     * @param labelText текст лейбла для заданного свойства
     */
    CrocoTypeDescriptionOverrider.SetLabelText = function (model, propertyName, labelText) {
        CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName).LabelText = labelText;
    };
    /**
     * Установить для свойства модели тип пользовательского интрефейса скрытый инпут
     * @param model модель для которой нужно поменять тип интерфейса для свойства
     * @param propertyName имя свойства
     */
    CrocoTypeDescriptionOverrider.SetHidden = function (model, propertyName) {
        CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty(model, propertyName, UserInterfaceType.Hidden);
    };
    /**
     *  Установить для свойства модели тип пользовательского интерфейса textarea (большой текстовый инпут)
     * @param model  модель для которой нужно поменять тип интерфейса для свойства
     * @param propertyName имя свойства
     */
    CrocoTypeDescriptionOverrider.SetTextArea = function (model, propertyName) {
        CrocoTypeDescriptionOverrider.SetUserInterfaceTypeForProperty(model, propertyName, UserInterfaceType.TextArea);
    };
    /**
     *  Установить для свойства модели тип пользовательского интерфейса выпадающий список
     * @param model  модель для которой нужно поменять тип интерфейса для свойства
     * @param propertyName имя свойства
     * @param selectList  данные для построения выпадающего списка
     */
    CrocoTypeDescriptionOverrider.SetDropDownList = function (model, propertyName, selectList) {
        var prop = CrocoTypeDescriptionOverrider.FindUserInterfacePropBlockByPropertyName(model, propertyName);
        prop.InterfaceType = UserInterfaceType.DropDownList;
        prop.SelectList = selectList;
    };
    return CrocoTypeDescriptionOverrider;
}());

var FormTypeAfterDrawnDrawer = /** @class */ (function () {
    function FormTypeAfterDrawnDrawer() {
    }
    FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty = function (propertyName, modelPrefix, innerHtml) {
        var elem = FormDrawHelper.GetOuterFormElement(propertyName, modelPrefix);
        console.log("SetInnerHtmlForProperty elem", elem);
        elem.innerHTML = innerHtml;
    };
    return FormTypeAfterDrawnDrawer;
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
    FormTypeDrawer.prototype.TextBoxFor = function (propertyName, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextBox(prop, wrap);
    };
    FormTypeDrawer.prototype.DatePickerFor = function (propertyName, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDatePicker(prop, wrap);
    };
    FormTypeDrawer.prototype.TextAreaFor = function (propertyName, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderTextArea(prop, wrap);
    };
    FormTypeDrawer.prototype.DropDownFor = function (propertyName, selectList, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderDropDownList(prop, selectList, wrap);
    };
    FormTypeDrawer.prototype.MultipleDropDownFor = function (propertyName, selectList, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderMultipleDropDownList(prop, selectList, wrap);
    };
    FormTypeDrawer.prototype.HiddenFor = function (propertyName, wrap) {
        var prop = PropertyFormTypeSearcher.FindPropByName(this._typeDescription, propertyName);
        return this._formDrawer.RenderHidden(prop, wrap);
    };
    return FormTypeDrawer;
}());

var FormTypeDrawerModelBuilder = /** @class */ (function () {
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

var GenericForm = /** @class */ (function () {
    function GenericForm(opts) {
        this._genericInterfaces = [];
        if (opts.FormDrawFactory == null) {
            GenericForm.ThrowError("Фабрика реализаций отрисовки обобщенных форм == null. Проверьте заполнение свойства opts.FormDrawFactory");
        }
        this._formDrawFactory = opts.FormDrawFactory;
    }
    GenericForm.UnWrapModel = function (model, drawer) {
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
    GenericForm.ThrowError = function (mes) {
        alert(mes);
        throw Error(mes);
    };
    /*
     * Отрисовать все формы, которые имеются на экране
     * */
    GenericForm.prototype.DrawForms = function () {
        var elems = document.getElementsByClassName("generic-user-interface");
        for (var i = 0; i < elems.length; i++) {
            this.FindFormAndSave(elems[i]);
        }
        for (var i = 0; i < this._genericInterfaces.length; i++) {
            this.DrawForm(this._genericInterfaces[i]);
        }
    };
    GenericForm.prototype.FindFormAndSave = function (elem) {
        var id = elem.getAttribute("data-id");
        var buildModel = window[id];
        if (buildModel == null) {
            return;
        }
        if (elem.id == null) {
            console.log(elem);
            GenericForm.ThrowError("\u041D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u0438\u043C\u0435\u044E\u0442\u0441\u044F \u044D\u043B\u0435\u043C\u0435\u043D\u0442\u044B \u0441 \u043D\u0430\u0441\u0442\u0440\u043E\u0439\u043A\u0430\u043C\u0438 \u0434\u043B\u044F \u0433\u0435\u043D\u0435\u0440\u0430\u0446\u0438\u0438 \u043E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u043E\u0433\u043E \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441\u0430, \u043D\u043E \u0443 \u044D\u043B\u0435\u043C\u0435\u043D\u0442\u0430 \u043D\u0435\u0442 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u0430");
        }
        this._genericInterfaces.push({
            ElementId: elem.id,
            FormDrawKey: "",
            FormModel: buildModel
        });
        window[id] = null;
    };
    GenericForm.prototype.DrawForm = function (renderModel) {
        var drawImpl = this._formDrawFactory.GetImplementation(renderModel.FormModel, renderModel.FormDrawKey);
        var drawer = new FormTypeDrawer(drawImpl, renderModel.FormModel.TypeDescription);
        drawer.BeforeFormDrawing();
        var elem = document.getElementById(renderModel.ElementId);
        if (elem == null) {
            GenericForm.ThrowError("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + renderModel.ElementId + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435");
        }
        elem.innerHTML = GenericForm.UnWrapModel(renderModel.FormModel, drawer);
        drawer.AfterFormDrawing();
    };
    return GenericForm;
}());

var PropertyFormTypeSearcher = /** @class */ (function () {
    function PropertyFormTypeSearcher() {
    }
    PropertyFormTypeSearcher.FindPropByNameInOneDimension = function (type, propName) {
        return type.Properties.find(function (x) { return x.PropertyDescription.PropertyName === propName; });
    };
    PropertyFormTypeSearcher.FindPropByName = function (type, propName) {
        if (propName.includes(".")) {
            var indexOfFirstDot = propName.indexOf(".");
            var fBit = propName.slice(0, indexOfFirstDot);
            var anotherBit = propName.slice(indexOfFirstDot + 1, propName.length);
            var innerProp = PropertyFormTypeSearcher.FindPropByNameInOneDimension(type, fBit);
            return PropertyFormTypeSearcher.FindPropByName(innerProp, anotherBit);
        }
        var prop = PropertyFormTypeSearcher.FindPropByNameInOneDimension(type, propName);
        if (prop == null) {
            throw new Error("\u0421\u0432\u043E\u0439\u0441\u0442\u0432\u043E " + propName + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E");
        }
        return prop;
    };
    return PropertyFormTypeSearcher;
}());

var ValueProviderBuilder = /** @class */ (function () {
    function ValueProviderBuilder() {
    }
    /**
     * Создать провайдера значений из объекта JavaScript
     * @param obj объект из которого нужно создать провайдер значений
     */
    ValueProviderBuilder.CreateFromObject = function (obj) {
        obj = CrocoAppCore.Application.FormDataUtils.ProccessAllDateTimePropertiesAsString(obj);
        var res = {
            Arrays: [],
            Singles: []
        };
        for (var index in obj) {
            var valueOfProp = obj[index];
            if (Array.isArray(valueOfProp)) {
                //TODO Добавить поддержку массивов
                continue;
            }
            if (valueOfProp !== undefined) {
                res.Singles.push({
                    PropertyName: index,
                    Value: valueOfProp
                });
            }
        }
        return res;
    };
    return ValueProviderBuilder;
}());