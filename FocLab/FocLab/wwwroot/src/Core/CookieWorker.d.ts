declare class CookieWorker {
    static setCookie: (name: any, value: any, days: any) => void;
    static getCookie: (name: any) => string;
    static eraseCookie: (name: any) => void;
}
