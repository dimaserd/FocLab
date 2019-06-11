var AccountWorker = /** @class */ (function () {
    function AccountWorker() {
    }
    AccountWorker.User = null;
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
    return AccountWorker;
}());
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser(); }, 1100);
});

var AjaxLoader = /** @class */ (function () {
    function AjaxLoader() {
    }
    AjaxLoader.InitAjaxLoads = function () {
        var elems = document.getElementsByClassName("ajax-load-html");
        for (var i = 0; i < elems.length; i++) {
            AjaxLoader.LoadInnerHtmlToElement(elems[i], function () { return 2; });
        }
    };
    AjaxLoader.LoadInnerHtmlToElement = function (element, onSuccessFunc) {
        var link = $(element).data('ajax-link');
        var method = $(element).data('ajax-method');
        var data = $(element).data('request-data');
        var onSuccessScript = $(element).data('on-finish-script');
        if (method == null) {
            method = "GET";
        }
        $.ajax({
            type: method,
            url: link,
            cache: false,
            data: data,
            success: function (response) {
                $(element).html(response);
                $(element).removeClass("ajax-load-html");
                if (onSuccessScript) {
                    eval(onSuccessScript);
                }
                if (onSuccessFunc) {
                    onSuccessFunc();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("\u041F\u0440\u043E\u0438\u0437\u043E\u0448\u043B\u0430 \u043E\u0448\u0438\u0431\u043A\u0430 \u0437\u0430\u043F\u0440\u043E\u0441\u0430 " + link);
                console.log(xhr);
            }
        });
    };
    return AjaxLoader;
}());
AjaxLoader.InitAjaxLoads();

var CookieWorker = /** @class */ (function () {
    function CookieWorker() {
    }
    CookieWorker.setCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    };
    CookieWorker.getCookie = function (name) {
        var nameEq = name + "=";
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === " ")
                c = c.substring(1, c.length);
            if (c.indexOf(nameEq) === 0)
                return c.substring(nameEq.length, c.length);
        }
        return null;
    };
    CookieWorker.eraseCookie = function (name) {
        document.cookie = name + "=; Max-Age=-99999999;";
    };
    return CookieWorker;
}());

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

var Logger = /** @class */ (function () {
    function Logger() {
    }
    Logger.LogException = function (exception, link) {
        $.ajax({
            type: "POST",
            data: {
                ExceptionDate: new Date().toISOString(),
                Description: "Ошибка запроса к апи",
                Message: exception,
                Uri: link !== null ? link : location.href
            },
            url: "/Api/Log/Exception",
            async: true,
            cache: false,
            success: function (data) {
                console.log("Исключение залоггировано", data);
            },
            error: function () {
                alert("Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения");
            }
        });
    };
    Logger.LogAction = function (message, description, groupName) {
        var data = {
            LogDate: new Date().toISOString(),
            GroupName: groupName,
            Uri: window.location.href,
            Description: description,
            Message: message
        };
        console.log("Logger.LogAction", data);
        $.ajax({
            type: "POST",
            data: data,
            url: "/Api/Log/Action",
            async: true,
            cache: false,
            success: function (response) {
                console.log("Действие залоггировано", response);
            },
            error: function () {
                alert("Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения");
            }
        });
    };
    return Logger;
}());

var ModalWorker = /** @class */ (function () {
    function ModalWorker() {
    }
    /** Показать модальное окно по идентификатору. */
    ModalWorker.ShowModal = function (modalId) {
        if (modalId == "" || modalId == null || modalId == undefined) {
            modalId = "loadingModal";
        }
        else {
            $('#' + modalId).modal('show');
        }
    };
    ModalWorker.ChangeModalValues = function (modalId, modalHeader, modalContent) {
        if (modalHeader != null) {
            document.getElementById("header" + modalId).innerText = modalHeader;
        }
        if (modalContent != null) {
            document.getElementById("body" + modalId).innerHTML = modalContent;
        }
    };
    ModalWorker.ShowModalWithValues = function (modalId, modalHeader, modalContent) {
        this.ChangeModalValues(modalId, modalHeader, modalContent);
        this.ShowModal(modalId);
    };
    ModalWorker.HideModals = function () {
        $('.modal').modal('hide');
        $(".modal-backdrop.fade").remove();
        $('.modal').on('shown.bs.modal', function () {
        });
    };
    ModalWorker.HideModal = function (modalId) {
        $("#" + modalId).modal('hide');
    };
    return ModalWorker;
}());

