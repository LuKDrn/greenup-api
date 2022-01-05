import { Address } from "./address.model";
import { Mission } from "./mission.model";

export class Association {
    public id: number = 0;
    public name: string = '';
    public rnaNumber: number = 0;
    public phoneNumber: string = '';
    public websiteUrl: string = '';
    public creationDate: string = '';
    public logo: string = '';
    public mail: string = '';
    public adresses: Address[] = [];
    public mission?: Mission[];
} 