USE SI2_Grupo02_2021i
GO

--(i) Actualizar o valor total de uma factura;

IF OBJECT_ID('updateInvoiceValues') IS NOT NULL
    DROP PROCEDURE updateInvoiceValues
GO

CREATE PROCEDURE updateInvoiceValues @invoice NVARCHAR(12)
AS
BEGIN
    --Update Invoice total_value and total_IVA
    UPDATE Invoice
    SET total_value = (
        SELECT SUM(sale_price * units - discount)
        FROM Item I
                 INNER JOIN Product P ON I.SKU = P.SKU
        WHERE I.code = @invoice
          AND I.credit_note IS NULL
    ),
        total_IVA   = (
            SELECT SUM((sale_price + (sale_price * IVA)) * units - discount)
            FROM Item I
                     INNER JOIN Product P ON I.SKU = P.SKU
            WHERE I.code = @invoice
              AND I.credit_note IS NULL
        )
    WHERE code = @invoice
END