var AjaxParameters = /** @class */ (function () {
    function AjaxParameters() {
    }
    return AjaxParameters;
}());
var Requester = /** @class */ (function () {
    function Requester() {
    }
    Requester.SendPostRequestWithAnimation = function (link, data, onSuccessFunc, onErrorFunc) {
        //Показываю крутилку
        ModalWorker.ShowModal("loadingModal");
        Requester.SendAjaxPost(link, data, onSuccessFunc, onErrorFunc, true);
    };
    Requester.UploadFilesToServer = function (inputId, onSuccessFunc, onErrorFunc) {
        var link = "/Api/FilesDirectory/UploadFiles";
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        var file_data = $("#" + inputId).prop("files");
        var form_data = new FormData();
        if (file_data.length === 0) {
            ToastrWorker.HandleBaseApiResponse(new BaseApiResponse(false, "Файлы не выбраны"));
            return;
        }
        for (var i = 0; i < file_data.length; i++) {
            form_data.append("Files", file_data[i]);
        }
        $.ajax({
            url: link,
            type: "POST",
            data: form_data,
            async: true,
            cache: false,
            dataType: "json",
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response);
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR, textStatus, errorThrown);
                //Логгирую ошибку
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                ModalWorker.HideModals();
                var resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, скоро с ней разберемся!");
                ToastrWorker.HandleBaseApiResponse(resp);
                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        });
    };
    Requester.SendAjaxGet = function (link, data, onSuccessFunc, onErrorFunc) {
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        var params = {
            type: "GET",
            data: data,
            url: link,
            async: true,
            cache: false,
            success: function (response) {
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //Логгирую ошибку
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        };
        $.ajax(params);
    };
    Requester.SendAjaxPost = function (link, data, onSuccessFunc, onErrorFunc, animations) {
        if (Requester.IsRequestGoing(link)) {
            return;
        }
        if (data == null) {
            alert("Вы подали пустой объект в запрос");
            return;
        }
        var params = {};
        params.type = "POST";
        params.data = data;
        params.url = link;
        params.async = true;
        params.cache = false;
        params.success = function (response) {
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnSuccessAnimationHandler(response);
            }
            if (onSuccessFunc) {
                onSuccessFunc(response);
            }
        };
        params.error = function (jqXHR, textStatus, errorThrown) {
            //Логгирую ошибку
            Logger.LogException(textStatus, link);
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnErrorAnimationHandler();
            }
            //Вызываю внешнюю функцию-обработчик
            if (onErrorFunc) {
                onErrorFunc(jqXHR, textStatus, errorThrown);
            }
        };
        var isArray = data.constructor === Array;
        if (isArray) {
            params.contentType = "application/json; charset=utf-8";
            params.dataType = "json";
            params.data = JSON.stringify(data);
        }
        Requester.GoingRequests.push(link);
        $.ajax(params);
        console.log("POST \u0437\u0430\u043F\u0440\u043E\u0441 " + link, data);
    };
    Requester.GoingRequests = new Array();
    Requester.DeleteCompletedRequest = function (link) {
        Requester.GoingRequests = Requester.GoingRequests.filter(function (x) { return x !== link; });
    };
    Requester.ParseDate = function (date) {
        var from = date.split(".");
        var d = new Date(+from[2], +from[1] - 1, +from[0]);
        return d.toISOString();
    };
    Requester.GetCombinedData = function (prefix, obj) {
        var resultObj = {};
        for (var prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                var objWithProps = Requester.GetCombinedData("" + prefix + prop + ".", obj[prop]);
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
    Requester.GetParams = function (obj) {
        obj = Requester.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    Requester.IsRequestGoing = function (link) {
        var any = Requester.GoingRequests.filter(function (x) { return x === link; });
        return any.length > 0;
    };
    Requester.OnSuccessAnimationHandler = function (data) {
        ModalWorker.HideModals();
        ToastrWorker.HandleBaseApiResponse(data);
    };
    Requester.OnErrorAnimationHandler = function () {
        ModalWorker.HideModals();
        var resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!");
        ToastrWorker.HandleBaseApiResponse(resp);
    };
    return Requester;
}());

var BaseApiResponse = /** @class */ (function () {
    function BaseApiResponse(isSucceeded, message) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
    }
    return BaseApiResponse;
}());
var ToastrWorker = /** @class */ (function () {
    function ToastrWorker() {
    }
    ToastrWorker.ShowError = function (text) {
        var data = {
            IsSucceeded: false,
            Message: text
        };
        this.HandleBaseApiResponse(data);
    };
    ToastrWorker.ShowSuccess = function (text) {
        var data = {
            IsSucceeded: true,
            Message: text
        };
        this.HandleBaseApiResponse(data);
    };
    ToastrWorker.HandleBaseApiResponse = function (data) {
        console.log("HandleBaseApiResponse", data);
        if (data.IsSucceeded === undefined) {
            alert("Произошла ошибка. Объект не является типом BaseApiResponse");
            return;
        }
        if (data.Message === undefined) {
            alert("Произошла ошибка. Объект не является типом BaseApiResponse");
            return;
        }
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        if (data.IsSucceeded) {
            toastr.success(data.Message);
        }
        else {
            toastr.error(data.Message);
        }
    };
    return ToastrWorker;
}());

