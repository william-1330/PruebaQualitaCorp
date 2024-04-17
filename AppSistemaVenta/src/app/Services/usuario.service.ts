import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Login } from '../Interfaces/login';
import { Usuario } from '../Interfaces/usuario';
import { Sesion } from '../Interfaces/sesion';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private urlApi:string = environment.endpoint + "Usuario/";

  constructor(private http:HttpClient) { }

  iniciarSesion(request: Login):Observable<Sesion>{
    return this.http.post<Sesion>(`${this.urlApi}IniciarSesion`,request)
  }

  lista():Observable<Usuario[]>{
    return this.http.get<Usuario[]>(`${this.urlApi}Lista`)
  }

  guardar(request: Usuario):Observable<Usuario>{
    return this.http.post<Usuario>(`${this.urlApi}Crear`,request)
  }

  editar(request: Usuario):Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}Editar`,request)
  }

  eliminar(id: number):Observable<boolean>{
    return this.http.delete<boolean>(`${this.urlApi}Eliminar/${id}`)
  }
}
