using Arizona.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Arizona.Data
{
    public class ArizonaSeeder
    {
        private readonly ArizonaContext _ctx;
        private readonly IWebHostEnvironment _hosting;

        public ArizonaSeeder(ArizonaContext ctx, IWebHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/arizonaSeedData.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order() {
                    OrderDate = DateTime.Now,
                    OrderNumber = "1",
                    Id = 0,
                    Items = new List<OrderItem>() {
                        new OrderItem() {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _ctx.AddRange(order);
                _ctx.SaveChanges();
            }
        }
    }
}
