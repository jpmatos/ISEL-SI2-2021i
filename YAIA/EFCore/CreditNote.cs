using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class CreditNote
    {
        public CreditNote()
        {
            ItemCredits = new HashSet<ItemCredit>();
        }

        public string Code { get; set; }
        public string CodeInvoice { get; set; }
        public string State { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalIva { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EmissionDate { get; set; }

        public virtual Invoice CodeInvoiceNavigation { get; set; }
        public virtual CreditNoteState StateNavigation { get; set; }
        public virtual ICollection<ItemCredit> ItemCredits { get; set; }
    }
}
