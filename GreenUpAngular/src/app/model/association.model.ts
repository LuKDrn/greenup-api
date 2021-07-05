class Association {
    public id: number;
    public name: string;
    public siren: number;
    public address?: Address[];
    public mission?: Mission[];

    constructor() {
        this.id = 0;
        this.name = '';
        this.siren = 0;
    }
} 