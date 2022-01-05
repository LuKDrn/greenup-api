import { Mission } from "./mission.model";
import { User } from "./user.model";

export class Participation {
    public userId: string = '';
    public user: User = new User()
    public missionId: number = 0;
    public mission: Mission = new Mission();
    public dateInscription: string = '';
}