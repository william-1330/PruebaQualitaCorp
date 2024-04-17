using AutoMapper;
using CapaDatos.DTO;
using CapaDatos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.DA
{
    public class ProductoDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public ProductoDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var tiendaContext = _context.Productos;
                return _mapper.Map<List<ProductoDTO>>(await tiendaContext.ToListAsync());
            }
            catch
            {
                throw;
            }
        }

        public async Task Crear(ProductoDTO producto)
        {
            _context.Add(_mapper.Map<Producto>(producto));
            await _context.SaveChangesAsync();
        }

        public async Task<ProductoDTO> Editar(ProductoDTO producto)
        {
            try
            {
                if (!ProductoExists(producto.IdProducto))
                    throw new TaskCanceledException("El producto no existe");

                var productoModelo = _mapper.Map<Producto>(producto);

                _context.Update(productoModelo);

                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception ex)
            {
                if (!ProductoExists(producto.IdProducto))
                {
                    return producto;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> Eliminar(int id) 
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
