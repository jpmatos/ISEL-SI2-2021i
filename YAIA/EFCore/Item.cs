using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Item
    {
        public Item()
        {
            ItemCredits = new HashSet<ItemCredit>();
        }

        public string Code { get; set; }
        public string Sku { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Iva { get; set; }
        public int Units { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }

        public virtual Invoice CodeNavigation { get; set; }
        public virtual Product SkuNavigation { get; set; }
        public virtual ICollection<ItemCredit> ItemCredits { get; set; }
    }
}
