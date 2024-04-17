import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { ProductoService } from 'src/app/Services/producto.service';
import { FacturaService } from 'src/app/Services/factura.service';
import { UtilidadService } from 'src/app/Services/utilidad.service';
import { UsuarioService } from 'src/app/Services/usuario.service';
import { MesaService } from 'src/app/Services/mesa.service';
import { ClienteService } from 'src/app/Services/cliente.service';

import { Producto } from 'src/app/Interfaces/producto';
import { Factura } from './../../../../Interfaces/factura';
import { DetalleXFactura } from 'src/app/Interfaces/detalle-x-factura';
import { Cliente } from 'src/app/Interfaces/cliente';
import { Usuario } from 'src/app/Interfaces/usuario';
import { Mesa } from 'src/app/Interfaces/mesa';

import Swal from 'sweetalert2';
import { MatDialog } from '@angular/material/dialog';
import { ModalClienteComponent } from '../../Modales/modal-cliente/modal-cliente.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Sesion } from 'src/app/Interfaces/sesion';

@Component({
  selector: 'app-generar-factura',
  templateUrl: './generar-factura.component.html',
  styleUrls: ['./generar-factura.component.css']
})
export class GenerarFacturaComponent implements OnInit {

  listaProductos: Producto[] = [];
  listaProductosFiltro: Producto[] = [];

  listaProductosParaVenta: DetalleXFactura[] = [];
  bloquearBotonRegistrar: boolean = false;

  listaMeseros: Usuario[] = [];
  listaMesas: Mesa[] = [];
  listaClientes: Cliente[] = [];
  listaClientesFiltro: Cliente[] = [];

  productoSeleccionado!: Producto;
  clienteSeleccionado!: Cliente;
  meseroSeleccionado!: Usuario;
  mesaSeleccionada!: Mesa;
  totalPagar: number = 0;
  review_btn: boolean = false;

