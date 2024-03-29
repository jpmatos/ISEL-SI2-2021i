using System;

namespace Entity
{
    public class Invoice
    {
        public string Code { get; set; }
        public int Nif { get; set; }
        public string State { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalIva { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EmissionDate { get; set; }
    }
}