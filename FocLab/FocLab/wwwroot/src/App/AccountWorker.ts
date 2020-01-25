class AccountWorker {

    static User: IUserModel = null;
    
    static CheckUser() : void {

        //TODO Implement User Checking
        if (!this.IsAuthenticated() || true) {
            return;
        }

    }

    static IsAuthenticated() : boolean {

        var value = this.User != null;

        console.log(`AccountWorker.IsAuthenticated()=${value}`);

        return value;
    }
}

document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () { AccountWorker.CheckUser() }, 1100);
});
