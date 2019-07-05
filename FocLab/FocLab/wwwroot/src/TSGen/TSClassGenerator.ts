///<reference path="Models/crocoTypeDescription.ts"/>

class TSClassTypeMapper {

    private static typesDictionary = new Dictionary<string>([

        { key: "String", value: "string" },

        { key: "Int32", value: "number" },

        { key: "Decimal", value: "number" },

        { key: "Boolean", value: "boolean" },

        { key: "DateTime", value: "Date" }
    ]);

    static GetPropertyType(typeDescription: CrocoTypeDescription): string {
        return this.typesDictionary[typeDescription.TypeName];
    }
}


class TSClassGenerator {

    constructor() {
    }

    public static GetDescription(typeDescription: CrocoTypeDescription) : string {   
        if (typeDescription.Descriptions.length > 0) {
            return `\t/**\n\t* ${typeDescription.Descriptions[0]}\n \t*/\n`;
        }

        return "";
    }


    public static GetEnum(typeDescription: CrocoTypeDescription): string {
        if (!typeDescription.IsEnumeration) {
            throw new DOMException("Данный тип не является перечислением");
        }
        let html = "";

        html += `enum ${typeDescription.DisplayName} {\n`;

        for (let i = 0; i < typeDescription.EnumValues.length; i++) {

            const enumValue = typeDescription.EnumValues[i];

            const comma = (i === typeDescription.EnumValues.length - 1) ? "" : ",";

            html += `\t${enumValue.StringRepresentation} = <any> '${enumValue.StringRepresentation}'${comma}\n`;
        }

        html += `}\n`;

        return html;
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

    public static GenerateClass(typeDescription: CrocoTypeDescription): string {

        let result: string = "";

        if (typeDescription.IsEnumeration) {
            result += this.GetEnum(typeDescription);
        }

        if (typeDescription.IsClass) {

            result += `interface ${typeDescription.TypeName} {\n`;

            for (let i = 0; i < typeDescription.Properties.length; i++) {

                const prop = typeDescription.Properties[i];

                if (prop.IsEnumerable) {

                    result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.Name}: Array<${this.GetEnumeratedDisplayTypeName(prop.EnumeratedType)}>; \n`;

                    continue;
                }

                if (prop.IsClass || prop.IsEnumeration) {
                    result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.Name}: ${prop.TypeName}; \n`;

                    continue;
                }

                result += `${TSClassGenerator.GetDescription(prop)}\t ${prop.Name}: ${TSClassTypeMapper.GetPropertyType(prop)}; \n`;
            }

            result += "}";
        }

        return result;
    }

    private static GetEnumeratedDisplayTypeName(typeDescription: CrocoTypeDescription) : string {
        if (typeDescription.IsClass || typeDescription.IsEnumeration) {
            return typeDescription.TypeName;
        }

        return TSClassTypeMapper.GetPropertyType(typeDescription);
    }

    public GenerateClassesForType(typeDescription: CrocoTypeDescription): string {

        const uniqueTypes = TSClassGenerator.RemoveDuplicates(this.GetUniqueTypes(typeDescription));

        return uniqueTypes.map(x => TSClassGenerator.GenerateClass(x)).join("\n\n\n");
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