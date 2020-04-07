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
    WHERE object_id = OBJECT_ID(N'[dbo].[CategoryPath]')
          AND type IN ( N'U' )
)
    CREATE TABLE [dbo].[CategoryPath]
    (
        Id BIGINT IDENTITY(1, 1) NOT NULL,
		ProductId BIGINT NOT NULL,
        CategoryId BIGINT NOT NULL,
		CatMain NVARCHAR(MAX) NULL,
		CatSub1 NVARCHAR(MAX) NULL,
		CatSub2 NVARCHAR(MAX) NULL,
		CatSub3 NVARCHAR(MAX) NULL,
            CONSTRAINT [PK_CategoryPath]
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
		ProductId BIGINT NOT NULL

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
		ProductId BIGINT NOT NULL,
		ProductKeyId VARCHAR(MAX) NOT NULL,
        IsParent BIT  NULL DEFAULT 0,
		CategoryId BIGINT NULL,
		CategoryName NVARCHAR(MAX) NULL,
		StoreId BIGINT NULL,
		StoreName NVARCHAR(MAX) NULL,
		ProductName NVARCHAR(MAX) NOT NULL,
		[Description] NVARCHAR(MAX) NULL,
		BuyingPrice DECIMAL(18,2) NULL,
		ItemLot NVARCHAR(MAX) NULL,
		BrandName NVARCHAR(MAX) NULL,
		StockNumber BIGINT NULL,

		StoreYear NVARCHAR(MAX) NULL,
		StoreRatingPercent VARCHAR(20) NULL,
		StoreRatingDescribed DECIMAL(18,2) NULL,
		StoreRatingCommunication DECIMAL(18,2) NULL,
		StoreRatingShippingSpeed DECIMAL(18,2) NULL,
		StoreRatingTotal INT NULL,
		OrderNumber BIGINT NOT NULL,
		RatingNumber BIGINT NULL,
		RatingPercent DECIMAL NULL,
		ProcessingTime NVARCHAR(MAX) NULL,
		ShippingCompany NVARCHAR(MAX) NULL,
		ShippingFee DECIMAL(18,2) NULL,
		Shipping1ST NVARCHAR(MAX) NULL,
		Shipping2ND NVARCHAR(MAX) NULL,
		Shipping3RD NVARCHAR(MAX) NULL,
		OnTimeDelivery VARCHAR(50) NULL,
		TotalPrice DECIMAL(18,2) NULL,
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

		VariationTheme VARCHAR(MAX)  NULL,
		VariationColor  VARCHAR(MAX)  NULL,
		VariationSize VARCHAR(MAX)  NULL,
		VariationPlus1ST VARCHAR(MAX)  NULL,
		VariationPlus2ND VARCHAR(MAX)  NULL,
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