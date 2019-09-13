declare class FormDrawImplementation implements IFormDraw {
    constructor(model: GenerateGenericUserInterfaceModel);
    _model: GenerateGenericUserInterfaceModel;
    _datePickerPropNames: Array<string>;
    _selectClass: string;
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    static InitCalendarForPrefixedProperty(prefixedPropName: string): void;
    RenderDatePicker(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderHidden(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderTextBox(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderTextArea(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean, wrap: boolean): string;
    private WrapInForm;
    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string;
    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string;
}
