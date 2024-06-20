import { Component } from '@angular/core';
import { IArticulo } from '../Interfaces/IArticulo';
import { ArticuloService } from '../Service/Articulo/articulo.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ArticuloClienteService } from '../Service/ArticuloCliente/articulo-cliente.service';

@Component({
  selector: 'app-articulos',
  templateUrl: './articulos.component.html',
  styleUrls: ['./articulos.component.css']
})
export class ArticulosComponent {
  indexItems!: IArticulo[];
  constructor(private articuloService:ArticuloService,private articuloCService:ArticuloClienteService, private router:Router) { }

  ngOnInit(): void {
    this.articuloService.getAll().subscribe(data=>{
      this.indexItems=data
    });
  }

  addArticulo(idArticulo:number){
    this.articuloCService.agregarItem(idArticulo).subscribe(data=>{
      alert('Agregado');
    });
  }

  getArticulos():Observable<IArticulo[]>{
    return this.articuloService.getAll();
  }
}
