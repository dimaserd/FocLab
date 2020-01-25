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

            const data = TryForm.GetDataForFormByModelPrefix(modelPrefix);

            CrocoAppCore.Application.Requester.SendPostRequestWithAnimation<IBaseApiResponse>("/Api/User/Create", data, resp => {
                if (resp.IsSucceeded) {
                    setTimeout(() => { location.href = '/Admin/Users/Index' }, 1000)
                }
            }, null);
        });
    }
}

UserCreatePage.SetHandlers("create.");