import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICompra } from '../../Interfaces/ICompra';
import { Observable } from 'rxjs';
import { ICompraItem } from '../../Interfaces/ICompraItem';
import { ICompraRequest } from '../../Interfaces/Request/ICompraRequest';
import { IEstadoRequest } from '../../Interfaces/Request/IEstadoRequest';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class CompraService {
  url:string= environment.BaseApiUrl+"compra/";

  constructor(private http: HttpClient) { }

  getByIdCliente(): Observable<ICompra[]>{
    let GetUrl=this.url+"cliente/";
    return this.http.get<ICompra[]>(GetUrl);
  }

  getByITienda(idTienda:number): Observable<ICompra[]>{
    let GetUrl=this.url+"tienda/"+idTienda;
    return this.http.get<ICompra[]>(GetUrl);
  }

  getAll(): Observable<ICompra[]>{
    let GetUrl=this.url;
    return this.http.get<ICompra[]>(GetUrl);
  }

  getPendientePago(): Observable<ICompra[]>{
    let GetUrl=this.url+"pendiente/pago";
    return this.http.get<ICompra[]>(GetUrl);
  }
  getPendienteEnvio(): Observable<ICompra[]>{
    let GetUrl=this.url+"pendiente/envio";
    return this.http.get<ICompra[]>(GetUrl);
  }


  getById(id:number): Observable<ICompra>{
    let GetUrl=this.url+id;
    return this.http.get<ICompra>(GetUrl);
  }

  getItems(idCompra:number): Observable<ICompraItem[]>{
    let GetUrl=this.url+"detalles/"+idCompra;
    return this.http.get<ICompraItem[]>(GetUrl);
  }

  postCompra(compraReq:ICompraRequest): Observable<any>{
    const headers= {
      'content-type':'application/json'
    }

  const body= JSON.stringify(compraReq);
  console.log(body);
    let GetUrl=this.url;
   return this.http.post<any>(GetUrl,body,{'headers':headers});
  }

  putCliente(id:number,req:IEstadoRequest): Observable<any>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify(req);
     console.log(body);
     let GetUrl=this.url+id;
    return this.http.put<any>(GetUrl,req,{'headers':headers});
  }
}
