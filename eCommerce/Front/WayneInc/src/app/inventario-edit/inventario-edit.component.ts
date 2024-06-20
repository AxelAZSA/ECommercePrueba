import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ITienda } from '../Interfaces/ITienda';
import { IArticulo } from '../Interfaces/IArticulo';
import { TiendaService } from '../services/Tienda/tienda.service';
import { ArticuloService } from '../services/Articulo/articulo.service';
import { StockService } from '../services/Stock/stock.service';
import { IStockRequest } from '../Interfaces/Request/IStockRequest';

@Component({
  selector: 'app-inventario-edit',
  templateUrl: './inventario-edit.component.html',
  styleUrls: ['./inventario-edit.component.css']
})
export class InventarioEditComponent {

  tiendas!:ITienda[];
  articulos!:IArticulo[];

  sentStock:FormGroup=this.form.group({
    stock: new FormControl(),
    idArticulo: new FormControl(),
    idTienda: new FormControl()
  });

  constructor(private tiendaService:TiendaService,private articuloService:ArticuloService,private stockService:StockService,private form:FormBuilder,private router:Router){}

  ngOnInit():void{
    this.articuloService.getAll().subscribe(data=>{
      this.articulos = data;
      this.tiendaService.getAll().subscribe(data=>{
        this.tiendas=data;
      })
    })

  }
  onSubmit(data:any){
    var idArticulo = Number(data.idArticulo)
    var stock = Number(data.stock)
    var idTienda = Number(data.idTienda)
    const stockRequest:IStockRequest =
    {
      idArticulo: idArticulo,
      idTienda: idTienda,
      cantidad: stock
    };

    this.stockService.postStock(stockRequest).subscribe(data=>{
      this.router.navigate(['/inventario'])
    });
  }
}
