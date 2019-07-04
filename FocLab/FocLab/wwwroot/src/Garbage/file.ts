class Template {
    Text: string;
    Translation: string;
}

class TemplateJoined {
    Trigger: string;
    Rules: string;
    Description: string;
    SmsTemplate: Template;
    EmailTemplate: Template;
    Time: string;
    Initiator: string;
    Type: string;
}

class TableParser {

    static GetTemplate(elem: HTMLTableDataCellElement): Template {
        return {
            Text: elem.innerText,
            Translation: ""
        };
    }

    static Mapper(row: HTMLTableRowElement): TemplateJoined {
        return {
            Trigger: row.cells[1].innerText,
            Rules: row.cells[2].innerText,
            Description: row.cells[3].innerText,
            SmsTemplate: TableParser.GetTemplate(row.cells[4]),
            EmailTemplate: TableParser.GetTemplate(row.cells[5]),
            Time: row.cells[6].innerText,
            Initiator: row.cells[7].innerText,
            Type: row.cells[8].innerText
        }
    }

    static GetArray(rows: Array<HTMLTableRowElement>): Array<TemplateJoined> {

        return rows.map(TableParser.Mapper);
    }
}

class TableFounder {
    static Find(): Array<HTMLTableRowElement> {
        var t = document.getElementsByClassName("wrapped relative-table confluenceTable")[0];
        return (t.children[1] as unknown as Array<HTMLTableRowElement>);
    }
}

var t = TableFounder.Find();
var s = TableParser.GetArray(t);

console.log(s);

