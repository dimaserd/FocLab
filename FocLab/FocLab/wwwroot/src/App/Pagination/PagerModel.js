var PagerModel = (function () {
    function PagerModel() {
    }
    PagerModel.ToPagerModel = function (model, link) {
        var startUrl = CrocoAppCore.Application.FormDataUtils.GetStartUrlNoParams(link);
        var linkObj = CrocoAppCore.Application.FormDataUtils.GetUrlParamsObject(link);
        var countToChangeTempName = "CountToChange";
        var offSetToChangeTempName = "OffSetToChange";
        linkObj["Count"] = countToChangeTempName;
        linkObj["OffSet"] = offSetToChangeTempName;
        var linkFormat = startUrl + "?" + PagerModel.GetParams(linkObj);
        return {
            CurrentPage: GetListResultExtensions.GetCurrentPageNumber(model),
            PagesCount: GetListResultExtensions.GetPagesCount(model),
            LinkFormat: linkFormat.replace(countToChangeTempName, "{0}").replace(offSetToChangeTempName, "{1}"),
            PageSize: model.Count
        };
    };
    PagerModel.GetCombinedData = function (prefix, obj) {
        var resultObj = {};
        for (var prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                var objWithProps = this.GetCombinedData("" + prefix + prop + ".", obj[prop]);
                for (var innerProp in objWithProps) {
                    resultObj[innerProp] = objWithProps[innerProp];
                }
            }
            else {
                resultObj[prefix + prop] = obj[prop];
            }
        }
        return resultObj;
    };
    PagerModel.GetParams = function (obj) {
        obj = PagerModel.GetCombinedData("", obj);
        return $.param(obj, true);
    };
    return PagerModel;
}());
