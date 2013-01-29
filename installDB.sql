
USE [Pryzmat_2013]
/****** Object:  Table [dbo].[Clients]    Script Date: 01/29/2013 17:39:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NULL,
	[city] [text] NULL,
	[address] [text] NULL,
	[nip] [text] NULL,
	[zip] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

USE [Pryzmat_2013]
GO

/****** Object:  Table [dbo].[Invoices]    Script Date: 01/29/2013 17:39:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[clientid] [int] NULL,
	[netto] [decimal](18, 2) NULL,
	[description] [text] NULL,
	[datesell] [text] NULL,
	[datepay] [text] NULL,
	[typeofpay] [text] NULL,
	[symbol] [text] NULL,
	[number] [text] NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
