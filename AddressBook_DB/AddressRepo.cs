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
        /// UC 2 Add Record UC 9: Refactor
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

                    SqlCommand CMD2 = new SqlCommand("SpAdd_Address", this.Connection);
                    CMD2.CommandType = CommandType.StoredProcedure;
                    CMD2.Parameters.AddWithValue("@firstName", Model.firstName);
                    CMD2.Parameters.AddWithValue("@lastName", Model.lastName);
                    CMD2.Parameters.AddWithValue("@address", Model.address);
                    CMD2.Parameters.AddWithValue("@city", Model.city);
                    CMD2.Parameters.AddWithValue("@state", Model.state);
                    CMD2.Parameters.AddWithValue("@zip", Model.zip);
                    CMD2.Parameters.AddWithValue("@phoneNumber", Model.phoneNumber);
                    CMD2.Parameters.AddWithValue("@BookName", Model.BookType);
                    CMD2.Parameters.AddWithValue("@BookType", Model.BookName);
                    this.Connection.Open();
                    var result2 = CMD2.ExecuteNonQuery();
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

        /// <summary>
        /// U3 Edit Contact 
        /// </summary>
        /// <param name="Model"></param>
        public void EditRecordUsingName(AddressModel Model)
        {
            try
            {
                using (this.Connection)
                {
                    string editQuery = @"Update AddressBook set lastName= @lastName, address = @address,city = @city, state = @state, zip=@zip,phoneNumber=@phoneNumber ,BookName = @BookName, BookType = @BookType WHERE firstName = @firstName;";
                    SqlCommand CMD = new SqlCommand(editQuery, this.Connection);
                    CMD.Parameters.AddWithValue("@firstName", Model.firstName);
                    CMD.Parameters.AddWithValue("@lastName", Model.lastName);
                    CMD.Parameters.AddWithValue("@address", Model.address);
                    CMD.Parameters.AddWithValue("@city", Model.city);
                    CMD.Parameters.AddWithValue("@state", Model.state);
                    CMD.Parameters.AddWithValue("@zip", Model.zip);
                    CMD.Parameters.AddWithValue("@phoneNumber", Model.phoneNumber);
                    CMD.Parameters.AddWithValue("@BookName", Model.BookName);
                    CMD.Parameters.AddWithValue("@BookType", Model.BookType);
                    this.Connection.Open();
                    var result = CMD.ExecuteNonQuery();
                    Console.WriteLine("Updated Success......");
                    this.Connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// U4 Delete Contact
        /// </summary>
        /// <param name="Model"></param>
        public void DeleteContact(AddressModel Model)
        {
            try
            {
                using (this.Connection)
                {
                    SqlCommand CMD = new SqlCommand("SpDelet_Address", this.Connection);
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@firstName", Model.firstName);
                    this.Connection.Open();
                    CMD.ExecuteNonQuery();
                    Console.WriteLine("Contact Deleted Success...");
                    this.Connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// UC 5 Retrives the record.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void RetriveRecord()
        {
            try
            {
                AddressModel Fetch = new AddressModel();
                using (this.Connection)
                {
                    using (SqlCommand fetch = new SqlCommand(@"Select * from AddressBook WHERE city='pune' OR state='Maha';", this.Connection))
                    {
                        this.Connection.Open();
                        using (SqlDataReader reader = fetch.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Fetch.firstName = reader.GetString(0);
                                Fetch.lastName = reader.GetString(1);
                                Fetch.address = reader.GetString(2);
                                Fetch.city = reader.GetString(3);
                                Fetch.state = reader.GetString(4);
                                Fetch.zip = reader.GetString(5);
                                Fetch.phoneNumber = reader.GetString(6);
                                Fetch.BookName = reader.GetString(7);
                                Fetch.BookType = reader.GetString(8);
                                Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", Fetch.firstName, Fetch.lastName, Fetch.address, Fetch.city, Fetch.state, Fetch.zip, Fetch.phoneNumber, Fetch.BookName, Fetch.BookType);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// UC 6 Count By City S
        /// </summary>
        public void CountByCityState()
        {
            try
            {
                using (this.Connection)
                {
                    using (SqlCommand CMD = new SqlCommand(@"select COUNT(firstName) from AddressBook WHERE city='Pune' AND  state='maha';", this.Connection))
                    {
                        this.Connection.Open();
                        using (SqlDataReader reader = CMD.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var counts = reader.GetInt32(0);
                                Console.WriteLine("number of person belongs City'Pune' and state 'Maharashtra':{0} ", counts);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// UC 7 Sort By name
        /// </summary>
        public void SortRecord()
        {
            try
            {
                AddressModel sorting = new AddressModel();
                using (this.Connection)
                {
                    using (SqlCommand sorts = new SqlCommand(@"Select * from AddressBook WHERE city='pune' ORDER By firstName;", this.Connection))
                    {
                        this.Connection.Open();
                        using (SqlDataReader reader = sorts.ExecuteReader())
                        {
                            Console.WriteLine("*********Sorted List******");
                            while (reader.Read())
                            {
                                sorting.firstName = reader.GetString(0);
                                sorting.lastName = reader.GetString(1);
                                sorting.address = reader.GetString(2);
                                sorting.city = reader.GetString(3);
                                sorting.state = reader.GetString(4);
                                sorting.zip = reader.GetString(5);
                                sorting.phoneNumber = reader.GetString(6);
                                sorting.BookName = reader.GetString(7);
                                sorting.BookType = reader.GetString(8);
                                Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", sorting.firstName, sorting.lastName, sorting.address, sorting.city, sorting.state, sorting.zip, sorting.phoneNumber, sorting.BookName, sorting.BookType);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// UC 8 Count By Type
        /// </summary>
        public void CountByPerson()
        {
            try
            {
                using (this.Connection)
                {
                    using (SqlCommand CMD = new SqlCommand(@"select COUNT(firstName) from AddressBook WHERE BookType='Friends'; select COUNT(firstName) from AddressBook WHERE BookType='Family'; ", this.Connection))
                    {
                        this.Connection.Open();
                        using (SqlDataReader reader = CMD.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var counts = reader.GetInt32(0);
                                Console.WriteLine("number of person belongs AddressType 'Family' ':{0} ", counts);
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var countPerson = reader.GetInt32(0);
                                    Console.WriteLine("number of person belongs AddressType 'Friends' ':{0} ", countPerson);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
