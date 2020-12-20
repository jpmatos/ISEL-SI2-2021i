using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Invoice
    {
        public Invoice()
        {
            CreditNotes = new HashSet<CreditNote>();
            InvoiceHistories = new HashSet<InvoiceHistory>();
            Items = new HashSet<Item>();
        }

        public string Code { get; set; }
        public int Nif { get; set; }
        public string State { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? TotalIva { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EmissionDate { get; set; }

        public virtual Contributor NifNavigation { get; set; }
        public virtual InvoiceState StateNavigation { get; set; }
        public virtual ICollection<CreditNote> CreditNotes { get; set; }
        public virtual ICollection<InvoiceHistory> InvoiceHistories { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
