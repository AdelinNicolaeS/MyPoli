USE [master]
GO
/****** Object:  Database [MyPoli]    Script Date: 17.11.2022 13:16:22 ******/
CREATE DATABASE [MyPoli]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyPoli_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MyPoli.mdf' , SIZE = 29696KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyPoli_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MyPoli.ldf' , SIZE = 16064KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MyPoli] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyPoli].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyPoli] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyPoli] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyPoli] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyPoli] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyPoli] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyPoli] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyPoli] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyPoli] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyPoli] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyPoli] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyPoli] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyPoli] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyPoli] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyPoli] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyPoli] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyPoli] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyPoli] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyPoli] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyPoli] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyPoli] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyPoli] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyPoli] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyPoli] SET RECOVERY FULL 
GO
ALTER DATABASE [MyPoli] SET  MULTI_USER 
GO
ALTER DATABASE [MyPoli] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyPoli] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyPoli] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyPoli] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyPoli] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyPoli] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyPoli', N'ON'
GO
ALTER DATABASE [MyPoli] SET QUERY_STORE = OFF
GO
USE [MyPoli]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certificate](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[Date] [date] NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Certificate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Circumstance]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Circumstance](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Accepted] [bit] NOT NULL,
 CONSTRAINT [PK_Circumstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [uniqueidentifier] NOT NULL,
	[IdSubject] [uniqueidentifier] NOT NULL,
	[IdStudent] [uniqueidentifier] NOT NULL,
	[LectureOpinion] [nvarchar](max) NULL,
	[LectureGrade] [int] NOT NULL,
	[SeminarOpinion] [nvarchar](max) NULL,
	[SeminarGrade] [int] NOT NULL,
	[DateTime] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[IdSubject] [uniqueidentifier] NOT NULL,
	[IdStudent] [uniqueidentifier] NOT NULL,
	[Grade] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IdGroup] [uniqueidentifier] NOT NULL,
	[IdTeacher] [uniqueidentifier] NOT NULL,
 CONSTRAINT [Pk_Grade] PRIMARY KEY CLUSTERED 
(
	[IdStudent] ASC,
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [uniqueidentifier] NOT NULL,
	[SpecializationId] [uniqueidentifier] NULL,
	[Name] [varchar](5) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nationality]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nationality](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](450) NULL,
	[NationalityId] [uniqueidentifier] NOT NULL,
	[GenderId] [uniqueidentifier] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonRoles]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonRoles](
	[PersonId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_PersonRoles] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Secretary]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Secretary](
	[Id] [uniqueidentifier] NOT NULL,
	[Salary] [decimal](15, 2) NULL,
	[Experience] [int] NULL,
 CONSTRAINT [PK_Secretary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecretarySpecialization]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecretarySpecialization](
	[IdSecretary] [uniqueidentifier] NOT NULL,
	[IdSpecialization] [uniqueidentifier] NOT NULL,
 CONSTRAINT [Pk_SecretarySpecialization] PRIMARY KEY CLUSTERED 
(
	[IdSecretary] ASC,
	[IdSpecialization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [uniqueidentifier] NOT NULL,
	[GroupId] [uniqueidentifier] NULL,
	[StatusId] [uniqueidentifier] NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubject]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubject](
	[IdStudent] [uniqueidentifier] NOT NULL,
	[IdSubject] [uniqueidentifier] NOT NULL,
 CONSTRAINT [Pk_StudentSubject] PRIMARY KEY CLUSTERED 
(
	[IdStudent] ASC,
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectTeacher]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectTeacher](
	[TeacherId] [uniqueidentifier] NOT NULL,
	[SubjectId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [Pk_SubjectTeacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [uniqueidentifier] NOT NULL,
	[Salary] [decimal](15, 2) NULL,
	[Experience] [int] NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherGroup]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherGroup](
	[IdTeacher] [uniqueidentifier] NOT NULL,
	[IdGroup] [uniqueidentifier] NOT NULL,
 CONSTRAINT [Pk_TeacherGroup] PRIMARY KEY CLUSTERED 
(
	[IdTeacher] ASC,
	[IdGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Thesis]    Script Date: 17.11.2022 13:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thesis](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[TeacherId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [varbinary](max) NOT NULL,
	[ApprovedByTeacher] [bit] NOT NULL,
 CONSTRAINT [PK_Thesis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Certificate_StudentId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Certificate_StudentId] ON [dbo].[Certificate]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Circumstance_StudentId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Circumstance_StudentId] ON [dbo].[Circumstance]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Feedback_IdStudent_IdSubject]    Script Date: 17.11.2022 13:16:23 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Feedback_IdStudent_IdSubject] ON [dbo].[Feedback]
(
	[IdStudent] ASC,
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Grade_IdTeacher_IdGroup]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Grade_IdTeacher_IdGroup] ON [dbo].[Grade]
(
	[IdTeacher] ASC,
	[IdGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Grade_IdTeacher_IdSubject]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Grade_IdTeacher_IdSubject] ON [dbo].[Grade]
(
	[IdTeacher] ASC,
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Group_SpecializationId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Group_SpecializationId] ON [dbo].[Group]
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Person_Email]    Script Date: 17.11.2022 13:16:23 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Person_Email] ON [dbo].[Person]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Person_GenderId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Person_GenderId] ON [dbo].[Person]
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Person_NationalityId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Person_NationalityId] ON [dbo].[Person]
(
	[NationalityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonRoles_RoleId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_PersonRoles_RoleId] ON [dbo].[PersonRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SecretarySpecialization_IdSpecialization]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_SecretarySpecialization_IdSpecialization] ON [dbo].[SecretarySpecialization]
