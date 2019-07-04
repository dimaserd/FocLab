declare class AjaxParameters {
    type: String;
    data: Object;
    url: String;
    async: Boolean;
    cache: Boolean;
    dataType: String;
    contentType: String;
    processData: Boolean;
    success: Function;
    error: Function;
}
declare class Requester {
    static GoingRequests: string[];
    static DeleteCompletedRequest: (link: string) => void;
    static ParseDate: (date: string) => string;
    static GetCombinedData: (prefix: string, obj: Object) => Object;
    static GetParams: (obj: Object) => string;
    static SendPostRequestWithAnimation(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function): void;
    static UploadFilesToServer(inputId: string, onSuccessFunc: Function, onErrorFunc: Function): void;
    static IsRequestGoing: (link: string) => boolean;
    static OnSuccessAnimationHandler: (data: BaseApiResponse) => void;
    static OnErrorAnimationHandler: () => void;
    static SendAjaxGet(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function): void;
    static SendAjaxPost(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean): void;
}
