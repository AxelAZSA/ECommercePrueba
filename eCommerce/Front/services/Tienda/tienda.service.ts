import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { ITienda } from '../../Interfaces/ITienda';

@Injectable({
  providedIn: 'root'
})
export class TiendaService {
  url:string= environment.BaseApiUrl+"Tienda/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<ITienda[]>{
    let GetUrl=this.url;
    return this.http.get<ITienda[]>(GetUrl);
  }

}
