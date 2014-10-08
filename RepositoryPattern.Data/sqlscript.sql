﻿/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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