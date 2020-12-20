using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class ItemHistory
    {
        public string Code { get; set; }
        public DateTime AlterationDate { get; set; }
        public string Sku { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? Iva { get; set; }
        public int? Units { get; set; }
        public decimal? Discount { get; set; }
        public string Description { get; set; }

        public virtual InvoiceHistory InvoiceHistory { get; set; }
    }
}
