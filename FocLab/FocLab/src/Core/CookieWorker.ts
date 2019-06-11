class CookieWorker {
    
    static setCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = `; expires=${date.toUTCString()}`;
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    static getCookie = function (name) {
            const nameEq = name + "=";
            const ca = document.cookie.split(";");
            for (let i = 0; i < ca.length; i++) {
                let c = ca[i];
                while (c.charAt(0) === " ") c = c.substring(1, c.length);
                if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
            }
            return null;
    }

    static eraseCookie = function (name) {
        document.cookie = name + "=; Max-Age=-99999999;";
    }
    
}