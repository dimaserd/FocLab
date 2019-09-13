class FormTypeAfterDrawnDrawer {
    static SetInnerHtmlForProperty(propertyName: string, modelPrefix: string, innerHtml: string): void {
        const elem = FormDrawHelper.GetOuterFormElement(propertyName, modelPrefix);

        console.log("SetInnerHtmlForProperty elem", elem);

        elem.innerHTML = innerHtml;
    }

    /**
     * Устанавливает для имени свойства выпадающий список
     * @param propertyName
     * @param modelPrefix
     * @param selectList
     */
    static SetSelectListForProperty(propertyName: string, modelPrefix: string, selectList: Array<SelectListItem>): void {
        const t = TryForm._genericInterfaces.find(x => x.Prefix === modelPrefix);

        const prop = t.TypeDescription.Properties.find(x => x.PropertyName === propertyName);

        const drawer = new FormDrawImplementation(t);

        const html = drawer.RenderDropDownList(prop, selectList, false);

        FormTypeAfterDrawnDrawer.SetInnerHtmlForProperty(prop.PropertyName, modelPrefix, html);

        drawer.AfterFormDrawing();
    }
}
