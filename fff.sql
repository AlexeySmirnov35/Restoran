USE [master]
GO
/****** Object:  Database [Restoran]    Script Date: 05.12.2023 14:09:19 ******/
CREATE DATABASE [Restoran]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Restoran', FILENAME = N'C:\Users\10210150\Restoran.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Restoran_log', FILENAME = N'C:\Users\10210150\Restoran_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Restoran] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Restoran].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Restoran] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Restoran] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Restoran] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Restoran] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Restoran] SET ARITHABORT OFF 
GO
ALTER DATABASE [Restoran] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Restoran] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Restoran] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Restoran] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Restoran] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Restoran] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Restoran] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Restoran] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Restoran] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Restoran] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Restoran] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Restoran] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Restoran] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Restoran] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Restoran] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Restoran] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Restoran] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Restoran] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Restoran] SET  MULTI_USER 
GO
ALTER DATABASE [Restoran] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Restoran] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Restoran] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Restoran] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Restoran] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Restoran] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Restoran] SET QUERY_STORE = OFF
GO
USE [Restoran]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 05.12.2023 14:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ReservationsID] [int] NOT NULL,
	[FIO] [nvarchar](max) NULL,
	[TableID] [int] NULL,
	[UserID] [int] NULL,
	[NumberOfPeople] [int] NULL,
	[DateTimeReserv] [datetime] NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[ReservationsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05.12.2023 14:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tables]    Script Date: 05.12.2023 14:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tables](
	[TableID] [int] NOT NULL,
	[TableName] [nvarchar](50) NULL,
	[PeopleMax] [int] NULL,
 CONSTRAINT [PK_Tables] PRIMARY KEY CLUSTERED 
(
	[TableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[time]    Script Date: 05.12.2023 14:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[time](
	[дата] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05.12.2023 14:09:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Patranomic] [nvarchar](max) NULL,
	[login] [nvarchar](max) NULL,
	[passwod] [nvarchar](max) NULL,
	[RoleID] [int] NULL,
	[PhoneNumber] [nvarchar](12) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'User')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Surname], [Name], [Patranomic], [login], [passwod], [RoleID], [PhoneNumber]) VALUES (0, N'dsf', N'dsf', NULL, N'dsf', N'dsf', 2, NULL)
INSERT [dbo].[User] ([UserID], [Surname], [Name], [Patranomic], [login], [passwod], [RoleID], [PhoneNumber]) VALUES (1, N'admin', N'admin', N'admin', N'Admin', N'Admin', 1, N'123')
INSERT [dbo].[User] ([UserID], [Surname], [Name], [Patranomic], [login], [passwod], [RoleID], [PhoneNumber]) VALUES (2, N'user', N'user', N'user', N'user', N'user', 2, N'321')
INSERT [dbo].[User] ([UserID], [Surname], [Name], [Patranomic], [login], [passwod], [RoleID], [PhoneNumber]) VALUES (3, N'dd', N'dd', NULL, N'dd', N'dd', 2, NULL)
INSERT [dbo].[User] ([UserID], [Surname], [Name], [Patranomic], [login], [passwod], [RoleID], [PhoneNumber]) VALUES (4, N'cc', N'cc', NULL, N'c', N'c', 2, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Tables] FOREIGN KEY([TableID])
REFERENCES [dbo].[Tables] ([TableID])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Tables]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [Restoran] SET  READ_WRITE 
GO
