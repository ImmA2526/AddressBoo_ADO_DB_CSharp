using System;

namespace AddressBook_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************Welcome To AddRessBook DB************");
            AddressRepo Repo = new AddressRepo();
            Repo.CheckConnection();
            AddressModel Add = new AddressModel();
            Add.firstName = "Imran";
            Add.lastName = "Shaikh";
            Add.address="Dighi";
            Add.city="Pune";
            Add.state="Maha";
            Add.zip="456789";
            Add.phoneNumber="9876543212";
            Add.BookName="Family";
            Add.BookType = "Friend";
            Repo.AddContacts(Add);
            Console.WriteLine("**********Insrted Recod**********");
            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}",Add.firstName,Add.lastName,Add.address,Add.city,Add.state,Add.zip,Add.phoneNumber,Add.BookName,Add.BookType);
        }
    }
}
