declare class FormDrawHelper {
    static FormPropertyName: string;
    static FormModelPrefix: string;
    static GetPropertyValueName(propertyName: string, modelPrefix: string): string;
    static GetOuterFormElement(propertyName: string, modelPrefix: string): Element;
    static GetOuterFormAttributes(propertyName: string, modelPrefix: string): string;
    static GetInputTypeHidden(propName: string, value: string, otherProps?: Map<string, string>): string;
}
