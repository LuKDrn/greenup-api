class Mission {
    public id: number;
    public place: string; 
    public date: string; 
    public reward: number;
    public isGroup: boolean;
    public available: boolean;
    public user?: User[];

    constructor() {
        this.id = 0;
        this.place = '';
        this.date = '';
        this.reward = 0;
        this.isGroup = false;
        this.available = false;
    }
}