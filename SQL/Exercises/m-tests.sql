USE SI2_Grupo02_2021i
GO

------d. Tests------
--Test 1 -- Insert Product 'P10'
PRINT ('d. Test 1 - Insert Product P10')
BEGIN TRY
    BEGIN TRAN
        DECLARE @product NVARCHAR(10) = 'P10'
        DECLARE @sale_price MONEY = 10
        DECLARE @IVA DECIMAL(3, 2) = 0.2
        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product) != 0
            BEGIN
                PRINT CONCAT('FAILED - Product ', @product, ' already exists!')
                RAISERROR ('Test Failed', 16, 1)
            END

        EXEC insertProduct @product, @sale_price, @IVA, 'insertProduct Procedure Test'
        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product AND sale_price = @sale_price AND IVA = @IVA) = 0
            BEGIN
                PRINT CONCAT('FAILED - Product ', @product, ' was not inserted!')
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT CONCAT('SUCCESS - Successfully inserted product ', @product, '!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed d.1', 16, 1)
END CATCH
GO

--Test 2 -- Update Product 'P09'
PRINT ('d. Test 2 - Update Product P09')
BEGIN TRY
    BEGIN TRAN
        DECLARE @product NVARCHAR(10) = 'P09'
        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product) = 0
            BEGIN
                PRINT CONCAT('FAILED - Product ', @product, ' does not exist!')
                RAISERROR ('Test Failed', 16, 1)
            END

        DECLARE @IVA DECIMAL(3, 2)
        SELECT @IVA = IVA FROM Product WHERE SKU = @product

        EXEC updateProduct @product, 11, NULL, 'insertProduct Edited Procedure Test'
        IF (SELECT COUNT(SKU)
            FROM Product
            WHERE SKU = @product
              AND description = 'insertProduct Edited Procedure Test') = 0
            BEGIN
                PRINT CONCAT('FAILED - Did not update product ', @product, ' description!')
                RAISERROR ('Test Failed', 16, 1)
            END

        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product AND IVA = @IVA) = 0
            BEGIN
                PRINT CONCAT('FAILED - IVA of product ', @product, ' was updated!')
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT CONCAT('SUCCESS - Updated product ', @product, ' description!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed d.2', 16, 1)
END CATCH
GO

--Test 3 -- Delete Product 'P09'
PRINT ('d. Test 3 - Delete Product P09')
BEGIN TRY
    BEGIN TRAN
        DECLARE @product NVARCHAR(10) = 'P09'
        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product) = 0
            BEGIN
                PRINT CONCAT('FAILED - Product ', @product, ' does not exist!')
                RAISERROR ('Test Failed', 16, 1)
            END

        EXEC deleteProduct @product
        IF (SELECT COUNT(SKU) FROM Product WHERE SKU = @product) != 0
            BEGIN
                PRINT CONCAT('FAILED - Product ', @product, ' was not deleted!')
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT CONCAT('SUCCESS - Product ', @product, ' was deleted!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed d.3', 16, 1)
END CATCH
GO


------e. Tests------
--Test 1 -- Get next Invoice code. Expected Value: 'FT2020-4'
PRINT ('e. Test 1 - Generate Invoice code FT2020-4')
BEGIN TRY
    BEGIN TRAN
        DECLARE @expected VARCHAR(MAX) = 'FT2020-4'
        DECLARE @res VARCHAR(MAX)
        EXEC @res = nextCode 'invoice'
        IF (@res != @expected)
            BEGIN
                PRINT CONCAT('FAILED - Invoice code generated did not match ', @expected, '!')
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT CONCAT('SUCCESS - Invoice code generated matched ', @expected, '!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed e.1', 16, 1)
END CATCH
GO

