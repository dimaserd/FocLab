///<reference path="../Core/Models/Documentation/CrocoTypeDescription.ts"/>
class TsSimpleTypeMapper {
    static simpleTypes = ["String", "Int32", "Int64", "Decimal", "Boolean", "DateTime"];
    static typesDictionary = new Map<string, string>()
        .set("String", "string")
        .set("Int32", "number")
        .set("Int64", "number")
        .set("Decimal", "number")
        .set("Boolean", "boolean")
        .set("DateTime", "Date");
    static GetPropertyType(typeDescription: CrocoTypeDescription): string {
        return TsSimpleTypeMapper.GetPropertyTypeByTypeDisplayName(typeDescription.TypeDisplayName);
    }
    static GetPropertyTypeByTypeDisplayName(typeDisplayName: string): string {
        return TsSimpleTypeMapper.typesDictionary.get(typeDisplayName);
    }
}
