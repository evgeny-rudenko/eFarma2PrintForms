
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SGAU_REP_UZO_PACK]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[SGAU_REP_UZO_PACK](
	[prefix_pack] [varchar](10) NOT NULL,
	[id_pack] [bigint] NOT NULL,
	[number_pack] [varchar](100) NULL,
	[date_pack] [datetime] NULL,
	[balance] [bit] NULL,
	[id_medical] [bigint] NULL,
	[id_contractor_perform] [bigint] NULL,
	[id_contractor_custom] [bigint] NULL
) ON [PRIMARY]

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SGAU_REP_UZO_PACK_OPTIONS]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[SGAU_REP_UZO_PACK_OPTIONS](
	[mnemocode] [varchar](100) NULL,
	[value] [varchar](200) NULL
) ON [PRIMARY]

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SGAU_REP_UZO_PACK_PERCENT_SERVICE]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [dbo].[SGAU_REP_UZO_PACK_PERCENT_SERVICE](
	[id_percent_service] [bigint] IDENTITY(1,1) NOT NULL,
	[date_start] [datetime] NULL,
	[date_end] [datetime] NULL,
	[percent_service] [money] NULL,
	[mnemocode_store_type] [varchar](12) NULL,
	[id_discount2_medical_organization_global] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PERCENT_SERVICE] PRIMARY KEY CLUSTERED 
(
	[id_percent_service] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF