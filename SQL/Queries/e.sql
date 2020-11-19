USE SI2_Grupo02_2021i
GO

--(e) Obter o próximo código de uma factura (ou nota de crédito);

IF OBJECT_ID('nextCode') IS NOT NULL
    DROP FUNCTION nextCode
GO

CREATE FUNCTION nextCode (@codeType VARCHAR(10))
RETURNS VARCHAR(MAX) AS
    BEGIN
        DECLARE @current_code VARCHAR(MAX)

        IF(@codeType = 'invoice')
            SELECT @current_code = code FROM Invoice
        ELSE IF (@codeType = 'creditnote')
            SELECT @current_code = code FROM CreditNote
        ELSE
            RETURN 'INVALID_PARAMETER'
        SET @current_code = SUBSTRING(@current_code, 3, LEN(@current_code))

        DECLARE @yearStr VARCHAR(MAX), @numStr VARCHAR(MAX)
        DECLARE cursor_code CURSOR FOR
            SELECT value FROM STRING_SPLIT(@current_code, '-')
        OPEN cursor_code
        FETCH NEXT FROM cursor_code INTO @yearStr
        FETCH NEXT FROM cursor_code INTO @numStr
        CLOSE cursor_code
        DEALLOCATE cursor_code

        DECLARE @year INT = CAST(@yearStr AS INT)
        DECLARE @num INT = CAST(@numStr AS INT)

        IF (@year < YEAR(GETDATE()))
            BEGIN
                SET @year = YEAR(GETDATE())
                SET @num = 0
            END
        ELSE
            BEGIN
                SET @num = @num + 1
            END

        DECLARE @res VARCHAR(MAX)
        IF(@codeType = 'invoice')
            SET @res = CONCAT('FT', @year, '-', @num)
        ELSE IF (@codeType = 'creditnote')
            SET @res = CONCAT('NC', @year, '-', @num)
        RETURN @res
    END
GO

--Test
DECLARE @res VARCHAR(MAX)
EXEC @res = nextCode 'invoice'
print @res

INSERT INTO Invoice VALUES('FT2020-13456', NULL, 'updating', 10, 0, GETDATE(), NULL)
INSERT INTO Invoice VALUES('FT2020-13458', NULL, 'updating', 10, 0, GETDATE(), NULL)
INSERT INTO Invoice VALUES('FT2020-13460', NULL, 'updating', 10, 0, GETDATE(), NULL)