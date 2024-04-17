import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as moment from 'moment';

import * as XLSX from "xlsx"

import { Reporte } from 'src/app/Interfaces/reporte';
import { UtilidadService } from 'src/app/Services/utilidad.service';
import { ReporteService } from 'src/app/Services/reporte.service';

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
  selector: 'app-reporte',
  templateUrl: './reporte.component.html',
  styleUrls: ['./reporte.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATA_FORMATS }
  ]
})
export class ReporteComponent implements OnInit {

  formularioFiltro: FormGroup;
  opcionesBusqueda: any[] = [
    { value: "general", descripcion: "Reporte General" },
    { value: "TotalXMesero", descripcion: "Reporte por Mesero" },
    { value: "CunsumoXCliente", descripcion: "Consumo por Cliente" },
    { value: "PlatoMasVendido", descripcion: "Plato más vendido" }
  ]
  listaVentasReporte: Reporte[] = [];
  listaValores: any[] = [];

  columnasTabla: string[] = ['nroFactura', 'fecha', 'producto', 'precio', 'cantidad', 'total', 'nombreMesero', 'nombreSupervisor'];
  columnasTabla1: string[] = ['idMesero', 'nombres', 'apellidos', 'valor'];
  columnasTabla2: string[] =  ['idCliente', 'nombres', 'apellidos', 'valor'];
  columnasTabla3: string[] = ['plato', 'valor', 'items'];


  dataVentaReporte = new MatTableDataSource(this.listaVentasReporte);
  dataVentaReporte1 = new MatTableDataSource(this.listaValores);
  dataVentaReporte2 = new MatTableDataSource(this.listaValores);
  dataVentaReporte3 = new MatTableDataSource(this.listaValores);

  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private _reporteServicio: ReporteService,
    private _utilidadServicio: UtilidadService
  ) {

    this.formularioFiltro = this.fb.group({
      buscarPor: ['general'],
      monto: [''],
      fechaInicio: ['', Validators.required],
      fechaFin: ['', Validators.required]
    })

  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.dataVentaReporte.paginator = this.paginacionTabla;
  }

  buscarVentas() {

    const _fechaInicio = moment(this.formularioFiltro.value.fechaInicio).format('DD/MM/YYYY');
    const _fechaFin = moment(this.formularioFiltro.value.fechaFin).format('DD/MM/YYYY');

    if (_fechaInicio === "Invalid date" || _fechaFin === "Invalid date") {
      this._utilidadServicio.mostrarAlerta("Debe ingresar ambas fechas", "Oops!")
      return;
    }

    if (this.formularioFiltro.value.buscarPor === "general") {
      this._reporteServicio.reporte(
        _fechaInicio,
        _fechaFin
      ).subscribe({
        next: (data) => {
          this.listaVentasReporte = data;
          this.dataVentaReporte.data = data;
        },
        error: (e) => { }
      })
    }
    else if(this.formularioFiltro.value.buscarPor === "TotalXMesero"){
      this._reporteServicio.TotalXMesero(
        _fechaInicio,
        _fechaFin
      ).subscribe({
        next: (data) => {
          this.listaVentasReporte = data;
          this.dataVentaReporte1.data = data;
        },
        error: (e) => { }
      })
    }
    else if(this.formularioFiltro.value.buscarPor === "CunsumoXCliente"){
      this._reporteServicio.CunsumoXCliente(
        _fechaInicio,
        _fechaFin
      ).subscribe({
        next: (data) => {
          this.listaVentasReporte = data;
          this.dataVentaReporte2.data = data;
        },
        error: (e) => { }
      })
    }
    else if(this.formularioFiltro.value.buscarPor === "PlatoMasVendido"){
      this._reporteServicio.PlatoMasVendido(
        _fechaInicio,
        _fechaFin
      ).subscribe({
        next: (data) => {
          this.listaVentasReporte = data;
          this.dataVentaReporte3.data = data;
        },
        error: (e) => { }
      })
    }




  }

  exportarExcel() {
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(this.listaVentasReporte);

    XLSX.utils.book_append_sheet(wb, ws, "Reporte");
    XLSX.writeFile(wb, "Reporte Ventas.xlsx");

  }



}
