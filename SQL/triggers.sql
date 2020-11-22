USE SI2_Grupo02_2021i

--Item sale_price and IVA trigger
CREATE TRIGGER copySalePriceAndIVA
    ON Item
    AFTER INSERT
    AS
BEGIN
    DECLARE @SKU NVARCHAR(10)
    DECLARE SKU_cursor CURSOR FOR
        SELECT SKU FROM inserted
    OPEN SKU_cursor
    FETCH NEXT FROM SKU_cursor INTO @SKU
    WHILE @@ROWCOUNT = 0
        BEGIN
            UPDATE Item
            SET sale_price = (SELECT sale_price FROM Product P WHERE P.SKU = @SKU),
                IVA        = (SELECT IVA FROM Product P WHERE P.SKU = @SKU)
            FETCH NEXT FROM SKU_cursor INTO @SKU
        END
END