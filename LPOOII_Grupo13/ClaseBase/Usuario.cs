using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClaseBase
{
    public class Usuario: INotifyPropertyChanged
    {
        private int user_Id;
        public int User_Id
        {
            get { return user_Id; }
            set { user_Id = value; }
        }

        private string user_UserName;
        public string User_UserName
        {
            get { return user_UserName; }
            set { 
                  user_UserName = value;
                  Notificador(user_UserName);
                }
        }

        private string user_Password;
        public string User_Password
        {
            get { return user_Password; }
            set { 
                  user_Password = value;
                  Notificador(user_Password);
            }
        }

        private string user_Apellido;
        public string User_Apellido
        {
            get { return user_Apellido; }
            set { 
                  user_Apellido = value;
                  Notificador(user_Apellido);
            }
        }

        private string user_Nombre;
        public string User_Nombre
        {
            get { return user_Nombre; }
            set { 
                  user_Nombre = value;
                  Notificador(user_Nombre);
            }
        }

        private string user_Rol;
        public string User_Rol
        {
            get { return user_Rol; }
            set { 
                  user_Rol = value;
                  Notificador(user_Rol);
            }
        }

        public Usuario()
        {
        }

        public Usuario(int id, string userName, string userPassword, string userApellido, string userNombre, string userRol) {
            user_Id = id;
            user_UserName = userName;
            user_Password = userPassword;
            user_Apellido = userApellido;
            user_Nombre = userNombre;
            user_Rol = userRol;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notificador(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
