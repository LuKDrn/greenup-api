import { Address } from "./address.model";
import { Mission } from "./mission.model";
import { Rewards } from "./rewards.model";
import { Roles } from "./roles.model";

export class User {
    public id: number;
    public mail: string;
    public password: string;
    public firstName: string; 
    public lastName: string; 
    public birthDate: string; 
    public address: Address; 
    public point: number;
    public roles?: Roles[];
    public rewards: Rewards[];
    public missions?: Mission[]; 

    constructor() {
        this.id = 0;
        this.mail = '';
        this.password = '';
        this.firstName = ''; 
        this.lastName = ''; 
        this.birthDate = ''; 
        this.address = new Address(); 
        this.point = 0;
        this.rewards = [];
    }

}