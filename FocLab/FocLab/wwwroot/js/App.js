var AccountWorker = (function () {
    function AccountWorker() {
    }
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
    AccountWorker.User = null;
    return AccountWorker;
}());
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser(); }, 1100);
});

var DatePickerUtils = (function () {
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
        $(selector).on("change", function (e) {
            var comingValue = e.target.value;
            var dateValue = new Date(DatePickerUtils.DDMMYYYYToMMDDDDYYYYString(comingValue));
            if (isNaN(dateValue.getTime())) {
                dateValue = null;
            }
            var valueToSet = dateValue == null ? CrocoAppCore.Application.FormDataHelper.NullValue
                : new Date(dateValue.getTime() - (dateValue.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
            console.log("Utils.DatePickerValueChanged", dateValue, valueToSet);
            document.getElementById(elementIdToUpdate).value = valueToSet;
        });
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

var ImageSizeType;
(function (ImageSizeType) {
    ImageSizeType[ImageSizeType["Icon"] = "Icon"] = "Icon";
    ImageSizeType[ImageSizeType["Medium"] = "Medium"] = "Medium";
    ImageSizeType[ImageSizeType["Small"] = "Small"] = "Small";
    ImageSizeType[ImageSizeType["Original"] = "Original"] = "Original";
})(ImageSizeType || (ImageSizeType = {}));

CrocoAppCore.InitFields();
CrocoAppCore.GenericInterfaceHelper.FormHelper.DrawForms();
setTimeout(function () {
    NotificationWorker.GetNotification();
}, 1000);


var NotificationWorker = (function () {
    function NotificationWorker() {
    }
    NotificationWorker.GetNotification = function () {
        CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/GetLast", {}, function (x) {
            console.log("GetNotification", x);
            if (x === null || x === undefined) {
                return;
            }
            CrocoAppCore.ToastrWorker.ShowSuccess(x.Text);
            setTimeout(function () {
                console.log("Marking notification");
                CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/Read", { Id: x.Id }, function () { }, null);
            }, 1000);
        }, null);
    };
    return NotificationWorker;
}());
var UserNotificationType;
(function (UserNotificationType) {
    UserNotificationType[UserNotificationType["Success"] = 'Success'] = "Success";
    UserNotificationType[UserNotificationType["Info"] = 'Info'] = "Info";
    UserNotificationType[UserNotificationType["Warning"] = 'Warning'] = "Warning";
    UserNotificationType[UserNotificationType["Danger"] = 'Danger'] = "Danger";
    UserNotificationType[UserNotificationType["Custom"] = 'Custom'] = "Custom";
})(UserNotificationType || (UserNotificationType = {}));

var TimePickerUtils = (function () {
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

var Utils = (function () {
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