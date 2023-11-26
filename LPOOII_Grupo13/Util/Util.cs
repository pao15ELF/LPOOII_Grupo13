using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ClaseBase;
namespace Util
{
    public static class Util
    {
        private static  Usuario usuario = new Usuario();

        public static void setUsuario(Usuario usuarioLog)
        {
            usuario = usuarioLog;
        }

        public static Usuario getUsuario()
        {
            return usuario;
        }


    }
}
