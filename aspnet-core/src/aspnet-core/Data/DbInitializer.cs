using aspnet_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Goods.Any())
                return;

            var goods = new Good[]
            {
                new Good { Title = "Dell XPS 13 9050", Price=1222, AmountInStock=12 },
                new Good { Title = "HP x360", Price=1111, AmountInStock=5 }
            };

            foreach (var good in goods)
                context.Goods.Add(good);
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer { FirstName = "Anna", LastName = "Alne" },
                new Customer { FirstName = "Bob", LastName = "Becker" }
            };

            foreach (var customer in customers)
                context.Customers.Add(customer);
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order { CustomerId=1, GoodId=1, Amount=2, Date=new DateTime(2017,1,1), IsPaid=false },
                new Order { CustomerId=2, GoodId=1, Amount=1, Date=new DateTime(2017,1,1), IsPaid=true },
                new Order { CustomerId=2, GoodId=2, Amount=1, Date=new DateTime(2017,1,1), IsPaid=false, DeliveryDate=new DateTime(2017,2,2) },
                new Order { CustomerId=2, GoodId=2, Amount=1, Date=new DateTime(2017,1,1), IsPaid=true, DeliveryDate=new DateTime(2017,2,2) }
            };

            foreach (var order in orders)
                context.Orders.Add(order);
            context.SaveChanges();
        }
    }
}
