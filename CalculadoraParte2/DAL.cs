using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;

namespace CalculadoraParte2
{
    class DAL
    {
        public static string connectionString = "Server=DESKTOP-N3O1BMO\\SQLEXPRESS;Database=Calculadora2;Integrated Security=true";
        SqlConnection sqlConn = new SqlConnection(connectionString);

        public static DataTable ExecuteDataTable(string cmdText,CommandType type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = type;
                   
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        public static void Salvar(string Anuncio, string NomeCliente,string DataInicio,string DataFim,string Investimento)
        {
            
            using (var conn = new SqlConnection(connectionString))
            {
                string Insert = @"INSERT INTO [dbo].[Anuncios]
                               ([NomeAnuncio]
                               ,[NomeCliente]
                               ,[DataInicio]
                               ,[DataFim]
                               ,[Investimento])
                         VALUES
                               (@Anuncio
                               ,@NomeCliente
                               ,@DataInicio
                               ,@DataFim
                               ,@Investimento)";
                SqlCommand cmd = new SqlCommand(Insert, conn);

                cmd.Parameters.AddWithValue("@Anuncio", Anuncio);
                cmd.Parameters.AddWithValue("@NomeCliente", NomeCliente);
                cmd.Parameters.AddWithValue("@DataInicio", DataInicio);
                cmd.Parameters.AddWithValue("@DataFim", DataFim);
                cmd.Parameters.AddWithValue("@Investimento", Investimento);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static DataTable ExecuteDataTable(string connectionString, string cmdText,
CommandType type)
        {
            return ExecuteDataTable(cmdText, type);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string cmdText,
CommandType type, SqlParameter[] prms)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = type;

                if (prms != null)
                {
                    foreach (SqlParameter p in prms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public static SqlDataReader ExecuteReader(string connectionString, string cmdText,
CommandType type)
        {
            return ExecuteReader(connectionString, cmdText, type, null);
        }

        public static int ExecuteNonQuery(string connectionString, string cmdText, CommandType type,
SqlParameter[] prms)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = type;

                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static int ExecuteNonQuery(string connectionString, string cmdText, CommandType type)
        {
            return ExecuteNonQuery(connectionString, cmdText, type, null);
        }

        public static object ExecuteScalar(string connectionString, string cmdText, CommandType type,
 SqlParameter[] prms)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = type;
                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static object ExecuteScalar(string connectionString, string cmdText, CommandType type)
        {
            return ExecuteScalar(connectionString, cmdText, type, null);
        }

    }
}
