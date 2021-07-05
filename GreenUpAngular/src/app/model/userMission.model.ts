class UserMission {
    public userId: number;
    public missionId: number;
    public validation: boolean;
    public doc: MissionDoc[];

    constructor() {
        this.userId = 0;
        this.missionId = 0;
        this.validation = false;
        this.doc = [];
    }
}