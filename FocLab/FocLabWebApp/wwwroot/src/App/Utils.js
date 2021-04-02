var Utils = (function () {
    function Utils() {
    }
    Utils.FillSelect = function (select, array, htmlFunc, valueFunc) {
        for (var i = 0; i < array.length; i++) {
            var opt = document.createElement("option");
            opt.innerHTML = htmlFunc(array[i]);
            opt.value = valueFunc(array[i]);
            select.append(opt);
        }
    };
    Utils.GetImageLinkByFileId = function (fileId, sizeType) {
        return "/FileCopies/Images/" + sizeType.toString() + "/" + fileId + ".jpg";
    };
    return Utils;
}());
