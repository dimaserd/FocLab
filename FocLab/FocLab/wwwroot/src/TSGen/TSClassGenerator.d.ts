/// <reference path="Models/crocoTypeDescription.d.ts" />
declare class TSClassTypeMapper {
    private static typesDictionary;
    static GetPropertyType(typeDescription: CrocoTypeDescription): string;
}
declare class TSClassGenerator {
    constructor();
    static GetDescription(typeDescription: CrocoTypeDescription): string;
    static GetEnum(typeDescription: CrocoTypeDescription): string;
    GetUniqueTypes(typeDescription: CrocoTypeDescription): Array<CrocoTypeDescription>;
    static GenerateClass(typeDescription: CrocoTypeDescription): string;
    private static GetEnumeratedDisplayTypeName;
    GenerateClassesForType(typeDescription: CrocoTypeDescription): string;
    static RemoveDuplicates(array: Array<CrocoTypeDescription>): Array<CrocoTypeDescription>;
}
