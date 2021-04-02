declare class FormDrawImplementation implements IFormDraw {
    _htmlDrawHelper: HtmlSelectDrawHelper;
    constructor(model: GenerateGenericUserInterfaceModel);
    _model: GenerateGenericUserInterfaceModel;
    _datePickerPropNames: Array<string>;
    _selectClass: string;
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    GetPropertyName(propName: string): string;
    GetPropertyBlock(propertyName: string): UserInterfaceBlock;
    static GetElementIdForFakeCalendar(modelPrefix: string, propName: string): string;
    static GetElementIdForRealCalendarBackProperty(modelPrefix: string, propName: string): string;
    static InitCalendarForPrefixedProperty(modelPrefix: string, propName: string): void;
    GetPropValue(typeDescription: CrocoTypeDescription): string;
    RenderDatePicker(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderHidden(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderTextBox(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderTextBoxInner(typeDescription: CrocoTypeDescription, wrap: boolean, id: string, propName: string): string;
    RenderTextArea(typeDescription: CrocoTypeDescription, wrap: boolean): string;
    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean, wrap: boolean): string;
    private WrapInForm;
    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string;
    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string;
}
