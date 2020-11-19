IF DB_ID ('SI2_Grupo02_2021i') IS NULL
    CREATE DATABASE SI2_Grupo02_2021i;
GO

USE SI2_Grupo02_2021i;

IF SCHEMA_ID('dbo') IS NULL
    EXEC ('CREATE SCHEMA dbo');
GO


CREATE TABLE Contributor(
    NIF INT PRIMARY KEY,
    name NVARCHAR(128),
    address NVARCHAR(128)
)

CREATE TABLE Invoice(
    code NVARCHAR(12) PRIMARY KEY,
    NIF INT FOREIGN KEY REFERENCES Contributor(NIF),
    state NVARCHAR(16) NOT NULL,
    total_value INT NOT NULL,
    IVA INT NOT NULL,
    creation_date DATETIME NOT NULL,
    emission_date DATETIME,
    CHECK(
        state = 'emitted' OR
        state = 'updating' OR
        state = 'proforma' OR
        state = 'canceled'
        )
)

CREATE TABLE InvoiceHistory(
    id INT PRIMARY KEY IDENTITY(1, 1),
    alteration_date DATETIME NOT NULL,
    state NVARCHAR(16),
    total_value INT,
    IVA INT,
    creation_date DATETIME,
    emission_date DATETIME
)

CREATE TABLE Product(
    SKU NVARCHAR(10) PRIMARY KEY,
    description NVARCHAR(128),
    units INT NOT NULL,
    discount INT NOT NULL
)

CREATE TABLE CreditNote(
    code NVARCHAR(12) PRIMARY KEY,
    codeInvoice NVARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Invoice(code),
    state NVARCHAR(16) NOT NULL,
    total_value INT NOT NULL,
    IVA INT NOT NULL,
    creation_date  DATETIME NOT NULL,
    emission_date DATETIME,
)

CREATE TABLE Item(
    number INT PRIMARY KEY IDENTITY (1, 1),
    code NVARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Invoice(code),
    SKU NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Product(SKU),
    credit_note NVARCHAR(12) FOREIGN KEY REFERENCES CreditNote(code),
    description NVARCHAR(128),
    units INT NOT NULL,
    discount INT NOT NULL
)

CREATE TABLE ItemHistory(
    id INT PRIMARY KEY IDENTITY(1, 1),
    alteration_date DATETIME NOT NULL,
    description NVARCHAR(128),
    units INT,
    discount INT
)