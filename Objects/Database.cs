using System.Data;
using System.Data.SqlClient;

namespace OnlineStore.Objects
{
    public class DB
    {
        public static SqlConnection Connection()
        {
            SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }

        public static void CloseSqlConnection(SqlConnection conn, SqlDataReader rdr = null)
        // This function will close a SqlConnection and reader opened with SqlConnection and SqlDataReader
        {
            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void Delete(int passedId, string baseTableName, string baseTableNameSingular, params string[] tableList)
        // This function will delete an item with id of passedId from a table baseTableName, and additionally delete it from tableList other tables where the foreign key follows the format baseTableNameSingular_id
        {
            string targetId = passedId.ToString();
            string sqlCommandString = string.Format("DELETE FROM {0} WHERE id = {1}; ", baseTableName, targetId);
            foreach(string table in tableList)
            {
                sqlCommandString += string.Format("DELETE FROM {0} WHERE {1}_id = {2}", table, baseTableNameSingular, targetId);
            }
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqlCommandString, conn);

            cmd.ExecuteNonQuery();
            CloseSqlConnection(conn);
        }

        public static void DeleteAll(string tableName)
        // This function will delete all values in a table tableName
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            string command = string.Format("DELETE FROM {0};", tableName);
            SqlCommand cmd = new SqlCommand(command, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
