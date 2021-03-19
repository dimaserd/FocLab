var NotificationWorker = (function () {
    function NotificationWorker() {
    }
    NotificationWorker.GetNotification = function () {
        CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/GetLast", {}, function (x) {
            console.log("GetNotification", x);
            if (x === null || x === undefined) {
                return;
            }
            CrocoAppCore.ToastrWorker.ShowSuccess(x.Text);
            setTimeout(function () {
                console.log("Marking notification");
                CrocoAppCore.Application.Requester.Post("/Api/Notification/Client/Read", { Id: x.Id }, function () { }, null);
            }, 1000);
        }, null);
    };
    return NotificationWorker;
}());
var UserNotificationType;
(function (UserNotificationType) {
    UserNotificationType[UserNotificationType["Success"] = 'Success'] = "Success";
    UserNotificationType[UserNotificationType["Info"] = 'Info'] = "Info";
    UserNotificationType[UserNotificationType["Warning"] = 'Warning'] = "Warning";
    UserNotificationType[UserNotificationType["Danger"] = 'Danger'] = "Danger";
    UserNotificationType[UserNotificationType["Custom"] = 'Custom'] = "Custom";
})(UserNotificationType || (UserNotificationType = {}));
