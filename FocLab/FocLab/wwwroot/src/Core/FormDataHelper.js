var FormDataHelper = /** @class */ (function () {
    function FormDataHelper() {
    }
    FormDataHelper.CollectDataByPrefix = function (object, prefix) {
        for (var index in object) {
            if (object.hasOwnProperty(index)) {
                var name_1 = prefix + index;
                var element = document.getElementsByName(name_1)[0];
                if (element == null) {
                    alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u043C\u0435\u043D\u0435\u043C " + name_1 + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u043F\u0440\u043E\u0432\u0435\u0440\u044C\u0442\u0435 \u043A\u043E\u0434");
                    continue;
                }
                if (element.type === "select-multiple") {
                    object[index] = Array.apply(null, element.options)
                        .filter(function (option) { return option.selected; })
                        .map(function (option) { return option.value; });
                    continue;
                }
                console.log(name_1, element, element.type);
                if (element.type === "radio") {
                    var value = document.querySelector("input[name=\"" + name_1 + "\"]:checked") != null;
                    if (value)
                        (object[index] = document.querySelector("input[name=\"" + name_1 + "\"]:checked")).value;
                    continue;
                }
                //Чекбоксы нужно проверять отдельно потому что у них свойство не value а почему-то checked
                object[index] = element.type === "checkbox" ? element.checked : element.value;
            }
        }
        return object;
    };
    FormDataHelper.FillData = function (object) {
        FormDataHelper.FillDataByPrefix(object, "");
    };
    FormDataHelper.FillDataByPrefix = function (object, prefix) {
        for (var index in object) {
            var name_2 = prefix + index;
            var element = document.getElementsByName(name_2)[0];
            if (element === null || element === undefined) {
                continue;
            }
            if (Array.isArray(object[index])) {
                if (element.type !== "select-multiple") {
                    alert("Попытка присвоить массив элементу ввода который не является select с атрибутом multiple");
                }
                var select = element;
                var _loop_1 = function (i) {
                    var opt = select.options[i];
                    var value = object[index].filter(function (x) { return opt.value === x; }).length > 0;
                    opt.selected = value;
                };
                for (var i = 0; i < select.options.length; i++) {
                    _loop_1(i);
                }
                var event_1 = new Event("change");
                element.dispatchEvent(event_1);
                continue;
            }
            if (element.type === "checkbox") {
                element.checked = object[index];
            }
            else if (element.type === "radio") {
                document.querySelector("input[name=" + name_2 + "][value=" + object[index] + "]").checked = true;
            }
            else {
                element.value = object[index];
            }
            //Выбрасываю событие об изменении значения
            var event_2 = new Event("change");
            element.dispatchEvent(event_2);
        }
    };
    FormDataHelper.CollectData = function (object) {
        return FormDataHelper.CollectDataByPrefix(object, "");
    };
    return FormDataHelper;
}());
