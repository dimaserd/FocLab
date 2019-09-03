var HtmlDrawHelper = /** @class */ (function () {
    function HtmlDrawHelper() {
    }
    HtmlDrawHelper.RenderSelect = function (className, propName, selectList) {
        var select = "<select class=\"" + className + "\" name=\"" + propName + "\">";
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
