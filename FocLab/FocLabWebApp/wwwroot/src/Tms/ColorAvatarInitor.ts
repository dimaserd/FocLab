class ColorAvatarInitor {

    private static _avatarsStorage: { 'id': string, 'color': string }[] = [];

    private static _colors: Array<string> = [
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

    public static InitColorForAvatar = function (task: DayTaskModel): string {
        /*Проверяю есть ли аватар у клиента*/
        if (task.AssigneeUser.AvatarFileId === null) {
            let idFound: boolean = false;
            let color: string = "";

            /*Если нет - проверяю, объявлен ли для него цвет*/
            ColorAvatarInitor._avatarsStorage.forEach(x => {
                if (x.id == task.AssigneeUser.Id) {

                    idFound = true;
                    color = x.color;
                }
            })

            if (idFound) {
                return `<span class='avatar-circle' style='background-color:${color}'></span>`;
            } else {
                /*Добавляю следующий цвет пользователю*/
                const count = ColorAvatarInitor._avatarsStorage.length % ColorAvatarInitor._colors.length;
                ColorAvatarInitor._avatarsStorage.push({ 'id': task.AssigneeUser.Id, 'color': ColorAvatarInitor._colors[count + 1] });

                return `<span class='avatar-circle' style='background-color:${ColorAvatarInitor._colors[count + 1]}'></span>`;
            }
        } else {
            return `<img style='height:30px;width:30px' class='rounded-circle' src='/FileCopies/Images/Icon/${task.AssigneeUser.AvatarFileId}.jpg'/>`;
        }
    }
}