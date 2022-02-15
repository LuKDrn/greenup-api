import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, Subscriber } from 'rxjs';
import { UserService } from '../../user/user.service';
import jwt_decode from 'jwt-decode';
import { MissionsService } from '../missions.service';
import { Address } from 'src/app/model/address.model';

@Component({
  selector: 'app-add-mission',
  templateUrl: './add-mission.component.html',
  styleUrls: ['./add-mission.component.scss']
})
export class AddMissionComponent implements OnInit {
  @ViewChild("fileUpload", {static: false}) fileUpload!: ElementRef;files  = [];  
  public form!: FormGroup;
  public loading: boolean;
  // public address: Address;

  constructor(
    public dialogRef: MatDialogRef<AddMissionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private userService: UserService,
    private missionService: MissionsService,
  ) {
    this.loading = false;
    // this.address = new Address();
    this.initForm();
  }

  ngOnInit(): void {}

  public insertImg(): void {
    console.log('insert img');
    const fileUpload = this.fileUpload.nativeElement;fileUpload.onchange = () => {
      const file = fileUpload.files[0];  
      console.log('file', file);
      this.convertToBase64(file);

    };
    fileUpload.click();  
  }

  // cacher le statut en création
  // Récupérer l'id du connecté (association)
  // Ensemble de tâche (plus tard)
  // Ensemble de tag (plus tard)

  // public selectionTypeChange($event: any): void {
  //   this.form.get('typeMission')?.setValue($event.value);
  // }

  public checkDate(): void {
    if (this.form.get('dateDebutMission')?.value && this.form.get('dateFinMission')?.value) {
      if (this.form.get('dateDebutMission')?.value > this.form.get('dateFinMission')?.value) {
        this.form.get('dateDebutMission')?.setValue('');
      }
    }
  }

  public save(): void {
    this.getAssociationId();
    console.log('form', this.form);
    this.submit(this.form);
  }

  public initForm(): void {
    this.form = this.fb.group({
      titre: new FormControl('',[Validators.required]),
      imgMission: new FormControl(''),
      description: new FormControl('', [Validators.required]),
      idAddress: new FormControl(''),
      adress: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      zipcode: new FormControl(null, [Validators.required]),
      statut: new FormControl(''),
      // typeMission: new FormControl(''),
      nombrePlace: new FormControl(0, [Validators.required]),
      isGroup: new FormControl(false),
      nombreJoursMission: new FormControl(0),
      dateDebutMission: new FormControl('', [Validators.required]),
      pointMission: new FormControl(0, [Validators.required]),
      nombreHeureParJoursMissions: new FormControl(0),
      dateFinMission: new FormControl('', [Validators.required]),
      associationId: new FormControl('', [Validators.required])
    });
  }

  private submit({value, valid}: {value: Form, valid: boolean}): void {
    if (valid) {
      console.log('it ok');
      this.loading = true;
      // this.address.place = this.form.get('adresse')?.value;
      // this.address.city = this.form.get('ville')?.value;
      // this.address.zipCode = this.form.get('zipcode')?.value;
      this.missionService.addMission(value).subscribe(
        (res) => {
          this.loading = false;
          console.log('res', res);
          this.dialogRef.close();
        },
        (error) => {
          this.loading = false;
          console.log(error)
        }
      )
    } else {
      console.log('it not ok');
    }
  }

  private convertToBase64(file: File): void {
    const observable = new Observable((subscriber: Subscriber<any>) => {
      this.readFile(file, subscriber);
    });
    observable.subscribe((d) => {
      console.log(d);
      this.form.get('imgMission')?.setValue(d);
    });
  }

  private readFile(file: File, subscriber: Subscriber<any>) {
    const fileReader = new FileReader();
    fileReader.readAsDataURL(file);

    fileReader.onload = () => {
      subscriber.next(fileReader.result);
      subscriber.complete();
    };
    fileReader.onerror = (error) => {
      subscriber.error(error);
      subscriber.complete();
    };
  }

  private getDecodedAccessToken(token: string): any {
    try{
        console.log('jwt', jwt_decode(token));
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }

  private getAssociationId() : any {
    this.loading = true;
    const $jwt: any = localStorage.getItem('jwt');
    const $userToken = this.getDecodedAccessToken($jwt);
    console.log($userToken);
    if ($userToken.type === 'Association') {
      console.log('id', $userToken.associationId);
      // this.address.userId = $userToken.associationId;
      this.form.get('associationId')?.setValue($userToken.associationId);
    } else {
      console.log('tu es pas assoc');
    }
  }
}
