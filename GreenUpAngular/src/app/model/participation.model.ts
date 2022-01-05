export class Participation {
    public userId: number;
    public missionId: number;
    public validation: boolean;
    public dateInscription: string;
    public doc: MissionDoc[];

    constructor() {
        this.userId = 0;
        this.missionId = 0;
        this.validation = false;
        this.dateInscription = Date.now.toString();
        this.doc = [];
    }
}