USE [DotNetAliexpress];
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Category]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[Category]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
        CategoryId BIGINT NULL,
        CategoryName NVARCHAR(MAX)
            CONSTRAINT [PK_Category]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Store]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[Store]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
        StoreId BIGINT NULL,
        StoreName NVARCHAR(MAX) NOT NULL,
		StoreYear NVARCHAR(MAX) NULL,
		StoreRatingPercent DECIMAL NULL,
		StoreRatingDescribed NVARCHAR(MAX) NULL,
		StoreRatingCommunication NVARCHAR(MAX) NULL,
		StoreRatingShippingSpeed NVARCHAR(MAX) NULL,
		StoreRatingTotal NVARCHAR(MAX) NULL

            CONSTRAINT [PK_Store]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[ProductParent]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[ProductParent]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductParrentId BIGINT NOT NULL,
		ProductKeyId VARCHAR(MAX) NOT NULL

            CONSTRAINT [PK_ProductParent]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO

IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Product]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[Product]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
        IsParent BIT  NULL DEFAULT 0,
		CategoryId BIGINT NULL,
		CategoryName NVARCHAR(MAX) NULL,
		StoreId BIGINT NULL,
		StoreName NVARCHAR(MAX) NULL,
		ProductId BIGINT NOT NULL,
		ProductKeyId VARCHAR(MAX) NOT NULL,
		ProductName NVARCHAR(MAX) NOT NULL,
		[Description] NVARCHAR(MAX) NULL,
		BuyingPrice DECIMAL NULL,
		ItemLot NVARCHAR(MAX) NULL,
		BrandName NVARCHAR(MAX) NULL,
		StockNumber BIGINT NULL,

		StoreYear NVARCHAR(MAX) NULL,
		StoreRatingPercent VARCHAR(20) NULL,
		StoreRatingDescribed DECIMAL NULL,
		StoreRatingCommunication DECIMAL NULL,
		StoreRatingShippingSpeed DECIMAL NULL,
		StoreRatingTotal INT NULL,
		OrderNumber BIGINT NOT NULL,
		RatingNumber BIGINT NULL,
		RatingPercent DECIMAL NULL,
		ProcessingTime NVARCHAR(MAX) NULL,
		ShippingCompany NVARCHAR(MAX) NULL,
		ShippingFee DECIMAL NULL,
		Shipping1ST NVARCHAR(MAX) NULL,
		Shipping2ND NVARCHAR(MAX) NULL,
		Shipping3RD NVARCHAR(MAX) NULL,
		OnTimeDelivery VARCHAR(50) NULL,
		TotalPrice DECIMAL NOT NULL,
		Bullet1ST NVARCHAR(MAX) NULL,
		Bullet2ND NVARCHAR(MAX) NULL,
		Bullet3RD NVARCHAR(MAX) NULL,
		Bullet4TH NVARCHAR(MAX) NULL,
		Bullet5TH NVARCHAR(MAX) NULL,

		Image1ST NVARCHAR(MAX) NULL,
		Image2ND NVARCHAR(MAX) NULL,
		Image3RD NVARCHAR(MAX) NULL,
		Image4TH NVARCHAR(MAX) NULL,
		Image5TH NVARCHAR(MAX) NULL,
		Image6TH NVARCHAR(MAX) NULL,
		Image7TH NVARCHAR(MAX) NULL,

		VariationTheme VARCHAR(MAX) NOT NULL,
		VariationColor  VARCHAR(MAX) NOT NULL,
		VariationSize VARCHAR(MAX) NOT NULL,
		VariationPlus1ST VARCHAR(MAX) NOT NULL,
		VariationPlus2ND VARCHAR(MAX) NOT NULL,
		VariationShippingFrom VARCHAR(MAX) NULL,

		ParentChild VARCHAR(50) NULL,
		ParentSku BIGINT NULL,
		RelationShipType VARCHAR(50) NULL,
		Specification10 NVARCHAR(MAX) NULL,
		ActionTime DATETIME NOT NULL DEFAULT GETDATE()

            CONSTRAINT [PK_Product]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Shipping]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[ProductShipping]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductId BIGINT NOT NULL,
		Company NVARCHAR(MAX) NULL,
		Fee DECIMAL NULL,
		Shipping1ST NVARCHAR(MAX) NULL,
		Shipping2ND NVARCHAR(MAX) NULL,
		Shipping3RD NVARCHAR(MAX) NULL,
		OnTimeDelivery NVARCHAR(MAX) NULL

            CONSTRAINT [PK_Shipping]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[ProductBullet]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[ProductBullet]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductId BIGINT NOT NULL,
		Bullet1ST NVARCHAR(MAX) NULL,
		Bullet2ND NVARCHAR(MAX) NULL,
		Bullet3RD NVARCHAR(MAX) NULL,
		Bullet4TH NVARCHAR(MAX) NULL,
		Bullet5TH NVARCHAR(MAX) NULL

            CONSTRAINT [PK_Bullet]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[ProductImage]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[ProductImage]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductId BIGINT NOT NULL,
		Image1ST NVARCHAR(MAX) NULL,
		Image2ND NVARCHAR(MAX) NULL,
		Image3RD NVARCHAR(MAX) NULL,
		Image4TH NVARCHAR(MAX) NULL,
		Image5TH NVARCHAR(MAX) NULL,
		Image6TH NVARCHAR(MAX) NULL,
		Image7TH NVARCHAR(MAX) NULL

            CONSTRAINT [PK_Image]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[ProductVariation]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[ProductVariation]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductId BIGINT NOT NULL,
		Theme VARCHAR(MAX) NOT NULL,
		Color  VARCHAR(MAX) NOT NULL,
		Size VARCHAR(MAX) NOT NULL,
		Plus1ST VARCHAR(MAX) NOT NULL,
		Plus2ND VARCHAR(MAX) NOT NULL,
		ShippingFrom VARCHAR(MAX) NULL

            CONSTRAINT [PK_Variation]
            PRIMARY KEY CLUSTERED ([Id] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                  ALLOW_PAGE_LOCKS = ON
                 ) ON [PRIMARY]
    ) ON [PRIMARY];

GO