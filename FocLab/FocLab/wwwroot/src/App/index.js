var AccountWorker = /** @class */ (function () {
    function AccountWorker() {
    }
    AccountWorker.CheckUser = function () {
        //TODO Implement User Checking
        if (!this.IsAuthenticated() || true) {
            return;
        }
    };
    AccountWorker.IsAuthenticated = function () {
        var value = this.User != null;
        console.log("AccountWorker.IsAuthenticated()=" + value);
        return value;
    };
    AccountWorker.User = null;
    return AccountWorker;
}());
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser(); }, 1100);
});

var DatePickerUtils = /** @class */ (function () {
    function DatePickerUtils() {
    }
    DatePickerUtils.InitDictionary = function () {
        $.fn.datepicker['dates']['ru'] = {
            days: ['понедельник', 'воскресенье', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
            daysShort: ['вс', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'],
            daysMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
            months: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
                'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            monthsShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
                'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
            today: "Today"
        };
    };
    DatePickerUtils.SetDatePicker = function (datePickerId, elementIdToUpdate, dateValue) {
        if (dateValue === void 0) { dateValue = null; }
        var datePickerElement = document.getElementById(datePickerId);
        if (datePickerElement == null) {
            alert("Utils.SetDatePicker \u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + datePickerId + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435");
        }
        var toUpdateElement = document.getElementById(elementIdToUpdate);
        if (toUpdateElement == null) {
            alert("Utils.SetDatePicker \u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + elementIdToUpdate + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435");
        }
        var selector = "#" + datePickerId;
        $(selector).datepicker({
            format: "dd/mm/yyyy",
            autoclose: true,
            language: "ru",
            zIndexOffset: 1000
        });
        DatePickerUtils.ActiveDatePickers.push({
            BackElementId: elementIdToUpdate,
            DatePickerId: datePickerId
        });
        //Ставлю обработчик на изменение основного элемента
        $(selector).on("change", function (e) {
            var comingValue = e.target.value;
            var dateValue = new Date(DatePickerUtils.DDMMYYYYToMMDDDDYYYYString(comingValue));
            if (isNaN(dateValue.getTime())) {
                dateValue = null;
            }
            var valueToSet = dateValue == null ? CrocoAppCore.Application.FormDataHelper.NullValue
                //Убираем добавленные таймзоны а затем отсекаем время
                : new Date(dateValue.getTime() - (dateValue.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
            console.log("Utils.DatePickerValueChanged", dateValue, valueToSet);
            //Устанавливаем дату обрезая время
            document.getElementById(elementIdToUpdate).value = valueToSet;
        });
        //Вкидываю событие об изменении чтобы фейковый элемент подхватил данные с дэйтпикера
        var event = new Event("change");
        var element = document.getElementById(datePickerId);
        element.dispatchEvent(event);
        if (dateValue !== undefined) {
            DatePickerUtils.SetDateToDatePicker(datePickerId, dateValue);
        }
    };
    DatePickerUtils.SetDateToDatePicker = function (datePickerId, dateValue) {
        var selector = "#" + datePickerId;
        $(selector).datepicker("setDate", dateValue);
    };
    DatePickerUtils.DDMMYYYYToMMDDDDYYYYString = function (s) {
        var bits = s.split("/");
        return bits[1] + "/" + bits[0] + "/" + bits[2];
    };
    DatePickerUtils.GetDateFromDatePicker = function (inputId) {
        var elem = DatePickerUtils.ActiveDatePickers.find(function (x) { return x.DatePickerId === inputId; });
        if (elem == null) {
            alert("DatePicker \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + inputId + " \u043D\u0435 \u0438\u043D\u0438\u0446\u0438\u0430\u043B\u0438\u0438\u0440\u043E\u0432\u0430\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435");
        }
        return new Date(document.getElementById(elem.BackElementId).value);
    };
    DatePickerUtils.ActiveDatePickers = [];
    return DatePickerUtils;
}());
DatePickerUtils.InitDictionary();

var GenericInterfaceAppHelper = /** @class */ (function () {
    function GenericInterfaceAppHelper() {
        this.FormHelper = new GenericForm({ FormDrawFactory: CrocoAppCore.GetFormDrawFactory() });
    }
    /**
     * Получить модель для построения обобщенного пользовательского интерфейса
     * @param typeName Полное или сокращенное название класса C#
     * @param modelPrefix Префикс для построения модели
     * @param callBack Функция, которая вызовется когда модель будет получена с сервера
     */
    GenericInterfaceAppHelper.prototype.GetUserInterfaceModel = function (typeName, modelPrefix, callBack) {
        var data = { typeName: typeName, modelPrefix: modelPrefix };
        CrocoAppCore.Application.Requester.Post("/Api/Documentation/GenericInterface", data, function (x) {
            if (x == null) {
                alert("\u041E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u044B\u0439 \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441 \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C '" + typeName + "' \u043D\u0435 \u043F\u0440\u0438\u0448\u0451\u043B \u0441 \u0441\u0435\u0440\u0432\u0435\u0440\u0430");
                return;
            }
            callBack(x);
        }, function () {
            var mes = "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430 \u043F\u0440\u0438 \u043F\u043E\u0438\u0441\u043A\u0435 \u043E\u0431\u043E\u0431\u0449\u0435\u043D\u043D\u043E\u0433\u043E \u0438\u043D\u0442\u0435\u0440\u0444\u0435\u0439\u0441\u0430 \u043F\u043E \u0442\u0438\u043F\u0443 " + typeName;
            alert(mes);
            CrocoAppCore.Application.Logger.LogAction(mes, "", "GenericInterfaceAppHelper.GetUserInterfaceModel.ErrorOnRequest", JSON.stringify(data));
        });
    };
    /**
     * Получить модель для построения обобщенного пользовательского интерфейса
     * @param enumTypeName Полное или сокращенное название перечисления в C#
     * @param callBack Функция, которая вызовется когда модель будет получена с сервера
     */
    GenericInterfaceAppHelper.prototype.GetEnumModel = function (enumTypeName, callBack) {
        var data = { typeName: enumTypeName };
        CrocoAppCore.Application.Requester.Post("/Api/Documentation/EnumType", data, function (x) {
            if (x == null) {
                alert("\u041F\u0435\u0440\u0435\u0447\u0438\u0441\u043B\u0435\u043D\u0438\u0435 \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C '" + enumTypeName + "' \u043D\u0435 \u043F\u0440\u0438\u0448\u043B\u043E \u0441 \u0441\u0435\u0440\u0432\u0435\u0440\u0430");
                return;
            }
            callBack(x);
        }, function () {
            var mes = "\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430 \u043F\u0440\u0438 \u043F\u043E\u0438\u0441\u043A\u0435 \u043F\u0435\u0440\u0435\u0447\u0435\u0438\u0441\u043B\u0435\u043D\u0438\u044F \u0441 \u043D\u0430\u0437\u0432\u0430\u043D\u0438\u0435\u043C " + enumTypeName;
            alert(mes);
            CrocoAppCore.Application.Logger.LogAction(mes, "", "GenericInterfaceAppHelper.GetEnumModel.ErrorOnRequest", JSON.stringify(data));
        });
    };
    return GenericInterfaceAppHelper;
}());

var ImageSizeType;
(function (ImageSizeType) {
    ImageSizeType[ImageSizeType["Icon"] = "Icon"] = "Icon";
    ImageSizeType[ImageSizeType["Medium"] = "Medium"] = "Medium";
    ImageSizeType[ImageSizeType["Small"] = "Small"] = "Small";
    ImageSizeType[ImageSizeType["Original"] = "Original"] = "Original";
})(ImageSizeType || (ImageSizeType = {}));

//В данном файле переопределются входные элементы основного приложения
//До вызова переопределений библиотека CrocoAppCore должна быть объявлена
CrocoAppCore.InitFields();
//Вызываю отрисовку обобщенных форм на UI
CrocoAppCore.GenericInterfaceHelper.FormHelper.DrawForms();
setTimeout(function () {
    NotificationWorker.GetNotification();
}, 1000);


var TimePickerUtils = /** @class */ (function () {
    function TimePickerUtils() {
    }
    TimePickerUtils.SetTimePicker = function (elementId) {
        $("#" + elementId).timepicker();
    };
    TimePickerUtils.GetTimeValueInMinutes = function (elementId) {
        var elem = document.getElementById(elementId);
        if (elem == null) {
            alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C '" + elementId + "' \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435");
            return;
        }
        var value = elem.value;
        if (value == '12:00 AM') {
            return 0;
        }
        var initBits = value.split(' ');
        var timeBits = initBits[0].split(':').map(function (x) { return Number(x); });
        var result = timeBits[0] * 60 + timeBits[1];
        return initBits[1] == 'PM' ? result + 12 * 60 : result;
    };
    return TimePickerUtils;
}());

/// <reference path="../../../node_modules/@types/bootstrap/index.d.ts"/>
/// <reference path="../../../node_modules/@types/bootstrap-datepicker/index.d.ts"/>
var Utils = /** @class */ (function () {
    function Utils() {
    }
    Utils.FillSelect = function (select, array, htmlFunc, valueFunc) {
        for (var i = 0; i < array.length; i++) {
            var opt = document.createElement("option");
            opt.innerHTML = htmlFunc(array[i]);
            opt.value = valueFunc(array[i]);
            select.append(opt);
        }
    };
    Utils.GetImageLinkByFileId = function (fileId, sizeType) {
        return "/FileCopies/Images/" + sizeType.toString() + "/" + fileId + ".jpg";
    };
    return Utils;
}());

var GetListResultExtensions = /** @class */ (function () {
    function GetListResultExtensions() {
    }
    /**
     * Получить номер текущей страницы
     * */
    GetListResultExtensions.GetCurrentPageNumber = function (model) {
        if (model.Count === null) {
            return 0;
        }
        return Math.trunc(model.OffSet / model.Count);
    };
    /**
     * Получить количество страниц
     * */
    GetListResultExtensions.GetPagesCount = function (model) {
        if (model.Count === null) {
            return 0;
        }
        var preRes = Math.trunc(model.TotalCount / model.Count);
        return model.TotalCount % model.Count > 0 ? preRes + 1 : preRes;
    };
    return GetListResultExtensions;
}());

var PagerModel = /** @class */ (function () {
    function PagerModel() {
    }
    PagerModel.ToPagerModel = function (model, link) {
        var startUrl = CrocoAppCore.Application.FormDataUtils.GetStartUrlNoParams(link);
        var linkObj = CrocoAppCore.Application.FormDataUtils.GetUrlParamsObject(link);
        var countToChangeTempName = "CountToChange";
        var offSetToChangeTempName = "OffSetToChange";
        linkObj["Count"] = countToChangeTempName;
        linkObj["OffSet"] = offSetToChangeTempName;
        var linkFormat = startUrl + "?" + PagerModel.GetParams(linkObj);
        return {
            CurrentPage: GetListResultExtensions.GetCurrentPageNumber(model),
            PagesCount: GetListResultExtensions.GetPagesCount(model),
            LinkFormat: linkFormat.replace(countToChangeTempName, "{0}").replace(offSetToChangeTempName, "{1}"),
            PageSize: model.Count
        };
    };
    PagerModel.GetCombinedData = function (prefix, obj) {
        var resultObj = {};
        for (var prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                var objWithProps = this.GetCombinedData("" + prefix + prop + ".", obj[prop]);
                for (var innerProp in objWithProps) {
                    resultObj[innerProp] = objWithProps[innerProp];
                }
            }
            else {
                resultObj[prefix + prop] = obj[prop];
            }
        }
        return resultObj;
    };
    PagerModel.GetParams = function (obj) {
        obj = PagerModel.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    return PagerModel;
}());

var Pagination = /** @class */ (function () {
    function Pagination() {
    }
    Pagination.RenderPaginationToElementIds = function (model, elementIds) {
        var html = this.RenderPagination(model);
        for (var i = 0; i < elementIds.length; i++) {
            var id = elementIds[i];
            var elem = document.getElementById(id);
            if (elem == null) {
                alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043F\u043E \u0443\u043A\u0430\u0437\u0430\u043D\u043E\u043C\u0443 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u0443 '" + id + "'");
            }
            elem.innerHTML = html;
        }
    };
    Pagination.RenderPagination = function (model) {
        if (model.PagesCount === 1) {
            return "";
        }
        var res = "<div class=\"table-responsive\">\n        <nav aria-label=\"Page navigation example\">\n            <ul class=\"pagination\">";
        for (var i = 0; i < model.PagesCount; i++) {
            var _class = i == model.CurrentPage ? "active" : "";
            var link = model.LinkFormat.format(model.PageSize.toString(), (i * model.PageSize).toString());
            res += "<li class=\"page-item " + _class + "\">\n                        <a class=\"page-link\" href=\"" + link + "\">" + (i + 1) + "</a>\n                    </li>";
        }
        res += "</ul>\n                </nav>\n                </div>";
        return res;
    };
    return Pagination;
}());