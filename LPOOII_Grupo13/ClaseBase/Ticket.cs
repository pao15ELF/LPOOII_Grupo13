using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Ticket
    {
        private int tick_Id;

        public int Tick_Id
        {
            get { return tick_Id; }
            set { tick_Id = value; }
        }
        private int tick_TicketNro;

        public int Tick_TicketNro
        {
            get { return tick_TicketNro; }
            set { tick_TicketNro = value; }
        }
        private DateTime tick_FechaHoraEnt;

        public DateTime Tick_FechaHoraEnt
        {
            get { return tick_FechaHoraEnt; }
            set { tick_FechaHoraEnt = value; }
        }
        private DateTime tick_FechaHoraSal;

        public DateTime Tick_FechaHoraSal
        {
            get { return tick_FechaHoraSal; }
            set { tick_FechaHoraSal = value; }
        }
        private int tick_ClienteDni;

        public int Tick_ClienteDni
        {
            get { return tick_ClienteDni; }
            set { tick_ClienteDni = value; }
        }
        private int tick_TVCodigo;

        public int Tick_TVCodigo
        {
            get { return tick_TVCodigo; }
            set { tick_TVCodigo = value; }
        }
        private string tick_Patente;

        public string Tick_Patente
        {
            get { return tick_Patente; }
            set { tick_Patente = value; }
        }
        private int tick_SectorCodigo;

        public int Tick_SectorCodigo
        {
            get { return tick_SectorCodigo; }
            set { tick_SectorCodigo = value; }
        }
        private double tick_Duracion;

        public double Tick_Duracion
        {
            get { return tick_Duracion; }
            set { tick_Duracion = value; }
        }
        private decimal tick_Tarifa;

        public decimal Tick_Tarifa
        {
            get { return tick_Tarifa; }
            set { tick_Tarifa = value; }
        }
        private decimal tick_Total;

        public decimal Tick_Total
        {
            get { return tick_Total; }
            set { tick_Total = value; }
        }
    }
}
