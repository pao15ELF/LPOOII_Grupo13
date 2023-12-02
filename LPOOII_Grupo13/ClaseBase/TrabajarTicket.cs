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
    public class TrabajarTicket
    {
        public static DataTable traerTicket()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Ticket";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable traerTipoVehiculoCombobox()
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

        public static DataTable traerZonaSectorDisponible()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Sector";
            cmd.CommandText += " WHERE Sec_Habilitado LIKE 'Disponible'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable buscarTarifaVehiculo(int codigo)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM TipoVehiculo";
            cmd.CommandText += " WHERE TV_Codigo = @codigo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@codigo", codigo);

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static void registrarEntradaTicket(Ticket ticket)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "registrarEntradaTicket";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@ticketNro", ticket.Tick_TicketNro);
            cmd.Parameters.AddWithValue("@fechaEntrada", ticket.Tick_FechaHoraEnt);
            cmd.Parameters.AddWithValue("@clienteDni", ticket.Tick_ClienteDni);
            cmd.Parameters.AddWithValue("@tipoVehiculoCodigo", ticket.Tick_TVCodigo);
            cmd.Parameters.AddWithValue("@patente", ticket.Tick_Patente);
            cmd.Parameters.AddWithValue("@sectorCodigo", ticket.Tick_SectorCodigo);
            cmd.Parameters.AddWithValue("@tarifa", ticket.Tick_Tarifa);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        //public static DataTable traerTicketConHoraSalNull()
        //{
        //    SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

        //    SqlCommand cmd = new SqlCommand();

        //    cmd.CommandText = "SELECT * FROM Ticket WHERE Tic_FecHoraSal IS NULL";
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = cnn;

        //    // Ejecuta la consulta
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    // Llena los datos de la consulta en la DataTable
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    return dt;
        //}

        public static ObservableCollection<Ticket> traerTicketConHoraSalNull()
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Ticket WHERE Tic_FecHoraSal IS NULL";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Ticket> listaTicket = new ObservableCollection<Ticket>();
            foreach (DataRow row in dt.Rows)
            {
                Ticket ticket = new Ticket
                {
                    Tick_Id= (int)row["Tic_Id"],
                    Tick_TicketNro= (int)row["Tic_TicketNro"],
                    Tick_FechaHoraEnt= (DateTime)row["Tic_FecHoraEnt"],
                    Tick_ClienteDni=(int)row["Tic_CliDni"],
                    Tick_TVCodigo=(int)row["Tic_TVCodigo"],
                    Tick_Patente=(string)row["Tic_Patente"],
                    Tick_SectorCodigo=(int)row["Tic_SecCodigo"],
                    Tick_Tarifa=(decimal)row["Tic_Tarifa"]
                };
                listaTicket.Add(ticket);
            }
            return listaTicket;
        }

        public static void registrarSalidaTicket(Ticket ticket)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "registrarSalidaTicket";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;


            cmd.Parameters.AddWithValue("@ticketId", ticket.Tick_Id);
            cmd.Parameters.AddWithValue("@duracion", ticket.Tick_Duracion);
            cmd.Parameters.AddWithValue("@total", ticket.Tick_Total);
            cmd.Parameters.AddWithValue("@fecYhoraSalida", ticket.Tick_FechaHoraSal);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }
         //FALTA PONER LO DE LA FECHA
        public static ObservableCollection<Ticket> traerTicketVentas(DateTime fechaInicio, DateTime fechaFinal)
        {
            SqlConnection cnn = new SqlConnection(ClaseBase.Properties.Settings.Default.playaConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Ticket ";
            cmd.CommandText += " WHERE Tic_FecHoraSal >= @FechaInicio AND Tic_FecHoraSal <= @FechaFinal";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Parámetros para las fechas
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);

            //Ejecuta la consulta
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Llena los datos de la consulta en la DateTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Ticket> listaTicket = new ObservableCollection<Ticket>();
            foreach (DataRow row in dt.Rows)
            {
                Ticket ticket = new Ticket
                {
                    Tick_Id = (int)row["Tic_Id"],
                    Tick_TicketNro = (int)row["Tic_TicketNro"],
                    Tick_FechaHoraEnt = (DateTime)row["Tic_FecHoraEnt"],
                    Tick_FechaHoraSal= (DateTime)row["Tic_FecHoraSal"],
                    Tick_ClienteDni = (int)row["Tic_CliDni"],
                    Tick_TVCodigo = (int)row["Tic_TVCodigo"],
                    Tick_Patente = (string)row["Tic_Patente"],
                    Tick_SectorCodigo = (int)row["Tic_SecCodigo"],
                    Tick_Duracion=(string)row["Tic_Duracion"],
                    Tick_Tarifa = (decimal)row["Tic_Tarifa"],
                    Tick_Total=(decimal)row["Tic_Total"]
                };
                listaTicket.Add(ticket);
            }
            return listaTicket;
        }
    }
}
