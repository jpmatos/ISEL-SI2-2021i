USE SI2_Grupo02_2021i
GO

-- (f) Criar o procedimento p/ criaFactura (insertInvoice) que permite criar uma factura, sem itens, mas
-- com informação de um contribuinte (novo ou existente);

IF OBJECT_ID('insertInvoice') IS NOT NULL
    DROP PROCEDURE insertInvoice
GO

CREATE PROCEDURE insertInvoice @NIF INT, @name NVARCHAR(128), @address NVARCHAR(128)
AS
BEGIN
    --Check if Contributor exists and update it if fields are not null, if not then create it
    SELECT * INTO #Temp FROM Contributor c WHERE NIF = @NIF
    IF (@@ROWCOUNT = 0)
        INSERT INTO Contributor VALUES (@NIF, @name, @address)
    ELSE
        UPDATE Contributor
        SET name    = ISNULL(@name, (SELECT name FROM #Temp)),
            address = ISNULL(@address, (SELECT address FROM #Temp))
        WHERE NIF = @NIF

    --Create Invoice
    DECLARE @code VARCHAR(MAX)
    EXEC @code = nextCode 'invoice'
    INSERT INTO Invoice VALUES (@code, @NIF, 'updating', 0, 0, GETDATE(), NULL)
END
