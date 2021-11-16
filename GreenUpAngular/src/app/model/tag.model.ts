import { Mission } from "./mission.model";

export class Tag {
    public name : string;
    public missions? : Mission[];

    constructor(){
        this.name = '';
    }
}