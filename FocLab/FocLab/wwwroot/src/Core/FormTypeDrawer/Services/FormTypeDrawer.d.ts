declare class FormTypeDrawer {
    _formDrawer: IFormDraw;
    _typeDescription: CrocoTypeDescription;
    constructor(formDrawer: IFormDraw, typeDescription: CrocoTypeDescription);
    BeforeFormDrawing(): void;
    AfterFormDrawing(): void;
    TextBoxFor(propertyName: string): string;
    DatePickerFor(propertyName: string): string;
    TextAreaFor(propertyName: string): string;
    DropDownFor(propertyName: string, selectList: SelectListItem[]): string;
    MultipleDropDownFor(propertyName: string, selectList: SelectListItem[]): string;
    HiddenFor(propertyName: string): string;
    static FindPropByName(type: CrocoTypeDescription, propName: string): CrocoTypeDescription;
}
