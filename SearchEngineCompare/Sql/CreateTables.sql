USE [Experiment]
GO

/****** Object:  Table [sec].[TempTest]    Script Date: 20.06.2021 13:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [sec].[TempTest](
	[TempTestId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TempTest] PRIMARY KEY CLUSTERED 
(
	[TempTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [sec].[Searches]    Script Date: 20.06.2021 13:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [sec].[Searches](
	[SearchId] [uniqueidentifier] NOT NULL,
	[SearchCompareId] [uniqueidentifier] NOT NULL,
	[SearchPhrase] [nvarchar](max) NULL,
	[PhraseTypeId] [int] NOT NULL,
	[SearchEngineId] [int] NOT NULL,
	[MeasuredParameterId] [int] NOT NULL,
	[MeasuredParameterPerformance] [float] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Searches] PRIMARY KEY CLUSTERED 
(
	[SearchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [sec].[SearchEngines]    Script Date: 20.06.2021 13:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [sec].[SearchEngines](
	[SearchEngineId] [int] NOT NULL,
	[SearchEngineDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SearchEngines] PRIMARY KEY CLUSTERED 
(
	[SearchEngineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [sec].[PhraseTypes]    Script Date: 20.06.2021 13:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [sec].[PhraseTypes](
	[PhraseTypeId] [int] NOT NULL,
	[PhraseTypeDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PhraseTypes] PRIMARY KEY CLUSTERED 
(
	[PhraseTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [sec].[MeasuredParameters]    Script Date: 20.06.2021 13:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [sec].[MeasuredParameters](
	[MeasuredParameterId] [int] NOT NULL,
	[MeasuredParameterDescription] [nvarchar](100) NULL,
 CONSTRAINT [PK_MeasuredParameters] PRIMARY KEY CLUSTERED 
(
	[MeasuredParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [sec].[TempTest] ADD  CONSTRAINT [DF_TempTest_TempTestId]  DEFAULT (newid()) FOR [TempTestId]
GO

ALTER TABLE [sec].[TempTest] ADD  CONSTRAINT [DF_TempTest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [sec].[Searches] ADD  CONSTRAINT [DF_Searches_SearchId]  DEFAULT (newid()) FOR [SearchId]
GO

ALTER TABLE [sec].[Searches] ADD  CONSTRAINT [DF_Searches_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [sec].[Searches]  WITH CHECK ADD  CONSTRAINT [FK_Searches_PhraseTypes] FOREIGN KEY([PhraseTypeId])
REFERENCES [sec].[PhraseTypes] ([PhraseTypeId])
GO

ALTER TABLE [sec].[Searches] CHECK CONSTRAINT [FK_Searches_PhraseTypes]
GO

ALTER TABLE [sec].[Searches]  WITH CHECK ADD  CONSTRAINT [FK_Searches_SearchEngines] FOREIGN KEY([SearchEngineId])
REFERENCES [sec].[SearchEngines] ([SearchEngineId])
GO

ALTER TABLE [sec].[Searches] CHECK CONSTRAINT [FK_Searches_SearchEngines]
GO

ALTER TABLE [sec].[Searches]  WITH CHECK ADD  CONSTRAINT [FK_Searches_Searches] FOREIGN KEY([MeasuredParameterId])
REFERENCES [sec].[MeasuredParameters] ([MeasuredParameterId])
GO

ALTER TABLE [sec].[Searches] CHECK CONSTRAINT [FK_Searches_Searches]
GO


