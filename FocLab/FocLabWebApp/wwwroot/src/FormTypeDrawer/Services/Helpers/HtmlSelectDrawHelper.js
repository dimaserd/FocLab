var HtmlSelectDrawHelper = (function () {
    function HtmlSelectDrawHelper(nullValue) {
        this.NullValue = nullValue;
    }
    HtmlSelectDrawHelper.prototype.ProccessSelectValues = function (typeDescription, rawValue, selectList) {
        var _this = this;
        selectList.forEach(function (x) {
            if (x.Value == null) {
                x.Value = _this.NullValue;
            }
        });
        if (rawValue != null) {
            selectList.forEach(function (x) { return x.Selected = false; });
            var item = typeDescription.TypeName == CSharpType.Boolean.toString() ?
                selectList.find(function (x) { return x.Value.toLowerCase() == rawValue.toLowerCase(); }) :
                selectList.find(function (x) { return x.Value == rawValue; });
            if (item != null) {
                item.Selected = true;
            }
        }
    };
    return HtmlSelectDrawHelper;
}());
