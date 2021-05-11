using Microsoft.Extensions.Configuration;
using System;
using WebStore.Clients.Products;

namespace WebStore_Console_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var product_client = new ProductsClient(config);

            Console.WriteLine("К запросу готов!");

            Console.ReadKey();

            foreach(var item in product_client.GetProducts().Products)
            {
                Console.WriteLine("{0} - {1}", item.Name, item.Price);
            }

            Console.ReadKey();
        }
    }
}
