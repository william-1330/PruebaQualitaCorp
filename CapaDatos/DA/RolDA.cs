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
    public class RolDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public RolDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var tiendaContext = _context.Roles;
                return _mapper.Map<List<RolDTO>>(await tiendaContext.Where(x => x.IdRol != 1).ToListAsync());
            }
            catch
            {
                throw;
            }
        }
    }
}