--Test 2 -- Get next Credit Note code. Expected Value: 'NC2020-1'
PRINT ('e. Test 2 - Generate Credit Note code NC2020-1')
BEGIN TRY
    BEGIN TRAN
        DECLARE @expected VARCHAR(MAX) = 'NC2020-1'
        DECLARE @res VARCHAR(MAX)
        EXEC @res = nextCode 'creditnote'
        IF (@res != @expected)
            BEGIN
                PRINT CONCAT('FAILED - Credit Note code generated did not match ', @expected, '!')
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT CONCAT('SUCCESS - Credit Note code generated matched ', @expected, '!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed e.2', 16, 1)
END CATCH
GO


------f. Tests------
--Test 1 -- Insert Invoice with new Contributor
PRINT ('f. Test 1 - Insert Invoice with new Contributor')
BEGIN TRY
    BEGIN TRAN
        DECLARE @contributor INT = 1337
        IF (SELECT COUNT(NIF) FROM Contributor WHERE NIF = @contributor) != 0
            BEGIN
                PRINT CONCAT('FAILED - Contributor ', @contributor, ' already exists!')
                RAISERROR ('Test Failed', 16, 1)
            END
        EXEC insertInvoice 1337, 'John Souls', 'phoney-street'
        IF (SELECT COUNT(NIF) FROM Contributor WHERE NIF = @contributor) != 1
            BEGIN
                PRINT CONCAT('FAILED - Contributor ', @contributor, ' was not inserted!')
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT CONCAT('SUCCESS - Contributor ', @contributor, ' was inserted!')
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed f.1', 16, 1)
END CATCH
GO


------g. Tests------
--Test 1 -- Insert CreditNote. Expected Result: total_value = 45.00; total_IVA = 2.25;
PRINT ('g. Test 1 - Insert CreditNote')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-1';
        DECLARE @expected_total_value MONEY = 45.00
        DECLARE @expected_total_IVA MONEY = 2.25

        UPDATE Invoice
        SET state = 'emitted'
        WHERE code = @invoice

        DECLARE @itemList ItemListType
        INSERT INTO @itemList
        VALUES ('P01', 1),
               ('P02', 1),
               ('P03', 1)
        EXEC insertCreditNote @invoice, @itemList

        IF (SELECT COUNT(code) FROM CreditNote WHERE codeInvoice = @invoice) = 0
            BEGIN
                PRINT 'FAILED - Credit Note was not inserted!'
                RAISERROR ('Test Failed', 16, 1)
            END

        IF (SELECT COUNT(code)
            FROM CreditNote
            WHERE codeInvoice = @invoice
              AND total_value = @expected_total_value
              AND total_IVA = @expected_total_IVA) = 0
            BEGIN
                PRINT 'FAILED - Credit Note values were not calculated properly!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Credit Note was inserted and values calculated properly!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed g.1', 16, 1)
END CATCH
GO

--Test 2 -- Insert CreditNote. Expected Result: total_price = 165.00; total_IVA = 27.00;
PRINT ('g. Test 2 - Insert another CreditNote')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-1';
        DECLARE @expected_total_value MONEY = 165.00
        DECLARE @expected_total_IVA MONEY = 27.00

        UPDATE Invoice
        SET state = 'emitted'
        WHERE code = @invoice

        DECLARE @itemList ItemListType
        INSERT INTO @itemList
        VALUES ('P05', 2),
               ('P06', 3)
        EXEC insertCreditNote @invoice, @itemList

        IF (SELECT COUNT(code) FROM CreditNote WHERE codeInvoice = @invoice) = 0
            BEGIN
                PRINT 'FAILED - Credit Note was not inserted!'
                RAISERROR ('Test Failed', 16, 1)
            END

        IF (SELECT COUNT(code)
            FROM CreditNote
            WHERE codeInvoice = @invoice
              AND total_value = @expected_total_value
              AND total_IVA = @expected_total_IVA) = 0
            BEGIN
                PRINT 'FAILED - Credit Note values were not calculated properly!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Credit Note was inserted and values calculated properly!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed g.2', 16, 1)
END CATCH
GO

