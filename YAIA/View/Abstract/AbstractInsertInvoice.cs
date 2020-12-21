using System.Collections.Generic;

namespace View.Abstract
{
    public class AbstractInsertInvoice : AbstractView
    {
        protected static readonly string Name = "Invoice - ";
        protected static readonly List<Parameter> Parameters = new List<Parameter>() {
            {new Parameter("NIF", typeof(int), false)},
            {new Parameter("Name", typeof(string), true)},
            {new Parameter("Address", typeof(string), true)}
        };
    }
}