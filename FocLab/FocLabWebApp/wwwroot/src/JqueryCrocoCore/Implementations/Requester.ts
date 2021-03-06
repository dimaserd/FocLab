class Requester_Resx {
    YouPassedAnEmtpyArrayOfObjects: string = "Вы подали пустой объект в запрос";
    ErrorOccuredWeKnowAboutIt: string = "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!";
    FilesNotSelected: string = "Файлы не выбраны";
}

class Requester implements ICrocoRequester {

    static Resources: Requester_Resx = new Requester_Resx();

    static GoingRequests = new Array<string>();

    GetParams(data: object): string {
        return $.param(data);
    }

    DeleteCompletedRequest(link: string): void {
        Requester.GoingRequests = Requester.GoingRequests.filter(x => x !== link);
    }

    SendPostRequestWithAnimation<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function):void {
        this.SendAjaxPostInner(link, data, onSuccessFunc, onErrorFunc, true, true);
    }

    UploadFilesToServer<TObject>(inputId: string, link: string, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function) {

        const file_data = $(`#${inputId}`).prop("files");

        const form_data = new FormData();

        if (file_data.length === 0) {

            CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.FilesNotSelected);
            if (onErrorFunc) {
                onErrorFunc();
            }
            return;
        }

        for (let i = 0; i < file_data.length; i++) {
            form_data.append("Files", file_data[i]);
        }


        $.ajax({
            url: link,
            type: "POST",
            data: form_data,
            async: true,
            cache: false,
            dataType: "json",
            contentType: false,
            processData: false,
            success: (response => {
                this.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            }).bind(this),
            error: ((jqXHR, textStatus, errorThrown) => {

                //Логгирую ошибку
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "ErrorOnApiRequest", link);

                this.DeleteCompletedRequest(link);
                CrocoAppCore.Application.ModalWorker.HideModals();

                CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.ErrorOccuredWeKnowAboutIt);

                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }
            }).bind(this)
        });

    }

    static OnSuccessAnimationHandler(data: IBaseApiResponse): void {
        CrocoAppCore.Application.ModalWorker.HideModals();
        CrocoAppCore.ToastrWorker.HandleBaseApiResponse(data);
    }

    static OnErrorAnimationHandler(): void {

        CrocoAppCore.Application.ModalWorker.HideModals();

        CrocoAppCore.ToastrWorker.ShowError(Requester.Resources.ErrorOccuredWeKnowAboutIt);
    }

    Get<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function) {

        CrocoAppCore.Application.FormDataUtils.ProccessAllDateTimePropertiesAsString(data);
        CrocoAppCore.Application.FormDataUtils.ProccessAllNumberPropertiesAsString(data);

        const params = {
            type: "GET",
            data: data,
            url: link,
            async: true,
            cache: false,
            success: (response => {
                this.DeleteCompletedRequest(link);

                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            }).bind(this),

            error: ((jqXHR, textStatus, errorThrown) => {
                //Логгирую ошибку
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "Error on Api Request", link);

                this.DeleteCompletedRequest(link);

                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }

            }).bind(this)
        };

        $.ajax(params);
    }


    public SendAjaxPostInner(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean, logOnError: boolean) {

        if (data == null) {
            data = {};
        }

        let params: any = {};

        params.type = "POST";
        params.data = JSON.stringify(data);
        params.url = link;
        params.contentType = "application/json; charset=utf-8",
        params.dataType = "json",

        params.success = (response => {
            this.DeleteCompletedRequest(link);

            if (animations) {
                Requester.OnSuccessAnimationHandler(response);
            }

            if (onSuccessFunc) {
                onSuccessFunc(response);
            }
        }).bind(this);

        params.error = ((jqXHR, textStatus, errorThrown) => {

            if (logOnError) {
                //Логгирую ошибку
                CrocoAppCore.Application.Logger.LogException(textStatus.toString(), "Error on Api Request", link);
            }

            this.DeleteCompletedRequest(link);

            if (animations) {
                Requester.OnErrorAnimationHandler();
            }

            //Вызываю внешнюю функцию-обработчик
            if (onErrorFunc) {
                onErrorFunc(jqXHR, textStatus, errorThrown);
            }

        }).bind(this);

        Requester.GoingRequests.push(link);

        $.ajax(params);
    }

    Post<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function) {
        this.SendAjaxPostInner(link, data, onSuccessFunc, onErrorFunc, false, true);
    }
}