
CREATE TABLE [dbo].[RehberlikOnlineLink] (
    [TCKIMLIKNO] VARCHAR (11)   NOT NULL,
    [TESTADI]    NVARCHAR (MAX) NOT NULL,
    [LINK]       NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Altering [dbo].[sp_Menu]...';


GO
ALTER procedure [dbo].[sp_Menu]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@TC_YETKI			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENUYARDIM		INT				= 1,
	@ID_MENU			INT				= 1,
	@ID_KADEME			INT				= NULL,
	@ID_KULLANICITIPI	INT				= NULL	
AS
BEGIN

BEGIN TRY    
	DECLARE @MSG			VARCHAR(4000)
	DECLARE @ErrorSeverity	INT
	DECLARE @ErrorState		INT
		
	DECLARE @return_variable INT
	EXEC @return_variable = [dbo].[sp_OturumKontrol] @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU

	DECLARE @TCKIMLIKNOTEMP VARCHAR(11) = @TCKIMLIKNO
	
	IF @return_variable = 1
	BEGIN
	
		SELECT DISTINCT ID_KADEME INTO #KADEMELER FROM OkyanusDB.dbo.v4KullaniciKademe WHERE TCKIMLIKNO = @TCKIMLIKNOTEMP
		IF @ISLEM = 1	--	Menü Getir
		BEGIN	
			IF dbo.fn_GenelHak(@TCKIMLIKNO)>0
			BEGIN
					SELECT DISTINCT M.ID_MENU, M.AD, M.KOD, M.URL, M.RESIM
					FROM Menu							M
					where M.GIZLI = 0					
						 AND (		(@TCKIMLIKNO = '62362359264' AND ID_MENU IN (1099,1193,1200,1335,1344))
								OR	@TCKIMLIKNO != '62362359264')				
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
							, OS.YOL							
							, CASE WHEN OS.ID_OZELSAYFATURU	= 1		THEN 'icon-globe'	ELSE 'icon-doc' END
					FROM OzelSayfa		OS
					JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA = OS.ID_OZELSAYFA				
					WHERE	AKTIF				= 1
						AND Y.ID_KULLANICITIPI	= 1	--	ADMİN
				ORDER BY M.KOD

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
								FROM OzelSayfa		OS
								JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA		= OS.ID_OZELSAYFA
								JOIN #KADEMELER		K	ON	K.ID_KADEME			= Y.ID_KADEME
								JOIN #v3SubeYetki	SY	ON	SY.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI 
								WHERE AKTIF = 1)
				
				UNION

				SELECT	-1
						, OS.AD
						, CASE WHEN OS.ID_OZELSAYFA > 999 THEN '000999' ELSE  RIGHT( '000000' + CAST(OS.ID_OZELSAYFA AS varchar),6) END
						, OS.YOL
						, CASE WHEN OS.ID_OZELSAYFATURU	= 1 THEN 'icon-globe' ELSE 'icon-doc' END
				FROM OzelSayfa		OS
				JOIN OzelSayfaYetki	Y	ON	Y.ID_OZELSAYFA		= OS.ID_OZELSAYFA
				JOIN #KADEMELER		K	ON	K.ID_KADEME			= Y.ID_KADEME
				JOIN #v3SubeYetki	SY	ON	SY.ID_KULLANICITIPI	= Y.ID_KULLANICITIPI 
				WHERE OS.AKTIF = 1

				-- CORPUS STUDIO
					UNION
					SELECT TOP 1
						ID_MENU				= -1,
						AD					= 'Corpus.Study',
						KOD					= '-00',
						URL					= '',
						RESIM				= 'icon-list'

					FROM Pusulam.dbo.CurpusStudy
					WHERE TCKIMLIKNO =  @TCKIMLIKNO

					UNION
					SELECT
						ID_MENU				= -1,
						AD					= 'Corpus.Study',
						KOD					= '-00001',
						URL					= 'https://okyanus.corpus.study/okul-kontrol.php?token='+TOKEN,
						RESIM				= 'icon-list'

					FROM Pusulam.dbo.CurpusStudy
					WHERE TCKIMLIKNO =  @TCKIMLIKNO AND YONETIM = 0

					-- CORPUS STUDIO YÖNETİM
					UNION
					SELECT
						ID_MENU				= -1,
						AD					= 'Corpus.Study Yönetim',
						KOD					= '-00002',
						URL					= 'https://okyanus.corpus.study/yonetim/ogrt-kontrol.php?token='+TOKEN,
						RESIM				= 'icon-list'
					FROM Pusulam.dbo.CurpusStudy
					WHERE TCKIMLIKNO =  @TCKIMLIKNO AND YONETIM = 1
					
				-- RehberlikOnline
					UNION
					SELECT TOP 1
						ID_MENU				= -1,
						AD					= 'Rehberlik Online',
						KOD					= '-01',
						URL					= '',
						RESIM				= 'icon-list'

					FROM Pusulam.dbo.RehberlikOnlineLink
					WHERE TCKIMLIKNO =  @TCKIMLIKNO
					
					UNION
					SELECT
						ID_MENU				= -1,
						AD					= TESTADI,
						KOD					= '-01' + RIGHT('000'+ CAST(ROW_NUMBER() OVER(ORDER BY TESTADI) AS varchar) ,3 ) ,
						URL					= LINK,
						RESIM				= 'icon-globe'

					FROM Pusulam.dbo.RehberlikOnlineLink
					WHERE TCKIMLIKNO =  @TCKIMLIKNO

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

				IF @TCKIMLIKNO = '98765432101'	--	GARANTİ BBVA
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

				DROP TABLE #v3SubeYetki
				DROP TABLE #MenuListe
			END
		END
		
		IF @ISLEM = 2	--	Menü Getir YETKİ SAYFASI
		BEGIN
			SELECT 
					M.ID_MENU,
					replicate('-', LEN(KOD)-3)+AD AD,
					YETKILI = (SELECT COUNT(*) FROM MenuYetki Y WHERE Y.ID_KULLANICITIPI=@ID_KULLANICITIPI AND Y.ID_MENU=M.ID_MENU AND Y.ID_KADEME = @ID_KADEME)
			FROM Menu		M
			WHERE GIZLI = 0 AND (OZEL = 0 /*OR dbo.fn_GenelHak(@TCKIMLIKNO)>0*/)
			ORDER BY KOD 
		END
		
		
		IF @ISLEM = 3	--	Kullanıcı Menü Getir YETKİ SAYFASI
		BEGIN
			SELECT 
					M.ID_MENU,
					replicate('-', LEN(KOD)-3)+AD AD,
					YETKILI = (SELECT COUNT(*) FROM MenuKullaniciYetki Y WHERE Y.TCKIMLIKNO=@TC_YETKI AND Y.ID_MENU=M.ID_MENU)
			FROM Menu		M
			WHERE GIZLI = 0 AND (OZEL = 0 OR dbo.fn_GenelHak(@TCKIMLIKNO)>0)
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
PRINT N'Creating [dbo].[sp_RehberlikOnlineYukle]...';


