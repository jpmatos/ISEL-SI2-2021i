USE SI2_Grupo02_2021i
GO

--(d) Inserir, remover e atualizar informação de um produto;

--Insert
IF OBJECT_ID('insertProduct') IS NOT NULL
    DROP PROCEDURE insertProduct
GO

CREATE PROCEDURE insertProduct @SKU NVARCHAR(10), @sale_price MONEY, @IVA DECIMAL(3, 2),
                               @description NVARCHAR(128)
AS
INSERT INTO Product
VALUES (@SKU, @sale_price, @IVA, @description)
GO

--Update
IF OBJECT_ID('updateProduct') IS NOT NULL
    DROP PROCEDURE updateProduct
GO

CREATE PROCEDURE updateProduct @SKU NVARCHAR(10), @sale_price MONEY, @IVA DECIMAL(3, 2),
                               @description NVARCHAR(128)
AS
    BEGIN TRAN
    IF NOT EXISTS(SELECT *
                  FROM Product
                  WHERE Product.SKU = @SKU)
        BEGIN
            ROLLBACK;
            THROW 51404, 'Product with SKU was not found!', 1
        END

SELECT *
INTO #Temp
FROM Product
WHERE SKU = @SKU
UPDATE Product
SET description = ISNULL(@description, (SELECT description FROM #Temp)),
    sale_price  = ISNULL(@sale_price, (SELECT sale_price FROM #Temp)),
    IVA         = ISNULL(@IVA, (SELECT IVA FROM #Temp))
WHERE SKU = @SKU
    COMMIT
GO

--Delete
IF OBJECT_ID('deleteProduct') IS NOT NULL
    DROP PROCEDURE deleteProduct
GO

CREATE PROCEDURE deleteProduct @SKU NVARCHAR(10)
AS
    BEGIN TRAN
    IF EXISTS(SELECT *
              FROM Item
              WHERE SKU = @SKU)
        BEGIN
            ROLLBACK
            ;THROW 51405, 'Item exists for this Product!', 1
        END
DELETE
FROM Product
WHERE SKU = @SKU
    COMMIT
GO