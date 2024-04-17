import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Mesa } from '../Interfaces/mesa';

@Injectable({
  providedIn: 'root'
})
export class MesaService {

  private urlApi:string = environment.endpoint + "Mesa/";

  constructor(private http:HttpClient) { }

 
  lista():Observable<Mesa[]>{
    return this.http.get<Mesa[]>(`${this.urlApi}Lista`)
  }

  guardar(request: Mesa):Observable<Mesa>{
    return this.http.post<Mesa>(`${this.urlApi}Crear`,request)
  }

  editar(request: Mesa):Observable<boolean>{
    return this.http.put<boolean>(`${this.urlApi}Editar`,request)
  }

  eliminar(id: number):Observable<boolean>{
    return this.http.delete<boolean>(`${this.urlApi}Eliminar/${id}`)
  }
}
