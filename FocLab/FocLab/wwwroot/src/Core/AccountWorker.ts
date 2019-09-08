interface UserWithId {
    Id: string;
}

class AccountWorker {
    
    static User: UserWithId = null;
    
    static CheckUser = function () : void {

        //TODO Implement User Checking
        if (!this.IsAuthenticated() || true) {
            return;
        }

    }

    static IsAuthenticated = function () : boolean {

        var value = this.User != null;

        console.log(`AccountWorker.IsAuthenticated()=${value}`);

        return value;
    }
}

document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser() }, 1100);
});
