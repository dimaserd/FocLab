/// <reference types="jquery" />
declare class Logger {
    static LogException: (exception: JQuery.Ajax.ErrorTextStatus, link: string) => void;
    static LogAction: (message: string, description: string, groupName: string) => void;
}
