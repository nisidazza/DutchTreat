import { Component, OnInit } from "@angular/core";
import { DataService } from '../shared/dataService';

@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: []
})
export class ProductList implements OnInit {
/** inject DataService **/
    // // Called first time before the ngOnInit()
    constructor(private data: DataService) {
    }

    //getting data from the service
    public products = [];

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
}