USE SI2_Grupo02_2021i

IF OBJECT_ID('ItemHistory') IS NOT NULL
    DELETE FROM ItemHistory

IF OBJECT_ID('InvoiceHistory') IS NOT NULL
    DELETE FROM InvoiceHistory

IF OBJECT_ID('ItemCredit') IS NOT NULL
    DELETE FROM ItemCredit

IF OBJECT_ID('CreditNote') IS NOT NULL
    DELETE FROM CreditNote

IF OBJECT_ID('Item') IS NOT NULL
    DELETE FROM Item

IF OBJECT_ID('Product') IS NOT NULL
    DELETE FROM Product

IF OBJECT_ID('Invoice') IS NOT NULL
    DELETE FROM Invoice

IF OBJECT_ID('Contributor') IS NOT NULL
    DELETE FROM Contributor

IF OBJECT_ID('CreditNoteState') IS NOT NULL
    DELETE FROM CreditNoteState

IF OBJECT_ID('InvoiceState') IS NOT NULL
    DELETE FROM InvoiceState