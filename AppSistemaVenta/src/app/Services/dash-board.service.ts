import { Injectable } from '@angular/core';

import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {

  private urlApi: string = environment.endpoint + "Dashboard/";

  constructor(private http: HttpClient) { }

  resumen(): Observable<any> {
    return this.http.get<any>(`${this.urlApi}Resumen`)
  }

}
