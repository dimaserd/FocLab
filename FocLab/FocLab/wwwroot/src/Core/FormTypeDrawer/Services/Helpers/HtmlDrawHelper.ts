class HtmlDrawHelper {
    static RenderSelect(className: string, propName: string, selectList: SelectListItem[]): string {

        var select = `<select class="${className}" name="${propName}">`;

        for (let i = 0; i < selectList.length; i++) {
            var item = selectList[i];
            var selected = item.Selected ? ` selected="selected"` : '';
            select += `<option${selected} value="${item.Value}">${item.Text}</option>`;
        }
        select += `</select>`;


        return select;
    }

    static ProceesSelectValues(typeDescription: CrocoTypeDescription, rawValue: string, selectList: SelectListItem[]): SelectListItem[] {
        if (rawValue != null) {
            selectList.forEach(x => x.Selected = false);

            //Заплатка для выпадающего списка 
            //TODO Вылечить это
            var item = typeDescription.TypeName == CSharpType.Boolean.toString() ?
                selectList.find(x => x.Value.toLowerCase() == rawValue.toLowerCase()) :
                selectList.find(x => x.Value == rawValue);

            if (item != null) {

                item.Selected = true;
            }
        }

        return selectList;
    }
}