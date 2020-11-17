USE SI2_Grupo02_2021i

IF OBJECT_ID('ItemHistory') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (ItemHistory, RESEED, 0)
        DROP TABLE ItemHistory
    END

IF OBJECT_ID('Item') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Item, RESEED, 0)
        DROP TABLE Item
    END

IF OBJECT_ID('CreditNote') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (CreditNote, RESEED, 0)
        DROP TABLE CreditNote
    END

IF OBJECT_ID('Product') IS NOT NULL
    DROP TABLE Product

IF OBJECT_ID('InvoiceHistory') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (InvoiceHistory, RESEED, 0)
        DROP TABLE InvoiceHistory
    END

IF OBJECT_ID('Invoice') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Invoice, RESEED, 0)
        DROP TABLE Invoice
    END

IF OBJECT_ID('Contributor') IS NOT NULL
    DROP TABLE Contributor