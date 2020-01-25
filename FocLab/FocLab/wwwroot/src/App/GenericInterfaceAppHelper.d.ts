declare class GenericInterfaceAppHelper {
    constructor();
    FormHelper: GenericForm;
    GetUserInterfaceModel(typeName: string, modelPrefix: string, callBack: (x: GenerateGenericUserInterfaceModel) => void): void;
    GetEnumModel(enumTypeName: string, callBack: (x: CrocoEnumTypeDescription) => void): void;
}
