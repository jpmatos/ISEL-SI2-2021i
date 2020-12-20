using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class InvoiceState
    {
        public InvoiceState()
        {
            Invoices = new HashSet<Invoice>();
        }

        public string State { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
