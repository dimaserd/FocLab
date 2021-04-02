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