GO
CREATE procedure [dbo].[sp_RehberlikOnlineYukle]
	@ISLEM			INT			= NULL,
	@TCKIMLIKNO		VARCHAR(11)	= NULL,
	@OTURUM			VARCHAR(36)	= NULL,
	@ID_MENU		INT			= NULL,

	@SQLJSON		VARCHAR(MAX)= NULL
	
AS
BEGIN
	
	
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 ISLEM			= @ISLEM	
				,TCKIMLIKNO		= @TCKIMLIKNO
				,OTURUM			= @OTURUM
				,ID_MENU		= @ID_MENU

				,SQLJSON		= @SQLJSON
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @_ID_LOG INT = 0

	BEGIN TRY    
		DECLARE @_DESTEK_KONTROL INT
		EXEC @_ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
		

		IF @_ID_LOG > 0
		BEGIN
			
			IF @ISLEM = 1	--	REHBERLİK ONLINE EXCEL YÜKLE						
			BEGIN
			
				DROP TABLE IF EXISTS #TEMP
				
				DELETE FROM RehberlikOnlineLink	

				SELECT TCKIMLIKNO = J.TCKIMLIKNO, TESTADI = J.TESTADI, LINK = J.LINK 
				INTO #TEMP
				FROM OPENJSON(@SQLJSON)
				WITH(
					 TCKIMLIKNO	VARCHAR(MAX)
					,TESTADI	VARCHAR(MAX)
					,LINK		VARCHAR(MAX)
				)  J

				INSERT INTO RehberlikOnlineLink (TCKIMLIKNO, TESTADI, LINK)
				SELECT T.TCKIMLIKNO, T.TESTADI, T.LINK 
				FROM #TEMP			T				
				JOIN v3Kullanici	K	ON	K.TCKIMLIKNO = T.TCKIMLIKNO
				WHERE	T.TESTADI	 IS NOT NULL
					AND T.LINK		 LIKE 'http%'

				--SELECT COUNT(1) FROM RehberlikOnlineLink

				SELECT ISNULL((
					SELECT  TCHATA = ISNULL((
								SELECT DISTINCT T.TCKIMLIKNO 
								FROM #TEMP T
								WHERE NOT EXISTS (SELECT TOP 1 1 FROM v3Kullanici K WHERE K.TCKIMLIKNO = T.TCKIMLIKNO)
								FOR JSON PATH
							),'[]'),
							TESTADIHATA = ISNULL((
								SELECT DISTINCT T.TCKIMLIKNO
								FROM #TEMP T
								WHERE T.TESTADI	IS NULL
								FOR JSON PATH
							),'[]'),
							LINKHATA = ISNULL((
								SELECT DISTINCT T.TCKIMLIKNO
								FROM #TEMP T
								WHERE T.LINK NOT LIKE 'http%'
								FOR JSON PATH
							),'[]')
					FOR JSON PATH, INCLUDE_NULL_VALUES
				),'[]')

				DROP TABLE IF EXISTS #TEMP

			END
			IF @ISLEM = 2	--	REHBERLİK ONLINE LİSTESİ			
			BEGIN
				SELECT ISNULL((
					SELECT	 K.TCKIMLIKNO
							,ADSOYAD	= K.AD+' '+K.SOYAD
							,K.SUBEAD
							,K.SINIFAD
							,ROL.TESTADI
							,ROL.LINK
					FROM RehberlikOnlineLink			ROL
					JOIN OkyanusDB..vw_OgrenciDetayTum	K	ON	K.TCKIMLIKNO	= ROL.TCKIMLIKNO
															AND K.DONEM			= OkyanusDB.dbo.fn_AktifDonem()
															AND K.ID_SINIF		> 0
					FOR JSON AUTO,INCLUDE_NULL_VALUES
				),'[]')
			END


		END


	END TRY
	BEGIN CATCH
		DECLARE @_ErrorSeverity INT;
		DECLARE @_ErrorState	INT;

		SELECT 
			@_ErrorSeverity	= ERROR_SEVERITY(),
			@_ErrorState	= ERROR_STATE()
				
		DECLARE @_MSG VARCHAR(4000)
		SELECT @_MSG = ERROR_MESSAGE()

		EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @_MSG, @SEVERITY = @_ErrorSeverity, @STATE = @_ErrorState, @ID_LOG = @_ID_LOG, @ID_LOGTUR = 2
	END CATCH;
END