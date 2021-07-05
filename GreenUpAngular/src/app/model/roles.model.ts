class Roles {
    public id: number;
    public value: string;
    public users: User[];

    constructor() {
        this.id = 0;
        this.value = '';
        this.users = [];
    }
}