﻿var TryForm = (function () {
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
