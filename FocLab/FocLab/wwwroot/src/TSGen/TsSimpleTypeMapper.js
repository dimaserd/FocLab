var TsSimpleTypeMapper = (function () {
    function TsSimpleTypeMapper() {
    }
    TsSimpleTypeMapper.GetPropertyType = function (typeDescription) {
        return TsSimpleTypeMapper.GetPropertyTypeByTypeDisplayName(typeDescription.TypeDisplayName);
    };
    TsSimpleTypeMapper.GetPropertyTypeByTypeDisplayName = function (typeDisplayName) {
        return TsSimpleTypeMapper.typesDictionary.get(typeDisplayName);
    };
    TsSimpleTypeMapper.simpleTypes = ["String", "Int32", "Int64", "Decimal", "Boolean", "DateTime"];
    TsSimpleTypeMapper.typesDictionary = new Map()
        .set("String", "string")
        .set("Int32", "number")
        .set("Int64", "number")
        .set("Decimal", "number")
        .set("Boolean", "boolean")
        .set("DateTime", "Date");
    return TsSimpleTypeMapper;
}());
