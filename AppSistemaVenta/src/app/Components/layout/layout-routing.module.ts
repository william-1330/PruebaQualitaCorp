import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { LayoutComponent } from './layout.component';
import { DashBoardComponent } from './Pages/dash-board/dash-board.component';
//import { HistorialVentaComponent } from './Pages/historial-venta/historial-venta.component';
import { ProductoComponent } from './Pages/producto/producto.component';
import { ReporteComponent } from './Pages/reporte/reporte.component';
import { UsuarioComponent } from './Pages/usuario/usuario.component';
import { FacturaComponent } from './Pages/factura/factura.component';
import { MesaComponent } from './Pages/mesa/mesa.component';
import { ClienteComponent } from './Pages/cliente/cliente.component';
import { GenerarFacturaComponent } from './Pages/generar-factura/generar-factura.component';

const routes: Routes = [{
  path: '',
  component: LayoutComponent,
  children: [
    { path: 'dashboard', component: DashBoardComponent },
    { path: 'usuarios', component: UsuarioComponent },
    { path: 'productos', component: ProductoComponent },
    { path: 'mesas', component: MesaComponent },
    { path: 'facturas', component: FacturaComponent },
    //{path:'historial_venta',component:HistorialVentaComponent},
    { path: 'reportes', component: ReporteComponent },
    { path: 'clientes', component: ClienteComponent },
    { path: 'generarFactura', component: GenerarFacturaComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
