using System;
using View.Interface;
using View.Util;

namespace View
{
    public class UpdateInvoiceValue : AbstractView
    {
        public override void Query(DataAccess e)
        {
            Console.WriteLine("Invoice value update is done via triggers. Nothing to do here!");
        }
    }
}