var Dictionary = /** @class */ (function () {
    function Dictionary(init) {
        this._keys = [];
        this._values = [];
        if (init) {
            for (var x = 0; x < init.length; x++) {
                this[init[x].key] = init[x].value;
                this._keys.push(init[x].key);
                this._values.push(init[x].value);
            }
        }
    }
    Dictionary.prototype.getByKey = function (key) {
        var index = this._keys.indexOf(key, 0);
        if (index > 0) {
            return this._values[index];
        }
        return null;
    };
    Dictionary.prototype.add = function (key, value) {
        if (this.containsKey(key)) {
            throw new DOMException("\u041A\u043B\u044E\u0447 " + key + " \u0443\u0436\u0435 \u0441\u0443\u0449\u0435\u0441\u0442\u0432\u0443\u0435\u0442 \u0432 \u0434\u0430\u043D\u043D\u043E\u043C \u0441\u043B\u043E\u0432\u0430\u0440\u0435");
        }
        this[key] = value;
        this._keys.push(key);
        this._values.push(value);
    };
    Dictionary.prototype.remove = function (key) {
        var index = this._keys.indexOf(key, 0);
        this._keys.splice(index, 1);
        this._values.splice(index, 1);
        delete this[key];
    };
    Dictionary.prototype.keys = function () {
        return this._keys;
    };
    Dictionary.prototype.values = function () {
        return this._values;
    };
    Dictionary.prototype.containsKey = function (key) {
        if (typeof this[key] === "undefined") {
            return false;
        }
        return true;
    };
    Dictionary.prototype.toLookup = function () {
        return this;
    };
    return Dictionary;
}());

var AccountWorker = (function () {
    function AccountWorker() {
    }
    AccountWorker.User = null;
    AccountWorker.CheckUser = function () {
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

var AjaxLoader = (function () {
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
                alert("There is an execption while executing request " + link);
                console.log(xhr);
            }
        });
    };
    return AjaxLoader;
}());
AjaxLoader.InitAjaxLoads();

