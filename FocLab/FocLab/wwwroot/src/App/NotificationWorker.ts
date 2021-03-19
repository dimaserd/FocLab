class NotificationWorker {

    static GetNotification() {
        CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/GetLast", {}, (x: NotificationModel) => {

            console.log("GetNotification", x);

            if (x === null || x === undefined) {
                return;
            }

            CrocoAppCore.ToastrWorker.ShowSuccess(x.Text);

            setTimeout(function () {
                console.log("Marking notification");
                CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/Read", { Id: x.Id }, () => { }, null);
            }, 1000);
        },
            null);
    }
}

enum UserNotificationType {
    Success = <any>'Success',
    Info = <any>'Info',
    Warning = <any>'Warning',
    Danger = <any>'Danger',
    Custom = <any>'Custom'
}

interface NotificationModel {
    Title: string;
    Text: string;
    ObjectJson: string;
    Type: UserNotificationType;
    CreatedOn: Date;
    ReadOn: Date;
    UserId: string;
    Id: string;
}