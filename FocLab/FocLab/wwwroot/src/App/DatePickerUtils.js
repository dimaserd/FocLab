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
