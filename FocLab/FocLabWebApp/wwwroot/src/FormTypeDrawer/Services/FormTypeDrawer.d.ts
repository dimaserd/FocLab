declare class FormTypeDrawer {
    _formDrawer: IFormDraw;
    _typeDescription: CrocoTypeDescription;
    constructor(formDrawer: IFormDraw, typeDescription: CrocoTypeDescription);
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    TextBoxFor(propertyName: string, wrap: boolean): string;
    DatePickerFor(propertyName: string, wrap: boolean): string;
    TextAreaFor(propertyName: string, wrap: boolean): string;
    DropDownFor(propertyName: string, selectList: SelectListItem[], wrap: boolean): string;
    MultipleDropDownFor(propertyName: string, selectList: SelectListItem[], wrap: boolean): string;
    HiddenFor(propertyName: string, wrap: boolean): string;
}