(
	[IdSpecialization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Student_GroupId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Student_GroupId] ON [dbo].[Student]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Student_StatusId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Student_StatusId] ON [dbo].[Student]
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StudentSubject_IdSubject]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_StudentSubject_IdSubject] ON [dbo].[StudentSubject]
(
	[IdSubject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SubjectTeacher_SubjectId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_SubjectTeacher_SubjectId] ON [dbo].[SubjectTeacher]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TeacherGroup_IdGroup]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_TeacherGroup_IdGroup] ON [dbo].[TeacherGroup]
(
	[IdGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Thesis_StudentId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Thesis_StudentId] ON [dbo].[Thesis]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Thesis_TeacherId]    Script Date: 17.11.2022 13:16:23 ******/
CREATE NONCLUSTERED INDEX [IX_Thesis_TeacherId] ON [dbo].[Thesis]
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Circumstance] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Accepted]
GO
ALTER TABLE [dbo].[Grade] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Grade] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [IdGroup]
GO
ALTER TABLE [dbo].[Grade] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [IdTeacher]
GO
ALTER TABLE [dbo].[Group] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT ((0)) FOR [RoleId]
GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Subject] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [FK_StudentCertificate] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [FK_StudentCertificate]
GO
ALTER TABLE [dbo].[Circumstance]  WITH CHECK ADD  CONSTRAINT [FK_StudentCircumstance] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Circumstance] CHECK CONSTRAINT [FK_StudentCircumstance]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_StudentSubject] FOREIGN KEY([IdStudent], [IdSubject])
REFERENCES [dbo].[StudentSubject] ([IdStudent], [IdSubject])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_StudentSubject]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_StudentSubject] FOREIGN KEY([IdStudent], [IdSubject])
REFERENCES [dbo].[StudentSubject] ([IdStudent], [IdSubject])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_StudentSubject]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_SubjectTeacher] FOREIGN KEY([IdTeacher], [IdSubject])
REFERENCES [dbo].[SubjectTeacher] ([TeacherId], [SubjectId])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_SubjectTeacher]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_TeacherGroup] FOREIGN KEY([IdTeacher], [IdGroup])
REFERENCES [dbo].[TeacherGroup] ([IdTeacher], [IdGroup])
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_TeacherGroup]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_SpecializationGroup] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_SpecializationGroup]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [Fk_Gender_Person] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [Fk_Gender_Person]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [Fk_Nationality_Person] FOREIGN KEY([NationalityId])
REFERENCES [dbo].[Nationality] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [Fk_Nationality_Person]
GO
ALTER TABLE [dbo].[PersonRoles]  WITH CHECK ADD  CONSTRAINT [FK_PersonRoles_Person_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonRoles] CHECK CONSTRAINT [FK_PersonRoles_Person_PersonId]
GO
ALTER TABLE [dbo].[PersonRoles]  WITH CHECK ADD  CONSTRAINT [FK_PersonRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonRoles] CHECK CONSTRAINT [FK_PersonRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[Secretary]  WITH CHECK ADD  CONSTRAINT [FK_SecretaryPerson] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Secretary] CHECK CONSTRAINT [FK_SecretaryPerson]
GO
ALTER TABLE [dbo].[SecretarySpecialization]  WITH CHECK ADD  CONSTRAINT [Fk_SecretarySpecialization_Secretary] FOREIGN KEY([IdSecretary])
REFERENCES [dbo].[Secretary] ([Id])
GO
ALTER TABLE [dbo].[SecretarySpecialization] CHECK CONSTRAINT [Fk_SecretarySpecialization_Secretary]
GO
ALTER TABLE [dbo].[SecretarySpecialization]  WITH CHECK ADD  CONSTRAINT [Fk_SecretarySpecialization_Specialization] FOREIGN KEY([IdSpecialization])
REFERENCES [dbo].[Specialization] ([Id])
GO
ALTER TABLE [dbo].[SecretarySpecialization] CHECK CONSTRAINT [Fk_SecretarySpecialization_Specialization]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [Fk_Status_Student] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [Fk_Status_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_StudentGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_StudentGroup]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_StudentPerson] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_StudentPerson]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [Fk_StudentSubject_Student] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [Fk_StudentSubject_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [Fk_StudentSubject_Subject] FOREIGN KEY([IdSubject])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [Fk_StudentSubject_Subject]
GO
ALTER TABLE [dbo].[SubjectTeacher]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTeacher_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[SubjectTeacher] CHECK CONSTRAINT [FK_SubjectTeacher_Subject]
GO
ALTER TABLE [dbo].[SubjectTeacher]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTeacher_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[SubjectTeacher] CHECK CONSTRAINT [FK_SubjectTeacher_Teacher]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_TeacherPerson] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_TeacherPerson]
GO
ALTER TABLE [dbo].[TeacherGroup]  WITH CHECK ADD  CONSTRAINT [Fk_TeacherGroup_Group] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[TeacherGroup] CHECK CONSTRAINT [Fk_TeacherGroup_Group]
GO
ALTER TABLE [dbo].[TeacherGroup]  WITH CHECK ADD  CONSTRAINT [Fk_TeacherGroup_Teacher] FOREIGN KEY([IdTeacher])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[TeacherGroup] CHECK CONSTRAINT [Fk_TeacherGroup_Teacher]
GO
ALTER TABLE [dbo].[Thesis]  WITH CHECK ADD  CONSTRAINT [FK_StudentThesis] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Thesis] CHECK CONSTRAINT [FK_StudentThesis]
GO
ALTER TABLE [dbo].[Thesis]  WITH CHECK ADD  CONSTRAINT [FK_TeacherThesis] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[Thesis] CHECK CONSTRAINT [FK_TeacherThesis]
GO
USE [master]
GO
ALTER DATABASE [MyPoli] SET  READ_WRITE 
GO
