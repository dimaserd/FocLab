var FormDrawHelper = (function () {
    function FormDrawHelper() {
    }
    FormDrawHelper.GetPropertyValueName = function (propertyName, modelPrefix) {
        return "" + modelPrefix + propertyName;
    };
    FormDrawHelper.GetOuterFormElement = function (propertyName, modelPrefix) {
        return document.querySelector("[" + FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\"][" + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"]");
    };
    FormDrawHelper.GetOuterFormAttributes = function (propertyName, modelPrefix) {
        return FormDrawHelper.FormPropertyName + "=\"" + propertyName + "\" " + FormDrawHelper.FormModelPrefix + "=\"" + modelPrefix + "\"";
    };
    FormDrawHelper.GetInputTypeHidden = function (propName, value, otherProps) {
        if (otherProps === void 0) { otherProps = null; }
        var t = otherProps != null ? otherProps : new Map();
        t.set("value", value);
        t.set("name", propName);
        return HtmlDrawHelper.RenderInput("hidden", t);
    };
    FormDrawHelper.FormPropertyName = "form-property-name";
    FormDrawHelper.FormModelPrefix = "form-model-prefix";
    return FormDrawHelper;
}());
