class Address {
    public uid: number;
    public address: string;
    public city: string;
    public zipCode: number;
    public company: Company[];

    constructor() {
        this.uid = 0;
        this.address = '';
        this.city = '';
        this.zipCode = 0;
        this.company = [];
    }
}