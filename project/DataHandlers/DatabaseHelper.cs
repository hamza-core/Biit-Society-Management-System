using project.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.DataHandlers
{
    internal static class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            string conn = @"Data Source  = DERIK\SQLEXPRESS01; Initial Catalog = society; Integrated Security = true ";
            return new SqlConnection(conn);
        }

        public static int ExecuteInsert(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                foreach (var p in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(p.Key, p.Value);
                }

                int affectedRows = sqlCommand.ExecuteNonQuery();
                return affectedRows;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message, "Sql Error");
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int ExecuteUpdate(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int ExecuteDelete(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool ExecuteExists(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value;
            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataRow ExecuteSelectSingle(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<DataRow> ExecuteSelect(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            List<DataRow> results = new List<DataRow>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                foreach (DataRow row in dt.Rows)
                {
                    results.Add(row);
                }
                return results.Count > 0 ? results : null;
            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool InsertData(string query, Dictionary<string, object> parameters)
        {
            SqlConnection connection = GetConnection();
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                foreach (var p in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(p.Key, p.Value);
                }

                int affectedRows = sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBoxHelper.ShowInfo("Record Inserted in the DataBase");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message, "Sql Error");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Dictionary<string, object>> GetData(string query, Dictionary<string, object> parameters)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            SqlConnection connection = GetConnection();

            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                if (parameters != null)
                    foreach (var param in parameters)
                    {
                        sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    results.Add(row);

                }
                return results.Count > 0 ? results : null;

            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"Database Error  :{ex.Message}");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateData(String query, Dictionary<String, object> parameters)
        {
            SqlConnection connection = GetConnection();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                int affectedRows = command.ExecuteNonQuery();

                return affectedRows > 0 ? true : false;

            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteData(String query, Dictionary<String, object> parameters)
        {
            SqlConnection connection = GetConnection();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                int affectedRows = command.ExecuteNonQuery();

                return affectedRows > 0 ? true : false;

            }
            catch (Exception e)
            {
                MessageBoxHelper.ShowError($"Database Error : {e.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
