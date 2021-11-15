class Mission {
    public id: number;
    public associationId: string;
    public titre: string;
    public description: string;
    public adress: string;
    public city: string;
    public zipCode: number;
    public date: string;
    public rewardValue: number;
    public isInGroup: boolean;
    public numberPlaces: number;
    public available: boolean;
    public user?: User[];

    constructor() {
        this.id = 0;
        this.date = '';
        this.associationId = '';
        this.titre = '';
        this.description = '';
        this.adress = '';
        this.city = '';
        this.zipCode = 0;
        this.rewardValue = 0;
        this.isInGroup = false;
        this.numberPlaces = 0;
        this.rewardValue = 0;
        this.isInGroup = false;
        this.available = false;
    }
}