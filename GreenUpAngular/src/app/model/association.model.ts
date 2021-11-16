import { Address } from "./address.model";
import { Mission } from "./mission.model";

export class Association {
    public id: number;
    public name: string;
    public siren: number;
    public phoneNumber: string;
    public website: string;
    public mail: string;
    public address?: Address[];
    public mission?: Mission[];

    constructor() {
        this.id = 0;
        this.name = '';
        this.siren = 0;
        this.phoneNumber = '';
        this.website = '';
        this.mail = '';
    }
} 