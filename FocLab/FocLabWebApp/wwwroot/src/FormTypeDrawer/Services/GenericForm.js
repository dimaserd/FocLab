var GenericForm = (function () {
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
