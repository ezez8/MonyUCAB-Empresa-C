import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Pago } from '../../models/pago.model';
import { PagoService } from '../../servicios/pago/pago.service';
import { UsuarioService } from '../../servicios/usuario/usuario.service';
import { Usuario } from '../../models/usuario.model';
import { CuentaService } from '../../servicios/cuenta/cuenta.service';
import { TarjetaService } from '../../servicios/tarjeta/tarjeta.service';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-pagos-sin-sol',
  templateUrl: './pagos-sin-sol.page.html',
  styleUrls: ['./pagos-sin-sol.page.scss'],
})
export class PagosSinSolPage implements OnInit {

  operacion: Pago;
  usuario: Usuario;
  user: string;
  saldo: number;
  metodoPagoC = [];
  metodoPagoT = [];
  auxT = false;
  auxC = false;
  tarjeta: any;
  cuenta: any;

  constructor(
    public _activatedRoute: ActivatedRoute,
    public _pagoServices: PagoService,
    public _usuarioServices: UsuarioService,
    public _cuentaServices: CuentaService,
    public _tarjetaServices: TarjetaService,
    public router: Router,
    public alert: AlertController,

  ) { }

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe(paramMap => {
      const recipeID = paramMap.get('pagoID');
      let id: number = +recipeID;
      this.operacion = this._pagoServices.getpagoSol(id);
    });
    this._usuarioServices.inforUsurio(this.operacion.idusuario_receptor)
        .subscribe((data: any) => {
          this.user = data.usuario;
        });
    this.usuario = this._usuarioServices.getUsuario();
    this._cuentaServices.getCuentas(this.usuario.idUsuario)
        .subscribe((data: any) => {
          this.metodoPagoC = data;
        });
    this._tarjetaServices.getTarjetas(this.usuario.idUsuario)
        .subscribe((data: any) => {
          this.metodoPagoT = data;
        });
    this.saldo = this._pagoServices.getSaldo();
  }

  boolTarjeta() {
    this.auxT = true;
    this.auxC = false;
  }

  boolCuenta() {
    this.auxC = true;
    this.auxT = false;
  }

  obtenerIDtajeta() {
  }

  obtenerIDcuenta() {
  }

  pagarTarjeta() {
    let id: number = + this.tarjeta;
    let ref: number = + this.operacion.referencia;
    let cant: number = + this.operacion.monto;

    this._pagoServices.pagoTarjeta(id, this.user, cant, ref)
        .subscribe((data: any) => {
          console.log(data);
          this.router.navigate(['/tabs/cuenta']);
        });

  }

  pagarCuenta() {
    let id: number = + this.cuenta;
    let ref: number = + this.operacion.referencia;
    let cant: number = + this.operacion.monto;

    this._pagoServices.pagoCuenta(id, this.user, cant, ref)
        .subscribe((data: any) => {
          this.router.navigate(['/tabs/cuenta']);
        });
  }

  pagarMonedero() {
    if (this.saldo >= this.operacion.monto) {
      let ref: number = + this.operacion.referencia;
      let cant: number = + this.operacion.monto;

      this._pagoServices.pagoMonedero(this.usuario.idUsuario, this.user, cant, ref)
          .subscribe((data: any) => {
            this.router.navigate(['/tabs/cuenta']);
          });
    }
    else {
      this.AlertaError();
    }

  }

  async AlertaError() {
    const alertElement = await this.alert.create({
      header: 'Error en pago',
      message: 'El saldo no es suficiente',
      buttons: [
        {
          text: 'Aceptar',
          handler: () => {
          }
        },
      ]
    });
    await alertElement.present();

  }

}
