using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class InvoiceHistory
    {
        public InvoiceHistory()
        {
            ItemHistories = new HashSet<ItemHistory>();
        }

        public string Code { get; set; }
        public DateTime AlterationDate { get; set; }
        public int? Nif { get; set; }
        public string State { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? TotalIva { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? EmissionDate { get; set; }

        public virtual Invoice CodeNavigation { get; set; }
        public virtual ICollection<ItemHistory> ItemHistories { get; set; }
    }
}
