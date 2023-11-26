using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace ClaseBase
{
    public class Cliente:IDataErrorInfo, INotifyPropertyChanged
    {
        private int Cli_Id;
        public int Cli_Id1
        {
            get { return Cli_Id; }
            set { 
                  Cli_Id = value;
                  Notificador(Cli_Id.ToString());
                  OnPropertyChanged("Cli_Id1");
            }
        }

        private int Cli_ClienteDni;
        public int Cli_ClienteDni1
        {
            get { return Cli_ClienteDni; }
            set { 
                  Cli_ClienteDni = value;
                  Notificador(Cli_ClienteDni.ToString());
                  OnPropertyChanged("Cli_ClienteDni1");
            }
        }

        private string Cli_Apellido;
        public string Cli_Apellido1
        {
            get { return Cli_Apellido; }
            set { 
                  Cli_Apellido = value;
                  Notificador(Cli_Apellido);
                  OnPropertyChanged("Cli_Apellido1");
            }
        }

        private string Cli_Nombre;
        public string Cli_Nombre1
        {
            get { return Cli_Nombre; }
            set { 
                  Cli_Nombre = value;
                  Notificador(Cli_Nombre);
                  OnPropertyChanged("Cli_Nombre1");
            }
        }

        private string Cli_Telefono;
        public string Cli_Telefono1
        {
            get { return Cli_Telefono; }
            set { 
                  Cli_Telefono = value;
                  Notificador(Cli_Telefono);
                  OnPropertyChanged("Cli_Telefono1");
            }
        }

        private string Cli_Estado;
        public string Cli_Estado1
        {
            get { return Cli_Estado; }
            set { 
                  Cli_Estado = value;
                  Notificador(Cli_Estado);
                  OnPropertyChanged("Cli_Estado1");
            }
        }


        // Metodos del IdataErrorInfo
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get {
                string result = null;

                switch (columnName)
                {
                    case "Cli_ClienteDni1":
                        result = validar_CliClienteDni();
                        break;
                    case "Cli_Apellido1":
                        result = validar_CliApellido();
                        break;
                    case "Cli_Nombre1":
                        result = validar_CliNombre();
                        break;
                    case "Cli_Telefono1":
                        result = validar_CliTelefono();
                        break;
                }
                return result;
            }
        }

        private string validar_CliClienteDni()
        {
            if (Cli_ClienteDni1 == 0)
            {
                return "Ingrese el DNI del Cliente";
            }
            return null;
        }

        private string validar_CliApellido()
        {
            if (String.IsNullOrEmpty(Cli_Apellido1))
            { 
                return "Ingrese Apellido del Cliente"; 
            }
            return null;
        }

        private string validar_CliNombre()
        {
            if (String.IsNullOrEmpty(Cli_Nombre1))
            {
                return "Ingrese Nombre del Cliente";
            }
            return null;
        }

        private string validar_CliTelefono()
        {
            if (String.IsNullOrEmpty(Cli_Telefono1))
            {
                return "Ingrese Teléfono del Cliente";
            }
            else if (Cli_Telefono1.Length > 11)
            {
                return "Debe tener como maximo 11 digitos";
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
