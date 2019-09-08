interface GenericUserInterfaceValueProvider {
    Singles: Array<GenericUserInterfacePropertySingleValue>;
    Arrays: Array<GenericUserInterfacePropertyListValue>;
}
interface GenericUserInterfacePropertyListValue {
    PropertyName: string;
    Value: Array<string>;
}
