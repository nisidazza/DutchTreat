import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ProductList } from './shop/productList.component';
import { Cart } from './shop/cart.component';
import { Shop } from './shop/shop.component';
import { Checkout } from './checkout/checkout.component';
import { Login } from './login/login.component';

import { DataService } from './shared/dataService';

import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

//collection of routes
let routes = [
    //root of client-side project
    { path: "", component: Shop },
    //client checkout page
    { path: "checkout", component: Checkout },

    {path: "login", component: Login}
];


@NgModule({
    declarations: [
        AppComponent,
        ProductList,
        Cart,
        Shop,
        Checkout,
        Login
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        //inject routes into the module in order to use routing
        RouterModule.forRoot(routes, {
            //options for configuring routing
            useHash: true,
            enableTracing: false, // for Debugging of the Routes
        }),
        FormsModule
    ],
    providers: [
        DataService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
