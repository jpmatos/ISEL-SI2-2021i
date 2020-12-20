using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class ItemCredit
    {
        public string CreditCode { get; set; }
        public string InvoiceCode { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }

        public virtual CreditNote CreditCodeNavigation { get; set; }
        public virtual Item Item { get; set; }
    }
}
