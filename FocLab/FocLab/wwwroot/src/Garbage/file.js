var Template = /** @class */ (function () {
    function Template() {
    }
    return Template;
}());
var TemplateJoined = /** @class */ (function () {
    function TemplateJoined() {
    }
    return TemplateJoined;
}());
var TableParser = /** @class */ (function () {
    function TableParser() {
    }
    TableParser.GetTemplate = function (elem) {
        return {
            Text: elem.innerText,
            Translation: ""
        };
    };
    TableParser.Mapper = function (row) {
        return {
            Trigger: row.cells[1].innerText,
            Rules: row.cells[2].innerText,
            Description: row.cells[3].innerText,
            SmsTemplate: TableParser.GetTemplate(row.cells[4]),
            EmailTemplate: TableParser.GetTemplate(row.cells[5]),
            Time: row.cells[6].innerText,
            Initiator: row.cells[7].innerText,
            Type: row.cells[8].innerText
        };
    };
    TableParser.Func = function (rows) {
        return rows.map(TableParser.Mapper);
    };
    return TableParser;
}());
var TableFounder = /** @class */ (function () {
    function TableFounder() {
    }
    TableFounder.Find = function () {
        var t = document.getElementsByClassName("wrapped relative-table confluenceTable")[0];
        return t.children[1];
    };
    return TableFounder;
}());
