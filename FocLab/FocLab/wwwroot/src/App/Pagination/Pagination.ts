class Pagination {
    static RenderPaginationToElementIds(model: PagerModel, elementIds: Array<string>): void {
        let html = this.RenderPagination(model);

        for (let i = 0; i < elementIds.length; i++) {
            let id = elementIds[i];
            let elem = document.getElementById(id);

            if (elem == null) {
                alert(`Элемент не найден по указаному идентификатору '${id}'`);
            }

            elem.innerHTML = html;
        }
    }

    static RenderPagination(model: PagerModel): string {

        if (model.PagesCount === 1) {
            return "";
        }

        var res = `<div class="table-responsive">
        <nav aria-label="Page navigation example">
            <ul class="pagination">`
        for (let i = 0; i < model.PagesCount; i++)
        {
            var _class = i == model.CurrentPage ? "active" : "";

            var link = model.LinkFormat.format(model.PageSize.toString(), (i * model.PageSize).toString());

            res += `<li class="page-item ${_class}">
                        <a class="page-link" href="${link}">${i + 1}</a>
                    </li>`;
        }
        res += `</ul>
                </nav>
                </div>`;

        return res;
    }
}