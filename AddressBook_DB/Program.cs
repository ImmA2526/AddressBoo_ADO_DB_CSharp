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
        }
    }
}
