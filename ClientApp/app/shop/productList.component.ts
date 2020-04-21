import { Component, OnInit } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Product } from '../shared/product';

@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: ["productList.component.css"]
})
export class ProductList implements OnInit {
/** inject DataService **/
    // // Called first time before the ngOnInit()
    constructor(private data: DataService) {
    }

    //add type safety to the products list
    public products : Product[] = [];

     // Called after the constructor and called  after the first ngOnChanges() 
    ngOnInit(): void {
        this.data.loadProducts()
            //get the data passed back in from the API
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            });
    }

    addProduct(product: Product) {
        //calling addToOrder from dataService
        this.data.addToOrder(product);
    }
}