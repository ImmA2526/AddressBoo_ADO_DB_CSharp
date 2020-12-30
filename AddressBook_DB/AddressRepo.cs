using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace AddressBook_DB
{
    class AddressRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True";
        SqlConnection Connection = new SqlConnection(connectionString);

        public void CheckConnection()
        {
            try
            {
                this.Connection.Open();
                Console.WriteLine("Connection Established");
                this.Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}

