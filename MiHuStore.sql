USE [master]
GO
/****** Object:  Database [mihu]    Script Date: 12/29/2019 8:31:58 PM ******/
CREATE DATABASE [mihu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mihu_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\mihu.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'mihu_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\mihu.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [mihu] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mihu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mihu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mihu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mihu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mihu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mihu] SET ARITHABORT OFF 
GO
ALTER DATABASE [mihu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mihu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mihu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mihu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mihu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mihu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mihu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mihu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mihu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mihu] SET  ENABLE_BROKER 
GO
ALTER DATABASE [mihu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mihu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mihu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mihu] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [mihu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mihu] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [mihu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mihu] SET RECOVERY FULL 
GO
ALTER DATABASE [mihu] SET  MULTI_USER 
GO
ALTER DATABASE [mihu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mihu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mihu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mihu] SET TARGET_RECOVERY_TIME = 120 SECONDS 
GO
ALTER DATABASE [mihu] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'mihu', N'ON'
GO
ALTER DATABASE [mihu] SET QUERY_STORE = OFF
GO
USE [mihu]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_diagramobjects]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE FUNCTION [dbo].[fn_diagramobjects]() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[orderID] [int] IDENTITY(1,1) NOT NULL,
	[createdDate] [datetime] NULL,
	[customerID] [varchar](50) NULL,
	[staffID] [varchar](50) NULL,
	[state] [int] NULL,
	[description] [varchar](200) NULL,
	[totalPrice] [float] NULL,
	[discountValue] [float] NULL,
	[completeDate] [datetime] NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cate]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cate](
	[cateID] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](20) NULL,
 CONSTRAINT [PK_Cate] PRIMARY KEY CLUSTERED 
(
	[cateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[username] [varchar](50) NOT NULL,
	[fullname] [varchar](50) NULL,
	[tel] [varchar](10) NULL,
	[address] [varchar](100) NULL,
	[email] [varchar](50) NULL,
	[point] [float] NULL,
	[rank] [varchar](10) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[discountDetail]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discountDetail](
	[rank] [varchar](10) NOT NULL,
	[discountValue] [float] NULL,
 CONSTRAINT [PK_discountDetail] PRIMARY KEY CLUSTERED 
(
	[rank] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderDetail]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderDetail](
	[orderID] [int] NOT NULL,
	[productID] [varchar](10) NOT NULL,
	[quantity] [int] NULL,
	[unitPrice] [float] NULL,
	[productName] [varchar](50) NULL,
 CONSTRAINT [PK_orderDetail] PRIMARY KEY CLUSTERED 
(
	[orderID] ASC,
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[productID] [varchar](10) NOT NULL,
	[productCategoryID] [int] NOT NULL,
	[dateOfCreate] [datetime] NULL,
	[unitPrice] [float] NULL,
	[productName] [varchar](50) NULL,
	[isDeleted] [bit] NULL,
	[image] [varchar](max) NULL,
	[stock] [int] NULL,
	[description] [varchar](500) NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roleDes]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roleDes](
	[role] [int] NOT NULL,
	[description] [varchar](10) NULL,
 CONSTRAINT [PK_roleDes] PRIMARY KEY CLUSTERED 
(
	[role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[staff]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[staff](
	[username] [varchar](50) NOT NULL,
	[fullname] [varchar](50) NULL,
	[tel] [varchar](10) NULL,
	[address] [varchar](100) NULL,
	[email] [varchar](50) NULL,
	[isDeleted] [bit] NULL,
 CONSTRAINT [PK_staff] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'hiep', N'123', 1)
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'huytrieu', N'123', 2)
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'kay', N'123', 0)
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'sang', N'123', 1)
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'thaitu', N'123', 2)
INSERT [dbo].[Account] ([username], [password], [role]) VALUES (N'tienlo', N'123', 2)
SET IDENTITY_INSERT [dbo].[bill] ON 

