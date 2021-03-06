USE [master]
GO
/****** Object:  Database [Mentor_Clients]    Script Date: 09-02-2017 15:11:47 ******/
CREATE DATABASE [Mentor_Clients]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApptrinoClients', FILENAME = N'D:\SQL2014\MDF\Mentor_Clients.mdf' , SIZE = 3328KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ApptrinoClients_log', FILENAME = N'D:\SQL2014\LDF\Mentor_Clients_log.ldf' , SIZE = 94528KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Mentor_Clients] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Mentor_Clients].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Mentor_Clients] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Mentor_Clients] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Mentor_Clients] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Mentor_Clients] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Mentor_Clients] SET ARITHABORT OFF 
GO
ALTER DATABASE [Mentor_Clients] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Mentor_Clients] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Mentor_Clients] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Mentor_Clients] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Mentor_Clients] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Mentor_Clients] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Mentor_Clients] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Mentor_Clients] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Mentor_Clients] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Mentor_Clients] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Mentor_Clients] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Mentor_Clients] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Mentor_Clients] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Mentor_Clients] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Mentor_Clients] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Mentor_Clients] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Mentor_Clients] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Mentor_Clients] SET RECOVERY FULL 
GO
ALTER DATABASE [Mentor_Clients] SET  MULTI_USER 
GO
ALTER DATABASE [Mentor_Clients] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Mentor_Clients] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Mentor_Clients] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Mentor_Clients] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Mentor_Clients] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Mentor_Clients]
GO
/****** Object:  User [psplhyd]    Script Date: 09-02-2017 15:11:47 ******/
CREATE USER [psplhyd] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [psplhyd]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 09-02-2017 15:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientNumber] [bigint] IDENTITY(1000,1) NOT NULL,
	[ClientID] [varchar](100) NOT NULL,
	[ConnectionString] [varbinary](500) NULL,
	[ServerName] [varchar](100) NULL,
	[CompanyName] [varchar](200) NOT NULL,
	[Country] [varchar](3) NULL,
	[Timesheet_Type] [varchar](100) NULL,
	[Timesheet] [varchar](10) NULL,
	[address1] [nvarchar](150) NULL,
	[address2] [nvarchar](150) NULL,
	[city] [char](50) NULL,
	[companyphone] [nvarchar](12) NULL,
	[fax] [nvarchar](15) NULL,
	[companywebsite] [nvarchar](150) NULL,
	[companylogo] [varchar](500) NULL,
	[state] [char](50) NULL,
	[zipcode] [nvarchar](50) NULL,
	[createddate] [datetime] NULL,
	[companytype] [nvarchar](50) NULL,
	[expirydate] [datetime] NULL,
	[active] [bit] NULL,
	[site_type] [varchar](100) NULL CONSTRAINT [DF__Clients__site_ty__38996AB5]  DEFAULT (''),
	[AttachmentUrl] [varchar](200) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Custom_DbError]    Script Date: 09-02-2017 15:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Custom_DbError](
	[ErrorId] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrorNo] [varchar](20) NULL,
	[ErrorSeverity] [varchar](20) NULL,
	[ErrorState] [varchar](20) NULL,
	[ErrorLine] [varchar](20) NULL,
	[ProcedureName] [varchar](100) NULL,
	[ErrorMessage] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExcelImportFiles]    Script Date: 09-02-2017 15:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExcelImportFiles](
	[Fileid] [int] IDENTITY(1,1) NOT NULL,
	[Excelfile] [varchar](150) NULL,
	[Status] [varchar](100) NULL,
	[Module] [varchar](100) NULL,
	[Importoption] [varchar](50) NULL,
	[ClientId] [varchar](150) NULL,
	[UId] [varchar](200) NULL,
	[ActualFName] [varchar](200) NULL,
 CONSTRAINT [PK_ExcelImportFiles] PRIMARY KEY CLUSTERED 
(
	[Fileid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImportHistory]    Script Date: 09-02-2017 15:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImportHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExcelFile] [varchar](200) NULL,
	[TotalRecords] [int] NULL,
	[FailedRecords] [int] NULL,
	[ClientId] [varchar](50) NULL,
	[Filepath] [varchar](50) NULL,
 CONSTRAINT [PK_ImportHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 09-02-2017 15:11:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserMaster](
	[Userid] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[Name] [varchar](100) NULL,
	[Email] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1001, N'CertusStaffing', 0x01000000D3307CC4057A7C72ADF6A516F1E9E11B0823BC4105B655F38023A30EADC645D8432C2413A0ABB623E11D18754E5DF929FE96EF9388C50B1DBB6BCB400DA913784507699D12F9F0050B9C645CA67F5069DEA83AA8F23A39848CCA2D70852FAF4B0DD508A641D51541308F15B6AF7E15BB, N'CODEROA-FV22QDN', N'Certus Staffing', N'CA', N'SUN_SAT', N'LUMPSUM', N'', N'', N'                                                  ', N'', N'', N'', N'', N'                                                  ', N'', CAST(N'2014-02-04 03:04:07.170' AS DateTime), N'', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1002, N'Workpro_Live', 0x01000000E88906B5123D1B3913B3689AE26152B9B7832B4A4D9D939B74412ADD053FCCD7B7FF582881AD5AB5BC2A33E1F257C45EDB2E8FE79AEB31E5E1CFD3A1B369DEF05DB34B6E5F8A287839F8AB1884E12B5A33902D3DE72AB47EC8C65FA487BDCA82EB8B4E95312E12A2, N'CODEROA-FV22QDN', N'Workpro', N'CA', N'SUN_SAT', N'REGULAR', N'', N'', N'                                                  ', N'', N'', N'', N'', N'                                                  ', N'', CAST(N'2014-02-04 03:04:07.170' AS DateTime), N'', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1003, N'Excel', 0x0100000024ACB442AE69D2A948FEDD0B3C0F00FA6C7A9BF236C410CE3ED518D131F6B408B0FC1244630835A686C95B6AC035F023A1A6C1813D661740390FFCB7CAD6AD1AE2E90C9B68888B8D0E22A9DAD1E40D457193EE85B0FE97285060BC8E37A2B4E010BD2118C61A39CE9B04E588A8E5DA33, N'server.excelemployment.net', N'Excel', N'CA', N'SUN_SAT', N'REGULAR', NULL, NULL, N'                                                  ', N'', N'', NULL, NULL, N'                                                  ', N'', CAST(N'2014-02-04 03:04:07.170' AS DateTime), N'Consultancy', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1119, N'msi', 0x01000000A277FCD3EF69943929D8ED463B27CB49983CB0B0ACD0EFFBEB0AC6110F18841A96A327FBD07A03F434032124C5137DCD5205AE4C308CDB355591DAB27CAFA79BA428A98C480AD71D2D43F8A708CF2CB4AFFE23E0A3A3A83B7647E415B613C34D, N'PSPL', N'Managing Solutions Inc.', N'USA', N'SUN_SAT', N'LUMPSUM', N'4909 Pearl East Circle #105', N'', N'Boulder                                           ', N'(303) 931500', N'0', N'www.managingsolutionsinc.com', N'http://priyanet.net/staffing/clients/msi/images/logo/logo.png', N'CO                                                ', N'80301', CAST(N'2014-03-13 00:00:00.000' AS DateTime), N'HR Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'HR', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1123, N'1PointCRM', 0x010000005366EAEE49C98103AF1A2D639EB626B8B1357C6E13EA9E0497265605F3CF2B62F9DFAF5014BD88108894C454323A1305E10FB561E25C8F2DB7E0C75939FF586E3290C9AAE0985A909D7778A802F70D1C5B69EF836E20EE02C3C885974B04DBC3, N'1Point', N'1Point', N'USA', N'SUN_SAT', N'LUMPSUM', N'23 sidcup tower', N'', N'Test                                              ', N'(098) 764323', N'', N'www.piltd.com', N'http://priyanet.net/staffing/clients/19thmarch/images/logo/logo.png', N'FL                                                ', N'12345', CAST(N'2014-03-19 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'QA', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1129, N'vlsit', 0x010000009F1EA5DB73DAAB4A03D464740E0DBCE076EECBC532907D30B0932796767F29FE9D002213C01C48B9E6CC94E059601499A1951B4EE8448B1EDE6444F78DDFEF26B1D94D9B5669FD42A24A4CB8407302FC9BD6719AE622DBBC652EE40BE3D2A1F0B3DE181A8542CA3D, N'CODEROA-FV22QDN', N'VLS IT Consulting', N'USA', N'SUN_SAT', N'LUMPSUM', N'University Plaza Office Complex', N'260 Chapman Rd., Suite #104A', N'Newark                                            ', N'(302) 368565', N'3023685788', N'www.vlsitconsulting.com', N'http://priyanet.net/staffing/clients/vlsit/images/logo/logo.png', N'DE                                                ', N'19702', CAST(N'2014-04-14 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1130, N'elevatearch', 0x01000000516FFA8D69B51D6A14A5731212FFBFC8BC1842426CD206B1DFF61D489147471FBE100882FE433035CC1A9108101996D1944BF3DDAB7F4E87FF97842EB1688CE88D4660CFE58970902CED07F0D252C70A5E9EB9EA317F71EF1BAA11BAC6E533DC576AE170CB4428B2, N'PSPL', N'Elevate Architecture', N'USA', N'SUN_SAT', N'LUMPSUM', N'552 Humboldt St,', N'', N'Denver                                            ', N'(303) 319127', N'', N'', N'http://priyanet.net/staffing/clients/elevatearch/images/logo/logo.png', N'CO                                                ', N'80218 ', CAST(N'2014-04-16 00:00:00.000' AS DateTime), N'HR Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1133, N'ProfitKeyCRM', 0x0100000061D005693299A12934661EA0AC6529C1D329E3D6DA19427463BE8DA889CFDC9AC788E6AF994F0C3B77DA6C0C797F4601BC0E0330B01C4CBC9F742B1A4DACFAC2807D015367851BFFAA3EF99EFFA3D90C027500A3AC4143AD6D0CD25C161F7ADA, N'PSPL', N'ProfitKey', N'USA', N'SUN_SAT', N'LUMPSUM', N'AAA', N'', N'Colorado                                          ', N'(332) 313123', N'', N'', N'http://priyanet.net/staffing/clients/gilmore/images/logo/logo.png', N'CO                                                ', N'2123312323', CAST(N'2014-05-19 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'QA', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1137, N'DentalStatewideTesting', 0x010000002DCD17183E0CD2FA0C6FAB30DCC99396DFB692D83109E8BB96F8869A527076FA2B60490ACAE2C2C94A80EAA000C4F38A24640D6558FC7528EB32EA7FFBCE692FDD574E06D456A34BACE9C08B1EF0BA95C68471094F8F4ADFBECA5947FE4FDFE338F0517CD598E9075C96D3B466352008E7C6CEBACC8F09E5, N'CODEROA-FV22QDN', N'Dental Statewide Testing', N'USA', N'SUN_SAT', N'REGULAR', N'', N'', N'                                                  ', N'', N'', N'', N'http://64.150.177.129/staffing/clients/DentalStatewideTesting/images/logo/logo.png', N'AA                                                ', N'', CAST(N'2014-06-05 02:52:49.203' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1138, N'dentalstatewide', 0x01000000EEAF8905D0016B3A7D1F1CD19C8A1AEFA177C3866E61FCB8C2857BCF2BE1A3E380207ACCDE0CE345BCE6A0611119CEBF61953A3344FCA29050DB9A32307AC8128EE908188AB9DE75B19BFAF33E64979CC14C2A56B31BD93A67A14CD46756E39D8CF05C05B33846D4F16E706E5140B0BA, N'CODEROA-FV22QDN', N'Dental Statewide', N'USA', N'SUN_SAT', N'REGULAR', N'', N'', N'                                                  ', N'', N'', N'', N'http://64.150.177.129/staffing/clients/dentalstatewide/images/logo/logo.png', N'AA                                                ', N'', CAST(N'2014-06-06 00:37:39.133' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1142, N'FCS', 0x01000000B828C912C30FC885F7487428229BA1D171C084E49BB601993EA64226F267BAFDAC470AEC46BB6C664AB65C3EDA7CF7EB6547F64EA6AE45A247AD57C3D5CBDB28957963FBD64A94D6E88F24AF0D84B0CF54AE994E0B72A3799D48ADF36F1DCCDB, N'PSPL', N'FCS', N'USA', N'SUN_SAT', N'REGULAR', N'', N'', N'                                                  ', N'', N'', N'', N'http://64.150.177.129/staffing/clients/mallard/images/logo/logo.png', N'AA                                                ', N'', CAST(N'2014-06-18 09:16:12.220' AS DateTime), N'HR Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'HR', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1147, N'apptrino_new', 0x0100000079D85877CD89B39A067BE2AFC57F413AA8C3B342E6689D153759A5AF4DCB4AE1EE5E97CFA8BF1E9F76894D166F945F1D79A9A585C802760348DF59454E23ACD14FC3245668D321D22EF9AAF76BD2D9B03C7E968EDAC3570EB75648AF933F42C084ADC324DFB08015, N'PSPL', N'Apptrino', N'USA', N'SUN_SAT', N'LUMPSUM', N'Boston', N'', N'Boston                                            ', N'(123) 456666', N'', N'', N'http://priyanet.net/staffing/clients/apptrino_new/images/logo/logo.png', N'MA                                                ', N'12345', CAST(N'2014-06-27 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2014-07-12 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1148, N'softwaresolution', 0x01000000460CB8DDD4A76B56F356E3A79D053354A355FA2A67E43051E617934E1410EEB013DC6B73E660537E990DDF439A21C7DAA6BA8F2E7BEF7A3A413BDF489C63BD1B7D79F575B39B7F6D84C61BE0EEC46C69B333366293208335FECB92A3B758A9568712BF0702C895FB, N'PSPL', N'software solution', N'USA', N'SUN_SAT', N'LUMPSUM', N'Boston', N'', N'Boston                                            ', N'(123) 456788', N'', N'', N'http://priyanet.net/staffing/clients/softwaresolution/images/logo/logo.png', N'MA                                                ', N'1234566666', CAST(N'2014-06-27 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2014-07-12 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1149, N'solution_new', 0x0100000075DD1FDF537EE79168ED9659E1122884B0A218D017877839F32AD0EAACCD37C11914F0EF5A3DBCA6FDC8DF0C56AF58F1E2CB47306C04ADBF438538367CFD8CAA42BA5594DAA239B4B21D02CBE5B83CE94D0A3D87FD75343532A37C4FDA3F3CCB9C0936E5A1452F8D, N'PSPL', N'solution_new', N'USA', N'SUN_SAT', N'LUMPSUM', N'boston', N'', N'boston                                            ', N'(123) 456789', N'', N'', N'http://priyanet.net/staffing/clients/solution_new/images/logo/logo.png', N'MA                                                ', N'1234567777', CAST(N'2014-06-30 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2014-07-15 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1150, N'netsolution', 0x010000006EF2F8501FB778C7477D1B4ED3A4E7195C6E4C8956DFCCC513194CF23C52FBE506CE2AA98E5E4C56BB16C19336ECB9E7A03E1335374FB0D41C563A3C45E1BE19404EF64289D034081F1ACAF2471CC600581D9C2BABB5221557615415315CB137798E7A84F6DE6976, N'PSPL', N'netsolution', N'USA', N'SUN_SAT', N'LUMPSUM', N'g-49 noida', N'', N'boston                                            ', N'(012) 042661', N'', N'netsolution.com', N'http://priyanet.net/staffing/clients/netsolution/images/logo/logo.png', N'AK                                                ', N'2013013444', CAST(N'2014-07-01 00:00:00.000' AS DateTime), N'Staffing Company', CAST(N'2014-07-16 00:00:00.000' AS DateTime), 1, N'STAFFING', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1152, N'dev', 0x010000000E6A0AFEA3FD4A5FD93F62991AC1258B320AEEB339710AAB4309057B34CA6EBD9806313615D832825092989F586336BDC0732D8B89AA5D46207EE9D4BC1B232F6AF3A6EE34DE8AEEB72F7EA521D251ECEE90EF8D01D9899B94F7D9260ED8772F, N'PSPL', N'DEV', N'USA', N'SUN_SAT', N'REGULAR', N'', N'', N'                                                  ', N'', N'', N'', N'http://priyanet.net/dev/clients/dev/images/logo/logo.png', N'AA                                                ', N'', CAST(N'2014-09-24 08:52:28.150' AS DateTime), N'Staffing Company', CAST(N'2015-02-24 00:00:00.000' AS DateTime), 1, N'QA', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1154, N'DevTesting', 0x00000000, N'', N'DEV SERVER', N'USA', N'SUN_SAT', N'LUMPSUM', N'', N'', N'                                                  ', N'', N'', N'', N'', N'                                                  ', N'', CAST(N'1900-01-01 00:00:00.000' AS DateTime), N'', CAST(N'1900-01-01 00:00:00.000' AS DateTime), NULL, N'', N'')
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1156, N'CRMDev', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20, N'PSPL', N'CRMDev', N'USA', N'SUN_SAT', N'LUMPSUM', N'Boston', NULL, NULL, N'00000000000', NULL, N'www.CRM1.com', NULL, N'MA                                                ', NULL, CAST(N'2014-06-27 00:00:00.000' AS DateTime), N'Staffing Company', NULL, 1, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1174, N'NewTestCRMDatabase', NULL, NULL, N'piltdcrmjs', NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, N'www.piltdcrmjs.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1176, N'crm_test1', NULL, NULL, N'crm_test1', NULL, NULL, NULL, NULL, NULL, NULL, N'65465456', NULL, N'www.test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1178, N'Mentor', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20, N'PSPL', N'CRM', N'USA', N'SUN_SAT', N'LUMPSUM', NULL, NULL, N'Boston                                            ', N'65465456', NULL, N'www.test.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1179, N'CRMTest', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20, N'PSPL', N'CRMTest', N'USA', N'SUN_SAT', N'LUMPSUM', NULL, NULL, N'Boston                                            ', N'65465456', NULL, N'www.test.com', NULL, NULL, NULL, CAST(N'2014-06-27 00:00:00.000' AS DateTime), N'Staffing Company', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1185, N'Psplcrm', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D5073706C63726D3B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'Psplcrm', NULL, NULL, NULL, NULL, NULL, NULL, N'89789', NULL, N'www.psplcrm.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1187, N'testpiltd', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D7465737470696C74643B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'testpiltd', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1188, N'testpspl1', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D746573747073706C313B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'testpspl1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'udaytest', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1189, N'testpspl', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D746573747073706C3B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'testpspl', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'testpspl', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1191, N'testpspsl', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D74657374707370736C3B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'testpspsl', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1192, N'TEstpspls', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D544573747073706C733B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'TEstpspls', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1193, N'TestDB', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D5465737444423B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'TestDB', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'test', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1194, N'CRM_PILTD', 0x4461746120536F757263653D73716C7365727665723B496E697469616C20436174616C6F673D43524D5F50494C54443B557365722049443D73613B50617373776F72643D73612132303134, N'sqlserver', N'CRM_PILTD', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'piltd.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1196, N'CRM_QA', 0x4461746120536F757263653D7073706C3B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D31706F696E743B50617373776F72643D695030316E74, N'pspl', N'CRM_QA', N'USA', NULL, NULL, N'Hyd', N'Hyd', N'Hyd                                               ', NULL, NULL, N'test', NULL, NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1200, N'Testddclient', 0x4461746120536F757263653D7073706C3B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D31706F696E743B50617373776F72643D695030316E74, N'pspl', N'Testddclient', N'USA', NULL, NULL, N'Hyd', N'Hyd', N'Hyd                                               ', NULL, NULL, N'test', NULL, NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1202, N'CRM_testclient', 0x4461746120536F757263653D7073706C3B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D31706F696E743B50617373776F72643D695030316E74, N'pspl', N'CRM_testclient', N'USA', NULL, NULL, N'HYD', N'HYD', N'HYD                                               ', NULL, NULL, N'test', NULL, NULL, NULL, NULL, N'staff', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1203, N'crm_details', 0x4461746120536F757263653D7073706C3B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D31706F696E743B50617373776F72643D695030316E74, N'pspl', N'crm_details', N'usa', NULL, NULL, N'usa', N'usa', N'usa                                               ', NULL, NULL, N'test', NULL, NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1214, N'CRM_bothworldsgroup', 0x4461746120536F757263653D57494E2D4D533049365339315530365C53514C32303134457870726573733B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303135, N'WIN-MS0I6S91U06\SQL2014Express', N'CRM_bothworldsgroup', N'usa', NULL, NULL, N'usa', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'staff', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1218, N'CRM_Devtest', 0x4461746120536F757263653D57494E2D4D533049365339315530365C53514C32303134457870726573733B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303135, N'WIN-MS0I6S91U06\SQL2014Express', N'CRM_Devtesttesting', N'US', NULL, NULL, N'Touch Stone building', N'Jubilee Hill', N'Hyderabad                                         ', NULL, NULL, N'piltd.co.in', NULL, NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1230, N'CRM_JBF', 0x4461746120536F757263653D53716C7365727665723B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303134, N'Sqlserver', N'CRM_Jindal', N'Ind', NULL, NULL, N'Hyderabad', N'Hyderabad', N'Hyderabad                                         ', NULL, NULL, N'www.crm.com', NULL, NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1231, N'CRM_NTPC', 0x4461746120536F757263653D53716C7365727665723B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303134, N'Sqlserver', N'CRM_LUPIN', N'Ind', NULL, NULL, N'Hyderabad', N'Hyderabad', N'Hyderabad                                         ', NULL, NULL, N'www.crm.com', N'IBM_logo_1947.png', NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1232, N'CRM_ITC', 0x4461746120536F757263653D53716C7365727665723B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303134, N'Sqlserver', N'CRM_HCL', N'Ind', NULL, NULL, N'Hyderabad', N'Hyderabad', N'Hyderabad                                         ', NULL, NULL, N'www.crm.com', NULL, NULL, NULL, NULL, N'Staffing', NULL, NULL, N'', NULL)
INSERT [dbo].[Clients] ([ClientNumber], [ClientID], [ConnectionString], [ServerName], [CompanyName], [Country], [Timesheet_Type], [Timesheet], [address1], [address2], [city], [companyphone], [fax], [companywebsite], [companylogo], [state], [zipcode], [createddate], [companytype], [expirydate], [active], [site_type], [AttachmentUrl]) VALUES (1234, N'CRMOrchard', 0x4461746120536F757263653D53716C7365727665723B496E697469616C20436174616C6F673D43524D436C69656E74733B557365722049443D73613B50617373776F72643D73612132303134, N'Sqlserver', N'CRMOrchard', N'Ind', NULL, NULL, N'newyork', N'newyorkCity', N'newyork                                           ', NULL, NULL, N'davidcrp.us', N'Dell.png', NULL, NULL, NULL, N'staffing', NULL, NULL, N'', NULL)
SET IDENTITY_INSERT [dbo].[Clients] OFF
SET IDENTITY_INSERT [dbo].[ExcelImportFiles] ON 

INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2129, N'6f52a42b-3a81-4664-b46c-d5e4238d8496.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2130, N'5c9de1ac-f44f-4313-b522-9648192f1515.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2131, N'39bd888c-3c25-4ac4-ac3b-d047b867f809.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2132, N'247d92fb-d2a0-4b2c-b1aa-b1aff5a7337f.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2133, N'3be9effe-eaf7-4926-a609-34991fa353a0.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2134, N'9e801f03-c37e-410d-a531-c1612d6c65b7.csv', N'Success', N'Contact', NULL, N'CRM', NULL, N'ImportContact (1).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2135, N'f4c0611c-bdf0-40aa-bdde-b293d25a0265.csv', N'Pending', N'Contact', NULL, N'CRM', NULL, N'ImportContact(12).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2136, N'706b9335-8ed7-4039-91b9-8e235e9cc376.csv', N'Pending', N'Contact', NULL, N'CRM', NULL, N'ImportContact(12).csv')
INSERT [dbo].[ExcelImportFiles] ([Fileid], [Excelfile], [Status], [Module], [Importoption], [ClientId], [UId], [ActualFName]) VALUES (2137, N'b684b02f-e454-40ba-bc64-7a287f85c24d.csv', N'Pending', N'Contact', NULL, N'CRM', NULL, N'ImportContact(3).csv')
SET IDENTITY_INSERT [dbo].[ExcelImportFiles] OFF
SET IDENTITY_INSERT [dbo].[ImportHistory] ON 

INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (19, N'ImportContact.csv', 3, 2, N'CRM', N'4bd94388-d04b-4faf-bb65-d307d5157a6c.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (20, N'ImportContact.csv', 3, 2, N'CRM', N'2e334c49-9388-4c5e-b5cf-4ab7cf114cfa.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (21, N'ImportContact.csv', 3, 2, N'CRM', N'dfd51e5d-6c93-49df-a68a-0cc58464fb6c.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (22, N'ImportContact.csv', 3, 2, N'CRM', N'aaddb6f3-61d8-4e11-81a1-834b5836d54d.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (23, N'ImportContact.csv', 3, 2, N'CRM', N'2ee512f5-a6de-4f17-8b8b-72c5b94290e5.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (24, N'ImportContact.csv', 3, 2, N'CRM', N'4fc5c33c-08b7-4b7b-812a-1af84b06ae24.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (25, N'ImportContact.csv', 3, 2, N'CRM', N'cdef8802-2f4e-4863-8291-1adcfeb1efde.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (26, N'ImportContact.csv', 3, 2, N'CRM', N'9459104d-c964-4672-952a-ccbfe265a5f4.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (27, N'ImportContact.csv', 3, 2, N'CRM', N'1abaa726-c8a4-4a64-8f4f-d82a3512a278.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (28, N'ImportContact.csv', 3, 2, N'CRM', N'72aa0bea-0046-4720-95bd-f040eeaf9261.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (29, N'ImportContact.csv', 4, 2, N'CRM', N'e84391ef-437f-4e50-b9a3-853104859f2e.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (30, N'ImportContact.csv', 5, 2, N'CRM', N'4da9e017-e867-4f8d-8a8e-a25f3082b4af.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (31, N'ImportContact.csv', 5, 2, N'CRM', N'0c1e6c7e-aa38-405d-8258-9e8fafe1d0d9.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (32, N'ImportContact.csv', 5, 2, N'CRM', N'f1a2f487-f9ce-4fdc-8543-8b1c77970dc8.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (33, N'ImportContact.csv', 2, 0, N'CRM', N'f1235419-a595-4ec6-8491-4f67f206c994.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (34, N'ImportContact.csv', 2, 0, N'CRM', N'254382ae-5244-4687-9934-dd197a098d33.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (35, N'ImportContact (1).csv', 2, 1, N'CRM', N'757635c3-5c99-4b24-87d8-849d9f0018ae.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (36, N'ImportContact (1).csv', 2, 1, N'CRM', N'9ec1f9ab-ecca-404f-b663-6a43a3513d13.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (37, N'ImportContact (1).csv', 2, 1, N'CRM', N'6f85cd69-3176-4aa3-ba24-0e93a3d0a6b0.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (38, N'ImportContact (1).csv', 2, 1, N'CRM', N'fa63023b-e256-459e-a4d2-58603456ce63.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (39, N'ImportContact (1).csv', 2, 1, N'CRM', N'8069144e-ff32-48bd-8be5-cc0c3038fa4a.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (40, N'ImportContact (1).csv', 2, 1, N'CRM', N'2d019be8-1d80-4085-a898-b001b69bb33b.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (41, N'ImportContact (1).csv', 2, 1, N'CRM', N'a73572dd-a17a-4873-99d0-bc2dc84058ad.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (42, N'ImportContact (1).csv', 2, 1, N'CRM', N'e2ff2ac4-c655-42e1-8a9f-55a720477f52.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (43, N'ImportContact (1).csv', 2, 1, N'CRM', N'fe779fb5-4495-476f-8a6f-75365db715a0.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (44, N'ImportContact (1).csv', 2, 1, N'CRM', N'f6db768c-4c93-4c1d-b62c-4e85de5b71d1.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (45, N'ImportContact (1).csv', 2, 1, N'CRM', N'6b2e8dfa-7cc5-42d9-b9aa-14774ca932da.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (46, N'ImportContact (1).csv', 2, 1, N'CRM', N'a9c2c707-d842-433c-9917-0557e2f2617d.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (47, N'ImportContact (1).csv', 2, 1, N'CRM', N'34a335f7-9962-410e-bd65-7f1e6e7b1181.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (48, N'ImportContact (1).csv', 2, 1, N'CRM', N'd9a8d73f-ea7e-4e97-b4c3-92cf4c7e27d2.csv')
INSERT [dbo].[ImportHistory] ([Id], [ExcelFile], [TotalRecords], [FailedRecords], [ClientId], [Filepath]) VALUES (49, N'ImportContact (1).csv', 2, 1, N'CRM', N'b74a0587-d49e-4663-95c9-5c9d4f84b539.csv')
SET IDENTITY_INSERT [dbo].[ImportHistory] OFF
SET IDENTITY_INSERT [dbo].[UserMaster] ON 

INSERT [dbo].[UserMaster] ([Userid], [Username], [password], [Name], [Email]) VALUES (1, N'apptrino', N'pspl!23', N'Super Admin', N'nitesh_singhal@priyanet.com')
INSERT [dbo].[UserMaster] ([Userid], [Username], [password], [Name], [Email]) VALUES (2, N'1Point', N'1Point', N'Super Admin', N'kali-charan@priyanet.com')
INSERT [dbo].[UserMaster] ([Userid], [Username], [password], [Name], [Email]) VALUES (3, N'Eagle', N'Eagle', N'Super Admin', N'HarshadS@eaglecmms.com')
SET IDENTITY_INSERT [dbo].[UserMaster] OFF
ALTER TABLE [dbo].[Custom_DbError] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  StoredProcedure [dbo].[CREATE_NEW_DATABASE]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- CREATE_NEW_DATABASE 'ANewTest'  
 
CREATE PROC [dbo].[CREATE_NEW_DATABASE] @NEW_DATABASE_NAME varchar(100)  
AS  
BEGIN  
SET NOCOUNT ON;   
DECLARE @DB_SQL NVARCHAR(max)  
DECLARE @NEW_DB_NAME varchar(100)  
DECLARE @NEW_DB_LOCATION varchar(100)  
SET @NEW_DB_NAME = @NEW_DATABASE_NAME  
SET @NEW_DB_LOCATION = 'F:\SQLDATA2008\'  
  
/* EXECUTE COPY COMMAND TP COPY MDF AND LDF FILES FROM ONE LOCATION TO ANOTHER */    
EXEC master.dbo.xp_cmdshell 'COPY "F:\NiteshProjectData(DoNotDelete)\ApptrinoMasterDatabase.mdf" "F:\SQLDATA2008\ApptrinoMasterDatabase.mdf"';   
EXEC master.dbo.xp_cmdshell 'COPY "F:\NiteshProjectData(DoNotDelete)\ApptrinoMasterDatabase_log.LDF" "F:\SQLDATA2008\ApptrinoMasterDatabase_log.LDF"';   
/* FOR ACCOUNTING DATABASE */
EXEC master.dbo.xp_cmdshell 'COPY "F:\NiteshProjectData(DoNotDelete)\ApptrinoAccountingMasterDatabase.mdf" "F:\SQLDATA2008\ApptrinoAccountingMasterDatabase.mdf"';   
EXEC master.dbo.xp_cmdshell 'COPY "F:\NiteshProjectData(DoNotDelete)\ApptrinoAccountingMasterDatabase_log.LDF" "F:\SQLDATA2008\ApptrinoAccountingMasterDatabase_log.LDF"';   


/* ATTACH MDF AND LDF FILES WITH DESIRED DATABSE NAME TO FROM LOCATION */    
SET @DB_SQL = '   
CREATE DATABASE ['+@NEW_DB_NAME+'] ON ( FILENAME = N'''+@NEW_DB_LOCATION+'ApptrinoMasterDatabase.mdf''), ( FILENAME = N'''+@NEW_DB_LOCATION+'ApptrinoMasterDatabase_log.ldf'') FOR ATTACH ; 
CREATE DATABASE ['+@NEW_DB_NAME+'Accounting] ON ( FILENAME = N'''+@NEW_DB_LOCATION+'ApptrinoAccountingMasterDatabase.mdf''), ( FILENAME = N'''+@NEW_DB_LOCATION+'ApptrinoAccountingMasterDatabase_log.ldf'') FOR ATTACH ; 

'  
  
/* UPDATE DATA DICTIONARY AND FILENAMES WITH NEW DATABSE NAME */   
SET @DB_SQL = @DB_SQL + '  
ALTER DATABASE ['+@NEW_DB_NAME+'] MODIFY FILE (NAME=N''ApptrinoMasterDatabase'', NEWNAME=N'''+@NEW_DB_NAME+''', FILENAME = N'''+@NEW_DB_LOCATION+''+@NEW_DB_NAME+'.mdf'');  
ALTER DATABASE ['+@NEW_DB_NAME+'] MODIFY FILE (NAME=N''ApptrinoMasterDatabase_log'', NEWNAME=N'''+@NEW_DB_NAME+'_log'', FILENAME = N'''+@NEW_DB_LOCATION+''+@NEW_DB_NAME+'_log.ldf'');

ALTER DATABASE ['+@NEW_DB_NAME+'Accounting] MODIFY FILE (NAME=N''ApptrinoAccountingMasterDatabase'', NEWNAME=N'''+@NEW_DB_NAME+'Accounting'', FILENAME = N'''+@NEW_DB_LOCATION+''+@NEW_DB_NAME+'Accounting.mdf'');  
ALTER DATABASE ['+@NEW_DB_NAME+'Accounting] MODIFY FILE (NAME=N''ApptrinoAccountingMasterDatabase_log'', NEWNAME=N'''+@NEW_DB_NAME+'Accounting_log'', FILENAME = N'''+@NEW_DB_LOCATION+''+@NEW_DB_NAME+'Accounting_log.ldf'');  

'  
  
  
/* TAKE NEW DATABASE OFFILINE */   
SET @DB_SQL = @DB_SQL + '
ALTER DATABASE ['+@NEW_DB_NAME+'] SET OFFLINE WITH ROLLBACK IMMEDIATE;
ALTER DATABASE ['+@NEW_DB_NAME+'Accounting] SET OFFLINE WITH ROLLBACK IMMEDIATE;  '  
  
/* RENAME MDF AND LDF FILES TO NEW DATABASE NAME ON PHYSICAL LOCATION */   
SET @DB_SQL = @DB_SQL + '
exec xp_cmdshell ''rename "'+@NEW_DB_LOCATION+'ApptrinoMasterDatabase.mdf" '+@NEW_DB_NAME+'.mdf ''; 
exec xp_cmdshell ''rename "'+@NEW_DB_LOCATION+'ApptrinoMasterDatabase_log.LDF" '+@NEW_DB_NAME+'_log.LDF ''; 

exec xp_cmdshell ''rename "'+@NEW_DB_LOCATION+'ApptrinoAccountingMasterDatabase.mdf" '+@NEW_DB_NAME+'Accounting.mdf ''; 
exec xp_cmdshell ''rename "'+@NEW_DB_LOCATION+'ApptrinoAccountingMasterDatabase_log.LDF" '+@NEW_DB_NAME+'Accounting_log.LDF ''; 

'  
  
/* BRING NEW DATABASE ONLINE */   
SET @DB_SQL = @DB_SQL + '
ALTER DATABASE ['+@NEW_DB_NAME+'] SET ONLINE; 
ALTER DATABASE ['+@NEW_DB_NAME+'Accounting] SET ONLINE; 
'  
  
--SET @DB_SQL = @DB_SQL + 'select * from '+@NEW_DB_NAME+'.sys.database_files;'  
  
--SELECT @DB_SQL  

/* INSERT DEFAULT DATA */
SET @DB_SQL = @DB_SQL + ' EXEC [USP_EXECUTE_SCRIPT] '''+@NEW_DATABASE_NAME+'''; '

EXEC (@DB_SQL)  
/* DETACH DATABASE */   
--SET @DB_SQL = 'EXEC master.dbo.sp_detach_db @dbname = N'''+@NEW_DB_NAME+''' '  


END  

GO
/****** Object:  StoredProcedure [dbo].[CRM_CheckifUserExists]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[CRM_CheckifUserExists]
(
@Username varchar(200),
@password varchar(200)
)
As
Declare @Result int
if exists(select * from UserMaster  where Username = @Username and password=@password)
begin
set @Result=1
select @result
end 
else
begin
set @Result=0
select @Result
end


GO
/****** Object:  StoredProcedure [dbo].[CRM_Getclients]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[CRM_Getclients]
As
Begin
select * from  [dbo].[Clients]

End
GO
/****** Object:  StoredProcedure [dbo].[CRM_GetExcelfilesbasedonstatus]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[CRM_GetExcelfilesbasedonstatus]
@Status varchar(100),
@Module varchar(100)
AS
BEGIN
	SELECT * from  ExcelImportFiles where Status=@Status and Module=@Module
END
GO
/****** Object:  StoredProcedure [dbo].[CRM_GetLogin]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRM_GetLogin] 
(             
@Username varchar(200),      
@Password varchar(200)     
)
AS              
BEGIN 
Select 
UserMaster.Userid,
UserMaster.Username,
UserMaster.password,
UserMaster.Name,
UserMaster.Email
from UserMaster
where usermaster.Username=@Username and usermaster.password=@Password
end

GO
/****** Object:  StoredProcedure [dbo].[crm_GetOpportunityDetail]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  Procedure [dbo].[crm_GetOpportunityDetail]      
@LeadID numeric(10)      
as      
      
select *,convert(char,createdDate,101) as mcreatedDate,CONVERT(VARCHAR(15),EstimatedCloseDate,101) as EstimatedCloseDate1 from dbo.tbl_Lead where LeadId=@LeadID AND  IsOpportunity=1  




GO
/****** Object:  StoredProcedure [dbo].[CRM_InsertClientDetails]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRM_InsertClientDetails] @ClientID VARCHAR(100)
	,@ClientName VARCHAR(200)
	,@companytype  NVARCHAR(50) =null
	,@Country VARCHAR(3)=null
	,@address1 NVARCHAR(150)=null
	,@address2 NVARCHAR(150)=null
	,@city CHAR(50)=null
	,@companywebsite NVARCHAR(150)=null
	,@ConnectionString VARBINARY(500)
	,@ServerName VARCHAR(100)
	,@CompanyLogo VARCHAR(200)=null
AS
BEGIN
					INSERT INTO dbo.Clients (
						ClientID
						,CompanyName
						,companytype
						,Country
						,address1
						,address2
						,city
						,companywebsite
						,ConnectionString
						,ServerName
						,companylogo
						)
					VALUES (
						@ClientID
						,@ClientName
						,@companytype
						,@Country
						,@address1
						,@address2
						,@city
						,@companywebsite
						,@ConnectionString
						,@ServerName
						,@CompanyLogo
						);
END
GO
/****** Object:  StoredProcedure [dbo].[crm_InsertOpportunityDetail]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[crm_InsertOpportunityDetail]                                                            
(                                                        
@Lead_Name varchar(50),                                                              
@Title varchar(200),                                                             
@Phone nvarchar(50),                                                              
@fax   nvarchar(50),         
@Email nvarchar(2000),         
@Mobile nvarchar(20),          
@PreferTime nvarchar(20),                                                           
@PreferMode nvarchar(20),            
@EmailOptOut varchar(50),         
@AccountId nvarchar(10),          
@AccountName varchar(250),           
@webSite varchar(50),                                                              
@Ownership varchar(50),           
@Employees varchar(50),           
@Industry varchar(50),           
@AnnualRevenue nvarchar(50),          
@Rating nvarchar(50),          
@LeadStatus nvarchar(10),        
@CurrentApplication nvarchar(200),        
@mailingAddress varchar(250),        
@mailingstreet varchar(250),        
@mailingcity varchar(100),        
@mailingstate varchar(250),        
@mailingzip nvarchar(50),        
@mailingcountry varchar(50),        
--@billingAddress varchar(250),        
--@billingstreet varchar(250),        
--@billingcity varchar(100),        
--@billingstate varchar(250),        
--@billingzip nvarchar(50),        
--@billingcountry varchar(50),      
@salesMgr1 varchar(50),        
@salesMgr2 varchar(50),        
@office varchar(50),        
@CreatedBy bigint,        
@ModifiedBy bigint,        
@description varchar(2500),  
@Source varchar(20),
@LeadAssigned bigint,
@LeadStage varchar(50),
@FacebookUserName varchar(60),
@TwitterUserName varchar(60),
@LinkedInUserName varchar(60),
@GooglePlusUserName varchar(60),
@PInterestUserName varchar(60),
@SkypeUsername varchar(60),
@EstimatedCloseDate DateTime=NULL,
@NextStep VARCHAR(1000)=NULL,
@Probability VARCHAR(500)=NULL,
@BusinessType VARCHAR(200)=NULL                            
)                                                          
AS        
BEGIN        
        
declare @retLeadid int                                             
set @retLeadid = 0;            
INSERT INTO [dbo].[tbl_Lead]        
([LeadName],[Title],[Phone],[Fax],[Email],[Mobile],[PreferTime],[PreferMode],[EmailOptOut],[AccountId],[AccountName],        
[Website],[Ownership],[Employees],[Industry],[AnnualRevenue],[Rating],[LeadStatus],[CurrentApplication],[mailingAddress],        
[mailingstreet],[mailingcity],[mailingstate],[mailingzip],[mailingcountry],[billingAddress],[billingstreet],[billingcity],        
[billingstate],[billingzip],[billingcountry],[salesMgr1],[salesMgr2],[office],[createdDate],[CreatedBy],[ModifiedBy],[description],[Source],[LeadAssigned],[LeadStage],
[FacebookUsername],[TwitterUsername],[LinkedInUsername],[GooglePlusUsername],[PinterestUsername],[SkypeUsername],
EstimatedCloseDate,NextStep,Probability,IsOpportunity,BusinessType)             
        
VALUES(        
@Lead_Name,@Title,@Phone,@fax,@Email,@Mobile,@PreferTime,@PreferMode,@EmailOptOut,@AccountId,@AccountName,        
@webSite,@Ownership,@Employees,@Industry,@AnnualRevenue,@Rating,@LeadStatus,@CurrentApplication,@mailingAddress,        
@mailingstreet,@mailingcity,@mailingstate,@mailingzip,@mailingcountry,'','','',        
'','','',@salesMgr1,@salesMgr2,@office,getdate(),@CreatedBy,@ModifiedBy,@description,@Source,@LeadAssigned,@LeadStage,        
@FacebookUserName,@TwitterUserName,@LinkedInUserName,@GooglePlusUserName,@PInterestUserName,@SkypeUsername,
@EstimatedCloseDate,@NextStep,@Probability,1,@BusinessType)       
        
set @retLeadid = SCOPE_IDENTITY();         
select @retLeadid as retVal        
END 




GO
/****** Object:  StoredProcedure [dbo].[CRM_SaveExtContactDetails]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CRM_SaveExtContactDetails]                                       
(                                                                                    
@FName varchar(100),                                          
@Email varchar(50),
@IsExists INT OUTPUT                                                                                                                                                            
)                                          
AS                                          
IF EXISTS (SELECT 1 FROM [CRMDev].[dbo].[tbl_Contact] WHERE  email LIKE @email)
    BEGIN
        SET @IsExists = 0;
    END
ELSE
    BEGIN
        
 
insert into [CRMDev].[dbo].[tbl_Contact] (fname,email,AccountId,FacebookUsername,createdDate)                                               
values(@FName,@Email,0,@Email,GETDATE())                                 
SET @IsExists = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[CRM_UpdateClientDetailsByClientId]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRM_UpdateClientDetailsByClientId]
@ClientID varchar(100), 
@ClientName VARCHAR(200)
,@companytype  NVARCHAR(50)
,@Country VARCHAR(3)
,@address1 NVARCHAR(150)
,@address2 NVARCHAR(150)
,@city CHAR(50)
,@CompanyLogo VARCHAR(200)
,@companywebsite VARCHAR(150)

AS
UPDATE  [dbo].[Clients]
    SET [CompanyName]       = @ClientName,
        [companytype]       = @companytype,
        [Country]			= @Country,
        [address1]          = @address1,
        [address2]          = @address2,
        [city]       = @city,
        [companylogo]   = @CompanyLogo,
		[companywebsite]   = @companywebsite
        
WHERE   ClientID = @ClientID;
GO
/****** Object:  StoredProcedure [dbo].[CRM_UpdateImportExcel]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  procedure [dbo].[CRM_UpdateImportExcel]                                          
(   
@FieldId nvarchar(50),
@Status VARCHAR(150),    
@Module VARCHAR(100)

)                                        
as           
                         
UPDATE [dbo].[ExcelImportFiles]      
   SET 
     
      [Status] = @Status      
      ,[Module] = @Module 
       
      
 WHERE Fileid=@FieldId      
GO
/****** Object:  StoredProcedure [dbo].[crm_UpdateOpportunityDetail]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[crm_UpdateOpportunityDetail]                                          
(        
@LeadID nvarchar(50),                                           
@Lead_Name varchar(50),                                                                
@Title varchar(200),                                                               
@Phone nvarchar(50),                                                                
@fax   nvarchar(50),           
@Email nvarchar(2000),           
@Mobile nvarchar(20),            
@PreferTime nvarchar(20),                                                             
@PreferMode nvarchar(20),              
@EmailOptOut varchar(50),           
@AccountId nvarchar(10),            
@AccountName varchar(250),             
@webSite varchar(50),                                                                
@Ownership varchar(50),             
@Employees varchar(50),             
@Industry varchar(50),             
@AnnualRevenue nvarchar(50),            
@Rating nvarchar(50),            
@LeadStatus nvarchar(10),          
@CurrentApplication nvarchar(200),          
@mailingAddress varchar(250),          
@mailingstreet varchar(250),          
@mailingcity varchar(100),          
@mailingstate varchar(250),          
@mailingzip nvarchar(50),          
@mailingcountry varchar(50),          
--@billingAddress varchar(250),          
--@billingstreet varchar(250),          
--@billingcity varchar(100),          
--@billingstate varchar(250),          
--@billingzip nvarchar(50),          
--@billingcountry varchar(50),        
@salesMgr1 varchar(50),          
@salesMgr2 varchar(50),          
@office varchar(50),              
@ModifiedBy bigint,          
@description varchar(2500),  
@Source varchar(20),
@LeadAssigned bigint,
@LeadStage varchar(50),
@FacebookUserName varchar(60),
@TwitterUserName varchar(60),
@LinkedInUserName varchar(60),
@GooglePlusUserName varchar(60),
@PInterestUserName varchar(60),
@SkypeUsername varchar(60),
@EstimatedCloseDate DateTime=NULL,
@NextStep VARCHAR(1000)=NULL,
@Probability VARCHAR(500)=NULL,
@BusinessType VARCHAR(200)=NULL
)                                        
as           
declare @created varchar(20)                                  
set @created='0'                              
UPDATE [dbo].[tbl_Lead]      
   SET [LeadName] = @Lead_Name      
      ,[Title] = @Title      
      ,[Phone] = @Phone      
      ,[Fax] = @fax       
      ,[Email] = @Email      
      ,[Mobile] = @Mobile      
      ,[PreferTime] = @PreferTime      
      ,[PreferMode] = @PreferMode      
      ,[EmailOptOut] = @EmailOptOut      
      ,[AccountId] = @AccountId      
      ,[AccountName] = @AccountName      
      ,[Website] = @webSite      
      ,[Ownership] = @Ownership      
      ,[Employees] = @Employees      
      ,[Industry] = @Industry      
      ,[AnnualRevenue] = @AnnualRevenue      
      ,[Rating] = @Rating      
      ,[LeadStatus] = @LeadStatus      
      ,[CurrentApplication] = @CurrentApplication      
      ,[mailingAddress] = @mailingAddress      
      ,[mailingstreet] = @mailingstreet      
      ,[mailingcity] = @mailingcity      
      ,[mailingstate] = @mailingstate      
      ,[mailingzip] = @mailingzip      
      ,[mailingcountry] = @mailingcountry      
      --,[billingAddress] = @billingAddress      
      --,[billingstreet] =@billingstreet      
      --,[billingcity] = @billingcity      
      --,[billingstate] = @billingstate      
      --,[billingzip] = @billingzip      
      --,[billingcountry] = @billingcountry      
      ,[salesMgr1] = @salesMgr1      
      ,[salesMgr2] = @salesMgr2      
      ,[office] =@office      
      ,[modifiedDate] =GETDATE()      
      ,[ModifiedBy] = @ModifiedBy      
      ,[description] = @description      
      ,[Source]=@Source
      ,[LeadAssigned]=@LeadAssigned
      ,LeadStage=@LeadStage
      ,[FacebookUsername] = @FacebookUserName
      ,[TwitterUsername] = @TwitterUserName
      ,[LinkedInUsername] = @LinkedInUserName
      ,[GooglePlusUsername] = @GooglePlusUserName
      ,[PinterestUsername] = @PInterestUserName
      ,[SkypeUsername] = @SkypeUsername,
       EstimatedCloseDate = @EstimatedCloseDate,
       NextStep = @NextStep,
       Probability = @Probability,
       BusinessType = @BusinessType
 WHERE LeadId=@LeadID      
      
set @created='1'                                  
                                  
select @created as retval                                  
return       
      
      
                  
                       
--set @created='1'                                  
                                  
--select @created as retval                                  
--return 



/****** Object:  StoredProcedure [dbo].[crm_GetLeadDetail]    Script Date: 03/20/2014 17:04:19 ******/


GO
/****** Object:  StoredProcedure [dbo].[CRM_UsersExist]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CRM_UsersExist]
(@Username varchar(100),
 @password varchar(100))
AS 
BEGIN
    IF EXISTS (SELECT * FROM dbo.UserMaster WHERE Username = @Username and password = @password )
        SELECT 1
    ELSE
        SELECT 0  
END
GO
/****** Object:  StoredProcedure [dbo].[CRMDefault_InsertClientDetails]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CRMDefault_InsertClientDetails]      
@ClientName varchar(200),@ConnectionString varbinary(500),@ServerName varchar(100),@companywebsite nvarchar(150)    
AS      
BEGIN     
INSERT  INTO dbo.Clients(ClientId,CompanyName,ConnectionString,ServerName,companywebsite)      
VALUES    (@ClientName,@ClientName,@ConnectionString,@ServerName,@companywebsite);     
End 
GO
/****** Object:  StoredProcedure [dbo].[DELETE_CLIENT]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 -- DELETE_CLIENT 'testcanadajignesh'
 -- SELECT * from CLIENTS order by CLIENTNUMBER DESC

 
CREATE PROC [dbo].[DELETE_CLIENT]
@ClientID varchar(100)
AS
BEGIN
--ALTER DATABASE ['+@ClientID+'] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
DELETE FROM CLIENTS WHERE CLIENTID = @ClientID

DECLARE @sql varchar(max)
SELECT @sql ='
DROP DATABASE ['+@ClientID+']'
EXEC (@sql)
----DROP DATABASE ['+@ClientID+'Accounting]

END
GO
/****** Object:  StoredProcedure [dbo].[DELETE_ClientDB]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DELETE_ClientDB]
@ClientID varchar(100)
AS
BEGIN

--DELETE FROM CLIENTS WHERE CLIENTID = @ClientID
ALTER DATABASE ['+@ClientID+'] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
DECLARE @sql varchar(max)
SELECT @sql ='
DROP DATABASE ['+@ClientID+']'
EXEC (@sql)
END
GO
/****** Object:  StoredProcedure [dbo].[DELETE_ClientDBNew]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DELETE_ClientDBNew]
@ClientID varchar(100)
AS
BEGIN

--DELETE FROM CLIENTS WHERE CLIENTID = @ClientID
Declare @sql Nvarchar;
set @sql ='ALTER DATABASE ''+@ClientID+'' SET  SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE '+@ClientID+''

--EXEC (@sql)
END
GO
/****** Object:  StoredProcedure [dbo].[GET_CLIENT_DETAILS]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
    
-- GET_CLIENT_DETAILS 'Apptrino'    
CREATE PROC [dbo].[GET_CLIENT_DETAILS]    
@CLIENTID varchar(100)    
As     
BEGIN    
 IF EXISTS (SELECT 'x' FROM CLIENTS WHERE ClientID = @CLIENTID)    
  BEGIN    
   --SELECT ClientNumber,ClientID,CompanyName,  
   --   Country,'success' as Result     
   --  FROM CLIENTS WHERE ClientID = @CLIENTID    
   SELECT ClientNumber,ClientID,CompanyName as 'ClientName',  
      Country,'success' as Result,companylogo,Timesheet     
     FROM CLIENTS WHERE ClientID = @CLIENTID   
  END    
 ELSE    
  BEGIN    
   SELECT 'Invalid Client ID' as Result    
  END    
END    
    
    
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- [UserLogin] 'sadmin','sadmin'    
    
CREATE PROCEDURE [dbo].[UserLogin]      
(       
@username varchar(100),        
@password varchar(100)      
)        
AS        
        
        
    SELECT * FROM usermaster WHERE username = @username and password = @password 
           

return   

GO
/****** Object:  StoredProcedure [dbo].[USP_CREATE_CLIENT]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
         
-- USP_CREATE_CLIENT 'Nitesh','server=PSPLSERVER\SQL2005;User ID=sa;Password=psplnoida;database=ApptrinoClients; Pooling=false;','nitesh',                  
--'PSPL','USA','','','Test','aa','aa','1111111111','','','true','false','','AA','111111111','02/03/2014','Registered','02/03/2015 12:00:00 AM',                  
--'nitesh','singhal','nitesh_singhal@priyanet.com','nitesh','nitesh','3333333333','test','test','nitesh_singhal@priyanet.com','test','test','3333333333'                         
CREATE procedure [dbo].[USP_CREATE_CLIENT]                      
(                              
@CompanyName varchar(200),                       
@ConnectionString varchar(500),                              
@ClientID varchar(100),                       
@ServerName varchar(100),                       
@Country varchar(3),                       
@Timesheet_Type varchar(100),                       
@Timesheet varchar(10),                       
@address1 nvarchar(150),                       
@address2 nvarchar(150),                       
@city char(50),                          
@companyphone nvarchar(12),                             
@fax nvarchar(15),                         
@companywebsite nvarchar(150),                      
@active bit,                               
@restoredemodata bit,                               
@companylogo nvarchar(500),                            
@state char(50),                         
@zipcode nvarchar(50),                         
@createddate datetime,                       
@companytype nvarchar(50) ,                          
@expirydate datetime,                      
                    
--------------------------                      
@SuperAdminFirstName varchar(200),                                                
@SuperAdminLastName varchar(200),                         
@SuperAdminEmail varchar(200),                      
@SuperAdminLoginID varchar(200),                      
@SuperAdminPassword varchar(200),                      
@SuperAdminPhone varchar(200),                      
--------------------------                      
@AdminFirstName varchar(200),                                                
@AdminLastName varchar(200),                         
@AdminEmail varchar(200),                      
@AdminLoginID varchar(200),                      
@AdminPassword varchar(200),                      
@AdminPhone varchar(200),              
@strFuntion  varchar(20),      
@TimeZone     VARCHAR(50)      
)                       
as                             
declare @success int                          
set @success=0              
            
BEGIN                     
IF(@strFuntion='Insert')              
BEGIN                  
 --   INSERT INTO CLIENTS IN MAIN DATABASE                      
 INSERT INTO dbo.clients                        
 ([CompanyName],[ConnectionString],[ClientID],[ServerName],[Country],[Timesheet_Type],[Timesheet],[address1],[address2],                      
 [city],[companyphone],[fax],[companywebsite],[active],[companylogo],[state],[zipcode],[createddate],[companytype],[expirydate],[site_type])                          
   VALUES                          
 (@CompanyName,EncryptByPassPhrase('SQL SERVER 2008','Data Source=173.48.37.245;User ID=sa;Password=sa!2008;Initial Catalog='+CONVERT(VARCHAR(400),@ClientID)+';'),@ClientID,                          
 @ServerName, @Country,@Timesheet_Type,@Timesheet,@address1,@address2,                          
 @city,@companyphone,@fax,@companywebsite,@active,                          
 @companylogo,@state, @zipcode,@createddate,@companytype,@expirydate,'STAFFING')                          
                       
 --  UPDATE COMPANY SETUP IN NEW DATABASE                      
 DECLARE @CompanySetupSql nvarchar(max);                      
 SET @CompanySetupSql = ' Insert into ['+@ClientID+'].[dbo].[companySetup] (adminEmail,modifiedDate,companyLogo,companyName,siteURL,companyURL,countrycode,companytype,TimeZone)      
   values('''+@SuperAdminEmail+''',getdate(),'''+@companylogo+''','''+@CompanyName+''','''+@companywebsite+''','''','''+@Country+''','''+@companytype+''','''+@TimeZone+''')    '             
                 
 EXEC (@CompanySetupSql)                      
                       
 --  UPDATE BRANCH,SUPER ADMIN,ADMIN IN NEW DATABASE                    
 if ltrim(rtrim(@fax)) = ''                  
 BEGIN                   
 SET @fax = '0'                   
 END                   
                   
 DECLARE @Sql nvarchar(max);                      
 SET @Sql = '                      
                       
 DECLARE @BranchID varchar(10);                      
                       
 Insert into ['+@ClientID+'].[dbo].[Branch]  (branchName, branchAddress1,branchAddress2,branchCity,branchStateCode,branchZip,                                        
  branchPhone,branchFax,branchEmail,createdDate,routingEmail )                                  
 values                                  
 ('''+@CompanyName+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''','''+@zipcode+''',                      
 '''+@companyphone+''','''+@fax+''','''+@SuperAdminEmail+''',getDate(),'''+@SuperAdminEmail+''');                       
                       
 Select @BranchID =  @@Identity;                      
 
 UPDATE ['+@ClientID+'].[dbo].[COMPANYSETUP] SET AssociateBranch=@BranchID,DefaultBranch=@BranchID,EnableBranchSelection=''0'',PTOCalculation=''None'' ;
 
                      
 Insert into ['+@ClientID+'].[dbo].[users] ( firstName,lastName,Fax,addressLine1 ,addressLine2,city,stateCode,                      
 postalCode,phone,email,loginId,password,roleId,status,createdDate,branchId)                               
 values                                                
 ('''+@SuperAdminFirstName+''','''+@SuperAdminLastName+''','''+@fax+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''',                      
 '''+@zipcode+''','''+@SuperAdminPhone+''','''+@SuperAdminEmail+''','''+@SuperAdminLoginID+''','''+@SuperAdminPassword+''',''18'',''1'',                      
 getDate(),@BranchID);                      
                       
 Insert into ['+@ClientID+'].[dbo].[users] ( firstName,lastName, addressLine1 ,addressLine2,city,stateCode,                      
 postalCode,phone,email,loginId,password,roleId,status,createdDate,branchId)                                                
 values                                                
 ('''+@AdminFirstName+''','''+@AdminLastName+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''',                      
 '''+@zipcode+''','''+@AdminPhone+''','''+@AdminEmail+''','''+@AdminLoginID+''','''+@AdminPassword+''',''1'',''1'',getDate(),@BranchID)                      
                       
  '              
  
 --   DECLARE @UpdateCompanySetUpSQL2 varchar(max);              
	--SET @UpdateCompanySetUpSQL2='';  
	--SET @UpdateCompanySetUpSQL2 = '
	--DECLARE @NewBranchID varchar(10);
	--SELECT top 1 @NewBranchID  = BRANCHID FROM ['+@ClientID+'].[dbo].[BRANCH] ORDER BY BRANCHID ASC;
  
	--UPDATE ['+@ClientID+'].[dbo].[COMPANYSETUP] SET AssociateBranch=''+@NewBranchID+'',DefaultBranch=''+@NewBranchID+'',EnableBranchSelection=''0'',PTOCalculation=''None''  '
          
	--EXEC (@UpdateCompanySetUpSQL2);    
   
 --SELECT @Sql                      
  EXEC (@Sql)                
  SET @success= 1;                
   END              
ELSE IF(@strFuntion='Update')              
   BEGIN              
               
  --  UPDATE CLIENT, BRANCH,SUPER ADMIN,ADMIN IN NEW DATABASE                     
 if ltrim(rtrim(@fax)) = ''                  
 BEGIN                   
 SET @fax = '0'                   
 END     
   
 UPDATE [dbo].[clients]              
    SET [CompanyName] =@CompanyName              
    ,[Country] = @Country              
    ,[Timesheet_Type] = @Timesheet_Type              
    ,[Timesheet] =@Timesheet              
    ,[address1] = @address1              
    ,[address2] = @address2              
    ,[city] = @city              
    ,[companyphone] = @companyphone              
    ,[fax] = @fax              
    ,[companywebsite] = @companywebsite              
    ,[active] = @active              
    ,[companylogo] = @companylogo              
    ,[state] = @state              
    ,[zipcode] = @zipcode              
    ,[companytype] = @companytype              
    ,[expirydate] = @expirydate              
  WHERE  ClientID=@ClientID  ;                    
                       
    DECLARE @UpdateCompanySetupSQL varchar(max);          
    DECLARE @UpdateUserSetupSQL varchar(max);              
 SET @UpdateCompanySetupSQL='';            
              
 SET @UpdateCompanySetupSQL = '   UPDATE ['+@ClientID+'].[dbo].[companySetup]              
    SET [companyName] = '''+@CompanyName +'''               
 ,[modifiedDate] = getdate()              
    ,[companyLogo] = '''+@companylogo+'''              
    ,[siteURL] = '''+@companywebsite+'''          
    ,[companytype] = '''+@companytype+'''               
    ,[countrycode] = '''+@Country+'''    
    ,[TimeZone] =  '''+@TimeZone+''' '            
EXEC (@UpdateCompanySetupSQL);            
            
    SET @UpdateUserSetupSQL = '   UPDATE ['+@ClientID+'].[dbo].[users]              
    SET [phone] = '''+@companyphone +'''               
    ,[Fax] = '''+@fax +'''             
    ,[addressLine1] = '''+@address1+'''              
    ,[addressLine2] = '''+@address2+'''          
    ,[city] = '''+@city+'''               
    ,[state] = '''+@state+'''          
    ,[postalCode] = '''+@zipcode+'''          
    ,[stateCode] = '''+@state+'''         
    ,[lastModified] = getdate() where roleId=18'         
               
EXEC (@UpdateUserSetupSQL);          

                  
    SET @success= 1;               
END                 
                
SELECT  @success;                        
              
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CREATE_CLIENT1]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_CREATE_CLIENT1]
(                            
@CompanyName varchar(200),                     
@ConnectionString varchar(500),                            
@ClientID varchar(100),                     
@ServerName varchar(100),                     
@Country varchar(3),                     
@Timesheet_Type varchar(100),                     
@Timesheet varchar(10),                     
@address1 nvarchar(150),                     
@address2 nvarchar(150),                     
@city char(50),                        
@companyphone nvarchar(12),                           
@fax nvarchar(15),                       
@companywebsite nvarchar(150),                    
@active bit,                             
@restoredemodata bit,                             
@companylogo nvarchar(500),                          
@state char(50),                       
@zipcode nvarchar(50),                       
@createddate datetime,                     
@companytype nvarchar(50) ,
@expirydate datetime,                    
                  
--------------------------                    
@SuperAdminFirstName varchar(200),                                              
@SuperAdminLastName varchar(200),                       
@SuperAdminEmail varchar(200),                    
@SuperAdminLoginID varchar(200),                    
@SuperAdminPassword varchar(200),                    
@SuperAdminPhone varchar(200),                    
--------------------------                    
@AdminFirstName varchar(200),                                              
@AdminLastName varchar(200),                       
@AdminEmail varchar(200),                    
@AdminLoginID varchar(200),                    
@AdminPassword varchar(200),                    
@AdminPhone varchar(200),            
@strFuntion  varchar(20),    
@TimeZone     VARCHAR(50)    
)                     
as                           
declare @success int                        
set @success=0            
      BEGIN try
BEGIN                   
IF(@strFuntion='Insert')            
BEGIN                
 --   INSERT INTO CLIENTS IN MAIN DATABASE                    
 INSERT INTO dbo.clients                      
 ([CompanyName],[ConnectionString],[ClientID],[ServerName],[Country],[Timesheet_Type],[Timesheet],[address1],[address2],                    
 [city],[companyphone],[fax],[companywebsite],[active],[companylogo],[state],[zipcode],[createddate],[companytype],[expirydate])                        
   VALUES                        
 (@CompanyName,EncryptByPassPhrase('SQL SERVER 2008',@ConnectionString),@ClientID,                        
 @ServerName, @Country,@Timesheet_Type,@Timesheet,@address1,@address2,                        
 @city,@companyphone,@fax,@companywebsite,@active,                        
 @companylogo,@state, @zipcode,@createddate,@companytype,@expirydate)                        
                     
 --  UPDATE COMPANY SETUP IN NEW DATABASE                    
 DECLARE @CompanySetupSql nvarchar(max);                    
 SET @CompanySetupSql = ' Insert into ['+@ClientID+'].[dbo].[companySetup] (adminEmail,modifiedDate,companyLogo,companyName,siteURL,companyURL,countrycode,companytype,TimeZone)    
   values('''+@SuperAdminEmail+''',getdate(),'''+@companylogo+''','''+@CompanyName+''','''+@companywebsite+''','''','''+@Country+''','''+@companytype+''','''+@TimeZone+''')    '           
               
 EXEC (@CompanySetupSql)                    
                     
 --  UPDATE BRANCH,SUPER ADMIN,ADMIN IN NEW DATABASE                  
 if ltrim(rtrim(@fax)) = ''                
 BEGIN                 
 SET @fax = '0'                 
 END                 
                 
 DECLARE @Sql nvarchar(max);                    
 SET @Sql = '                    
                     
 DECLARE @BranchID varchar(10);                    
                     
 Insert into ['+@ClientID+'].[dbo].[Branch]  (branchName, branchAddress1,branchAddress2,branchCity,branchStateCode,branchZip,                                      
  branchPhone,branchFax,branchEmail,createdDate,routingEmail )                                
 values                                
 ('''+@CompanyName+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''','''+@zipcode+''',                    
 '''+@companyphone+''','''+@fax+''','''+@SuperAdminEmail+''',getDate(),'''+@SuperAdminEmail+''');                     
                     
 Select @BranchID =  @@Identity;                    
                     
 Insert into ['+@ClientID+'].[dbo].[users] ( firstName,lastName,Fax,addressLine1 ,addressLine2,city,stateCode,                    
 postalCode,phone,email,loginId,password,roleId,status,createdDate,branchId)                             
 values                                              
 ('''+@SuperAdminFirstName+''','''+@SuperAdminLastName+''','''+@fax+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''',                    
 '''+@zipcode+''','''+@SuperAdminPhone+''','''+@SuperAdminEmail+''','''+@SuperAdminLoginID+''','''+@SuperAdminPassword+''',''18'',''1'',                    
 getDate(),@BranchID);                    
                     
 Insert into ['+@ClientID+'].[dbo].[users] ( firstName,lastName, addressLine1 ,addressLine2,city,stateCode,                    
 postalCode,phone,email,loginId,password,roleId,status,createdDate,branchId)                                              
 values                                              
 ('''+@AdminFirstName+''','''+@AdminLastName+''','''+@address1+''','''+@address2+''','''+@city+''','''+@state+''',                    
 '''+@zipcode+''','''+@AdminPhone+''','''+@AdminEmail+''','''+@AdminLoginID+''','''+@AdminPassword+''',''1'',''1'',getDate(),@BranchID)                    
                     
  '                    
 --SELECT @Sql                    
  EXEC (@Sql)              
  SET @success= 1;              
   END            
ELSE IF(@strFuntion='Update')            
   BEGIN            
   --  UPDATE CLIENT, BRANCH,SUPER ADMIN,ADMIN IN NEW DATABASE     
    if ltrim(rtrim(@fax)) = ''                
 BEGIN                 
 SET @fax = '0'                 
 END 
                 
 UPDATE [dbo].[clients]            
    SET [CompanyName] =@CompanyName            
    ,[Country] = @Country            
    ,[Timesheet_Type] = @Timesheet_Type            
    ,[Timesheet] =@Timesheet            
    ,[address1] = @address1            
    ,[address2] = @address2            
    ,[city] = @city            
    ,[companyphone] = @companyphone            
    ,[fax] = @fax            
    ,[companywebsite] = @companywebsite            
    ,[active] = @active            
    ,[companylogo] = @companylogo            
    ,[state] = @state            
    ,[zipcode] = @zipcode            
    ,[companytype] = @companytype            
    ,[expirydate] = @expirydate            
  WHERE  ClientID=@ClientID  ;                  
                     
    DECLARE @UpdateCompanySetupSQL varchar(max);        
    DECLARE @UpdateUserSetupSQL varchar(max);            
 SET @UpdateCompanySetupSQL='';    
 SET @UpdateCompanySetupSQL = '   UPDATE ['+@ClientID+'].[dbo].[companySetup]            
    SET [companyName] = '''+@CompanyName +'''             
    ,[modifiedDate] = getdate()            
    ,[companyLogo] = '''+@companylogo+'''            
    ,[siteURL] = '''+@companywebsite+'''        
    ,[companytype] = '''+@companytype+'''             
    ,[countrycode] = '''+@Country+'''  
    ,[TimeZone] =  '''+@TimeZone+''' '          
EXEC (@UpdateCompanySetupSQL); 
    SET @UpdateUserSetupSQL = '   UPDATE ['+@ClientID+'].[dbo].[users]            
    SET [phone] = '''+@companyphone +'''             
    ,[Fax] = '''+@fax +'''           
    ,[addressLine1] = '''+@address1+'''            
    ,[addressLine2] = '''+@address2+'''        
    ,[city] = '''+@city+'''             
    ,[state] = '''+@state+'''        
    ,[postalCode] = '''+@zipcode+'''        
    ,[stateCode] = '''+@state+'''       
    ,[lastModified] = getdate() where roleId=18'       
             
EXEC (@UpdateUserSetupSQL);        
 --   DECLARE @UpdateBranchSetupSQL varchar(max);            
 --SET @UpdateBranchSetupSQL='';          
          
 -- SET @UpdateBranchSetupSQL = '  UPDATE ['+@ClientID+'].[dbo].[Branch]            
 --  SET [branchName] ='''+ LTRIM(RTRIM(@CompanyName))+'''           
                
 -- WHERE [branchName] = '''+@CompanyName+''';'            
 --   EXEC (@UpdateBranchSetupSQL);              
            
                
    SET @success= 1;             
END               
              
SELECT  @success;                      
            
END  
End try

begin catch

 SELECT 
        ERROR_NUMBER() AS ErrorNumber
        ,ERROR_MESSAGE() AS ErrorMessage
        ,ERROR_LINE() AS ErrorLine;
end catch
GO
/****** Object:  StoredProcedure [dbo].[Usp_Custom_Manage_Master]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Manoj Kumar
-- Create date: 10-March-2014
-- Description:	to Manage Custom Fields
-- =============================================
---- exec Usp_Custom_Manage_Master @UserId=1,@Module='company',@flag=3
CREATE PROCEDURE [dbo].[Usp_Custom_Manage_Master]
@FieldId BIGINT=NULL,
@UserID	BIGINT=NULL,
@Module	VARCHAR(20)=NULL,
@Column_Id	INT=NULL,
@ActualColumnName VARCHAR(200)=NULL,
@Column_Label VARCHAR(200)=NULL,
@Column_Description VARCHAR(200)=NULL,
@HoverText VARCHAR(200)=NULL,
@Column_Type VARCHAR(200)=NULL,
@InputDataType VARCHAR(200)=NULL,
@DefaultValue VARCHAR(200)=NULL,
@MaxLength INT=NULL,
@RequiredField INT=NULL,
@IsActive INT=NULL,
@flag INT

AS
BEGIN

--BEGIN TRANSACTION
BEGIN TRY
 
IF @flag=1
BEGIN

Declare @ColCount AS INT
Declare @ColName AS VARCHAR(200)
SET @ColCount = (SELECT Count(Column_Id) From Custom_Manage_Master  Where Module = @Module) -- AND UserId = @UserID)
IF @ColCount < 20
	BEGIN
		SET @ColCount = @ColCount +1;
		SET @ColName = 'CustomField'+ Cast(@ColCount as VARCHAR(10))

		INSERT INTO Custom_Manage_Master
		(
			UserID,
			Module,
			Column_Id,
			ActualColumnName,
			Column_Label,
			Column_Description,
			HoverText,
			InputDataType
		)
		VALUES
		(
			@UserID,
			@Module,
			@ColCount,
			@ColName,
			@ColName,
			@ColName,
			@ColName,
			'text'
		)
		SELECT @@IDENTITY as 'id'
	END

 END

-------- To update
IF @flag=2
BEGIN

	--SET @RequiredField = (Select 0)
	Update Custom_Manage_Master
	SET 
	Column_Label = @Column_Label,
	Column_Description = @Column_Description,
	HoverText = @HoverText,
	Column_Type = @Column_Type,
	InputDataType = @InputDataType,
	DefaultValue = @DefaultValue,
	RequiredField = @RequiredField,
	MaxLength = @MaxLength,
	IsActive = @IsActive,
	ModifiedDate = GetUTCDate()
	WHERE FieldId = @FieldId;
	
	Select 'Successfully Saved.' as 'msg'
	
END

-------- To Get Record by Module and UserId
IF @flag=3
BEGIN
	Select *,Case IsActive WHEN 1 THEN 'Active' ELSE 'In-Active' END AS CStatus from Custom_Manage_Master Where  Module = @Module ---- AND UserID = @UserID 
END

-------- To Get Record By FieldId
IF @flag=4
BEGIN
	Select *,Case IsActive WHEN 1 THEN 'Active' ELSE 'In-Active' END AS CStatus from Custom_Manage_Master Where FieldId = @FieldId
END

IF @flag=5
BEGIN
	Select *,Case IsActive WHEN 1 THEN 'Active' ELSE 'In-Active' END AS CStatus from Custom_Manage_Master Where  Module = @Module AND IsActive=1 ---- AND UserID = @UserID 
END

--COMMIT TRANSACTION
END TRY

BEGIN CATCH

--ROLLBACK TRANSACTION
Insert Into Custom_DbError SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_LINE() AS ErrorLine,
ERROR_PROCEDURE() AS ErrorProcedure,
'Flag '+CAST(@flag as VARCHAR(10))+' '+ ERROR_MESSAGE() AS ErrorMessage ,getDate()

END CATCH

END


GO
/****** Object:  StoredProcedure [dbo].[Usp_Custom_Manage_Value_Master]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Manoj Kumar
-- Create date: 14-March-2014
-- Description:	to Manage Custom Fields Values
-- =============================================
---- exec Usp_Custom_Manage_Value_Master 
CREATE PROCEDURE [dbo].[Usp_Custom_Manage_Value_Master]
@CValueId BIGINT=NULL,
@UserID BIGINT=NULL,
@Module VARCHAR(20)=NULL,
@ModuleRecordId BIGINT=NULL,
@ModuleRecordIdTmp BIGINT=NULL,
@Query VARCHAR(MAX)=NULL,
@flag INT

AS
BEGIN

--BEGIN TRANSACTION
BEGIN TRY
 
 ---- to Execute Insert and Update Query
IF @flag=1
BEGIN

	EXEC (@Query)

END

-------- To Get Record by ModuleRecordId and Module and UserId
IF @flag=2
BEGIN

	SELECT * FROM Custom_Value_Master WHERE ModuleRecordId = @ModuleRecordId AND Module = @Module ---- AND UserID = @UserID
	
END


--COMMIT TRANSACTION
END TRY

BEGIN CATCH

--ROLLBACK TRANSACTION
Insert Into Custom_DbError SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_LINE() AS ErrorLine,
ERROR_PROCEDURE() AS ErrorProcedure,
'Flag '+CAST(@flag as VARCHAR(10))+' '+ ERROR_MESSAGE() AS ErrorMessage ,getDate()

END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[USP_EXECUTE_SCRIPT]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- CREATE_NEW_DATABASE 'Aaaaa'  [USP_EXECUTE_SCRIPT]'Aaaaa'  
-- Select * from Aaaaa.dbo.CountryManager   
CREATE PROC [dbo].[USP_EXECUTE_SCRIPT]    
@ClientID varchar(100)    
AS    
BEGIN    
DECLARE @sql varchar(max)  
SET @sql = '  
  
  
USE ['+@ClientID+']  
  
EXEC sp_fulltext_database  ''enable''  
  
EXEC sp_fulltext_catalog   ''FTCatalog_'+@ClientID+''',''create''   
  
EXEC sp_fulltext_table     ''candidate'', ''create'', ''FTCatalog_'+@ClientID+''', ''PK_candidate''   
  
EXEC sp_fulltext_column    ''candidate'', ''nonXMLresume_textresume'', ''add''   
  
EXEC sp_fulltext_table     ''candidate'',''activate''   
  
EXEC sp_fulltext_catalog   ''FTCatalog_'+@ClientID+''', ''start_full''   
  
  
  
ALTER FULLTEXT INDEX ON candidate START FULL POPULATION;  
  
ALTER FULLTEXT INDEX ON candidate SET CHANGE_TRACKING AUTO;  
  
ALTER FULLTEXT INDEX on candidate START UPDATE POPULATION;  
  
  
  
  
EXEC sp_fulltext_table     ''Resume_Parsed'', ''create'', ''FTCatalog_'+@ClientID+''', ''PK_Resume''   
  
EXEC sp_fulltext_column    ''Resume_Parsed'', ''nonXMLresume_textresume'', ''add''   
  
EXEC sp_fulltext_table     ''Resume_Parsed'',''activate''   
  
  
  
ALTER FULLTEXT INDEX ON Resume_Parsed START FULL POPULATION;  
  
ALTER FULLTEXT INDEX ON Resume_Parsed SET CHANGE_TRACKING AUTO;  
  
ALTER FULLTEXT INDEX on Resume_Parsed START UPDATE POPULATION;  
  
  
  
ALTER TABLE companySetup ALTER COLUMN companyLogo varchar(200)  

'  
--SELECT @sql  
EXEC (@sql)  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ACTIVE_CLIENTS]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GET_ACTIVE_CLIENTS]  
@SITE_TYPE VARCHAR(200)  
  
AS  
BEGIN  
 SELECT distinct ltrim(rtrim(LOWER(ClientID))) from CLIENTS where active='1' and site_type=@SITE_TYPE  
END  
  
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_Client]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[USP_GET_ALL_Client]    
--@clientName varchar(200)    
AS    
BEGIN    
 SELECT  ClientNumber,ClientID,
  CompanyName,Country,Timesheet_Type,Timesheet,address1,address2,city,
  companyphone,fax,companywebsite,active,companylogo,state,zipcode,createddate,companytype,expirydate
     FROM CLIENTS where ClientNumber>1010
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ALL_Opportunity]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- USP_GET_ALL_LEADS '','','','All','50','123179','18'            
Create PROC [dbo].[USP_GET_ALL_Opportunity]              
@FilterExpression nvarchar(max),            
@Keyword nvarchar(max),            
@Email nvarchar(200),            
@LeadOwner varchar(10),            
@office varchar(10),            
@logInId bigint,              
@roleId int              
AS                                
BEGIN              
                                                                                                   
Declare @sql nvarchar(max)                             
Declare @branchs varchar(200)             
Select @branchs=branchId from users  where userId = @logInId and roleId = @roleId                                                                    
                                                                                                                        
set @sql  ='                  
SELECT LeadId,LeadName,LeadName as ''Lead_Name'',Title,tbl_Lead.Phone,tbl_Lead.Fax,tbl_Lead.Email,tbl_Lead.Mobile,        
PreferTime,PreferMode,tbl_Lead.AccountId,AccountName,tbl_Lead.createdDate,modifiedDate,        
users.lastname+'', ''+users.firstname as salesMgr1,        
salesMgr2,office,IsContact,FacebookUsername,TwitterUsername,LinkedInUsername,GooglePlusUsername,PinterestUsername,SkypeUsername,LeadStatus,CONVERT(VARCHAR(15),EstimatedCloseDate,101) as EstimatedCloseDate,AnnualRevenue,
Probability,  
CASE   
WHEN Source = ''Career'' THEN ''Career Builder''  
WHEN Source = ''JOB'' THEN ''Job Fair''  
WHEN Source = ''Monster'' THEN ''Monster''  
WHEN Source = ''NewsPpr'' THEN ''Newspaper''  
WHEN Source = ''WI'' THEN ''Walk In''  
WHEN Source = ''WOM'' THEN ''Word of Mouth''  
WHEN Source = ''YLP'' THEN ''Yellow Pages''  
WHEN Source = ''Dice'' THEN ''Dice''  
WHEN Source = ''ON'' THEN ''Online Networking''  
WHEN Source = ''ER'' THEN ''Employee Referral''  
WHEN Source = ''KJ'' THEN ''Kijiji''  
WHEN Source = ''WP'' THEN ''Workopolis''  
WHEN Source = ''HRDC'' THEN ''HRDC''  
WHEN Source = ''Networking'' THEN ''Networking''  
ELSE '''' END AS Source  
 from tbl_Lead         
INNER JOIN users ON users.userid = tbl_Lead.salesMgr1           
WHERE IsContact=''0'' AND IsOpportunity = 1 '            
            
IF (@FilterExpression!='')                                                                                                      
BEGIN                                          
 SET @FilterExpression=replace(@FilterExpression,'LeadId','LeadId')                                                                                                 
 SET @FilterExpression=replace(@FilterExpression,'LeadName','LeadName')            
 SET @FilterExpression=replace(@FilterExpression,'AccountName','AccountName')                                  
 SET @FilterExpression=replace(@FilterExpression,'Email','tbl_Lead.Email')            
 SET @FilterExpression=replace(@FilterExpression,'phone','tbl_Lead.Phone')                                                         
 SET @FilterExpression=replace(@FilterExpression,'DateCreated','dbo.Get_Telerik_DateTime(convert(datetime,convert(char,tbl_Lead.createdDate ,101)))')                             
 SET @FilterExpression=replace(@FilterExpression,'DateModified','dbo.Get_Telerik_DateTime(convert(datetime,convert(char,tbl_Lead.modifiedDate ,101)))')                           
 SET @sql = @sql + ' and ' + @FilterExpression                                                                                                      
END              
            
                                                                                                                           
IF @Keyword <> ''                                                                                                                                          
BEGIN                                                                                          
 --SET @sql = @sql + ' AND ( LeadName like  ''%' + @Keyword + '%'' '                                                                    
 --SET @sql = @sql + ' or tbl_Lead.Email like  ''%' + @Keyword + '%'' '                                                                                                   
 --SET @sql = @sql + ' or AccountName like  ''%' + @Keyword + '%'' '                                                                                               SET @sql = @sql + ' or Title like  ''%' + @Keyword + '%'' '                     
 --SET @sql = @sql + ' ) '                                                                                                                                      
  
--- NEW IMPLEMENTATION
 SET @sql = @sql + ' AND (  tbl_Lead.SeachIndex like  ''%' + @Keyword + '%''  ) '

END             
            
IF @Email <> ''                                                                                                                                          
BEGIN             
 SET @sql = @sql + ' AND (tbl_Lead.Email like  ''%' + @Email + '%'') '                                                                    
END                      
IF @office <> 'All'                                                                                                                        
BEGIN                                                                                                        
 SET @sql = @sql + '  AND (office =  '''+  @office + ''')'            
END             
            
IF @LeadOwner <> 'All'                      
BEGIN                                      
     set @sql = @sql + ' AND (salesMgr1  = '''+ @LeadOwner +''')'                                          
END                 
 print @sql           
EXEC (@sql)            
--SELECT @sql            
              
           
                          
END 




GO
/****** Object:  StoredProcedure [dbo].[USP_GET_ClientDetail]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EXEC USP_GET_ClientDetail 'nitesh'        
CREATE PROC [dbo].[USP_GET_ClientDetail]
@ClientID varchar(200)          
AS          
BEGIN          
DECLARE @Sql nvarchar(max);              
            
select ClientNumber, ClientID,CompanyName,     
Country,Timesheet_Type,Timesheet,address1,address2,city,companyphone,fax,companywebsite,active,      
companylogo,state,zipcode,createddate,companytype,expirydate      
 from clients where ClientID=@ClientID       
SET @Sql = ' select * from ['+@ClientID+'].[dbo].[users] where roleid=18 ;       
select * from ['+@ClientID+'].[dbo].[users] where roleid=1;  
select * from ['+@ClientID+'].[dbo].[tbl_ApptrinoSettings];  
select * from ['+@ClientID+'].[dbo].[companysetup];  
'        
        
        
EXEC (@Sql)         
--SELECT @Sql        
END        

GO
/****** Object:  StoredProcedure [dbo].[USP_GET_DASHBOARD_DATA]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GET_DASHBOARD_DATA]   
AS   
BEGIN  
  
SELECT   
(SELECT Count(*) from clients where ClientID in (SELECT name FROM sys.databases WHERE  state_desc = 'ONLINE')) as 'TotalClients',  
(SELECT Count(*) from clients where active=1 and  ClientID in (SELECT name FROM sys.databases WHERE  state_desc = 'ONLINE')) as 'ActiveClients',  
(SELECT Count(*) from clients where active=0 and  ClientID in (SELECT name FROM sys.databases WHERE  state_desc = 'ONLINE')) as 'InActiveClients'  
  
-------------------------------------  
  
DECLARE @sql varchar(max)  
SET @sql=''  
declare @acc as int  
set @acc = 1;  
DECLARE @ClientID varchar(100)  
DECLARE @getClientID CURSOR  
SET @getClientID = CURSOR FOR  
SELECT  ClientID from Clients;  
OPEN @getClientID  
FETCH NEXT FROM @getClientID INTO @ClientID  
WHILE @@FETCH_STATUS = 0  
BEGIN  
  
IF  EXISTS (SELECT name FROM sys.databases WHERE  state_desc = 'ONLINE' and name = N''+@ClientID+'')  
BEGIN  
IF @sql<>''  
BEGIN  
SET @sql=@sql + ' UNION ALL '   
END  
SET @sql=@sql + 'SELECT '''+@ClientID+''' as ''ClientID'',  
(select COUNT(userid) from ['+@ClientID+'].[dbo].users where roleid=18) as ''SAU'',  
(select COUNT(userid) from ['+@ClientID+'].[dbo].users where roleid=1) as ''AU'',  
(select COUNT(userid) from ['+@ClientID+'].[dbo].users where roleid=13) as ''SU'' '  
END  
  
------------  
set @acc = @acc + 1  
FETCH NEXT  
FROM @getClientID INTO @ClientID  
END  
CLOSE @getClientID  
DEALLOCATE @getClientID  
  
  
---------------------------------------  
  
DECLARE @sql2 varchar(max);  
  
SET @sql2 = '  
SELECT (SUM(SAU)+SUM(AU)+SUM(SU)) as ''TotalUsers'',  
SUM(SAU) as ''TotalSuperAdminUsers'',SUM(AU) as ''TotalAdminUsers'',SUM(SU) as ''TotalSalesUsers'' FROM  ('+@sql+')t;  
  '  
  
EXEC (@sql2);  
EXEC (@sql);  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_SITE_DETAILS_CLIENTID]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USP_GET_SITE_DETAILS 'local'        
CREATE PROC [dbo].[USP_GET_SITE_DETAILS_CLIENTID] @ClientID varchar(500)        
AS        
BEGIN        
  
  
  IF EXISTS (SELECT 'x' FROM clients  where ClientID=@ClientID)        
   BEGIN        
    SELECT ClientNumber,ClientID,CompanyName,CONVERT(VARCHAR(MAX),DecryptByPassPhrase('SQL SERVER 2008',Connectionstring)) as Connectionstring,AttachmentUrl    
    FROM clients  where ClientID=@ClientID       
   END        
  ELSE        
   BEGIN        
    SELECT '' as 'Connectionstring','' as 'AttachmentUrl'        
   END        
  
END   

GO
/****** Object:  StoredProcedure [dbo].[USP_Validate_Client]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--[USP_Validate_Client] '5000'      
CREATE PROC [dbo].[USP_Validate_Client] @ClientID varchar(500)      
AS      
BEGIN      
DECLARE @value as varchar(500)      
SET @value=''      
 IF @ClientID!=''      
 BEGIN      
  SELECT @value=ConnectionString FROM clients  where ClientID = @ClientID      
  IF @value!=''       
   BEGIN      
    SELECT ClientID,ConnectionString,CompanyName FROM clients  where ClientID = @ClientID    
   END      
  ELSE      
   BEGIN      
    SELECT '' as ClientID, '' as ConnectionString  
   END      
 END      
END 






GO
/****** Object:  StoredProcedure [dbo].[USP_VALIDATE_CLIENT_EXPIRY]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROC [dbo].[USP_VALIDATE_CLIENT_EXPIRY]        
@CLIENTID varchar(100)                         
AS        
BEGIN        
DECLARE @success varchar(10)        
SET @success = '0'      
         
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE EXPIRYDATE>GETDATE() and CLIENTID = @CLIENTID)        
    BEGIN        
      
     SET @success = '1'        
          
   END     
       
SELECT @success        
END
GO
/****** Object:  StoredProcedure [dbo].[USP_VALIDATE_CLIENT_EXPIRY_BY_TYPE]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
-- USP_VALIDATE_CLIENT_EXPIRY_BY_TYPE 'nitesh','Elearning'        
CREATE PROC [dbo].[USP_VALIDATE_CLIENT_EXPIRY_BY_TYPE]    
@CLIENTID varchar(100),      
@SITE_TYPE varchar(100) = NULL                               
AS          
BEGIN          
DECLARE @success varchar(10)          
SET @success = '0'        
      
IF @SITE_TYPE <> 'NULL'   
 BEGIN         
  PRINT '1'     
IF @SITE_TYPE = 'Elearning'  
BEGIN         
  PRINT '2'             
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE EXPIRYDATE>GETDATE() and CLIENTID = @CLIENTID)          
    BEGIN          
            
     SET @success = '1'          
             
   END       
 END    
 ELSE  
 Begin  
  
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE EXPIRYDATE>GETDATE() and CLIENTID = @CLIENTID AND SITE_TYPE = @SITE_TYPE)          
    BEGIN          
           
     SET @success = '1'          
          
   END       
 END  
  
 END   
  
ELSE       
 BEGIN         
  PRINT '3'             
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE EXPIRYDATE>GETDATE() and CLIENTID = @CLIENTID)          
    BEGIN          
            
     SET @success = '1'          
             
   END       
 END           
SELECT @success          
END
GO
/****** Object:  StoredProcedure [dbo].[VALIDATE_CLIENT]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--  VALIDATE_CLIENT 'nitesh','QA'               
CREATE PROC [dbo].[VALIDATE_CLIENT]       
@CLIENTID varchar(100)               
As                   
BEGIN        
           
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE CLIENTID = @CLIENTID)                  
   BEGIN                  
          
    SELECT CLIENTID,'1' as Result,active,convert(varchar(500), DECRYPTBYPASSPHRASE ('SQL SERVER 2008',connectionstring)) as ClientConnectionString FROM CLIENTS WHERE CLIENTID = @CLIENTID                   
   END                  
  ELSE                  
   BEGIN                  
    SELECT '0' as Result                  
   END      
       
END 


GO
/****** Object:  StoredProcedure [dbo].[VALIDATE_CLIENT_BY_TYPE]    Script Date: 09-02-2017 15:11:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
          
          
--  VALIDATE_CLIENT_BY_TYPE 'nitesh','QA'           
CREATE PROC [dbo].[VALIDATE_CLIENT_BY_TYPE]   
@CLIENTID varchar(100),  
@SITE_TYPE varchar(100)
As               
BEGIN    
IF @SITE_TYPE <> 'NULL'  
 BEGIN  
  PRINT '1'            
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE CLIENTID = @CLIENTID AND SITE_TYPE = @SITE_TYPE)              
   BEGIN              
    --SELECT CLIENTID,'1' as Result   FROM CLIENTS WHERE CLIENTID = @CLIENTID         
    SELECT CLIENTID,'1' as Result,active FROM CLIENTS WHERE CLIENTID = @CLIENTID  AND SITE_TYPE = @SITE_TYPE                
   END              
  ELSE              
   BEGIN    
   declare @boolresult bit =0
    -- SELECT '0' as Result            
    SELECT  '' as CLIENTID,'0' as Result  ,@boolresult as active            
   END  
 END  
ELSE   
 BEGIN      
  PRINT '2'        
  IF EXISTS (SELECT 'x' FROM CLIENTS WHERE CLIENTID = @CLIENTID)              
   BEGIN              
    --SELECT CLIENTID,'1' as Result   FROM CLIENTS WHERE CLIENTID = @CLIENTID         
    SELECT CLIENTID,'1' as Result,active FROM CLIENTS WHERE CLIENTID = @CLIENTID               
   END              
  ELSE              
   BEGIN              
   -- SELECT '0' as Result   
   declare @boolresult2 bit =0

   SELECT  '' as CLIENTID,'0' as Result  ,@boolresult2 as active             
   END  
 END    
END 
GO
USE [master]
GO
ALTER DATABASE [Mentor_Clients] SET  READ_WRITE 
GO
