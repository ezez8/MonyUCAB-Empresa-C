import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cuenta } from '../../models/cuenta.model';

@Injectable({
  providedIn: 'root'
})
export class CuentaService {

  cuentas: Cuenta[] = [];

  constructor(
    public http: HttpClient
  ) { }

  getVacio() {

    return [...this.cuentas];

  }

  getCuentas(idusuario: number) {
    let url: string = 'http://monyucab.somee.com/api/Usuario/infoCuentas';

    let data = {
      "id" : idusuario
    };

    return this.http.post(url, data);
  }
}
