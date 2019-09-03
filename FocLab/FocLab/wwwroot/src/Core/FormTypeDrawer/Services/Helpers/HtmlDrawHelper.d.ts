declare class HtmlDrawHelper {
    static RenderAttributesString(attrs: Dictionary<string>): string;
    static RenderSelect(className: string, propName: string, selectList: SelectListItem[], attrs: Dictionary<string>): string;
    static ProceesSelectValues(typeDescription: CrocoTypeDescription, rawValue: string, selectList: SelectListItem[]): SelectListItem[];
}
