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

        if (elem == null) {
            return null;
        }

        return {
            Text: elem != null? elem.innerText : null,
            Translation: ""
        };
    }

    static Mapper(row: HTMLTableRowElement): TemplateJoined {

        console.log("Mapper", row);

        if (row.cells[1] == null) {
            return null;
        }

        return {
            Trigger: row.cells[1] != null ? row.cells[1].innerText : null,
            Rules: row.cells[2] != null ? row.cells[2].innerText : null,
            Description: row.cells[3] != null ? row.cells[3].innerText : null,
            SmsTemplate: TableParser.GetTemplate(row.cells[4]),
            EmailTemplate: TableParser.GetTemplate(row.cells[5]),
            Time: row.cells[6] != null ? row.cells[6].innerText : null,
            Initiator: row.cells[7] != null ? row.cells[7].innerText : null,
            Type: row.cells[8] != null ? row.cells[8].innerText : null
        }
    }

    static GetArray(rows: Array<HTMLTableRowElement>): Array<TemplateJoined> {

        console.log("GetArray", rows);
        return rows.map(TableParser.Mapper)
            .filter(x => x != null)
            .filter(x => !(x.Description == null && x.EmailTemplate == null && x.Initiator == null && x.Rules == null && x.SmsTemplate == null && x.Time == null))
            .filter(x => !(x.Description === "↵" && x.Type == "↵" && x.Initiator == "↵"));
    }
}

class TableFounder {
    static Find(): Array<HTMLTableRowElement> {
        var t = document.getElementById("myTable");

        console.log("Find", t);

        var s: HTMLCollection = t.children[1].children;

        var d = Array.from(s);

        d.shift();

        return d as Array<HTMLTableRowElement>;
    }
}

var t = TableFounder.Find();
var s = TableParser.GetArray(t);

console.log(s);

