using Arizona.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Data
{
    public class ArizonaRepository : IArizonaRepository
    {
        private readonly ArizonaContext _ctx;
        private readonly ILogger<ArizonaRepository> _logger;

        public ArizonaRepository(ArizonaContext ctx, ILogger<ArizonaRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();
            }
            else
            {
                return _ctx.Orders
                           .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUsername(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                            .Where(o => o.User.UserName == username)
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();
            }
            else
            {
                return _ctx.Orders
                           .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                           .OrderBy(p => p.Name)
                           .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Product GetProductById(int id)
        {
            return _ctx.Products
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .Where(o => o.Id == id && o.User.UserName == username)
                       .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                       .Where(p => p.Category == category)
                       .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
