USE SI2_Grupo02_2021i
GO

--(h) Adicionar itens a uma factura;

IF TYPE_ID('ItemToAddListType') IS NULL
CREATE TYPE ItemToAddListType AS TABLE
(
    SKU         NVARCHAR(10),
    units       INT,
    discount    DECIMAL(9, 2),
    description NVARCHAR(128)
)
GO

IF OBJECT_ID('insertItemToInvoice') IS NOT NULL
    DROP PROCEDURE insertItemToInvoice
GO

CREATE PROCEDURE insertItemToInvoice @invoice NVARCHAR(12), @itemToAdd ItemToAddListType READONLY
AS
BEGIN
    --Add items
    INSERT INTO Item (code, SKU, sale_price, IVA, units, discount, description)
    SELECT @invoice,
           I.SKU,
           sale_price,
           IVA,
           units,
           discount,
           I.description
    FROM @itemToAdd I
        INNER JOIN Product P ON I.SKU = P.SKU
END



