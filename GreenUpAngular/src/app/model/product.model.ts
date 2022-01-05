import { BasketProduct } from "./basketProduct.model";
import { Favorite } from "./favorite.model";
import { Order } from "./order.model";
import { User } from "./user.model";

export class Product {
    public id: number = 0;
    public name: string = '';
    public description?: string = '';
    public quantity: number = 0;
    public price: number = 0;
    public isAvailable: boolean = false;
    public companyId: string = '';
    public company : User = new User();
    public favorites?: Favorite[];  
    public basketProduct?: BasketProduct[];
}