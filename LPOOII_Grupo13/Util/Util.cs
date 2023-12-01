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

        //Para guardar informacion de los ticket de entrada
        private static Ticket ticketEntrada = new Ticket();

        public static void setTicketEntrada(Ticket ticketParaEntrada)
        {
            ticketEntrada = ticketParaEntrada;
        }

        public static Ticket getTicketEntrada() 
        {
            return ticketEntrada;
        }

        //Para guardar informacion de los ticket de salida
        private static Ticket ticketSalida = new Ticket();

        public static void setTicketSalida(Ticket ticketParaSalir)
        {
            ticketSalida = ticketParaSalir;
        }

        public static Ticket getTicketSalida()
        {
            return ticketSalida;
        }
    }
}