INSERT [dbo].[bill] ([orderID], [createdDate], [customerID], [staffID], [state], [description], [totalPrice], [discountValue], [completeDate]) VALUES (1, CAST(N'2019-12-23T16:07:59.303' AS DateTime), N'thaitu', NULL, 0, NULL, 17.100000381469727, 0.05000000074505806, NULL)
INSERT [dbo].[bill] ([orderID], [createdDate], [customerID], [staffID], [state], [description], [totalPrice], [discountValue], [completeDate]) VALUES (2, CAST(N'2019-12-23T16:09:07.530' AS DateTime), N'thaitu', NULL, 0, NULL, 33.25, 0.05000000074505806, NULL)
INSERT [dbo].[bill] ([orderID], [createdDate], [customerID], [staffID], [state], [description], [totalPrice], [discountValue], [completeDate]) VALUES (3, CAST(N'2019-12-23T16:11:22.627' AS DateTime), N'thaitu', NULL, 0, NULL, 33.25, 0.05000000074505806, NULL)
INSERT [dbo].[bill] ([orderID], [createdDate], [customerID], [staffID], [state], [description], [totalPrice], [discountValue], [completeDate]) VALUES (4, CAST(N'2019-12-29T17:02:38.523' AS DateTime), N'tienlo', NULL, 0, NULL, 3572.01904296875, 0.05000000074505806, NULL)
SET IDENTITY_INSERT [dbo].[bill] OFF
SET IDENTITY_INSERT [dbo].[Cate] ON 

