declare class FormTypeDataGetter {
    _typeDescription: CrocoTypeDescription;
    constructor(data: CrocoTypeDescription);
    private BuildObject;
    GetData(modelPrefix: string): object;
}
