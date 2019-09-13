interface Select2Option {
    id: string;
    text: string;
}

interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    select2(arg0: {
        theme: string; width: string;
        language: any,
        placeholder: string, dropdownAutoWidth: boolean;
        data: Select2Option[];
        templateResult: (state: any) => JQuery<HTMLElement>
    });
    select2(arg0: { width: string; });
    select2(arg0: {
        placeholder: string;
        language: { "noResults": () => string; };
        data: any;
        templateSelection: any;
        templateResult: any;
        escapeMarkup: (markup: any) => any;
    });
    select2(arg0: {
        placeholder: string;
        language: { "noResults": () => string; };
        escapeMarkup: (markup: any) => any;
    });
}

interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    sortable(arg0: { group: string; vertical: boolean; pullPlaceholder: boolean; onDrop: ($item: any, container: any, _super: any) => void; onDragStart: ($item: any, container: any, _super: any) => void; onDrag: ($item: any, position: any) => void; });
    sortable(arg0: { handle: string });
    sortable(arg0: string);
    selectpicker(arg0: string);
    //select2(arg0: { theme: string; width: string; dropdownAutoWidth: boolean; data: Select2Option[]; templateResult: any; });
    slider(arg0: { range: boolean; min: number; max: number; values: number[]; slide(event: any, ui: any): void; stop(): void; });    //Расширение интерфейса под слайдер

    daterangepicker(arg0: { autoUpdateInput: boolean; locale: { cancelLabel: string; applyLabel: string; daysOfWeek: string[]; monthNames: string[]; firstDay: number; }; });
}
interface JQuery<TElement = HTMLElement> extends Iterable<TElement> {
    datepicker(arg0: { format: string; autoclose: boolean; language: string; startDate: string; });
    daterangepicker(arg0: { autoUpdateInput: boolean; locale: { cancelLabel: string; applyLabel: string; daysOfWeek: string[]; monthNames: string[]; firstDay: number; }; });
}

/// <reference path="../../../node_modules/@types/bootstrap/index.d.ts"/>
/// <reference path="../../../node_modules/@types/bootstrap-datepicker/index.d.ts"/>

class Utils {

    public static SetDatePicker = (selector: string, startDate: string = null): void => {
        $.fn.datepicker['dates']['ru'] = {
            days: ['понедельник', 'воскресенье', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
            daysShort: ['вс', 'пн', 'вт', 'ср', 'чт', 'пт', 'сб'],
            daysMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
            months: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
                'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
            monthsShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
                'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
            today: "Today"
        };

        $(selector).datepicker({
            format: "dd/mm/yyyy",
            autoclose: true,
            language: "ru",
            startDate: startDate
        });
    }

    public static SetDateRangePicker(selector: string): void {

        $(selector).daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: "Очистить",
                applyLabel: "Применить",
                daysOfWeek: [
                    "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"
                ],
                monthNames: ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                    "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"],
                firstDay: 0
            }
        });

        $(selector).on("apply.daterangepicker", function (ev, picker) {
            $(this).val(picker.startDate.format("DD/MM/YYYY") + " - " + picker.endDate.format("DD/MM/YYYY"));
        });

        $(selector).on("cancel.daterangepicker", function (ev, picker) {
            $(this).val("");
        });

    };

    public static GetDateFromDateRangePicker(inputId: string): object {
        let inputDate: any = document.getElementById(inputId);

        if (inputDate === null) {
            alert(`Элемент с идентификатором ${inputId} не найден на странице проверьте код`);
            return null;
        }
        let dates = inputDate.value.replace(/ /g, "");
        dates = dates.split('-');
        if (dates !== "") {
            for (var d in dates) {
                var tempStr = dates[d].split('/');
                tempStr.reverse();
                dates[d] = tempStr.join('-');
            };
            let data: any = {};
            if (dates[0] !== "") {

                data = {
                    Min: dates[0]
                }
            }

            if (dates[1] != undefined)
                data.Max = dates[1];
            console.log(data, dates[0], dates[1]);
            return data;
        } else {
            return null;
        }
    };

    public static GetDateFromDatePicker(inputId: string): object {
        let inputDate: any = document.getElementById(inputId);

        if (inputDate === []) {
            alert(`Элемент с идентификатором ${inputId} не найден на странице проверьте код`);
            return null;
        }
        var date = inputDate.value.replace(/ /g, "");
        if (date !== "") {
            console.log("Not ''");
            const tempStr = date.split('/');
            tempStr.reverse();
            date = tempStr.join('-');

            return (date);
        } else {
            return null;
        }
    }

    public static FillSelect(select: HTMLElement, array: Array<string>, htmlFunc: Function, valueFunc: Function): void {

        for (let i = 0; i < array.length; i++) {
            const opt = document.createElement("option");
            opt.innerHTML = htmlFunc(array[i]);
            opt.value = valueFunc(array[i]);
            select.append(opt);
        }
    }

    public static GetImageLinkByFileId(fileId: number, sizeType: ImageSizeType): string {
        return `/FileCopies/Images/${sizeType.toString()}/${fileId}.jpg`;
    }
}

enum ImageSizeType {
    Icon = <any>"Icon",
    Medium = <any>"Medium",
    Small = <any>"Small",
    Original = <any>"Original"
}