class TsEnumTypeDescriptor {
    public static GetEnum(typeDescription: CrocoTypeDescription): string {
        if (!typeDescription.IsEnumeration) {
            throw new DOMException("Данный тип не является перечислением");
        }
        let html = "";
        html += `enum ${typeDescription.TypeDisplayName} {\n`;
        for (let i = 0; i < typeDescription.EnumDescription.EnumValues.length; i++) {
            const enumValue = typeDescription.EnumDescription.EnumValues[i];
            const comma = (i === typeDescription.EnumDescription.EnumValues.length - 1) ? "" : ",";
            html += `\t${enumValue.StringRepresentation} = <any> '${enumValue.StringRepresentation}'${comma}\n`;
        }
        html += `}\n`;
        return html;
    }
}