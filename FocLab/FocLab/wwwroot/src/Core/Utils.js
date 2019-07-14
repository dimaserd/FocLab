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
            return null;
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
    return Utils;
}());