/// <reference path="../../node_modules/@types/bootstrap/index.d.ts"/>
/// <reference path="../../node_modules/@types/bootstrap-datepicker/index.d.ts"/>
var Utils = /** @class */ (function () {
    function Utils() {
    }
    Utils.SetDatePicker = function (selector, startDate) {
        if (startDate === void 0) { startDate = null; }
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
        $(selector).datepicker({
            format: "dd/mm/yyyy",
            autoclose: true,
            language: "ru",
            startDate: startDate
        });
    };
    Utils.SetDateRangePicker = function (selector) {
        $(selector).daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: "Очистить",
                applyLabel: "Применить",
                daysOfWeek: [
                    "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"
                ],
                monthNames: ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                    "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"],
                firstDay: 0
            }
        });
        $(selector).on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format("DD/MM/YYYY") + " - " + picker.endDate.format("DD/MM/YYYY"));
        });
        $(selector).on("cancel.daterangepicker", function (ev, picker) {
            $(this).val("");
        });
    };
    Utils.GetDateFromDateRangePicker = function (inputId) {
        var inputDate = document.getElementById(inputId);
        if (inputDate === null) {
            alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + inputId + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u043F\u0440\u043E\u0432\u0435\u0440\u044C\u0442\u0435 \u043A\u043E\u0434");
            return null;
        }
        var dates = inputDate.value.replace(/ /g, "");
        dates = dates.split('-');
        if (dates !== "") {
            for (var d in dates) {
                var tempStr = dates[d].split('/');
                tempStr.reverse();
                dates[d] = tempStr.join('-');
            }
            ;
            var data = {};
            if (dates[0] !== "") {
                data = {
                    Min: dates[0]
                };
            }
            if (dates[1] != undefined)
                data.Max = dates[1];
            console.log(data, dates[0], dates[1]);
            return (data);
        }
        else {
            return null;
        }
    };
    Utils.GetDateFromDatePicker = function (inputId) {
        var inputDate = document.getElementById(inputId);
        if (inputDate === []) {
            alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + inputId + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u043F\u0440\u043E\u0432\u0435\u0440\u044C\u0442\u0435 \u043A\u043E\u0434");
            return;
        }
        var date = inputDate.value.replace(/ /g, "");
        if (date != "") {
            console.log("Not ''");
            var tempStr = date.split('/');
            tempStr.reverse();
            date = tempStr.join('-');
            return (date);
        }
        else {
            return;
        }
    };
    Utils.FillSelect = function (select, array, htmlFunc, valueFunc) {
        for (var i = 0; i < array.length; i++) {
            var opt = document.createElement("option");
            opt.innerHTML = htmlFunc(array[i]);
            opt.value = valueFunc(array[i]);
            select.append(opt);
        }
    };
    return Utils;
}());