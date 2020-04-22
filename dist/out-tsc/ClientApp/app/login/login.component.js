import { __decorate } from "tslib";
import { Component } from "@angular/core";
let Login = class Login {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.errorMessage = "";
        //object that represents the data on the form
        this.creds = {
            //untyped properties
            username: "",
            password: ""
        };
    }
    onLogin() {
        //Call the login Service
        this.data.login(this.creds)
            .subscribe(success => {
            if (success) {
                if (this.data.order.items.length == 0) {
                    this.router.navigate([""]);
                }
                else {
                    this.router.navigate(["checkout"]);
                }
            }
        }, err => this.errorMessage = "Failed to login");
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