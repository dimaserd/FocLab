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
            var renderModel = CrocoAppCore.GenericInterfaceHelper
                .FormHelper._genericInterfaces.find(function (x) { return x.FormModel.Prefix === modelPrefix; }).FormModel;
            var data = CrocoAppCore.Application.FormDataHelper
                .CollectDataByPrefixWithTypeMatching(renderModel.Prefix, renderModel.TypeDescription);
            CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/User/Create", data, function (resp) {
                if (resp.IsSucceeded) {
                    setTimeout(function () { location.href = '/Admin/Users/Index'; }, 1000);
                }
            }, null);
        });
    };
    return UserCreatePage;
}());
UserCreatePage.SetHandlers("create.");