var CookieWorker = (function () {
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

var EditableComponents = (function () {
    function EditableComponents() {
    }
    EditableComponents.InitEditable = function (element, onValueChangedHandler, isTextArea) {
        if (isTextArea === void 0) { isTextArea = false; }
        var elementId = Math.random().toString(36);
        element.style.display = "none";
        element.setAttribute("data-editable-real-input", elementId);
        EditableComponents.Editables.push({
            ElementId: elementId,
            Value: element.value,
            OnValueChangedHandler: onValueChangedHandler
        });
        var newNode = EditableComponents.GetFakeElement(element, elementId, isTextArea);
        element.before(newNode);
        this.InitEditableInner(elementId);
        this.InitLiasoningOnChange(element, elementId);
    };
    EditableComponents.InitLiasoningOnChange = function (element, elementId) {
        element.addEventListener("change", function (e) {
            var elemId = elementId;
            var fakeElement = document.querySelectorAll("[data-editable-input=\"" + elemId + "\"]")[0];
            fakeElement.value = e.target.value;
        });
        var fakeElement = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        fakeElement.addEventListener("change", function (x) {
            var elemId = elementId;
            var realElement = document.querySelectorAll("[data-editable-real-input=\"" + elemId + "\"]")[0];
            realElement.value = x.target.value;
        });
    };
    EditableComponents.GetFakeElement = function (element, elementId, isTextArea) {
        var div = document.createElement("div");
        var html = "<div class=\"form-group editable\" data-editable-form=\"" + elementId + "\">\n        <div class=\"editable-backdrop\" data-editable-backdrop=\"" + elementId + "\"></div>\n        " + EditableComponents.GetFakeInputElement(element, elementId, isTextArea) + "\n        <div class=\"editable-buttons\">\n            <button class=\"btn btn-sm btn-editable\" data-editable-check=\"" + elementId + "\"><i class=\"fas fa-check\"></i></button>\n            <button class=\"btn btn-sm btn-editable\" data-editable-cancel=\"" + elementId + "\"><i class=\"fas fa-times\"></i></button>\n        </div>\n        </div>";
        div.innerHTML = html;
        return div;
    };
    EditableComponents.GetFakeInputElement = function (element, elementId, isTextArea) {
        if (!isTextArea) {
            return "<input type=\"text\" class=\"" + element.className + " editable-input\" name=\"" + element.id + "\" data-editable-input=\"" + elementId + "\"\n                    data-editable-input-id=\"" + elementId + "\" value=\"" + element.value + "\">";
        }
        return "<textarea class=\"" + element.className + " editable-input\" name=\"" + element.id + "\" data-editable-input=\"" + elementId + "\"\n                    data-editable-input-id=\"" + elementId + "\">" + element.value + "</textarea>";
    };
    EditableComponents.InitEditableInner = function (elementId) {
        var _this = this;
        var element = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        element.addEventListener("click", function (x) { return _this.OnInputClickHandler(x.target); }, false);
        var btnCheck = document.querySelectorAll("[data-editable-check=\"" + elementId + "\"]")[0];
        btnCheck.addEventListener("click", (function () { this.OnBtnCheckClickHandler(elementId); }).bind(this));
        var btnBack = document.querySelectorAll("[data-editable-cancel=\"" + elementId + "\"]")[0];
        btnBack.addEventListener("click", (function () { this.OnBtnCancelClickHandler(elementId); }).bind(this));
        var backDrop = document.querySelectorAll("[data-editable-backdrop=\"" + elementId + "\"]")[0];
        backDrop.addEventListener("click", (function () { this.BackDropClickHandler(elementId); }).bind(this));
    };
    EditableComponents.CheckValueChanged = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        var record = EditableComponents.Editables.filter(function (x) { return x.ElementId === elementId; })[0];
        var newValue = input.value;
        if (record.Value !== newValue) {
            if (record.OnValueChangedHandler) {
                record.OnValueChangedHandler(newValue);
            }
            record.Value = newValue;
            EditableComponents.Editables = this.Editables.filter(function (x) { return x.ElementId !== elementId; });
            EditableComponents.Editables.push(record);
        }
    };
    EditableComponents.BackDropClickHandler = function (elementId) {
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        EditableComponents.CheckValueChanged(elementId);
    };
    EditableComponents.OnInputClickHandler = function (x) {
        var elementId = x.getAttribute("data-editable-input-id");
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.setAttribute("editable", "editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.add("editable--active");
    };
    EditableComponents.OnBtnCheckClickHandler = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        EditableComponents.CheckValueChanged(elementId);
    };
    EditableComponents.OnBtnCancelClickHandler = function (elementId) {
        var input = document.querySelectorAll("[data-editable-input=\"" + elementId + "\"]")[0];
        input.removeAttribute("editable");
        var form = document.querySelectorAll("[data-editable-form=\"" + elementId + "\"]")[0];
        form.classList.remove("editable--active");
        var record = EditableComponents.Editables.find(function (x) { return x.ElementId === elementId; });
        input.value = record.Value;
    };
    EditableComponents.Editables = [];
    return EditableComponents;
}());

var FormDataHelper = (function () {
    function FormDataHelper() {
    }
    FormDataHelper.FillData = function (object) {
        FormDataHelper.FillDataByPrefix(object, "");
    };
    FormDataHelper.FillDataByPrefix = function (object, prefix) {
        for (var index in object) {
            var name_1 = prefix + index;
            var element = document.getElementsByName(name_1)[0];
            if (element === null || element === undefined) {
                continue;
            }
            if (Array.isArray(object[index])) {
                if (element.type !== "select-multiple") {
                    alert("An attempt to set an array to HTMLInputElement which is not a select with multiple attribute");
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
                document.querySelector("input[name=" + name_1 + "][value=" + object[index] + "]").checked = true;
            }
            else {
                element.value = object[index];
            }
            var event_2 = new Event("change");
            element.dispatchEvent(event_2);
        }
    };
    FormDataHelper.CollectData = function (object) {
        return FormDataHelper.CollectDataByPrefix(object, "");
    };
    FormDataHelper.CollectDataByPrefix = function (object, prefix) {
        for (var index in object) {
            if (object.hasOwnProperty(index)) {
                var name_2 = prefix + index;
                var element = document.getElementsByName(name_2)[0];
                if (element == null) {
                    alert("Element with name " + name_2 + " not found check the source code");
                    continue;
                }
                if (element.type === "select-multiple") {
                    object[index] = Array.apply(null, element.options)
                        .filter(function (option) { return option.selected; })
                        .map(function (option) { return option.value; });
                    continue;
                }
                if (element.type === "radio") {
                    var value = document.querySelector("input[name=\"" + name_2 + "\"]:checked") != null;
                    if (value)
                        (object[index] = document.querySelector("input[name=\"" + name_2 + "\"]:checked")).value;
                    continue;
                }
                object[index] = element.type === "checkbox" ? element.checked : element.value;
            }
        }
        return object;
    };
    return FormDataHelper;
}());

var Logger_Resx = (function () {
    function Logger_Resx() {
        this.LoggingAttempFailed = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";
        this.ErrorOnApiRequest = "Ошибка запроса к апи";
        this.ActionLogged = "Action logged";
        this.ExceptionLogged = "Исключение залоггировано";
        this.ErrorOccuredOnLoggingException = "Произошла ошибка в логгировании ошибки, срочно обратитесь к разработчикам приложения";
    }
    return Logger_Resx;
}());
var Logger = (function () {
    function Logger() {
    }
    Logger.LogException = function (exception, link) {
        $.ajax({
            type: "POST",
            data: {
                ExceptionDate: new Date().toISOString(),
                Description: Logger.Resourcses.ErrorOnApiRequest,
                Message: exception,
                Uri: link !== null ? link : location.href
            },
            url: "/Api/Log/Exception",
            async: true,
            cache: false,
            success: function (data) {
                console.log(Logger.Resourcses.ExceptionLogged, data);
            },
            error: function () {
                alert(Logger.Resourcses.ErrorOccuredOnLoggingException);
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
                console.log(Logger.Resourcses.ActionLogged, response);
            },
            error: function () {
                alert(Logger.Resourcses.LoggingAttempFailed);
            }
        });
    };
    Logger.Resourcses = new Logger_Resx();
    return Logger;
}());

var ModalWorker = (function () {
    function ModalWorker() {
    }
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

var Requester_Resx = (function () {
    function Requester_Resx() {
        this.YouPassedAnEmtpyArrayOfObjects = "Вы подали пустой объект в запрос";
        this.ErrorOccuredWeKnowAboutIt = "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!";
        this.FilesNotSelected = "Файлы не выбраны";
    }
    return Requester_Resx;
}());
var Requester = (function () {
    function Requester() {
    }
    Requester.ParseDate = function (date) {
        date = date.replace(new RegExp("/", 'g'), ".");
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
    Requester.SendPostRequestWithAnimation = function (link, data, onSuccessFunc, onErrorFunc) {
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
            ToastrWorker.HandleBaseApiResponse(new BaseApiResponse(false, Requester.Resources.FilesNotSelected));
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
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
                ModalWorker.HideModals();
                var resp = new BaseApiResponse(false, Requester.Resources.ErrorOccuredWeKnowAboutIt);
                ToastrWorker.HandleBaseApiResponse(resp);
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }
        });
    };
    Requester.IsRequestGoing = function (link) {
        var index = Requester.GoingRequests.indexOf(link);
        return index >= 0;
    };
    Requester.OnSuccessAnimationHandler = function (data) {
        ModalWorker.HideModals();
        ToastrWorker.HandleBaseApiResponse(data);
    };
    Requester.OnErrorAnimationHandler = function () {
        ModalWorker.HideModals();
        var resp = new BaseApiResponse(false, Requester.Resources.ErrorOccuredWeKnowAboutIt);
        ToastrWorker.HandleBaseApiResponse(resp);
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
                Logger.LogException(textStatus, link);
                Requester.DeleteCompletedRequest(link);
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
            alert(Requester.Resources.YouPassedAnEmtpyArrayOfObjects);
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
            Logger.LogException(textStatus, link);
            Requester.DeleteCompletedRequest(link);
            if (animations) {
                Requester.OnErrorAnimationHandler();
            }
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
    };
    Requester.Resources = new Requester_Resx();
    Requester.GoingRequests = new Array();
    Requester.DeleteCompletedRequest = function (link) {
        Requester.GoingRequests = Requester.GoingRequests.filter(function (x) { return x !== link; });
    };
    Requester.GetParams = function (obj) {
        obj = Requester.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    return Requester;
}());

var GenericBaseApiResponse = (function () {
    function GenericBaseApiResponse(isSucceeded, message, resp) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
        this.ResponseObject = resp;
    }
    return GenericBaseApiResponse;
}());
var BaseApiResponse = (function () {
    function BaseApiResponse(isSucceeded, message) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
    }
    return BaseApiResponse;
}());
var ToastrWorker = (function () {
    function ToastrWorker() {
    }
    ToastrWorker.ShowError = function (text) {
        var data = {
            IsSucceeded: false,
            Message: text
        };
        ToastrWorker.HandleBaseApiResponse(data);
    };
    ToastrWorker.ShowSuccess = function (text) {
        var data = {
            IsSucceeded: true,
            Message: text
        };
        ToastrWorker.HandleBaseApiResponse(data);
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

var Utils = (function () {
    function Utils() {
    }
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
    ;
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
            return data;
        }
        else {
            return null;
        }
    };
    ;
    Utils.GetDateFromDatePicker = function (inputId) {
        var inputDate = document.getElementById(inputId);
        if (inputDate === []) {
            alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u0441 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u043E\u043C " + inputId + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0435 \u043F\u0440\u043E\u0432\u0435\u0440\u044C\u0442\u0435 \u043A\u043E\u0434");
            return null;
        }
        var date = inputDate.value.replace(/ /g, "");
        if (date !== "") {
            console.log("Not ''");
            var tempStr = date.split('/');
            tempStr.reverse();
            date = tempStr.join('-');
            return (date);
        }
        else {
            return null;
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
    Utils.GetImageLinkByFileId = function (fileId, sizeType) {
        return "/FileCopies/Images/" + sizeType.toString() + "/" + fileId + ".jpg";
    };
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
    return Utils;
}());
var ImageSizeType;
(function (ImageSizeType) {
    ImageSizeType[ImageSizeType["Icon"] = "Icon"] = "Icon";
    ImageSizeType[ImageSizeType["Medium"] = "Medium"] = "Medium";
    ImageSizeType[ImageSizeType["Small"] = "Small"] = "Small";
    ImageSizeType[ImageSizeType["Original"] = "Original"] = "Original";
})(ImageSizeType || (ImageSizeType = {}));

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
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + "\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderTextBox = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n                <input autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" type=\"text\" value=\"" + value + "\">";
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n                </div>";
    };
    FormDrawImplementation.prototype.RenderTextArea = function (typeDescription, wrap) {
        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);
        var styles = "style=\"margin-top: 0px; margin-bottom: 0px; height: 79px;\"";
        var html = "<label for=\"" + typeDescription.PropertyName + "\">" + typeDescription.PropertyDisplayName + "</label>\n            <textarea autocomplete=\"false\" class=\"form-control m-input\" name=\"" + FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix) + "\" rows=\"3\" " + styles + ">" + value + "</textarea>";
        if (!wrap) {
            return html;
        }
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                      " + html + "\n                </div>";
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
        return "<div class=\"form-group m-form__group\" " + FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix) + ">\n                    " + html + "\n            </div>";
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
                    initData[prop.PropertyName] = initData[prop.PropertyName].toLowerCase() == "true";
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
        var model = TryForm._genericInterfaces.find(function (x) { return x.Prefix == modelPrefix; });
        if (model == null) {
        }
        return TryForm.GetDataForForm(model);
    };
    TryForm.GetDataForFirstForm = function () {
        if (TryForm._genericInterfaces.length == 0) {
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