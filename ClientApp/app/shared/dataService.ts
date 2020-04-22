﻿import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Product } from './product';
import { Order, OrderItem } from './order';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    //Support Login
    private token: string = "";
    private tokenExpiration: Date;

    public order: Order = new Order();

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

    //read-only property
    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    public addToOrder(newProduct: Product) {

        let item: OrderItem = this.order.items.find(i => i.productId == newProduct.id);

        if (item) {

            item.quantity++

        } else {

            item = new OrderItem();
            item.productId = newProduct.id;
            item.productArtist = newProduct.artist;
            item.productArtId = newProduct.artId;
            item.productCategory = newProduct.category;
            item.productSize = newProduct.size;
            item.productTitle = newProduct.title;
            item.unitPrice = newProduct.price;
            item.quantity = 1;

            this.order.items.push(item);
        }
    }
}