using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Zona
    {
        private int zona_cod;

        public int Zona_cod
        {
            get { return zona_cod; }
            set { zona_cod = value; }
        }
        private string zona_descripcion;

        public string Zona_descripcion
        {
            get { return zona_descripcion; }
            set { zona_descripcion = value; }
        }
        private string zona_piso;

        public string Zona_piso
        {
            get { return zona_piso; }
            set { zona_piso = value; }
        }
    }
}
