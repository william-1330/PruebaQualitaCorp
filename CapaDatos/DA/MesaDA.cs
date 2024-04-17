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
    public class MesaDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public MesaDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MesaDTO>> Lista()
        {
            try
            {
                var tiendaContext = _context.Mesas;
                return _mapper.Map<List<MesaDTO>>(await tiendaContext.ToListAsync());
            }
            catch
            {
                throw;
            }
        }

        public async Task Crear(MesaDTO mesa)
        {
            _context.Add(_mapper.Map<Mesa>(mesa));
            await _context.SaveChangesAsync();
        }

        public async Task<MesaDTO> Editar(MesaDTO mesa)
        {
            try
            {
                if (!MesaExists(mesa.NroMesa))
                    throw new TaskCanceledException("El mesa no existe");

                var mesaModelo = _mapper.Map<Mesa>(mesa);

                _context.Update(mesaModelo);

                await _context.SaveChangesAsync();
                return mesa;
            }
            catch (Exception ex)
            {
                if (!MesaExists(mesa.NroMesa))
                {
                    return mesa;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa != null)
            {
                _context.Mesas.Remove(mesa);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.NroMesa == id);
        }
    }
}
