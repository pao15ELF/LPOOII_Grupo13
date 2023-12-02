using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class Ventas
    {
        private int nTicket;

        public int NTicket
        {
            get { return nTicket; }
            set { nTicket = value; }
        }

        private int zona;

        public int Zona
        {
            get { return zona; }
            set { zona = value; }
        }
        private int sector;

        public int Sector
        {
            get { return sector; }
            set { sector = value; }
        }
        private DateTime fecHoraEntrada;

        public DateTime FecHoraEntrada
        {
            get { return fecHoraEntrada; }
            set { fecHoraEntrada = value; }
        }

        private DateTime fechaHoraSalida;

        public DateTime FechaHoraSalida
        {
            get { return fechaHoraSalida; }
            set { fechaHoraSalida = value; }
        }

        private string cliente;

        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        private string tipoVehiculo;

        public string TipoVehiculo
        {
            get { return tipoVehiculo; }
            set { tipoVehiculo = value; }
        }
        private string patente;

        public string Patente
        {
            get { return patente; }
            set { patente = value; }
        }
        private string duracion;

        public string Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }

        private decimal tarifa;

        public decimal Tarifa
        {
            get { return tarifa; }
            set { tarifa = value; }
        }

        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public Ventas() { }

    }
}
