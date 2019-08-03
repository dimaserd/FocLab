interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    select2(arg0: {
        width: string;
    }): any;
    select2(arg0: {
        placeholder: string;
        language: {
            "noResults": () => string;
        };
        data: any;
        templateSelection: any;
        templateResult: any;
        escapeMarkup: (markup: any) => any;
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
    static GetDateFromDatePicker: (inputId: string) => string;
    static FillSelect: (select: HTMLElement, array: object[], htmlFunc: Function, valueFunc: Function) => void;
}
