class FormDrawHelper {

    static FormPropertyName: string = "form-property-name";
    static FormModelPrefix: string = "form-model-prefix";

    static GetPropertyValueName(propertyName: string, modelPrefix: string): string {
        return `${modelPrefix}${propertyName}`;
    }

    static GetOuterFormElement(propertyName: string, modelPrefix: string): Element {
        return document.querySelector(`[${FormDrawHelper.FormPropertyName}="${propertyName}"][${FormDrawHelper.FormModelPrefix}="${modelPrefix}"]`)
    }

    static GetOuterFormAttributes(propertyName: string, modelPrefix: string): string {
        return `${FormDrawHelper.FormPropertyName}="${propertyName}" ${FormDrawHelper.FormModelPrefix}="${modelPrefix}"`;
    }
}