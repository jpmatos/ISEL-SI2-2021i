USE SI2_Grupo02_2021i

--Invoice History Update Trigger
IF OBJECT_ID('trackInvoiceHistory') IS NOT NULL
    DROP TRIGGER trackInvoiceHistory
GO

CREATE TRIGGER trackInvoiceHistory
    ON Invoice
    AFTER UPDATE, INSERT
    AS
BEGIN
    DECLARE @date DATETIME = GETDATE()
    INSERT INTO InvoiceHistory (code, alteration_date, NIF, state, total_value, total_IVA, creation_date, emission_date)
    SELECT code,
           @date,
           NIF,
           state,
           total_value,
           total_IVA,
           creation_date,
           emission_date
    FROM inserted
    WHERE NOT EXISTS(SELECT * FROM InvoiceHistory IC WHERE IC.code = code AND IC.alteration_date = @date)
END
GO


--Item History Update Trigger
IF OBJECT_ID('trackItemHistory') IS NOT NULL
    DROP TRIGGER trackItemHistory
GO

CREATE TRIGGER trackItemHistory
    ON Item
    AFTER UPDATE, INSERT
    AS
BEGIN
    DECLARE @code NVARCHAR(12)
    DECLARE cursor_code CURSOR FOR
        SELECT DISTINCT code FROM inserted
    OPEN cursor_code
    FETCH NEXT FROM cursor_code INTO @code
    WHILE @@FETCH_STATUS = 0
        BEGIN
            DECLARE @date DATETIME = GETDATE()

            INSERT INTO InvoiceHistory (code, alteration_date, NIF, state, total_value, total_IVA, creation_date,
                                        emission_date)
            SELECT code,
                   @date,
                   NIF,
                   state,
                   total_value,
                   total_IVA,
                   creation_date,
                   emission_date
            FROM Invoice I
            WHERE I.code = @code
              AND NOT EXISTS(SELECT * FROM InvoiceHistory IC WHERE IC.code = code AND IC.alteration_date = @date)

            INSERT INTO ItemHistory(code, alteration_date, SKU, sale_price, IVA, units, discount, description)
            SELECT code,
                   @date,
                   SKU,
                   sale_price,
                   IVA,
                   units,
                   discount,
                   description
            FROM inserted
            WHERE inserted.code = @code
            FETCH NEXT FROM cursor_code INTO @code
        END
    CLOSE cursor_code
    DEALLOCATE cursor_code
END
GO


--Invoice Delete Instead Of Trigger
IF OBJECT_ID('stopInvoiceDeletion') IS NOT NULL
    DROP TRIGGER stopInvoiceDeletion
GO

CREATE TRIGGER stopInvoiceDeletion
    ON Invoice
    INSTEAD OF DELETE
    AS
BEGIN
    DECLARE @code NVARCHAR(12)
    DECLARE cursor_code CURSOR FOR
        SELECT code FROM deleted
    OPEN cursor_code
    FETCH NEXT FROM cursor_code INTO @code
    WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC updateInvoiceState @code, 'canceled'
            FETCH NEXT FROM cursor_code INTO @code
        END
END
GO


-- SELECT *
-- FROM InvoiceHistory
--
-- SELECT *
-- FROM Invoice
--
-- INSERT INTO Invoice
-- VALUES ('FT2020-4', 123, 'updating', 0, 0, GETDATE(), NULL)
--
-- UPDATE Invoice
-- SET total_value = 28
-- WHERE code = 'FT2020-4'
-- DELETE
-- FROM Invoice
-- WHERE code = 'FT2020-4'
--
--
-- SELECT *
-- FROM ItemHistory
-- SELECT *
-- FROM InvoiceHistory
-- SELECT *
-- FROM Invoice
--
-- INSERT INTO Item
-- VALUES ('FT2020-4', 'P01', 0, 0, 1, 0, 'asd')
-- UPDATE Item
-- SET sale_price = '29'
-- WHERE code = 'FT2020-4'