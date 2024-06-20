import { Component } from '@angular/core';
import { ICliente } from '../Interfaces/ICliente';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../Service/Cliente/cliente.service';
import { ISesion } from '../Interfaces/ISesion';
import { SesionService } from '../Service/Sesion/sesion.service';

@Component({
  selector: 'app-cliente-edit',
  templateUrl: './cliente-edit.component.html',
  styleUrls: ['./cliente-edit.component.css']
})
export class ClienteEditComponent {

  idCliente!:number;

  item:ICliente={
   id: 0,
   nombre:"",
   apellido:"",
   direccion:""
  };

  sesion:ISesion={
    id: 0,
    correo:"",
    password:"",
    idCliente:this.idCliente
   };

 sentCliente:FormGroup=this.form.group({
   nombre: new FormControl(this.item.nombre),
   apellido: new FormControl(this.item.apellido),
   calle: new FormControl(),
   numeroCasa:new FormControl(),
   codigoPostal: new FormControl(),
   ciudad: new FormControl(),
   correo: new FormControl(),
   password: new FormControl(),
   confirmPassword: new FormControl()
 });
 constructor(private clienteService:ClienteService,private sesionService:SesionService,private form:FormBuilder, private router:Router, private actRoute:ActivatedRoute) { }

 ngOnInit(): void {
   this.actRoute.paramMap.subscribe(params => {
     this.idCliente= Number(params.get('id'));

    if(this.idCliente!=0)
    this.clienteService.getClienteById(this.idCliente).subscribe(data=>{
      this.item = data
    this.sesionService.getByCliente(Number(data.id)).subscribe(data=>{
      this.sesion=data
    })
    });
   });
  }

 onSubmit(data:any):void{
  if(String(data.password)==String(data.confirmPassword)){
  this.item.id=Number(this.idCliente)
   this.item.nombre=String(data.nombre );
   this.item.apellido=String(data.apellido);
   this.item.direccion= String(data.calle+data.numeroCasa+";"+data.codigoPostal+";"+data.ciudad);
   this.sesion.correo=String(data.correo);
   this.sesion.password=String(data.password);
if(this.idCliente!=0){
   this.clienteService.putCliente(this.item).subscribe(data=>{
    this.sesionService.putSesion(this.sesion).subscribe(data=>{
    alert(data)
     this.router.navigate(['/cliente'])})
    });
   }else
   {
    this.clienteService.postCliente(this.item).subscribe(data=>{
      this.sesion.idCliente=data
      this.sesionService.postSesion(this.sesion).subscribe(data=>
        this.router.navigate(['/cliente']))});
   }
 }else{
  alert("Las contraseñas no coinciden")
 }
}
}
