class GetListResultExtensions {
    /**
     * Получить номер текущей страницы
     * */
    public static GetCurrentPageNumber<TModel>(model: GetListResult<TModel>): number {
        if (model.Count === null) {
            return 0;
        }
        return Math.trunc(model.OffSet / model.Count);
    }
    /**
     * Получить количество страниц
     * */
    public static GetPagesCount<TModel>(model: GetListResult<TModel>): number {
        if (model.Count === null) {
            return 0;
        }
        const preRes = Math.trunc(model.TotalCount / model.Count);
        return model.TotalCount % model.Count > 0 ? preRes + 1 : preRes;
    }
}