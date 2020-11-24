USE SI2_Grupo02_2021i
GO

--(j) Criar uma função para produzir a listagem das notas de crédito de um determinado ano;

IF OBJECT_ID('viewCreditNoteYear') IS NOT NULL
    DROP FUNCTION viewCreditNoteYear
GO

CREATE FUNCTION viewCreditNoteYear(@year INT)
    RETURNS TABLE AS
        RETURN
            (
                SELECT *
                FROM CreditNote
                WHERE YEAR(creation_date) = @year
            )