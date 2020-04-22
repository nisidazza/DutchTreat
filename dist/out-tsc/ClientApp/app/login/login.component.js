import { __decorate } from "tslib";
import { Component } from "@angular/core";
let Login = class Login {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        //object that represents the data on the form
        this.creds = {
            //untyped properties
            username: "",
            password: ""
        };
    }
    onLogin() {
        //Call the login Service
        alert(this.creds.username);
        this.creds.username += "!";
    }
};
Login = __decorate([
    Component({
        selector: "login",
        templateUrl: "login.component.html"
    })
], Login);
export { Login };
//# sourceMappingURL=login.component.js.map