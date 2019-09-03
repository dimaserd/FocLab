class TabFormDrawImplementation implements IFormDraw {
    

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

    RenderTextBox(typeDescription: CrocoTypeDescription): string {

        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        return `<div class="form-group m-form__group row">
                    <label class="col-xl-3 col-lg-3 col-form-label">
                        ${typeDescription.PropertyDisplayName}:
                    </label>
                    <div class="col-xl-9 col-lg-9">
                        <div class="input-group">
                            <input type="text" name="${this.GetPropertyValueName(typeDescription.PropertyName)}" class="form-control m-input" placeholder="" value="${value}">
                        </div>
                    </div>
                </div>`;
    }
    RenderTextArea(typeDescription: CrocoTypeDescription): string {

        var value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        return `<div class="form-group m-form__group row">
                    <label class="col-xl-3 col-lg-3 col-form-label">${typeDescription.PropertyDisplayName}</label>
                    <div class="col-xl-9 col-lg-9">
                        <textarea class="form-control m-input" name="${this.GetPropertyValueName(typeDescription.PropertyName)}" rows="3">${value}</textarea>
                    </div>

                </div>`;
    }

    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean): string {

        let rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);

        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);

        const _class = `${this._selectClass} form-control m-input m-bootstrap-select m_selectpicker`;

        let dict = isMultiple ? new Dictionary<string>([{ key: "multiple", value: "" }]) : null;

        var select = HtmlDrawHelper.RenderSelect(_class, this.GetPropertyValueName(typeDescription.PropertyName), selectList, dict);

        return `<div class="form-group m-form__group row">
                    <label class="col-xl-3 col-lg-3 col-form-label">${typeDescription.PropertyDisplayName}:</label>
                    <div class="col-xl-9 col-lg-9">
                        ${select}
                    </div>
                </div>`;
    }


    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string {

        return this.RenderGenericDropList(typeDescription, selectList, false);
    }

    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[]): string {

        return this.RenderGenericDropList(typeDescription, selectList, true);
    }

    RenderHidden(typeDescription: CrocoTypeDescription): string {
        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        return `<input type="hidden" name="${this.GetPropertyValueName(typeDescription.PropertyName)}" value="${value}">`;
    }
    RenderDatePicker(typeDescription: CrocoTypeDescription): string {
        this._datePickerPropNames.push(typeDescription.PropertyName);

        return this.RenderTextBox(typeDescription);
    }

    private GetPropertyValueName(propName: string): string {
        return `${this._model.Prefix}${propName}`;
    }
}