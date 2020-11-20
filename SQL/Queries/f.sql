USE SI2_Grupo02_2021i
GO

-- (f) Criar o procedimento p criaFactura (createInvoice) que permite criar uma factura, sem itens, mas
-- com informação de um contribuinte (novo ou existente);

IF OBJECT_ID('createInvoice') IS NOT NULL
    DROP PROCEDURE createInvoice
GO

CREATE PROCEDURE createInvoice @NIF INT, @IVA INT, @name NVARCHAR(128), @address NVARCHAR(128)
AS
    BEGIN
        --Check if Contributor exists, if not then create it
        SELECT * FROM Contributor c WHERE NIF = @NIF
        IF(@@ROWCOUNT = 0)
            INSERT INTO Contributor VALUES(@NIF, @name, @address)

        --Create Invoice
        DECLARE @code VARCHAR(MAX)
        EXEC @code = nextCode 'invoice'
        INSERT INTO Invoice VALUES(@code, @NIF, 'updating', 0, @IVA, GETDATE(), NULL)
    END

--Test
EXEC createInvoice '123', 10, 'John Souls', 'addr123'
SELECT * FROM Invoice
SELECT * FROM Contributor