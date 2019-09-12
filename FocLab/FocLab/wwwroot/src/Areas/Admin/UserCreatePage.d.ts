interface ApplicationUser {
    Id: string;
    Email: string;
    Name: string;
    AvatarFileId: number;
}
declare class GetListResut<T> {
    TotalCount: number;
    List: Array<T>;
    Count: number;
    OffSet: number;
}
declare class UserCreatePage {
    static AfterDrawHandler(modelPrefix: string): void;
}
