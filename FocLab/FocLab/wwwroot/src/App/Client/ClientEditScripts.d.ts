interface EditClient {
    Name: string;
    BirthDate: Date;
    Surname: string;
    Patronymic: string;
    Sex?: boolean;
    PhoneNumber: string;
}
declare class ClientEditor {
    static getUserData(): EditClient;
    static Edit(): void;
    static BtnUpdateAvatar(): void;
    static SetHandlers(): void;
}
