import { Association } from "./association.model";
import { Company } from "./company.model";
import { User } from "./user.model";

export class Address {
    public id: number;
    public place: string;
    public city: string;
    public zipCode: number;
    public companies: Company[];
    public users: User[];
    public associations: Association[];

    constructor() {
        this.id = 0;
        this.place = '';
        this.city = '';
        this.zipCode = 0;
        this.users = [];
        this.associations = [];
        this.companies = [];
    }
}