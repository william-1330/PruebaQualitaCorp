import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Producto } from '../Interfaces/producto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private urlApi:string = environment.endpoint + "Producto/";

  constructor(private http:HttpClient) { }

  lista():Observable<Producto[]>{
    return this.http.get<Producto[]>(`${this.urlApi}Lista`)
  }

  guardar(request: Producto):Observable<Producto>{
    return this.http.post<Producto>(`${this.urlApi}Crear`,request)
  }

  editar(request: Producto):Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}Editar`,request)
  }

  eliminar(id: number):Observable<boolean>{
    return this.http.delete<boolean>(`${this.urlApi}Eliminar/${id}`)
  }
}
