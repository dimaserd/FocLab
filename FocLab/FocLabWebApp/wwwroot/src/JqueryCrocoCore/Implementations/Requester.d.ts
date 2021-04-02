declare class Requester_Resx {
    YouPassedAnEmtpyArrayOfObjects: string;
    ErrorOccuredWeKnowAboutIt: string;
    FilesNotSelected: string;
}
declare class Requester implements ICrocoRequester {
    static Resources: Requester_Resx;
    static GoingRequests: string[];
    GetParams(data: object): string;
    DeleteCompletedRequest(link: string): void;
    SendPostRequestWithAnimation<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    UploadFilesToServer<TObject>(inputId: string, link: string, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    static OnSuccessAnimationHandler(data: IBaseApiResponse): void;
    static OnErrorAnimationHandler(): void;
    Get<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    SendAjaxPostInner(link: string, data: Object, onSuccessFunc: Function, onErrorFunc: Function, animations: boolean, logOnError: boolean): void;
    Post<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
}
