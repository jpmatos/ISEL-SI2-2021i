USE SI2_Grupo02_2021i
GO

--(e) Obter o próximo código de uma factura (ou nota de crédito);

IF OBJECT_ID('nextCode') IS NOT NULL
    DROP FUNCTION nextCode
GO

CREATE FUNCTION nextCode(@codeType VARCHAR(10))
    RETURNS VARCHAR(MAX) AS
BEGIN

    --Check if it is Invoice or CreditNote code
    DECLARE @count VARCHAR(4)
    IF (@codeType = 'invoice')
        BEGIN
            SELECT @count = COUNT(*) FROM Invoice I WHERE YEAR(I.creation_date) = YEAR(GETDATE())
            RETURN CONCAT('FT', YEAR(GETDATE()), '-', @count + 1)
        END
    ELSE
        IF (@codeType = 'creditnote')
            BEGIN
                SELECT @count = COUNT(*) FROM CreditNote C WHERE YEAR(C.creation_date) = YEAR(GETDATE())
                RETURN CONCAT('NC', YEAR(GETDATE()), '-', @count + 1)
            END

    RETURN 'INVALID_PARAMETER'
END
GO
