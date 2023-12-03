using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

using ClaseBase;
namespace ClaseBase
{
    public class TrabajarUsuario
    {
        /// <summary>
        /// Metodo para traer todos los usuarios.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Usuario> TraerUsuarios()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Usuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Usuario> listaUsuario = new ObservableCollection<Usuario>();

            foreach (DataRow row in dt.Rows)
            {
                Usuario usuario = new Usuario
                {
                    User_Id=(int)row["Usu_Id"],
                    User_UserName=(string)row["Usu_UserName"],
                    User_Password=(string)row["Usu_Password"],
                    User_Apellido=(string)row["Usu_Apellido"],
                    User_Nombre=(string)row["Usu_Nombre"],
                    User_Rol=(string)row["Usu_Rol"],
                };

                listaUsuario.Add(usuario);
            }
            return listaUsuario;
        }

        /// <summary>
        /// Metodo para dar de alta un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        public static void insertar_Usuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "altaUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@apellido", usuario.User_Apellido);
            cmd.Parameters.AddWithValue("@nombre", usuario.User_Nombre);
            cmd.Parameters.AddWithValue("@usuario", usuario.User_UserName);
            cmd.Parameters.AddWithValue("@password", usuario.User_Password);
            cmd.Parameters.AddWithValue("@rol", usuario.User_Rol);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        /// <summary>
        /// Metodo para modificar un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        public static void modificar_Usuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@id", usuario.User_Id);
            cmd.Parameters.AddWithValue("@apellido", usuario.User_Apellido);
            cmd.Parameters.AddWithValue("@nombre", usuario.User_Nombre);
            cmd.Parameters.AddWithValue("@usuario", usuario.User_UserName);
            cmd.Parameters.AddWithValue("@password", usuario.User_Password);
            cmd.Parameters.AddWithValue("@rol", usuario.User_Rol);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        /// <summary>
        /// Metodo para eliminar un usuario
        /// </summary>
        /// <param name="id"></param>
        public static void eliminar_Usuario(int id)
        {
            SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        /// <summary>
        /// Metodo para listar los usuario filtrados.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Usuario> ListarUsuariosGrila()
        {

            SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "listaUsuariosSP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            DataTable oTablaUsuarios = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(oTablaUsuarios);


            var oListaUsuarios = new ObservableCollection<Usuario>();
            foreach (DataRow row in oTablaUsuarios.Rows)
            {
                Usuario lUsuario = new Usuario();

                lUsuario.User_Id = (int)row["Usu_Id"];
                lUsuario.User_UserName = (string)row["Usu_UserName"];
                lUsuario.User_Password = (string)row["Usu_Password"];
                lUsuario.User_Apellido = (string)row["Usu_Apellido"];
                lUsuario.User_Nombre = (string)row["Usu_Nombre"];
                lUsuario.User_Rol = (string)row["Usu_Rol"];

                oListaUsuarios.Add(lUsuario);
            }
            return oListaUsuarios;

        }

        /// <summary>
        /// Metodo para buscar un usuario por su nombre de usuario y contraseña
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static Usuario buscarUsuario(Usuario usuario)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Usuario ";
            cmd.CommandText += " WHERE @usuario LIKE Usu_UserName AND @password LIKE Usu_Password";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@usuario", usuario.User_UserName);
            cmd.Parameters.AddWithValue("@password", usuario.User_Password);

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Usuario usu = new Usuario();

                usu.User_Id = Convert.ToInt32(dt.Rows[0]["Usu_Id"]);
                usu.User_UserName = dt.Rows[0]["Usu_UserName"].ToString();
                usu.User_Password = dt.Rows[0]["Usu_Password"].ToString();
                usu.User_Apellido = dt.Rows[0]["Usu_Apellido"].ToString();
                usu.User_Nombre = dt.Rows[0]["Usu_Nombre"].ToString();
                usu.User_Rol = dt.Rows[0]["Usu_Rol"].ToString();
                return usu;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para buscar un usuario solo por nombre de usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Usuario</returns>
        public static Usuario buscarUsuarioXUserName(string nombreUsu)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Usuario ";
            cmd.CommandText += " WHERE @usuario LIKE Usu_UserName";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@usuario", nombreUsu);

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Usuario usu = new Usuario();

                usu.User_Id = Convert.ToInt32(dt.Rows[0]["Usu_Id"]);
                usu.User_UserName = dt.Rows[0]["Usu_UserName"].ToString();
                usu.User_Password = dt.Rows[0]["Usu_Password"].ToString();
                usu.User_Apellido = dt.Rows[0]["Usu_Apellido"].ToString();
                usu.User_Nombre = dt.Rows[0]["Usu_Nombre"].ToString();
                usu.User_Rol = dt.Rows[0]["Usu_Rol"].ToString();
                return usu;
            }
            else
            {
                return null;
            }
        }
    }
}
