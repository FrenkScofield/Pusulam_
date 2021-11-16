USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_SportifKulup]    Script Date: 13.09.2021 16:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[sp_SportifKulup]
	@ISLEM							INT				= NULL,
	@TCKIMLIKNO						VARCHAR(11)		= NULL,
	@OTURUM							VARCHAR(36)		= NULL,
	@ID_MENU						INT				= NULL,
	@SQLJSON						VARCHAR(MAX)	= NULL,
	
	@ID_KADEME3						INT				= NULL,
	@ID_DERS						INT				= NULL,
	@BAS_TARIH						VARCHAR(MAX)    = NULL,
	@BIT_TARIH						VARCHAR(MAX)	= NULL,
	@ID_SUBE						INT				= NULL,
	@ID_KULUP						INT				= NULL,
	@KOTA							INT				= NULL,
	@ID_KULUPKOTA					INT				= NULL,
	@KOTALI							BIT				= NULL,
	@ID_KULUPPERIYOT				INT				= NULL,
	@ID_SINIFs						NVARCHAR(MAX)   = NULL,
	@OGRENCI_TC						NVARCHAR(MAX)   = NULL,
	@KulupPeriyotId				    INT				= NULL

	
AS
BEGIN
	BEGIN TRY    
		DECLARE @MSG			VARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT
		DECLARE @_ID_LOG INT = 0
		DECLARE @return_variable INT
		DECLARE @KulupAdi VARCHAR(100)
		EXEC @return_variable = [dbo].[sp_OturumKontrol] @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU

		IF @return_variable = 1
		BEGIN
			IF @ISLEM = 1  --Periyot Listele
			BEGIN
			    SELECT ISNULL((
					SELECT
						ID_KULUPPERIYOT,
						BAS_TARIH = FORMAT (BAS_TARIH, 'dd/MM/yyyy'),
						BIT_TARIH = FORMAT (BIT_TARIH, 'dd/MM/yyyy')
					 FROM KulupPeriyot
					FOR JSON PATH
				),'[]')
			END	

			IF @ISLEM = 2  -- Periyot Ekle
			BEGIN
				INSERT INTO KulupPeriyot (BAS_TARIH, BIT_TARIH) VALUES (@BAS_TARIH, @BIT_TARIH)

				 SELECT ISNULL((
					SELECT
						ID_KULUPPERIYOT,				   
					    BAS_TARIH = FORMAT (BAS_TARIH, 'dd/MM/yyyy'),
						BIT_TARIH = FORMAT (BIT_TARIH, 'dd/MM/yyyy')
					 FROM KulupPeriyot
					FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 3  --Kota Listele
			BEGIN
			    SELECT ISNULL((
					SELECT 
						KK.ID_KULUPKOTA, 
						S.ID_SUBE,
						SUBEAD = S.AD,
						KK.ID_KULUP,
						K.KULUPADI,
						GRUPAD = KD.AD,
						KK.ID_KADEME3,
						KK.KOTA
					FROM KulupKota KK
					JOIN Kulup K ON KK.ID_KULUP = K.ID_KULUP
					JOIN OkyanusDB.dbo.v3Kademe3 KD ON KK.ID_KADEME3 = KD.ID_KADEME3
					JOIN OkyanusDB.DBO.v3Sube S ON KK.ID_SUBE = S.ID_SUBE
				FOR JSON PATH
				),'[]')
			END	

			IF @ISLEM = 4  -- Kota Ekle
			BEGIN
				INSERT INTO KulupKota (ID_KADEME3, ID_KULUP, KOTA,ID_SUBE) VALUES (@ID_KADEME3, @ID_KULUP, @KOTA, @ID_SUBE)

			    SELECT ISNULL((
					SELECT 
						KK.ID_KULUPKOTA, 
						S.ID_SUBE,
						SUBEAD = S.AD,
						KK.ID_KULUP,
						K.KULUPADI,
						GRUPAD = KD.AD,
						KK.ID_KADEME3,
						KK.KOTA
					FROM KulupKota KK
					JOIN Kulup K ON KK.ID_KULUP = K.ID_KULUP
					JOIN OkyanusDB.dbo.v3Kademe3 KD ON KK.ID_KADEME3 = KD.ID_KADEME3
					JOIN OkyanusDB.DBO.v3Sube S ON KK.ID_SUBE = S.ID_SUBE
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 5  --Kulüp Listele
			BEGIN
				IF @KOTALI=1
				BEGIN
					
					SELECT DISTINCT						
						K.ID_KULUP
					   ,K.KULUPADI 
					FROM Kulup								K  WITH (NOLOCK)
					JOIN KulupKota							KK WITH (NOLOCK) ON K.ID_KULUP    = KK.ID_KULUP
					JOIN OkyanusDB.dbo.vw_SubeGrupSinif		SG WITH (NOLOCK) ON KK.ID_KADEME3 = SG.ID_KADEME3
					JOIN v3Ogrenci							O  WITH (NOLOCK) ON O.ID_SINIF    = SG.ID_SINIF
																			AND O.ID_SUBE	  = KK.ID_SUBE
					WHERE O.TCKIMLIKNO = @OGRENCI_TC AND
						 KK.Kota > (SELECT COUNT(1) 
										FROM KulupOgrenci WITH (NOLOCK)
										WHERE  ID_KULUPPERIYOT = (SELECT TOP 1 ID_KULUPPERIYOT  FROM KulupPeriyot KP WITH (NOLOCK) WHERE  KP.BAS_TARIH <= CAST(GETDATE() AS DATE) AND KP.BIT_TARIH >= CAST(GETDATE() AS DATE) ) 
										AND ID_KULUP = KK.ID_KULUP
										)
				END
				BEGIN
					SELECT DISTINCT
						ID_KULUP
					   ,KULUPADI
					 FROM Kulup ORDER BY KULUPADI 	
				END
			END	

			IF @ISLEM = 6  -- Kota Sil
			BEGIN
				DELETE FROM KulupKota WHERE ID_KULUPKOTA = @ID_KULUPKOTA
				    SELECT ISNULL((
					SELECT 
						KK.ID_KULUPKOTA, 
						S.ID_SUBE,
						SUBEAD = S.AD,
						KK.ID_KULUP,
						K.KULUPADI,
						GRUPAD = KD.AD,
						KK.ID_KADEME3,
						KK.KOTA
					FROM KulupKota KK
					JOIN Kulup K ON KK.ID_KULUP = K.ID_KULUP
					JOIN OkyanusDB.dbo.v3Kademe3 KD ON KK.ID_KADEME3 = KD.ID_KADEME3
					JOIN OkyanusDB.DBO.v3Sube S ON KK.ID_SUBE = S.ID_SUBE
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 7  -- Kota Düzenle
			BEGIN				
				--UPDATE KulupKota SET ID_SUBE = @ID_SUBE, ID_KULUP = @ID_KULUP, KOTA = @KOTA WHERE ID_KULUPKOTA = @ID_KULUPKOTA
				UPDATE KulupKota SET KOTA = @KOTA WHERE ID_KULUPKOTA = @ID_KULUPKOTA

				    SELECT ISNULL((
					SELECT 
						KK.ID_KULUPKOTA, 
						S.ID_SUBE,
						SUBEAD = S.AD,
						KK.ID_KULUP,
						K.KULUPADI,
						GRUPAD = KD.AD,
						KK.ID_KADEME3,
						KK.KOTA
					FROM KulupKota KK
					JOIN Kulup K ON KK.ID_KULUP = K.ID_KULUP
					JOIN OkyanusDB.dbo.v3Kademe3 KD ON KK.ID_KADEME3 = KD.ID_KADEME3
					JOIN OkyanusDB.DBO.v3Sube S ON KK.ID_SUBE = S.ID_SUBE
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 8  --Kulup Belirle/Listele
			BEGIN	
				DROP TABLE IF EXISTS #KulupOgrenci
				CREATE TABLE #KulupOgrenci(ID INT)
				INSERT INTO #KulupOgrenci(ID)VALUES(1) 
			 -- Bu sorgu tek sorguda halledilebilir mi?
				SET @KulupPeriyotId = (
					SELECT TOP 1 KP.ID_KULUPPERIYOT
					FROM KulupPeriyot KP 
					WHERE KP.BAS_TARIH <= CAST(GETDATE() AS DATE) AND KP.BIT_TARIH >= CAST(GETDATE() AS DATE)
					);				

				IF @KulupPeriyotId IS NULL
				BEGIN
					SET @KulupPeriyotId = (
						SELECT TOP 1 KP.ID_KULUPPERIYOT
						FROM KulupPeriyot KP 
						ORDER BY 1 DESC
						);	

					SET @KulupAdi = (
					SELECT K.KULUPADI
					FROM KulupOgrenci KO 	
					JOIN Kulup K ON K.ID_KULUP = KO.ID_KULUP	
					WHERE KO.ID_KULUPPERIYOT = @KulupPeriyotId
						AND KO.TCKIMLIKNO = @TCKIMLIKNO
					);

					SELECT ISNULL((
						SELECT TOP 1
							KULUPPERIYOT = 0, 
							KULUPADI = ISNULL(@KulupAdi,'YOK')
						FROM #KulupOgrenci 						
					FOR JSON PATH
					),'[]')	
				END 
				ELSE
				BEGIN
					SELECT ISNULL((
						SELECT 
							KULUPPERIYOT = ISNULL(@KulupPeriyotId, 0), 
							K.KULUPADI, 
							KO.ID_KULUP
						FROM KulupOgrenci KO
						JOIN Kulup K ON K.ID_KULUP = KO.ID_KULUP
						WHERE KO.TCKIMLIKNO = @TCKIMLIKNO AND KO.ID_KULUPPERIYOT = @KulupPeriyotId
					FOR JSON PATH
					),'[]')				
				END

				
			END	

			IF @ISLEM = 9  --Periyotlar
			BEGIN			    
				SELECT DISTINCT
					ID_KULUPPERIYOT
					,PERIYOT = FORMAT (BAS_TARIH, 'dd/MM/yyyy') + ' - ' + FORMAT (BIT_TARIH, 'dd/MM/yyyy') 
					FROM KulupPeriyot					
			END	

			IF @ISLEM = 10  -- Kulüp Düzenle
			BEGIN				
				IF @KulupPeriyotId IS NULL
				BEGIN
					SET @KulupPeriyotId = (
					SELECT TOP 1 KP.ID_KULUPPERIYOT
					FROM KulupPeriyot KP 
					WHERE KP.BAS_TARIH <= CAST(GETDATE() AS DATE) AND KP.BIT_TARIH >= CAST(GETDATE() AS DATE)
					);				
				END
					
				UPDATE KulupOgrenci SET ID_KULUP = @ID_KULUP, KYE_TCKIMLIKNO = @TCKIMLIKNO WHERE TCKIMLIKNO = @OGRENCI_TC AND ID_KULUPPERIYOT = @KulupPeriyotId

				SELECT ISNULL((
					SELECT TOP 1
						KO.ID_KULUPPERIYOT,
						K.KULUPADI, 
						KO.ID_KULUP
					FROM KulupOgrenci KO
					JOIN Kulup K ON K.ID_KULUP = KO.ID_KULUP
					WHERE TCKIMLIKNO = @OGRENCI_TC AND ID_KULUPPERIYOT = @KulupPeriyotId
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 11  -- Kulüpten Ayrıl
			BEGIN
					
				DELETE FROM KulupOgrenci WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULUPPERIYOT = @ID_KULUPPERIYOT

				 SELECT ISNULL((					
					SELECT TOP 1
						KO.ID_KULUPPERIYOT,
						K.KULUPADI, 
						KO.ID_KULUP
					FROM KulupOgrenci KO
					JOIN Kulup K ON K.ID_KULUP = KO.ID_KULUP
					WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULUPPERIYOT = @ID_KULUPPERIYOT
				FOR JSON AUTO
				),'[]')
			END

			IF @ISLEM = 12  --Öğrenci Kulüpler Raporu
			BEGIN
			    SELECT ISNULL((
					SELECT 
						TC = O.TCKIMLIKNO, 
						ADSOYAD = KUL.AD + ' ' + KUL.SOYAD, 
						PERIYOT = FORMAT (KP.BAS_TARIH, 'dd/MM/yyyy') + ' - ' + FORMAT (KP.BIT_TARIH, 'dd/MM/yyyy'),
						K.KULUPADI ,
						KAYDEDEN = (SELECT KK.AD + ' '  + KK.SOYAD FROM v3Kullanici KK WHERE KK.TCKIMLIKNO = KO.KYE_TCKIMLIKNO)
					FROM OkyanusDB.dbo.v3Ogrenci O WITH (NOLOCK)
						JOIN v3Kullanici KUL WITH (NOLOCK) ON KUL.TCKIMLIKNO = O.TCKIMLIKNO
						LEFT JOIN KulupOgrenci KO WITH (NOLOCK) ON O.TCKIMLIKNO = KO.TCKIMLIKNO
						LEFT JOIN KulupPeriyot KP ON KP.ID_KULUPPERIYOT = KO.ID_KULUPPERIYOT	
						LEFT JOIN Kulup K WITH (NOLOCK) ON K.ID_KULUP = KO.ID_KULUP	
					WHERE O.ID_SINIF IN (SELECT VALUE FROM OPENJSON(@ID_SINIFs)) 
						AND (KP.ID_KULUPPERIYOT IS NULL OR KP.ID_KULUPPERIYOT = @ID_KULUPPERIYOT)
				FOR JSON PATH
				),'[]')
			END	

			IF @ISLEM = 13  --Öğrenci Kulüpler Raporu Yazdır
			BEGIN			    				
				SELECT 
					[T.C KİMLİK NO] = O.TCKIMLIKNO, 
					[AD SOYAD] = KUL.AD + ' ' + KUL.SOYAD, 
					[PERİYOT] = FORMAT (KP.BAS_TARIH, 'dd/MM/yyyy') + ' - ' + FORMAT (KP.BIT_TARIH, 'dd/MM/yyyy'),
					[KULÜP ADI] = K.KULUPADI
				FROM OkyanusDB.dbo.v3Ogrenci O WITH (NOLOCK)
					JOIN v3Kullanici KUL WITH (NOLOCK) ON KUL.TCKIMLIKNO = O.TCKIMLIKNO
					LEFT JOIN KulupOgrenci KO WITH (NOLOCK) ON O.TCKIMLIKNO = KO.TCKIMLIKNO
					LEFT JOIN KulupPeriyot KP ON KP.ID_KULUPPERIYOT = KO.ID_KULUPPERIYOT	
					LEFT JOIN Kulup K WITH (NOLOCK) ON K.ID_KULUP = KO.ID_KULUP	
				WHERE O.ID_SINIF IN (SELECT VALUE FROM OPENJSON(@ID_SINIFs)) 
					AND (KP.ID_KULUPPERIYOT IS NULL OR KP.ID_KULUPPERIYOT = @ID_KULUPPERIYOT)			
			END	

			IF @ISLEM = 14  -- Periyot Düzenle
			BEGIN				
				UPDATE KulupPeriyot SET BAS_TARIH = @BAS_TARIH, BIT_TARIH = @BIT_TARIH WHERE ID_KULUPPERIYOT = @ID_KULUPPERIYOT

				 SELECT ISNULL((
					SELECT TOP 1 
						ID_KULUPPERIYOT,
						BAS_TARIH = FORMAT (BAS_TARIH, 'dd/MM/yyyy'),
						BIT_TARIH = FORMAT (BIT_TARIH, 'dd/MM/yyyy')
					 FROM KulupPeriyot
					FOR JSON AUTO
				),'[]')
			END

			IF @ISLEM = 15  -- Kulüp Ekle
			BEGIN
				IF @KulupPeriyotId IS NULL 
				BEGIN
					SET @KulupPeriyotId = (
					SELECT TOP 1 KP.ID_KULUPPERIYOT
					FROM KulupPeriyot KP 
					WHERE KP.BAS_TARIH <= CAST(GETDATE() AS DATE) AND KP.BIT_TARIH >= CAST(GETDATE() AS DATE)
					);	
				END
				

				INSERT INTO KulupOgrenci (TCKIMLIKNO, ID_KULUP, ID_KULUPPERIYOT,KYE_TCKIMLIKNO) VALUES (@OGRENCI_TC, @ID_KULUP, @KulupPeriyotId,@TCKIMLIKNO)
				
				SELECT ISNULL((
					SELECT TOP 1
						KO.ID_KULUPPERIYOT,
						K.KULUPADI, 
						KO.ID_KULUP
					FROM KulupOgrenci KO
					JOIN Kulup K ON K.ID_KULUP = KO.ID_KULUP
					WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULUPPERIYOT = @KulupPeriyotId--@ID_KULUPPERIYOT									
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 16  -- Kontrol Kota Ekle
			BEGIN				
				 SELECT ISNULL((
					SELECT * FROM KulupKota WHERE ID_KADEME3 = @ID_KADEME3 AND ID_SUBE = @ID_SUBE AND ID_KULUP = @ID_KULUP
					FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 17  -- Yetkisiz Grup kademe listeleme 
			BEGIN
				SELECT	K.ID_KADEME3 ID_SINAVGRUP,K.AD GRUP
				FROM	v3Kademe3			K				
			END
			IF @ISLEM = 18 -- YETENEK KONTROL
			BEGIN
				CREATE TABLE #RESULT(SONUC BIT)
				DECLARE  @SUB_DONEM VARCHAR(MAX) = (SELECT TOP 1 DONEM FROM v3AktifDonem WHERE AKTIF = 1)

				IF EXISTS(SELECT KK.ID_KADEME3 FROM OkyanusDB..v3KullaniciKademe3 KK WHERE KK.TCKIMLIKNO = @OGRENCI_TC AND KK.ID_KADEME3 IN (SELECT K.ID_KADEME3 FROM OkyanusDB..v3Kademe3 K WHERE K.DUZEY IN ('8', '11')))
				BEGIN
					IF EXISTS(SELECT * FROM YG_KategoriOgrenci KO WHERE KO.DONEM = @SUB_DONEM AND KO.TCKIMLIKNO = @OGRENCI_TC)
					BEGIN
						INSERT #RESULT(SONUC) VALUES (0)
					END
					ELSE
					BEGIN
						INSERT #RESULT(SONUC) VALUES (1)
					END
				END
				ELSE
				BEGIN
					INSERT #RESULT(SONUC) VALUES (1)
				END

				SELECT TOP 1 SONUC FROM #RESULT FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
				DROP TABLE #RESULT
			END
		END
	END TRY
	BEGIN CATCH
		DECLARE @_ErrorSeverity INT;
		DECLARE @_ErrorState INT;

		SELECT 
			@_ErrorSeverity	= ERROR_SEVERITY(),
			@_ErrorState		= ERROR_STATE()
				
		DECLARE @_MSG VARCHAR(4000)
		SELECT @_MSG = ERROR_MESSAGE()

		EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @_MSG, @SEVERITY = @_ErrorSeverity, @STATE = @_ErrorState, @ID_LOG = @_ID_LOG, @ID_LOGTUR = 2
	END CATCH;
END
