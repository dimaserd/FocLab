interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    daterangepicker(arg0: {
        autoUpdateInput: boolean;
        locale: {
            cancelLabel: string;
            applyLabel: string;
            daysOfWeek: string[];
            monthNames: string[];
            firstDay: number;
        };
    }): any;
}
interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    datepicker(arg0: {
        format: string;
        autoclose: boolean;
        language: string;
        startDate: string;
    }): any;
    daterangepicker(arg0: {
        autoUpdateInput: boolean;
        locale: {
            cancelLabel: string;
            applyLabel: string;
            daysOfWeek: string[];
            monthNames: string[];
            firstDay: number;
        };
    }): any;
}
declare class Utils {
    static SetDatePicker: (selector: string, startDate?: string) => void;
    static SetDateRangePicker: (selector: string) => void;
    static GetDateFromDateRangePicker: (inputId: string) => object;
    static GetDateFromDatePicker: (inputId: string) => object;
    static FillSelect: (select: HTMLElement, array: string[], htmlFunc: Function, valueFunc: Function) => void;
}