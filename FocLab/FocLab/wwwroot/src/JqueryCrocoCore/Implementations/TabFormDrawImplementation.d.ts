declare class TabFormDrawImplementation implements IFormDraw {
    constructor(model: GenerateGenericUserInterfaceModel);
    _drawHelper: HtmlSelectDrawHelper;
    _model: GenerateGenericUserInterfaceModel;
    _datePickerPropNames: Array<string>;
    _selectClass: string;
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    RenderTextBox(typeDescription: CrocoTypeDescription): string;
    RenderTextArea(typeDescription: CrocoTypeDescription): string;
    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean): string;
    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
    RenderHidden(typeDescription: CrocoTypeDescription): string;
    RenderDatePicker(typeDescription: CrocoTypeDescription): string;
    private GetPropertyValueName;
}
