USE SI2_Grupo02_2021i
GO

--(g) Criar uma nota de crédito;

IF OBJECT_ID('insertCreditNote') IS NOT NULL
    DROP PROCEDURE insertCreditNote
GO

IF TYPE_ID('ItemListType') IS NULL
CREATE TYPE ItemListType AS TABLE
(
    number int
)
GO


CREATE PROCEDURE insertCreditNote @invoice NVARCHAR(12), @itemList ItemListType READONLY
AS
BEGIN
    --Create CreditNote
    DECLARE @code VARCHAR(MAX)
    EXEC @code = nextCode 'creditnote'
    INSERT INTO CreditNote VALUES (@code, @invoice, 'updating', 0, 0, GETDATE(), NULL)

    --Update Items
    UPDATE Item SET credit_note = @code WHERE number IN (SELECT * FROM @itemList)

    --Update CreditNote total_value and total_IVA
    UPDATE CreditNote
    SET total_value = (
        SELECT SUM(sale_price * units - discount)
        FROM Item I
                 INNER JOIN Product P ON I.SKU = P.SKU
        WHERE I.number IN (SELECT * FROM @itemList)
    ),
        total_IVA   = (
            SELECT SUM((sale_price + (sale_price * IVA)) * units - discount)
            FROM Item I
                     INNER JOIN Product P ON I.SKU = P.SKU
            WHERE I.number IN (SELECT * FROM @itemList)
        )
    WHERE code = @code

    --Update Invoice total_value and total_IVA
    EXEC updateInvoiceValues @invoice
END

--Test
INSERT INTO Invoice
VALUES ('FT2020-1', NULL, 'updating', 0, 0, GETDATE(), NULL)
INSERT INTO Product
VALUES ('P01', 10, 0.05, 'potatoes'),
       ('P02', 15, 0.05, 'pencils'),
       ('P03', 20, 0.05, 'plungers'),
       ('P04', 25, 0.05, 'toothbrushes'),
       ('P05', 30, 0.10, 'marbles'),
       ('P06', 35, 0.20, 'pens'),
       ('P07', 30, 0.10, 'toy'),
       ('P08', 35, 0.20, 'cucumber')
--Test 1: 1 2 3 = 45.00 47.25
INSERT INTO Item
VALUES ('FT2020-1', 'P01', NULL, 'item01', 1, 0),
       ('FT2020-1', 'P02', NULL, 'item02', 1, 0),
       ('FT2020-1', 'P03', NULL, 'item03', 1, 0),
       ('FT2020-1', 'P04', NULL, 'item04', 1, 0)
--Test 2: 5 6 = 165.00 192.00
INSERT INTO Item
VALUES ('FT2020-1', 'P05', NULL, 'item05', 2, 0),
       ('FT2020-1', 'P06', NULL, 'item06', 3, 0)
--Test 3: 7 8 = 150.00 177.00
INSERT INTO Item
VALUES ('FT2020-1', 'P07', NULL, 'item07', 2, 10),
       ('FT2020-1', 'P08', NULL, 'item08', 3, 5)

DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES (1),
       (2),
       (3)
    EXEC insertCreditNote 'FT2020-1', @itemList

