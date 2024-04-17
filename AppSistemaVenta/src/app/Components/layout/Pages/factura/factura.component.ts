import { Component, OnInit, ViewChild } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';

import { FacturaService } from 'src/app/Services/factura.service';
import { UtilidadService } from 'src/app/Services/utilidad.service';

import { Factura } from './../../../../Interfaces/factura';
import { DetalleXFactura } from 'src/app/Interfaces/detalle-x-factura';


import Swal from 'sweetalert2';
import { MatDialog } from '@angular/material/dialog';
import { ModalClienteComponent } from '../../Modales/modal-cliente/modal-cliente.component';
import { MatPaginator } from '@angular/material/paginator';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';
import { ModalDetalleXFacturaComponent } from '../../Modales/modal-detalle-x-factura/modal-detalle-x-factura.component';

export const MY_DATA_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY'
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY'
  }
}

@Component({
  selector: 'app-factura',
  templateUrl: './factura.component.html',
  styleUrls: ['./factura.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS }
  ]
})
export class FacturaComponent implements OnInit {

  formularioBusqueda: FormGroup;
  opcionesBusqueda: any[] = [
    { value: "fecha", descripcion: "Por fechas" },
    { value: "numero", descripcion: "Numero venta" }
  ]
  columnasTabla: string[] = ['nroFactura', 'fecha', 'total', 'accion']
  dataInicio: Factura[] = [];
  datosListaFactura = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private _facturaServicio: FacturaService,
    private _utilidadServicio: UtilidadService
  ) {

    this.formularioBusqueda = this.fb.group({
      buscarPor: ['fecha'],
      numero: [''],
      fechaInicio: [''],
      fechaFin: ['']
    })

    this.formularioBusqueda.get("buscarPor")?.valueChanges.subscribe(value => {
      this.formularioBusqueda.patchValue({
        numero: "",
        fechaInicio: "",
        fechaFin: ""
      })
    })
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.datosListaFactura.paginator = this.paginacionTabla;
  }

  aplicarFiltroTabla(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datosListaFactura.filter = filterValue.trim().toLocaleLowerCase();
  }

  buscarFacturas() {
    let _fechaInicio: string = "";
    let _fechaFin: string = "";

    if (this.formularioBusqueda.value.buscarPor === "fecha") {
      _fechaInicio = moment(this.formularioBusqueda.value.fechaInicio).format('DD/MM/YYYY');
      _fechaFin = moment(this.formularioBusqueda.value.fechaFin).format('DD/MM/YYYY');

      if (_fechaInicio === "Invalid date" || _fechaFin === "Invalid date") {
        this._utilidadServicio.mostrarAlerta("Debe ingresar ambas fechas", "Oops!")
        return;
      }
    }

    this._facturaServicio.historial(
      this.formularioBusqueda.value.buscarPor,
      this.formularioBusqueda.value.numero,
      _fechaInicio,
      _fechaFin
    ).subscribe({
      next: (data) => {

        //if(data.status)
        this.datosListaFactura.data = data;
        //else
        //  this._utilidadServicio.mostrarAlerta("No se encontraron datos","Oops!");

      },
      error: (e) => { }
    })

  }

  verDetalleXFactura(_factura: Factura) {

    this.dialog.open(ModalDetalleXFacturaComponent, {
      data: _factura,
      disableClose: true,
      width: '700px'
    })
  }


}
