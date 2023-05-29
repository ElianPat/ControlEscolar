using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiciosLinqEscolar.Modelo
{
    public static class UsuarioDAO
    {
        public static List<usuario> obtenerUsuarios()
        {
            DataClassesEscolarUVDataContext conexionBD = getConnection();

            IQueryable<usuario> usuariosBD = from usuarioQuery in conexionBD.usuario
                                             select usuarioQuery;
            return usuariosBD.ToList();

        }

        public static Mensaje iniciarSesion(string username, string password)
        {

            DataClassesEscolarUVDataContext conexionBD = getConnection();

            var usuarioSesion = (from usuario in conexionBD.usuario
                                 where usuario.username == username
                                 && usuario.password == password
                                 select usuario).FirstOrDefault();

            if(usuarioSesion != null)
            {
                Mensaje msj = new Mensaje()
                {
                    error = false,
                    mensaje = "Usuario encontrado",
                    usuarioLogin = usuarioSesion
                    
                };
                return msj;
            }
            else
            {
                Mensaje msj = new Mensaje()
                {
                    error = true,
                    mensaje = "Usuario no encontrado",
                    usuarioLogin = null
                };
                return msj;
            }

            


            /* IQueryable<usuario> usuariosBD = from usuarioQuery in conexionBD.usuario
                                                 where usuarioQuery.username == username 
                                                 && usuarioQuery.password == password
                                                 select usuarioQuery;
            if (usuariosBD.ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
*/



        }

        public static Boolean guardarUsuario(usuario usuarioNuevo)
        {
            try
            {
                DataClassesEscolarUVDataContext conexionBD = getConnection();

                var usuario = new usuario()
                {
                    nombre = usuarioNuevo.nombre,
                    apellidoPaterno = usuarioNuevo.apellidoPaterno,
                    apellidoMaterno = usuarioNuevo.apellidoPaterno,
                    username = usuarioNuevo.username,
                    password = usuarioNuevo.password
                };
                conexionBD.usuario.InsertOnSubmit(usuario);
                conexionBD.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean editarUsuario(usuario usuarioEdicion)
        {
            try
            {
                DataClassesEscolarUVDataContext conexionBD = getConnection();

                var usuario = (from UsuarioEdicion in conexionBD.usuario
                               where UsuarioEdicion.idUsuario == usuarioEdicion.idUsuario
                               select UsuarioEdicion).FirstOrDefault();

                if (usuario != null)
                {
                    usuario.nombre = usuarioEdicion.nombre;
                    usuario.apellidoMaterno = usuarioEdicion.apellidoMaterno;
                    usuario.apellidoPaterno = usuarioEdicion.apellidoPaterno;
                    usuario.password = usuarioEdicion.password;
                    conexionBD.SubmitChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean eliminarUsuario(int idUsuario)
        {
            try
            {
                DataClassesEscolarUVDataContext conexionBD = getConnection();

                usuario usuarioEliminar = (from usuario in conexionBD.usuario
                                           where usuario.idUsuario == idUsuario
                                           select usuario).FirstOrDefault();
                if (usuarioEliminar != null)
                {
                    conexionBD.usuario.DeleteOnSubmit(usuarioEliminar);
                    conexionBD.SubmitChanges();
                    return true;
                }
                else { return false; }

            }catch (Exception ex) { return false; }
            

        }

        public static DataClassesEscolarUVDataContext getConnection()
        {                     
            return new DataClassesEscolarUVDataContext(global::System.Configuration.ConfigurationManager.
                                ConnectionStrings["escolaruvConnectionString"].ConnectionString);
        }
    }
}