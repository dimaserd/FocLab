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

            console.log("UserCreatePage.Click");

            const data = TryForm.GetDataForFormByModelPrefix(modelPrefix);;

            Requester.SendPostRequestWithAnimation("/Api/User/Create", data, resp => {
                if (resp.IsSucceeded) {
                    setTimeout(() => { location.href = '/Admin/Users/Index' }, 1000)
                }
            }, null);
        });
    }

    static AfterDrawHandler(modelPrefix: string): void {

        Requester.SendAjaxPost("/Api/User/Get", { Count: null, OffSet: 0 }, (resp: GetListResut<ApplicationUser>) => {

            let t = TryForm._genericInterfaces.find(x => x.Prefix == modelPrefix);

            let prop = t.TypeDescription.Properties.find(x => x.PropertyName == "Email")

            let drawer: FormDrawImplementation = new FormDrawImplementation(t);

            let selList: Array<SelectListItem> = resp.List.map(t => {
                return {
                    Value: t.Id,
                    Text: t.Email,
                    Selected: false
                }
            });

            let html = drawer.RenderDropDownList(prop, selList, false);
            
            FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);

            drawer.AfterFormDrawing();

        }, null, false)
    }
}

UserCreatePage.SetHandlers("create.")

//UserCreatePage.AfterDrawHandler("create.");