import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { Cart } from './shop/cart.component';
import { DataService } from './shared/dataService';

import { RouterModule } from '@angular/router';

//collection of routes
let routes = [
    //root of client-side project
    { path: "", component: Shop },
    //client checkout page
    { path: "/checkout", component: Checkout }
];


@NgModule({
    declarations: [
        AppComponent,
        ProductList,
        Cart
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        //inject routes into the module in order to use routing
        RouterModule.forRoot(routes, {
            //options for configuring routing
            useHash: true,
            enableTracing: false, // for Debugging of the Routes
        })
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
