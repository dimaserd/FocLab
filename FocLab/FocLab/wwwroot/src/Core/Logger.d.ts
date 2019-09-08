/// <reference types="jquery" />
declare class Logger {
    static Resourcses: Logger_Resx;
    static SetResources(): void;
    static LogException(exception: JQuery.Ajax.ErrorTextStatus, link: string): void;
    static LogAction(message: string, description: string, groupName: string): void;
}
declare class Logger_Resx {
    LoggingAttempFailed: string;
    ErrorOnApiRequest: string;
    ActionLogged: string;
    ExceptionLogged: string;
    ErrorOccuredOnLoggingException: string;
}
