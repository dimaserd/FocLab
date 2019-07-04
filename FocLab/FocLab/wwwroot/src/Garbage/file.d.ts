declare class Template {
    Text: string;
    Translation: string;
}
declare class TemplateJoined {
    Trigger: string;
    Rules: string;
    Description: string;
    SmsTemplate: Template;
    EmailTemplate: Template;
    Time: string;
    Initiator: string;
    Type: string;
}
declare class TableParser {
    static GetTemplate(elem: HTMLTableDataCellElement): Template;
    static Mapper(row: HTMLTableRowElement): TemplateJoined;
    static Func(rows: Array<HTMLTableRowElement>): Array<TemplateJoined>;
}
declare class TableFounder {
    static Find(): Array<HTMLTableRowElement>;
}
