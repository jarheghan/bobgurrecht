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
CREATE TABLE dbo.OrderItem
	(
	ort int NOT NULL IDENTITY (1, 1),
	ort_guid uniqueidentifier NULL,
	ort_ord_id int NULL,
	ort_prd_id int NULL,
	ort_quantity int NULL,
	ort_prv_id int NULL,
	ort_add_user nvarchar(200) NULL,
	ort_add_date datetime NULL,
	ort_change_user nvarchar(200) NULL,
	ort_change_date datetime NULL,
	ort_delete_flag bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.OrderItem ADD CONSTRAINT
	PK_OrderItem PRIMARY KEY CLUSTERED 
	(
	ort
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.OrderItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'CONTROL') as Contr_Per 



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
CREATE TABLE dbo.[Order]
	(
	ord_id intNOT NULL IDENTITY (1, 1),
	ord_number nvarchar(4000) NULL,
	ord_guid uniqueidentifier NULL,
	ord_usr_id int NULL,
	ord_add_user nvarchar(200) NULL,
	ord_add_date datetime NULL,
	ord_change_user nvarchar(200) NULL,
	ord_change_date datetime NULL,
	ord_delete_flag bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	PK_Order PRIMARY KEY CLUSTERED 
	(
	ort
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Order] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'CONTROL') as Contr_Per 


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
CREATE TABLE dbo.Tmp_Order
	(
	ord_id int NOT NULL IDENTITY (1, 1),
	ord_number nvarchar(4000) NULL,
	ord_guid uniqueidentifier NULL,
	ord_usr_id int NULL,
	ord_add_user nvarchar(200) NULL,
	ord_add_date datetime NULL,
	ord_change_user nvarchar(200) NULL,
	ord_change_date datetime NULL,
	ord_delete_flag bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Order SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Order ON
GO
IF EXISTS(SELECT * FROM dbo.[Order])
	 EXEC('INSERT INTO dbo.Tmp_Order (ord_id, ord_number, ord_guid, ord_usr_id, ord_add_user, ord_add_date, ord_change_user, ord_change_date, ord_delete_flag)
		SELECT ord_id, ord_number, ord_guid, ord_usr_id, ord_add_user, ord_add_date, ord_change_user, ord_change_date, ord_delete_flag FROM dbo.[Order] WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Order OFF
GO
DROP TABLE dbo.[Order]
GO
EXECUTE sp_rename N'dbo.Tmp_Order', N'Order', 'OBJECT' 
GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	PK_Order PRIMARY KEY CLUSTERED 
	(
	ord_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'CONTROL') as Contr_Per 




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
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Users', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CustomerInfo ADD CONSTRAINT
	FK_CustomerInfo_Users FOREIGN KEY
	(
	cus_id
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CustomerInfo SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CustomerInfo', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CustomerInfo', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CustomerInfo', 'Object', 'CONTROL') as Contr_Per 




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
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Users', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Products', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Products', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	FK_Order_Users FOREIGN KEY
	(
	ord_usr_id
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.[Order] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderItem
	DROP CONSTRAINT FK_OrderItem_OrderItem
GO
ALTER TABLE dbo.OrderItem ADD CONSTRAINT
	FK_OrderItem_OrderItem FOREIGN KEY
	(
	ort_ord_id
	) REFERENCES dbo.[Order]
	(
	ord_id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderItem ADD CONSTRAINT
	FK_OrderItem_Products FOREIGN KEY
	(
	ort_prd_id
	) REFERENCES dbo.Products
	(
	prd_id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'CONTROL') as Contr_Per 



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
ALTER TABLE dbo.[Order]
	DROP CONSTRAINT FK_Order_Users
GO
ALTER TABLE dbo.Users SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Users', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Users', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Order
	(
	ord_id int NOT NULL IDENTITY (1, 1),
	ord_number nvarchar(4000) NULL,
	ord_guid uniqueidentifier NULL,
	ord_active bit NULL,
	ord_usr_id int NULL,
	ord_add_user nvarchar(200) NULL,
	ord_add_date datetime NULL,
	ord_change_user nvarchar(200) NULL,
	ord_change_date datetime NULL,
	ord_delete_flag bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Order SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Order ON
GO
IF EXISTS(SELECT * FROM dbo.[Order])
	 EXEC('INSERT INTO dbo.Tmp_Order (ord_id, ord_number, ord_guid, ord_usr_id, ord_add_user, ord_add_date, ord_change_user, ord_change_date, ord_delete_flag)
		SELECT ord_id, ord_number, ord_guid, ord_usr_id, ord_add_user, ord_add_date, ord_change_user, ord_change_date, ord_delete_flag FROM dbo.[Order] WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Order OFF
GO
ALTER TABLE dbo.OrderItem
	DROP CONSTRAINT FK_OrderItem_OrderItem
GO
DROP TABLE dbo.[Order]
GO
EXECUTE sp_rename N'dbo.Tmp_Order', N'Order', 'OBJECT' 
GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	PK_Order PRIMARY KEY CLUSTERED 
	(
	ord_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Order] ADD CONSTRAINT
	FK_Order_Users FOREIGN KEY
	(
	ord_usr_id
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Order]', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.OrderItem ADD CONSTRAINT
	FK_OrderItem_OrderItem FOREIGN KEY
	(
	ort_ord_id
	) REFERENCES dbo.[Order]
	(
	ord_id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.OrderItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.OrderItem', 'Object', 'CONTROL') as Contr_Per 
