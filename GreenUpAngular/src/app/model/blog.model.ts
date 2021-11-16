export class Blog {
    public uid: number;
    public categorie: string;
    public titre: string;
    public resume: string;
    public corps: string;
    public img: string;
    public date: string;

    constructor() {
        this.uid = 0;
        this.categorie = '';
        this.titre = '';
        this.resume = '';
        this.corps = '';
        this.img = '';
        this.date = '';
    }
}