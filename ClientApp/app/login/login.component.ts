import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from '@angular/router';


@Component({
    selector: "login",
    templateUrl: "login.component.html"
})

export class Login {

    constructor(private data: DataService, private router: Router) { }

    //object that represents the data on the form
    public creds = {
        //untyped properties
        username: "",
        paswword: ""
    }

    onLogin() {
      //Call the login Service
    }

}