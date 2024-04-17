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
    public class UsuarioDA
    {
        private readonly TiendaContext _context;
        private readonly IMapper _mapper;

        public UsuarioDA(TiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            var queryUsuario = await _context.Usuarios.Include(rol => rol.Rol).FirstOrDefaultAsync(m => m.Correo == correo && m.Clave == clave);

            if (queryUsuario == null)
                throw new TaskCanceledException("El usuario no existe");

            return _mapper.Map<SesionDTO>(queryUsuario);
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            var tiendaContext = _context.Usuarios.Include(rol => rol.Rol);
            return _mapper.Map<List<UsuarioDTO>>(await tiendaContext.Where(x => x.IdRol != 1).ToListAsync());
        }

        //public async Task<UsuarioDTO> Details(int? id)
        //{
        //    var Usuario = await _context.Usuarios
        //        .FirstOrDefaultAsync(m => m.IdUsuario == id);
        //    return Usuario;
        //}

        public async Task Crear(UsuarioDTO Usuario)
        {
            _context.Add(_mapper.Map<Usuario>(Usuario));
            await _context.SaveChangesAsync();
        }

        //public async Task<Usuario> Edit(int? id)
        //{
        //    var Usuario = await _context.Usuarios.FindAsync(id);
        //    return Usuario;
        //}

        public async Task<UsuarioDTO> Editar(UsuarioDTO Usuario)
        {
            try
            {
                if (!UsuarioExists(Usuario.IdUsuario))
                    throw new TaskCanceledException("El usuario no existe");

                var usuarioModelo = _mapper.Map<Usuario>(Usuario);

                _context.Update(usuarioModelo);

                await _context.SaveChangesAsync();
                return Usuario;
            }
            catch (Exception ex)
            {
                if (!UsuarioExists(Usuario.IdUsuario))
                {
                    return Usuario;
                }
                else
                {
                    throw;
                }
            }
        }

        //public async Task<UsuarioDTO> Delete(int? id)
        //{
        //    var Usuario = await _context.Usuarios
        //        .FirstOrDefaultAsync(m => m.IdUsuario == id);

        //    return Usuario;
        //}

        public async Task<bool> Eliminar(int id)
        {
            var Usuario = await _context.Usuarios.FindAsync(id);
            if (Usuario != null)
            {
                _context.Usuarios.Remove(Usuario);
            }
            if (await _context.SaveChangesAsync() <= 0)
                return false;

            return true;
        }

        public bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
