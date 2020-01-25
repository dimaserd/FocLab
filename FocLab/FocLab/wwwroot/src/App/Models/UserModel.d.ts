interface ApplicationUserBaseModel {
    EmailConfirmed: boolean;
    PhoneNumber: string;
    PhoneNumberConfirmed: boolean;
    Balance: number;
    Sex: boolean;
    ObjectJson: string;
    SecurityStamp: string;
    BirthDate: Date;
    DeActivated: boolean;
    RoleNames: Array<string>;
    Surname: string;
    Patronymic: string;
    PasswordHash: string;
    CreatedOn: Date;
    Email: string;
    Name: string;
    AvatarFileId: number;
    Id: string;
}
