USE TaxiVerificationAI

USE [TaxiVerificationAI]
GO

/****** Object:  Table [dbo].[Agents]    Script Date: 24/10/2023 05:06:15 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Agents](
	[IdAgent] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[IdUser] [int] NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAgent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Agents]  WITH CHECK ADD FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO

/****** Object:  Table [dbo].[Brands]    Script Date: 24/10/2023 05:07:06 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Brands](
	[IdBrand] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBrand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Colors]    Script Date: 24/10/2023 05:07:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Colors](
	[IdColor] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Models]    Script Date: 24/10/2023 05:08:03 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Models](
	[IdModel] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdModel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 24/10/2023 05:08:33 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxiDrivers]    Script Date: 24/10/2023 05:09:01 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaxiDrivers](
	[IdTaxiDriver] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DriverLicense] [varchar](100) NULL,
	[IdTaxi] [int] NULL,
	[IdUser] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTaxiDriver] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TaxiDrivers]  WITH CHECK ADD FOREIGN KEY([IdTaxi])
REFERENCES [dbo].[Taxis] ([IdTaxi])
GO

ALTER TABLE [dbo].[TaxiDrivers]  WITH CHECK ADD FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO

/****** Object:  Table [dbo].[Taxis]    Script Date: 24/10/2023 05:09:27 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Taxis](
	[IdTaxi] [int] IDENTITY(1,1) NOT NULL,
	[IdBrand] [int] NULL,
	[IdModel] [int] NULL,
	[Number] [int] NULL,
	[Plate] [varchar](50) NULL,
	[IdColor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTaxi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Taxis]  WITH CHECK ADD FOREIGN KEY([IdBrand])
REFERENCES [dbo].[Brands] ([IdBrand])
GO

ALTER TABLE [dbo].[Taxis]  WITH CHECK ADD FOREIGN KEY([IdColor])
REFERENCES [dbo].[Colors] ([IdColor])
GO

ALTER TABLE [dbo].[Taxis]  WITH CHECK ADD FOREIGN KEY([IdModel])
REFERENCES [dbo].[Models] ([IdModel])
GO

/****** Object:  Table [dbo].[Users]    Script Date: 24/10/2023 05:09:59 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](100) NULL,
	[IdRol] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([IdRol])
GO

/****** Object:  Table [dbo].[Verifications]    Script Date: 24/10/2023 05:10:30 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Verifications](
	[IdVerification] [int] IDENTITY(1,1) NOT NULL,
	[Folio] [varchar](50) NULL,
	[IdTaxi] [int] NULL,
	[IdTaxiDriver] [int] NULL,
	[IdAgent] [int] NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVerification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Verifications]  WITH CHECK ADD FOREIGN KEY([IdAgent])
REFERENCES [dbo].[Agents] ([IdAgent])
GO

ALTER TABLE [dbo].[Verifications]  WITH CHECK ADD FOREIGN KEY([IdTaxi])
REFERENCES [dbo].[Taxis] ([IdTaxi])
GO

ALTER TABLE [dbo].[Verifications]  WITH CHECK ADD FOREIGN KEY([IdTaxiDriver])
REFERENCES [dbo].[TaxiDrivers] ([IdTaxiDriver])
GO

/****** Object:  Table [dbo].[VerificationsImages]    Script Date: 24/10/2023 05:10:57 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VerificationsImages](
	[IdVerificationImages] [int] IDENTITY(1,1) NOT NULL,
	[FrontalImage] [varbinary](1) NULL,
	[LeftSideImage] [varbinary](1) NULL,
	[RightSideImage] [varbinary](1) NULL,
	[IdVerification] [int] NULL,
	[CreateDate] [datetime] NULL,
	[FrontImageName] [nvarchar](50) NULL,
	[LeftImageName] [nvarchar](50) NULL,
	[RightImageName] [nvarchar](50) NULL,
	[FrontImageUrl] [nvarchar](max) NULL,
	[LeftImageUrl] [nvarchar](max) NULL,
	[RightImageUrl] [nvarchar](max) NULL,
	[AcceptCompliance] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVerificationImages] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[VerificationsImages]  WITH CHECK ADD FOREIGN KEY([IdVerification])
REFERENCES [dbo].[Verifications] ([IdVerification])
GO

/****** Object:  Table [dbo].[VerificationsResults]    Script Date: 24/10/2023 05:11:21 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VerificationsResults](
	[IdVerificationResult] [int] IDENTITY(1,1) NOT NULL,
	[IsPlate] [bit] NULL,
	[PlateMatchAvg] [decimal](18, 0) NULL,
	[IsLabels] [bit] NULL,
	[LabelsMatchAvg] [decimal](18, 0) NULL,
	[IsColor] [bit] NULL,
	[ColorMatchAvg] [decimal](18, 0) NULL,
	[IsApproved] [bit] NULL,
	[IdVerification] [int] NULL,
	[CreateDate] [datetime] NULL,
	[VerificationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVerificationResult] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VerificationsResults]  WITH CHECK ADD FOREIGN KEY([IdVerification])
REFERENCES [dbo].[Verifications] ([IdVerification])
GO