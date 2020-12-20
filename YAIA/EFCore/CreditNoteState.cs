using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class CreditNoteState
    {
        public CreditNoteState()
        {
            CreditNotes = new HashSet<CreditNote>();
        }

        public string State { get; set; }

        public virtual ICollection<CreditNote> CreditNotes { get; set; }
    }
}
