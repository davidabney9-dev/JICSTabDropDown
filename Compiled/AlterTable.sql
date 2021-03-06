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
ALTER TABLE dbo.CUS_CNTR_TabDropDownSettings
	DROP CONSTRAINT DF_CUS_CNTR_TabDropDownSettings_SettingID
GO
CREATE TABLE dbo.Tmp_CUS_CNTR_TabDropDownSettings
	(
	SettingID uniqueidentifier NOT NULL,
	EnableDropDown varchar(1) NULL,
	DisplayPages varchar(1) NULL,
	DisplaySubSections varchar(1) NULL,
	DisplaySubPagesPortlets varchar(1) NULL,
	DisplayAlphaOrder varchar(1) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_CUS_CNTR_TabDropDownSettings SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_CUS_CNTR_TabDropDownSettings ADD CONSTRAINT
	DF_CUS_CNTR_TabDropDownSettings_SettingID DEFAULT (newid()) FOR SettingID
GO
IF EXISTS(SELECT * FROM dbo.CUS_CNTR_TabDropDownSettings)
	 EXEC('INSERT INTO dbo.Tmp_CUS_CNTR_TabDropDownSettings (SettingID, EnableDropDown, DisplayPages, DisplaySubSections, DisplayAlphaOrder)
		SELECT SettingID, EnableDropDown, DisplayPages, DisplaySubSections, DisplayAlphaOrder FROM dbo.CUS_CNTR_TabDropDownSettings WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.CUS_CNTR_TabDropDownSettings
GO
EXECUTE sp_rename N'dbo.Tmp_CUS_CNTR_TabDropDownSettings', N'CUS_CNTR_TabDropDownSettings', 'OBJECT' 
GO
ALTER TABLE dbo.CUS_CNTR_TabDropDownSettings ADD CONSTRAINT
	PK_CUS_CNTR_TabDropDownSettings PRIMARY KEY CLUSTERED 
	(
	SettingID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
