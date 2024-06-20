import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { CarritoComponent } from './carrito/carrito.component';
import { PagoComponent } from './pago/pago.component';
import { InventarioComponent } from './inventario/inventario.component';
import { InventarioEditComponent } from './inventario-edit/inventario-edit.component';
import { ClienteComponent } from './cliente/cliente.component';
import { ClienteEditComponent } from './cliente-edit/cliente-edit.component';
import { JwtInterceptorInterceptor } from './jwt-interceptor.interceptor';
import { CookieService } from 'ngx-cookie-service';
import { NavbarComponent } from './navbar/navbar.component';
import { ArticuloAddComponent } from './articulo-add/articulo-add.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ArticulosComponent,
    CarritoComponent,
    PagoComponent,
    InventarioComponent,
    InventarioEditComponent,
    ClienteComponent,
    ClienteEditComponent,
    NavbarComponent,
    ArticuloAddComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptorInterceptor,multi:true},
  CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
