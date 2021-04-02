var Pagination = (function () {
    function Pagination() {
    }
    Pagination.RenderPaginationToElementIds = function (model, elementIds) {
        var html = this.RenderPagination(model);
        for (var i = 0; i < elementIds.length; i++) {
            var id = elementIds[i];
            var elem = document.getElementById(id);
            if (elem == null) {
                alert("\u042D\u043B\u0435\u043C\u0435\u043D\u0442 \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D \u043F\u043E \u0443\u043A\u0430\u0437\u0430\u043D\u043E\u043C\u0443 \u0438\u0434\u0435\u043D\u0442\u0438\u0444\u0438\u043A\u0430\u0442\u043E\u0440\u0443 '" + id + "'");
            }
            elem.innerHTML = html;
        }
    };
    Pagination.RenderPagination = function (model) {
        if (model.PagesCount === 1) {
            return "";
        }
        var res = "<div class=\"table-responsive\">\n        <nav aria-label=\"Page navigation example\">\n            <ul class=\"pagination\">";
        for (var i = 0; i < model.PagesCount; i++) {
            var _class = i == model.CurrentPage ? "active" : "";
            var link = model.LinkFormat.format(model.PageSize.toString(), (i * model.PageSize).toString());
            res += "<li class=\"page-item " + _class + "\">\n                        <a class=\"page-link\" href=\"" + link + "\">" + (i + 1) + "</a>\n                    </li>";
        }
        res += "</ul>\n                </nav>\n                </div>";
        return res;
    };
    return Pagination;
}());
