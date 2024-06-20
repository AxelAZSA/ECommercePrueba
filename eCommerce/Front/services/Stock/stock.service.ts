import { Injectable } from '@angular/core';
import { environment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { IStock } from '../../Interfaces/IStock';
import { Observable } from 'rxjs';
import { IStockRequest } from '../../Interfaces/Request/IStockRequest';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  url:string= environment.BaseApiUrl+"stock/";

  constructor(private http: HttpClient) { }

  getbyId(id:number): Observable<IStock>{
    let GetUrl=this.url+id;
    return this.http.get<IStock>(GetUrl);
  }

  getbyAll(): Observable<IStock[]>{
    let GetUrl=this.url;
    return this.http.get<IStock[]>(GetUrl);
  }
  getbyArticulo(idArticulo:number): Observable<IStock[]>{
    let GetUrl=this.url+"articulo/"+idArticulo;
    return this.http.get<IStock[]>(GetUrl);
  }

  postStock(request:IStockRequest): Observable<any>{
    const headers= {
      'content-type':'application/json'
    }
    const body=JSON.stringify(request);
    console.log(body);
    let GetUrl=this.url;
   return this.http.post<any>(GetUrl,{'headers':headers});
  }

  putItemHome(idStock:number,stock:IStock): Observable<any>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify(stock);
     console.log(body);
     let GetUrl=this.url+idStock;
    return this.http.put<any>(GetUrl,body,{'headers':headers});
  }


  delete(id:number): Observable<any>{
    const headers= {
      'content-type':'application/json'
     }

     let GetUrl=this.url+id;
    return this.http.delete<any>(GetUrl,{'headers':headers});
  }
  }

