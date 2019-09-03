class FormDrawImplementation implements IFormDraw {
    

    constructor(model: GenerateGenericUserInterfaceModel) {
        this._model = model;
    }

    _model: GenerateGenericUserInterfaceModel;
    _datePickerPropNames: Array<string> = [];

    _selectClass = 'form-draw-select';

    BeforeFormDrawing(): void {
        //TODO Init calendar or some scripts
    }
    AfterFormDrawing(): void {
        //Красивые селекты
        $(`.${this._selectClass}`).selectpicker('refresh');

        //Инициация календарей
        for (let i = 0; i < this._datePickerPropNames.length; i++) {
            let datePickerPropName = this._datePickerPropNames[i];
            let propName = `${this._model.Prefix}${datePickerPropName}`;

            FormDrawImplementation.InitCalendarForPrefixedProperty(propName);
        }
    }

    static InitCalendarForPrefixedProperty(prefixedPropName: string) : void {
        Utils.SetDatePicker(`input[name='${prefixedPropName}']`);
    }
    
    private GetPropertyValueName(typeDescription: CrocoTypeDescription): string {
        return `${this._model.Prefix}${typeDescription.PropertyName}`;
    }

    RenderDatePicker(typeDescription: CrocoTypeDescription): string {
        this._datePickerPropNames.push(typeDescription.PropertyName);

        return this.RenderTextBox(typeDescription);
    }

    RenderHidden(typeDescription: CrocoTypeDescription): string {
        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        return `<input type="hidden" name="${this.GetPropertyValueName(typeDescription)}" value="${value}">`;
    }

    RenderTextBox(typeDescription: CrocoTypeDescription): string {

        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        var t = `<div class="form-group m-form__group">
                <label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
                <input autocomplete="false" class="form-control m-input" name="${this.GetPropertyValueName(typeDescription)}" type="text" value="${value}">
            </div>`;
        return t;
    }

    RenderTextArea(typeDescription: CrocoTypeDescription): string {

        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        let styles = `style="margin-top: 0px; margin-bottom: 0px; height: 79px;"`;

        var t = `<div class="form-group m-form__group">
                <label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
                <textarea autocomplete="false" class="form-control m-input" name="${this.GetPropertyValueName(typeDescription)}" rows="3" ${styles}>${value}</textarea>
            </div>`;
        return t;
    }

    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean): string {


        let rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);

        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);

        const _class = `${this._selectClass} form-control m-input m-bootstrap-select m_selectpicker`;

        let dict = isMultiple ? new Dictionary<string>([{ key: "multiple", value: "" }]) : null;

        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription), selectList, dict);

        return `<div class="form-group m-form__group">
                <label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
                ${select}   
            </div>`;
    }

    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string {
        return this.RenderGenericDropList(typeDescription, selectList, false);
    }

    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string {
        return this.RenderGenericDropList(typeDescription, selectList, true);
    }
}
