using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

using ClaseBase;

namespace ClaseBase
{
    public class TrabajarCliente
    {
        public Cliente TraerCliente()
        {
            Cliente cliente = new Cliente();
            return cliente;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable listar_Clientes()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Cliente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<Cliente> listarClientes()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Cliente ";
            cmd.CommandText += " WHERE Cli_Estado LIKE 'Habilitado'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Cliente> listaCliente = new ObservableCollection<Cliente>();
            foreach (DataRow row in dt.Rows)
            {
                Cliente cliente = new Cliente
                {
                    Cli_Id1 = (int)row["Cli_Id"],
                    Cli_ClienteDni1 = (int)row["Cli_Dni"],
                    Cli_Apellido1 = (string)row["Cli_Apellido"],
                    Cli_Nombre1 = (string)row["Cli_Nombre"],
                    Cli_Telefono1 = (string)row["Cli_Telefono"],
                    Cli_Estado1 = (string)row["Cli_Estado"]
                };
                listaCliente.Add(cliente);
            }
            return listaCliente;
        }

        /// <summary>
        /// Alta decliente
        /// </summary>
        /// <param name="tipoVehiculo"></param>
        public static void insertar_Cliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "altaCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@dni", cliente.Cli_ClienteDni1);
            cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido1);
            cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre1);
            cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono1);
            cmd.Parameters.AddWithValue("@estado", cliente.Cli_Estado1);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void modificar_Cliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@id", cliente.Cli_Id1);
            cmd.Parameters.AddWithValue("@dni", cliente.Cli_ClienteDni1);
            cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido1);
            cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre1);
            cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono1);
            cmd.Parameters.AddWithValue("@estado", cliente.Cli_Estado1);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        /// <summary>
        /// Baja logica de un cliente
        /// </summary>
        /// <param name="cliente"></param>
        public static void eliminar_Cliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@id", cliente.Cli_Id1);
            //cmd.Parameters.AddWithValue("@dni", cliente.Cli_ClienteDni1);
            //cmd.Parameters.AddWithValue("@apellido", cliente.Cli_Apellido1);
            //cmd.Parameters.AddWithValue("@nombre", cliente.Cli_Nombre1);
            //cmd.Parameters.AddWithValue("@telefono", cliente.Cli_Telefono1);
            cmd.Parameters.AddWithValue("@estado", cliente.Cli_Estado1);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
