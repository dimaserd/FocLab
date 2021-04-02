var TsEnumTypeDescriptor = (function () {
    function TsEnumTypeDescriptor() {
    }
    TsEnumTypeDescriptor.GetEnum = function (typeDescription) {
        if (!typeDescription.IsEnumeration) {
            throw new DOMException("Данный тип не является перечислением");
        }
        var html = "";
        html += "enum " + typeDescription.TypeDisplayName + " {\n";
        for (var i = 0; i < typeDescription.EnumDescription.EnumValues.length; i++) {
            var enumValue = typeDescription.EnumDescription.EnumValues[i];
            var comma = (i === typeDescription.EnumDescription.EnumValues.length - 1) ? "" : ",";
            html += "\t" + enumValue.StringRepresentation + " = <any> '" + enumValue.StringRepresentation + "'" + comma + "\n";
        }
        html += "}\n";
        return html;
    };
    return TsEnumTypeDescriptor;
}());
