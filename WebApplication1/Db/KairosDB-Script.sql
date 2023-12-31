USE [KAIROS]
GO
/****** Object:  Table [dbo].[Produk]    Script Date: 6/29/2023 10:15:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produk](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[kode_barang] [varchar](200) NOT NULL,
	[nama_barang] [varchar](200) NOT NULL,
	[jumlah_barang] [int] NOT NULL,
	[tanggal] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Produk] ON 

INSERT [dbo].[Produk] ([id], [kode_barang], [nama_barang], [jumlah_barang], [tanggal]) VALUES (11, N'001', N'Barang 1', 5, CAST(N'2023-06-29T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Produk] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_delete]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_delete]
@ProductId INT
AS
BEGIN
	 DELETE FROM produk 
	WHERE id = @ProductId
	 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_get]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_produk_get]
AS
BEGIN
	SELECT
		id [ProductId],
		kode_barang [ProductCode],
		nama_barang [ProductName],
		jumlah_barang [ProductQty],
		tanggal [ProductDate]
	FROM PRODUK
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_get_by_id]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_get_by_id]
@ID INT
AS
BEGIN
	SELECT
		id [ProductId],
		kode_barang [ProductCode],
		nama_barang [ProductName],
		jumlah_barang [ProductQty],
		tanggal [ProductDate]
	FROM PRODUK
	WHERE id = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_get_duplicate_code]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_get_duplicate_code]
@ProductCode VARCHAR(200)
AS
BEGIN
	SELECT
		id [ProductId],
		kode_barang [ProductCode],
		nama_barang [ProductName],
		jumlah_barang [ProductQty],
		tanggal [ProductDate]
	FROM PRODUK
	WHERE kode_barang = @ProductCode
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_get_duplicate_name]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_get_duplicate_name]
@ProductName VARCHAR(50)
AS
BEGIN
	SELECT
		id [ProductId],
		kode_barang [ProductCode],
		nama_barang [ProductName],
		jumlah_barang [ProductQty],
		tanggal [ProductDate]
	FROM PRODUK
	WHERE nama_barang = @ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_insert]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_insert]
@ProductCode VARCHAR(200),
@ProductName VARCHAR(50),
@ProductQty INT,
@ProductDate datetime
AS
BEGIN
	 INSERT INTO produk 
	 ( 
		 [kode_barang]
		,[nama_barang]
		,[jumlah_barang]
		,[tanggal]
	  )
	  VALUES
	  (
		@ProductCode,
		@ProductName,
		@ProductQty,
		@ProductDate
	 )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_produk_update]    Script Date: 6/29/2023 10:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_produk_update]
@ProductId INT,
@ProductCode VARCHAR(200),
@ProductName VARCHAR(50),
@ProductQty INT,
@ProductDate datetime
AS
BEGIN
	 UPDATE produk 
	 SET [kode_barang] = @ProductCode
		,[nama_barang] = @ProductName
		,[jumlah_barang] = @ProductQty
		,[tanggal] = @ProductDate
	WHERE id = @ProductId
	 
END
GO
