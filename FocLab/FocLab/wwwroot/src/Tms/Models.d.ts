interface UserFullNameEmailAndAvatarModel {
    Surname: string;
    Patronymic: string;
    Email: string;
    Name: string;
    AvatarFileId: number;
    Id: string;
}
interface DayTaskModel {
    Id: string;
    TaskDate: Date;
    TaskText: string;
    TaskTitle: string;
    FinishDate: Date;
    TaskTarget: string;
    TaskReview: string;
    TaskComment: string;
    Comments: Array<DayTaskCommentModel>;
    Author: UserFullNameEmailAndAvatarModel;
    AssigneeUser: UserFullNameEmailAndAvatarModel;
}
interface DayTaskCommentModel {
    Id: string;
    Comment: string;
    Author: UserFullNameEmailAndAvatarModel;
}
