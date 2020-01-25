/// <reference types="core/models/documentation/crocotypedescription" />
declare class TsSimpleTypeMapper {
    static simpleTypes: string[];
    static typesDictionary: Map<string, string>;
    static GetPropertyType(typeDescription: CrocoTypeDescription): string;
    static GetPropertyTypeByTypeDisplayName(typeDisplayName: string): string;
}
