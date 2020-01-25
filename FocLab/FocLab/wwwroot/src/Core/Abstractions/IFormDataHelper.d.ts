interface IFormDataHelper {
    readonly NullValue: string;
    readonly DataTypeAttributeName: string;
    FillDataByPrefix(object: Object, prefix: string): void;
    CollectDataByPrefix(object: object, prefix: string): void;
    CollectDataByPrefixWithTypeMatching(modelPrefix: string, typeDescription: CrocoTypeDescription): object;
}
