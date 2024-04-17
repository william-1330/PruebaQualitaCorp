import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Cliente } from '../Interfaces/cliente';


@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private urlApi:string = environment.endpoint + "Cliente/";

  constructor(private http:HttpClient) { }

  lista():Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.urlApi}Lista`)
  }

  guardar(request: Cliente):Observable<Cliente>{
    return this.http.post<Cliente>(`${this.urlApi}Crear`,request)
  }

  editar(request: Cliente):Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}Editar`,request)
  }

  eliminar(id: number):Observable<boolean>{
    return this.http.delete<boolean>(`${this.urlApi}Eliminar/${id}`)
  }
}
