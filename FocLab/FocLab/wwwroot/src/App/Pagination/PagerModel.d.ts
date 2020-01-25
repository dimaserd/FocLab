declare class PagerModel {
    static ToPagerModel<TModel>(model: GetListResult<TModel>, link: string): PagerModel;
    static GetCombinedData(prefix: string, obj: Object): Object;
    static GetParams(obj: Object): string;
    PagesCount: number;
    CurrentPage: number;
    PageSize: number;
    LinkFormat: string;
}
