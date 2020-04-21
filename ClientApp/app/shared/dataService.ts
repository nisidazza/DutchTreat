import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Product } from './product';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    public products: Product[] = [];

    //call the API 
    //Observable specify what return type loadProducts is
    loadProducts(): Observable<boolean> {
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