import { Product } from "./product.model";
import { User } from "./user.model";

export class Favorite {
    public userId: number = 0;
    public user: User = new User();
    public productId: number = 0;
    public product: Product = new Product()
}