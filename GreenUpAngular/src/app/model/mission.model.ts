import { Address } from "./address.model";
import { Participation } from "./participation.model";
import { Status } from "./status.model";
import { Tag } from "./tag.model";
import { User } from "./user.model";

export class Mission {
    public id: number = 0;
    public title: string = '';
    public description: string = '';
    public address: Address = new Address();
    public creation: string = '';
    public edit: string = '';
    public start: string = '';
    public end: string = '';
    public status: Status = new Status();
    public rewardValue: number = 0;
    public isInGroup: boolean = false;
    public numberPlaces: number = 0;
    public available: boolean = false;
    public associationId: string = '';
    public association: User = new User();
    public participation?: Participation[];
    public tags: Tag[] = [];
}