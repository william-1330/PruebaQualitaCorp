import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';

import { DashBoardComponent } from './Pages/dash-board/dash-board.component';
import { UsuarioComponent } from './Pages/usuario/usuario.component';
import { ProductoComponent } from './Pages/producto/producto.component';
import { FacturaComponent } from './Pages/factura/factura.component';
import { ReporteComponent } from './Pages/reporte/reporte.component';
import { MesaComponent } from './Pages/mesa/mesa.component';

import { SharedModule } from 'src/app/Reutilizable/shared.module';
import { ModalUsuarioComponent } from './Modales/modal-usuario/modal-usuario.component';
import { ModalProductoComponent } from './Modales/modal-producto/modal-producto.component';
import { ModalDetalleXFacturaComponent } from './Modales/modal-detalle-x-factura/modal-detalle-x-factura.component';
import { ModalMesaComponent } from './Modales/modal-mesa/modal-mesa.component';
import { ModalClienteComponent } from './Modales/modal-cliente/modal-cliente.component';
import { ClienteComponent } from './Pages/cliente/cliente.component';
import { GenerarFacturaComponent } from './Pages/generar-factura/generar-factura.component';



@NgModule({
  declarations: [
    DashBoardComponent,
    UsuarioComponent,
    ProductoComponent,
    FacturaComponent,
    MesaComponent,
    ReporteComponent,
    ModalUsuarioComponent,
    ModalProductoComponent,
    ModalDetalleXFacturaComponent,
    ModalMesaComponent,
    ModalClienteComponent,
    ClienteComponent,
    GenerarFacturaComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,

    SharedModule
  ]
})
export class LayoutModule { }
