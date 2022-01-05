import { Association } from "./association.model";
import { Order } from "./order.model";
import { User } from "./user.model";

export class Address {
    public id: number = 0;
    public place: string = '';
    public city: string = '';
    public zipCode: number = 0;
    public userId: string = '';
    public user: User = new User();
    public orders?: Order[];
}