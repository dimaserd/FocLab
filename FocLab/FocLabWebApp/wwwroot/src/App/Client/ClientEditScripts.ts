interface EditClient {
    Name: string;
    BirthDate: Date;
    Surname: string;
    Patronymic: string;
    Sex?: boolean;
    PhoneNumber: string;
}

class ClientEditor {
    
    static getUserData(): EditClient {

        var data: EditClient = {
            Name: "",
            BirthDate: new Date(),
            Surname: "",
            Sex: null,
            Patronymic: "",
            PhoneNumber: "",
        };

        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "client.");

        return data;
    }

    static Edit() : void {
        const data = ClientEditor.getUserData();

        console.log("data  ", data);

        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Client/Update", data, (resp: IBaseApiResponse) => {
            if (resp.IsSucceeded) {
                setTimeout(() => location.href = '/Client', 1000)
            }
        }, null);
    }

    static BtnUpdateAvatar() {

        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");

        //Загружаю файлы на сервер по идентификатору инпута где находятся файлы
        CrocoAppCore.Application.Requester.UploadFilesToServer("ImageInput", "/Api/FilesDirectory/UploadFiles", (x: IGenericBaseApiResponse<number[]>) => {

            //При успешной загрузке файлов метод вернет идентификаторы файлов из бд
            if (x.IsSucceeded) {
                //Устанавливаю идентификатор файла, как первый попавшийся из закачанных файлов
                const fileId = Number(x.ResponseObject[0]);

                CrocoAppCore.Application.Requester.SendPostRequestWithAnimation(`/Api/Client/UpdateClientPhoto?fileId=${fileId}`, {}, (resp: IBaseApiResponse) => {
                    if (resp.IsSucceeded) {
                        setTimeout(() => location.reload(), 1200)
                    }
                }, null);
            }
        }, () => CrocoAppCore.Application.ModalWorker.HideModals());
    }

    static SetHandlers() {
        $(".client-save-changes").on("click", () => ClientEditor.Edit());
        $(".client-update-avatar").on("click", () => ClientEditor.BtnUpdateAvatar());
        DatePickerUtils.SetDatePicker("birthDateDatePicker", "birthDate");
    }
}

ClientEditor.SetHandlers();