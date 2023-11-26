using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace ClaseBase
{
    public class TipoVehiculo : IDataErrorInfo, INotifyPropertyChanged
    {
        private int typV_Id;

        public int TypV_Id
        {
            get { return typV_Id; }
            set { typV_Id = value;
                  OnPropertyChanged("TypV_Id");
            }
        }

        private int typV_TVCodigo;
        public int TypV_TVCodigo
        {
            get { return typV_TVCodigo; }
            set { 
                  typV_TVCodigo = value;
                  Notificador(typV_TVCodigo.ToString());
                  OnPropertyChanged("TypV_TVCodigo");
            }
        }

        private string typV_Descripcion;
        public string TypV_Descripcion
        {
            get { return typV_Descripcion; }
            set { 
                  typV_Descripcion = value;
                  Notificador(typV_Descripcion);
                  OnPropertyChanged("TypV_Descripcion");
            }
        }

        private decimal typV_Tarifa;
        public decimal TypV_Tarifa
        {
            get { return typV_Tarifa; }
            set { 
                  typV_Tarifa = value;
                  Notificador(typV_Tarifa.ToString());
                  OnPropertyChanged("TypV_Tarifa");
            }
        }

        private string typV_Estado;
        public string TypV_Estado
        {
            get { return typV_Estado; }
            set { 
                  typV_Estado = value;
                  Notificador(typV_Estado);
                  OnPropertyChanged("TypV_Estado");
            }
        }


        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "TypV_TVCodigo":
                        result = validar_TVCodigo();
                        break;
                    case "TypV_Descripcion":
                        result = validar_TVDescripcion();
                        break;
                    case "TypV_Tarifa":
                        result = validar_TVTarifa();
                        break;
                }
                return result;
            }
        }

        private string validar_TVCodigo()
        {
            if (TypV_TVCodigo == 0)
            {
                return "Debe ingresar el codigo del vehiculo";
            }
            return null;
        }

        private string validar_TVDescripcion()
        {
            if (String.IsNullOrEmpty(TypV_Descripcion))
            {
                return "Ingrese la descripción del vehiculo";
            }
            return null;
        }

        private string validar_TVTarifa()
        {
            if (TypV_Tarifa == 0)
            {
                return "Debe ingresar la tarifa del vehiculo";
            }
            return null;
        }

        //---------------IDENTIFICADOR-----------------
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notificador(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
