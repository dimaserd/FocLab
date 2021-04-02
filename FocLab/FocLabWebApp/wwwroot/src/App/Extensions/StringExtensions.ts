interface String {
    format(...params: String[]): string;
}

String.prototype.format = function (...params: String[]) {
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof params[number] != 'undefined' ? params[number] : match;
    });
};