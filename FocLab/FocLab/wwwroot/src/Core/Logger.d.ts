/// <reference types="jquery" />
declare class Logger_Resx {
    LoggingAttempFailed: string;
    ErrorOnApiRequest: string;
    ActionLogged: string;
    ExceptionLogged: string;
    ErrorOccuredOnLoggingException: string;
}
declare class Logger {
    static Resourcses: Logger_Resx;
    static LogException(exception: JQuery.Ajax.ErrorTextStatus, link: string): void;
    static LogAction(message: string, description: string, groupName: string): void;
}
