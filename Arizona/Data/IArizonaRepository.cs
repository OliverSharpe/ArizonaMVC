using Arizona.Data.Entities;
using System.Collections.Generic;

namespace Arizona.Data
{
    public interface IArizonaRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUsername(string username, bool includeItems);
        Order GetOrderById(string username, int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}
