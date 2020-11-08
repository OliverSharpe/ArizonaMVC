using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arizona.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
