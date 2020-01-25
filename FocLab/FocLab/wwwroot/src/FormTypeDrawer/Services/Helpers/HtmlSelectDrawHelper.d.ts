declare class HtmlSelectDrawHelper {
    NullValue: string;
    constructor(nullValue: string);
    ProccessSelectValues(typeDescription: CrocoTypeDescription, rawValue: string, selectList: SelectListItem[]): void;
}
