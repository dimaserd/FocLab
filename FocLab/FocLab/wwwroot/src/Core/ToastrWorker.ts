declare var toastr: any;

class GenericBaseApiResponse<T> {


    constructor(isSucceeded: boolean, message: string, resp: T) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
        this.ResponseObject = resp;
    }

    public IsSucceeded: boolean;
    public Message: string;
    public ResponseObject: T;

}

class BaseApiResponse {

    
    constructor(isSucceeded: boolean, message: string) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
    }

    public IsSucceeded: boolean;
    public Message: string;

}

class ToastrWorker {
    

    public static ShowError(text: string) : void {
        const data = {
            IsSucceeded: false,
            Message: text
        };

        ToastrWorker.HandleBaseApiResponse(data);
    }


    public static ShowSuccess(text : string) : void {
        const data = {
            IsSucceeded: true,
            Message: text
        };

        ToastrWorker.HandleBaseApiResponse(data);
    }

    public static HandleBaseApiResponse(data: BaseApiResponse): void {

        console.log("HandleBaseApiResponse", data);

        if (data.IsSucceeded === undefined) {
            alert("Произошла ошибка. Объект не является типом BaseApiResponse");
            return;
        }

        if (data.Message === undefined) {
            alert("Произошла ошибка. Объект не является типом BaseApiResponse");
            return;
        }

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        if (data.IsSucceeded) {
            toastr.success(data.Message);
        }
        else {
            toastr.error(data.Message);
        }

    }
}