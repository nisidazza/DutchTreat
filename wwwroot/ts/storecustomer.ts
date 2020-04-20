class StoreCustomer {

    //constructor
    constructor(private firstName:string, private lastName:string) {
       
    }

    //field
    public visits: number = 0; 
    private ourName: string;

    //function
    public showName() {
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

