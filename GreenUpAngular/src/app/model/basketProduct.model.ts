import { Basket } from "./basket.model";
import { Product } from "./product.model";

export class BasketProduct{
    public productAdded: string = '';
    public productId: number = 0;
    public product: Product = new Product();
    public basketId: string = '';
    public basket: Basket = new Basket();
}