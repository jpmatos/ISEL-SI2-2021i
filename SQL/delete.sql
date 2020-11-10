USE SI2_Grupo02_2021i

IF OBJECT_ID('ItemAcreditado') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (ItemAcreditado, RESEED, 0)
        DELETE FROM ItemAcreditado
    END

IF OBJECT_ID('NotaDeCredito') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (NotaDeCredito, RESEED, 0)
        DELETE FROM NotaDeCredito
    END

IF OBJECT_ID('Item') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Item, RESEED, 0)
        DELETE FROM Item
    END

IF OBJECT_ID('Produto') IS NOT NULL
    DELETE FROM Produto

IF OBJECT_ID('Alteracao') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Alteracao, RESEED, 0)
        DELETE FROM Alteracao
    END

IF OBJECT_ID('Fatura') IS NOT NULL
    BEGIN
        DBCC CHECKIDENT (Fatura, RESEED, 0)
        DELETE FROM Fatura
    END

IF OBJECT_ID('Contribuinte') IS NOT NULL
    DELETE FROM Contribuinte