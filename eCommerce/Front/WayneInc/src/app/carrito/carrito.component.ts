import { Component } from '@angular/core';
import { ICarrito } from '../Interfaces/ICarrito';
import { ArticuloClienteService } from '../Service/ArticuloCliente/articulo-cliente.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
})
export class CarritoComponent {
  Carrito!: ICarrito;

  constructor(private articuloCService:ArticuloClienteService, private router:Router) { }

  ngOnInit(): void {
    this.articuloCService.getCarrito().subscribe(data=>{
      console.log(data);
      this.Carrito=data;
    });
  }

  addArticulo(idArticulo:number){
    this.articuloCService.agregarItem(idArticulo).subscribe(data=>{
      alert(data)
      this.reloadCurrentRoute()
    });
  }

  removeArticulo(idItem:number){
    this.articuloCService.removeItem(idItem,this.Carrito.idCarrito).subscribe(data=>{
      alert(data)
      this.reloadCurrentRoute()
    });
  }
  comprar(){
    this.articuloCService.postComprar(this.Carrito.idCarrito).subscribe(data=>{
      alert(data)
    });
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/Carrito', {skipLocationChange: true}).then(() => {
        this.router.navigate(['']);
    });}
}
