using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class Contributor
    {
        public Contributor()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Nif { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
