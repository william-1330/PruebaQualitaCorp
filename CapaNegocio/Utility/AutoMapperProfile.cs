using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using CapaDatos.DTO;
using CapaDatos.Entidades;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;


namespace CapaNegocio.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion  Rol

            //#region Menu
            //CreateMap<Menu, MenuDTO>().ReverseMap();
            //#endregion  Menu

            #region Usuario

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.Rol.Nombre)
                );
            //.ForMember(destino =>
            //    destino.EsActivo,
            //    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
            //);

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.NombreCompleto,
                    opt => opt.MapFrom(origen => origen.Nombres + origen.Nombres)
                )
                .ForMember(destino =>
                    destino.RolDescripcion,
                    opt => opt.MapFrom(origen => origen.Rol.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                    destino.Rol,
                    opt => opt.Ignore()
                );
            //  .ForMember(destino =>
            //    destino.EsActivo,
            //    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            //);

            #endregion  Usuario

            //#region Categoria
            //CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            //#endregion  Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>();
            //.ForMember(destino =>
            //    destino.DescripcionCategoria,
            //    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
            //)
            //.ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //)
            //.ForMember(destino =>
            //    destino.EsActivo,
            //    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
            //);

            CreateMap<ProductoDTO, Producto>()
               .ForMember(destino =>
                   destino.Fecha,
                   opt => opt.Ignore()
               );
            //.ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => origen.Precio)
            //);
            //.ForMember(destino =>
            //    destino.EsActivo,
            //    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            //);

            #endregion  Producto

            #region Mesa
            CreateMap<Mesa, MesaDTO>().ReverseMap();
            #endregion  Mesa

            #region Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            #endregion  Cliente

            #region Factura
            CreateMap<Factura, FacturaDTO>();
            // .ForMember(destino =>
            //    destino.TotalTexto,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            //)
            // .ForMember(destino =>
            //    destino.FechaRegistro,
            //    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            //);

            CreateMap<FacturaDTO, Factura>()
                .ForMember(destino =>
                      destino.Cliente,
                      opt => opt.Ignore()
                )
                .ForMember(destino =>
                      destino.Mesa,
                      opt => opt.Ignore()
                )
                .ForMember(destino =>
                      destino.Mesero,
                      opt => opt.Ignore()
                )
                .ForMember(destino =>
                      destino.Supervisor,
                      opt => opt.Ignore()
                )
                .ForMember(destino =>
                      destino.Fecha,
                      opt => opt.MapFrom(origen => origen.Fecha == "" ? DateTime.Now : DateTime.ParseExact(origen.Fecha, "dd/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture))
                )
                ;
            //.ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
            //);
            #endregion  Factura

            #region DetalleXFactura
            CreateMap<DetalleXFactura, DetalleXFacturaDTO>()
                .ForMember(destino =>
                    destino.DescripcionProducto,
                    opt => opt.MapFrom(origen => origen.Producto.Nombre)
                );
            //.ForMember(destino =>
            //    destino.PrecioTexto,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //)
            // .ForMember(destino =>
            //    destino.TotalTexto,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            //);


            CreateMap<DetalleXFacturaDTO, DetalleXFactura>()
                .ForMember(destino =>
                    destino.Factura,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Producto,
                    opt => opt.Ignore()
                );
            //  .ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-PE")))
            //).ForMember(destino =>
            //    destino.Total,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
            //);

            #endregion  DetalleXFactura

            //#region Reporte
            //CreateMap<DetalleVenta, ReporteDTO>()
            //     .ForMember(destino =>
            //        destino.FechaRegistro,
            //        opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            //    )
            //     .ForMember(destino =>
            //        destino.NumeroDocumento,
            //        opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
            //    )
            //     .ForMember(destino =>
            //        destino.TipoPago,
            //        opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
            //    )
            //     .ForMember(destino =>
            //        destino.TotalVenta,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE")))
            //    )
            //     .ForMember(destino =>
            //        destino.Producto,
            //        opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
            //    ).ForMember(destino =>
            //        destino.Precio,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //    )
            //    .ForMember(destino =>
            //        destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            //    );
            //#endregion  Reporte


        }
    }
}
