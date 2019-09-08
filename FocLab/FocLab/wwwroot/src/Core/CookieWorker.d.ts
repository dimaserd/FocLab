declare class CookieWorker {
    static setCookie(name: string, value: string, days: number): void;
    static getCookie(name: string): string;
    static eraseCookie(name: string): void;
}
