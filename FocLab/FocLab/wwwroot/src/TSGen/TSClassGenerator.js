///<reference path="Models/crocoTypeDescription.ts"/>
var TSClassTypeMapper = /** @class */ (function () {
    function TSClassTypeMapper() {
    }
    TSClassTypeMapper.GetPropertyType = function (typeDescription) {
        return this.typesDictionary[typeDescription.TypeName];
    };
    TSClassTypeMapper.typesDictionary = new Dictionary([
        { key: "String", value: "string" },
        { key: "Int32", value: "number" },
        { key: "Decimal", value: "number" },
        { key: "Boolean", value: "boolean" },
        { key: "DateTime", value: "Date" }
    ]);
    return TSClassTypeMapper;
}());
var TSClassGenerator = /** @class */ (function () {
    function TSClassGenerator() {
    }
    TSClassGenerator.GetDescription = function (typeDescription) {
        if (typeDescription.Descriptions.length > 0) {
            return "\t/**\n\t* " + typeDescription.Descriptions[0] + "\n \t*/\n";
        }
        return "";
    };
    TSClassGenerator.GetEnum = function (typeDescription) {
        if (!typeDescription.IsEnumeration) {
            throw new DOMException("Данный тип не является перечислением");
        }
        var html = "";
        html += "enum " + typeDescription.PropertyName + " {\n";
        for (var i = 0; i < typeDescription.EnumValues.length; i++) {
            var enumValue = typeDescription.EnumValues[i];
            var comma = (i === typeDescription.EnumValues.length - 1) ? "" : ",";
            html += "\t" + enumValue.StringRepresentation + " = <any> '" + enumValue.StringRepresentation + "'" + comma + "\n";
        }
        html += "}\n";
        return html;
    };
    TSClassGenerator.prototype.GetUniqueTypes = function (typeDescription) {
        if (typeDescription == null) {
            return [];
        }
        if (typeDescription.IsEnumerable) {
            var t_1 = this.GetUniqueTypes(typeDescription.EnumeratedType);
            return t_1;
        }
        if (typeDescription.IsEnumeration) {
            return [typeDescription];
        }
        if (typeDescription.IsClass) {
            var types = [typeDescription];
            for (var i = 0; i < typeDescription.Properties.length; i++) {
                var prop = typeDescription.Properties[i];
                var typesByProp = this.GetUniqueTypes(prop);
                types = types.concat(typesByProp);
            }
            return types;
        }
        return [];
    };
    TSClassGenerator.GenerateClass = function (typeDescription) {
        var result = "";
        if (typeDescription.IsEnumeration) {
            result += this.GetEnum(typeDescription);
        }
        if (typeDescription.IsClass) {
            result += "interface " + typeDescription.TypeName + " {\n";
            for (var i = 0; i < typeDescription.Properties.length; i++) {
                var prop = typeDescription.Properties[i];
                if (prop.IsEnumerable) {
                    result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyName + ": Array<" + this.GetEnumeratedDisplayTypeName(prop.EnumeratedType) + ">; \n";
                    continue;
                }
                if (prop.IsClass || prop.IsEnumeration) {
                    result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyName + ": " + prop.TypeName + "; \n";
                    continue;
                }
                result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyName + ": " + TSClassTypeMapper.GetPropertyType(prop) + "; \n";
            }
            result += "}";
        }
        return result;
    };
    TSClassGenerator.GetEnumeratedDisplayTypeName = function (typeDescription) {
        if (typeDescription.IsClass || typeDescription.IsEnumeration) {
            return typeDescription.TypeName;
        }
        return TSClassTypeMapper.GetPropertyType(typeDescription);
    };
    TSClassGenerator.prototype.GenerateClassesForType = function (typeDescription) {
        var uniqueTypes = TSClassGenerator.RemoveDuplicates(this.GetUniqueTypes(typeDescription));
        return uniqueTypes.map(function (x) { return TSClassGenerator.GenerateClass(x); }).join("\n\n\n");
    };
    /*
     * TODO Неоптимизированное говно
     */
    TSClassGenerator.RemoveDuplicates = function (array) {
        var _loop_1 = function (i) {
            var elem = array[i];
            array = array.filter(function (x) { return x.FullTypeName !== elem.FullTypeName; });
            array.push(elem);
        };
        for (var i = 0; i < array.length; i++) {
            _loop_1(i);
        }
        return array.reverse();
    };
    return TSClassGenerator;
}());
