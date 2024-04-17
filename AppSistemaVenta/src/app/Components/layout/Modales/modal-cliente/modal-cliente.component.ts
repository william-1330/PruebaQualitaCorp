import { Component, OnInit, Inject } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Cliente } from 'src/app/Interfaces/cliente';
import { ClienteService } from 'src/app/Services/cliente.service';
import { RolService } from 'src/app/Services/rol.service';
import { UtilidadService } from 'src/app/Services/utilidad.service';

@Component({
  selector: 'app-modal-cliente',
  templateUrl: './modal-cliente.component.html',
  styleUrls: ['./modal-cliente.component.css']
})
export class ModalClienteComponent implements OnInit {

  formularioCliente: FormGroup;
  ocultarPassword: boolean = true;
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";
  //listaRoles: Rol[] = [];

  constructor(
    private modalActual: MatDialogRef<ModalClienteComponent>,
    @Inject(MAT_DIALOG_DATA) public datosCliente: Cliente,
    private fb: FormBuilder,
    private _clienteServicio: ClienteService,
    private _utilidadServicio: UtilidadService
  ) {

    this.formularioCliente = this.fb.group({
      cedula: ['', Validators.required],
      nombres: ['', Validators.required],
      apellidos: ['', Validators.required],
      direccion: ['', Validators.required],
      telefono: ['', Validators.required]
    });

    if (this.datosCliente != null) {
      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";
    }
  }

  ngOnInit(): void {

    if (this.datosCliente != null) {

      this.formularioCliente.patchValue({
        cedula: this.datosCliente.cedula,
        nombres: this.datosCliente.nombres,
        apellidos: this.datosCliente.apellidos,
        direccion: this.datosCliente.direccion,
        telefono: this.datosCliente.telefono
      })
    }
  }


  guardarEditar_Cliente() {

    const _cliente: Cliente = {
      idCliente: this.datosCliente == null ? 0 : this.datosCliente.idCliente,
      cedula: this.formularioCliente.value.cedula,
      nombres: this.formularioCliente.value.nombres,
      apellidos: this.formularioCliente.value.apellidos,
      direccion: this.formularioCliente.value.direccion,
      telefono: this.formularioCliente.value.telefono
    }

    if (this.datosCliente == null) {

      this._clienteServicio.guardar(_cliente).subscribe({
        next: (data) => {
          //if(data.status){
          this._utilidadServicio.mostrarAlerta("El cliente fue registrado", "Exito");
          this.modalActual.close("true")
          //}else
          //  this._utilidadServicio.mostrarAlerta("No se pudo registrar el usuario","Error")
        },
        error: (e) => { }
      })

    } else {

      this._clienteServicio.editar(_cliente).subscribe({
        next: (data) => {
          //if(data.status){
          this._utilidadServicio.mostrarAlerta("El cliente fue editado", "Exito");
          this.modalActual.close("true")
          //}else
          //  this._utilidadServicio.mostrarAlerta("No se pudo editar el usuario","Error")
        },
        error: (e) => { }
      })
    }

  }
}
