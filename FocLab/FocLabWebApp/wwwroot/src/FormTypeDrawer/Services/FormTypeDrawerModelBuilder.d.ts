declare class FormTypeDrawerModelBuilder {
    _model: GenerateGenericUserInterfaceModel;
    constructor(model: GenerateGenericUserInterfaceModel);
    SetMultipleDropDownListFor(propertyName: string, selectListItems: Array<SelectListItem>): FormTypeDrawerModelBuilder;
    SetDropDownListFor(propertyName: string, selectListItems: Array<SelectListItem>): FormTypeDrawerModelBuilder;
    SetTextAreaFor(propertyName: string): FormTypeDrawerModelBuilder;
    SetHiddenFor(propertyName: string): FormTypeDrawerModelBuilder;
    private ResetBlock;
    private GetPropertyBlockByName;
}
