USE SI2_Grupo02_2021i
GO

--(k) Actualizar o estado de uma factura;

IF OBJECT_ID('updateInvoiceState') IS NOT NULL
    DROP PROCEDURE updateInvoiceState
GO

CREATE PROCEDURE updateInvoiceState @code NVARCHAR(12), @state NVARCHAR(16)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
    BEGIN TRAN
        IF (SELECT COUNT(*) FROM InvoiceState WHERE state = @state) = 0
            THROW 50000, 'Invalid Invoice state', 1

        DECLARE @current NVARCHAR(12)
        SELECT @current = state FROM Invoice WHERE code = @code

        IF (@current = 'emitted' OR @current = 'canceled')
            THROW 50000, 'Can not change Invoice state!', 1

        IF (@current = 'proforma' AND @state = 'updating')
            THROW 50000, 'Invalid Invoice state change!', 1

        IF ((@current = 'proforma' AND @state = 'proforma') OR
            (@current = 'updating' AND @state = 'updating'))
            THROW 50000, 'Invoice already in the specified state!', 1

        if (@state = 'emitted')
            UPDATE Invoice SET state = @state, emission_date = GETDATE() WHERE code = @code
        ELSE
            UPDATE Invoice SET state = @state WHERE code = @code
    COMMIT
END
GO

--Also make a procedure for Credit Note
IF OBJECT_ID('updateCreditNoteState') IS NOT NULL
    DROP PROCEDURE updateCreditNoteState
GO

CREATE PROCEDURE updateCreditNoteState @code NVARCHAR(12), @state NVARCHAR(16)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
    BEGIN TRAN
        IF (SELECT COUNT(*) FROM CreditNoteState WHERE state = @state) = 0
            BEGIN
                ROLLBACK;
                THROW 50000, 'Invalid Credit Note state', 1
            END

        DECLARE @current NVARCHAR(12)
        SELECT @current = state FROM CreditNote WHERE code = @code

        IF (@current = 'emitted')
            BEGIN
                ROLLBACK;
                THROW 50000, 'Can not change Credit Note state!', 1
            END

        UPDATE Invoice SET state = @state WHERE code = @code
    COMMIT
END
