var FormTypeAfterDrawnDrawer = (function () {
    function FormTypeAfterDrawnDrawer() {
    }
    FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty = function (propertyName, modelPrefix, innerHtml) {
        var elem = FormDrawHelper.GetOuterFormElement(propertyName, modelPrefix);
        console.log("SetInnerHtmlForProperty elem", elem);
        elem.innerHTML = innerHtml;
    };
    FormTypeAfterDrawnDrawer.SetSelectListForProperty = function (propertyName, modelPrefix, selectList) {
        var t = TryForm._genericInterfaces.find(function (x) { return x.Prefix === modelPrefix; });
        var prop = t.TypeDescription.Properties.find(function (x) { return x.PropertyName === propertyName; });
        var drawer = new FormDrawImplementation(t);
        var html = drawer.RenderDropDownList(prop, selectList, false);
        FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);
        drawer.AfterFormDrawing();
    };
    return FormTypeAfterDrawnDrawer;
}());
