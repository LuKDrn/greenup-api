import { Address } from "./address.model";
import { Basket } from "./basket.model";
import { Step } from "./step.model";

export class Order {
    public id: string = '';
    public amount: number = 0;
    public orderNumber: string = '';
    public date: string = '';
    public stepId: number = 0;
    public step: Step = new Step();
    public basketId: string = '';
    public basket: Basket = new Basket();
    public deliveryId: number = 0;
    public delivery: Address = new Address();
    public billingId: number = 0;
    public billing: Address = new Address();  
}