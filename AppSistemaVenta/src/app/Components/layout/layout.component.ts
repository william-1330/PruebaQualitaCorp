import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { UtilidadService } from 'src/app/Services/utilidad.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  //listaMenus:Menu[] = [];
  correoUsuario:string = '';
  rolUsuario:string = '';


  constructor(
    private router:Router,
    private _utilidadServicio: UtilidadService
  ) { }

  ngOnInit(): void {

    const usuario = this._utilidadServicio.obtenerSesionUsuario();

    if(usuario != null){

      this.correoUsuario = usuario.correo;
      this.rolUsuario = usuario.rolDescripcion;
    }
  }


  cerrarSesion(){
    this._utilidadServicio.eliminarSesionUsuario();
    this.router.navigate(['login']);
  }

}
