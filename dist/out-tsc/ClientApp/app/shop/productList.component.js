import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    /** inject DataService **/
    constructor(data) {
        this.data = data;
        //getting data from the service
        this.products = [];
        this.products = data.products;
    }
};
ProductList = __decorate([
    Component({
        selector: "product-list",
        templateUrl: "productList.component.html",
        styleUrls: []
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=productList.component.js.map