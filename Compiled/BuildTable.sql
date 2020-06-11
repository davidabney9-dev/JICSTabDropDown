USE [ICS_NET]
GO

/****** Object:  Table [dbo].[CUS_CNTR_TabDropDownSettings]    Script Date: 2/26/2018 2:20:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CUS_CNTR_TabDropDownSettings](
	[SettingID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_CUS_CNTR_TabDropDownSettings_SettingID]  DEFAULT (newid()),
	[EnableDropDown] [varchar](1) NULL,
	[DisplayPages] [varchar](1) NULL,
	[DisplaySubSections] [varchar](1) NULL,
	[DisplaySubPagesPortlets] [varchar](1) NULL,
	[DisplayAlphaOrder] [varchar](1) NULL,
 CONSTRAINT [PK_CUS_CNTR_TabDropDownSettings] PRIMARY KEY CLUSTERED 
(
	[SettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


