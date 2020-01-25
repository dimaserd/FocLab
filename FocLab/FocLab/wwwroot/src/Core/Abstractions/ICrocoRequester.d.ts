interface ICrocoRequester {
    UploadFilesToServer<TObject>(inputId: string, link: string, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    Get<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
    Post<TObject>(link: string, data: Object, onSuccessFunc: (x: TObject) => void, onErrorFunc: Function): void;
}
