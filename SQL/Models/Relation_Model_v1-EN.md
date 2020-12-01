InvoiceState(**state**)

CreditNoteState(**state**)

Contributor(**NIF**, name, address)

Invoice(**code**, *NIF*, *state*, total_value, total_IVA, creation_date, emission_date)
- FK: {NIF Ref Contributor(NIF)}
- FK: {state Ref InvoiveState(state)}
- IR1: code has values 'FTyyyy-xxxxx', where 'yyyy' is year and 'xxxx' is invoice number for this year
- IR2: creation_date and emission_date have values as 'YYYY/MM/DD-HH:MM:SS'
- IR3: state has values:
    - Emitted, making subsequent invoice changes impossible;
    - Updating, when it is not yet finished (e.g. adding items);
    - Proforma, preventing any change to the invoice, except for the change to the states
Issued or Canceled;
    - Canceled, making subsequent changes to the invoice impossible;
- IR4: total_value and total_IVA values are calculated based on Item entries with the Invoice's FK

Product(**SKU**, description, sale_price, IVA)
- IR1: SKU has a Stock Keeping Unit format
- IR2: IVA has decimal values [0, 1]

Item(*code*, *SKU*, sale_price, IVA, units, discount, description)
- PK: {code, SKU}
- FK: {{code Ref Invoice(code)},
        {SKU Ref Product(SKU)}}
- IR1: IVA has decimal values [0, 1]
- IR2: sale_price and IVA are copied from Product

CreditNote(**code**, *codeInvoice* *state*, total_value, total_IVA, creation_date, emission_date)
- FK: {codeInvoice Ref Invoice(code)}
- FK: {state Ref CreditNoteState(state)}
- IR1: code has values 'NCyyyy-xxxxx', where 'yyyy' is year and 'xxxx' is invoice number for this year
- IR2: creation_date and emission_date have values as 'YYYY/MM/DD-HH:MM:SS'
- IR3: state has values:
    - Emitted, making subsequent credit note changes impossible;
    - Updating, when it is not yet finished (e.g. adding items);

ItemCredit(*credit_code*, *invoice_code*, *SKU*, quantity)
- PK: {{credit_code, invoice_code, SKU}
- FK: {credit_code Ref CreditNote(code)
- FK: {{invoice_code Ref Invoice(code)}, 
        {SKU Ref Product(SKU)}}
- IR1: quantity value greater than 1

InvoiceHistory(*code* alteration_date, NIF, state, total_value, IVA, creation_date, emission_date)
- PK: {code, alteration_date}
- FK: {code Ref. Invoice(code)}
- IR1: alteration_date has value as 'YYYY/MM/DD-HH:MM:SS'
- IR2: the remaining field values are copied from Invoice


ItemHistory(*code*, *alternation_date*, SKU, sale_price, IVA, units, discount, description)
- PK: {code, alteration_date, SKU}
- FK: {code Ref InvoiceHistory(code), 
        {alteration_date Ref InvoiceHistory(alteration_date)}}
- IR1: the field values are copied from Item
