import { Component } from '@angular/core';
import { ITienda } from '../Interfaces/ITienda';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IArticulo } from '../Interfaces/IArticulo';
import { Observable, Subscriber } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { IStock } from '../Interfaces/IStock';
import { AdminService } from '../services/Admin/admin.service';
import { ArticuloService } from '../services/Articulo/articulo.service';
import { TiendaService } from '../services/Tienda/tienda.service';

@Component({
  selector: 'app-inventario',
  templateUrl: './inventario.component.html',
  styleUrls: ['./inventario.component.css']
})

export class InventarioComponent {
  articuloDisplay:boolean=false;
  inventarioDisplay:boolean=false;
  tiendas!:ITienda[];
  stocks!:IStock[];
  articulos!:IArticulo[];
  articulo:IArticulo={
    id:0,
    codigo:"",
    descripcion:"",
    precio:0
  };
imgBase64!:String;
sentStock:FormGroup=this.form.group({
  stock: new FormControl(),
  idArticulo: new FormControl(),
  idTienda: new FormControl()
});

sentArticulo:FormGroup=this.form.group({
  codigo: new FormControl(),
  descripcion: new FormControl(),
  precio: new FormControl()
});

  constructor(private stockService,private adminService:AdminService,private cookieService:CookieService,private articuloService:ArticuloService,private tiendaService:TiendaService, private form:FormBuilder,private router:Router){}

  ngOnInit(): void {

    this.articuloService.getAll().subscribe(data=>{
      this.articulos = data
      })
    this.tiendaService.getAll().subscribe(data=>{
      this.tiendas=data
    });


  }


onSubmitInv(data:any){
  alert("dentro");
  this.articulo.codigo=String(data.codigo);
  this.articulo.descripcion=String(data.descripcion);
  this.articulo.precio=Number(data.precio);
  this.articuloService.post(this.articulo).subscribe(data=>{
    this.router.navigate([''])
  });
  }

  onSubmitStk(data:any){
    var idArticulo = Number(data.idArticulo)
    var stock = Number(data.stock)
    var idTienda = Number(data.idTienda)
    this.articuloTService.postStock(idArticulo,idTienda,stock).subscribe(data=>{
      this.router.navigate(['/inventario'])
    });
  }

  onChange = ($event: Event)=>{

    const target = $event.target as HTMLInputElement;

    const file = (target.files as FileList)[0];

    this.convertImgToBase64(file);
  }

  convertImgToBase64(file:File){
    const observable = new Observable((subscriber: Subscriber<any>)=>{
      this.readFile(file,subscriber)
    });

    observable.subscribe((d)=>{
    this.imgBase64=String(d)
    console.log(this.imgBase64)
    })
  }
  readFile(file:File,subscriber:Subscriber<any>){
    const fileReader = new FileReader();
    fileReader.readAsDataURL(file);
    fileReader.onload=()=>{
      subscriber.next(fileReader.result);
      subscriber.complete();
    }

    fileReader.onerror = ()=>{
      subscriber.error()
      subscriber.complete()
    }
  }
logout(){
  console.log("saliendo");
  this.adminService.logut()
  .subscribe(data=>
    {
  this.cookieService.deleteAll();
   this.router.navigate(['/']);
    });
}
}
