USE SI2_Grupo02_2021i
GO

------d. Tests------
--Test 1 -- Insert Product 'P09'
SELECT *
FROM Product

EXEC insertProduct 'P09', 10, 0.2, 'insertProduct Procedure Test'

SELECT *
FROM Product

--Test 2 -- Update Product 'P09', IVA should stay the same
SELECT *
FROM Product

EXEC updateProduct 'P09', 11, NULL, 'insertProduct Edited Procedure Test'

SELECT *
FROM Product

--Test 3 -- Delete Product 'P09'
SELECT *
FROM Product

EXEC deleteProduct 'P09'

SELECT *
FROM Product


------e. Tests------
--Test 1 -- Get next Invoice code. Expected Value: 'FT2020-4'
DECLARE @res VARCHAR(MAX)
EXEC @res = nextCode 'invoice'
print @res

--Test 1 -- Get next Credit Note code. Expected Value: 'NC2020-1'
DECLARE @res VARCHAR(MAX)
EXEC @res = nextCode 'creditnote'
print @res


------f. Tests------
--Test 1 -- Insert Invoice with new Contributor
SELECT *
FROM Invoice
SELECT *
FROM Contributor

EXEC insertInvoice 1337, 'John Souls', 'phoney-street'

SELECT *
FROM Invoice
SELECT *
FROM Contributor


------g. Tests------
--Test 1 -- Insert CreditNote. Expected Result: total_price = 45.00; total_IVA = 47.25;
SELECT *
FROM CreditNote

DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P01', 1),
       ('P02', 1),
       ('P03', 1)
EXEC insertCreditNote 'FT2020-1', @itemList

SELECT *
FROM CreditNote

--Test 2 -- Insert CreditNote. Expected Result: total_price = 165.00; total_IVA = 192.00;
SELECT *
FROM CreditNote

DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P05', 2),
       ('P06', 3)
EXEC insertCreditNote 'FT2020-1', @itemList

SELECT *
FROM CreditNote

--Test 3 -- Insert CreditNote. Expected Result: total_price = 150.00; total_IVA = 175.00;
SELECT *
FROM CreditNote

DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P07', 1),
       ('P08', 3)
EXEC insertCreditNote 'FT2020-1', @itemList

SELECT *
FROM CreditNote


------h. Tests------
--Test 1 -- Add Items to Invoice. Check how it copies the values from Product
SELECT *
FROM Item I
WHERE I.code = 'FT2020-2'

DECLARE @itemToAdd ItemToAddListType
INSERT INTO @itemToAdd
VALUES ('P01', 1, 0, 'h. Test 1 Item01'),
       ('P03', 3, 1, 'h. Test 1 Item02'),
       ('P05', 5, 2, 'h. Test 1 Item03')
EXEC insertItemToInvoice 'FT2020-2', @itemToAdd

SELECT *
FROM Item I
WHERE I.code = 'FT2020-2'


------i. Tests------
--Test 1 -- Add Items to Invoice and check Invoice values update.
-- Expected Values: total_value = 138.00; total_IVA = 6.90
SELECT *
FROM Invoice I
WHERE I.code = 'FT2020-3'

DECLARE @itemToAdd ItemToAddListType
INSERT INTO @itemToAdd
VALUES ('P02', 1, 0, 'i. Test 1 Item01'),
       ('P04', 5, 2, 'i. Test 1 Item03')
EXEC insertItemToInvoice 'FT2020-3', @itemToAdd

SELECT *
FROM Invoice I
WHERE I.code = 'FT2020-3'

--Test 2 -- Create CreditNote and check Invoice values update
-- Expected Values: total_value = 123.00; total_IVA = 6.15
SELECT *
FROM Invoice I
WHERE I.code = 'FT2020-3'

DECLARE @itemList ItemListType
INSERT INTO @itemList
VALUES ('P02', 1)
EXEC insertCreditNote 'FT2020-3', @itemList

SELECT *
FROM Invoice I
WHERE I.code = 'FT2020-3'