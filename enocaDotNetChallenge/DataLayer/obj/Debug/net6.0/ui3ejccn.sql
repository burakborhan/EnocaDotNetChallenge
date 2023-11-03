IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Carriers] (
    [Id] int NOT NULL IDENTITY,
    [CarrierConfigurationId] int NOT NULL,
    [CarrierName] nvarchar(max) NOT NULL,
    [CarrierIsActive] bit NOT NULL,
    [CarrierPlusDesiCost] int NOT NULL,
    CONSTRAINT [PK_Carriers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CarrierConfigrations] (
    [Id] int NOT NULL IDENTITY,
    [CarrierId] int NOT NULL,
    [CarrierMaxDesi] int NOT NULL,
    [CarrierMinDesi] int NOT NULL,
    [CarrierCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_CarrierConfigrations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CarrierConfigrations_Carriers_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CarrierId] int NOT NULL,
    [OrderDesi] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [OrderCarrierCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Carriers_CarrierId] FOREIGN KEY ([CarrierId]) REFERENCES [Carriers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CarrierConfigrations_CarrierId] ON [CarrierConfigrations] ([CarrierId]);
GO

CREATE INDEX [IX_Orders_CarrierId] ON [Orders] ([CarrierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231102165955_init', N'6.0.24');
GO

COMMIT;
GO

