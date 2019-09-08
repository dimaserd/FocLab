interface IFormDraw {
    RenderTextBox(typeDescription: CrocoTypeDescription): string;
    RenderTextArea(typeDescription: CrocoTypeDescription): string;
    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string;
    RenderHidden(typeDescription: CrocoTypeDescription): string;
    RenderDatePicker(typeDescription: CrocoTypeDescription): string;
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
}
