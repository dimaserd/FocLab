var ColorAvatarInitor = /** @class */ (function () {
    function ColorAvatarInitor() {
    }
    ColorAvatarInitor._avatarsStorage = [];
    ColorAvatarInitor._colors = [
        "#007bff",
        "#6610f2",
        "#6f42c1",
        "#e83e8c",
        "#dc3545",
        "#fd7e14",
        "#ffc107",
        "#28a745",
        "#20c997",
        "#17a2b8",
        "#fff",
        "#6c757d"
    ];
    ColorAvatarInitor.InitColorForAvatar = function (task) {
        /*Проверяю есть ли аватар у клиента*/
        if (task.AssigneeUser.AvatarFileId === null) {
            var idFound_1 = false;
            var color_1 = "";
            /*Если нет - проверяю, объявлен ли для него цвет*/
            ColorAvatarInitor._avatarsStorage.forEach(function (x) {
                if (x.id == task.AssigneeUser.Id) {
                    idFound_1 = true;
                    color_1 = x.color;
                }
            });
            if (idFound_1) {
                return "<span class='avatar-circle' style='background-color:" + color_1 + "'></span>";
            }
            else {
                /*Добавляю следующий цвет пользователю*/
                var count = ColorAvatarInitor._avatarsStorage.length % ColorAvatarInitor._colors.length;
                ColorAvatarInitor._avatarsStorage.push({ 'id': task.AssigneeUser.Id, 'color': ColorAvatarInitor._colors[count + 1] });
                return "<span class='avatar-circle' style='background-color:" + ColorAvatarInitor._colors[count + 1] + "'></span>";
            }
        }
        else {
            return "<img style='height:30px;width:30px' class='rounded-circle' src='/FileCopies/Images/Icon/" + task.AssigneeUser.AvatarFileId + ".jpg'/>";
        }
    };
    return ColorAvatarInitor;
}());
