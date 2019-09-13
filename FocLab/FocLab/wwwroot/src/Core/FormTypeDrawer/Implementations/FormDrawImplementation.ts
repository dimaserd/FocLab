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

        //Иницилизация календарей
        for (let i = 0; i < this._datePickerPropNames.length; i++) {
            const datePickerPropName = this._datePickerPropNames[i];
            const propName = `${this._model.Prefix}${datePickerPropName}`;

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
        const value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        const html = `<input type="hidden" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" value="${value}">`;

        if (!wrap) {
            return html;
        }

        return `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}
                    ${html}
                </div>`;
    }

    RenderTextBox(typeDescription: CrocoTypeDescription, wrap: boolean): string {

        const value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        const html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
                <input autocomplete="false" class="form-control m-input" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" type="text" value="${value}">`;

        if (!wrap) {
            return html;
        }

        return  `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}>
                    ${html}
                </div>`;
    }

    RenderTextArea(typeDescription: CrocoTypeDescription, wrap: boolean): string {

        const value = ValueProviderHelper.GetStringValueFromValueProvider(typeDescription, this._model.ValueProvider);

        const styles = `style="margin-top: 0px; margin-bottom: 0px; height: 79px;"`;

        const html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>
            <textarea autocomplete="false" class="form-control m-input" name="${FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix)}" rows="3" ${styles}>${value}</textarea>`;

        if (!wrap) {
            return html;
        }

        return `<div class="form-group m-form__group" ${FormDrawHelper.GetOuterFormAttributes(typeDescription.PropertyName, this._model.Prefix)}>
                      ${html}
                </div>`;
    }

    RenderGenericDropList(typeDescription: CrocoTypeDescription, selectList: SelectListItem[], isMultiple: boolean, wrap: boolean): string {

        const rawValue = ValueProviderHelper.GetRawValueFromValueProvider(typeDescription, this._model.ValueProvider);

        selectList = HtmlDrawHelper.ProceesSelectValues(typeDescription, rawValue, selectList);

        const _class = `${this._selectClass} form-control m-input m-bootstrap-select m_selectpicker`;

        const dict = isMultiple ? new Dictionary<string>([{ key: "multiple", value: "" }]) : null;

        const select = HtmlDrawHelper.RenderSelect(_class, FormDrawHelper.GetPropertyValueName(typeDescription.PropertyName, this._model.Prefix), selectList, dict);

        const html = `<label for="${typeDescription.PropertyName}">${typeDescription.PropertyDisplayName}</label>${select}`;

        if (!wrap) {
            return html;
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
