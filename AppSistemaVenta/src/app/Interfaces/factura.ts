import { DetalleXFactura } from "./detalle-x-factura"

export interface Factura {

    nroFactura: number,
    idCliente: number,
    nroMesa: number,
    idMesero: number,
    idSupervisor: number
    fecha: string,
    total: number,

    detalleXFacturas: DetalleXFactura[]
}
