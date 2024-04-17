import { Component, OnInit, Inject } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Factura } from './../../../../Interfaces/factura';
import { DetalleXFactura } from './../../../../Interfaces/detalle-x-factura';

@Component({
  selector: 'app-modal-detalle-x-factura',
  templateUrl: './modal-detalle-x-factura.component.html',
  styleUrls: ['./modal-detalle-x-factura.component.css']
})
export class ModalDetalleXFacturaComponent implements OnInit {

  fechaRegistro:string = "";
  //numeroDocumento:string = "";
  //tipoPago: string = "";
  total: string = "";
  detalleXFactura: DetalleXFactura[] = [];
  columnasTabla :string[] = ['producto','cantidad','precio','total']

  constructor(  @Inject(MAT_DIALOG_DATA) public _factura: Factura) { 

     this.fechaRegistro = _factura.fecha!;

    // this.numeroDocumento = _factura.numeroDocumento!;
    // this.tipoPago = _factura.tipoPago;
    // this.total = _factura.totalTexto;
     this.detalleXFactura = _factura.detalleXFacturas; 

  }

  ngOnInit(): void {
  }

}
