import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    public products = [];

    //call the API
    loadProducts() {
        return this.http.get("/api/products")
            //inside the pipe there will be a list of interceptors
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                })
            )

    }
}