import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  title: string;
  form: FormGroup;

  constructor(private auth:AuthService, private router: Router, private fb: FormBuilder, @Inject('BASE_URL') private baseUrl: string) {
    this.title = "User Login";

    //initialize the form
    this.createForm();

  }

  ngOnInit() {
  }

  createForm() {
    this.form = this.fb.group({
      Username: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }

  onSubmit() {
    var url = this.baseUrl + "api/token/auth";
    var username = this.form.value.Username;
    var password = this.form.value.Password;

    this.auth.login(username, password)
      .subscribe(result => {
        alert("Login successful!");
        this.router.navigate(["home"]);
      }, error => {
        console.log(error);
        this.form.setErrors({ "auth": "Incorrect username or password" });
      });
  }

  onBack() {
    this.router.navigate(["home"]);
  }

  getFormControl(name: string) {
    return this.form.get(name);
  }

  // returns TRUE if the FormControl is valid
  isValid(name: string) {
    var e = this.getFormControl(name);
    return e && e.valid;
  }

  // returns TRUE if the FormControl has been changed
  isChanged(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched);
  }

  hasError(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched) && !e.valid;
  }

}
