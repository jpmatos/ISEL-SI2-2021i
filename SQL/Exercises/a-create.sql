IF DB_ID('SI2_Grupo02_2021i') IS NULL
CREATE DATABASE SI2_Grupo02_2021i;
GO

USE SI2_Grupo02_2021i;

IF SCHEMA_ID('dbo') IS NULL
    EXEC ('CREATE SCHEMA dbo');
GO

CREATE TABLE InvoiceState
(
    state NVARCHAR(16) PRIMARY KEY
)

CREATE TABLE CreditNoteState
(
    state NVARCHAR(16) PRIMARY KEY
)

CREATE TABLE Contributor
(
    NIF     INT PRIMARY KEY,
    name    NVARCHAR(128),
    address NVARCHAR(128)
)

CREATE TABLE Invoice
(
    code          NVARCHAR(12) PRIMARY KEY,
    NIF           INT          NOT NULL FOREIGN KEY REFERENCES Contributor (NIF),
    state         NVARCHAR(16) NOT NULL FOREIGN KEY REFERENCES InvoiceState (state),
    total_value   MONEY, --without IVA
    total_IVA     MONEY,
    creation_date DATETIME     NOT NULL,
    emission_date DATETIME
)

CREATE TABLE Product
(
    SKU         NVARCHAR(10) PRIMARY KEY,
    sale_price  MONEY,
    IVA         DECIMAL(3, 2),
    description NVARCHAR(128),
    CHECK (
            IVA >= 0 AND
            IVA <= 1
        )
)

CREATE TABLE Item
(
    code        NVARCHAR(12)  NOT NULL FOREIGN KEY REFERENCES Invoice (code),
    SKU         NVARCHAR(10)  NOT NULL FOREIGN KEY REFERENCES Product (SKU),
    sale_price  MONEY         NOT NULL,
    IVA         DECIMAL(3, 2) NOT NULL,
    units       INT           NOT NULL,
    discount    MONEY NOT NULL,
    description NVARCHAR(128),
    CONSTRAINT number PRIMARY KEY (code, SKU)
)

CREATE TABLE CreditNote
(
    code          NVARCHAR(12) PRIMARY KEY,
    codeInvoice   NVARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Invoice (code),
    state         NVARCHAR(16) NOT NULL FOREIGN KEY REFERENCES CreditNoteState (state),
    total_value   MONEY        NOT NULL, --without IVA
    total_IVA     MONEY        NOT NULL,
    creation_date DATETIME     NOT NULL,
    emission_date DATETIME
)

CREATE TABLE ItemCredit
(
    credit_code     NVARCHAR(12) NOT NULL FOREIGN KEY REFERENCES CreditNote (code),
    item_code NVARCHAR(12)  NOT NULL,
    SKU         NVARCHAR(10)  NOT NULL,
    quantity INT          NOT NULL,
    FOREIGN KEY (item_code, SKU) REFERENCES Item(code, SKU),
    CONSTRAINT item_credit_id PRIMARY KEY (credit_code, item_code, SKU)
)

CREATE TABLE InvoiceHistory
(
    code            NVARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Invoice (code),
    alteration_date DATETIME     NOT NULL,
    NIF             INT,
    state           NVARCHAR(16),
    total_value     MONEY,
    total_IVA       MONEY,
    creation_date   DATETIME,
    emission_date   DATETIME,
    CONSTRAINT invoice_history_id PRIMARY KEY (code, alteration_date)
)

CREATE TABLE ItemHistory
(
    code            NVARCHAR(12) NOT NULL,
    alteration_date DATETIME     NOT NULL,
    SKU             NVARCHAR(10),
    sale_price      MONEY,
    IVA             DECIMAL(3, 2),
    units           INT,
    discount        MONEY,
    description     NVARCHAR(128),
    FOREIGN KEY (code, alteration_date) REFERENCES InvoiceHistory(code, alteration_date),
    CONSTRAINT item_history_id PRIMARY KEY (code, alteration_date, SKU)
)