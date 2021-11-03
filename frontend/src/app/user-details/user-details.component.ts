import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { Location } from '@angular/common'
@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {
  public name: FormControl = new FormControl;
  public email: FormControl = new FormControl;
  public animalControl: FormControl = new FormControl;
  public animals: any[] = [
    { name: 'Dog', sound: 'Woof!' },
    { name: 'Cat', sound: 'Meow!' },
    { name: 'Cow', sound: 'Moo!' },
    { name: 'Fox', sound: 'Wa-pa-pa-pa-pa-pa-pow!' },
  ];
  public userId: number | undefined;
  public user: any = {};
  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.name.setValidators([Validators.required]);
    this.email.setValidators([Validators.required]);
    this.animalControl.setValidators([Validators.required]);
    this.activatedRoute.params.subscribe((params) => {
      this.userId = params.userId;
      this.getUser();
    })
  }

  public back() {
    this.location.back();
  }


  public updateOrCreate() {
    this.user.name = this.name.value;
    this.user.email = this.email.value;
    if (this.user.id) {
      this.userService.updateUser(this.userId, this.user).subscribe((res) => {
        this.back();
      })
    } else {
      this.userService.createUser(this.user).subscribe((res) => {
        this.back();
      })
    }

  }

  public getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }
    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  private getUser() {
    this.userService.getUser(this.userId).subscribe((user: any) => {
      this.user = user;
      this.name.setValue(user.name);
      this.email.setValue(user.email);
    })
  }

}
