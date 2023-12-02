using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class SectoresOcupados
    {
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
        private string tiempoTranscurrido;

        public string TiempoTranscurrido
        {
            get { return tiempoTranscurrido; }
            set { tiempoTranscurrido = value; }
        }

        public SectoresOcupados()
        {
        }
    }
}
