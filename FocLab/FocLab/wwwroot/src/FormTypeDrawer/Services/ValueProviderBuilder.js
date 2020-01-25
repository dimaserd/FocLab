var ValueProviderBuilder = (function () {
    function ValueProviderBuilder() {
    }
    ValueProviderBuilder.CreateFromObject = function (obj) {
        obj = CrocoAppCore.Application.FormDataUtils.ProccessAllDateTimePropertiesAsString(obj);
        var res = {
            Arrays: [],
            Singles: []
        };
        for (var index in obj) {
            var valueOfProp = obj[index];
            if (Array.isArray(valueOfProp)) {
                continue;
            }
            if (valueOfProp !== undefined) {
                res.Singles.push({
                    PropertyName: index,
                    Value: valueOfProp
                });
            }
        }
        return res;
    };
    return ValueProviderBuilder;
}());
