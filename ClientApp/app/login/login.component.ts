import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from '@angular/router';


@Component({
    selector: "login",
    templateUrl: "login.component.html"
})

export class Login {

    constructor(private data: DataService, private router: Router) { }

    errorMessage: string = "";
    //object that represents the data on the form
    public creds = {
        //untyped properties
        username: "",
        password: ""
    }

    onLogin() {
        //Call the login Service
        this.data.login(this.creds)
            .subscribe(success => {
                if (success) {
                    if (this.data.order.items.length == 0) {
                        this.router.navigate([""]);
                    } else {
                        this.router.navigate(["checkout"]);
                    }
                }
            },
                err => this.errorMessage = "Failed to login")
    }

}