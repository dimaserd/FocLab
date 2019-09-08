interface CrocoTypeDescription {
    PropertyDisplayName?: string;
    PropertyName?: string;
    readonly TypeName?: string;
    readonly FullTypeName?: string;
    readonly TypeDisplayName?: string;
    IsNullable?: boolean;
    IsEnumerable?: boolean;
    IsEnumeration?: boolean;
    IsClass?: boolean;
    EnumeratedType?: CrocoTypeDescription;
    EnumValues?: Array<CrocoEnumTypeDescription>;
    Properties?: Array<CrocoTypeDescription>;
    Description: string;
    Descriptions?: Array<string>;
    readonly JsonExample?: string;
}
