interface ApplicationUser {
    Id: string;
    Email: string;
    Name: string;
    AvatarFileId: number;
}

class GetListResut<T>{
    TotalCount: number;
    List: Array<T>;
    Count: number;
    OffSet: number;
}

class UserCreatePage {

    static SetHandlers(modelPrefix: string): void {
        $(".action-btn").on("click", () => {

            const renderModel = CrocoAppCore.GenericInterfaceHelper
                .FormHelper._genericInterfaces.find(x => x.FormModel.Prefix === modelPrefix).FormModel;

            const data = CrocoAppCore.Application.FormDataHelper
                .CollectDataByPrefixWithTypeMatching(renderModel.Prefix, renderModel.TypeDescription);

            CrocoAppCore.Application.Requester.SendPostRequestWithAnimation<IBaseApiResponse>("/Api/User/Create", data, resp => {
                if (resp.IsSucceeded) {
                    setTimeout(() => { location.href = '/Admin/Users/Index' }, 1000)
                }
            }, null);
        });
    }
}

UserCreatePage.SetHandlers("create.");