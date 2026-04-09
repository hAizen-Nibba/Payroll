using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public static class DbConnection
{
    // Change this based on your MySQL setup
    public static string ConnectionString = "server=localhost;userid=root;password=;database=your_database_name;";
    public static MySqlConnection Conn = new MySqlConnection(ConnectionString);

    public static void OpenConnection()
    {
        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to open database connection: " + ex.Message,
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void CloseConnection()
    {
        try
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed to close database connection: " + ex.Message,
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}