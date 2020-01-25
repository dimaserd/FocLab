var GetListResultExtensions = (function () {
    function GetListResultExtensions() {
    }
    GetListResultExtensions.GetCurrentPageNumber = function (model) {
        if (model.Count === null) {
            return 0;
        }
        return Math.trunc(model.OffSet / model.Count);
    };
    GetListResultExtensions.GetPagesCount = function (model) {
        if (model.Count === null) {
            return 0;
        }
        var preRes = Math.trunc(model.TotalCount / model.Count);
        return model.TotalCount % model.Count > 0 ? preRes + 1 : preRes;
    };
    return GetListResultExtensions;
}());
