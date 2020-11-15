using Arizona.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<StoreUser> _userManager;

        public ArizonaSeeder(ArizonaContext ctx, IWebHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("test@arizona.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Test",
                    LastName = "Arizona",
                    Email = "test@arizona.com",
                    UserName = "Test.Arizona"
                };

                var result = await _userManager.CreateAsync(user, "Test@123");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("User was not created in seeder");
                }
            }

            if (!_ctx.Products.Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/arizonaSeedData.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order() {
                    User = user,
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
