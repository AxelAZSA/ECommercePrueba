import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class JwtInterceptorInterceptor implements HttpInterceptor {

  constructor(private cookieService: CookieService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.cookieService.get('token')
    let req = request;
    if(token)
    {
      req = request.clone({setHeaders: {authorization: `Bearer ${token}`}});
    }else{
      req = request.clone({setHeaders: {authorization: `Bearer ${this.cookieService.get('tokenAd')}`}});
    }
    return next.handle(req);
  }
}
