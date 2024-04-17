import { HttpClient } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Reporte } from '../Interfaces/reporte';

@Injectable({
  providedIn: 'root'
})
export class ReporteService {

  private urlApi: string = environment.endpoint + "Reporte/";

  constructor(private http: HttpClient) { }

  reporte(fechaInicio: string, fechaFin: string): Observable<Reporte[]> {
    return this.http.get<Reporte[]>(`${this.urlApi}General?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }

  TotalXMesero(fechaInicio: string, fechaFin: string): Observable<Reporte[]> {
    return this.http.get<Reporte[]>(`${this.urlApi}TotalXMesero?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }
  CunsumoXCliente(fechaInicio: string, fechaFin: string): Observable<Reporte[]> {
    return this.http.get<Reporte[]>(`${this.urlApi}CunsumoXCliente?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }

  PlatoMasVendido(fechaInicio: string, fechaFin: string): Observable<Reporte[]> {
    return this.http.get<Reporte[]>(`${this.urlApi}PlatoMasVendido?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }



}
