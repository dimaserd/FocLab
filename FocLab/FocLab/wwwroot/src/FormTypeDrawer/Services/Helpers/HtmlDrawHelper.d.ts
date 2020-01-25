declare class HtmlDrawHelper {
    static RenderInput(type: string, attrs: Map<string, string>): string;
    static RenderAttributesString(attrs: Map<string, string>): string;
    static RenderSelect(className: string, propName: string, selectList: SelectListItem[], attrs: Map<string, string>): string;
}
