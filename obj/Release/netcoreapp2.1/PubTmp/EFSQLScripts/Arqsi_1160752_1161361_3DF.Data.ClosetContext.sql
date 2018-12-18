IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181004093028_NewCategory')
BEGIN
    CREATE TABLE [Category] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [ParentCategoryID] int NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Category_Category_ParentCategoryID] FOREIGN KEY ([ParentCategoryID]) REFERENCES [Category] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181004093028_NewCategory')
BEGIN
    CREATE INDEX [IX_Category_ParentCategoryID] ON [Category] ([ParentCategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181004093028_NewCategory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181004093028_NewCategory', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181004094811_ChildCategory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181004094811_ChildCategory', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181006181813_MaterialAndFinishAdded')
BEGIN
    CREATE TABLE [MaterialAndFinishes] (
        [ID] int NOT NULL IDENTITY,
        [Material] nvarchar(max) NULL,
        [Finish] nvarchar(max) NULL,
        CONSTRAINT [PK_MaterialAndFinishes] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181006181813_MaterialAndFinishAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181006181813_MaterialAndFinishAdded', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    ALTER TABLE [MaterialAndFinishes] ADD [ProductID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE TABLE [PossibleDimensions] (
        [ID] int NOT NULL IDENTITY,
        [Discriminator] nvarchar(max) NOT NULL,
        [MinDimension] real NULL,
        [MaxDimension] real NULL,
        CONSTRAINT [PK_PossibleDimensions] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE TABLE [Float] (
        [ID] int NOT NULL IDENTITY,
        [FloatValue] real NOT NULL,
        [DiscretePossibleDimensionsID] int NULL,
        CONSTRAINT [PK_Float] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Float_PossibleDimensions_DiscretePossibleDimensionsID] FOREIGN KEY ([DiscretePossibleDimensionsID]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE TABLE [Products] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [ProductCategoryID] int NOT NULL,
        [PossibleDimensionsID] int NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Products_PossibleDimensions_PossibleDimensionsID] FOREIGN KEY ([PossibleDimensionsID]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Products_Category_ProductCategoryID] FOREIGN KEY ([ProductCategoryID]) REFERENCES [Category] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE TABLE [ProductRelationships] (
        [ID] int NOT NULL IDENTITY,
        [ParentProductID] int NOT NULL,
        [ChildProductID] int NOT NULL,
        CONSTRAINT [PK_ProductRelationships] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_ProductRelationships_Products_ChildProductID] FOREIGN KEY ([ChildProductID]) REFERENCES [Products] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProductRelationships_Products_ParentProductID] FOREIGN KEY ([ParentProductID]) REFERENCES [Products] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE TABLE [Restriction] (
        [ID] int NOT NULL IDENTITY,
        [Discriminator] nvarchar(max) NOT NULL,
        [ProductRelationshipID] int NULL,
        [DimensionsID] int NULL,
        CONSTRAINT [PK_Restriction] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Restriction_PossibleDimensions_DimensionsID] FOREIGN KEY ([DimensionsID]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Restriction_ProductRelationships_ProductRelationshipID] FOREIGN KEY ([ProductRelationshipID]) REFERENCES [ProductRelationships] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_MaterialAndFinishes_ProductID] ON [MaterialAndFinishes] ([ProductID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_Float_DiscretePossibleDimensionsID] ON [Float] ([DiscretePossibleDimensionsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_ProductRelationships_ChildProductID] ON [ProductRelationships] ([ChildProductID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_ProductRelationships_ParentProductID] ON [ProductRelationships] ([ParentProductID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_Products_PossibleDimensionsID] ON [Products] ([PossibleDimensionsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_Products_ProductCategoryID] ON [Products] ([ProductCategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_Restriction_DimensionsID] ON [Restriction] ([DimensionsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    CREATE INDEX [IX_Restriction_ProductRelationshipID] ON [Restriction] ([ProductRelationshipID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    ALTER TABLE [MaterialAndFinishes] ADD CONSTRAINT [FK_MaterialAndFinishes_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011141713_RestrictionsAndProducts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181011141713_RestrictionsAndProducts', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [Restriction] DROP CONSTRAINT [FK_Restriction_PossibleDimensions_DimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    DROP INDEX [IX_Restriction_DimensionsID] ON [Restriction];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Restriction]') AND [c].[name] = N'DimensionsID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Restriction] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Restriction] DROP COLUMN [DimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    EXEC sp_rename N'[PossibleDimensions].[MinDimension]', N'MinWidthDimension', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    EXEC sp_rename N'[PossibleDimensions].[MaxDimension]', N'MinHeightDimension', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [Restriction] ADD [PossibleDimensionsID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD [MaxHeightDimension] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD [MaxWidthDimension] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [Float] ADD [DiscretePossibleDimensionsID1] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    CREATE INDEX [IX_Restriction_PossibleDimensionsID] ON [Restriction] ([PossibleDimensionsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    CREATE INDEX [IX_Float_DiscretePossibleDimensionsID1] ON [Float] ([DiscretePossibleDimensionsID1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [Float] ADD CONSTRAINT [FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1] FOREIGN KEY ([DiscretePossibleDimensionsID1]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    ALTER TABLE [Restriction] ADD CONSTRAINT [FK_Restriction_PossibleDimensions_PossibleDimensionsID] FOREIGN KEY ([PossibleDimensionsID]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181011192151_ChangeDimension')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181011192151_ChangeDimension', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    ALTER TABLE [MaterialAndFinishes] DROP CONSTRAINT [FK_MaterialAndFinishes_Products_ProductID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_PossibleDimensions_PossibleDimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    DROP INDEX [IX_MaterialAndFinishes_ProductID] ON [MaterialAndFinishes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MaterialAndFinishes]') AND [c].[name] = N'ProductID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [MaterialAndFinishes] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [MaterialAndFinishes] DROP COLUMN [ProductID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    DROP INDEX [IX_Products_PossibleDimensionsID] ON [Products];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'PossibleDimensionsID');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Products] ALTER COLUMN [PossibleDimensionsID] int NOT NULL;
    CREATE INDEX [IX_Products_PossibleDimensionsID] ON [Products] ([PossibleDimensionsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    CREATE TABLE [ProductMaterialRelationships] (
        [Id] int NOT NULL IDENTITY,
        [ProductId] int NOT NULL,
        [MaterialAndFinishId] int NOT NULL,
        CONSTRAINT [PK_ProductMaterialRelationships] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductMaterialRelationships_MaterialAndFinishes_MaterialAndFinishId] FOREIGN KEY ([MaterialAndFinishId]) REFERENCES [MaterialAndFinishes] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ProductMaterialRelationships_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    CREATE INDEX [IX_ProductMaterialRelationships_MaterialAndFinishId] ON [ProductMaterialRelationships] ([MaterialAndFinishId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    CREATE INDEX [IX_ProductMaterialRelationships_ProductId] ON [ProductMaterialRelationships] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_PossibleDimensions_PossibleDimensionsID] FOREIGN KEY ([PossibleDimensionsID]) REFERENCES [PossibleDimensions] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013132143_AdicionadaRelaçaoProdutoMaterial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181013132143_AdicionadaRelaçaoProdutoMaterial', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013162628_ProductWithPrice')
BEGIN
    ALTER TABLE [Products] ADD [Price] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181013162628_ProductWithPrice')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181013162628_ProductWithPrice', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Category] DROP CONSTRAINT [FK_Category_Category_ParentCategoryID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Float] DROP CONSTRAINT [FK_Float_PossibleDimensions_DiscretePossibleDimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Float] DROP CONSTRAINT [FK_Float_PossibleDimensions_DiscretePossibleDimensionsID1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [ProductMaterialRelationships] DROP CONSTRAINT [FK_ProductMaterialRelationships_MaterialAndFinishes_MaterialAndFinishId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Category_ProductCategoryID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] DROP CONSTRAINT [FK_Restriction_PossibleDimensions_PossibleDimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DROP TABLE [MaterialAndFinishes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DROP INDEX [IX_Restriction_PossibleDimensionsID] ON [Restriction];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DROP INDEX [IX_Float_DiscretePossibleDimensionsID] ON [Float];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Category] DROP CONSTRAINT [PK_Category];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Restriction]') AND [c].[name] = N'PossibleDimensionsID');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Restriction] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Restriction] DROP COLUMN [PossibleDimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PossibleDimensions]') AND [c].[name] = N'MaxHeightDimension');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PossibleDimensions] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [PossibleDimensions] DROP COLUMN [MaxHeightDimension];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PossibleDimensions]') AND [c].[name] = N'MaxWidthDimension');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PossibleDimensions] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [PossibleDimensions] DROP COLUMN [MaxWidthDimension];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PossibleDimensions]') AND [c].[name] = N'MinHeightDimension');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PossibleDimensions] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [PossibleDimensions] DROP COLUMN [MinHeightDimension];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PossibleDimensions]') AND [c].[name] = N'MinWidthDimension');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PossibleDimensions] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [PossibleDimensions] DROP COLUMN [MinWidthDimension];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PossibleDimensions]') AND [c].[name] = N'Discriminator');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [PossibleDimensions] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [PossibleDimensions] DROP COLUMN [Discriminator];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Float]') AND [c].[name] = N'DiscretePossibleDimensionsID');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Float] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Float] DROP COLUMN [DiscretePossibleDimensionsID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    EXEC sp_rename N'[Category]', N'Categories';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    EXEC sp_rename N'[Float].[DiscretePossibleDimensionsID1]', N'DiscretePossibleValuesID', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    EXEC sp_rename N'[Float].[IX_Float_DiscretePossibleDimensionsID1]', N'IX_Float_DiscretePossibleValuesID', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    EXEC sp_rename N'[Categories].[IX_Category_ParentCategoryID]', N'IX_Categories_ParentCategoryID', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxDepthValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxHeightValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxWidthValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MinDepthValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MinHeightValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Restriction] ADD [MinWidthValue] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD [DepthPossibleValuesID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD [HeightPossibleValuesID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD [WidthPossibleValuesID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY ([ID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE TABLE [Materials] (
        [ID] int NOT NULL IDENTITY,
        [MaterialName] nvarchar(max) NULL,
        CONSTRAINT [PK_Materials] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE TABLE [PossibleValues] (
        [ID] int NOT NULL IDENTITY,
        [Discriminator] nvarchar(max) NOT NULL,
        [MinValue] real NULL,
        [MaxValue] real NULL,
        CONSTRAINT [PK_PossibleValues] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE TABLE [Finishes] (
        [ID] int NOT NULL IDENTITY,
        [FinishName] nvarchar(max) NULL,
        [MaterialID] int NULL,
        CONSTRAINT [PK_Finishes] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Finishes_Materials_MaterialID] FOREIGN KEY ([MaterialID]) REFERENCES [Materials] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE INDEX [IX_PossibleDimensions_DepthPossibleValuesID] ON [PossibleDimensions] ([DepthPossibleValuesID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE INDEX [IX_PossibleDimensions_HeightPossibleValuesID] ON [PossibleDimensions] ([HeightPossibleValuesID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE INDEX [IX_PossibleDimensions_WidthPossibleValuesID] ON [PossibleDimensions] ([WidthPossibleValuesID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    CREATE INDEX [IX_Finishes_MaterialID] ON [Finishes] ([MaterialID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Categories] ADD CONSTRAINT [FK_Categories_Categories_ParentCategoryID] FOREIGN KEY ([ParentCategoryID]) REFERENCES [Categories] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Float] ADD CONSTRAINT [FK_Float_PossibleValues_DiscretePossibleValuesID] FOREIGN KEY ([DiscretePossibleValuesID]) REFERENCES [PossibleValues] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD CONSTRAINT [FK_PossibleDimensions_PossibleValues_DepthPossibleValuesID] FOREIGN KEY ([DepthPossibleValuesID]) REFERENCES [PossibleValues] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD CONSTRAINT [FK_PossibleDimensions_PossibleValues_HeightPossibleValuesID] FOREIGN KEY ([HeightPossibleValuesID]) REFERENCES [PossibleValues] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [PossibleDimensions] ADD CONSTRAINT [FK_PossibleDimensions_PossibleValues_WidthPossibleValuesID] FOREIGN KEY ([WidthPossibleValuesID]) REFERENCES [PossibleValues] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [ProductMaterialRelationships] ADD CONSTRAINT [FK_ProductMaterialRelationships_Materials_MaterialAndFinishId] FOREIGN KEY ([MaterialAndFinishId]) REFERENCES [Materials] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Categories_ProductCategoryID] FOREIGN KEY ([ProductCategoryID]) REFERENCES [Categories] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181104103609_DimensionsAndMaterialsChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181104103609_DimensionsAndMaterialsChanges', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    ALTER TABLE [ProductMaterialRelationships] DROP CONSTRAINT [FK_ProductMaterialRelationships_Materials_MaterialAndFinishId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    EXEC sp_rename N'[ProductMaterialRelationships].[MaterialAndFinishId]', N'MaterialId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    EXEC sp_rename N'[ProductMaterialRelationships].[IX_ProductMaterialRelationships_MaterialAndFinishId]', N'IX_ProductMaterialRelationships_MaterialId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    ALTER TABLE [ProductRelationships] ADD [IsMandatory] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    ALTER TABLE [ProductMaterialRelationships] ADD CONSTRAINT [FK_ProductMaterialRelationships_Materials_MaterialId] FOREIGN KEY ([MaterialId]) REFERENCES [Materials] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181109195543_MaterialAndPercentageRestrictions')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181109195543_MaterialAndPercentageRestrictions', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxDepthPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxHeightPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MaxWidthPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MinDepthPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MinHeightPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    ALTER TABLE [Restriction] ADD [MinWidthPercentage] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20181111221306_DbSetNovasRestricoes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20181111221306_DbSetNovasRestricoes', N'2.1.4-rtm-31024');
END;

GO

