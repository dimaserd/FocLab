declare class NotificationWorker {
    static GetNotification(): void;
}
declare enum UserNotificationType {
    Success,
    Info,
    Warning,
    Danger,
    Custom
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
