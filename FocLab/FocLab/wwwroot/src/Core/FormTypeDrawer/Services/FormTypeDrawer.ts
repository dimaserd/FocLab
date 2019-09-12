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

    public TextBoxFor(propertyName: string, wrap: boolean): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderTextBox(prop, wrap);
    }

    public DatePickerFor(propertyName: string, wrap: boolean): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderDatePicker(prop, wrap);
    }


    public TextAreaFor(propertyName: string, wrap: boolean) : string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderTextArea(prop, wrap);
    }

    public DropDownFor(propertyName: string, selectList: SelectListItem[], wrap: boolean) : string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderDropDownList(prop, selectList, wrap);
    }

    public MultipleDropDownFor(propertyName: string, selectList: SelectListItem[], wrap: boolean): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderMultipleDropDownList(prop, selectList, wrap);
    }

    public HiddenFor(propertyName: string, wrap: boolean): string {
        var prop = FormTypeDrawer.FindPropByName(this._typeDescription, propertyName);

        return this._formDrawer.RenderHidden(prop, wrap);
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