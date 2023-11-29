using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaseBase
{
    public class Sector
    {
        private int sec_Id;

        public int Sec_Id
        {
            get { return sec_Id; }
            set { sec_Id = value; }
        }
        private int sec_Codigo;

        public int Sec_Codigo
        {
            get { return sec_Codigo; }
            set { sec_Codigo = value; }
        }
        private string sec_Descripcion;

        public string Sec_Descripcion
        {
            get { return sec_Descripcion; }
            set { sec_Descripcion = value; }
        }
        private string sec_Identificador;

        public string Sec_Identificador
        {
            get { return sec_Identificador; }
            set { sec_Identificador = value; }
        }
        private string sec_Habilitado;

        public string Sec_Habilitado
        {
            get { return sec_Habilitado; }
            set { sec_Habilitado = value; }
        }
    }
}
