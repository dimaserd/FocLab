class FormTypeDrawer {

    _formDrawer: IFormDraw;
    _typeDescription: CrocoTypeDescription;

    constructor(formDrawer: IFormDraw, typeDescription: CrocoTypeDescription) {
        this._formDrawer = formDrawer;
        this._typeDescription = typeDescription;
    }

    public BeforeFormDrawing(): void {
        this._formDrawer.BeforeFormDrawing();
    }


    public AfterFormDrawing(): void {
        this._formDrawer.AfterFormDrawing();
    }

    public TextBoxFor(propertyName: string): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderTextBox(prop);
    }

    public DatePickerFor(propertyName: string): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderDatePicker(prop);
    }


    public TextAreaFor(propertyName: string) : string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderTextArea(prop);
    }

    public DropDownFor(propertyName: string, selectList: SelectListItem[]) : string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderDropDownList(prop, selectList);
    }

    public MultipleDropDownFor(propertyName: string, selectList: SelectListItem[]): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderMultipleDropDownList(prop, selectList);
    }

    public HiddenFor(propertyName: string): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderHidden(prop);
    }

    static FindPropByName(type: CrocoTypeDescription, propName: string): CrocoTypeDescription {
        for (let i = 0; i < type.Properties.length; i++) {
            let prop = type.Properties[i];

            if (prop.PropertyName == propName) {
                return prop;
            }
        }

        throw new Error(`Свойство ${propName} не найдено`)
    }
}