declare class HtmlDrawHelper {
    static RenderSelect(className: string, propName: string, selectList: SelectListItem[]): string;
    static ProceesSelectValues(typeDescription: CrocoTypeDescription, rawValue: string, selectList: SelectListItem[]): SelectListItem[];
}
