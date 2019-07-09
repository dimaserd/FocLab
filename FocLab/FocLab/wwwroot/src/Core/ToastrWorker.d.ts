declare var toastr: any;
declare class GenericBaseApiResponse<T> {
    constructor(isSucceeded: boolean, message: string, resp: T);
    IsSucceeded: boolean;
    Message: string;
    ResponseObject: T;
}
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
