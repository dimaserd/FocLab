var HtmlDrawHelper = (function () {
    function HtmlDrawHelper() {
    }
    HtmlDrawHelper.RenderInput = function (type, attrs) {
        var attrString = HtmlDrawHelper.RenderAttributesString(attrs);
        return "<input type=" + type + " " + attrString + "/>";
    };
    HtmlDrawHelper.RenderAttributesString = function (attrs) {
        var result = "";
        if (attrs == null) {
            return result;
        }
        for (var _i = 0, _a = Array.from(attrs.keys()); _i < _a.length; _i++) {
            var key = _a[_i];
            var res = attrs.get(key);
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
        var select = "<select" + attrStr + " class=\"" + className + "\" name=\"" + propName + "\">";
        for (var i = 0; i < selectList.length; i++) {
            var item = selectList[i];
            var selected = item.Selected ? " selected=\"selected\"" : '';
            select += "<option" + selected + " value=\"" + item.Value + "\">" + item.Text + "</option>";
        }
        select += "</select>";
        return select;
    };
    return HtmlDrawHelper;
}());
