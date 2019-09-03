class HtmlDrawHelper {

    static RenderAttributesString(attrs: Dictionary<string>): string {

        let result = "";

        if (attrs == null) {
            return result;
        }

        for (let i = 0; i < attrs._keys.length; i++) {

            let key = attrs._keys[i];

            let res = attrs.getByKey(key);

            if (res == null || res === "") {
                result += ` ${key}`;
            }
            else {
                result += ` ${key}="${res}"`;
            }
        }

        return result;
    }

    static RenderSelect(className: string, propName: string, selectList: SelectListItem[], attrs: Dictionary<string>): string {

        let attrStr = HtmlDrawHelper.RenderAttributesString(attrs);

        console.log("RenderSelect", attrStr);

        var select = `<select${attrStr} class="${className}" name="${propName}">`;

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