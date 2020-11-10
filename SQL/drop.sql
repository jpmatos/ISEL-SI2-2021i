USE SI2_Grupo02_2021i

IF OBJECT_ID('ItemAcreditado') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (ItemAcreditado, RESEED, 0)
        DROP TABLE ItemAcreditado
    END

IF OBJECT_ID('NotaDeCredito') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (NotaDeCredito, RESEED, 0)
        DROP TABLE NotaDeCredito
    END

IF OBJECT_ID('Item') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Item, RESEED, 0)
        DROP TABLE Item
    END

IF OBJECT_ID('Produto') IS NOT NULL
    DROP TABLE Produto

IF OBJECT_ID('Alteracao') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Alteracao, RESEED, 0)
        DROP TABLE Alteracao
    END

IF OBJECT_ID('Fatura') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Fatura, RESEED, 0)
        DROP TABLE Fatura
    END

IF OBJECT_ID('Contribuinte') IS NOT NULL
    DROP TABLE Contribuinte