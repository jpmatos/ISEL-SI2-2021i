USE SI2_Grupo02_2021i

IF OBJECT_ID('ItemHistory') IS NOT NULL
    DROP TABLE ItemHistory

IF OBJECT_ID('InvoiceHistory') IS NOT NULL
    DROP TABLE InvoiceHistory

IF OBJECT_ID('ItemCredit') IS NOT NULL
    DROP TABLE ItemCredit

IF OBJECT_ID('CreditNote') IS NOT NULL
    DROP TABLE CreditNote

IF OBJECT_ID('Item') IS NOT NULL
    DROP TABLE Item

IF OBJECT_ID('Product') IS NOT NULL
    DROP TABLE Product

IF OBJECT_ID('Invoice') IS NOT NULL
    DROP TABLE Invoice

IF OBJECT_ID('Contributor') IS NOT NULL
    DROP TABLE Contributor

IF OBJECT_ID('CreditNoteState') IS NOT NULL
    DROP TABLE CreditNoteState

IF OBJECT_ID('InvoiceState') IS NOT NULL
    DROP TABLE InvoiceState