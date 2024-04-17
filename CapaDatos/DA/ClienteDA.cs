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
    public class ClienteDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public ClienteDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var tiendaContext = _context.Clientes;
            return _mapper.Map<List<ClienteDTO>>(await tiendaContext.ToListAsync());
        }

        //public async Task<Cliente> Details(int? id)
        //{
        //    var Cliente = await _context.Clientes
        //        .FirstOrDefaultAsync(m => m.IdCliente == id);
        //    return Cliente;
        //}

        public async Task Crear(ClienteDTO Cliente)
        {
            _context.Add(_mapper.Map<Cliente>(Cliente));
            await _context.SaveChangesAsync();
        }

        //public async Task<Cliente> Edit(int? id)
        //{
        //    var Cliente = await _context.Clientes.FindAsync(id);
        //    return Cliente;
        //}

        public async Task<ClienteDTO> Editar(ClienteDTO cliente)
        {
            try
            {
                if (!ClienteExists(cliente.IdCliente))
                    throw new TaskCanceledException("El cliente no existe");

                var clienteModelo = _mapper.Map<Cliente>(cliente);

                _context.Update(clienteModelo);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ClienteExists(cliente.IdCliente))
                {
                    return cliente;
                }
                else
                {
                    throw;
                }
            }
        }

        //public async Task<Cliente> Delete(int? id)
        //{
        //    var Cliente = await _context.Clientes
        //        .FirstOrDefaultAsync(m => m.IdCliente == id);

        //    return Cliente;
        //}

        public async Task<bool> Eliminar(int id)
        {
            var Cliente = await _context.Clientes.FindAsync(id);
            if (Cliente != null)
            {
                _context.Clientes.Remove(Cliente);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
