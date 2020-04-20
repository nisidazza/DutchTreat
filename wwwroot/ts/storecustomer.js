/*export*/ class StoreCustomer {
    //constructor
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        //field
        this.visits = 0;
    }
    //function
    showName() {
        alert(this.firstName + " " + this.lastName);
    }
    //accessors - similar to properties
    set name(val) {
        this.ourName = val;
    }
    get name() {
        return this.ourName;
    }
}
//# sourceMappingURL=storecustomer.js.map