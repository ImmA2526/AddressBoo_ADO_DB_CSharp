using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace AddressBook_DB
{
    class AddressRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook;Integrated Security=True";
        SqlConnection Connection = new SqlConnection(connectionString);

        /// <summary>
        /// UC1 Check Connection
        /// </summary>
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

        /// <summary>
        /// UC 2 Add Record
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool AddContacts(AddressModel Model)
        {
            try
            {
                using (this.Connection)
                {
                    SqlCommand CMD = new SqlCommand("SpAdd_Address", this.Connection);
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@firstName", Model.firstName);
                    CMD.Parameters.AddWithValue("@lastName", Model.lastName);
                    CMD.Parameters.AddWithValue("@address", Model.address);
                    CMD.Parameters.AddWithValue("@city", Model.city);
                    CMD.Parameters.AddWithValue("@state", Model.state);
                    CMD.Parameters.AddWithValue("@zip", Model.zip);
                    CMD.Parameters.AddWithValue("@phoneNumber", Model.phoneNumber);
                    CMD.Parameters.AddWithValue("@BookName", Model.BookType);
                    CMD.Parameters.AddWithValue("@BookType", Model.BookName);
                    this.Connection.Open();
                    var result = CMD.ExecuteNonQuery();
                    this.Connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