--Test 3 -- Insert CreditNote. Expected Result: total_price = 120.00; total_IVA = 22.00;
PRINT ('g. Test 3 - Insert yet another CreditNote')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-1';
        DECLARE @expected_total_value MONEY = 120.00
        DECLARE @expected_total_IVA MONEY = 22.00

        UPDATE Invoice
        SET state = 'emitted'
        WHERE code = @invoice

        DECLARE @itemList ItemListType
        INSERT INTO @itemList
        VALUES ('P07', 1),
               ('P08', 3)
        EXEC insertCreditNote @invoice, @itemList

        IF (SELECT COUNT(code) FROM CreditNote WHERE codeInvoice = @invoice) = 0
            BEGIN
                PRINT 'FAILED - Credit Note was not inserted!'
                RAISERROR ('Test Failed', 16, 1)
            END

        IF (SELECT COUNT(code)
            FROM CreditNote
            WHERE codeInvoice = @invoice
              AND total_value = @expected_total_value
              AND total_IVA = @expected_total_IVA) = 0
            BEGIN
                PRINT 'FAILED - Credit Note values were not calculated properly!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Credit Note was inserted and values calculated properly!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed g.3', 16, 1)
END CATCH
GO


------h. Tests------
--Test 1 -- Add Items to Invoice. Check how it copies the values from Product
PRINT ('h. Test 1 - Add Items to Invoice. Check how it copies the values from Product')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-2'
        DECLARE @itemToAdd ItemToAddListType
        INSERT INTO @itemToAdd
        VALUES ('P01', 1, 0, 'h. Test 1 Item01'),
               ('P03', 3, 1, 'h. Test 1 Item02'),
               ('P05', 5, 2, 'h. Test 1 Item03')
        EXEC insertItemToInvoice @invoice, @itemToAdd

        IF (SELECT COUNT(*) FROM Item WHERE code = @invoice AND IVA != 0 AND sale_price != 0) != 3
            BEGIN
                PRINT 'FAILED - Did not properly insert Items in Invoice!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Properly inserted Items in Invoice!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed h.1', 16, 1)
END CATCH
GO


------i. Tests------
--Test 1 -- Add Items to Invoice and check Invoice values update.
PRINT ('i. Test 1 - Add Items to Invoice and check Invoice values update')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-3'
        DECLARE @expected_total_value MONEY = 138.00
        DECLARE @expected_total_IVA MONEY = 6.90

        DECLARE @itemToAdd ItemToAddListType
        INSERT INTO @itemToAdd
        VALUES ('P02', 1, 0, 'i. Test 1 Item01'),
               ('P04', 5, 2, 'i. Test 1 Item03')
        EXEC insertItemToInvoice @invoice, @itemToAdd
        IF (SELECT COUNT(code)
            FROM Invoice
            WHERE code = @invoice
              AND total_value = @expected_total_value
              AND total_IVA = @expected_total_IVA) = 0
            BEGIN
                PRINT 'FAILED - Did not properly calculate Invoice values!'
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT 'SUCCESS - Properly calculated Invoice values!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed i.1', 16, 1)
END CATCH
GO


------j. Tests------
--Test 1 -- Get CreditNote for current year
PRINT ('j. Test 1 - Get CreditNote for current year')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-1';
        DECLARE @year INT = YEAR(GETDATE())

        UPDATE Invoice
        SET state = 'emitted'
        WHERE code = @invoice

        DECLARE @itemList ItemListType
        INSERT INTO @itemList
        VALUES ('P01', 1),
               ('P02', 1),
               ('P03', 1)
        EXEC insertCreditNote @invoice, @itemList

        IF (SELECT COUNT(*) FROM viewCreditNoteYear(@year)) != 1
            BEGIN
                PRINT 'FAILED - Could not get Credit Notes!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Got Credit Notes!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed j.1', 16, 1)
END CATCH
GO


------k. Tests------
--Test 1 -- Update Invoice state to 'proforma'
PRINT ('k. Test 1 - Update Invoice state to proforma')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-2'
        EXEC updateInvoiceState @invoice, 'proforma'
        IF (SELECT COUNT(code) FROM Invoice WHERE code = @invoice AND state = 'proforma') = 0
            BEGIN
                PRINT 'FAILED - Did not change state to proforma!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Changed state to proforma!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed k.1', 16, 1)
