import { Address } from "./address.model";

export class Company {
    public id: number;
    public name: string;
    public siren: number;
    public address: Address[];

    constructor() {
        this.id = 0;
        this.name = '';
        this.siren = 0;
        this.address = [];
    }
}