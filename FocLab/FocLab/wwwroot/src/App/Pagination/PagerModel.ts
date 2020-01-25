class PagerModel {

    static ToPagerModel<TModel>(model: GetListResult<TModel>, link: string): PagerModel {

        let startUrl = CrocoAppCore.Application.FormDataUtils.GetStartUrlNoParams(link);

        let linkObj = CrocoAppCore.Application.FormDataUtils.GetUrlParamsObject(link);

        let countToChangeTempName = "CountToChange";
        let offSetToChangeTempName = "OffSetToChange";

        linkObj["Count"] = countToChangeTempName;
        linkObj["OffSet"] = offSetToChangeTempName;

        let linkFormat = `${startUrl}?${PagerModel.GetParams(linkObj)}`;

        return {
            CurrentPage: GetListResultExtensions.GetCurrentPageNumber(model),
            PagesCount: GetListResultExtensions.GetPagesCount(model),
            LinkFormat: linkFormat.replace(countToChangeTempName, "{0}").replace(offSetToChangeTempName, "{1}"),
            PageSize: model.Count
        };
    }

    static GetCombinedData(prefix: string, obj: Object): Object {

        const resultObj = {};

        for (let prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                const objWithProps = this.GetCombinedData(`${prefix}${prop}.`, obj[prop]);

                for (let innerProp in objWithProps) {
                    resultObj[innerProp] = objWithProps[innerProp];
                }
            }
            else {
                resultObj[prefix + prop] = obj[prop];
            }
        }

        return resultObj;
    }

    static GetParams(obj: Object): string {

        obj = PagerModel.GetCombinedData("", obj);

        return $.param(obj, true);
    }

    PagesCount: number;

    CurrentPage: number;

    PageSize: number;

    LinkFormat: string;
}