interface GetListResult<TModel> {
    Count: number;
    OffSet: number;
    TotalCount: number;
    List: Array<TModel>;
}
