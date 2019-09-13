class FormDrawHelper {

    static FormPropertyName = "form-property-name";
    static FormModelPrefix = "form-model-prefix";

    static GetPropertyValueName(propertyName: string, modelPrefix: string): string {
        return `${modelPrefix}${propertyName}`;
    }

    static GetPropertySelector(propertyName: string, modelPrefix: string): string {
        const prefixedPropName = FormDrawHelper.GetPropertyValueName(propertyName, modelPrefix);

        return `input[name='${prefixedPropName}']`;
    }

    static GetOuterFormElement(propertyName: string, modelPrefix: string): Element {
        return document.querySelector(`[${FormDrawHelper.FormPropertyName}="${propertyName}"][${FormDrawHelper.FormModelPrefix}="${modelPrefix}"]`);
    }

    static GetOuterFormAttributes(propertyName: string, modelPrefix: string): string {
        return `${FormDrawHelper.FormPropertyName}="${propertyName}" ${FormDrawHelper.FormModelPrefix}="${modelPrefix}"`;
    }
}