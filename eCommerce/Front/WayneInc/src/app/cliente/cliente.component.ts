import { Component } from '@angular/core';
import { ClienteService } from '../Service/Cliente/cliente.service';
import { ICliente } from '../Interfaces/ICliente';
import { SesionService } from '../Service/Sesion/sesion.service';
import { ISesion } from '../Interfaces/ISesion';
import { ArticuloClienteService } from '../Service/ArticuloCliente/articulo-cliente.service';
import { IArticuloCliente } from '../Interfaces/IArticuloCliente';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {

  compras!:IArticuloCliente[];
  sesion!:ISesion;

  item:ICliente={
    id: 0,
    nombre:"",
    apellido:"",
    direccion:""
   };

   constructor(private clienteService:ClienteService, private sesionService:SesionService,private cookieService: CookieService, private articuloCService: ArticuloClienteService,private router:Router) { }

 ngOnInit(): void {
    this.clienteService.getCliente().subscribe(data=>{
      this.item = data
      this.sesionService.getByCliente(data.id).subscribe(data=>
        this.sesion=data)
    });

    this.articuloCService.getAll().subscribe(data=>
      this.compras=data);
   }

   OnLogout():void{
    console.log("adentro");
    this.sesionService.logut()
    .subscribe(data=>
      {
    this.cookieService.deleteAll();
     this.router.navigate(['']);
      });
  }

  logout(){
    console.log("saliendo");
    this.sesionService.logut()
    .subscribe(data=>
      {
    this.cookieService.deleteAll();
     this.router.navigate(['/']);
      });
  }
}
