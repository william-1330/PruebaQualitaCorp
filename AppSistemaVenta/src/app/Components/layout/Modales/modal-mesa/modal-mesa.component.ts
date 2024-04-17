import { Component, OnInit, Inject } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
//import { Rol } from 'src/app/Interfaces/rol';
import { Mesa } from 'src/app/Interfaces/mesa';

//import { RolService } from 'src/app/Services/rol.service';
import { MesaService } from 'src/app/Services/mesa.service';
import { UtilidadService } from 'src/app/Services/utilidad.service';


@Component({
  selector: 'app-modal-mesa',
  templateUrl: './modal-mesa.component.html',
  styleUrls: ['./modal-mesa.component.css']
})
export class ModalMesaComponent implements OnInit {

  formularioMesa: FormGroup;
  ocultarPassword: boolean = true;
  tituloAccion: string = "Agregar";
  botonAccion: string = "Guardar";
  //listaRoles: Rol[] = [];

  constructor(
    private modalActual: MatDialogRef<ModalMesaComponent>,
    @Inject(MAT_DIALOG_DATA) //public datosUsuario: Usuario,
    public datosMesa: Mesa,
    private fb: FormBuilder,
    //private _rolServicio: RolService,
    private _mesaServicio: MesaService,
    private _utilidadServicio: UtilidadService
  ) {

    this.formularioMesa = this.fb.group({
      nroMesa: ['', Validators.required],
      nombre: ['', Validators.required],
      reservada: ['', Validators.required],
      puestos: ['', Validators.required]

      // nombres: ['', Validators.required],
      // apellidos: ['', Validators.required],
      // edad: ['', Validators.required],
      // antiguedad: ['', Validators.required],
      // correo: ['', Validators.required],
      // clave: ['', Validators.required],
      // idRol: ['', Validators.required]
    });

    if (this.datosMesa != null) {
      this.tituloAccion = "Editar";
      this.botonAccion = "Actualizar";
    }

    // this._rolServicio.lista().subscribe({
    //   next: (data) => {
    //     //if(data.status) 
    //     this.listaRoles = data
    //   },
    //   error: (e) => { }
    // })

  }

  ngOnInit(): void {

    if (this.datosMesa != null) {

      this.formularioMesa.patchValue({
         nroMesa : this.datosMesa.nroMesa,
         nombre: this.datosMesa.nombre,
         reservada: this.datosMesa.reservada,
         puestos:  this.datosMesa.puestos
        // apellidos : this.datosUsuario.apellidos,
        // edad : this.datosUsuario.edad,
        // antiguedad : this.datosUsuario.antiguedad,
        // correo: this.datosUsuario.correo,
        // clave: this.datosUsuario.clave,
        // idRol: this.datosUsuario.idRol ,
        // rolDescripcion : this.datosUsuario.rolDescripcion  
      })
    }
  }


  guardarEditar_Mesa() {

    const _mesa: Mesa = {
      nroMesa: this.formularioMesa.value.nroMesa,
      nombre: this.formularioMesa.value.nombre,
      reservada: this.formularioMesa.value.reservada,
      puestos: this.formularioMesa.value.puestos

      // idUsuario: this.datosUsuario == null ? 0 : this.datosUsuario.idUsuario,
      // nombres: this.formularioUsuario.value.nombres,
      // apellidos: this.formularioUsuario.value.apellidos,
      // edad: this.formularioUsuario.value.edad,
      // antiguedad: this.formularioUsuario.value.antiguedad,
      // correo: this.formularioUsuario.value.correo,
      // clave: this.formularioUsuario.value.clave,
      // idRol: this.formularioUsuario.value.idRol,
      // rolDescripcion: ''
    }

    if (this.datosMesa == null) {

      this._mesaServicio.guardar(_mesa).subscribe({
        next: (data) => {
          //if(data.status){
          this._utilidadServicio.mostrarAlerta("La mesa fue registrada", "Exito");
          this.modalActual.close("true")
          //}else
          //  this._utilidadServicio.mostrarAlerta("No se pudo registrar el usuario","Error")
        },
        error: (e) => { }
      })

    } else {

      this._mesaServicio.editar(_mesa).subscribe({
        next: (data) => {
          //if(data.status){
          this._utilidadServicio.mostrarAlerta("La mesa fue editada", "Exito");
          this.modalActual.close("true")
          //}else
          //  this._utilidadServicio.mostrarAlerta("No se pudo editar el usuario","Error")
        },
        error: (e) => { }
      })
    }

  }
}
