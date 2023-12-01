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
    public class TrabajarTipoVehiculo
    {

        public TipoVehiculo TraerVehiculo()
        {
            TipoVehiculo tipoVehiculo = new TipoVehiculo();
            return tipoVehiculo;
        }

        /// <summary>
        /// Retorna un DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable traerTiposVehiculos()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM TipoVehiculo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Retorna una collection
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TipoVehiculo> listarTipoVehiculo()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM TipoVehiculo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            var listaTipoVehiculo = new ObservableCollection<TipoVehiculo>();
            foreach (DataRow row in dt.Rows) 
            {
                TipoVehiculo tipoVehiculo = new TipoVehiculo();
                
                tipoVehiculo.TypV_Id = (int)row["TV_Id"];
                tipoVehiculo.TypV_TVCodigo= (int)row["TV_Codigo"];
                tipoVehiculo.TypV_Descripcion= (string)row["TV_Descripcion"];
                tipoVehiculo.TypV_Tarifa= (decimal)row["TV_Tarifa"];
                tipoVehiculo.TypV_Estado = (string)row["TV_Estado"];
                tipoVehiculo.TypV_Imagen = (string)row["TV_Imagen"];
                
                listaTipoVehiculo.Add(tipoVehiculo);
            }
            // Depuración para verificar el tipo del DataContext
            //Console.WriteLine("DataContext type: " + (listaTipoVehiculo.GetType() != null ? listaTipoVehiculo.GetType().ToString() : "null"));

            return new ObservableCollection<TipoVehiculo>(listaTipoVehiculo);
            //return listaTipoVehiculo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoVehiculo"></param>
        public static void insertar_TipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "altaTipoVehiculo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@codigo", tipoVehiculo.TypV_TVCodigo);
            cmd.Parameters.AddWithValue("@descripcion", tipoVehiculo.TypV_Descripcion);
            cmd.Parameters.AddWithValue("@tarifa", tipoVehiculo.TypV_Tarifa);
            cmd.Parameters.AddWithValue("@estado", tipoVehiculo.TypV_Estado);
            cmd.Parameters.AddWithValue("@imagen", tipoVehiculo.TypV_Imagen);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void eliminar_TipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            SqlConnection cn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "eliminarTipoVehiculo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", tipoVehiculo.TypV_Id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void modificar_TipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "modificarTipoVehiculo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@id", tipoVehiculo.TypV_Id);
            cmd.Parameters.AddWithValue("@codigo", tipoVehiculo.TypV_TVCodigo);
            cmd.Parameters.AddWithValue("@descripcion", tipoVehiculo.TypV_Descripcion);
            cmd.Parameters.AddWithValue("@tarifa", tipoVehiculo.TypV_Tarifa);
            cmd.Parameters.AddWithValue("@estado", tipoVehiculo.TypV_Estado);
            cmd.Parameters.AddWithValue("@imagen", tipoVehiculo.TypV_Imagen);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static TipoVehiculo buscarTipoVehiculoXCodigo(int codigo)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "buscarTipoVehiculoXCodigo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@codigo", codigo);
            //ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //llena los datos de la consulta en el DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count>0)
            {
                TipoVehiculo tipoVehiculo = new TipoVehiculo();
                tipoVehiculo.TypV_Descripcion = dt.Rows[0]["TV_Descripcion"].ToString();

                //Cliente cliente = new Cliente();
                //cliente.Cli_Id1 = Convert.ToInt32(dt.Rows[0]["Cli_Id"]);
                //cliente.Cli_ClienteDni1 = Convert.ToInt32(dt.Rows[0]["Cli_Dni"]);
                //cliente.Cli_Apellido1 = dt.Rows[0]["Cli_Apellido"].ToString();
                //cliente.Cli_Nombre1 = dt.Rows[0]["Cli_Nombre"].ToString();
                //cliente.Cli_Telefono1 = dt.Rows[0]["Cli_Telefono"].ToString();
                //return cliente;
                return tipoVehiculo;
            }
            else
            {
                return null;
            }
        }

    }
}
