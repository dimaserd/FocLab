interface ApplicationUserBaseModel {
	/**
	* Email подтвержден
 	*/
    EmailConfirmed: boolean;
	/**
	* Номер телефона
 	*/
    PhoneNumber: string;
	/**
	* Номер телефона подтвержден
 	*/
    PhoneNumberConfirmed: boolean;
	/**
	* Баланс
 	*/
    Balance: number;
	/**
	* Пол
 	*/
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