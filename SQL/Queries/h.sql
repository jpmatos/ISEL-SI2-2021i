USE SI2_Grupo02_2021i
GO

--(h) Adicionar itens a uma factura;

IF OBJECT_ID('addItemToInvoice') IS NOT NULL
    DROP PROCEDURE insertItemToInvoice
GO

IF TYPE_ID('ItemToAddListType') IS NULL
CREATE TYPE ItemToAddListType AS TABLE
(
    SKU         NVARCHAR(10),
    units       INT,
    discount    DECIMAL(9, 2),
    description NVARCHAR(128)
)
GO

CREATE PROCEDURE insertItemToInvoice @invoice NVARCHAR(12), @itemToAdd ItemToAddListType READONLY
AS
BEGIN
    --Add items
    INSERT INTO Item (code, SKU, units, discount, description)
    SELECT @invoice, SKU, units, discount, description
    FROM @itemToAdd

    --Recalculate Invoice values
    --TODO Extract procedure from  g.
END

--Test
INSERT INTO Invoice
VALUES ('FT2020-1', NULL, 'updating', 0, 0, GETDATE(), NULL)
INSERT INTO Product
VALUES ('P01', 10, 0.05, 'potatoes'),
       ('P02', 15, 0.05, 'pencils'),
       ('P03', 20, 0.05, 'plungers')
DECLARE @itemToAdd ItemToAddListType
INSERT INTO @itemToAdd
VALUES ('P01', 1, 0, 'desc1'),
       ('P02', 3, 1, 'desc2'),
       ('P03', 5, 2, 'desc3')
EXEC insertItemToInvoice 'FT2020-1', @itemToAdd