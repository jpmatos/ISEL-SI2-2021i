USE SI2_Grupo02_2021i

--InvoiceState
INSERT INTO InvoiceState
VALUES ('emitted'),
       ('updating'),
       ('proforma'),
       ('canceled')

--CreditNoteState
INSERT INTO CreditNoteState
VALUES ('emitted'),
       ('updating')