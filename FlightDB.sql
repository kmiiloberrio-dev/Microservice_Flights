USE [master]
GO
/****** Object:  Database [FlightDB]    Script Date: 17/02/2021 18:17:42 ******/
CREATE DATABASE [FlightDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlightDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FlightDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlightDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\FlightDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FlightDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlightDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlightDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlightDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlightDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlightDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlightDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlightDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FlightDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlightDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlightDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlightDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FlightDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlightDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlightDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlightDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlightDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FlightDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlightDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlightDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlightDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FlightDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlightDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FlightDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlightDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FlightDB] SET  MULTI_USER 
GO
ALTER DATABASE [FlightDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FlightDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlightDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FlightDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FlightDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FlightDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FlightDB] SET QUERY_STORE = OFF
GO
USE [FlightDB]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 17/02/2021 18:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client_Flight]    Script Date: 17/02/2021 18:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client_Flight](
	[Client_Id] [int] NOT NULL,
	[Flight_Id] [int] NOT NULL,
 CONSTRAINT [PK_Client_Flight] PRIMARY KEY CLUSTERED 
(
	[Client_Id] ASC,
	[Flight_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 17/02/2021 18:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartureStation] [nvarchar](100) NULL,
	[ArrivalStation] [nvarchar](100) NULL,
	[DepartureDate] [date] NULL,
	[Price] [decimal](18, 2) NULL,
	[Currency] [nvarchar](50) NULL,
	[Transport_Id] [int] NOT NULL,
 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transport]    Script Date: 17/02/2021 18:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlightNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_Transport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [FlightDB] SET  READ_WRITE 
GO
