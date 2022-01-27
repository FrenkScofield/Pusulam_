USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_Menu]    Script Date: 3.01.2022 14:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_Menu]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@TC_YETKI			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENUYARDIM		INT				= 1,
	@ID_MENU			INT				= 1,
	@ID_KADEME			INT				= NULL,
	@ID_KULLANICITIPI	INT				= NULL,
	@ID_UYGULAMA		INT				= NULL,
	@IP					VARCHAR(50)	= NULL

AS
BEGIN
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	
			ISLEM						= @ISLEM			
			,TCKIMLIKNO					= @TCKIMLIKNO		
			,TC_YETKI					= @TC_YETKI		
			,OTURUM						= @OTURUM			
			,ID_MENUYARDIM				= @ID_MENUYARDIM	
			,ID_MENU					= @ID_MENU		
			,ID_KADEME					= @ID_KADEME		
			,ID_KULLANICITIPI			= @ID_KULLANICITIPI
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0
BEGIN TRY   
	DECLARE @TCKIMLIKNOTEMP VARCHAR(11) = @TCKIMLIKNO
	--EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNOTEMP, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
	 DECLARE @return_variable INT
	EXEC @return_variable = [dbo].[sp_OturumKontrol] @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU

	IF @return_variable = 1
	BEGIN
	
		SELECT DISTINCT ID_KADEME INTO #KADEMELER FROM OkyanusDB.dbo.v4KullaniciKademe WHERE TCKIMLIKNO = @TCKIMLIKNOTEMP
		IF @ISLEM = 1	--	Menü Getir
		BEGIN	
			IF dbo.fn_GenelHak(@TCKIMLIKNOTEMP)>0
			BEGIN
					SELECT DISTINCT M.ID_MENU, M.AD, M.KOD, M.URL, M.RESIM
					FROM Menu							M
					where M.GIZLI = 0					
						 AND (		(@TCKIMLIKNOTEMP = '62362359264' AND ID_MENU IN (1099,1193,1200,1335,1344))
								OR	@TCKIMLIKNOTEMP != '62362359264')
				 UNION				
					SELECT	  -1
							, OS.AD
							, '001'
							, OS.YOL + @TCKIMLIKNO+'&OTURUM='+@OTURUM							
							, CASE WHEN OS.ID_OZELSAYFATURU	= 1		THEN 'icon-globe'	ELSE 'icon-doc' END
					FROM OzelSayfa		OS
					JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA = OS.ID_OZELSAYFA				
					WHERE	AKTIF				= 1
					AND OS.ID_OZELSAYFA =85 
					AND Y.ID_KULLANICITIPI	= 1	--	ADMİN				
				UNION 				
					SELECT -1, 'Bağlantılar', '000', '', 'icon-directions'
					WHERE EXISTS (	SELECT * 
									FROM OzelSayfa		OS
									JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA = OS.ID_OZELSAYFA
									WHERE	AKTIF				= 1
										AND Y.ID_KULLANICITIPI	= 1) --	ADMİN
				UNION				
					SELECT	  -1
							, OS.AD
							, CASE WHEN OS.ID_OZELSAYFA		> 999	THEN '000999'		ELSE  RIGHT( '000000' + CAST(OS.ID_OZELSAYFA AS varchar),6) END
							, OS.YOL + CASE WHEN OS.PARAMETRE IS NOT NULL THEN ( '?' + OS.PARAMETRE + '=' + @TCKIMLIKNO) ELSE '' END 						
							, CASE WHEN OS.ID_OZELSAYFATURU	= 1		THEN 'icon-globe'	ELSE 'icon-doc' END
					FROM OzelSayfa		OS
					JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA = OS.ID_OZELSAYFA				
					WHERE	AKTIF				= 1
						AND Y.ID_KULLANICITIPI	= 1	--	ADMİN
				--		UNION	-- RehberlikOnline
				--SELECT TOP 1
				--	ID_MENU				= -1,
				--	AD					= 'Rehberlik Online',
				--	KOD					= '-01',
				--	URL					= '',
				--	RESIM				= 'icon-list'

				--FROM Pusulam.dbo.RehberlikOnlineLink
				--WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP
			--UNION
			--	SELECT
			--		ID_MENU				= -1,
			--		AD					= TESTADI,
			--		KOD					= '-01' + RIGHT('000'+ CAST(ROW_NUMBER() OVER(ORDER BY TESTADI) AS varchar) ,3 ) ,
			--		URL					= LINK,
			--		RESIM				= 'icon-globe'

			--	FROM Pusulam.dbo.RehberlikOnlineLink
			--	WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP
				UNION
					SELECT 
						ID_MENU	= -1,
						AD		=  'Dijital Platformlar',
						KOD		= '999',
						URL		= '',
						RESIM	= 'icon-list'
				UNION
					SELECT DISTINCT
						ID_MENU	= -1,
						AD		= D.PROJE,
						KOD		= '999' + RIGHT('000'+ CAST(ROW_NUMBER() OVER(ORDER BY D.PROJE) AS varchar) ,3 ),
						URL		= D.URL + (CASE KY.ID_KULLANICITIPI WHEN  4 THEN '?ogrenciId=' ELSE '?ogretmenId=' END ) + Convert(varchar(max), EncryptByPassPhrase('prtkzprtkl',@TCKIMLIKNO),1),
						RESIM	= 'icon-list'
					FROM DisKaynakKademe  DK
					JOIN DisKaynak D ON  DK.ID_DISKAYNAK = D.ID_DISKAYNAK
					JOIN OkyanusDB..vw_KullaniciYetki KY  ON DK.ID_KADEME3 = KY.ID_KADEME3
					JOIN OkyanusDB..v3Kademe3		  K   ON K.ID_KADEME3 = KY.ID_KADEME3
					JOIN OkyanusDB..vw_SinifAlan	  S	  ON S.ID_SINIF = KY.ID_SINIF
					JOIN OkyanusDB..v4SinifAlan		  SA  ON SA.AD		= DK.ALAN AND SA.ID_SINIFALAN = S.ID_SINIFALAN
					WHERE KY.TCKIMLIKNO  = @TCKIMLIKNO AND KY.ID_KULLANICITIPI IN (3,4) 
					GROUP BY D.PROJE,URL, ID_KULLANICITIPI,KY.ID_KADEME3
					ORDER BY M.KOD

			END
			ELSE
			BEGIN	
			IF EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TCKIMLIKNOTEMP)
			BEGIN
				SELECT DISTINCT
					ID_MENU,
					AD,
					KOD,
					URL,
					RESIM
				FROM MENU
				WHERE ID_MENU IN(1050, 1333, 1381)
			END
			ELSE IF @ID_UYGULAMA = 5 --mobie
			BEGIN
				SELECT DISTINCT ID_KULLANICITIPI 
				INTO #v3SubeYetkiler FROM v3SubeYetki
				WHERE TCKIMLIKNO = @TCKIMLIKNOTEMP
				
				SELECT	DISTINCT
					 M.ID_MENU
					,M.AD
					,M.KOD
					,M.RESIM
				 FROM Mobile_Menu		M
				INNER JOIN Mobile_MenuYetki			Y	ON Y.ID_MENU			= M.ID_MENU  
				INNER JOIN #v3SubeYetkiler			S	ON S.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI
				INNER JOIN #KADEMELER			K	ON K.ID_KADEME			= Y.ID_KADEME
				WHERE M.GIZLI = 0 ORDER BY M.KOD

				DROP TABLE #v3SubeYetkiler
			END
			ELSE
			BEGIN						
				SELECT DISTINCT ID_KULLANICITIPI 
				INTO #v3SubeYetki FROM v3SubeYetki
				WHERE TCKIMLIKNO = @TCKIMLIKNOTEMP
				
				SELECT	DISTINCT
					 M.ID_MENU
					,M.AD
					,M.KOD
					--,M.URL					
					,URL	= CASE WHEN LEFT(M.URL, 7) = 'Dinamik' THEN eokul_v2.dbo.Fn_DinamikUrl(M.URL, @TCKIMLIKNOTEMP) ELSE M.URL END
					,M.RESIM
				INTO #MenuListe FROM Menu		M
				INNER JOIN MenuYetki			Y	ON Y.ID_MENU			= M.ID_MENU  
				INNER JOIN #v3SubeYetki			S	ON S.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI
				INNER JOIN #KADEMELER			K	ON K.ID_KADEME			= Y.ID_KADEME
				WHERE M.GIZLI = 0
				UNION			
			 		SELECT	-1
						, OS.AD
						, '001'
						, OS.YOL + @TCKIMLIKNO+'&OTURUM='+@OTURUM							
						, CASE WHEN OS.ID_OZELSAYFATURU	= 1 THEN 'icon-globe' ELSE 'icon-doc' END
				FROM OzelSayfa							OS
				JOIN OzelSayfaYetki						Y	ON	Y.ID_OZELSAYFA		= OS.ID_OZELSAYFA
				JOIN #KADEMELER							K	ON	K.ID_KADEME			= Y.ID_KADEME
				JOIN #v3SubeYetki						SY	ON	SY.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI 
				JOIN OkyanusDB.dbo.vw_KullaniciYetki	KY	ON	KY.TCKIMLIKNO		= @TCKIMLIKNOTEMP
															AND KY.ID_KADEME3		= Y.ID_KADEME3
				WHERE OS.AKTIF = 1 AND os.ID_OZELSAYFA=85			

					
				UNION										
				SELECT
					 M.ID_MENU
					,M.AD
					,M.KOD
					,M.URL
					,M.RESIM
				FROM Menu							M
				INNER  JOIN MenuKullaniciYetki		KY			ON M.ID_MENU		= KY.ID_MENU	
				WHERE KY.TCKIMLIKNO = @TCKIMLIKNOTEMP	AND M.GIZLI = 0
								
				UNION 

				SELECT -1, 'Bağlantılar', '000', '', 'icon-directions'
				WHERE EXISTS (	SELECT * 
								FROM OzelSayfa							OS
								JOIN OzelSayfaYetki						Y	ON	Y.ID_OZELSAYFA		= OS.ID_OZELSAYFA
								JOIN #KADEMELER							K	ON	K.ID_KADEME			= Y.ID_KADEME
								JOIN #v3SubeYetki						SY	ON	SY.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI 
								JOIN OkyanusDB.dbo.vw_KullaniciYetki	KY	ON	KY.TCKIMLIKNO		= @TCKIMLIKNOTEMP
																			AND KY.ID_KADEME3		= Y.ID_KADEME3
								WHERE AKTIF = 1)				
			UNION
				SELECT	-1
						, OS.AD
						, CASE WHEN OS.ID_OZELSAYFA > 999 THEN '000999' ELSE  RIGHT( '000000' + CAST(OS.ID_OZELSAYFA AS varchar),6) END
						, OS.YOL + CASE WHEN OS.PARAMETRE IS NOT NULL THEN ( '?' + OS.PARAMETRE + '=' + @TCKIMLIKNO) ELSE '' END 
						, CASE WHEN OS.ID_OZELSAYFATURU	= 1 THEN 'icon-globe' ELSE 'icon-doc' END
				FROM OzelSayfa							OS
				JOIN OzelSayfaYetki						Y	ON	Y.ID_OZELSAYFA		= OS.ID_OZELSAYFA
				JOIN #KADEMELER							K	ON	K.ID_KADEME			= Y.ID_KADEME
				JOIN #v3SubeYetki						SY	ON	SY.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI 
				JOIN OkyanusDB.dbo.vw_KullaniciYetki	KY	ON	KY.TCKIMLIKNO		= @TCKIMLIKNOTEMP
															AND KY.ID_KADEME3		= Y.ID_KADEME3
				WHERE OS.AKTIF = 1
			--UNION	-- CORPUS STUDIO
			--	SELECT TOP 1
			--		ID_MENU				= -1,
			--		AD					= 'Kariyer Profil Testi',
			--		KOD					= '-00',
			--		URL					= '',
			--		RESIM				= 'icon-list'

			--	FROM Pusulam.dbo.CurpusStudy
			--	WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP

			--	UNION
				--SELECT
				--	ID_MENU				= -1,
				--	AD					= 'Kariyer Profil Testi',
				--	KOD					= '-00001',
				--	URL					= 'http://okyanuskoleji.com/pdr/index.php?token='+TOKEN,
				--	RESIM				= 'icon-list'

				--FROM Pusulam.dbo.CurpusStudy
				--WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP AND YONETIM = 0
			--UNION	-- CORPUS STUDIO YÖNETİM
				--SELECT
				--	ID_MENU				= -1,
				--	AD					= 'Kariyer Profil Testi Yükle',
				--	KOD					= '-00002',
				--	URL					= 'http://okyanuskoleji.com/pdr/index.php?token='+TOKEN,
				--	RESIM				= 'icon-list'
				--FROM Pusulam.dbo.CurpusStudy
				--WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP AND YONETIM = 1
			--UNION	-- RehberlikOnline
			--	SELECT TOP 1
			--		ID_MENU				= -1,
			--		AD					= 'Rehberlik Online',
			--		KOD					= '-01',
			--		URL					= '',
			--		RESIM				= 'icon-list'

			--	FROM Pusulam.dbo.RehberlikOnlineLink
			--	WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP
			----UNION
			--	SELECT
			--		ID_MENU				= -1,
			--		AD					= TESTADI,
			--		KOD					= '-01' + RIGHT('000'+ CAST(ROW_NUMBER() OVER(ORDER BY TESTADI) AS varchar) ,3 ) ,
			--		URL					= LINK,
			--		RESIM				= 'icon-globe'

			--	FROM Pusulam.dbo.RehberlikOnlineLink
			--	WHERE TCKIMLIKNO =  @TCKIMLIKNOTEMP
				UNION
					SELECT 
						ID_MENU	= -1,
						AD		=  'Dijital Platformlar',
						KOD		= '999',
						URL		= '',
						RESIM	= 'icon-list'
					from OkyanusDB..vw_KullaniciYetki KY  
					WHERE KY.TCKIMLIKNO  = @TCKIMLIKNO AND KY.ID_KULLANICITIPI IN (3,4)
				UNION
						SELECT DISTINCT
						ID_MENU	= -1,
						AD		= D.PROJE,
						KOD		= '999' + RIGHT('000'+ CAST(ROW_NUMBER() OVER(ORDER BY D.PROJE) AS varchar) ,3 ),
						URL		= D.URL + (CASE KY.ID_KULLANICITIPI WHEN  4 THEN '?ogrenciId=' ELSE '?ogretmenId=' END ) + Convert(varchar(max), EncryptByPassPhrase('prtkzprtkl',@TCKIMLIKNO),1),
						RESIM	= 'icon-list'
					FROM DisKaynakKademe  DK
					JOIN DisKaynak D ON  DK.ID_DISKAYNAK = D.ID_DISKAYNAK
					JOIN OkyanusDB..vw_KullaniciYetki KY  ON DK.ID_KADEME3 = KY.ID_KADEME3
					JOIN OkyanusDB..v3Kademe3		  K   ON K.ID_KADEME3 = KY.ID_KADEME3
					JOIN OkyanusDB..vw_SinifAlan	  S	  ON S.ID_SINIF = KY.ID_SINIF
					JOIN OkyanusDB..v4SinifAlan		   SA  ON SA.AD		= DK.ALAN AND SA.ID_SINIFALAN = S.ID_SINIFALAN
					WHERE KY.TCKIMLIKNO  = @TCKIMLIKNO AND KY.ID_KULLANICITIPI IN (3,4) 
					GROUP BY D.PROJE,URL, ID_KULLANICITIPI,KY.ID_KADEME3
					ORDER BY M.KOD



				--UPDATE #MenuListe SET URL = '#Ogrenci/Hedefler/MevcutDurum'
				--WHERE 
				--	ID_MENU = 1 
				--AND EXISTS(SELECT ID_KADEME			FROM #KADEMELER			WHERE ID_KADEME			= 5)
				--AND EXISTS(SELECT ID_KULLANICITIPI	FROM #v3SubeYetki		WHERE ID_KULLANICITIPI	= 4)
				--
				--UPDATE #MenuListe SET URL = '#Ogrenci/Hedefler/MevcutDurumOO'
				--WHERE 
				--		ID_MENU = 1 
				--	AND EXISTS(SELECT ID_KADEME			FROM #KADEMELER			WHERE ID_KADEME			= 4)
				--	AND EXISTS(SELECT ID_KULLANICITIPI	FROM #v3SubeYetki		WHERE ID_KULLANICITIPI	= 4)

				IF @TCKIMLIKNOTEMP = '98765432101'	--	GARANTİ BBVA
				BEGIN

					SELECT	DISTINCT
						 M.ID_MENU
						,M.AD
						,M.KOD
						,M.URL
						,M.RESIM
					FROM Menu		M
					WHERE M.ID_MENU = 1316
					
				END 
				ELSE
				BEGIN
					SELECT * FROM #MenuListe
					ORDER BY KOD
				END
				END
				DROP TABLE #v3SubeYetki
				DROP TABLE #MenuListe
			END
		END
		
		IF @ISLEM = 2	--	Menü Getir YETKİ SAYFASI
		BEGIN
			SELECT 
					M.ID_MENU,
					replicate('-', LEN(KOD)-3)+AD AD,
					YETKILI = (SELECT COUNT(1) FROM MenuYetki Y WHERE Y.ID_KULLANICITIPI=@ID_KULLANICITIPI AND Y.ID_MENU=M.ID_MENU AND Y.ID_KADEME = @ID_KADEME)
			FROM Menu		M
			WHERE GIZLI = 0 AND (OZEL = 0 /*OR dbo.fn_GenelHak(@TCKIMLIKNO)>0*/)
			ORDER BY KOD 
		END
		
		
		IF @ISLEM = 3	--	Kullanıcı Menü Getir YETKİ SAYFASI
		BEGIN
			SELECT 
					M.ID_MENU,
					replicate('-', LEN(KOD)-3)+AD AD,
					YETKILI = (SELECT COUNT(1) FROM MenuKullaniciYetki Y WHERE Y.TCKIMLIKNO=@TC_YETKI AND Y.ID_MENU=M.ID_MENU)
			FROM Menu		M
			WHERE GIZLI = 0 AND (OZEL = 0 OR dbo.fn_GenelHak(@TCKIMLIKNOTEMP)>0)
			ORDER BY KOD 
		END
		
		
		IF @ISLEM = 4	--	Menü Yardım Getir
		BEGIN
			SELECT 
					YARDIMHTML
			FROM Menu		M
			WHERE ID_MENU=@ID_MENUYARDIM
		END

		DROP TABLE IF EXISTS #KADEMELER
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

			EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @_MSG, @SEVERITY = @_ErrorSeverity, @STATE = @_ErrorState, @ID_LOG = @ID_LOG, @ID_LOGTUR = 2
		END CATCH;
	
END





GO

USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_DisKaynakKademe]    Script Date: 3.01.2022 14:14:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_DisKaynakKademe]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	@ID_KADEME3			INT				= NULL,
	@ID_DISKAYNAK		INT				= NULL,
	@ID_KADEME3s		VARCHAR(MAX)	= NULL,
	@ALANs				VARCHAR(MAX)	= NULL,
	@IP					VARCHAR(50)		= NULL
AS
BEGIN
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 
			ISLEM						= @ISLEM		
			,TCKIMLIKNO					= @TCKIMLIKNO	
			,OTURUM						= @OTURUM		
			,ID_MENU					= @ID_MENU	
			,ID_KADEME					= @ID_KADEME3
			,ID_DISKAYNAK				= @ID_DISKAYNAK
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0

	BEGIN TRY
		EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME

		IF @ID_LOG > 1
		BEGIN
			IF @ISLEM = 1 -- DIŞKAYNAKKADEME TABLOSUNA KAYIT ATAR 
			BEGIN
				IF EXISTS(SELECT DK.ID_DISKAYNAK_KADEME FROM DisKaynakKademe DK WHERE DK.ID_KADEME3 = @ID_KADEME3 AND DK.ID_DISKAYNAK = @ID_DISKAYNAK)
				BEGIN
					EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = 'Zaten kayıtlı. Lütfen kontrol ediniz.', @SEVERITY = 16, @STATE = 1, @ID_LOG = @ID_LOG, @ID_LOGTUR = 3
				END
				ELSE
				BEGIN
					DROP TABLE IF EXISTS #ALAN
					DROP TABLE IF EXISTS #KADEME
					SELECT ALAN    = VALUE INTO #ALAN    FROM OPENJSON(@ALANs)
					SELECT KADEME3 = VALUE INTO #KADEME  FROM OPENJSON(@ID_KADEME3s)
					
					INSERT INTO DisKaynakKademe (ID_DISKAYNAK, ID_KADEME3, KYE_TARIH, KYE_TCKIMLIKNO,ALAN)
					SELECT @ID_DISKAYNAK AS ID_DISKAYNAK, KADEME3,GETDATE(),@TCKIMLIKNO, ALAN FROM #KADEME,#ALAN
				END
			END
			IF @ISLEM = 2 -- DIŞKAYNAKKADEME LİSTELER
			BEGIN
				SELECT 
					DK.ID_DISKAYNAK_KADEME,
					DK.ID_KADEME3,
					DK.ID_DISKAYNAK,
					D.PROJE,
					K.AD,
					DK.ALAN
				FROM DisKaynakKademe DK
				JOIN DisKaynak D ON DK.ID_DISKAYNAK = D.ID_DISKAYNAK
				JOIN OkyanusDB.dbo.v3Kademe3 K ON DK.ID_KADEME3 = K.ID_KADEME3
				WHERE D.AKTIF = 1
				ORDER BY D.PROJE
				FOR JSON PATH
			END
			IF @ISLEM = 3 -- DIŞKAYNKKADEMESİL
			BEGIN
				DELETE FROM DisKaynakKademe WHERE ID_DISKAYNAK = @ID_DISKAYNAK AND ID_KADEME3 = @ID_KADEME3
			END
			IF @ISLEM = 4 -- SELECT İÇİN DIŞ KAYNAK PROJELERİ LİSTELENİR
			BEGIN
				SELECT 
					ID_DISKAYNAK,
					PROJE
				FROM DisKaynak D WHERE D.AKTIF = 1 ORDER BY D.PROJE
				FOR JSON PATH
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

		EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @_MSG, @SEVERITY = @_ErrorSeverity, @STATE = @_ErrorState, @ID_LOG = @ID_LOG, @ID_LOGTUR = 2
	END CATCH
END



GO

USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_Bulten]    Script Date: 3.01.2022 14:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_Bulten]
	@ISLEM					INT				= NULL,
	@TCKIMLIKNO				VARCHAR(11)		= NULL,
	@OTURUM					VARCHAR(36)		= NULL,
	@ID_MENU				INT				= NULL,
	@SQLJSON				VARCHAR(MAX)	= NULL,
	@TC_OGRENCI				VARCHAR(11)		= NULL,
	@YAYIN_TARIH			VARCHAR(MAX)    = NULL,
	@YAYIN_BITIS			VARCHAR(MAX)	= NULL,
	@SINIF_LIST				VARCHAR(MAX)    = NULL,
	@DOSYA_GUID				VARCHAR(MAX)	= NULL,
	@ID_BULTEN				INT			    = NULL,
	@YAYINDA				BIT				= NULL,
	@IP					VARCHAR(50)	= NULL
