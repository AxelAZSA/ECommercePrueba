import { Component, OnInit } from '@angular/core';
import { ILogin } from '../Interfaces/ILogin';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ClienteService } from '../services/Cliente/cliente.service';
import { AdminService } from '../services/Admin/admin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login:ILogin={
    correo:"",
    password:""
  };
  sentLogin: FormGroup =this.form.group({
    correo: new FormControl(),
    password: new FormControl(),
    adminBool: new FormControl()
  });

  constructor(private serviceS:ClienteService,private serviceA:AdminService,private form:FormBuilder, private router:Router,private cookieService : CookieService) { }

  ngOnInit(): void {
  }


  onSubmit(data:any):void{
    this.login.correo = String(data.correo);
    this.login.password = String(data.password);
    if(data.adminBool)
    {
      this.serviceA.postLogin(this.login).subscribe(data=>{this.cookieService.set('tokenAd',data.token), this.router.navigate([''])});
    }else{
    this.serviceS.postLogin(this.login).subscribe(data=>{this.cookieService.set('token',data.token), this.router.navigate([''])});
    }
  }
}
