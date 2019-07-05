class DefaultHandlers {
    static IfSuccessReloadPageAfter1500MSecs(x: BaseApiResponse) {
        if (x.IsSucceeded) {
            setTimeout(() => location.reload(), 1500);
        }
    }

    static NoHandler(x: any) {

    }
}