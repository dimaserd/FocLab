interface DatePickerModel {
    DatePickerId: string;
    BackElementId: string;
}
declare class DatePickerUtils {
    static ActiveDatePickers: Array<DatePickerModel>;
    static InitDictionary(): void;
    static SetDatePicker(datePickerId: string, elementIdToUpdate: string, dateValue?: string | Date): void;
    static SetDateToDatePicker(datePickerId: string, dateValue: string | Date): void;
    static DDMMYYYYToMMDDDDYYYYString(s: string): string;
    static GetDateFromDatePicker(inputId: string): Date;
}
