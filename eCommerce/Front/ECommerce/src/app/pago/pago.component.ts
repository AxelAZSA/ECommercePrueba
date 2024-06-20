import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompraService } from '../services/Compra/compra.service';
import { ICompraRequest } from '../Interfaces/Request/ICompraRequest';

@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.css']
})
export class PagoComponent {

idCarrito!:number;
  constructor(private CompraService:CompraService,private form:FormBuilder, private router:Router, private actRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.actRoute.paramMap.subscribe(params => {
      this.idCarrito= Number(params.get('idCarrito'));
    });}

    onSubmit(){
      const req:ICompraRequest={
        idTienda: 1
      }
      this.CompraService.postCompra(req).subscribe(data=>{
        this.router.navigate(['/cliente']);
      });
    }
}
