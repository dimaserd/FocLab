var GetListResut = (function () {
    function GetListResut() {
    }
    return GetListResut;
}());
var UserCreatePage = (function () {
    function UserCreatePage() {
    }
    UserCreatePage.SetHandlers = function (modelPrefix) {
        $(".action-btn").on("click", function () {
            console.log("UserCreatePage.Click");
            var data = TryForm.GetDataForFormByModelPrefix(modelPrefix);
            ;
            Requester.SendPostRequestWithAnimation("/Api/User/Create", data, function (resp) {
                if (resp.IsSucceeded) {
                    setTimeout(function () { location.href = '/Admin/Users/Index'; }, 1000);
                }
            }, null);
        });
    };
    UserCreatePage.AfterDrawHandler = function (modelPrefix) {
        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 }, function (resp) {
            var t = TryForm._genericInterfaces.find(function (x) { return x.Prefix == modelPrefix; });
            var prop = t.TypeDescription.Properties.find(function (x) { return x.PropertyName == "Email"; });
            var drawer = new FormDrawImplementation(t);
            var selList = resp.List.map(function (t) {
                return {
                    Value: t.Id,
                    Text: t.Email,
                    Selected: false
                };
            });
            var html = drawer.RenderDropDownList(prop, selList, false);
            FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);
            drawer.AfterFormDrawing();
        }, null, false);
    };
    return UserCreatePage;
}());
UserCreatePage.SetHandlers("create.");
