declare var toastr: any;
declare class BaseApiResponse {
    constructor(isSucceeded: boolean, message: string);
    IsSucceeded: boolean;
    Message: string;
}
declare class ToastrWorker {
    static ShowError: (text: string) => void;
    static ShowSuccess: (text: string) => void;
    static HandleBaseApiResponse: (data: BaseApiResponse) => void;
}
