class FormTypeAfterDrawnDrawer {
    static SetInnerHtmlForProperty(propertyName: string, modelPrefix: string, innerHtml: string): void {
        var elem = FormDrawHelper.GetOuterFormElement(propertyName, modelPrefix);

        console.log("SetInnerHtmlForProperty elem", elem);

        elem.innerHTML = innerHtml;
    }
}
