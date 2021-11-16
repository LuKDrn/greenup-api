import { Address } from "./address.model";
import { Association } from "./association.model";
import { User } from "./user.model";

export class Mission {
    public id: number;
    public associationId: string;
    public titre: string;
    public description: string;
    public adress: Address;
    public date: string;
    public rewardValue: number;
    public isInGroup: boolean;
    public numberPlaces: number;
    public available: boolean;
    public user?: User[];
    public association: Association;

    constructor() {
        this.id = 0;
        this.date = '';
        this.associationId = '';
        this.titre = '';
        this.description = '';
        this.rewardValue = 0;
        this.isInGroup = false;
        this.numberPlaces = 0;
        this.rewardValue = 0;
        this.isInGroup = false;
        this.available = false;
        this.association = new Association();
        this.adress = new Address();
    }
}