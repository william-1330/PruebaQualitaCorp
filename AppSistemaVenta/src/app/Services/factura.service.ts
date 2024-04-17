import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Factura } from '../Interfaces/factura';
import { Reporte } from '../Interfaces/reporte';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FacturaService {

  private urlApi: string = environment.endpoint + "Factura/";

  constructor(private http: HttpClient) { }

  registrar(request: Factura): Observable<Factura> {
    return this.http.post<Factura>(`${this.urlApi}Crear`, request)
  }

  historial(buscarPor: string, numeroFactura: string, fechaInicio: string, fechaFin: string): Observable<Factura[]> {

    if (buscarPor === 'fecha') {
      return this.http.get<Factura[]>(`${this.urlApi}ListaXFecha?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
    }
    else {
      return this.http.get<Factura[]>(`${this.urlApi}FacturaXNumero?numFactura=${numeroFactura}`)
    }

  }

  reporte(fechaInicio: string, fechaFin: string): Observable<Reporte[]> {
    return this.http.get<Reporte[]>(`${this.urlApi}Reporte?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`)
  }
}
