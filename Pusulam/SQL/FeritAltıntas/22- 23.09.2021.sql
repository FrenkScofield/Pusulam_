USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_Login]    Script Date: 24.09.2021 18:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[sp_Login]
	@TCKIMLIKNO	VARCHAR(11) = NULL,
	@OTURUM		VARCHAR(50) = NULL,

	@ISLEM		INT			= NULL,
	@SIFRE		VARCHAR(50)	= NULL,
	@ID_UYGULAMA	INT			= NULL,
	@DEVICE_ID VARCHAR(100 )= NULL,
	@K_ANAHTAR	VARCHAR(MAX) = NULL,
	@Session    VARCHAR(MAX) = NULL
AS

BEGIN


BEGIN TRY    
	    DECLARE @MSG			VARCHAR(4000)
	    DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT

		IF @ISLEM = 1	--	PUSULAM SİTE GİRİŞ
		BEGIN
			
			SET @OTURUM = NEWID()
			
			IF @ID_UYGULAMA = 5
			BEGIN
				IF EXISTS (SELECT TOP 1 1 FROM OkyanusIletisim.dbo.LoginLog WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND ID_UYGULAMA = @ID_UYGULAMA AND DEVICE_ID = @DEVICE_ID AND LOGOUT = 0)
				BEGIN
					UPDATE OkyanusIletisim.dbo.LoginLog SET LOGOUT = 1 WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND ID_UYGULAMA = @ID_UYGULAMA AND DEVICE_ID = @DEVICE_ID AND LOGOUT = 0
				END
			END
			ELSE
			BEGIN
				SET @ID_UYGULAMA = 4
				SET @DEVICE_ID = 'Pusulam'
			END

			DECLARE @DIS_TCKIMLIKNO VARCHAR(11) = CAST(CAST((@TCKIMLIKNO) AS BIGINT)+1 AS VARCHAR)
			
			IF EXISTS (SELECT TOP 1 1 
							FROM v3Kullanici K
							WHERE K.TCKIMLIKNO = @TCKIMLIKNO 
								AND (	EXISTS (SELECT TOP 1 1 FROM v3Ogrenci		O WHERE O.TCKIMLIKNO = K.TCKIMLIKNO)
									OR	EXISTS (SELECT TOP 1 1 FROM v3OgrenciVeli	O WHERE O.TCKIMLIKNO = K.TCKIMLIKNO)
									)
								AND (	eokul_v2.dbo.clr_Crypto_CheckPass( @SIFRE, isnull( SIFREHASH, -1))	= 1
									OR	@SIFRE = SIFRE
									) 
								AND K.AKTIF = 1  
							)
			BEGIN
			
				INSERT INTO OkyanusIletisim.dbo.LoginLog(KYE_TCKIMLIKNO, OTURUM, ID_UYGULAMA, DEVICE_ID, FCM_TOKEN)VALUES(@TCKIMLIKNO, @OTURUM, @ID_UYGULAMA, @DEVICE_ID, '')

				UPDATE OkyanusDb.dbo.v3Kullanici SET 
					OTURUM			= @OTURUM,
					SONGIRISTARIH	= GETDATE() 
				WHERE TCKIMLIKNO = @TCKIMLIKNO

				SELECT
					K.AD, 
					K.SOYAD, 
					K.TCKIMLIKNO,
					K.CINSIYET,
					K.DOGUMTARIHI,
					K.OTURUM,
					0 KADEME,
					1 AS OV,
					1 AS OVT,
					0 AS DISOGRENCI
				FROM v3Kullanici	K
				WHERE K.TCKIMLIKNO = @TCKIMLIKNO AND K.AKTIF = 1

			END
			ELSE IF EXISTS (	SELECT TOP 1 1
						FROM DisOgrenci	D
						WHERE	D.TCKIMLIKNO= @DIS_TCKIMLIKNO
							AND D.SIFRE		= @SIFRE
					)
			BEGIN

			
				INSERT INTO OkyanusIletisim.dbo.LoginLog(KYE_TCKIMLIKNO, OTURUM, ID_UYGULAMA, DEVICE_ID, FCM_TOKEN)VALUES(@DIS_TCKIMLIKNO, @OTURUM, 4, 'Pusulam', '')

				SELECT
					AD,
					SOYAD,
					TCKIMLIKNO,
					0 AS CINSIYET,
					GETDATE() AS DOGUMTARIHI,
					@OTURUM AS OTURUM,
					0 KADEME,
					1 AS OV,
					0 AS OVT,
					1 AS DISOGRENCI
				FROM DisOgrenci
				WHERE TCKIMLIKNO = @DIS_TCKIMLIKNO
			END
		END
		ELSE IF @ISLEM = 2
		BEGIN
				DROP TABLE IF EXISTS #TEMPID
				DECLARE @TEMPID TABLE
				(
					ID		INT,
					AD		VARCHAR(40),
					SOYAD   VARCHAR(40),
					SubeID	INT		   ,
					Session	VARCHAR(100),
					TC		VARCHAR(11),
					TipID	INT			,
					GUID	VARCHAR(100)
				)
				Select * INTO #TEMPID FROM @TEMPID
				DECLARE @ID INT = NULL
				SELECT @ID = ID_KULLANICI FROM OkyanusDB.dbo.v3Kullanici WHERE K_ANAHTAR = @K_ANAHTAR and AKTIF = 1					 

				IF(@ID is not null)
				BEGIN
					set @Session = NEWID()
					IF NOT EXISTS (Select SONGIRISTARIH from OkyanusDb.dbo.v3Kullanici  where  ID_KULLANICI = @ID)
					BEGIN
					   UPDATE OkyanusDb.dbo.v3Kullanici  
						  set OTURUM = 'false',
							  SONGIRISTARIH=GETDATE() 
						where ID_KULLANICI = @ID  
					END
					ELSE
					BEGIN
					  Declare @SonGiris datetime=(Select GETDATE())
						  
					   UPDATE OkyanusDb.dbo.v3Kullanici 
						  set OTURUM = @Session,
							  SONGIRISTARIH=@SonGiris
						where ID_KULLANICI = @ID  
					END

					SET @TCKIMLIKNO = (Select TCKIMLIKNO from OkyanusDB.dbo.v3Kullanici where ID_KULLANICI=@ID)

					INSERT INTO #TEMPID
					Select ID_KULLANICI,
						   AD,
						   SOYAD,
						   (Select top 1 ID_SUBE from OkyanusDB.dbo.v3SubeYetki where TCKIMLIKNO=@TCKIMLIKNO),
						   @Session,
						   isnull(TCKIMLIKNO,''),
						   (Select top 1 ID_KULLANICITIPI from OkyanusDB.dbo.v3SubeYetki where TCKIMLIKNO=@TCKIMLIKNO order by ID_KULLANICITIPI),
						   '' as GUID
					  from OkyanusDb.dbo.v3Kullanici
					 where ID_KULLANICI=@ID

				END
						SELECT
							K.AD, 
							K.SOYAD, 
							K.TCKIMLIKNO,
							K.CINSIYET,
							K.DOGUMTARIHI,
							K.OTURUM,
							0 KADEME,
							1 AS OV,
							1 AS OVT,
							0 AS DISOGRENCI
						FROM v3Kullanici	K
						WHERE K.TCKIMLIKNO = @TCKIMLIKNO AND K.AKTIF = 1


		END
		ELSE	--	INTERAKTİF TEN GEÇİŞ 
		BEGIN

			IF	(NOT EXISTS(SELECT TCKIMLIKNO FROM [dbo].[v3Kullanici] WHERE TCKIMLIKNO = @TCKIMLIKNO AND OTURUM = @OTURUM AND AKTIF = 1) 
				AND 
				NOT EXISTS (SELECT TOP 1 1 FROM OkyanusIletisim.dbo.LoginLog WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND OTURUM = @OTURUM AND LOGOUT = 0))
				AND 
				NOT EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TCKIMLIKNO)
			
			BEGIN
			
				SELECT 
				@ErrorSeverity	= ERROR_SEVERITY(),
				@ErrorState		= ERROR_STATE()

				SELECT @MSG = 'Hatalı Kullanıcı Adı ya da Şifre'

				RAISERROR (@MSG,		-- Message text.
						   16,			-- Severity.
						   @ErrorState	-- State.
						   );
			
			END
			ELSE
			BEGIN
				DECLARE @OV BIT=0
				IF EXISTS(SELECT TOP 1 1 FROM v3SubeYetki WHERE TCKIMLIKNO=@TCKIMLIKNO AND ID_KULLANICITIPI IN (4))
				BEGIN
					SET @OV=1
				END

				--Login olan kullanıcı sadece veli yada sadece öğrenci olma durum kontrolü
				DECLARE @OVT BIT=0

				DROP TABLE IF EXISTS #TEMPID_KULLANICITIPI
				SELECT DISTINCT ID_KULLANICITIPI INTO #TEMPID_KULLANICITIPI FROM v3SubeYetki WHERE TCKIMLIKNO=@TCKIMLIKNO


				IF NOT EXISTS (SELECT TOP 1 1 FROM #TEMPID_KULLANICITIPI WHERE ID_KULLANICITIPI NOT IN (4,5))
				BEGIN
					SET @OVT=1
				END
				DROP TABLE IF EXISTS #TEMPID_KULLANICITIPI
			
				INSERT INTO OkyanusIletisim.dbo.LoginLog(KYE_TCKIMLIKNO, OTURUM, ID_UYGULAMA, DEVICE_ID, FCM_TOKEN)VALUES(@TCKIMLIKNO, @OTURUM, 4, 'Pusulam', '')

				IF EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TCKIMLIKNO)
				BEGIN
					SELECT
						AD,
						SOYAD,
						TCKIMLIKNO,
						0 AS CINSIYET,
						GETDATE() AS DOGUMTARIHI,
						@OTURUM AS OTURUM,
						0 KADEME,
						1 AS OV,
						0 AS OVT,
						1 AS DISOGRENCI
					FROM DisOgrenci
					WHERE TCKIMLIKNO=@TCKIMLIKNO
				END
				ELSE
				BEGIN
					-- Login Bilgileri
					SELECT
						K.AD, 
						K.SOYAD, 
						K.TCKIMLIKNO,
						K.CINSIYET,
						K.DOGUMTARIHI,
						K.OTURUM,
						--kk.ID_KADEME as KADEME,
						0 KADEME,
						@OV AS OV,
						@OVT AS OVT,
						0 AS DISOGRENCI
					FROM		[dbo].v3Kullanici				K
					--INNER JOIN	OkyanusDB.dbo.v4KullaniciKademe	kk	on	kk.TCKIMLIKNO = k.TCKIMLIKNO	
					WHERE K.TCKIMLIKNO = @TCKIMLIKNO AND K.AKTIF = 1
				END
					
			END

		END

		
END TRY
		BEGIN CATCH
			
			SELECT 
				@ErrorSeverity	= ERROR_SEVERITY(),
				@ErrorState		= ERROR_STATE()
							
			SELECT @MSG = /*'Prosedür: ' + ISNULL(ERROR_PROCEDURE(), '') + ', Satır: ' + CAST(ERROR_LINE() as varchar) + ', Hata: ' +*/ ERROR_MESSAGE()
			
			RAISERROR (@MSG , -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );
		END CATCH;
	
END