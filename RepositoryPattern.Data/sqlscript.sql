/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProductVariation ADD
	prv_add_user nvarchar(100) NULL,
	prv_add_date datetime NULL,
	prv_change_user nvarchar(100) NULL,
	prv_change_date datetime NULL,
	prv_delete_flag bit NULL
GO
ALTER TABLE dbo.ProductVariation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'CONTROL') as Contr_Per 









BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProductVariation
	DROP CONSTRAINT FK_ProductVariation_Products
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Products', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ProductVariation
	(
	prv_id int NOT NULL IDENTITY (1, 1),
	prv_prd_id int NULL,
	prv_description nvarchar(100) NULL,
	prv_size nvarchar(50) NULL,
	prv_type nvarchar(200) NULL,
	prv_color nvarchar(50) NULL,
	prv_add_user nvarchar(100) NULL,
	prv_add_date datetime NULL,
	prv_change_user nvarchar(100) NULL,
	prv_change_date datetime NULL,
	prv_delete_flag bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ProductVariation SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_ProductVariation ON
GO
IF EXISTS(SELECT * FROM dbo.ProductVariation)
	 EXEC('INSERT INTO dbo.Tmp_ProductVariation (prv_id, prv_prd_id, prv_description, prv_size, prv_type, prv_color, prv_add_user, prv_add_date, prv_change_user, prv_change_date, prv_delete_flag)
		SELECT prv_id, prv_prd_id, prv_description, CONVERT(nvarchar(50), prv_size), prv_type, prv_color, prv_add_user, prv_add_date, prv_change_user, prv_change_date, prv_delete_flag FROM dbo.ProductVariation WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_ProductVariation OFF
GO
DROP TABLE dbo.ProductVariation
GO
EXECUTE sp_rename N'dbo.Tmp_ProductVariation', N'ProductVariation', 'OBJECT' 
GO
ALTER TABLE dbo.ProductVariation ADD CONSTRAINT
	FK_ProductVariation_Products FOREIGN KEY
	(
	prv_prd_id
	) REFERENCES dbo.Products
	(
	prd_id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProductVariation', 'Object', 'CONTROL') as Contr_Per 



USE [RPStore]
GO

/****** Object:  Table [dbo].[CustomerInfo]    Script Date: 10/13/2014 5:32:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerInfo](
	[cus_id] [int] NOT NULL,
	[cus_guid] [uniqueidentifier] NULL,
	[cus_email] [nvarchar](1000) NULL,
	[cus_first_name] [nvarchar](200) NULL,
	[cus_last_name] [nvarchar](200) NULL,
	[cus_address1] [nvarchar](300) NULL,
	[cus_address2] [nvarchar](200) NULL,
	[cus_address3] [nvarchar](200) NULL,
	[cus_city] [nvarchar](100) NULL,
	[cus_state] [nvarchar](100) NULL,
	[cus_country] [nvarchar](100) NULL,
	[cus_zip] [nvarchar](20) NULL,
	[cus_phone] [nvarchar](20) NULL,
	[cus_company] [nvarchar](100) NOT NULL,
	[cus_admin_comment] [nvarchar](2000) NULL,
	[cus_active] [bit] NULL,
	[cus_add_user] [nvarchar](100) NULL,
	[cus_add_date] [datetime] NULL,
	[cus_change_user] [nvarchar](100) NULL,
	[cus_change_date] [datetime] NULL,
	[cus_delete_flag] [bit] NULL,
 CONSTRAINT [Customer_PK_Customer] PRIMARY KEY CLUSTERED 
(
	[cus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


