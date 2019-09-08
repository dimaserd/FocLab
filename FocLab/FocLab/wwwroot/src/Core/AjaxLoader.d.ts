declare class AjaxLoader {
    static InitAjaxLoads: () => void;
    static LoadInnerHtmlToElement: (element: Element, onSuccessFunc: Function) => void;
}
