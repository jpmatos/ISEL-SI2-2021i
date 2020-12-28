USE SI2_Grupo02_2021i

SELECT *
FROM InvoiceState

SELECT *
FROM CreditNoteState

SELECT *
FROM Contributor

SELECT *
FROM Invoice

UPDATE Invoice SET NIF = 0 WHERE Invoice.code = 'FT2020-1'

SELECT *
FROM Product

SELECT *
FROM Item

SELECT *
FROM CreditNote

SELECT *
FROM ItemCredit

SELECT *
FROM InvoiceHistory

SELECT *
FROM ItemHistory