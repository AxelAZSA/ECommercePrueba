import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICarrito } from '../../Interfaces/ICarrito';
import { Observable } from 'rxjs';
import { ICarritoRequest } from '../../Interfaces/Request/ICarritoRequest.';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  //agregar item, item , post get

  url:string= environment.BaseApiUrl+"carrito/";

  constructor(private http: HttpClient) { }

  getCarrito(): Observable<ICarrito>{
    let GetUrl=this.url;
    return this.http.get<ICarrito>(GetUrl);
  }

  postCarrito(): Observable<any>{
    const headers= {
      'content-type':'application/json'
    }

    let GetUrl=this.url;
   return this.http.post<any>(GetUrl,{'headers':headers});
  }

  putItemHome(idArticulo:number): Observable<any>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify({'idArticulo':idArticulo});
     console.log(body);
     let GetUrl=this.url+"home";
    return this.http.put<any>(GetUrl,body,{'headers':headers});
  }


  putItem(idCarritoItem:number,cantidad:number): Observable<any>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify({'idCarritoItem':idCarritoItem,'cantidad':cantidad});
     console.log(body);
     let GetUrl=this.url;
    return this.http.put<any>(GetUrl,body,{'headers':headers});
  }
}