AS
BEGIN	
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 ISLEM					= @ISLEM	
				,TCKIMLIKNO				= @TCKIMLIKNO
				,OTURUM					= @OTURUM
				,ID_MENU				= @ID_MENU

				,TC_OGRENCI				= @TC_OGRENCI
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @_ID_LOG INT = 0

	BEGIN TRY    
		DECLARE @_DESTEK_KONTROL INT
		EXEC @_ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME,@IP = @IP
		

		IF @_ID_LOG > 0
		BEGIN			
			IF @ISLEM = 1	--	BÜLTEN LİSTESİ			
			BEGIN
				SELECT SO.ID_SUBE, S.ID_SINIF, S.ID_KADEME3 
				INTO #OGRENCISUBESINIF
				FROM OkyanusDB.dbo.v3Ogrenci SO
				INNER JOIN OkyanusDB.dbo.v3SubeGrupSinif S ON S.ID_SINIF = SO.ID_SINIF
				WHERE TCKIMLIKNO = @TC_OGRENCI
				UNION
				SELECT S.ID_SUBE, S.ID_SINIF, S.ID_KADEME3 FROM OkyanusDB.dbo.v4SinifOgrenci SO
				INNER JOIN OkyanusDB.dbo.v4Sinif S ON S.ID_SINIF = SO.ID_SINIF AND S.AKTIF = 1
				INNER JOIN OkyanusDB.dbo.v3AktifDonem D ON D.DONEM = S.DONEM AND D.AKTIF = 1
				WHERE TC_OGRENCI = @TC_OGRENCI AND SO.AKTIF = 1

				SELECT
				ISNULL(
				(
					SELECT 
						TARIH = B.YAYIN_TARIH
					   ,DERS  = B.DERS
					   ,B.ACIKLAMA
					   ,EKLEYEN = K.AD + ' ' + K.SOYAD
					   ,B.DOSYA_GUID
					   ,B.LINK
					   ,B.BASLIK		
					FROM Bulten								B
					JOIN OkyanusDB.dbo.v3Kullanici			K  ON K.TCKIMLIKNO = B.KYE_TCKIMLIKNO
					JOIN BultenSinif						BS ON BS.ID_BULTEN = B.ID_BULTEN 
					WHERE B.AKTIF = 1 AND YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE))
				    AND	(BS.ID_SINIF IN (SELECT O.ID_SINIF FROM #OGRENCISUBESINIF O) OR BS.ID_SINIF = 0)
					ORDER BY B.YAYIN_TARIH DESC
					FOR JSON PATH
				 ), '[]')

				--SELECT
				--ISNULL(
				--(
				--	SELECT distinct D.ID_DOKUMHAN,
				--					D.ACIKLAMA,
				--					D.DOSYA,
				--					isnull(nullif(D.ICERIK,''),'yok') AS ICERIK,
				--					Convert(varchar(50),D.YAYINTARIHI,104) as TARIH,
				--					ISNULL((SELECT DOSYA as AD, RIGHT(DOSYA, 3) AS UZANTI FROM eokul_v2.dbo.DokumanDosya DD WHERE DD.ID_DOKUMAN = D.ID_DOKUMHAN FOR JSON PATH), '[]') as DOSYALAR,
				--					isnull(D.LINK,'') AS LINK,
				--					isnull(D.DERS,'') AS DERS,
				--					isnull(K.AD+' '+K.SOYAD,'') as EKLEYEN,
				--					'BUL' as MODUL,
				--					ID_KULLANICI=0,
				--					D.SILINMETARIHI,
				--					D.YAYINTARIHI
				--	FROM eokul_v2.dbo.Dokumhan D
				--	INNER JOIN eokul_v2.dbo.DokumhanYetki DY ON DY.ID_DOKUMHAN = D.ID_DOKUMHAN
				--	INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.AD = DY.GRUP
				--	INNER JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = D.TC_KULLANICI
				--	WHERE D.IPTAL = 0
				--	AND (DY.ID_SUBE IN (SELECT O.ID_SUBE FROM #OGRENCISUBESINIF O))
				--	AND (DY.ID_SINIF IN (SELECT O.ID_SINIF FROM #OGRENCISUBESINIF O) OR DY.ID_SINIF = 0)
				--	AND (K3.ID_KADEME3 IN (SELECT O.ID_KADEME3 FROM #OGRENCISUBESINIF O) OR DY.GRUP = '')
				--	AND d.SILINMETARIHI  >= CAST(GETDATE() as date)
				--	AND d.YAYINTARIHI    <= CAST(GETDATE() as date)
				--	order by d.YAYINTARIHI desc, d.ID_DOKUMHAN desc
				--	FOR JSON PATH
				--), '[]')

				DROP TABLE #OGRENCISUBESINIF
			END

			IF @ISLEM = 2   --  Bülten Ekle
			BEGIN			

				CREATE TABLE #BULTEN(ID_BULTEN INT)

				INSERT INTO  Bulten (DERS,BASLIK,LINK,ACIKLAMA,YAYIN_TARIH,YAYIN_BITIS,KYE_TCKIMLIKNO,KYE_TARIH,AKTIF)
				OUTPUT INSERTED.ID_BULTEN INTO #BULTEN(ID_BULTEN)
				SELECT Ders,Baslik,Link,Aciklama,@YAYIN_TARIH,@YAYIN_BITIS,@TCKIMLIKNO,GETDATE(),1
				FROM OPENJSON(@SQLJSON)
				WITH
				(
				  Ders			 VARCHAR(MAX),
				  Baslik		 VARCHAR(MAX),
				  Link			 VARCHAR(MAX),
				  Aciklama		 VARCHAR(MAX)
				)

				INSERT INTO BultenSinif(ID_BULTEN,ID_SINIF)
				SELECT (SELECT ID_BULTEN FROM #BULTEN),S.VALUE
				FROM OPENJSON(@SINIF_LIST)		S WHERE S.VALUE > 0

				SELECT ID_BULTEN FROM #BULTEN
			END

			IF @ISLEM = 3   -- Bülten Dosya Kaydet
			BEGIN
				UPDATE Bulten SET DOSYA_GUID = @DOSYA_GUID WHERE ID_BULTEN = @ID_BULTEN
			END
			IF @ISLEM = 4   -- Bülten Listele
			BEGIN
				IF EXISTS(SELECT TOP 1 * FROM OkyanusDB..vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULLANICITIPI = 1 )
				BEGIN
					IF @YAYINDA = 1
					BEGIN
					SELECT ISNULL((
							SELECT 
								 ID_BULTEN
								,BASLIK
								,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
								,ACIKLAMA
								,DOSYA_GUID
								,K.AD + ' ' +K.SOYAD AS EKLEYEN
								,B.LINK
							FROM Bulten							B
							JOIN v3Kullanici					K  ON K.TCKIMLIKNO  = B.KYE_TCKIMLIKNO
 							WHERE YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE)) AND B.AKTIF = 1 
							ORDER BY YAYIN_TARIH
							FOR JSON PATH
					),'[]')
					END
					ELSE
					BEGIN
						SELECT ISNULL((
							SELECT 
								 ID_BULTEN
								,BASLIK
								,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
								,ACIKLAMA
								,DOSYA_GUID
								,K.AD + ' ' +K.SOYAD AS EKLEYEN
								,B.LINK
							FROM Bulten					B
							JOIN v3Kullanici			K ON K.TCKIMLIKNO = B.KYE_TCKIMLIKNO
							WHERE NOT(YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE))) AND B.AKTIF = 1	
							ORDER BY YAYIN_TARIH
							FOR JSON PATH
					),'[]')
					END
				END
				ELSE
				BEGIN
					IF @YAYINDA = 1
					BEGIN
					SELECT ISNULL((
							SELECT 
								 ID_BULTEN
								,BASLIK
								,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
								,ACIKLAMA
								,DOSYA_GUID
								,K.AD + ' ' +K.SOYAD AS EKLEYEN
								,B.LINK
							FROM Bulten							B
							JOIN v3Kullanici					K  ON K.TCKIMLIKNO  = B.KYE_TCKIMLIKNO
 							WHERE YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE)) AND B.AKTIF = 1 AND   K.TCKIMLIKNO =@TCKIMLIKNO 
							ORDER BY YAYIN_TARIH
							FOR JSON PATH
					),'[]')
					END
					ELSE
					BEGIN
						SELECT ISNULL((
							SELECT 
								 ID_BULTEN
								,BASLIK
								,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
								,ACIKLAMA
								,DOSYA_GUID
								,K.AD + ' ' +K.SOYAD AS EKLEYEN
								,B.LINK
							FROM Bulten					B
							JOIN v3Kullanici			K ON K.TCKIMLIKNO = B.KYE_TCKIMLIKNO
							WHERE NOT(YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE))) AND B.AKTIF = 1
							AND KYE_TCKIMLIKNO=@TCKIMLIKNO
							ORDER BY YAYIN_TARIH
							FOR JSON PATH
					),'[]')
					END
				END
				
			END

			IF @ISLEM = 5   -- Bülten Sil
			BEGIN
				UPDATE Bulten SET AKTIF = 0 WHERE ID_BULTEN = @ID_BULTEN
				IF @YAYINDA = 1
				BEGIN
				SELECT ISNULL((
						SELECT 
							 ID_BULTEN
							,BASLIK
							,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
							,ACIKLAMA
							,DOSYA_GUID
							,K.AD + ' ' +K.SOYAD AS EKLEYEN
							,B.LINK
						FROM Bulten					B
						JOIN v3Kullanici			K ON K.TCKIMLIKNO = B.KYE_TCKIMLIKNO
						WHERE YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE)) AND B.AKTIF = 1
						ORDER BY YAYIN_TARIH
						FOR JSON PATH
				),'[]')
				END
				ELSE
				BEGIN
					SELECT ISNULL((
						SELECT 
							 ID_BULTEN
							,BASLIK
							,CAST(FORMAT(YAYIN_TARIH,'dd.MM.yyyy') AS VARCHAR(MAX)) + ' - ' + CAST(FORMAT(YAYIN_BITIS,'dd.MM.yyyy') AS VARCHAR(MAX)) AS TARIH
							,ACIKLAMA
							,DOSYA_GUID
							,K.AD + ' ' +K.SOYAD AS EKLEYEN
							,B.LINK
						FROM Bulten					B
						JOIN v3Kullanici			K ON K.TCKIMLIKNO = B.KYE_TCKIMLIKNO
						WHERE NOT(YAYIN_TARIH <= (CAST (GETDATE() AS DATE)) AND YAYIN_BITIS >= (CAST (GETDATE() AS DATE))) AND B.AKTIF = 1
						ORDER BY YAYIN_TARIH
						FOR JSON PATH
				),'[]')
				END
			END

			IF @ISLEM = 6   -- Bülten Getir 
			BEGIN
				SELECT ISNULL((
					SELECT 
						DERS
					   ,BASLIK
					   ,LINK
					   ,ACIKLAMA
					   ,YAYIN_TARIH
					   ,YAYIN_BITIS					   
					FROM BULTEN B
					WHERE ID_BULTEN = @ID_BULTEN
					FOR JSON PATH
				),'{}')				
			END

			IF @ISLEM = 7  -- Bülten Düzenle
			BEGIN				
				UPDATE B 
				SET B.DERS = J.Ders,B.BASLIK = J.Baslik, B.LINK = J.Link,B.ACIKLAMA = J.Aciklama, B.YAYIN_TARIH = @YAYIN_TARIH, B.YAYIN_BITIS = @YAYIN_BITIS
				FROM Bulten as B
				JOIN OPENJSON(@SQLJSON)
				WITH (
				  ID_BULTEN		 INT,
				  Ders			 VARCHAR(MAX),
				  Baslik		 VARCHAR(MAX),
				  Link			 VARCHAR(MAX),
				  Aciklama		 VARCHAR(MAX)
				) AS J ON J.ID_BULTEN = B.ID_BULTEN WHERE B.ID_BULTEN = @ID_BULTEN

				SELECT @ID_BULTEN
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

