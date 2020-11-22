Contributor(**NIF**, name, address)

Invoice(**code**, *NIF*, state, total_value, IVA, creation_date, emission_date)
- FK: {NIF Ref Contributor(NIF)}
- IR1: code has values 'FTyyyy-xxxxx', where 'yyyy' is year and 'xxxx' is invoice number for this year
- IR2: creation_date and emission_date have values as 'YYYY/MM/DD-HH:MM:SS'
- IR3: state has values:
    - Emitted, making subsequent invoice changes impossible;
    - Updating, when it is not yet finished (e.g. adding items);
    - Proforma, preventing any change to the invoice, except for the change to the states
Issued or Canceled;
    - Canceled, making subsequent changes to the invoice impossible;

InvoiceHistory(**id**, *code* creation_date, state, total_value, IVA, creation_date, emission_date)
- FK: {code Ref. Invoice(code)}
- IR1: creation_date and emission_date have values as 'YYYY/MM/DD-HH:MM:SS'

Product(**SKU**, description, sale_price, IVA)

Item(**number**, *code*, *SKU*, *credit_note*, description, units, discount)
- FK: {{code Ref Invoice(code)},
        {SKU Ref Product(SKU)}}
- FK: {credit_note Ref CreditNote(code)}

ItemHistory(**id**, *number*, creation_date, SKU, credit_note, description, units, discount)
- FK: {number Ref Item(number)}

CreditNote(**code**, *codeInvoice* state, total_value, IVA, creation_date, emission_date)
- FK: {codeInvoice Ref Invoice(code)}
- IR1: code has values 'NCyyyy-xxxxx', where 'yyyy' is year and 'xxxx' is invoice number for this year
- IR2: creation_date and emission_date have values as 'YYYY/MM/DD-HH:MM:SS'
- IR3: state has values:
    - Emitted, making subsequent credit note changes impossible;
    - Updating, when it is not yet finished (e.g. adding items);