END CATCH
GO

--Test 2 -- Try to update it to the same state and see it fail
PRINT ('k. Test 2 - Try to update it to the same state and see it fail')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-2'
        EXEC updateInvoiceState @invoice, 'proforma'
        BEGIN TRY
            EXEC updateInvoiceState @invoice, 'proforma'
            PRINT 'FAILED - Allowed to change to same state!'
        END TRY
        BEGIN CATCH
            PRINT 'SUCCESS - Did not allow to change to same state!'
        END CATCH
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed k.2', 16, 1)
END CATCH
GO

--Test 3 -- Update Invoice state to 'emitted'
PRINT ('k. Test 3 - Update Invoice state to emitted')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-2'
        EXEC updateInvoiceState @invoice, 'proforma'
        EXEC updateInvoiceState @invoice, 'emitted'
        IF (SELECT COUNT(code) FROM Invoice WHERE code = @invoice AND state = 'emitted') = 0
            BEGIN
                PRINT 'FAILED - Did not change state to emitted!'
                RAISERROR ('Test Failed', 16, 1)
            END

        PRINT 'SUCCESS - Changed state to emitted!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed k.3', 16, 1)
END CATCH
GO

--Test 4 -- Try to update from 'emitted' and see it fail
PRINT ('k. Test 4 - Try to update from emitted and see it fail')
BEGIN TRY
    BEGIN TRAN
        DECLARE @invoice NVARCHAR(12) = 'FT2020-2'
        EXEC updateInvoiceState @invoice, 'proforma'
        EXEC updateInvoiceState @invoice, 'emitted'
        BEGIN TRY
            EXEC updateInvoiceState 'FT2020-2', 'canceled'
            PRINT 'FAILED - Allowed to change from emitted!'
        END TRY
        BEGIN CATCH
            BEGIN TRY
                EXEC updateInvoiceState 'FT2020-2', 'canceled'
                PRINT 'FAILED - Allowed to change from emitted!'
            END TRY
            BEGIN CATCH
                BEGIN TRY
                    EXEC updateInvoiceState 'FT2020-2', 'proforma'
                    PRINT 'FAILED - Allowed to change from emitted!'
                END TRY
                BEGIN CATCH
                    BEGIN TRY
                        EXEC updateInvoiceState 'FT2020-2', 'updating'
                        PRINT 'FAILED - Allowed to change from emitted!'
                    END TRY
                    BEGIN CATCH
                        PRINT 'SUCCESS - Did not allow to change from emitted!'
                    END CATCH
                END CATCH
            END CATCH
        END CATCH
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed k.4', 16, 1)
END CATCH
GO


------l. Tests------
--Test 1 -- See what's in the View
PRINT ('l. Test 1 - See what is in the View')
BEGIN TRY
    BEGIN TRAN
        IF (SELECT COUNT(*) FROM InvoiceContributor) != 3
            BEGIN
                PRINT 'FAILED - View did not return expected number of rows!'
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT 'SUCCESS - View returned expected number of rows!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed l.1', 16, 1)
END CATCH
GO

--Test 2 -- Update Invoice state from View
PRINT ('l. Test 2 - Update Invoice state from View')
BEGIN TRY
    BEGIN TRAN
        DECLARE @address NVARCHAR(128) = 'new address'
        DECLARE @invoice NVARCHAR(12) = 'FT2020-3'

        UPDATE InvoiceContributor
        SET address = @address
        WHERE code = @invoice

        IF (SELECT COUNT(*) FROM InvoiceContributor WHERE code = @invoice AND address = @address) != 1
            BEGIN
                PRINT 'FAILED - Did not update value from View!'
                RAISERROR ('Test Failed', 16, 1)
            END
        PRINT 'SUCCESS - Updated value from View!'
    ROLLBACK
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Test Failed l.2', 16, 1)
END CATCH
GO