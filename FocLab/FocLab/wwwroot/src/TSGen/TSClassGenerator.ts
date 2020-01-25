class TSClassGenerator {

    public static GetDescription(typeDescription: CrocoTypeDescription): string {
        if (typeDescription.PropertyDescription != null && typeDescription.PropertyDescription.Descriptions.length > 0) {
            return `\t/**\n\t* ${typeDescription.PropertyDescription.Descriptions[0]}\n \t*/\n`;
        }

        return "";
    }

    
    public GetUniqueTypes(typeDescription: CrocoTypeDescription): Array<CrocoTypeDescription> {

        if (typeDescription == null) {
            return [];
        }

        if (typeDescription.IsEnumerable) {

            const t = this.GetUniqueTypes(typeDescription.EnumeratedType);

            return t;
        }

        if (typeDescription.IsEnumeration) {
            return [typeDescription];
        }

        if (typeDescription.IsClass) {

            let types: Array<CrocoTypeDescription> = [typeDescription];

            for (let i = 0; i < typeDescription.Properties.length; i++) {

                const prop = typeDescription.Properties[i];

                const typesByProp = this.GetUniqueTypes(prop);

                types = types.concat(typesByProp);
            }

            return types;
        }

        return [];
    }

    static GetTypeDisplayName(typeDescription: CrocoTypeDescription, isDeclaration: boolean, useGenerics: boolean): string {

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

            let genDescr = typeDescription.GenericDescription;

            let genTypeTsNames = genDescr.GenericArgumentTypeNames.map(x => {
                if (TsSimpleTypeMapper.simpleTypes.indexOf(x) >= 0) {
                    return TsSimpleTypeMapper.GetPropertyTypeByTypeDisplayName(x);
                }

                return x;
            });

            return `${genDescr.TypeNameWithoutGenericArgs}<${genTypeTsNames.join(',')}>`;
        }

        return typeDescription.TypeDisplayName;
    }

    public static GenerateClass(typeDescription: CrocoTypeDescription, useGenerics: boolean): string {

        let result: string = "";

        if (typeDescription.IsEnumeration) {
            result += TsEnumTypeDescriptor.GetEnum(typeDescription);
        }

        if (typeDescription.IsClass) {

            result += `interface ${this.GetTypeDisplayName(typeDescription, true, useGenerics)} {\n`;

            for (let i = 0; i < typeDescription.Properties.length; i++) {

                const prop = typeDescription.Properties[i];

                if (prop.IsEnumerable) {

                    result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.PropertyDescription.PropertyName}: Array<${this.GetEnumeratedDisplayTypeName(prop.EnumeratedType)}>; \n`;

                    continue;
                }

                if (prop.IsClass || prop.IsEnumeration) {
                    result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.PropertyDescription.PropertyName}: ${this.GetTypeDisplayName(prop, false, useGenerics)}; \n`;

                    continue;
                }

                result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.PropertyDescription.PropertyName}: ${TsSimpleTypeMapper.GetPropertyType(prop)}; \n`;
            }

            result += "}";
        }

        return result;
    }

    private static GetEnumeratedDisplayTypeName(typeDescription: CrocoTypeDescription) : string {
        if (typeDescription.IsClass || typeDescription.IsEnumeration) {
            return typeDescription.TypeName;
        }

        return TsSimpleTypeMapper.GetPropertyType(typeDescription);
    }

    public GenerateClassesForType(typeDescription: CrocoTypeDescription, useGenerics: boolean): string {

        console.log("GenerateClassesForType", typeDescription, useGenerics);

        const uniqueTypes = TSClassGenerator.RemoveDuplicates(this.GetUniqueTypes(typeDescription));

        return uniqueTypes.map(x => TSClassGenerator.GenerateClass(x, useGenerics)).join("\n\n\n");
    }

    /*
     * TODO Неоптимизированное говно
     */
    public static RemoveDuplicates(array: Array<CrocoTypeDescription>) : Array<CrocoTypeDescription> {
        for (let i = 0; i < array.length; i++) {
            const elem = array[i];
            array = array.filter(x => x.FullTypeName !== elem.FullTypeName);
            array.push(elem);
        }

        return array.reverse();
    }
}