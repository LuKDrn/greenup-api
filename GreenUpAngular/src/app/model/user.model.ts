import { fakeAsync } from "@angular/core/testing";
import { Address } from "./address.model";
import { Favorite } from "./favorite.model";
import { Mission } from "./mission.model";
import { Order } from "./order.model";
import { Participation } from "./participation.model";
import { Product } from "./product.model";

export class User {
    public id: number = 0;
    public mail: string = '';
    public password: string = '';
    public firstName: string = ''; 
    public lastName: string = ''; 
    public birthDate: string = ''; 
    public photo: string = '';
    public phoneNumber: string = '';
    public points: number = 0;
    public isActive: boolean = false;
    public isEmailConfirmed: boolean = false;
    public isPhoneNumberConfirmed: boolean = false;
    public isUser: boolean = false;
    public isAssociation: boolean = false;
    public isCompany: boolean = false;
    public isAdmin: boolean = false;
    public rnaNumber: string = '';
    public siretNumber: string = '';
    public websiteUrl: string = '';
    public addresses: Address[] = []; 
    public participations?: Participation[];
    public orders?: Order[];
    public favorites?: Favorite[];
    public missions?: Mission[]; 
    public products?: Product[];
}