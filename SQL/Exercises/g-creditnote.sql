USE SI2_Grupo02_2021i
GO

--(g) Criar uma nota de crédito;

IF TYPE_ID('ItemListType') IS NULL
CREATE TYPE ItemListType AS TABLE
(
    SKU      NVARCHAR(10),
    quantity INT
)
GO

IF OBJECT_ID('insertCreditNote') IS NOT NULL
    DROP PROCEDURE insertCreditNote
GO

CREATE PROCEDURE insertCreditNote @invoice NVARCHAR(12), @itemList ItemListType READONLY
AS
BEGIN
    --Check Invoice state
    DECLARE @state NVARCHAR(12)
    SELECT @state = state FROM Invoice WHERE code = @invoice
    IF( @state != 'emitted')
        THROW 50000, 'Can not create Credit Note for a non-emitted Invoice!', 1

    --Create CreditNote
    DECLARE @code VARCHAR(MAX)
    EXEC @code = nextCode 'creditnote'
    INSERT INTO CreditNote VALUES (@code, @invoice, 'updating', 0, 0, GETDATE(), NULL)

    --Create ItemCredit
    INSERT INTO ItemCredit (credit_code, invoice_code, SKU, quantity)
    SELECT @code, @invoice, SKU, quantity
    FROM @itemList

    --Update CreditNote total_value and total_IVA
    UPDATE CreditNote
    SET total_value = (
        SELECT SUM(sale_price * quantity - discount)
        FROM Item I
                 INNER JOIN ItemCredit IC ON I.code = IC.invoice_code AND I.SKU = IC.SKU
        WHERE IC.invoice_code IN (SELECT code FROM @itemList)
        AND IC.SKU IN (SELECT SKU FROM @itemList)
        AND IC.credit_code = @code
    ),
        total_IVA   = (
            SELECT SUM((sale_price * quantity - discount) * IVA)
            FROM Item I
                     INNER JOIN ItemCredit IC ON I.code = IC.invoice_code AND I.SKU = IC.SKU
        WHERE IC.invoice_code IN (SELECT code FROM @itemList)
        AND IC.SKU IN (SELECT SKU FROM @itemList)
        AND IC.credit_code = @code
        )
    WHERE code = @code
END