INSERT [dbo].[Cate] ([cateID], [description]) VALUES (9, N'accessory')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (10, N'bag')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (8, N'dress')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (4, N'hoodie')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (3, N'jacket')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (6, N'pant')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (1, N'shirt')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (7, N'skirt')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (5, N'sweater')
INSERT [dbo].[Cate] ([cateID], [description]) VALUES (2, N't-shirt')
SET IDENTITY_INSERT [dbo].[Cate] OFF
INSERT [dbo].[customer] ([username], [fullname], [tel], [address], [email], [point], [rank]) VALUES (N'thaitu', N'Thai Tu De Nhat', N'1234567890', N'Chung cu sky 9 quan 99', N'thaitu@gmail.com', 83.600000381469727, N'bronze')
INSERT [dbo].[customer] ([username], [fullname], [tel], [address], [email], [point], [rank]) VALUES (N'tienlo', N'Tien lo lo lo', N'1234567890', N'jaslkdalksdjaskl aslkdjaskld', N'tienlo@gmail.co', 3672.01904296875, N'silver')
INSERT [dbo].[discountDetail] ([rank], [discountValue]) VALUES (N'bronze', 0.05)
INSERT [dbo].[discountDetail] ([rank], [discountValue]) VALUES (N'gold', 0.15)
INSERT [dbo].[discountDetail] ([rank], [discountValue]) VALUES (N'platinum', 0.2)
INSERT [dbo].[discountDetail] ([rank], [discountValue]) VALUES (N'silver', 0.1)
INSERT [dbo].[orderDetail] ([orderID], [productID], [quantity], [unitPrice], [productName]) VALUES (3, N'SW5', 1, 35, N'Long-sleeved round neck pullover')
INSERT [dbo].[orderDetail] ([orderID], [productID], [quantity], [unitPrice], [productName]) VALUES (4, N'SW7', 99, 37.979999542236328, N'Super fire cec jacket')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'HD1', 4, CAST(N'2019-10-10T18:43:58.943' AS DateTime), 49.98, N'Quality Pullover Hoodie', 0, N'https://lh3.googleusercontent.com/WSXQK-7WgLbuAVrM5bmQ8ivUHjQqAO_DJcn2s9dnyhFelGw_dNOkKklT5jviPLoCv71Lo-0zuAe6pDRpbuukP6PGYc7BKG7rwzALZvIrAbWyJ5jj5NEntaQAXFSA2bLY9C9IxdfzPaRO4J-l9uY2Y-CJZnbQMGmZBMxVndez-VYFWDFkvuJVqLgayEw8Rp4PqUU_vCAHS7n7bV2HE3Z-tL7vjcRpyny-Uv8j-bSh8R8DBfYRgPU7-1l-OtQoekuquK2L-QoXKTuY0TNAtAdKK0yh14A8sQwbca8E1HdkTX2uT2rXrjY5bOvQYGLG5jZPR_XWorOj_FIAvADZQoy1HDbsLWGM7Oyd41Fmg_hn3itdq7FqWtNDMYB7Ldz8SUprcvQ2bZ24TCyb01igrqhpygOHd6YYxpj6xQLbIsmvyZqkx24bqAQTlRQLbS-EJ6JF_KKl-kmtMa0ob6MrukRoc4paSNgdq5ZN2v9wt208GFpwnUhxglj5z96WQNfRhD_74tVzGL9bXW6KP9hODx_P8PY7F5KoE5VREg5uB17Zlomhlw4teEm80x30sMNRNEfIbz4rDyjGjeLtto_525-Sw08gC3QW5eVbUnLFRLuSWNyVbCNu1xUuXGJyikfFof_J7NacGoZY5nEJ5FqufW3PUrotr54Wq0f7zBJA0rRq8o8M7e5_CtKu1W3_1uNHiQyIjK6f75OJK9Ad7gLa_XyB0qjPNCQ44kSTB1WhjuET0DlXtAYh=s800-no', 46, N'Korean version of chic hooded pullover sweater women spring and autumn thin section 2019 new loose bf long section super purchase jacket')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'HD2', 4, CAST(N'2019-10-10T18:44:42.770' AS DateTime), 34.98, N'Pink Hoodie', 0, N'https://lh3.googleusercontent.com/KF0wg7RmU8CPdrgWUhpvjEm5_oabQFaF_qxXub0M-sRKJVBPXZ9GM4eXu7tPgdLBtViW7VmoN-tUZR133Ywb_JqMmsOrN32qQQmYjOUhH_esxwZy6GTzXtl9HH8TsO1S53cGv4rMArLqmMe1jWzuHthWWvlQ94vh4wc2LvZyzonpBjmyNafVrb10tktrW_BgGXp0-HFqtll_-qjS8VRsnRUsGdf2DQp6F4d1-Vpmoth0lhZ4L77OLD67K-OhepXJGWqtyZXKsLHDCw-4UAnmWxIhZ_Lbk63g556X7cA0fNNUoCON1arjce5voVFJMlqzaL8ip2f5KyM9EdTVd15xQqSoATEXcjc6aPH_JKsFu_UjAJlFkk5LvTJ4RQRHHLyw7HWT7bxPpLsWHZ0L8swqWfczrc66KL9OUBDLg7p5YeWJ1MRAf4khoME4IHnGJx5Yo_3EajmJBxL_8A6wjphm4zUVW0JcTJYavQhFc6l18rHv_dAF8RSQxREfDi5eqkGDo-yFqAgL2rSoqH2a5nyqPqANhm5OcZpAPMgFmdYo23I8zGqeNyGt4kf456XcTEueXOgdGoewzJ1x9-lgbgJ70Y6XlKds6oFlrxcD8xSBIQlFE2OsSUjczvtzJ6GdPqR6NNF4MUCNpF4wQxGZ-ERqJAMaHuFuEtlNbK_KInir8J_EXAxa1fNi_2WmLmP6_Mwxk9W4_Rgnb1xb-bFvQ9N6SUb2CHi7oi-qke-GvLVjje4LZsNa=s800-no', 342, N'Pink sweater women ins autumn shirt 2019 new Korean version of the loose long paragraph spring and autumn hooded long-sleeved jacket')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'PA6', 6, CAST(N'2019-10-05T16:12:08.617' AS DateTime), 19, N'Quan', 0, N'https://i.imgur.com/sdxyPR6.jpg', 200, N'Nice')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SH1', 1, CAST(N'2019-10-02T17:19:31.267' AS DateTime), 15, N'Shirt', 0, N'https://i.imgur.com/sdxyPR6.jpg', 100, N'supreme')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW1', 5, CAST(N'2019-10-10T09:13:16.093' AS DateTime), 20, N'Color Striped Knit Sweater', 0, N'https://lh3.googleusercontent.com/aN70_dWc6He_FA6Y6cLewsIgnEZ5hdlrS8FijS9j80HcPDhS0qvKxZqmzOB1cI6lXGA7lTbEyjjBvvH-rE9XDt-6bFzpb2T8HVMhtqmms_NseF0QAxocVL2V6sJ6ObeeDgVlQPGZRZeO0yHjO46u7KJyHnAkliD_XuwUZFFFtI1NcY11BI1MD2X_ua5tB-jczFTr5yGNx6k4vlJZGVmdZAnvdw4naGmgoIJaJFP6XEXBZu4vokXqJeOyE-_n88sc_vV4wSxrE3ecAV5qvCJJNw3TOEzH0gZduozdhdkv3TXCLS4yr88M0UzHR0Hw0qXpzOqozhpYnuVACqVVxHl5BGdOUhNaFZOSB3LvK1SRJ2XFuRDW6qqTJGVxLf8kxC2gGc4r5FjhJkpJp6OClOGtxp4r-W7vbhXZ9pCMstM7TzntTSHQgLFWgpVFUX0LUtmsgDIej4QGguMm1ZDg7uihPLSE_Q0L0JJNCp6wec_y7oN5t_yPeWxaaLopZ7xmakDPJJhzGmHfIkcxB67P5_U3C4oheWCit-L1wp9v741vT1hoEWtM5Hi4N26CT6MKuYNfhwJHiBtJ9KGtjcJUF5D7gnUq05GSz2a8TrpKKGHLad1MVuXHl0-RwciPdwm18a-N_al_uF-lvYN9CYIBMj1NfmWgUYjO9mGvS9-FIWEeh8vPZos1CFxP8-JcFGmHHGU8jjam_JFE5gOPMg6hnrhVuD3tFYJEXIALoY8pXjZ_HezKKDXe=s800-no', 101, N'super good')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW2', 5, CAST(N'2019-10-10T09:31:04.230' AS DateTime), 18, N'Couple Knit Blue Sweater', 0, N'https://lh3.googleusercontent.com/ejsLTlUXpdzQwxcf11rFwL2eOEptmXFsvIQLqlKDYZlpDXOE6qE4PskMNTnZ1xIYt5eM5ce8Ibp6ghcSp9CyBEC4tvilbFts-QpufsCFbs3nUyZEolQpDhq9pNeA-_ulCbiQv1ouR_5r-rucGhdU0GdKDkva5EGNIpXRjRtLd5_iSO1xjNm3GGnc4F1eyu_KUVjiIhsRRUE4wdEKN-eT4xGqb59wEGuP0eZBUmgfl1h4MWvkwxsLYPTYOYDPA-Y-gtK5LRCUj7fqhBlzA-CQlSnzvguUl6qL5CzNxpK_93nRZEknJ0YAOUVwNiaSyMNSN5MYm7BCyjgul0qbL_e8G0Rzht-Oy7rLAqqiWYZuIh2Zrqh_FbrbtQyZq2rZumbSXZTH2S6szyHnJtFnYO5I3FtjKcqJvayuV8CBPW3GaN5lYditURgdZDUPzzy6rYNSbyZ-WXOicT60HzApZguLyd5bW2DWvYyGFfJIW1qSd5mOliDwnN61psbx15I2YiYPlNppBXXiUnSlQjaP-zuVBEGoV-8Bm38Wkoi6BWp1aZQoQmR-lIRrtjMCP8knWIL4C_9Y6HH2PkkbVl3Zw6azbMPRua6J7HGjnsfgPd0N2T-7PfYGdXh_5tIz6c2XiXcGkv8fK_2_jAPJP5gGYDGvq99hU-C_GTBptnqGBxJNIMpEGB9hlMOYGGXZoIOlPyS9AV7t2BdezxRjJAuIVaI4wbNkzYsH1neQj0PyW6HjeFxg9BAj=s760-no', 59, N'Very good')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW3', 5, CAST(N'2019-10-10T09:32:22.150' AS DateTime), 22, N'Very colorful knit sweater', 0, N'https://lh3.googleusercontent.com/sBmF5JHyMPlSuPQ4wpxTmkDzSXzT4MGOG0OfUnYlRBPsvocx6rPaoD2YRHAqgjsS19ME1jO7AjVyVPngd8YFHSmypScX7bJuv_T9hYRKuWKTSM2npAEjVBO9LDJKFi-WHv2qLHMNbPof1w8z7FKqL1M6aVnTcMEgKlIuzLP0rBfdbenhS-JxwldH_m6iCpTXUJOTAivr4ACf7f_JrVvVQG1odENnKzdZOx_LBi73dyLcIDm2J5g1aXV1o7hoCmEW_4r_zVZDWtdRBE5a6D1zAlyUuIsxtNCmd8a5E99BOnf092R-FtK-qs-ttNPY_aIeNESoO02o86-83BKtBb4PGLJnoldG8efB1BkcktWD4_stdJf6YgNo014wIuLKS7D7XCVMlaDm_MFX9_cc2fbp3zFKbb9ZnWNOMfa0liNhXlzUN3K1jTAkg7QFEKjeHUx46WEil62DsJhzhqhLP5aClrT2lFTVnJrx95SOU8n-4mQEzmaq4MTqgR5b9IC6_EWln5zBZ1EGEaOYtjHV8EId5lkpmbSVmlqz9cwK3sObXJ0D1fLAqeVNwkrM7k3XjVYo2H4YTAgozmLlivES-tp_h-WlduVqsaKz5IyMDbfU0V2S6p-kRH4xRWqitly2sUhD05mrPU5rBtAT8ERDR7g-fWjQm5GgVB2tfcQrlhSf7LVdQ3gWeIWQA8K-iPhU7tXPR1DQNb3apFbPP2vVMhV68nhv8aKYjfQMIIqG2y8nN0AWT4XY=s620-no', 73, N'Blue, Green, White and yellow mix together make a beautiful knit sweater')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW4', 5, CAST(N'2019-10-10T10:14:29.527' AS DateTime), 19, N'Very colorful knit sweater', 0, N'https://lh3.googleusercontent.com/sBmF5JHyMPlSuPQ4wpxTmkDzSXzT4MGOG0OfUnYlRBPsvocx6rPaoD2YRHAqgjsS19ME1jO7AjVyVPngd8YFHSmypScX7bJuv_T9hYRKuWKTSM2npAEjVBO9LDJKFi-WHv2qLHMNbPof1w8z7FKqL1M6aVnTcMEgKlIuzLP0rBfdbenhS-JxwldH_m6iCpTXUJOTAivr4ACf7f_JrVvVQG1odENnKzdZOx_LBi73dyLcIDm2J5g1aXV1o7hoCmEW_4r_zVZDWtdRBE5a6D1zAlyUuIsxtNCmd8a5E99BOnf092R-FtK-qs-ttNPY_aIeNESoO02o86-83BKtBb4PGLJnoldG8efB1BkcktWD4_stdJf6YgNo014wIuLKS7D7XCVMlaDm_MFX9_cc2fbp3zFKbb9ZnWNOMfa0liNhXlzUN3K1jTAkg7QFEKjeHUx46WEil62DsJhzhqhLP5aClrT2lFTVnJrx95SOU8n-4mQEzmaq4MTqgR5b9IC6_EWln5zBZ1EGEaOYtjHV8EId5lkpmbSVmlqz9cwK3sObXJ0D1fLAqeVNwkrM7k3XjVYo2H4YTAgozmLlivES-tp_h-WlduVqsaKz5IyMDbfU0V2S6p-kRH4xRWqitly2sUhD05mrPU5rBtAT8ERDR7g-fWjQm5GgVB2tfcQrlhSf7LVdQ3gWeIWQA8K-iPhU7tXPR1DQNb3apFbPP2vVMhV68nhv8aKYjfQMIIqG2y8nN0AWT4XY=s620-no', 89, N'Blue, Green, White and yellow mix together make a beautiful knit sweater')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW5', 5, CAST(N'2019-10-10T10:30:34.970' AS DateTime), 35, N'Long-sleeved round neck pullover', 0, N'https://lh3.googleusercontent.com/ffvJUtwoWf1JUEnnNYEyLZVvoXQwPKFL74Qww4oI7uTqgxBEXA6T0C7CSikZ9RihZayqTZkU0UyVNuTciP6JocYneMf0QL1awy58eDzsYO0coc6NWz2Bmp7iORe0aSyA0p_0TLPAdOOFuGLcfToqjBJ0pd0lohzhik8jIdixufAibemCvVrc4FHM07AjqYJWPg6qb5X6aWe0Hz0uYc9lnQUDSz7uhQkszd9HTqSngeCmVO1l0n1Mj-Ja1gZLRo2QcMcP32HSEI7HRVE7WiOZAw0zA51HpvSobBZM10DPNc-lctiY7GosYFivLcpT96acOfSD6Yxbxhr4T0uLhD2a0RZFVdnruH1gqecEVRtRQsImRKuYrdxyD4unPk-HyBSd-eMccwZZv5KD_vsPdUqkTdnYdzq_nS51gkLqko6CvE9F0LbZhN9C9_584P4ka1nE32ncDwa0qXqJ83euQpGI5ujrT4J1iU9rDbihXULef72z-QAygUctuk82wKlelGGBykhrXO7sug_oCDyqFETp4CSe1W6xRo07eYSlOF-UlbLrO9mgaT1HOojO4HPMMTRou5FfUDlVi6Le_2hOd8oETttIi5LjhlAnS6mA5wsXv8A9_QUrQP3GlSWdhSKLMnFyu-Muk4fiWIchXHf_BP58o32aKONquUAAHjW8NoCw37aC6Adoj07G4Y_csrFs7jq87cI3kCKR7C-rFb78z7aJ_LFYMfFL38ZCEnMZ7-S6Ma4At8l8=s800-no', 106, N'Clothes autumn 2019 new Korean version of the long-sleeved round neck pullover gray sweater women''s tide ins autumn loose shirt')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW6', 5, CAST(N'2019-10-10T10:31:50.600' AS DateTime), 35, N'Spring and autumn thin sweater', 0, N'https://lh3.googleusercontent.com/6QqpPfcdtR8kGzm3ERYX9RFBEstvdxOHQ-7MJy-_QP6HSYRBVRO0X0B9zChS-UPV62my2ejrUIqIe9_3G5O0U0h5uhsjx9StVIyBcI2lNY_LMU6lzxvsm2XOnpKZaqH9OTjODxDTcHM7f6IGy7bCxwljUcIre-JmbJvnYk_JnogoXu52dSJFsMbWqyzVDsWnM6eMcEQVGY87dWNJxEcuBzZlKuX85-sEPRXRUrk3yNZdYamtdN5OPrJuyF2sNUShKb5mFQiWqyegHNzfiQIIaW9olP0H7wnIo7NCELoyr3nmfdvI4dEMNmfHya6fvwzIa4_DH19NhMuzhrx96epXlO4qOPuy91hEk4I-uaYbCi1Rvmth24WmIhJIBaxADlyBnBp0mriuq0xNPBC07vyXJkQd2ruy2KYCYKUMf9kxb_0v1W8gk-ZPY7O6g1y7qmvLSDkJg1qCJ451eBxRlnlRuxZAZ-Rmw5IxKKcEW_ij0bJTUAaL1JVNujNVfW8eHPuzQRHCasFKROhsV2QZZsjM48f3fTxSk2NdRnZnD5fUHJH4wD5P1JQW68kvOKnqD4cY1QOI0n6DsXvZvSt6TBmflJVkfCFyLEAq0tTX1ZvR8oWBF4rAjOkLdsp45MEnwt9wLe0wiOefB4RBrgGhE6QZ3ozHn0gFsphYJvWUk1j-PiW0re5BE0ZvDlGYADm1cEc56OYgfNQxVUmDUJ8YQPliKGulFqf0DwOHeozW9a0zbLE5RFAo=s357-no', 70, N'Long section hooded spring and autumn thin sweater women''s autumn 2019 new Korean version of the loose BF wind wild long-sleeved jacket')
INSERT [dbo].[product] ([productID], [productCategoryID], [dateOfCreate], [unitPrice], [productName], [isDeleted], [image], [stock], [description]) VALUES (N'SW7', 5, CAST(N'2019-10-10T18:42:46.980' AS DateTime), 37.98, N'Super fire cec jacket', 0, N'https://lh3.googleusercontent.com/PVLkMvJvEb2ts_Qdryzn4Jr-M21vu9e-NZOvNkPyPULv0kd7URpk1C_vyUyMUpT2C0LyuigrilLP5Yc95gUVLv5BjabYWUSAyZMluT-QS3SwZHGUd9K8ksIgXfduj8N7Wn07BqnRKn5YOGEspVw1UrqU4FQz5HxCB7aK3iAHt_JJUyoM88FYdt2AaQE4LIQ3LwBoJx9mw7eVvHGLvG4zWsL4c_hidSIQ0DYpjVXyAauR8wy7P97NtGnxwi2Y-a_rxX4bIhUblOyggCjyHmMoBYmlqYrZnCO0B2wEcngRA19sTVdxwVv6lEFY3ld_fO3lf9zM8LJTUfJymz3x465VsRRBFuLtxUhXOtYPu0Afc2PLv4HteCCLTytBAGviFVEba_ezGY5AjdjirXHx4K9o7lTXh8TG1v4FodDN9uTju61_LioVC3X4wlp3aVL7JGDpHBfWybGwwI_KyZkvTEf2bMvd2V8NREfs-_5y-jjL761xvLnrZbkvPRRqXXP3Vzw77gcN2PLELLGKPCJEyDiWvWKilM33UKdmb6Dru1opfFVcKJywjE3z0xa1sBQotKqQTtIygY6mQ6NjwPX72RqLUVd5BS3Inc0xfKJbTvNK-ezz18OgcpvN5FvEqT-WFNZUkQAwNVM5OwatxeltfsAuABimOzy53WJabGwSVNc9ybci6h53EzzmWqT_4CctX5B8yDWYrACDHJgipcZ1qisWFI4hHlOw8U3PPOsPVvd9Svmbb0H-=s800-no', 0, N'Autumn new 2019 Korean version of the high collar zipper sweater women''s spring and autumn thin section loose shirt super fire cec jacket')
INSERT [dbo].[roleDes] ([role], [description]) VALUES (0, N'admin')
INSERT [dbo].[roleDes] ([role], [description]) VALUES (1, N'staff')
INSERT [dbo].[roleDes] ([role], [description]) VALUES (2, N'customer')
INSERT [dbo].[staff] ([username], [fullname], [tel], [address], [email], [isDeleted]) VALUES (N'hiep', N'hiepduc', N'927837283', N'dongnai', N'123@123.a', 0)
INSERT [dbo].[staff] ([username], [fullname], [tel], [address], [email], [isDeleted]) VALUES (N'sang', N'sang sang', N'085123123', N'quan 5', N'sangsang@123.1823', 0)
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cate]    Script Date: 12/29/2019 8:31:58 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Cate] ON [dbo].[Cate]
(
	[description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_principal_name]    Script Date: 12/29/2019 8:31:58 PM ******/
ALTER TABLE [dbo].[sysdiagrams] ADD  CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bill] ADD  CONSTRAINT [DF_order_createdDate]  DEFAULT (getdate()) FOR [createdDate]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF_customer_point]  DEFAULT ((0)) FOR [point]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_dateOfCreate]  DEFAULT (getdate()) FOR [dateOfCreate]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_roleDes] FOREIGN KEY([role])
REFERENCES [dbo].[roleDes] ([role])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_roleDes]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [FK_order_customer] FOREIGN KEY([customerID])
REFERENCES [dbo].[customer] ([username])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [FK_order_customer]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [FK_order_staff] FOREIGN KEY([staffID])
REFERENCES [dbo].[staff] ([username])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [FK_order_staff]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_Account] FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_Account]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_discountDetail] FOREIGN KEY([rank])
REFERENCES [dbo].[discountDetail] ([rank])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_discountDetail]
GO
ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_order] FOREIGN KEY([orderID])
REFERENCES [dbo].[bill] ([orderID])
GO
ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_order]
GO
ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_product] FOREIGN KEY([productID])
REFERENCES [dbo].[product] ([productID])
GO
ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_product]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_Cate] FOREIGN KEY([productCategoryID])
REFERENCES [dbo].[Cate] ([cateID])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_Cate]
GO
ALTER TABLE [dbo].[staff]  WITH CHECK ADD  CONSTRAINT [FK_staff_Account] FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[staff] CHECK CONSTRAINT [FK_staff_Account]
GO
/****** Object:  StoredProcedure [dbo].[sp_alterdiagram]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_alterdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_creatediagram]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_creatediagram]
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_dropdiagram]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_dropdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagramdefinition]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagramdefinition]
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagrams]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagrams]
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_renamediagram]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_renamediagram]
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_upgraddiagrams]    Script Date: 12/29/2019 8:31:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_upgraddiagrams]
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO
USE [master]
GO
ALTER DATABASE [mihu] SET  READ_WRITE 
GO
