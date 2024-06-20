import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICliente } from '../../Interfaces/ICliente';
import { IAuthentication } from '../../Interfaces/IAuthentication';
import { ILogin } from '../../Interfaces/Request/ILogin';
import { IClienteRequest } from '../../Interfaces/Request/IClienteRequest';
import { environment } from 'src/app/environment/Enviroment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  url:string= environment.BaseApiUrl+"cliente/";

  constructor(private http: HttpClient) { }

  getClienteById(id:number): Observable<ICliente>{
    let GetUrl=this.url+id;
    return this.http.get<ICliente>(GetUrl);
  }

  getCliente(): Observable<ICliente>{
    let GetUrl=this.url;
    return this.http.get<ICliente>(GetUrl);
  }

  postCliente(clienteP:IClienteRequest): Observable<IAuthentication>{
    const headers= {
      'content-type':'application/json'
    }

  const body= JSON.stringify(clienteP);
  console.log(body);
    let GetUrl=this.url+"register";
   return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
  }

  putCliente(clienteP:ICliente): Observable<ICliente>{
    const headers= {
      'content-type':'application/json'
     }
    const body=JSON.stringify({'cliente':clienteP});
     console.log(body);
     let GetUrl=this.url+clienteP.id;
    return this.http.put<ICliente>(GetUrl,clienteP,{'headers':headers});
  }

  deleteCliente(id:number): Observable<ICliente>{
    const headers= {
      'content-type':'application/json'
     }

     let GetUrl=this.url+id;
    return this.http.delete<ICliente>(GetUrl,{'headers':headers});
  }

  postLogin(login:ILogin): Observable<IAuthentication>{
    const headers= {
      'content-type':'application/json'
    }

  const body= JSON.stringify(login);
  console.log(body);
    let GetUrl=this.url+"login";
   return this.http.post<IAuthentication>(GetUrl,body,{'headers':headers});
  }
  logut(): Observable<any>{
    let GetUrl=this.url+'logout';
    return this.http.delete<any>(GetUrl);
  }
}
