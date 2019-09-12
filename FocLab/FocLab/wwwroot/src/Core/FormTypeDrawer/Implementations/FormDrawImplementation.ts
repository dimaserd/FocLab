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

    RenderDatePicker(typeDescription: CrocoTypeDescription, wrap: boolean): string {
        this._datePickerPropNames.push(typeDescription.PropertyName);

        return this.RenderTextBox(typeDescription, wrap);
    }

    RenderHidden(typeDescription: CrocoTypeDescription, wrap: boolean): string {
        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        let html = `<input type="hidden" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" value="${value}">`;

        if (!wrap) {
            return html;
        }

        return `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}
                    ${html}
                </div>`;
    }

    RenderTextBox(typeDescription: CrocoTypeDescription, wrap: boolean): string {

        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        let html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
                <input autocomplete="false" class="form-control m-input" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" type="text" value="${value}">`;

        if (!wrap) {
            return html;
        }

        var t = `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}>
                    ${html}
                </div>`;
        return t;
    }

    RenderTextArea(typeDescription: CrocoTypeDescription, wrap: boolean): string {

        let value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        let styles = `style="margin-top: 0px; margin-bottom: 0px; height: 79px;"`;

        let html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
            <textarea autocomplete="false" class="form-control m-input" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" rows="3" ${styles}>${value}</textarea>`;

        if (!wrap) {
            return html;
        }

        return `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}>
                      ${html}
                </div>`;
    }

    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean, wrap: boolean): string {

        let rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);

        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);

        const _class = `${this._selectClass} form-control m-input m-bootstrap-select m_selectpicker`;

        let dict = isMultiple ? new Dictionary<string>([{ key: "multiple", value: "" }]) : null;

        var select = HtmlDrawHelper.RenderSelect(_class, FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix), selectList, dict);

        let html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>${select}`;

        if (!wrap) {
            return html
        }

        return `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}>
                    ${html}
            </div>`;
    }

    RenderDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string {
        return this.RenderGenericDropList(typeDescription, selectList, false, wrap);
    }

    RenderMultipleDropDownList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], wrap: boolean): string {
        return this.RenderGenericDropList(typeDescription, selectList, true, wrap);
    }
}
