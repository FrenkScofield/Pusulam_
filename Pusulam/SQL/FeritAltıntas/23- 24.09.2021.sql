INSERT INTO MENU (AD,ACIKLAMA,KOD,URL,RESIM,GIZLI,OZEL)
			VALUES('İdari İşler','İdari İşler','022','','icon-user',0,0)

INSERT INTO MENU (AD,ACIKLAMA,KOD,URL,RESIM,GIZLI,OZEL)
			VALUES('Kullanıcı Listesi','Kullanıcı Yetki İşlemleri','022001','#IdariIsler/KullaniciYetkiIslemleri','icon-user',0,0)

GO

USE [Pusulam]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_Split2]    Script Date: 24.09.2021 12:20:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create Function [dbo].[fn_Split2]
(
 @String NVARCHAR (max),
 @Delimiter NVARCHAR (10),
 @Number INT = null
)
returns @RT_ValueTable table (RN INT, [Value] NVARCHAR(max))

BEGIN

	 DECLARE @ValueTable TABLE (RN INT, [Value] NVARCHAR(max))

	 DECLARE @NextString NVARCHAR(max)
	 DECLARE @Pos INT
	 DECLARE @NextPos INT
	 DECLARE @CommaCheck NVARCHAR(1)
	 DECLARE @index	INT
	 
	 --Initialize
	 SET @NextString = ''
	 SET @CommaCheck = right(@String,1) 
	 SET @index = 1
	 
	 --Check for trailing Comma, if not exists, INSERT
	 --if (@CommaCheck <> @Delimiter )
	 SET @String = @String + @Delimiter
	 
	 --Get position of first Comma
	 SET @Pos = charindex(@Delimiter,@String)
	 SET @NextPos = 1
	 
	 --Loop while there is still a comma in the String of levels
	 WHILE (@pos <>  0)  
	 BEGIN
	  SET @NextString = substring(@String,1,(@Pos - 1))
	 
	  IF (@NextString <> '')
	  BEGIN
		INSERT INTO @ValueTable (RN, [Value]) VALUES (@index, @NextString)
		SET @index = @index + 1
	  END
	 
	  SET @String = substring(@String,@pos + len(@Delimiter),len(@String))
	  
	  SET @NextPos = @Pos
	  SET @pos  = charindex(@Delimiter,@String)
	  
	END
 
	IF @Number IS NULL
		INSERT INTO @RT_ValueTable SELECT RN, [Value] FROM @ValueTable
	ELSE
		INSERT INTO @RT_ValueTable SELECT RN, [Value] FROM @ValueTable WHERE RN = @Number
			
 RETURN

END
GO


USE [Pusulam]
GO

/****** Object:  UserDefinedFunction [dbo].[Fn_SubeYetkiGetir]    Script Date: 24.09.2021 12:22:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE FUNCTION [dbo].[Fn_SubeYetkiGetir](@TC varchar(11))
	RETURNS VARCHAR(MAX)
	AS	BEGIN
	Declare @BILGILER varchar(max)='' 
	
	Select @BILGILER = @BILGILER + (sb.AD) +' '+kt.[AD]+' <br>' from OkyanusDB.dbo.v3Subeyetki s 
	inner join OkyanusDB..V3Sube sb on s.ID_SUBE = sb.ID_SUBE
	inner join OkyanusDB.dbo.v3KullaniciTipi kt on kt.ID_KULLANICITIPI=s.ID_KULLANICITIPI 
	where s.TCKIMLIKNO = @TC
	
	Return IsNull(nullif(@BILGILER,''),'')
	END

	-- Select [dbo].[Fn_SubeYetkiGetir]('53110117830')
GO


USE [Pusulam]
GO

/****** Object:  UserDefinedFunction [dbo].[Fn_KullaniciTipiGetir]    Script Date: 27.09.2021 10:42:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[Fn_KullaniciTipiGetir](@TC varchar(11))
RETURNS VARCHAR(MAX)
AS	BEGIN
Declare @BILGILER varchar(max)='' 
	
SELECT @BILGILER = @BILGILER + kt.[AD]+', ' 
FROM OkyanusDB.dbo.v3Subeyetki s 
INNER JOIN OkyanusDB..v3Sube sb on s.ID_SUBE = sb.ID_SUBE
INNER JOIN OkyanusDB.dbo.v3KullaniciTipi kt on kt.ID_KULLANICITIPI=s.ID_KULLANICITIPI 
WHERE s.TCKIMLIKNO = @TC
GROUP BY kt.[AD]
	
Return IsNull(nullif(@BILGILER,''),'')
END
GO


USE [Pusulam]
GO
/****** Object:  UserDefinedFunction [dbo].[Fn_KullaniciKademeGetir]    Script Date: 27.09.2021 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Fn_KullaniciKademeGetir](@TC varchar(11))
RETURNS VARCHAR(MAX)
AS	BEGIN
Declare @BILGILER varchar(max)='' 
DECLARE @ID VARCHAR(MAX)=''
SELECT @BILGILER = @BILGILER + K.AD + CASE WHEN KK.XBASE = 1 THEN ' (XBASE)' ELSE '' END+', ', @ID = K.ID_KADEME 
FROM OkyanusDB.dbo.v4KullaniciKademe KK 
INNER JOIN OkyanusDB.dbo.v3Kademe K on K.ID_KADEME = KK.ID_KADEME
WHERE KK.TCKIMLIKNO = @TC
GROUP BY K.AD, K.ID_KADEME, KK.XBASE
ORDER BY K.ID_KADEME
	
Return IsNull(nullif(@BILGILER,''),'')
END

GO			


USE [Pusulam]
GO

/****** Object:  Table [dbo].[BransOgretmen]    Script Date: 27.09.2021 10:48:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BransOgretmen](
	[ID_BRANSOGRETMEN] [int] IDENTITY(1,1) NOT NULL,
	[ID_OGRETMEN] [int] NULL,
	[BRANS] [varchar](50) NULL,
 CONSTRAINT [PK_BransOgretmen] PRIMARY KEY CLUSTERED 
(
	[ID_BRANSOGRETMEN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO BransOgretmen (ID_OGRETMEN,BRANS)
SELECT ID_OGRETMEN,BRANS FROM eokul_v2.dbo.BransOgretmen

			GO