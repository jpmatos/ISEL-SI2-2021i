IF DB_ID ('SI2_Grupo02_2021i') IS NULL
	CREATE DATABASE SI2_Grupo02_2021i;
GO

USE SI2_Grupo02_2021i;

CREATE TABLE Contribuinte(
    NIF INT PRIMARY KEY,
    nome NVARCHAR(128),
    morada NVARCHAR(128)
)

CREATE TABLE Fatura(
    codigo INT PRIMARY KEY IDENTITY (1, 1),
    NIF INT FOREIGN KEY REFERENCES Contribuinte(NIF),
    estado NVARCHAR(16) NOT NULL,
    valor_total INT NOT NULL,
    IVA INT NOT NULL,
    data_de_criacao DATETIME NOT NULL,
    data_de_emissao DATETIME,
    CHECK(
        estado = 'emitida' OR
        estado = 'em actualizacao' OR
        estado = 'proforma' OR
        estado = 'anulada'
        )
)

CREATE TABLE Alteracao(
    ID INT PRIMARY KEY IDENTITY(1, 1),
    data_de_alteracao DATETIME NOT NULL,
    estado NVARCHAR(16),
    valor_total INT,
    IVA INT,
    data_de_criacao DATETIME,
    data_de_emissao DATETIME
)

CREATE TABLE Produto(
    SKU NVARCHAR(10) PRIMARY KEY,
    descricao NVARCHAR(128),
    unidades INT NOT NULL,
    desconto INT NOT NULL
)

CREATE TABLE Item(
    numero INT PRIMARY KEY IDENTITY (1, 1),
    codigo INT NOT NULL FOREIGN KEY REFERENCES Fatura(codigo),
    SKU NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Produto(SKU),
    descricao NVARCHAR(128),
    unidades INT NOT NULL,
    desconto INT NOT NULL
)

CREATE TABLE NotaDeCredito(
    codigo INT PRIMARY KEY IDENTITY (1, 1),
    codigoFatura INT NOT NULL FOREIGN KEY REFERENCES Fatura(codigo),
    estado NVARCHAR(16) NOT NULL,
    valor_total INT NOT NULL,
    IVA INT NOT NULL,
    data_de_criacao  DATETIME NOT NULL,
    data_de_emissao DATETIME,
)

CREATE TABLE ItemAcreditado(
    ID INT PRIMARY KEY IDENTITY (1, 1),
    numero INT NOT NULL FOREIGN KEY REFERENCES Item(numero),
    codigo INT NOT NULL FOREIGN KEY REFERENCES NotaDeCredito(codigo)
)