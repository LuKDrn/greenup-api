import { BasketProduct } from "./basketProduct.model";
import { User } from "./user.model";

export class Basket {
    public amount: number = 0;
    public userId: string = '';
    public user: User = new User();
    public product?: BasketProduct[];
}