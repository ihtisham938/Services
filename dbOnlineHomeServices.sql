
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/07/2022 08:23:05
-- Generated from EDMX file: E:\bbbbbbbbbbbbbbbbbbb\DAL\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbOnlineHomeServices];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Tbl_Cart__Servic__38996AB5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_Cart] DROP CONSTRAINT [FK__Tbl_Cart__Servic__38996AB5];
GO
IF OBJECT_ID(N'[dbo].[FK__Tbl_Servi__Categ__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_Service] DROP CONSTRAINT [FK__Tbl_Servi__Categ__398D8EEE];
GO
IF OBJECT_ID(N'[dbo].[FK__Tbl_Shipp__Membe__3A81B327]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_ShippingDetails] DROP CONSTRAINT [FK__Tbl_Shipp__Membe__3A81B327];
GO
IF OBJECT_ID(N'[dbo].[FK_Tbl_Roles_Tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tbl_Roles] DROP CONSTRAINT [FK_Tbl_Roles_Tbl_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tbl_Cart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Cart];
GO
IF OBJECT_ID(N'[dbo].[Tbl_CartStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_CartStatus];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Category];
GO
IF OBJECT_ID(N'[dbo].[Tbl_MemberRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_MemberRole];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Members];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Orders];
GO
IF OBJECT_ID(N'[dbo].[Tbl_review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_review];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Roles];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Service];
GO
IF OBJECT_ID(N'[dbo].[Tbl_ShippingDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_ShippingDetails];
GO
IF OBJECT_ID(N'[dbo].[Tbl_SlideImage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_SlideImage];
GO
IF OBJECT_ID(N'[dbo].[Tbl_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tbl_Cart'
CREATE TABLE [dbo].[Tbl_Cart] (
    [CartId] int IDENTITY(1,1) NOT NULL,
    [ServiceId] int  NULL,
    [MemberId] int  NULL,
    [CartStatusId] int  NULL
);
GO

-- Creating table 'Tbl_CartStatus'
CREATE TABLE [dbo].[Tbl_CartStatus] (
    [CartStatusId] int IDENTITY(1,1) NOT NULL,
    [CartStatus] varchar(500)  NULL
);
GO

-- Creating table 'Tbl_Category'
CREATE TABLE [dbo].[Tbl_Category] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryName] varchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsDelete] bit  NULL
);
GO

-- Creating table 'Tbl_MemberRole'
CREATE TABLE [dbo].[Tbl_MemberRole] (
    [MemberRoleID] int IDENTITY(1,1) NOT NULL,
    [memberId] int  NULL,
    [RoleId] int  NULL
);
GO

-- Creating table 'Tbl_Members'
CREATE TABLE [dbo].[Tbl_Members] (
    [MemberId] int IDENTITY(1,1) NOT NULL,
    [FristName] varchar(200)  NULL,
    [LastName] varchar(200)  NULL,
    [EmailId] varchar(200)  NULL,
    [Password] varchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsDelete] bit  NULL,
    [CreatedOn] datetime  NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Tbl_Orders'
CREATE TABLE [dbo].[Tbl_Orders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [description] varchar(1000)  NULL,
    [SellerName] nvarchar(50)  NULL,
    [CustomerName] nvarchar(50)  NULL,
    [Status] nvarchar(50)  NULL,
    [Address] nvarchar(200)  NULL,
    [Long] varchar(50)  NULL,
    [Lat] varchar(50)  NULL,
    [Phone_number] nvarchar(50)  NULL,
    [Date] datetime  NULL,
    [orderprice] int  NULL
);
GO

-- Creating table 'Tbl_review'
CREATE TABLE [dbo].[Tbl_review] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Orderid] int  NULL,
    [rating] int  NULL,
    [Review] nvarchar(max)  NULL,
    [date] datetime  NULL,
    [reviwername] nvarchar(50)  NULL,
    [reviewname] nvarchar(50)  NULL
);
GO

-- Creating table 'Tbl_Roles'
CREATE TABLE [dbo].[Tbl_Roles] (
    [id] int  NOT NULL,
    [UserId] int  NOT NULL,
    [RoleName] varchar(200)  NOT NULL
);
GO

-- Creating table 'Tbl_Service'
CREATE TABLE [dbo].[Tbl_Service] (
    [ServiceId] int IDENTITY(1,1) NOT NULL,
    [ProductName] varchar(500)  NULL,
    [CategoryId] int  NULL,
    [IsActive] bit  NULL,
    [IsDelete] bit  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [Description] nvarchar(500)  NULL,
    [ServiceImage] varchar(max)  NULL,
    [IsFeatured] bit  NULL,
    [Quantity] int  NULL,
    [Price] decimal(18,0)  NULL,
    [Username] nvarchar(50)  NULL
);
GO

-- Creating table 'Tbl_ShippingDetails'
CREATE TABLE [dbo].[Tbl_ShippingDetails] (
    [ShippingDetailId] int IDENTITY(1,1) NOT NULL,
    [MemberId] int  NULL,
    [Adress] varchar(500)  NULL,
    [City] varchar(500)  NULL,
    [State] varchar(500)  NULL,
    [Country] varchar(50)  NULL,
    [ZipCode] varchar(50)  NULL,
    [OrderId] int  NULL,
    [AmountPaid] decimal(18,0)  NULL,
    [PaymentType] varchar(50)  NULL
);
GO

-- Creating table 'Tbl_SlideImage'
CREATE TABLE [dbo].[Tbl_SlideImage] (
    [SlideId] int IDENTITY(1,1) NOT NULL,
    [SlideTitle] varchar(500)  NULL,
    [SlideImage] varchar(max)  NULL
);
GO

-- Creating table 'Tbl_User'
CREATE TABLE [dbo].[Tbl_User] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Email] nchar(50)  NULL,
    [Username] nchar(50)  NULL,
    [password] nchar(50)  NULL,
    [location] nchar(50)  NULL,
    [Profilepic] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CartId] in table 'Tbl_Cart'
ALTER TABLE [dbo].[Tbl_Cart]
ADD CONSTRAINT [PK_Tbl_Cart]
    PRIMARY KEY CLUSTERED ([CartId] ASC);
GO

-- Creating primary key on [CartStatusId] in table 'Tbl_CartStatus'
ALTER TABLE [dbo].[Tbl_CartStatus]
ADD CONSTRAINT [PK_Tbl_CartStatus]
    PRIMARY KEY CLUSTERED ([CartStatusId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Tbl_Category'
ALTER TABLE [dbo].[Tbl_Category]
ADD CONSTRAINT [PK_Tbl_Category]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [MemberRoleID] in table 'Tbl_MemberRole'
ALTER TABLE [dbo].[Tbl_MemberRole]
ADD CONSTRAINT [PK_Tbl_MemberRole]
    PRIMARY KEY CLUSTERED ([MemberRoleID] ASC);
GO

-- Creating primary key on [MemberId] in table 'Tbl_Members'
ALTER TABLE [dbo].[Tbl_Members]
ADD CONSTRAINT [PK_Tbl_Members]
    PRIMARY KEY CLUSTERED ([MemberId] ASC);
GO

-- Creating primary key on [id] in table 'Tbl_Orders'
ALTER TABLE [dbo].[Tbl_Orders]
ADD CONSTRAINT [PK_Tbl_Orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Tbl_review'
ALTER TABLE [dbo].[Tbl_review]
ADD CONSTRAINT [PK_Tbl_review]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Tbl_Roles'
ALTER TABLE [dbo].[Tbl_Roles]
ADD CONSTRAINT [PK_Tbl_Roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ServiceId] in table 'Tbl_Service'
ALTER TABLE [dbo].[Tbl_Service]
ADD CONSTRAINT [PK_Tbl_Service]
    PRIMARY KEY CLUSTERED ([ServiceId] ASC);
GO

-- Creating primary key on [ShippingDetailId] in table 'Tbl_ShippingDetails'
ALTER TABLE [dbo].[Tbl_ShippingDetails]
ADD CONSTRAINT [PK_Tbl_ShippingDetails]
    PRIMARY KEY CLUSTERED ([ShippingDetailId] ASC);
GO

-- Creating primary key on [SlideId] in table 'Tbl_SlideImage'
ALTER TABLE [dbo].[Tbl_SlideImage]
ADD CONSTRAINT [PK_Tbl_SlideImage]
    PRIMARY KEY CLUSTERED ([SlideId] ASC);
GO

-- Creating primary key on [id] in table 'Tbl_User'
ALTER TABLE [dbo].[Tbl_User]
ADD CONSTRAINT [PK_Tbl_User]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ServiceId] in table 'Tbl_Cart'
ALTER TABLE [dbo].[Tbl_Cart]
ADD CONSTRAINT [FK__Tbl_Cart__Servic__38996AB5]
    FOREIGN KEY ([ServiceId])
    REFERENCES [dbo].[Tbl_Service]
        ([ServiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Tbl_Cart__Servic__38996AB5'
CREATE INDEX [IX_FK__Tbl_Cart__Servic__38996AB5]
ON [dbo].[Tbl_Cart]
    ([ServiceId]);
GO

-- Creating foreign key on [CategoryId] in table 'Tbl_Service'
ALTER TABLE [dbo].[Tbl_Service]
ADD CONSTRAINT [FK__Tbl_Servi__Categ__398D8EEE]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Tbl_Category]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Tbl_Servi__Categ__398D8EEE'
CREATE INDEX [IX_FK__Tbl_Servi__Categ__398D8EEE]
ON [dbo].[Tbl_Service]
    ([CategoryId]);
GO

-- Creating foreign key on [MemberId] in table 'Tbl_ShippingDetails'
ALTER TABLE [dbo].[Tbl_ShippingDetails]
ADD CONSTRAINT [FK__Tbl_Shipp__Membe__3A81B327]
    FOREIGN KEY ([MemberId])
    REFERENCES [dbo].[Tbl_Members]
        ([MemberId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Tbl_Shipp__Membe__3A81B327'
CREATE INDEX [IX_FK__Tbl_Shipp__Membe__3A81B327]
ON [dbo].[Tbl_ShippingDetails]
    ([MemberId]);
GO

-- Creating foreign key on [UserId] in table 'Tbl_Roles'
ALTER TABLE [dbo].[Tbl_Roles]
ADD CONSTRAINT [FK_Tbl_Roles_Tbl_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Tbl_User]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tbl_Roles_Tbl_User'
CREATE INDEX [IX_FK_Tbl_Roles_Tbl_User]
ON [dbo].[Tbl_Roles]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------