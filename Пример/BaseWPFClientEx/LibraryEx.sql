USE [master]
GO
/****** Object:  Database [LibraryEx]    Script Date: 09.05.2021 22:46:58 ******/
CREATE DATABASE [LibraryEx]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibraryEx', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LibraryEx.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibraryEx_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LibraryEx_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LibraryEx] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibraryEx].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibraryEx] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibraryEx] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibraryEx] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibraryEx] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibraryEx] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibraryEx] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibraryEx] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibraryEx] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibraryEx] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibraryEx] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibraryEx] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibraryEx] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibraryEx] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibraryEx] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibraryEx] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibraryEx] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibraryEx] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibraryEx] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibraryEx] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibraryEx] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibraryEx] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibraryEx] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibraryEx] SET RECOVERY FULL 
GO
ALTER DATABASE [LibraryEx] SET  MULTI_USER 
GO
ALTER DATABASE [LibraryEx] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibraryEx] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibraryEx] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibraryEx] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibraryEx] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibraryEx] SET QUERY_STORE = OFF
GO
USE [LibraryEx]
GO
USE [LibraryEx]
GO
/****** Object:  Sequence [dbo].[ImageCode]    Script Date: 09.05.2021 22:46:58 ******/
CREATE SEQUENCE [dbo].[ImageCode] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[Books]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[IdBook] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Authors] [nvarchar](250) NULL,
	[PublishYear] [smallint] NULL,
	[IdPublisher] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[IdBook] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Copies]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Copies](
	[IdCopy] [int] IDENTITY(100,1) NOT NULL,
	[IdBook] [int] NULL,
	[InvNum] [nvarchar](12) NULL,
	[CommissionDate] [date] NULL,
	[DecommissionDate] [date] NULL,
 CONSTRAINT [PK_Copies] PRIMARY KEY CLUSTERED 
(
	[IdCopy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[IdPublisher] [int] IDENTITY(1,100) NOT NULL,
	[PublisherName] [nvarchar](250) NULL,
 CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED 
(
	[IdPublisher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Readers](
	[IdReader] [int] IDENTITY(100,1) NOT NULL,
	[Surname] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Birthdate] [date] NULL,
	[IdSex] [int] NULL,
	[Photo] [nvarchar](255) NULL,
 CONSTRAINT [PK_Readers] PRIMARY KEY CLUSTERED 
(
	[IdReader] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Readings]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Readings](
	[IdReading] [int] IDENTITY(1,1) NOT NULL,
	[IdReader] [int] NULL,
	[IdCopy] [int] NULL,
	[BeginDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_Readings] PRIMARY KEY CLUSTERED 
(
	[IdReading] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRole] [int] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sexes]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sexes](
	[IdSex] [int] NOT NULL,
	[Name] [nvarchar](10) NULL,
	[ShortName] [nvarchar](1) NULL,
 CONSTRAINT [PK_Sexes] PRIMARY KEY CLUSTERED 
(
	[IdSex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[FriendlyName] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IdRole] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (1, N'Базы данных', N'Петров А', 1990, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (2, N'Архитектура ЭВМ', N'Сидоров И А', 2005, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (3, N'Готовка в домашних условиях', N'Ромова И Е', 1999, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (4, N'Информационные технологии', N'Топов П Р, Алексеева И Т', 2000, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (5, N'Идиот', N'Достоевский Ф М', 1974, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (100, N'Базы данных', N'Цветкова Н К', 2012, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (101, N'Рисование в Photoshop', N'Петров И В', 2000, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (102, N'Обслуживание печатных устройств', N'Рокин П Н, Гудков В С', 2000, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (103, N'Лоренцо', N'Ложкин Е В', 2000, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (108, N'Информатика в школе', N'Иванов И А', 2010, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (109, N'Электротехника', N'Румянцева Н Г', 2001, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (110, N'Как закалялась сталь', N'Островский Н А', 2001, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (117, N'Короли', N'Заливов Е Н', 2003, 2)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (118, N'Мир наизнанку', N'Петров Е А', 2005, 1)
INSERT [dbo].[Books] ([IdBook], [Name], [Authors], [PublishYear], [IdPublisher]) VALUES (1101, N'Экономическая география для 11 класса', N'Дудкин Г Ф', NULL, 2)
SET IDENTITY_INSERT [dbo].[Books] OFF
SET IDENTITY_INSERT [dbo].[Copies] ON 

INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (1, 1, N'000001', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (2, 1, N'000002', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (3, 1, N'000003', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (4, 1, N'000004', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (5, 2, N'000005', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (6, 3, N'000006', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (7, 3, N'000007', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (8, 3, N'000008', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (9, 4, N'000009', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (10, 5, N'000010', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (11, 5, N'000011', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (12, 5, N'000012', CAST(N'2015-01-01' AS Date), NULL)
INSERT [dbo].[Copies] ([IdCopy], [IdBook], [InvNum], [CommissionDate], [DecommissionDate]) VALUES (13, 4, N'000013', CAST(N'2015-01-01' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[Copies] OFF
SET IDENTITY_INSERT [dbo].[Publishers] ON 

INSERT [dbo].[Publishers] ([IdPublisher], [PublisherName]) VALUES (-1, N' ...')
INSERT [dbo].[Publishers] ([IdPublisher], [PublisherName]) VALUES (1, N'Москва')
INSERT [dbo].[Publishers] ([IdPublisher], [PublisherName]) VALUES (2, N'BBK')
SET IDENTITY_INSERT [dbo].[Publishers] OFF
SET IDENTITY_INSERT [dbo].[Readers] ON 

INSERT [dbo].[Readers] ([IdReader], [Surname], [Name], [Patronymic], [Birthdate], [IdSex], [Photo]) VALUES (1, N'Иванов', N'Иван', N'Иванович', CAST(N'2000-04-12' AS Date), 1, NULL)
INSERT [dbo].[Readers] ([IdReader], [Surname], [Name], [Patronymic], [Birthdate], [IdSex], [Photo]) VALUES (2, N'Петрова', N'Ирина', N'Сергеевна', CAST(N'2002-08-15' AS Date), 2, NULL)
INSERT [dbo].[Readers] ([IdReader], [Surname], [Name], [Patronymic], [Birthdate], [IdSex], [Photo]) VALUES (102, N'Романов', N'Михаил', N'Константинович', CAST(N'2012-02-09' AS Date), 1, N'D:\C#\BaseWPFClientEx\BaseWPFClientEx\BaseWPFClientEx\bin\Debug\Images\29252562105052021')
INSERT [dbo].[Readers] ([IdReader], [Surname], [Name], [Patronymic], [Birthdate], [IdSex], [Photo]) VALUES (103, N'Кондратьева', N'Алевтина', N'Григорьевна', CAST(N'1998-08-05' AS Date), 2, N'D:\C#\BaseWPFClientEx\BaseWPFClientEx\BaseWPFClientEx\bin\Debug\Images\00101151805052021')
INSERT [dbo].[Readers] ([IdReader], [Surname], [Name], [Patronymic], [Birthdate], [IdSex], [Photo]) VALUES (108, N'Гусев', N'Тихон', N'Петрович', CAST(N'2021-05-05' AS Date), 1, NULL)
SET IDENTITY_INSERT [dbo].[Readers] OFF
SET IDENTITY_INSERT [dbo].[Readings] ON 

INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (1, 1, 3, CAST(N'2020-01-01' AS Date), CAST(N'2020-02-01' AS Date))
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (2, 1, 5, CAST(N'2020-01-01' AS Date), CAST(N'2020-01-15' AS Date))
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (3, 2, 5, CAST(N'2020-02-01' AS Date), CAST(N'2020-03-15' AS Date))
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (4, 102, 13, CAST(N'2020-11-05' AS Date), CAST(N'2020-11-10' AS Date))
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (5, 102, 10, CAST(N'2020-11-15' AS Date), CAST(N'2020-11-18' AS Date))
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (6, 2, 9, CAST(N'2020-06-05' AS Date), NULL)
INSERT [dbo].[Readings] ([IdReading], [IdReader], [IdCopy], [BeginDate], [EndDate]) VALUES (7, 2, 11, CAST(N'2020-06-05' AS Date), CAST(N'2020-07-07' AS Date))
SET IDENTITY_INSERT [dbo].[Readings] OFF
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (0, N'Гость')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (1, N'Библиотекарь')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (2, N'Заведующий библиотекой')
INSERT [dbo].[Roles] ([IdRole], [RoleName]) VALUES (3, N'Администратор')
INSERT [dbo].[Sexes] ([IdSex], [Name], [ShortName]) VALUES (-1, N' ...', N'')
INSERT [dbo].[Sexes] ([IdSex], [Name], [ShortName]) VALUES (1, N'Мужчина', N'М')
INSERT [dbo].[Sexes] ([IdSex], [Name], [ShortName]) VALUES (2, N'Женщина', N'Ж')
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([IdUser], [FriendlyName], [Login], [Password], [IdRole]) VALUES (0, N'Администратор', N'Adm', N'123', 3)
INSERT [dbo].[Users] ([IdUser], [FriendlyName], [Login], [Password], [IdRole]) VALUES (1, N'Старший библиотекарь', N'MainLibWorker', N'123', 2)
INSERT [dbo].[Users] ([IdUser], [FriendlyName], [Login], [Password], [IdRole]) VALUES (2, N'Младший библиотекарь', N'LibWorker', N'123', 1)
INSERT [dbo].[Users] ([IdUser], [FriendlyName], [Login], [Password], [IdRole]) VALUES (3, N'Гость', N'Guest', N'123', 0)
INSERT [dbo].[Users] ([IdUser], [FriendlyName], [Login], [Password], [IdRole]) VALUES (4, NULL, N'Test', N'123', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publishers] FOREIGN KEY([IdPublisher])
REFERENCES [dbo].[Publishers] ([IdPublisher])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publishers]
GO
ALTER TABLE [dbo].[Copies]  WITH CHECK ADD  CONSTRAINT [FK_Copies_Books] FOREIGN KEY([IdBook])
REFERENCES [dbo].[Books] ([IdBook])
GO
ALTER TABLE [dbo].[Copies] CHECK CONSTRAINT [FK_Copies_Books]
GO
ALTER TABLE [dbo].[Readers]  WITH CHECK ADD  CONSTRAINT [FK_Readers_Sexes] FOREIGN KEY([IdSex])
REFERENCES [dbo].[Sexes] ([IdSex])
GO
ALTER TABLE [dbo].[Readers] CHECK CONSTRAINT [FK_Readers_Sexes]
GO
ALTER TABLE [dbo].[Readings]  WITH CHECK ADD  CONSTRAINT [FK_Readings_Copies] FOREIGN KEY([IdCopy])
REFERENCES [dbo].[Copies] ([IdCopy])
GO
ALTER TABLE [dbo].[Readings] CHECK CONSTRAINT [FK_Readings_Copies]
GO
ALTER TABLE [dbo].[Readings]  WITH CHECK ADD  CONSTRAINT [FK_Readings_Readers] FOREIGN KEY([IdReader])
REFERENCES [dbo].[Readers] ([IdReader])
GO
ALTER TABLE [dbo].[Readings] CHECK CONSTRAINT [FK_Readings_Readers]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[GetNextImageId]    Script Date: 09.05.2021 22:46:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNextImageId] 
	-- Add the parameters for the stored procedure here
	@ImageId int output
AS
BEGIN
	SELECT @ImageId = NEXT VALUE FOR ImageCode;
END
GO
USE [master]
GO
ALTER DATABASE [LibraryEx] SET  READ_WRITE 
GO
