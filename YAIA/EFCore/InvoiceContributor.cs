using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore
{
    public partial class InvoiceContributor
    {
        public int Nif { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? TotalIva { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EmissionDate { get; set; }
    }
}
