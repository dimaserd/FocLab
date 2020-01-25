declare class FormDataHelper implements IFormDataHelper {
    readonly NullValue: string;
    readonly DataTypeAttributeName: string;
    FillDataByPrefix(object: Object, prefix: string): void;
    CollectDataByPrefix(object: object, prefix: string): void;
    private GetRawValueFromElement;
    CollectDataByPrefixWithTypeMatching(modelPrefix: string, typeDescription: CrocoTypeDescription): object;
    private ValueMapper;
    private GetInitValue;
    private CheckData;
    private BuildObject;
}
