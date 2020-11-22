USE SI2_Grupo02_2021i
GO

------d. Tests------
--Test 1 -- Insert Product 'P09'
EXEC insertProduct 'P09', 10, 0.2, 'insertProduct Procedure Test'
SELECT * FROM Product

--Test 2 -- Update Product 'P09', IVA should stay the same
EXEC updateProduct 'P09', 11, NULL, 'insertProduct Edited Procedure Test'
SELECT * FROM Product

--Test 3 -- Delete Product 'P09'
EXEC deleteProduct 'P09'
SELECT * FROM Product


------e. Tests------
--Test 1 -- Get next Invoice code. Expected Value: 'FT2020-2'
DECLARE @res VARCHAR(MAX)
EXEC @res = nextCode 'invoice'
print @res

--Test 1 -- Get next Credit Note code. Expected Value: 'NC2020-1'
DECLARE @res VARCHAR(MAX)
EXEC @res = nextCode 'creditnote'
print @res


------f. Tests------
--Test 1 -- Insert Invoice with new Contributor
    EXEC insertInvoice 1337, 'John Souls', 'phoney-street'
SELECT *
FROM Invoice
SELECT *
FROM Contributor


------g. Tests------
--Test 1 -- Insert CreditNote. Expected Result: total_price = 45.00; total_IVA = 47.25;
DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P01', 1),
       ('P02', 1),
       ('P03', 1)
    EXEC insertCreditNote 'FT2020-1', @itemList
SELECT * FROM CreditNote

--Test 2 -- Insert CreditNote. Expected Result: total_price = 165.00; total_IVA = 192.00;
DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P05', 2),
       ('P06', 3)
    EXEC insertCreditNote 'FT2020-1', @itemList
SELECT * FROM CreditNote

--Test 3 -- Insert CreditNote. Expected Result: total_price = 150.00; total_IVA = 175.00;
DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P07', 1),
       ('P08', 3)
    EXEC insertCreditNote 'FT2020-1', @itemList
SELECT * FROM CreditNote
