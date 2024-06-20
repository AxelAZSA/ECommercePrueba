import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ClienteComponent } from './cliente/cliente.component';
import { InventarioComponent } from './inventario/inventario.component';
import { CarritoComponent } from './carrito/carrito.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { ClienteEditComponent } from './cliente-edit/cliente-edit.component';
import { InventarioEditComponent } from './inventario-edit/inventario-edit.component';
import { ArticuloAddComponent } from './articulo-add/articulo-add.component';
import { PagoComponent } from './pago/pago.component';
import { ClienteGuardGuard } from './cliente-guard.guard';
import { AdminGuardGuard } from './admin-guard.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'cliente', component: ClienteComponent, canActivate:[ClienteGuardGuard]},
  { path: 'cliente/:id', component: ClienteEditComponent},
  { path: 'inventario', component: InventarioComponent, canActivate:[AdminGuardGuard]},
  { path: 'carrito', component: CarritoComponent, canActivate:[ClienteGuardGuard]},
  { path: 'carrito/pago/:idCarrito', component: PagoComponent, canActivate:[ClienteGuardGuard]},
  { path: '', component: ArticulosComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
