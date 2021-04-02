var PropertyFormTypeSearcher = (function () {
    function PropertyFormTypeSearcher() {
    }
    PropertyFormTypeSearcher.FindPropByNameInOneDimension = function (type, propName) {
        return type.Properties.find(function (x) { return x.PropertyDescription.PropertyName === propName; });
    };
    PropertyFormTypeSearcher.FindPropByName = function (type, propName) {
        if (propName.includes(".")) {
            var indexOfFirstDot = propName.indexOf(".");
            var fBit = propName.slice(0, indexOfFirstDot);
            var anotherBit = propName.slice(indexOfFirstDot + 1, propName.length);
            var innerProp = PropertyFormTypeSearcher.FindPropByNameInOneDimension(type, fBit);
            return PropertyFormTypeSearcher.FindPropByName(innerProp, anotherBit);
        }
        var prop = PropertyFormTypeSearcher.FindPropByNameInOneDimension(type, propName);
        if (prop == null) {
            throw new Error("\u0421\u0432\u043E\u0439\u0441\u0442\u0432\u043E " + propName + " \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E");
        }
        return prop;
    };
    return PropertyFormTypeSearcher;
}());
