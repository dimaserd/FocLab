var HtmlDrawHelper = /** @class */ (function () {
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
            //Заплатка для выпадающего списка 
            //TODO Вылечить это
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
