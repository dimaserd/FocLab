declare class FormTypeDataGetter {
    private readonly _typeDescription;
    constructor(data: CrocoTypeDescription);
    private BuildObject;
    GetData(modelPrefix: string): object;
}
