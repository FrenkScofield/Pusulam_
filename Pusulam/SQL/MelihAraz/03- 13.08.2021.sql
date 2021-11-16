USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_VIU]    Script Date: 12.08.2021 16:58:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_VIU]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	
	@BASTARIH			VARCHAR(MAX)	= NULL,
	@BITTARIH			VARCHAR(MAX)	= NULL,
	@ID_SUBELIST		VARCHAR(MAX)	= NULL,
	@TCOGRETMENLIST		VARCHAR(MAX)	= NULL,
	@ID_ARAMADURUMLIST	VARCHAR(MAX)	= NULL,
	@ID_ARAMASEBEPLIST	VARCHAR(MAX)	= NULL,
	@JSON				BIT				= NULL,
	@ID_SUBELER		    VARCHAR(MAX)	= NULL,
	@ID_SINIFLAR        VARCHAR(MAX)	= NULL,
	@SQLJSON			VARCHAR(MAX)	= NULL,
	@TC_OGRETMEN		VARCHAR(11)		= NULL,
	@DEVICE_ID			VARCHAR(MAX)	= NULL
AS
BEGIN
	
	
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 ISLEM				= @ISLEM
				,TCKIMLIKNO			= @TCKIMLIKNO
				,OTURUM				= @OTURUM
				,ID_MENU			= @ID_MENU
									
				,BASTARIH			= @BASTARIH
				,BITTARIH			= @BITTARIH
				,ID_SUBELIST		= @ID_SUBELIST
				,TCOGRETMENLIST		= @TCOGRETMENLIST
				,ID_ARAMADURUMLIST	= @ID_ARAMADURUMLIST
				,ID_ARAMASEBEPLIST	= @ID_ARAMASEBEPLIST
				,[JSON]				= @JSON
				,ID_SUBELER		    = @ID_SUBELER
				,ID_SINIFLAR        = @ID_SINIFLAR
				,SQLJSON			= @SQLJSON
				,TC_OGRETMEN		= @TC_OGRETMEN
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0

	BEGIN TRY    
		EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
		
		IF @ID_LOG > 0
		BEGIN	
			IF @ISLEM = 1	--	VIU Rapor Mesaj
			BEGIN			
				SELECT
					 ACIKLAMA	=	B.AD + IIF(D.ID_DOKUMAN IS NOT NULL ,'https://okyanusdata.s3-eu-west-1.amazonaws.com/viu/'+ D.GUID ,'')
					,KYE_TARIH	=	CONVERT(VARCHAR(10), B.KYE_TARIH, 104) +' - '+ CONVERT(VARCHAR(5), B.KYE_TARIH, 108)
					,GONDERENTC	=	KYE.TCKIMLIKNO
					,GONDEREN	=	KYE.AD  +' '+ KYE.SOYAD
					,KIME		=	KIME.AD +' '+ KIME.SOYAD
					,KIMICIN	=	KYI.AD  +' '+ KYI.SOYAD
					
					,GONDERENSUBE	=	STUFF((
									SELECT DISTINCT ', ' + SB.AD
									FROM OkyanusDB.dbo.v3SubeYetki	SY
									INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
									WHERE SY.TCKIMLIKNO = KYE.TCKIMLIKNO
									FOR XML PATH('')
									), 1, 1, '')
					,GONDERENTIP	=	STUFF((
									SELECT DISTINCT ', ' + SB.AD
									FROM OkyanusDB.dbo.v3SubeYetki				SY
									INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
									WHERE SY.TCKIMLIKNO = KYE.TCKIMLIKNO
									FOR XML PATH('')
									), 1, 1, '')
					,KIMESUBE		=	STUFF((
									SELECT DISTINCT ', ' + SB.AD
									FROM OkyanusDB.dbo.v3SubeYetki	SY
									INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
									WHERE SY.TCKIMLIKNO = KIME.TCKIMLIKNO
									FOR XML PATH('')
									), 1, 1, '')
					,KIMETIP		=	STUFF((
									SELECT DISTINCT ', ' + SB.AD
									FROM OkyanusDB.dbo.v3SubeYetki				SY
									INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
									WHERE SY.TCKIMLIKNO = KIME.TCKIMLIKNO
									FOR XML PATH('')
									), 1, 1, '')
					,RENK = IIF( IPTAL = 1 , 'Red' , 'Black' )
				FROM OkyanusIletisim.dbo.BilgiNot					B
				INNER JOIN OkyanusIletisim.dbo.BilgiNotKullanici	N		ON	N.ID_BILGINOT	= B.ID_BILGINOT
				LEFT  JOIN OkyanusIletisim.dbo.Dokuman				D		ON	D.ID_DOKUMAN	= B.ID_DOKUMAN
				INNER JOIN OkyanusDB.dbo.v3Kullanici				KYE		ON	KYE.TCKIMLIKNO	= B.KYE_TCKIMLIKNO
				INNER JOIN OkyanusDB.dbo.v3Kullanici				KYI		ON	KYI.TCKIMLIKNO	= N.TCKIMLIKNO_KIMICIN
				INNER JOIN OkyanusDB.dbo.v3Kullanici				KIME	ON	KIME.TCKIMLIKNO	= N.TCKIMLIKNO_KIME
				WHERE CAST(B.KYE_TARIH AS DATE) BETWEEN CAST(@BASTARIH AS DATE) AND CAST(@BITTARIH AS DATE)
					AND (
							(
								EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND
								KYE.TCKIMLIKNO IN (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
							)
							OR
							(
								NOT EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND
								KYE.TCKIMLIKNO IN ( 
													SELECT DISTINCT  SY.TCKIMLIKNO
													FROM OkyanusDB.dbo.v3SubeYetki	SY
													INNER JOIN OkyanusDB.dbo.v3Sube	SB ON SB.ID_SUBE = SY.ID_SUBE
													WHERE SY.TCKIMLIKNO = KYE.TCKIMLIKNO AND SB.ID_SUBE	IN ( SELECT VALUE FROM OPENJSON (@ID_SUBELIST))				
													)	
							)
					)		
				ORDER BY B.KYE_TARIH DESC
			END
	
			IF @ISLEM = 2	--	VIU Rapor VIÜ Kullanmayanlar
			BEGIN				
				
				DECLARE @DONEM NVARCHAR(MAX)

				SELECT TOP 1 @DONEM = '01.09.' + DONEM 
				FROM OkyanusDB.dbo.v3AktifDonem
				WHERE AKTIF = 1


				--SELECT DISTINCT
				--	 SUBE			= SB.AD
				--	,TCKIMLIKNO		= K.TCKIMLIKNO
				--	,AD				= K.AD
				--	,SOYAD			= K.SOYAD
				--	,KULLANICIAD	= K.KULLANICIAD
				--	,SIFRE			= K.SIFRE
				--	,SINIF			= SN.AD
				--	,KADEME			= K3.AD
				--	,ID_LOGINLOG	= MAX(ID_LOGINLOG) 
				--	,[LOGIN]		= IIF(L.KYE_TCKIMLIKNO IS NULL, 0, 1)
				--INTO #KULLANICILIST
				--FROM		OkyanusDB.dbo.v3Kullanici	K
				--INNER JOIN	OkyanusDB.dbo.v3SubeYetki	SY	ON	SY.TCKIMLIKNO	= K.TCKIMLIKNO 
				--INNER JOIN	OkyanusDB.dbo.v3OgrenciVeli	OV	ON	OV.TCKIMLIKNO	= K.TCKIMLIKNO
				--INNER JOIN	OkyanusDB.dbo.v3Ogrenci		OG	ON	OG.TCKIMLIKNO	= OV.TCKIMLIKNO_OGR
				--INNER JOIN	OkyanusDB.dbo.v3Sinif		SN	ON	SN.ID_SINIF		= OG.ID_SINIF
				--INNER JOIN	OkyanusDB.dbo.v3Satislar	SL	ON	SL.ID_SINIF		= SN.ID_SINIF
				--INNER JOIN	OkyanusDB.dbo.v3SatisTuru	ST	ON	ST.ID_SATISTURU	= SL.ID_SATISTURU
				--INNER JOIN	OkyanusDB.dbo.v3Kademe3		K3	ON	K3.ID_KADEME3	= ST.ID_KADEME3 
				--INNER JOIN	OkyanusDB.dbo.v3Sube		SB	ON	SB.ID_SUBE		= SY.ID_SUBE
				--LEFT JOIN	OkyanusIletisim.dbo.LoginLog L	ON	CAST(L.KYE_TARIH AS DATE) >= CAST(@DONEM AS DATE) 
				--											AND L.KYE_TCKIMLIKNO = K.TCKIMLIKNO
				--WHERE	K.AKTIF = 1 
				--	AND SY.ID_KULLANICITIPI IN (5, 3, 53, 54, 26) --veli-öğretmen-müdür-mdryrd-rehber 
				--	AND SY.ID_SUBE IN (SELECT VALUE FROM OPENJSON(@ID_SUBELIST))
				--GROUP BY SB.AD, K.TCKIMLIKNO, K.AD, K.SOYAD, K.KULLANICIAD, K.SIFRE, SN.AD, K3.AD, L.KYE_TCKIMLIKNO
				
				DROP TABLE IF EXISTS #KULLANICILIST
				DROP TABLE IF EXISTS #SUBELIST

				SELECT VALUE ID_SUBE INTO #SUBELIST FROM OPENJSON(@ID_SUBELIST)

				SELECT DISTINCT 
					 SUBE			= (SELECT STUFF((	SELECT DISTINCT ', ' +  CAST( SB.AD AS VARCHAR) 
														FROM OkyanusDB.dbo.vw_KullaniciYetki	SKY 
														JOIN OkyanusDB.dbo.v3Sube				SB	ON	SB.ID_SUBE = SKY.ID_SUBE
														WHERE	SKY.TCKIMLIKNO	= KY.TCKIMLIKNO													
															AND SKY.ID_KULLANICITIPI IN (5, 3, 53, 54, 26) --veli-öğretmen-müdür-mdryrd-rehber 													
										FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 2, ''))
					,TCKIMLIKNO		= KY.TCKIMLIKNO
					,AD				= K.AD
					,SOYAD			= K.SOYAD
					,KULLANICIAD	= K.KULLANICIAD
					,SIFRE			= K.SIFRE
					,KULLANICITIPI	= (SELECT STUFF((	SELECT DISTINCT ', ' +  CAST( KT.AD AS VARCHAR) 
														FROM OkyanusDB.dbo.vw_KullaniciYetki	SKY 
														JOIN #SUBELIST							SL	ON	SL.ID_SUBE	= SKY.ID_SUBE
														JOIN OkyanusDB.dbo.v3KullaniciTipi		KT	ON	KT.ID_KULLANICITIPI	= SKY.ID_KULLANICITIPI
														WHERE	SKY.TCKIMLIKNO			= KY.TCKIMLIKNO
															AND SKY.ID_KULLANICITIPI	 IN (5, 3, 53, 54, 26)
										FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 2, ''))
					,SINIF			= (SELECT STUFF((	SELECT DISTINCT ', ' +  CAST( SN.AD AS VARCHAR) 
														FROM OkyanusDB.dbo.vw_KullaniciYetki	SKY 
														JOIN #SUBELIST							SL	ON	SL.ID_SUBE	= SKY.ID_SUBE
														JOIN OkyanusDB.dbo.v3Sinif				SN	ON	SN.ID_SINIF = SKY.ID_SINIF
														WHERE	SKY.TCKIMLIKNO			= KY.TCKIMLIKNO
															AND SKY.ID_KULLANICITIPI	= 5
										FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 2, ''))
					,KADEME			= (SELECT STUFF((	SELECT DISTINCT ', ' +  CAST( SGS.KADEME3 AS VARCHAR) 
														FROM OkyanusDB.dbo.vw_KullaniciYetki	SKY 
														JOIN #SUBELIST							SL	ON	SL.ID_SUBE	= SKY.ID_SUBE
														JOIN OkyanusDB.dbo.vw_SubeGrupSinif		SGS	ON	SGS.ID_SINIF = SKY.ID_SINIF
														WHERE	SKY.TCKIMLIKNO			= KY.TCKIMLIKNO
															AND SKY.ID_KULLANICITIPI	= 5
										FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 2, ''))
					,ID_LOGINLOG	= MAX(ID_LOGINLOG) 
					,[LOGIN]		= IIF(L.KYE_TCKIMLIKNO IS NULL, 0, 1)
				INTO #KULLANICILIST
				FROM OkyanusDB.dbo.vw_KullaniciYetki	KY
				JOIN v3Kullanici						K	ON	K.TCKIMLIKNO	= KY.TCKIMLIKNO
				JOIN #SUBELIST							SL	ON	SL.ID_SUBE		= KY.ID_SUBE
				LEFT JOIN OkyanusIletisim.dbo.LoginLog	L	ON	CAST(L.KYE_TARIH AS DATE) >= CAST(@DONEM AS DATE) 
															AND L.KYE_TCKIMLIKNO = K.TCKIMLIKNO
				WHERE	KY.ID_KULLANICITIPI IN (5, 3, 53, 54, 26) --veli-öğretmen-müdür-mdryrd-rehber 
				GROUP BY KY.TCKIMLIKNO, K.AD, K.SOYAD, K.KULLANICIAD, K.SIFRE, L.KYE_TCKIMLIKNO--, SN.AD, K3.AD, SB.AD




				SELECT KL.*
					,LOGINTARIH		= CONVERT(VARCHAR(10), L.KYE_TARIH, 104) + ' ' + CONVERT(VARCHAR(8), L.KYE_TARIH, 108) 
					,LOGINOTURUM	= SUBSTRING(CAST(L.OTURUM AS VARCHAR(MAX)),1,LEN(CAST(L.OTURUM AS VARCHAR(MAX)))-5)+ '*****'
				FROM #KULLANICILIST						KL
				LEFT JOIN OkyanusIletisim.dbo.LoginLog	L	ON	L.ID_LOGINLOG	= KL.ID_LOGINLOG
				ORDER BY SUBE, AD, SOYAD
								
				DROP TABLE IF EXISTS #KULLANICILIST
				DROP TABLE IF EXISTS #SUBELIST


			END
			
			IF @ISLEM = 3	--	Arama Sebep Liste
			BEGIN
				SELECT
				(
					SELECT ID_ARAMASEBEP, S.AD + ' (' + K.AD + ')' AS AD
					FROM [OkyanusIletisim].[dbo].[AramaSebep] S
					JOIN OkyanusDB.dbo.v3Kademe K ON K.ID_KADEME = S.ID_KADEME
					ORDER BY S.ID_KADEME, S.AD
					FOR JSON PATH
				)
			END

			IF @ISLEM = 4	--	Arama Durum Liste
			BEGIN
				SELECT
				(
					SELECT ID_ARAMADURUM, S.AD
					FROM [OkyanusIletisim].[dbo].[AramaDurum] S
					ORDER BY S.AD
					FOR JSON PATH
				)
			END

			IF @ISLEM = 5	--	Arama Rapor
			BEGIN
				IF @JSON = 1
				BEGIN
					SELECT 
					ISNULL (
					(
						SELECT	 GUID			=	A.GUID
								,KYE_TARIH		=	CONVERT(VARCHAR(10), A.KYE_TARIH, 104) +' - '+ CONVERT(VARCHAR(5), A.KYE_TARIH, 108)
								,ARAYANTC		=	A.TC_ARAYAN
								,ARAYAN			=	K.AD  +' '+ K.SOYAD
								,KIME			=	A.ADSOYAD_ARANAN
								,ARAMASEBEP		=	S.AD 
								,ARAMADURUM		=	D.AD 
								,KIMICIN		=	KYI.AD  +' '+ KYI.SOYAD
								,GRUP			=	K3.AD
								,SINIF			=	SNF.AD
								,ARAYANSUBE		=	STUFF(( SELECT DISTINCT ', ' + SB.AD
															FROM OkyanusDB.dbo.v3SubeYetki	SY
															INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
															WHERE SY.TCKIMLIKNO = A.TC_ARAYAN
															FOR XML PATH('')
													), 1, 1, '')
								--,ARAYANTIP		=	STUFF((
								--				SELECT DISTINCT ', ' + SB.AD
								--				FROM OkyanusDB.dbo.v3SubeYetki				SY
								--				INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
								--				WHERE SY.TCKIMLIKNO = A.TC_ARAYAN
								--				FOR XML PATH('')
								--				), 1, 1, '')
								,KIMESUBE		=	STUFF(( SELECT DISTINCT ', ' + SB.AD
															FROM OkyanusDB.dbo.v3SubeYetki	SY
															INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
															WHERE SY.TCKIMLIKNO = KYI.TCKIMLIKNO
															FOR XML PATH('')
													), 1, 1, '')
								--,KIMETIP		=	STUFF((
								--				SELECT DISTINCT ', ' + SB.AD
								--				FROM OkyanusDB.dbo.v3SubeYetki				SY
								--				INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
								--				WHERE SY.TCKIMLIKNO = KYI.TCKIMLIKNO
								--				FOR XML PATH('')
								--				), 1, 1, '') 
						FROM OkyanusIletisim.dbo.Arama		A	WITH (NOLOCK)
						JOIN OkyanusDB.dbo.v3Kullanici		K	ON	K.TCKIMLIKNO	= A.TC_ARAYAN
						JOIN OkyanusDB.dbo.v3Kullanici		KYI ON	KYI.TCKIMLIKNO	= A.TC_ARAMASEBEP
						JOIN OkyanusDB.dbo.v3Ogrenci		O	ON	O.TCKIMLIKNO	= A.TC_ARAMASEBEP
						JOIN OkyanusDB.dbo.v3Sinif			SNF	ON	SNF.ID_SINIF	= O.ID_SINIF
						JOIN OkyanusDB.dbo.v3SatisTuru		ST	ON	ST.ID_SATISTURU	= SNF.ID_SATISTURU
						JOIN OkyanusDB.dbo.v3Kademe3		K3	ON	K3.ID_KADEME3	= ST.ID_KADEME3
						JOIN OkyanusIletisim.dbo.AramaSebep S	ON	S.ID_ARAMASEBEP = A.ID_ARAMASEBEP
						JOIN OkyanusIletisim.dbo.AramaDurum D	ON	D.ID_ARAMADURUM = A.ID_ARAMADURUM
						WHERE	CAST(A.KYE_TARIH AS DATE) BETWEEN CAST(@BASTARIH AS DATE) AND CAST(@BITTARIH AS DATE)
						AND A.ID_ARAMADURUM IN (SELECT VALUE FROM OPENJSON(@ID_ARAMADURUMLIST))
						AND A.ID_ARAMASEBEP IN (SELECT VALUE FROM OPENJSON(@ID_ARAMASEBEPLIST))
						AND (
									(	EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND K.TCKIMLIKNO IN (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST)))
							OR		(NOT EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND K.TCKIMLIKNO IN (	SELECT DISTINCT  SY.TCKIMLIKNO
														FROM OkyanusDB.dbo.v3SubeYetki	SY
														INNER JOIN OkyanusDB.dbo.v3Sube	SB ON SB.ID_SUBE = SY.ID_SUBE
														WHERE SY.TCKIMLIKNO = K.TCKIMLIKNO AND SB.ID_SUBE IN ( SELECT VALUE FROM OPENJSON (@ID_SUBELIST))				
									)	
								)
							)	
						AND EXISTS ( SELECT TOP 1 Y.ID_SUBE FROM v3SubeYetki Y WHERE Y.TCKIMLIKNO = A.TC_ARAMASEBEP AND Y.ID_SUBE IN ( SELECT VALUE FROM OPENJSON (@ID_SUBELIST)  ))
						ORDER BY A.KYE_TARIH DESC
						FOR JSON PATH
					), '[]')
				END
				ELSE
				BEGIN
					SELECT	 GUID			=	A.GUID
							,KYE_TARIH		=	CONVERT(VARCHAR(10), A.KYE_TARIH, 104) +' - '+ CONVERT(VARCHAR(5), A.KYE_TARIH, 108)
							,ARAYANTC		=	A.TC_ARAYAN
							,ARAYAN			=	K.AD  +' '+ K.SOYAD
							,KIME			=	A.ADSOYAD_ARANAN
							,ARAMASEBEP		=	S.AD 
							,ARAMADURUM		=	D.AD 
							,KIMICIN		=	KYI.AD  +' '+ KYI.SOYAD
							,GRUP			=	K3.AD
							,SINIF			=	SNF.AD
							,ARAYANSUBE		=	STUFF((
											SELECT DISTINCT ', ' + SB.AD
											FROM OkyanusDB.dbo.v3SubeYetki	SY
											INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
											WHERE SY.TCKIMLIKNO = A.TC_ARAYAN
											FOR XML PATH('')
											), 1, 1, '')
							--,ARAYANTIP		=	STUFF((
							--				SELECT DISTINCT ', ' + SB.AD
							--				FROM OkyanusDB.dbo.v3SubeYetki				SY
							--				INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
							--				WHERE SY.TCKIMLIKNO = A.TC_ARAYAN
							--				FOR XML PATH('')
							--				), 1, 1, '')
							,KIMESUBE		=	STUFF((
											SELECT DISTINCT ', ' + SB.AD
											FROM OkyanusDB.dbo.v3SubeYetki	SY
											INNER JOIN OkyanusDB.dbo.v3Sube	SB	ON	SB.ID_SUBE = SY.ID_SUBE
											WHERE SY.TCKIMLIKNO = KYI.TCKIMLIKNO
											FOR XML PATH('')
											), 1, 1, '')
							--,KIMETIP		=	STUFF((
							--				SELECT DISTINCT ', ' + SB.AD
							--				FROM OkyanusDB.dbo.v3SubeYetki				SY
							--				INNER JOIN OkyanusDB.dbo.v3KullaniciTipi	SB	ON	SB.ID_KULLANICITIPI = SY.ID_KULLANICITIPI
							--				WHERE SY.TCKIMLIKNO = KYI.TCKIMLIKNO
							--				FOR XML PATH('')
							--				), 1, 1, '') 
					FROM OkyanusIletisim.dbo.Arama A	WITH (NOLOCK)
					JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = A.TC_ARAYAN
					JOIN OkyanusDB.dbo.v3Kullanici KYI ON KYI.TCKIMLIKNO = A.TC_ARAMASEBEP
					JOIN OkyanusDB.dbo.v3Ogrenci		O	ON	O.TCKIMLIKNO	= A.TC_ARAMASEBEP
					JOIN OkyanusDB.dbo.v3Sinif			SNF	ON	SNF.ID_SINIF	= O.ID_SINIF
					JOIN OkyanusDB.dbo.v3SatisTuru		ST	ON	ST.ID_SATISTURU	= SNF.ID_SATISTURU
					JOIN OkyanusDB.dbo.v3Kademe3		K3	ON	K3.ID_KADEME3	= ST.ID_KADEME3
					JOIN OkyanusIletisim.dbo.AramaSebep S ON S.ID_ARAMASEBEP = A.ID_ARAMASEBEP
					JOIN OkyanusIletisim.dbo.AramaDurum D ON D.ID_ARAMADURUM = A.ID_ARAMADURUM
					WHERE CAST(A.KYE_TARIH AS DATE) BETWEEN CAST(@BASTARIH AS DATE) AND CAST(@BITTARIH AS DATE)
					AND A.ID_ARAMADURUM IN (SELECT VALUE FROM OPENJSON(@ID_ARAMADURUMLIST))
					AND A.ID_ARAMASEBEP IN (SELECT VALUE FROM OPENJSON(@ID_ARAMASEBEPLIST))
					AND (
							(
								EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND
								K.TCKIMLIKNO IN (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
							)
							OR
							(
								NOT EXISTS (SELECT VALUE FROM OPENJSON (@TCOGRETMENLIST))
								AND
								K.TCKIMLIKNO IN ( 
													SELECT DISTINCT  SY.TCKIMLIKNO
													FROM OkyanusDB.dbo.v3SubeYetki	SY
													INNER JOIN OkyanusDB.dbo.v3Sube	SB ON SB.ID_SUBE = SY.ID_SUBE
													WHERE SY.TCKIMLIKNO = K.TCKIMLIKNO AND SB.ID_SUBE IN ( SELECT VALUE FROM OPENJSON (@ID_SUBELIST))				
													)	
							)
						)	
					AND EXISTS ( SELECT TOP 1 Y.ID_SUBE FROM v3SubeYetki Y WHERE Y.TCKIMLIKNO = A.TC_ARAMASEBEP AND Y.ID_SUBE IN ( SELECT VALUE FROM OPENJSON (@ID_SUBELIST)  ))
					ORDER BY A.KYE_TARIH DESC
				END
			END

			IF @ISLEM = 6   --  Yoklama Rapor
			BEGIN
				--SELECT distinct 
				--	 D.SUBEAD
				--	,D.SINIF
				--	,K.AD + ' '+ K.SOYAD [AD SOYAD]
				--	,CONVERT(VARCHAR(10), Y.TARIH, 105)AS TARIH
				--	,K.TCKIMLIKNO
				--	,D.ID_SINIF
				--	,Y.ID_YOKLAMA
				--	,YK.GELDI
				--	,Y.TARIH AS tarih
				--	,CASE
				--			WHEN GELDI>0 THEN 'GELDİ'
				--			WHEN GELDI<1 THEN 'GELMEDİ'
				--		END AS DURUM
				--FROM  OkyanusDB.dbo.vw_SinifOgrenci						S 
				--INNER JOIN  OkyanusDB.dbo.vw_v4SinifDetay				D	ON S.ID_SINIF	= D.ID_SINIF 			
				--INNER JOIN  Pusulam.dbo.v3AktifDonem					AD	ON AD.DONEM		= d.DONEM 
				--INNER JOIN	OkyanusDB.dbo.v3Kullanici					K	ON K.TCKIMLIKNO	= S.TCKIMLIKNO 
				--INNER JOIN  [OkyanusIletisim].[dbo].[Yoklama]			Y	ON Y.ID_SINIF	= D.ID_SINIF	AND	S.ID_SINIF		= Y.ID_SINIF
				--INNER JOIN  [OkyanusIletisim].[dbo].[YoklamaKullanici] YK	ON YK.ID_YOKLAMA= Y.ID_YOKLAMA  AND YK.TCKIMLIKNO	= K.TCKIMLIKNO
				--WHERE AD.AKTIF=1        
				--AND ( ( D.ID_SUBE		IN (SELECT VALUE FROM Openjson (@ID_SUBELER)))) 
				--AND ( ( D.ID_SINIF		IN (SELECT VALUE FROM Openjson (@ID_SINIFLAR))))  
				--AND CAST(TARIH AS DATE) BETWEEN CAST(@BASTARIH AS DATE) AND CAST(@BITTARIH AS DATE)
				--ORDER BY  D.SUBEAD ASC,D.SINIF ASC,[AD SOYAD] ASC ,tarih DESC

				SELECT DERSNO, AD = BASSAAT + ' - ' + BITSAAT
				INTO #DERSSAAT
				FROM Pusulam.dbo.OnlineDersSaat   
				WHERE	BASSAAT != '00:00'
					AND BITSAAT != '00:00'

				SELECT CAST(DATEADD(DAY, number, @BASTARIH) AS DATE) [Date]
				INTO #DATES
				FROM master..spt_values
				WHERE type = 'P'
				AND DATEADD(DAY, number, @BASTARIH) <= @BITTARIH

				SELECT distinct 
					 SD.SUBEAD
					,SD.SINIF
					,K.AD + ' '+ K.SOYAD [AD SOYAD]
					,K.TCKIMLIKNO
					,S.ID_SINIF
				INTO #KULLANICI
				FROM		OkyanusDB.dbo.vw_SinifOgrenci				S 
				INNER JOIN  OkyanusDB.dbo.vw_v4SinifDetay				SD	ON S.ID_SINIF	= SD.ID_SINIF 			
				INNER JOIN  Pusulam.dbo.v3AktifDonem					AD	ON AD.DONEM		= SD.DONEM 
				INNER JOIN	OkyanusDB.dbo.v3Kullanici					K	ON K.TCKIMLIKNO	= S.TCKIMLIKNO 
				WHERE AD.AKTIF=1        
				AND ( ( SD.ID_SUBE		IN (SELECT VALUE FROM Openjson (@ID_SUBELER)))) 
				AND ( ( SD.ID_SINIF		IN (SELECT VALUE FROM Openjson (@ID_SINIFLAR))))  

				SELECT K.*, D.Date AS TARIH, CONVERT(VARCHAR(MAX), D.Date, 104) AS STRTARIH
				INTO #KULLANICITARIH
				FROM #KULLANICI K
				CROSS APPLY #DATES D

				SELECT K.TCKIMLIKNO, K.TARIH, DP.DERSSAAT, CASE WHEN YK.GELDI = 1 THEN 'GELDİ' ELSE 'GELMEDİ' END AS DURUM
				INTO #KULLANICIDURUM
				FROM #KULLANICITARIH K
				INNER JOIN [OkyanusIletisim].[dbo].[Yoklama]			Y	ON Y.ID_SINIF = K.ID_SINIF AND Y.TARIH = K.TARIH
				INNER JOIN  [OkyanusIletisim].[dbo].[YoklamaKullanici]	YK	ON YK.ID_YOKLAMA= Y.ID_YOKLAMA AND YK.TCKIMLIKNO = K.TCKIMLIKNO
				INNER JOIN  OkyanusDB.dbo.v3DersProgram DP					ON DP.ID_DERSPROGRAM = Y.ID_DERSPROGRAM


				DECLARE @INDEX INT = 1
				DECLARE @COLUMN VARCHAR(MAX) = ''
				DECLARE @DERSNO VARCHAR(MAX) = ''

				WHILE @INDEX <= (SELECT MAX(DERSNO) FROM #DERSSAAT WHERE AD != ' - ')
				BEGIN
					SELECT @COLUMN = AD, @DERSNO = DERSNO FROM #DERSSAAT WHERE DERSNO = @INDEX

					EXECUTE('
					ALTER TABLE #KULLANICITARIH
					ADD [' + @COLUMN + '] VARCHAR(MAX)
					')
		
					EXECUTE ('UPDATE K SET K.[' + @COLUMN + '] = ISNULL((
																			SELECT DURUM
																			FROM #KULLANICIDURUM KD
																			WHERE KD.TARIH = K.TARIH AND KD.TCKIMLIKNO = K.TCKIMLIKNO AND KD.DERSSAAT = ' + @DERSNO + '
																), ''-'') FROM #KULLANICITARIH K')


					SET @INDEX = @INDEX + 1
				END

				SELECT *
				FROM #KULLANICITARIH
				ORDER BY SUBEAD ASC,SINIF ASC, [AD SOYAD] ASC, TARIH

				DROP TABLE #KULLANICI
				DROP TABLE #DERSSAAT
				DROP TABLE #DATES
				DROP TABLE #KULLANICITARIH
				DROP TABLE #KULLANICIDURUM
			END

			IF @ISLEM = 7   --  Viu Yetki Listesi
			BEGIN
			
				SELECT 
				(
					SELECT	Distinct K.AD + ' '+ K.SOYAD AS ADSOYAD ,K.TCKIMLIKNO
							
							,(	
							   
								SELECT distinct D.SUBEAD AS SUBEAD,D.ID_SUBE, 
								SECILI = (SELECT COUNT(1) FROM  ViuAramaKullaniciSube S where D.ID_SUBE=S.ID_SUBE and S.TCKIMLIKNO=@TC_OGRETMEN)
								FROM OkyanusDB.dbo.vw_v4SinifDetay D 
								INNER JOIN OkyanusDB.dbo.vw_SinifOgrenci S   ON D.ID_SINIF=S.ID_SINIF 
								GROUP BY D.SUBEAD,D.ID_SUBE
								ORDER BY D.SUBEAD,D.ID_SUBE
								FOR JSON PATH
							 ) AS SUBE
							 ,(
							 SELECT distinct D.KADEME3 AS GRUP,D.ID_KADEME3, 
							 SECILI = (SELECT COUNT(1) FROM  ViuAramaKullaniciKademe K where K.ID_KADEME3=D.ID_KADEME3 and K.TCKIMLIKNO=@TC_OGRETMEN)
								FROM OkyanusDB.dbo.vw_v4SinifDetay D 
								INNER JOIN OkyanusDB.dbo.vw_SinifOgrenci S   ON D.ID_SINIF=S.ID_SINIF 
								GROUP BY D.KADEME3,D.ID_KADEME3
								ORDER BY D.ID_KADEME3
								FOR JSON PATH
							 )
							 AS GRUP
					FROM OkyanusDB.dbo.v3Kullanici	K
					INNER JOIN OkyanusDB.[dbo].[v4KullaniciSubeTurKademe] TK  ON   TK.TCKIMLIKNO=K.TCKIMLIKNO 
					WHERE K.TCKIMLIKNO =@TC_OGRETMEN
					ORDER BY K.AD + ' ' + K.SOYAD
					FOR JSON PATH
				)
			END

			IF @ISLEM = 8   --  Viu Yetki Kaydet
			BEGIN
		   
				DELETE FROM ViuAramaKullanici WHERE TCKIMLIKNO = @TC_OGRETMEN
				DELETE FROM ViuAramaKullaniciSube WHERE TCKIMLIKNO = @TC_OGRETMEN
				DELETE FROM ViuAramaKullaniciKademe WHERE TCKIMLIKNO = @TC_OGRETMEN
				
				MERGE INTO ViuAramaKullanici AS A 
                USING ( 
						SELECT *
                        FROM OPENJSON(@SQLJSON) WITH ( TCKIMLIKNO VARCHAR(MAX) )) B
                ON (A.TCKIMLIKNO = B.TCKIMLIKNO) 
                WHEN MATCHED THEN 
                UPDATE  SET A.TCKIMLIKNO =B.TCKIMLIKNO                  
                WHEN NOT MATCHED THEN 
                INSERT  (TCKIMLIKNO)
				VALUES (B.TCKIMLIKNO);

					

				SELECT DISTINCT TCKIMLIKNO
                FROM OPENJSON(@SQLJSON)
                WITH(	 TCKIMLIKNO	NVARCHAR(MAX)
		                ,SUBE		NVARCHAR(MAX) AS JSON 
		                ,GRUP		NVARCHAR(MAX) AS JSON
                ) AS J
					
                     
				INSERT INTO ViuAramaKullaniciSube(TCKIMLIKNO, ID_SUBE)
				SELECT DISTINCT TCKIMLIKNO, ID_SUBE 
				FROM OPENJSON(@SQLJSON)
				WITH(	 TCKIMLIKNO	NVARCHAR(MAX)
						,SUBE		NVARCHAR(MAX) AS JSON 
						,GRUP		NVARCHAR(MAX) AS JSON
				) AS J
				CROSS APPLY OPENJSON(J.SUBE)
				WITH(	 ID_SUBE	INT
						,SECILI		BIT	
				) AS S
				WHERE S.SECILI = 1

				INSERT INTO ViuAramaKullaniciKademe(TCKIMLIKNO,ID_KADEME3)
				SELECT DISTINCT TCKIMLIKNO, ID_KADEME3  
				FROM OPENJSON(@SQLJSON)
				WITH(	 TCKIMLIKNO	NVARCHAR(MAX)
						,SUBE		NVARCHAR(MAX) AS JSON 
						,GRUP		NVARCHAR(MAX) AS JSON
				) AS J
				CROSS APPLY OPENJSON(J.GRUP)
				WITH (	 ID_KADEME3	INT
						,SECILI		BIT	
				) AS G
				WHERE G.SECILI = 1








        END 

			IF @ISLEM = 9	--	Kullanıcı Tipine Göre Kullanıcı Listele
			BEGIN
		
				SELECT DISTINCT KL.TCKIMLIKNO,KL.AD + ' ' + KL.SOYAD ADSOYAD 
				FROM [dbo].[v3Kullanici]			KL	
				JOIN OkyanusDB.[dbo].[v3SubeYetki]	TK  ON   TK.TCKIMLIKNO=KL.TCKIMLIKNO 
				WHERE	TK.ID_KULLANICITIPI=101
				order by ADSOYAD 

			END

			IF @ISLEM = 10  --  Viu Toplu Mesaj Kaydet
			BEGIN				
				DROP TABLE IF EXISTS #MESAJLIST

				SELECT *, RN = ROW_NUMBER() OVER(ORDER BY TC_GONDEREN)
				INTO #MESAJLIST
				FROM OPENJSON(@SQLJSON)
				WITH (
					TC_GONDEREN	VARCHAR(MAX),
					TC_ALAN		VARCHAR(MAX),
					MESAJ		VARCHAR(MAX),
					LINK_ETIKET	VARCHAR(MAX),
					LINK		VARCHAR(MAX)
				)	ML
				JOIN v3Kullanici	K	ON	K.TCKIMLIKNO	= ML.TC_ALAN
				
				DECLARE @TOPLUMESAJ INT = NULL
				DECLARE @INSERT10 TABLE (ID INT)
				
				INSERT INTO OkyanusIletisim.dbo.TopluMesaj (KYE_TARIH,KYE_TCKIMLIKNO) 
					OUTPUT inserted.ID_TOPLUMESAJ INTO @INSERT10(ID) 
				VALUES(GETDATE(),@TCKIMLIKNO)

				SET @TOPLUMESAJ =(SELECT TOP 1 ID FROM @INSERT10)



				WHILE EXISTS ( SELECT TOP 1 1 FROM #MESAJLIST )
				BEGIN
		
					DECLARE @TC_GONDEREN	VARCHAR(MAX),
							@TC_ALAN		VARCHAR(MAX),
							@MESAJ			VARCHAR(MAX),
							@LINK_ETIKET	VARCHAR(MAX),
							@LINK			VARCHAR(MAX),
							@RN				INT
					SELECT TOP 1 
						 @TC_GONDEREN	= ML.TC_GONDEREN
						,@TC_ALAN		= ML.TC_ALAN
						,@MESAJ			= ML.MESAJ	
						,@LINK_ETIKET	= ML.LINK_ETIKET
						,@LINK			= ML.LINK
						,@RN			= ML.RN
					FROM #MESAJLIST		ML
					ORDER BY RN

					IF NOT EXISTS (
						SELECT TOP 1 1
						FROM v3Kullanici K
						WHERE K.TCKIMLIKNO	= @TC_GONDEREN
							AND K.AKTIF		= 1
					)
						SET  @TC_GONDEREN = '14531453000'	-- OKYANUS KOLEJLERİ
	
					
					EXEC OkyanusIletisim.dbo.sp_SendPushMessage
						 @TCKIMLIKNO		= @TC_GONDEREN
						,@HEADER			= 'Yeni Mesaj'
						,@MESAJ				= @MESAJ
						,@LINK				= @LINK
						,@MENU				= '000'
						,@TCKIMLIKNO_KIME	= @TC_ALAN
						,@ID_UYGULAMA		= 1
						,@MESAJTAM			= @MESAJ
						,@LINK_ETIKET		= @LINK_ETIKET
						,@TOPLUMESAJ		= @TOPLUMESAJ

					DELETE FROM #MESAJLIST WHERE RN = @RN

				END

				DROP TABLE IF EXISTS #MESAJLIST

			END

			IF @ISLEM = 11 --Kullanıcı Cihaz Listesi
			BEGIN
				SELECT ISNULL((
					SELECT DISTINCT
						K.TCKIMLIKNO, 
						ADSOYAD = K.AD + ' ' + K.SOYAD, 
						LL.DEVICE_ID
					FROM OkyanusIletisim.dbo.LoginLog LL WITH (NOLOCK)
					JOIN v3Kullanici K WITH (NOLOCK) ON K.TCKIMLIKNO = LL.KYE_TCKIMLIKNO
					WHERE DEVICE_ID != 'Pusulam'
						AND (KYE_TCKIMLIKNO = @TC_OGRETMEN OR DEVICE_ID = @DEVICE_ID)
				FOR JSON PATH
				),'[]')
			END
			
			IF @ISLEM = 12 --Kullanıcı Cihaz Raporu
			BEGIN				
				SELECT DISTINCT
					[TC KİMLİK NO] = K.TCKIMLIKNO, 
					[AD SOYAD] = K.AD + ' ' + K.SOYAD, 
					[CİHAZ ID] = LL.DEVICE_ID
				FROM OkyanusIletisim.dbo.LoginLog LL WITH (NOLOCK)
				JOIN v3Kullanici K WITH (NOLOCK) ON K.TCKIMLIKNO = LL.KYE_TCKIMLIKNO
				WHERE DEVICE_ID != 'Pusulam'
					AND (KYE_TCKIMLIKNO = @TC_OGRETMEN OR DEVICE_ID = @DEVICE_ID)				
			END
		END 
	END TRY
	BEGIN CATCH
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorSeverity 	= ERROR_SEVERITY(),
			@ErrorState		= ERROR_STATE()
				
		DECLARE @MSG VARCHAR(4000)
		SELECT @MSG = ERROR_MESSAGE()

		EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @MSG, @SEVERITY = @ErrorSeverity , @STATE = @ErrorState , @ID_LOG = @ID_LOG, @ID_LOGTUR = 2
	END CATCH;
END
