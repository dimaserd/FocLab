var Template = (function () {
    function Template() {
    }
    return Template;
}());
var TemplateJoined = (function () {
    function TemplateJoined() {
    }
    return TemplateJoined;
}());
var TableParser = (function () {
    function TableParser() {
    }
    TableParser.GetTemplate = function (elem) {
        if (elem == null) {
            return null;
        }
        return {
            Text: elem != null ? elem.innerText : null,
            Translation: ""
        };
    };
    TableParser.Mapper = function (row) {
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
        };
    };
    TableParser.GetArray = function (rows) {
        console.log("GetArray", rows);
        return rows.map(TableParser.Mapper)
            .filter(function (x) { return x != null; })
            .filter(function (x) { return !(x.Description == null && x.EmailTemplate == null && x.Initiator == null && x.Rules == null && x.SmsTemplate == null && x.Time == null); })
            .filter(function (x) { return !(x.Description === "↵" && x.Type == "↵" && x.Initiator == "↵"); });
    };
    return TableParser;
}());
var TableFounder = (function () {
    function TableFounder() {
    }
    TableFounder.Find = function () {
        var t = document.getElementById("myTable");
        console.log("Find", t);
        var s = t.children[1].children;
        var d = Array.from(s);
        d.shift();
        return d;
    };
    return TableFounder;
}());
var t = TableFounder.Find();
var s = TableParser.GetArray(t);
console.log(s);
