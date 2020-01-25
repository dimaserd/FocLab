var TSClassGenerator = (function () {
    function TSClassGenerator() {
    }
    TSClassGenerator.GetDescription = function (typeDescription) {
        if (typeDescription.PropertyDescription != null && typeDescription.PropertyDescription.Descriptions.length > 0) {
            return "\t/**\n\t* " + typeDescription.PropertyDescription.Descriptions[0] + "\n \t*/\n";
        }
        return "";
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
    TSClassGenerator.GetTypeDisplayName = function (typeDescription, isDeclaration, useGenerics) {
        if (!useGenerics) {
            return typeDescription.TypeDisplayName;
        }
        if (isDeclaration) {
            if (!typeDescription.IsGeneric) {
                return typeDescription.TypeDisplayName;
            }
            return typeDescription.GenericDescription.GenericTypeNameWithUndefinedArgs;
        }
        if (typeDescription.IsGeneric) {
            var genDescr = typeDescription.GenericDescription;
            var genTypeTsNames = genDescr.GenericArgumentTypeNames.map(function (x) {
                if (TsSimpleTypeMapper.simpleTypes.indexOf(x) >= 0) {
                    return TsSimpleTypeMapper.GetPropertyTypeByTypeDisplayName(x);
                }
                return x;
            });
            return genDescr.TypeNameWithoutGenericArgs + "<" + genTypeTsNames.join(',') + ">";
        }
        return typeDescription.TypeDisplayName;
    };
    TSClassGenerator.GenerateClass = function (typeDescription, useGenerics) {
        var result = "";
        if (typeDescription.IsEnumeration) {
            result += TsEnumTypeDescriptor.GetEnum(typeDescription);
        }
        if (typeDescription.IsClass) {
            result += "interface " + this.GetTypeDisplayName(typeDescription, true, useGenerics) + " {\n";
            for (var i = 0; i < typeDescription.Properties.length; i++) {
                var prop = typeDescription.Properties[i];
                if (prop.IsEnumerable) {
                    result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyDescription.PropertyName + ": Array<" + this.GetEnumeratedDisplayTypeName(prop.EnumeratedType) + ">; \n";
                    continue;
                }
                if (prop.IsClass || prop.IsEnumeration) {
                    result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyDescription.PropertyName + ": " + this.GetTypeDisplayName(prop, false, useGenerics) + "; \n";
                    continue;
                }
                result += TSClassGenerator.GetDescription(prop) + "\t " + prop.PropertyDescription.PropertyName + ": " + TsSimpleTypeMapper.GetPropertyType(prop) + "; \n";
            }
            result += "}";
        }
        return result;
    };
    TSClassGenerator.GetEnumeratedDisplayTypeName = function (typeDescription) {
        if (typeDescription.IsClass || typeDescription.IsEnumeration) {
            return typeDescription.TypeName;
        }
        return TsSimpleTypeMapper.GetPropertyType(typeDescription);
    };
    TSClassGenerator.prototype.GenerateClassesForType = function (typeDescription, useGenerics) {
        console.log("GenerateClassesForType", typeDescription, useGenerics);
        var uniqueTypes = TSClassGenerator.RemoveDuplicates(this.GetUniqueTypes(typeDescription));
        return uniqueTypes.map(function (x) { return TSClassGenerator.GenerateClass(x, useGenerics); }).join("\n\n\n");
    };
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
