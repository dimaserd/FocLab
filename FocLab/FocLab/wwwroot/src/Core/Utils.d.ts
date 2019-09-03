interface Select2Option {
    id: string;
    text: string;
}
interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    select2(arg0: {
        theme: string;
        width: string;
        language: any;
        placeholder: string;
        dropdownAutoWidth: boolean;
        data: Select2Option[];
        templateResult: (state: any) => JQuery<HTMLElement>;
    }): any;
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
}
interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    sortable(arg0: {
        group: string;
        vertical: boolean;
        pullPlaceholder: boolean;
        onDrop: ($item: any, container: any, _super: any) => void;
        onDragStart: ($item: any, container: any, _super: any) => void;
        onDrag: ($item: any, position: any) => void;
    }): any;
    sortable(arg0: {
        handle: string;
    }): any;
    sortable(arg0: string): any;
    selectpicker(arg0: string): any;
    slider(arg0: {
        range: boolean;
        min: number;
        max: number;
        values: number[];
        slide(event: any, ui: any): void;
        stop(): void;
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
    static GetDateFromDateRangePicker(inputId: string): object;
    static GetDateFromDatePicker(inputId: string): object;
    static FillSelect(select: HTMLElement, array: Array<string>, htmlFunc: Function, valueFunc: Function): void;
    static GetImageLinkByFileId(fileId: number, sizeType: ImageSizeType): string;
}
declare enum ImageSizeType {
    Icon,
    Medium,
    Small,
    Original
}
