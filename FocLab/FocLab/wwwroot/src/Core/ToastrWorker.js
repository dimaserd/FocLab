var GenericBaseApiResponse = /** @class */ (function () {
    function GenericBaseApiResponse(isSucceeded, message, resp) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
        this.ResponseObject = resp;
    }
    return GenericBaseApiResponse;
}());
var BaseApiResponse = /** @class */ (function () {
    function BaseApiResponse(isSucceeded, message) {
        this.IsSucceeded = isSucceeded;
        this.Message = message;
    }
    return BaseApiResponse;
}());
var ToastrWorker = /** @class */ (function () {
    function ToastrWorker() {
    }
    ToastrWorker.ShowError = function (text) {
        var data = {
            IsSucceeded: false,
            Message: text
        };
        ToastrWorker.HandleBaseApiResponse(data);
    };
    ToastrWorker.ShowSuccess = function (text) {
        var data = {
            IsSucceeded: true,
            Message: text
        };
        ToastrWorker.HandleBaseApiResponse(data);
    };
    ToastrWorker.HandleBaseApiResponse = function (data) {
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
    };
    return ToastrWorker;
}());
