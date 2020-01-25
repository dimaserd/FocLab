declare class GetListResultExtensions {
    static GetCurrentPageNumber<TModel>(model: GetListResult<TModel>): number;
    static GetPagesCount<TModel>(model: GetListResult<TModel>): number;
}
