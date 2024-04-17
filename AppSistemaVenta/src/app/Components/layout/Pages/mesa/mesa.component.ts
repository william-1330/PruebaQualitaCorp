import { Component, OnInit, ViewChild } from '@angular/core';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

import { Mesa } from 'src/app/Interfaces/mesa';
import { MesaService } from 'src/app/Services/mesa.service';
import { UtilidadService } from 'src/app/Services/utilidad.service';
import Swal from 'sweetalert2';
import { ModalMesaComponent } from '../../Modales/modal-mesa/modal-mesa.component';

@Component({
  selector: 'app-mesa',
  templateUrl: './mesa.component.html',
  styleUrls: ['./mesa.component.css']
})
export class MesaComponent implements OnInit {
  columnasTabla: string[] = ['nroMesa', 'nombre', 'reservada', 'puestos', 'acciones'];
  dataInicio: Mesa[] = [];
  dataListaMesas = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) paginacionTabla!: MatPaginator;


  constructor(
    private dialog: MatDialog,
    private _mesaServicio: MesaService,
    private _utilidadServicio: UtilidadService

  ) { }

  obtenerMesas() {

    this._mesaServicio.lista().subscribe({
      next: (data) => {
        //if(data.status)
        this.dataListaMesas.data = data;
        //else
        //  this._utilidadServicio.mostrarAlerta("No se encontraron datos","Oops!")
      },
      error: (e) => { }
    })

  }

  ngOnInit(): void {
    this.obtenerMesas();
  }

  ngAfterViewInit(): void {
    this.dataListaMesas.paginator = this.paginacionTabla;
  }

  aplicarFiltroTabla(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListaMesas.filter = filterValue.trim().toLocaleLowerCase();
  }

  nuevaMesa() {
    this.dialog.open(ModalMesaComponent, {
      disableClose: true
    }).afterClosed().subscribe(resultado => {
      if (resultado === "true") this.obtenerMesas();
    });
  }

  editarMesa(mesa: Mesa) {
    this.dialog.open(ModalMesaComponent, {
      disableClose: true,
      data: mesa
    }).afterClosed().subscribe(resultado => {
      if (resultado === "true") this.obtenerMesas();
    });
  }

  eliminarMesa(mesa: Mesa) {

    Swal.fire({
      title: '¿Desea eliminar el producto?',
      text: mesa.nroMesa.toString(),
      icon: "warning",
      confirmButtonColor: '#3085d6',
      confirmButtonText: "Si, eliminar",
      showCancelButton: true,
      cancelButtonColor: '#d33',
      cancelButtonText: 'No, volver'
    }).then((resultado) => {

      if (resultado.isConfirmed) {

        this._mesaServicio.eliminar(mesa.nroMesa).subscribe({
          next: (data) => {

            //if(data.status){
            this._utilidadServicio.mostrarAlerta("El producto fue eliminado", "Listo!");
            this.obtenerMesas();
            //}else
            //  this._utilidadServicio.mostrarAlerta("No se pudo eliminar el producto","Error");

          },
          error: (e) => { }
        })

      }

    })

  }

}
