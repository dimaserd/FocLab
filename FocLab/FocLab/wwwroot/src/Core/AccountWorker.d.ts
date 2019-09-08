interface UserWithId {
    Id: string;
}
declare class AccountWorker {
    static User: UserWithId;
    static CheckUser: () => void;
    static IsAuthenticated: () => boolean;
}
