declare class FormDrawImplementation implements IFormDraw {
    constructor(model: GenerateGenericUserInterfaceModel);
    _model: GenerateGenericUserInterfaceModel;
    _datePickerPropNames: Array<string>;
    _selectClass: string;
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    static InitCalendarForPrefixedProperty(prefixedPropName: string): void;
    private GetPropertyValueName;
    RenderDatePicker(typeDescription: CrocoTypeDescription): string;
    RenderHidden(typeDescription: CrocoTypeDescription): string;
    RenderTextBox(typeDescription: CrocoTypeDescription): string;
    RenderTextArea(typeDescription: CrocoTypeDescription): string;
    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean): string;
    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
}
