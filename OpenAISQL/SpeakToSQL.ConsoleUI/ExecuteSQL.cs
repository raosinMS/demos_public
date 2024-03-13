using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToSQL.ConsoleUI
{
    internal static class ExecuteSQL
    {
        public static async Task<string> GetResult(string sql)
        {
            try
            {
                using SqlConnection connection = new SqlConnection("Server=tcp:YOURSERVERNAME.database.windows.net,1433;Initial Catalog=Nortwindsales;Persist Security Info=False;User ID=YOURUSER;Password=YOURPASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                using SqlCommand command = new SqlCommand(sql, connection);
                await connection.OpenAsync();

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                StringBuilder result = new StringBuilder();

                while (reader.Read())
                {
                    result.AppendLine(string.Join(", ", Enumerable.Range(0, reader.FieldCount).Select(i => reader[i])));
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                return "FAILED: " + ex.Message;
            }
        }
    }
}