  formularioProductoVenta: FormGroup;
  columnasTabla: string[] = ['producto', 'cantidad', 'precio', 'total', 'accion'];
  datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);

  usuarioSesion!: Sesion;

  retornarProductosPorFiltro(busqueda: any): Producto[] {
    const valorBuscado = typeof busqueda === "string" ? busqueda.toLocaleLowerCase() : busqueda.nombre.toLocaleLowerCase();

    return this.listaProductos.filter(item => item.nombre.toLocaleLowerCase().includes(valorBuscado));
  }

  retornarClientesPorFiltro(busqueda: any): Cliente[] {
    const valorBuscado = typeof busqueda === "string" ? busqueda.toLocaleLowerCase() : busqueda.cedula.toString().toLocaleLowerCase();

    return this.listaClientes.filter(item => item.cedula.toString().toLocaleLowerCase().includes(valorBuscado));
  }

  constructor(
    private fb: FormBuilder,
    private _productoServicio: ProductoService,
    private _facturaServicio: FacturaService,
    private _utilidadServicio: UtilidadService,
    private _usuarioServicio: UsuarioService,
    private _mesaServicio: MesaService,
    private _clienteServicio: ClienteService,
    private dialog: MatDialog,
  ) {

    this.formularioProductoVenta = this.fb.group({
      producto: ['', Validators.required],
      cliente: ['', Validators.required],
      cantidad: ['', Validators.required],
      mesero: ['', Validators.required],
      mesa: ['', Validators.required]
    });

    this._productoServicio.lista().subscribe({
      next: (data) => {
        //if(data.status){
        const lista = data as Producto[];
        this.listaProductos = lista;//.filter(p => p.esActivo == 1 && p.stock > 0);
        //}
      },
      error: (e) => { }
    })

    this._usuarioServicio.lista().subscribe({
      next: (data) => {
        //if(data.status) 
        this.listaMeseros = data.filter(p => p.idRol == 3)
      },
      error: (e) => { }
    })

    this._mesaServicio.lista().subscribe({
      next: (data) => {
        //if(data.status) 
        this.listaMesas = data;
      },
      error: (e) => { }
    })

    this._clienteServicio.lista().subscribe({
      next: (data) => {
        //if(data.status) 
        this.listaClientes = data;
        //this.listaClientesFiltro = data;
      },
      error: (e) => { }
    })

    this.formularioProductoVenta.get('producto')?.valueChanges.subscribe(value => {
      this.listaProductosFiltro = this.retornarProductosPorFiltro(value);
    })

    this.formularioProductoVenta.get('cliente')?.valueChanges.subscribe(value => {
      this.listaClientesFiltro = this.retornarClientesPorFiltro(value);
    })
  }

  ngOnInit(): void {
    this.usuarioSesion = this._utilidadServicio.obtenerSesionUsuario();
  }

  mostrarProducto(producto: Producto): string {
    return producto.nombre;
  }

  mostrarCliente(cliente: Cliente): string {
    return cliente.cedula.toString();
  }

  productoParaVenta(event: any) {
    this.productoSeleccionado = event.option.value;
  }

  selectCliente(event: any) {
    this.clienteSeleccionado = event.option.value;
    this.review_btn = false;
  }

  selectMesero(event: any) {
    this.meseroSeleccionado = event.value;
  }

  selectMesa(event: any) {
    this.mesaSeleccionada = event.value;
  }

  nuevoCliente() {
    this.dialog.open(ModalClienteComponent, {
      disableClose: true
    }).afterClosed().subscribe(resultado => {
      if (resultado === "true")
        this._clienteServicio.lista().subscribe({
          next: (data) => {
            //if(data.status) 
            this.listaClientes = data;
          },
          error: (e) => { }
        })
    });
  }

  agregarProductoParaVenta() {

    const _cantidad: number = this.formularioProductoVenta.value.cantidad;
    const _precio: number = this.productoSeleccionado.precio;
    const _total: number = _cantidad * _precio;
    this.totalPagar = this.totalPagar + _total;

    //index: Number;

    var index = this.listaProductosParaVenta.findIndex((item) => item.idProducto === this.productoSeleccionado.idProducto);

    if (index < 0) {
      this.listaProductosParaVenta.push({
        idDetalleXFactura: 0,
        nroFactura: 0,
        idProducto: this.productoSeleccionado.idProducto,
        descripcionProducto: this.productoSeleccionado.nombre,
        precio: _precio,
        cantidad: _cantidad,
        total: _total
      })
    }
    else {
      this.listaProductosParaVenta[index].cantidad+=_cantidad;
      this.listaProductosParaVenta[index].total+=_total;
    }

    this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);

    this.formularioProductoVenta.patchValue({
      producto: '',
      cantidad: ''
    })
  }

  eliminarProducto(detalle: DetalleXFactura) {
    this.totalPagar = this.totalPagar - detalle.total,
      this.listaProductosParaVenta = this.listaProductosParaVenta.filter(p => p.idProducto != detalle.idProducto);

    this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);
  }

  registrarVenta() {

    if (this.listaProductosParaVenta.length > 0) {

      this.bloquearBotonRegistrar = true;

      const request: Factura = {
        nroFactura: 0,
        idCliente: this.clienteSeleccionado.idCliente,
        nroMesa: this.mesaSeleccionada.nroMesa,
        idMesero: this.meseroSeleccionado.idUsuario,
        idSupervisor: this.usuarioSesion.idUsuario,
        fecha: '',
        total: this.totalPagar,
        detalleXFacturas: this.listaProductosParaVenta
      }

      this._facturaServicio.registrar(request).subscribe({
        next: (response) => {
          //if(response.status){
          this.totalPagar = 0.00;
          this.listaProductosParaVenta = [];
          this.datosDetalleVenta = new MatTableDataSource(this.listaProductosParaVenta);

          Swal.fire({
            icon: 'success',
            title: 'Factura Registrada!',
            text: `Numero de Factura: ${response.nroFactura}`
          })
          //}else
          //  this._utilidadServicio.mostrarAlerta("No se pudo registrar la venta","Oops");
        },
        complete: () => {
          this.bloquearBotonRegistrar = false;
        },
        error: (e) => {
          this.bloquearBotonRegistrar = false;
        }

      })

    }
  }


}
