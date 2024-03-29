USE [master]
GO
/****** Object:  Database [ClinicGAP]    Script Date: 19/10/2019 01:00:18 ******/
CREATE DATABASE [ClinicGAP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClinicGAP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ClinicGAP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClinicGAP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ClinicGAP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ClinicGAP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClinicGAP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClinicGAP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClinicGAP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClinicGAP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClinicGAP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClinicGAP] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClinicGAP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClinicGAP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClinicGAP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClinicGAP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClinicGAP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClinicGAP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClinicGAP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClinicGAP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClinicGAP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClinicGAP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ClinicGAP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClinicGAP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClinicGAP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClinicGAP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClinicGAP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClinicGAP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClinicGAP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClinicGAP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClinicGAP] SET  MULTI_USER 
GO
ALTER DATABASE [ClinicGAP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClinicGAP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClinicGAP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClinicGAP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClinicGAP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClinicGAP] SET QUERY_STORE = OFF
GO
USE [ClinicGAP]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 19/10/2019 01:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[idAppointment] [int] IDENTITY(1,1) NOT NULL,
	[fk_idPatient] [int] NOT NULL,
	[fk_idAppointmentType] [int] NOT NULL,
	[fk_idDoctor] [int] NOT NULL,
	[AppointmentDateTime] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[idAppointment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentsTypes]    Script Date: 19/10/2019 01:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentsTypes](
	[idAppointmentType] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[observation] [varchar](250) NULL,
 CONSTRAINT [PK_AppointmentsTypes] PRIMARY KEY CLUSTERED 
(
	[idAppointmentType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 19/10/2019 01:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[idDoctor] [int] NOT NULL,
	[doctorName] [varchar](250) NOT NULL,
	[doctorPhone] [varchar](10) NULL,
	[doctorBirth] [date] NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[idDoctor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 19/10/2019 01:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[idPatient] [int] IDENTITY(1,1) NOT NULL,
	[patientName] [varchar](250) NOT NULL,
	[patientIdCard] [varchar](15) NOT NULL,
	[patientGender] [varchar](50) NOT NULL,
	[patientPhone] [varchar](50) NOT NULL,
	[patientBirth] [date] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[idPatient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19/10/2019 01:00:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[fk_idRole] [int] NOT NULL,
	[lastConnection] [datetime] NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([idAppointment], [fk_idPatient], [fk_idAppointmentType], [fk_idDoctor], [AppointmentDateTime], [isActive]) VALUES (50, 18, 2, 1, CAST(N'2019-12-06T16:40:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
INSERT [dbo].[AppointmentsTypes] ([idAppointmentType], [name], [observation]) VALUES (1, N'Medicina General', NULL)
INSERT [dbo].[AppointmentsTypes] ([idAppointmentType], [name], [observation]) VALUES (2, N'Odontología', NULL)
INSERT [dbo].[AppointmentsTypes] ([idAppointmentType], [name], [observation]) VALUES (3, N'Pediatría', NULL)
INSERT [dbo].[AppointmentsTypes] ([idAppointmentType], [name], [observation]) VALUES (4, N'Neurología', NULL)
INSERT [dbo].[Doctors] ([idDoctor], [doctorName], [doctorPhone], [doctorBirth]) VALUES (1, N'Jacky Heinz', N'3233455432', CAST(N'1980-12-12' AS Date))
INSERT [dbo].[Doctors] ([idDoctor], [doctorName], [doctorPhone], [doctorBirth]) VALUES (2, N'Hugo Lombardi', N'2123123', CAST(N'1980-12-12' AS Date))
INSERT [dbo].[Doctors] ([idDoctor], [doctorName], [doctorPhone], [doctorBirth]) VALUES (3, N'Daniel Valencia', N'123123', CAST(N'1980-12-12' AS Date))
INSERT [dbo].[Doctors] ([idDoctor], [doctorName], [doctorPhone], [doctorBirth]) VALUES (4, N'Marcela Castiblanco', N'43434344', CAST(N'1980-12-12' AS Date))
INSERT [dbo].[Doctors] ([idDoctor], [doctorName], [doctorPhone], [doctorBirth]) VALUES (5, N'Sandra Patiño', N'45422312', CAST(N'1980-12-12' AS Date))
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (13, N'Beatriz Pinzón', N'123', N'Femal', N'3233433890', CAST(N'1970-12-12' AS Date))
INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (14, N'Nicolás Mora', N'656785678', N'Male', N'3433456789', CAST(N'1975-10-10' AS Date))
INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (15, N'Patricia Fernandez', N'8967230849', N'Female', N'397670870', CAST(N'1972-08-12' AS Date))
INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (16, N'Hermes Pinzón', N'8987652109', N'Male', N'9867584', CAST(N'1972-08-12' AS Date))
INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (17, N'Angélica Pineda Martinez', N'1032496993', N'Female', N'3506039320', CAST(N'1998-03-22' AS Date))
INSERT [dbo].[Patients] ([idPatient], [patientName], [patientIdCard], [patientGender], [patientPhone], [patientBirth]) VALUES (18, N'Armando Mendoza', N'2323456431', N'Male', N'445678', CAST(N'1980-07-30' AS Date))
SET IDENTITY_INSERT [dbo].[Patients] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([idUser], [userName], [password], [fk_idRole], [lastConnection], [isActive]) VALUES (1, N'gap_user', N'aq123456', 1, NULL, 1)
INSERT [dbo].[Users] ([idUser], [userName], [password], [fk_idRole], [lastConnection], [isActive]) VALUES (2, N'jhgjhg', N'gfdgfd', 1, NULL, 1)
INSERT [dbo].[Users] ([idUser], [userName], [password], [fk_idRole], [lastConnection], [isActive]) VALUES (3, N'jhgjhg', N'gfdgfd', 1, NULL, 1)
INSERT [dbo].[Users] ([idUser], [userName], [password], [fk_idRole], [lastConnection], [isActive]) VALUES (4, N'fdfdf', N'dfdfdf', 1, NULL, 1)
INSERT [dbo].[Users] ([idUser], [userName], [password], [fk_idRole], [lastConnection], [isActive]) VALUES (5, N'fdfdf', N'dfdfdf', 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_AppointmentsTypes1] FOREIGN KEY([fk_idAppointmentType])
REFERENCES [dbo].[AppointmentsTypes] ([idAppointmentType])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_AppointmentsTypes1]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Doctors] FOREIGN KEY([fk_idDoctor])
REFERENCES [dbo].[Doctors] ([idDoctor])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Doctors]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patients] FOREIGN KEY([fk_idPatient])
REFERENCES [dbo].[Patients] ([idPatient])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Patients]
GO
USE [master]
GO
ALTER DATABASE [ClinicGAP] SET  READ_WRITE 
GO
