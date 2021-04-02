declare class TSClassGenerator {
    static GetDescription(typeDescription: CrocoTypeDescription): string;
    GetUniqueTypes(typeDescription: CrocoTypeDescription): Array<CrocoTypeDescription>;
    static GetTypeDisplayName(typeDescription: CrocoTypeDescription, isDeclaration: boolean, useGenerics: boolean): string;
    static GenerateClass(typeDescription: CrocoTypeDescription, useGenerics: boolean): string;
    private static GetEnumeratedDisplayTypeName;
    GenerateClassesForType(typeDescription: CrocoTypeDescription, useGenerics: boolean): string;
    static RemoveDuplicates(array: Array<CrocoTypeDescription>): Array<CrocoTypeDescription>;
}
