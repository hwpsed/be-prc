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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Accounts] (
        [AccountId] int NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PasswordHash] varbinary(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        [Created] datetime2 NOT NULL,
        [FullName] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [OrderCreateTemps] (
        [OrderCreateTempId] int NOT NULL,
        [OrderId] int NOT NULL,
        [ServiceId] int NOT NULL,
        [Note] nvarchar(max) NULL,
        [FeedBack] nvarchar(max) NULL,
        [Status] nvarchar(max) NOT NULL,
        [ReceiveAddress] nvarchar(max) NOT NULL,
        [DeliveryAddress] nvarchar(max) NOT NULL,
        [AccountId] int NOT NULL,
        CONSTRAINT [PK_OrderCreateTemps] PRIMARY KEY ([OrderCreateTempId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Stores] (
        [StoreId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Stores] PRIMARY KEY ([StoreId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Orders] (
        [OrderId] int NOT NULL,
        [Note] nvarchar(max) NULL,
        [FeedBack] nvarchar(max) NULL,
        [Status] nvarchar(max) NOT NULL,
        [ReceiveAddress] nvarchar(max) NOT NULL,
        [DeliveryAddress] nvarchar(max) NOT NULL,
        [AccountId] int NOT NULL,
        [isDeleted] bit NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_Orders_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Photos] (
        [Id] int NOT NULL,
        [Url] nvarchar(max) NULL,
        [AccountId] int NOT NULL,
        [Status] bit NOT NULL,
        [IsMain] bit NOT NULL,
        CONSTRAINT [PK_Photos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Photos_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [FavoriteStores] (
        [FavoriteStoreId] int NOT NULL,
        [Status] bit NOT NULL,
        [AccountId] int NOT NULL,
        [StoreId] int NOT NULL,
        CONSTRAINT [PK_FavoriteStores] PRIMARY KEY ([FavoriteStoreId]),
        CONSTRAINT [FK_FavoriteStores_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FavoriteStores_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([StoreId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Services] (
        [ServiceId] int NOT NULL,
        [StoreId] int NOT NULL,
        [ServiceTypeId] int NOT NULL,
        [Status] bit NOT NULL,
        [Name] nvarchar(max) NULL,
        [Price] nvarchar(max) NULL,
        CONSTRAINT [PK_Services] PRIMARY KEY ([ServiceId]),
        CONSTRAINT [FK_Services_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([StoreId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [StorePhoto] (
        [Id] int NOT NULL,
        [Url] nvarchar(max) NULL,
        [StoreId] int NOT NULL,
        [IsMain] bit NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_StorePhoto] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_StorePhoto_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Stores] ([StoreId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [OrderHistories] (
        [OrderHistoryId] int NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [Location] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        [OrderId] int NOT NULL,
        CONSTRAINT [PK_OrderHistories] PRIMARY KEY ([OrderHistoryId]),
        CONSTRAINT [FK_OrderHistories_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [Payments] (
        [PaymentId] int NOT NULL,
        [PaymentType] nvarchar(max) NULL,
        [Prepaid] nvarchar(max) NULL,
        [Postpaid] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        [OrderId] int NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId]),
        CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE TABLE [OrderDetails] (
        [OrderDetailId] int NOT NULL,
        [OrderId] int NOT NULL,
        [ServiceId] int NOT NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderDetailId]),
        CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_OrderDetails_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_FavoriteStores_AccountId] ON [FavoriteStores] ([AccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_FavoriteStores_StoreId] ON [FavoriteStores] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_OrderDetails_ServiceId] ON [OrderDetails] ([ServiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_OrderHistories_OrderId] ON [OrderHistories] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_Orders_AccountId] ON [Orders] ([AccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_Payments_OrderId] ON [Payments] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_Photos_AccountId] ON [Photos] ([AccountId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_Services_StoreId] ON [Services] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    CREATE INDEX [IX_StorePhoto_StoreId] ON [StorePhoto] ([StoreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210716143752_CreateInstance')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210716143752_CreateInstance', N'5.0.6');
END;
GO

COMMIT;
GO

