USE SI2_Grupo02_2021i

--InvoiceState
INSERT INTO InvoiceState
VALUES ('emitted'),
       ('updating'),
       ('proforma'),
       ('canceled')

--CreditNoteState
INSERT INTO CreditNoteState
VALUES ('emitted'),
       ('updating')

--Contributor
INSERT INTO Contributor
VALUES (123, 'John', 'address123')

--Invoice
INSERT INTO Invoice
VALUES ('FT2020-1', 123, 'updating', 0, 0, GETDATE(), NULL),
       ('FT2020-2', 123, 'updating', 0, 0, GETDATE(), NULL),
       ('FT2020-3', 123, 'updating', 0, 0, GETDATE(), NULL)

--Product
INSERT INTO Product
VALUES ('P01', 10, 0.05, 'potatoes'),
       ('P02', 15, 0.05, 'pencils'),
       ('P03', 20, 0.05, 'plungers'),
       ('P04', 25, 0.05, 'toothbrushes'),
       ('P05', 30, 0.10, 'marbles'),
       ('P06', 35, 0.20, 'pens'),
       ('P07', 30, 0.10, 'toy'),
       ('P08', 35, 0.20, 'cucumber')

--Item
INSERT INTO Item
VALUES ('FT2020-1', 'P01', 10, 0.05, 1, 0, 'item01'),
       ('FT2020-1', 'P02', 15, 0.05, 1, 0, 'item02'),
       ('FT2020-1', 'P03', 20, 0.05, 1, 0, 'item03'),
       ('FT2020-1', 'P04', 25, 0.05, 1, 0, 'item04'),
       ('FT2020-1', 'P05', 30, 0.10, 2, 0, 'item05'),
       ('FT2020-1', 'P06', 35, 0.20, 3, 0, 'item06'),
       ('FT2020-1', 'P07', 30, 0.10, 2, 10, 'item07'),
       ('FT2020-1', 'P08', 35, 0.20, 3, 5, 'item08')