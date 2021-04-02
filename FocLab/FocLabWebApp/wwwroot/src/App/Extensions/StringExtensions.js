String.prototype.format = function () {
    var params = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        params[_i] = arguments[_i];
    }
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof params[number] != 'undefined' ? params[number] : match;
    });
};
