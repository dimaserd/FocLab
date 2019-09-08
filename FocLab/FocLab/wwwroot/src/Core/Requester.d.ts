declare class Requester_Resx {
    YouPassedAnEmtpyArrayOfObjects: string;
    ErrorOccuredWeKnowAboutIt: string;
    FilesNotSelected: string;
}
declare class Requester {
    static Resources: Requester_Resx;
    static GoingRequests: string[];
    static DeleteCompletedRequest: (link: string) => void;
    static ParseDate(date: string): string;
    static GetCombinedData(prefix: string, obj: Object): Object;
    static GetParams: (obj: Object) => string;
    static SendPostRequestWithAnimation(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function): void;
    static UploadFilesToServer(inputId: string, onSuccessFunc: Function, onErrorFunc: Function): void;
    static IsRequestGoing(link: string): boolean;
    static OnSuccessAnimationHandler(data: BaseApiResponse): void;
    static OnErrorAnimationHandler(): void;
    static SendAjaxGet(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function): void;
    static SendAjaxPost(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean): void;
}
