using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Product
    {
        public Product()
        {
            Items = new HashSet<Item>();
        }

        public string Sku { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? Iva { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
