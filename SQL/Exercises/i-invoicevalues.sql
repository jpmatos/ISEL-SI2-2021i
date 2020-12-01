USE SI2_Grupo02_2021i
GO

--(i) Actualizar o valor total de uma factura;

IF TYPE_ID('CodesListType') IS NULL
CREATE TYPE CodesListType AS TABLE
(
    code NVARCHAR(12)
)
GO

IF OBJECT_ID('updateInvoiceValue') IS NOT NULL
    DROP PROCEDURE updateInvoiceValue
GO

CREATE PROCEDURE updateInvoiceValue @codes CodesListType READONLY
AS
BEGIN
    DECLARE @code NVARCHAR(12)
    DECLARE cursor_code CURSOR FOR
        SELECT code FROM @codes
    OPEN cursor_code
    FETCH NEXT FROM cursor_code INTO @code
    WHILE @@FETCH_STATUS = 0
        BEGIN
            UPDATE Invoice
            SET total_value = (
                SELECT SUM(sale_price * units - discount)
                FROM Item I
                WHERE I.code = @code
            ),
                total_IVA   = (
                    SELECT SUM((sale_price * units - discount) * IVA)
                    FROM Item I
                    WHERE I.code = @code
                )
            WHERE Invoice.code = @code
            FETCH NEXT FROM cursor_code INTO @code
        END
    CLOSE cursor_code
    DEALLOCATE cursor_code
END
GO


--Item Trigger
IF OBJECT_ID('updateInvoiceValueItem') IS NOT NULL
    DROP TRIGGER updateInvoiceValueItem
GO

--Will trigger even if only description was updated
CREATE TRIGGER updateInvoiceValueItem
    ON Item
    AFTER INSERT, UPDATE, DELETE
    AS
BEGIN
    DECLARE @codes CodesListType
    INSERT INTO @codes SELECT code FROM inserted
    INSERT INTO @codes SELECT code FROM deleted
    EXEC updateInvoiceValue @codes
END
GO
