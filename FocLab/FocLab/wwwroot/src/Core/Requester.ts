class AjaxParameters {
    public type: String;
    public data: Object;
    public url: String;
    public async: Boolean;
    public cache: Boolean;
    public dataType: String;
    public contentType: String;
    public processData: Boolean;
    public success: Function;
    public error: Function;
}

class Requester {

    static GoingRequests = new Array<string>();

    static DeleteCompletedRequest = (link: string): void => {
        Requester.GoingRequests = Requester.GoingRequests.filter(x => x !== link);
    }

    static ParseDate = (date: string): string => {

        date = date.replace(new RegExp("/", 'g'), ".");
        const from = date.split(".");

        const d = new Date(+from[2], +from[1] - 1, +from[0]);

        return d.toISOString();
    }

    static GetCombinedData = (prefix: string, obj: Object): Object => {

        const resultObj = {};

        for (let prop in obj) {
            if (Array.isArray(obj[prop])) {
                resultObj[prefix + prop] = obj[prop];
            }
            else if (typeof obj[prop] == "object") {
                const objWithProps = Requester.GetCombinedData(`${prefix}${prop}.`, obj[prop]);

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

    static GetParams = (obj: Object): string => {

        obj = Requester.GetCombinedData("", obj);

        return $.param(obj, true);
    }

    static SendPostRequestWithAnimation(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function):void {

        //Показываю крутилку
        ModalWorker.ShowModal("loadingModal");

        Requester.SendAjaxPost(link, data, onSuccessFunc, onErrorFunc, true);
    }

    static UploadFilesToServer(inputId: string, onSuccessFunc: Function, onErrorFunc: Function) {

        const link = "/Api/FilesDirectory/UploadFiles";

        if (Requester.IsRequestGoing(link)) {
            return;
        }

        const file_data = $(`#${inputId}`).prop("files");

        const form_data = new FormData();

        if (file_data.length === 0) {
                
            ToastrWorker.HandleBaseApiResponse(new BaseApiResponse(false, "Файлы не выбраны"));
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
            success: response => {
                console.log(response);
                Requester.DeleteCompletedRequest(link);
                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },
            error: (jqXHR, textStatus, errorThrown) => {

                console.log(jqXHR, textStatus, errorThrown);

                //Логгирую ошибку
                Logger.LogException(textStatus, link);

                Requester.DeleteCompletedRequest(link);
                ModalWorker.HideModals();

                var resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, скоро с ней разберемся!");

                ToastrWorker.HandleBaseApiResponse(resp);

                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }

            }
        });

    }

    static IsRequestGoing = (link: string): boolean => {
        const any = Requester.GoingRequests.filter(x => x === link);

        return any.length > 0;
    }

    static OnSuccessAnimationHandler = (data: BaseApiResponse): void => {
        ModalWorker.HideModals();
        ToastrWorker.HandleBaseApiResponse(data);
    }

    static OnErrorAnimationHandler = (): void => {

        ModalWorker.HideModals();

        const resp = new BaseApiResponse(false, "Произошла ошибка! Мы уже знаем о ней, и скоро с ней разберемся!");
        
        ToastrWorker.HandleBaseApiResponse(resp);
    }

    static SendAjaxGet(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function) {

        if (Requester.IsRequestGoing(link)) {
            return;
        }

        const params = {
            type: "GET",
            data: data,
            url: link,
            async: true,
            cache: false,
            success: response => {
                Requester.DeleteCompletedRequest(link);

                if (onSuccessFunc) {
                    onSuccessFunc(response);
                }
            },

            error: (jqXHR, textStatus, errorThrown) => {
                //Логгирую ошибку
                Logger.LogException(textStatus, link);

                Requester.DeleteCompletedRequest(link);

                //Вызываю внешнюю функцию-обработчик
                if (onErrorFunc) {
                    onErrorFunc(jqXHR, textStatus, errorThrown);
                }

            }
        };

        $.ajax(params);
    }

    static SendAjaxPost(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean) {

        if (Requester.IsRequestGoing(link)) {
            return;
        }

        if (data == null) {
            alert("Вы подали пустой объект в запрос");
            return;
        }


        let params: any = {};

        params.type = "POST";
        params.data = data;
        params.url = link;
        params.async = true;
        params.cache = false;
        params.success = response => {
            Requester.DeleteCompletedRequest(link);

            if (animations) {
                Requester.OnSuccessAnimationHandler(response);
            }

            if (onSuccessFunc) {
                onSuccessFunc(response);
            }
        };

        params.error = (jqXHR, textStatus, errorThrown) => {
            //Логгирую ошибку
            Logger.LogException(textStatus, link);

            Requester.DeleteCompletedRequest(link);

            if (animations) {
                Requester.OnErrorAnimationHandler();
            }

            //Вызываю внешнюю функцию-обработчик
            if (onErrorFunc) {
                onErrorFunc(jqXHR, textStatus, errorThrown);
            }

        };

        const isArray = data.constructor === Array;

        if (isArray) {
            params.contentType = "application/json; charset=utf-8";
            params.dataType = "json";
            params.data = JSON.stringify(data);
        }

        Requester.GoingRequests.push(link);

        $.ajax(params);    

        console.log(`POST запрос ${link}`, data);
    }

}