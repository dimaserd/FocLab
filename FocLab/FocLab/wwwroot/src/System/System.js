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
