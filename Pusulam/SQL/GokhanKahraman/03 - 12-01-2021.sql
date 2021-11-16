USE [Pusulam]
GO

/****** Object:  StoredProcedure [dbo].[sp_SifremiDegistir]    Script Date: 12.01.2021 16:28:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_SifremiDegistir] 
@ISLEM			INT,
@OLDPASS		VARCHAR(50),
@NEWPASS		VARCHAR(50),
@TCKIMLIKNO		VARCHAR(11)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		IF @ISLEM=1
			BEGIN
			DECLARE @ERRCODE VARCHAR(2),@ERRMESSAGE VARCHAR(300)
			DROP TABLE IF EXISTS #UPDATE_TABLE
				CREATE TABLE #UPDATE_TABLE
				(
					ERRCODE		INT,
					ERRMESSAGE  varchar(200), 
				)

				DECLARE @TABLE_OLDPASS VARCHAR(50)=(SELECT SIFRE FROM OkyanusDB.dbo.v3Kullanici WHERE TCKIMLIKNO=@TCKIMLIKNO)

				IF @TABLE_OLDPASS=@OLDPASS
					BEGIN
						UPDATE OkyanusDB.dbo.v3Kullanici 
					    SET SIFRE = @NEWPASS,
						   SIFREHASH=eokul_v2.dbo.clr_Crypto_HashPass(@NEWPASS) 
						WHERE AKTIF=1 
					    AND TCKIMLIKNO=@TCKIMLIKNO

						SET @ERRCODE=0
						SET @ERRMESSAGE='Þifreniz baþarýyla güncellendi!'
						INSERT INTO #UPDATE_TABLE (ERRCODE,ERRMESSAGE) VALUES(@ERRCODE,@ERRMESSAGE)
						SELECT * FROM #UPDATE_TABLE FOR JSON AUTO
					END
				ELSE
					BEGIN
						SET @ERRCODE=1
						SET @ERRMESSAGE='Eski þifrenizi yanlýþ girdiniz!'

						INSERT INTO #UPDATE_TABLE (ERRCODE,ERRMESSAGE) VALUES(@ERRCODE,@ERRMESSAGE)
						SELECT * FROM #UPDATE_TABLE FOR JSON AUTO
					END
			END
    -- Insert statements for procedure here
	SET NOCOUNT OFF;
END
GO


