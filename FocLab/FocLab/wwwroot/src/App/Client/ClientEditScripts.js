var ClientEditor = (function () {
    function ClientEditor() {
    }
    ClientEditor.getUserData = function () {
        var data = {
            Name: "",
            BirthDate: new Date(),
            Surname: "",
            Sex: null,
            Patronymic: "",
            PhoneNumber: "",
        };
        CrocoAppCore.Application.FormDataHelper.CollectDataByPrefix(data, "client.");
        return data;
    };
    ClientEditor.Edit = function () {
        var data = ClientEditor.getUserData();
        console.log("data  ", data);
        CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Client/Update", data, function (resp) {
            if (resp.IsSucceeded) {
                setTimeout(function () { return location.href = '/Client'; }, 1000);
            }
        }, null);
    };
    ClientEditor.BtnUpdateAvatar = function () {
        CrocoAppCore.Application.ModalWorker.ShowModal("loadingModal");
        CrocoAppCore.Application.Requester.UploadFilesToServer("ImageInput", "/Api/FilesDirectory/UploadFiles", function (x) {
            if (x.IsSucceeded) {
                var fileId = Number(x.ResponseObject[0]);
                CrocoAppCore.Application.Requester.SendPostRequestWithAnimation("/Api/Client/UpdateClientPhoto?fileId=" + fileId, {}, function (resp) {
                    if (resp.IsSucceeded) {
                        setTimeout(function () { return location.reload(); }, 1200);
                    }
                }, null);
            }
        }, function () { return CrocoAppCore.Application.ModalWorker.HideModals(); });
    };
    ClientEditor.SetHandlers = function () {
        $(".client-save-changes").on("click", function () { return ClientEditor.Edit(); });
        $(".client-update-avatar").on("click", function () { return ClientEditor.BtnUpdateAvatar(); });
        DatePickerUtils.SetDatePicker("birthDateDatePicker", "birthDate");
    };
    return ClientEditor;
}());
ClientEditor.SetHandlers();
