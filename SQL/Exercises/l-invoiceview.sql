USE SI2_Grupo02_2021i
GO

--(l) Criar uma vista que mostre o resumo das facturas (atributos de factura e de con-
--tribuinte), que possibilite a alteração do estado de uma factura;

IF OBJECT_ID('InvoiceContributor') IS NOT NULL
    DROP VIEW InvoiceContributor
GO

CREATE VIEW InvoiceContributor AS
SELECT C.NIF,
       C.name,
       C.address,
       I.code,
       I.state,
       I.total_value,
       I.total_IVA,
       I.creation_date,
       I.emission_date
FROM Invoice I
         JOIN Contributor C on I.NIF = C.NIF