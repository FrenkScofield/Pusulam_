

GO
PRINT N'Disabling all DDL triggers...'
GO
DISABLE TRIGGER ALL ON DATABASE
GO
PRINT N'Altering [dbo].[sp_Login]...';


GO

ALTER procedure [dbo].[sp_Login]
	@TCKIMLIKNO	VARCHAR(11) = NULL,
	@OTURUM		VARCHAR(50) = NULL,

	@ISLEM		INT			= NULL,
	@SIFRE		VARCHAR(50)	= NULL,
	@ID_UYGULAMA	INT			= NULL,
	@DEVICE_ID VARCHAR(100 )= NULL
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
GO
PRINT N'Altering [dbo].[sp_OturumKontrol]...';


GO
ALTER procedure [dbo].[sp_OturumKontrol]
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	@return_variable	INT				= NULL		
AS
BEGIN
		
		BEGIN TRY    
		
			DECLARE @ErrorSeverity INT;
			DECLARE @ErrorState INT;
			DECLARE @MSG VARCHAR(4000)
			
			SELECT 
				@ErrorSeverity	= ERROR_SEVERITY(),
				@ErrorState		= ERROR_STATE()
			
			--IF 1 = (SELECT AKTIF FROM OkyanusDB.dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO)
			--BEGIN
			--RETURN 1
			--END
			--ELSE
			--BEGIN
			--	SELECT @MSG = 'Oturum Sonlandırıldı!'
				
			--	RAISERROR (@MSG , -- Message text.
			--		   16, -- Severity.
			--		   1 -- State.
			--		   );
			--END

			SET @return_variable = (SELECT COUNT(TCKIMLIKNO) FROM OkyanusDB.[dbo].[v3Kullanici] WHERE TCKIMLIKNO = @TCKIMLIKNO AND AKTIF = 1 and LOWER([OTURUM]) = LOWER(@OTURUM))  

			IF @return_variable IS NULL OR @return_variable = 0
			BEGIN
				SET @return_variable = (SELECT COUNT(*) FROM OkyanusIletisim.dbo.LoginLog WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND LOWER([OTURUM]) = LOWER(@OTURUM) AND LOGOUT = 0)				
			END
			IF @return_variable > 0
			AND (EXISTS(SELECT DISTINCT	 M.ID_MENU
						FROM Menu							M
						INNER JOIN MenuYetki				Y	ON Y.ID_MENU			= M.ID_MENU  
						INNER JOIN OkyanusDB.DBO.v4KullaniciSubeTurKademe	KT	ON KT.ID_KULLANICITIPI = Y.ID_KULLANICITIPI
						INNER JOIN OkyanusDB.dbo.v3KademeBilgi				KB	ON	KB.ID_KADEME = Y.ID_KADEME AND KB.ID_KADEME3 = KT.ID_KADEME
						WHERE KT.TCKIMLIKNO = @TCKIMLIKNO	AND	M.GIZLI = 0 --AND M.ID_MENU = @ID_MENU

						UNION ALL
							
						SELECT DISTINCT	 M.ID_MENU
						FROM Menu							M
						INNER  JOIN MenuKullaniciYetki		KY	ON M.ID_MENU		= KY.ID_MENU	
						INNER  JOIN v3SubeYetki				SY	ON SY.TCKIMLIKNO	= @TCKIMLIKNO
						WHERE KY.TCKIMLIKNO = @TCKIMLIKNO	AND M.GIZLI=0 /*AND M.ID_MENU = @ID_MENU*/) OR  dbo.fn_GenelHak(@TCKIMLIKNO) = 1 )
			OR EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TCKIMLIKNO )	
			OR EXISTS(SELECT TOP 1 K.TCKIMLIKNO
							FROM Mobile_Menu											M
							INNER JOIN Mobile_MenuYetki								Y	ON	Y.ID_MENU			= M.ID_MENU
							INNER JOIN OkyanusDB.DBO.v4KullaniciSubeTurKademe	KT	ON	KT.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI
							INNER JOIN OkyanusDB.dbo.v3KademeBilgi				KB	ON	KB.ID_KADEME		= Y.ID_KADEME 
																					AND KB.ID_KADEME3		= KT.ID_KADEME
							INNER JOIN OkyanusDB.dbo.v3Kullanici				K	ON	K.TCKIMLIKNO		= KT.TCKIMLIKNO 
																					AND K.AKTIF				= 1
							WHERE	KT.TCKIMLIKNO	= @TCKIMLIKNO	
								AND	M.GIZLI			= 0
								AND M.ID_MENU		= @ID_MENU
								)

			BEGIN
				RETURN 1
			END
			ELSE
			BEGIN
				SELECT @MSG = 'Oturum Sonlandırıldı!'
			
				RAISERROR (@MSG , -- Message text.
					   16, -- Severity.
					   1 -- State.
					   );
			END
			 
		END TRY
		BEGIN CATCH
			
			SELECT 
				@ErrorSeverity	= ERROR_SEVERITY(),
				@ErrorState		= ERROR_STATE()
			
			SELECT @MSG = 'Oturum Sonlandırıldı!'
			
			RAISERROR (@MSG , -- Message text.
					   @ErrorSeverity, -- Severity.
					   @ErrorState -- State.
					   );
		END CATCH;
		
END
GO
PRINT N'Refreshing [dbo].[sp_KonuAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_KonuAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_EtutOgretmenRapor]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_EtutOgretmenRapor]';


GO
PRINT N'Refreshing [dbo].[sp_Sinav]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_Sinav]';


GO
PRINT N'Refreshing [dbo].[sp_HedefBelirlemeRapor]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_HedefBelirlemeRapor]';


GO
PRINT N'Refreshing [dbo].[sp_GenelSinavRaporu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GenelSinavRaporu]';


GO
PRINT N'Refreshing [dbo].[sp_HataLog]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_HataLog]';


GO
PRINT N'Refreshing [dbo].[sp_Menu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_Menu]';


GO
PRINT N'Refreshing [dbo].[sp_OnlineSinavMailYolla]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OnlineSinavMailYolla]';


GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO
