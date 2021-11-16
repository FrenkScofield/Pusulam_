﻿USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp_SinavSonuclari]    Script Date: 16.02.2021 22:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_SinavSonuclari]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@TC_OGRENCI			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@IL					VARCHAR(36)		= NULL,
	@ILCE				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	@DONEM				char(4)			= NULL,
	@ID_KADEME3			INT				= NULL,
	@ID_SINAVTURU		INT				= NULL,
	@ID_SINAV			INT				= NULL,
	@ID_DERS			INT				= NULL,
	@ID_SUBE			INT			    = NULL,
	@ID_SUBES			NVARCHAR(MAX)   = NULL,
	@ID_SINIF			INT				= NULL,
	@ID_SINIFS			INT				= NULL,
	@DOSYAADI			varchar(100)	= NULL,
	@DOSYAGUID			varchar(50)		= NULL,
	@ID_SINAVDOSYA		int				= NULL,
	@SQLJSON			NVARCHAR(MAX)	= NULL		
AS
BEGIN

	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	
			ISLEM						= @ISLEM			
			,TCKIMLIKNO					= @TCKIMLIKNO		
			,TC_OGRENCI					= @TC_OGRENCI		
			,OTURUM						= @OTURUM			
			,IL							= @IL				
			,ILCE						= @ILCE			
			,ID_MENU					= @ID_MENU		
			,DONEM						= @DONEM			
			,ID_KADEME3					= @ID_KADEME3		
			,ID_SINAVTURU				= @ID_SINAVTURU	
			,ID_SINAV					= @ID_SINAV		
			,ID_DERS					= @ID_DERS		
			,ID_SUBE					= @ID_SUBE		
			,ID_SINIF					= @ID_SINIF		
			,DOSYAADI					= @DOSYAADI		
			,DOSYAGUID					= @DOSYAGUID		
			,ID_SINAVDOSYA				= @ID_SINAVDOSYA	
			,SQLJSON					= @SQLJSON		
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0

BEGIN TRY    
	DECLARE @MSG			VARCHAR(4000)
	DECLARE @ErrorSeverity	INT
	DECLARE @ErrorState		INT
		
	 

	EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME

	IF @ID_LOG<1
	BEGIN
		IF EXISTS(SELECT TOP 1 1 FROM DisOgrenci WHERE TCKIMLIKNO=@TC_OGRENCI)
			BEGIN
				SET @ID_LOG=2
			END
	END
	IF @ID_LOG > 1
	BEGIN
	
		DECLARE  @OZELSINAVGOR BIT=0
		IF EXISTS ( SELECT TOP 1 * FROM OkyanusDB.DBO.v3SubeYetki WHERE TCKIMLIKNO= @TCKIMLIKNO AND ID_KULLANICITIPI IN (1,60) ) -- ADMİN VEYA ÖD İSE ÖZEL SINAVLARI GÖRECEK
		BEGIN
			SET @OZELSINAVGOR=1
		END
						
		IF (@ISLEM = 1 OR @ISLEM = 2)	--	SINAV SONUÇLARI
		BEGIN
						
			DROP TABLE IF EXISTS #T1
			DROP TABLE IF EXISTS #T2
			DROP TABLE IF EXISTS #T3
			DROP TABLE IF EXISTS #T4
			DROP TABLE IF EXISTS #T5

			DROP TABLE IF EXISTS #TT
			DROP TABLE IF EXISTS #KONU
			
			DECLARE @GENEL INT = 999

			SELECT @ID_SINIF=ID_SINIF,@ID_SUBE=ID_SUBE FROM v3Ogrenci WHERE TCKIMLIKNO =@TC_OGRENCI		
			IF EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TC_OGRENCI)
			BEGIN
				SELECT  distinct 
					 SOT.TCKIMLIKNO
					,DO.AD+' '+DO.SOYAD ADSOYAD
					,K.AD AS SINIF
					--,SS.SUBEAD
					,'' AS SUBEAD
					,S.AD SINAVAD
					,CONVERT(VARCHAR, S.SINAVTARIH ,105) SINAVTARIH
					,(SELECT STUFF((SELECT   ',' +CAST(K.KITAPCIK AS VARCHAR)	FROM SinavOgrenci K WITH(NOLOCK) WHERE K.TCKIMLIKNO = SOT.TCKIMLIKNO and k.ID_SINAV = s.ID_SINAV FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '')) KITAPCIK
					,OP.PUAN
					,PT.KOD PUANTURU
					--,UPPER(ST.KISAAD+' SINAV SONUÇ BELGESİ') AS SINAVTURU
					,UPPER(S.RAPORBASLIK) AS SINAVTURU
					--,UPPER(ST.AD) AS SINAVTURUUZUN
					,'Testlerdeki Doğru ve Yanlış Sayıları' AS SINAVTURUUZUN
					,ST.ID_SINAVTURU
					,VD.ACIKLAMA DONEM
					,OP.SINIFSIRA,OP.OKULSIRA,OP.ILCESIRA,OP.ILSIRA,OP.GENELSIRA
					,S.PUANGIZLE
					,SINIFKATILIM	= SOT.KATILIM_SINIF
					,SUBEKATILIM	= SOT.KATILIM_OKUL
					,ILCEKATILIM	= SOT.KATILIM_ILCE
					,ILKATILIM		= SOT.KATILIM_IL
					,GENELKATILIM	= SOT.KATILIM_GENEL
					,ISNULL(PT.ID_SINAVPUANTURU,0) ID_SINAVPUANTURU
					,ID_KADEME = 0

				INTO #T11
				FROM SinavOgrenciToplam			SOT WITH(NOLOCK)
				LEFT JOIN vw_SinavOgrenciPuan	OP  WITH(NOLOCK)	ON	OP.TCKIMLIKNO		= SOT.TCKIMLIKNO AND OP.ID_SINAV = SOT.ID_SINAV
				LEFT JOIN SinavPuanTuru			PT  WITH(NOLOCK)	ON	PT.ID_SINAVPUANTURU	= OP.ID_SINAVPUANTURU
				JOIN Sinav						S   WITH(NOLOCK)	ON	S.ID_SINAV			= SOT.ID_SINAV
				JOIN SinavTuru					ST  WITH(NOLOCK)	ON	ST.ID_SINAVTURU		= S.ID_SINAVTURU
				JOIN DisOgrenci					DO  WITH(NOLOCK)	ON	DO.TCKIMLIKNO		= SOT.TCKIMLIKNO
				--JOIN vw_SubeSinifGrupKademeTum	SS  WITH(NOLOCK)	ON	SS.ID_KADEME3		= DO.ID_KADEME3
				JOIN v3Kademe3					K	WITH(NOLOCK)	ON	K.ID_KADEME3		= DO.ID_KADEME3
				JOIN v3AktifDonem				VD  WITH(NOLOCK)	ON  VD.DONEM			= S.DONEM
				WHERE	SOT.TCKIMLIKNO	= @TC_OGRENCI
					AND	SOT.ID_SINAV	= @ID_SINAV
			END
			ELSE
			BEGIN
				SELECT  distinct 
				 SOT.TCKIMLIKNO
				,VK.AD+' '+VK.SOYAD ADSOYAD
				,SINIF
				,SS.SUBEAD
				,S.AD SINAVAD
				,CONVERT(VARCHAR, S.SINAVTARIH ,105) SINAVTARIH
				,(SELECT STUFF((SELECT   ',' +CAST(K.KITAPCIK AS VARCHAR)	FROM SinavOgrenci K WITH(NOLOCK) WHERE K.TCKIMLIKNO = SOT.TCKIMLIKNO and k.ID_SINAV = s.ID_SINAV FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '')) KITAPCIK
				,OP.PUAN
				,PT.KOD PUANTURU
				--,UPPER(ST.KISAAD+' SINAV SONUÇ BELGESİ') AS SINAVTURU
				,UPPER(S.RAPORBASLIK) AS SINAVTURU
				--,UPPER(ST.AD) AS SINAVTURUUZUN
				,'Testlerdeki Doğru ve Yanlış Sayıları' AS SINAVTURUUZUN
				,ST.ID_SINAVTURU
				,VD.ACIKLAMA DONEM
				,OP.SINIFSIRA,OP.OKULSIRA,OP.ILCESIRA,OP.ILSIRA,OP.GENELSIRA
				,S.PUANGIZLE
				,SINIFKATILIM	= SOT.KATILIM_SINIF
				,SUBEKATILIM	= SOT.KATILIM_OKUL
				,ILCEKATILIM	= SOT.KATILIM_ILCE
				,ILKATILIM		= SOT.KATILIM_IL
				,GENELKATILIM	= SOT.KATILIM_GENEL
				,ISNULL(PT.ID_SINAVPUANTURU,0) ID_SINAVPUANTURU
				,ID_KADEME = SS.ID_KADEME
			INTO #T1
			FROM SinavOgrenciToplam			SOT WITH(NOLOCK)
			LEFT JOIN vw_SinavOgrenciPuan	OP  WITH(NOLOCK)	ON	OP.TCKIMLIKNO		= SOT.TCKIMLIKNO AND OP.ID_SINAV = SOT.ID_SINAV
			LEFT JOIN SinavPuanTuru			PT  WITH(NOLOCK)	ON	PT.ID_SINAVPUANTURU	= OP.ID_SINAVPUANTURU
			JOIN Sinav						S   WITH(NOLOCK)	ON	S.ID_SINAV			= SOT.ID_SINAV
			JOIN SinavTuru					ST  WITH(NOLOCK)	ON	ST.ID_SINAVTURU		= S.ID_SINAVTURU
			JOIN v3Kullanici				VK  WITH(NOLOCK)	ON	VK.TCKIMLIKNO		= SOT.TCKIMLIKNO
			JOIN V3Ogrenci					VO  WITH(NOLOCK)	ON	VO.TCKIMLIKNO		= SOT.TCKIMLIKNO
			JOIN vw_SubeSinifGrupKademeTum	SS  WITH(NOLOCK)	ON	SS.ID_SINIF			= VO.ID_SINIF
			JOIN v3AktifDonem				VD  WITH(NOLOCK)	ON  VD.DONEM			= S.DONEM
			WHERE	SOT.TCKIMLIKNO	= @TC_OGRENCI
				AND	SOT.ID_SINAV	= @ID_SINAV
			END
			SELECT 
				 DERSAD					= SD.TAKMAAD
				,TOPLAMSORU				= (SELECT SUM(DOGRU+YANLIS+BOS) FROM vw_SinavOgrenciBolum SB WITH(NOLOCK) WHERE SB.TCKIMLIKNO = B.TCKIMLIKNO	AND SB.ID_SINAVDERS = B.ID_SINAVDERS )
				,DOGRU					= B.DOGRU
				,YANLIS					= B.YANLIS
				,BOS					= B.BOS
				,NET					= B.NET
				,PUAN					= B.PUAN
				,OLCEKSINAVI			= S.OLCEKSINAVI
				,TCKIMLIKNO				= B.TCKIMLIKNO
				,BOLUMNO				= SD.BOLUMNO
				,YUZDENET				= B.YUZDENET
			
				,SINIFNETORTALAMA		= B.NET_SINIFORT
				,SINIFDOGRUORTALAMA		= B.DOGRU_SINIFORT 
				,SINIFYUZDENETORTALAMA	= B.YUZDENET_SINIFORT

				,SUBENETORTALAMA		= B.NET_OKULORT

				,GENELDOGRUORTALAMA		= B.DOGRU_GENELORT
				,GENELNETORTALAMA		= B.NET_GENELORT
				,GENELYUZDENETORTALAMA	= B.YUZDENET_GENELORT
				,YUZDEBASARI			= YUZDENET 
				
				,SINIFSIRA				= B.NET_SINIFSIRA
				,OKULSIRA				= B.NET_OKULSIRA
				,ILCESIRA				= B.NET_ILCESIRA
				,ILSIRA					= B.NET_ILSIRA
				,GENELSIRA				= B.NET_GENELSIRA
			
				,B.ID_SINAV
			INTO #T2 
			FROM vw_SinavOgrenciBolum	B  WITH(NOLOCK)
			JOIN SinavOgrenciToplam		T  WITH(NOLOCK)	ON	T.TCKIMLIKNO		= B.TCKIMLIKNO 
														AND T.ID_SINAV			= B.ID_SINAV
			JOIN Sinav					S  WITH(NOLOCK)	ON	S.ID_SINAV			= B.ID_SINAV
			JOIN SinavDers				SD WITH(NOLOCK)	ON	SD.ID_SINAVDERS		= B.ID_SINAVDERS
			JOIN v3SinavDers			D  WITH(NOLOCK)	ON	D.ID_DERS			= SD.ID_DERS		
			WHERE	B.ID_SINAV	= @ID_SINAV
				AND B.TCKIMLIKNO= @TC_OGRENCI
	
			INSERT INTO #T2 				
				SELECT 
				 'TOPLAM'					
				,sum(TOPLAMSORU)
				,ST.TD
				,ST.TY
				,ST.TB
				,ST.TN 
				,0 PUAN					
				,T.OLCEKSINAVI			
				,T.TCKIMLIKNO				
				,@GENEL BOLUMNO				
				,cast((TN/(TD+TY+TB)*100.0) as decimal(8,2))		
				
				,cast(avg(SINIFNETORTALAMA		 ) as decimal(8,2))	
				,cast(avg(SINIFDOGRUORTALAMA	) as decimal(8,2))	
				,cast(avg(SINIFYUZDENETORTALAMA	) as decimal(8,2))				
				,cast(avg(SUBENETORTALAMA		) as decimal(8,2))				
				,cast(avg(GENELDOGRUORTALAMA	) as decimal(8,2))	
				,cast(avg(GENELNETORTALAMA		 ) as decimal(8,2))	
				,cast(avg(GENELYUZDENETORTALAMA	 ) as decimal(8,2))	
				,cast((TN/(TD+TY+TB)*100.0) as decimal(8,2))	
				
				,ST.TN_SINIFSIRA				
				,ST.TN_OKULSIRA				
				,ST.TN_ILCESIRA				
				,ST.TN_ILSIRA					
				,ST.TN_GENELSIRA				
				,ST.ID_SINAV

				FROM #T2 T
				JOIN SinavOgrenciToplam	ST	ON	ST.ID_SINAV		= T.ID_SINAV
											AND ST.TCKIMLIKNO	= T.TCKIMLIKNO
				JOIN Sinav				S	ON	S.ID_SINAV		= T.ID_SINAV
				--JOIN v3KademeBilgi		KB	ON	KB.ID_KADEME3	= S.ID_KADEME3
				WHERE (SELECT TOP 1 ID_KADEME FROM OkyanusDB.dbo.v3KademeBilgi WHERE ID_KADEME3 = S.ID_KADEME3) = 4
				GROUP BY ST.TD
				,ST.TY
				,ST.TB
				,ST.TN 				
				,T.OLCEKSINAVI			
				,T.TCKIMLIKNO	
				,ST.TN_SINIFSIRA				
				,ST.TN_OKULSIRA				
				,ST.TN_ILCESIRA				
				,ST.TN_ILSIRA					
				,ST.TN_GENELSIRA
				,ST.ID_SINAV

			SELECT 
				 OS.SORUNO
				,OS.CEVAP OGRENCICEVAP
				,OS.DOGRUCEVAP
				,OS.DURUM
				,OS.ID_SINAVDERS
				,SD.TAKMAAD DERSAD
				,SD.BOLUMNO
			INTO #T3
			FROM vw_SinavOgrenciSoru	OS WITH(NOLOCK)
			JOIN SinavDers				SD WITH(NOLOCK)	ON	SD.ID_SINAVDERS	= OS.ID_SINAVDERS		
			WHERE	OS.TCKIMLIKNO	= @TC_OGRENCI
				AND OS.ID_SINAV		= @ID_SINAV
			ORDER BY SD.BOLUMNO,ID_SINAVDERS,SORUNO

			SELECT   
				DERSAD
				,BOLUMNO
				,OGRENCI	= YUZDENET 
				,SINIF		= SINIFYUZDENETORTALAMA
				,GENEL		= GENELYUZDENETORTALAMA
			INTO #T4
			FROM #T2	B
			WHERE BOLUMNO != @GENEL
			ORDER BY BOLUMNO
		
			SELECT	
					count(1) SORUSAYISI,
					SOS.DURUM,
					D.BOLUMNO,
					d.TAKMAAD DERSAD,
					isnull(u.KOD,'') KOD,
					isnull(u.AD,'') KONUAD,
					max(SB.YUZDEDOGRU) YUZDE,
					SOS.ID_SINAV
					,(SELECT STUFF((SELECT   ',' +CAST(K.KITAPCIK AS VARCHAR)	FROM SinavOgrenci K WITH(NOLOCK) WHERE K.TCKIMLIKNO = @TC_OGRENCI and k.ID_SINAV = SOS.ID_SINAV FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '')) KITAPCIK
			into #KONU
			from vw_SinavOgrenciSoru	SOS WITH(NOLOCK) 
			JOIN vw_SinavOgrenciBolum	SB  WITH(NOLOCK)	ON	SB.ID_SINAVDERS = SOS.ID_SINAVDERS AND SB.TCKIMLIKNO	= SOS.TCKIMLIKNO
			JOIN SinavDers				D   WITH(NOLOCK)	ON	D.ID_SINAVDERS	= SOS.ID_SINAVDERS AND D.ID_SINAV		= SOS.ID_SINAV
			JOIN v3SinavDersUnite		U	WITH(NOLOCK)	ON  U.KOD			= SOS.KOD
			WHERE  SOS.TCKIMLIKNO=@TC_OGRENCI AND SOS.ID_SINAV=@ID_SINAV
			GROUP BY SOS.DURUM,d.TAKMAAD,u.kod,u.AD,D.ID_SINAVDERS,SOS.ID_SINAV,D.BOLUMNO
			ORDER BY d.TAKMAAD

			SELECT DISTINCT
					KONUAD,
					DERSAD,
					BOLUMNO,
					KOD,						
					YUZDE AS BOLUMYUZDE
					,ISNULL([D],0) DOGRU,ISNULL([Y],0) YANLIS,ISNULL([B],0) BOS,ID_SINAV,
					ISNULL( 
								CAST((((	100 	/ NULLIF( 	
									CAST((		 
										CAST(ISNULL([D],0) AS INT)
										+CAST(ISNULL([Y],0) AS INT)
										+CAST(ISNULL([B],0) AS INT)) AS DECIMAL(11,2) ) 
											,0)
									))
									* CAST([D] AS INT)  
								) AS DECIMAL(11,2)) 
						,0) AS YUZDE
						,KITAPCIK
			INTO #TT
			FROM ( SELECT * FROM #KONU  )AS GTABLO
			PIVOT ( SUM(SORUSAYISI) FOR DURUM IN ([D],[Y],[B]) ) AS p
			order by ID_SINAV,DERSAD,KOD

			SELECT	
				KONUAD,
				BOLUMNO,
				DERSAD,
				KOD,
				CAST(AVG(BOLUMYUZDE)AS DECIMAL(11,2)) BOLUMYUZDE,
				SUM(DOGRU+YANLIS+BOS) SORU,
				SUM(DOGRU) DOGRU,
				SUM(YANLIS) YANLIS,
				SUM(BOS) BOS,
				CAST(AVG(YUZDE)AS DECIMAL(11,2)) YUZDE,
				KITAPCIK
			INTO	#T5
			FROM	#TT
			GROUP BY KOD,KONUAD,DERSAD,BOLUMNO,KITAPCIK
		union 
			SELECT  DERSAD,
					BOLUMNO,
					DERSAD,
					'',
					CAST(AVG(BOLUMYUZDE)AS DECIMAL(11,2)) BOLUMYUZDE,
					SUM(DOGRU+YANLIS+BOS) SORU,
					SUM(DOGRU) DOGRU,
					SUM(YANLIS) YANLIS,
					SUM(BOS) BOS,
					CAST(AVG(YUZDE)AS DECIMAL(11,2)) YUZDE ,
					KITAPCIK
			FROM	#TT
			GROUP BY DERSAD,BOLUMNO,KITAPCIK
			order by BOLUMNO,kod


			IF @ISLEM = 1	--JSON SEND
			BEGIN
				select(
					select * from(
						select 
						(						 
							SELECT * FROM #T1
							ORDER BY ID_SINAVPUANTURU
							FOR JSON AUTO
						) as t1
						,( 
							SELECT * FROM #T2
							ORDER BY BOLUMNO
							FOR JSON AUTO
						)as t2
						,(
							SELECT * 				
								,MAXSORU=(SELECT MAX (SORUNO) FROM #T3)
								,DERSSAYISI=(SELECT COUNT (DISTINCT BOLUMNO) FROM #T3) 
							FROM #T3
							ORDER BY BOLUMNO,ID_SINAVDERS,SORUNO
							FOR JSON AUTO
						) as t3 
						,( 
								
							SELECT * FROM #T4
							ORDER BY BOLUMNO					
							FOR JSON AUTO
						)as t4
						,(
							SELECT * FROM #T5
							ORDER BY BOLUMNO,DERSAD,KOD
							FOR JSON AUTO
						) as t5 
					) as tablo FOR JSON AUTO
				) as tt
			END
			IF @ISLEM = 2	--DATASET SEND
			BEGIN
			IF EXISTS(SELECT TCKIMLIKNO FROM DisOgrenci WHERE TCKIMLIKNO=@TC_OGRENCI)
			BEGIN
				SELECT * FROM #T11
				ORDER BY ID_SINAVPUANTURU
			END
			ELSE
			BEGIN
			SELECT * FROM #T1
				ORDER BY ID_SINAVPUANTURU
			END
				SELECT * FROM #T2
				ORDER BY BOLUMNO

				SELECT * 				
					,MAXSORU=(SELECT MAX (SORUNO) FROM #T3)
					,DERSSAYISI=(SELECT COUNT (DISTINCT BOLUMNO) FROM #T3) 
				FROM #T3
				ORDER BY BOLUMNO,ID_SINAVDERS,SORUNO
								
				SELECT * FROM #T4
				ORDER BY BOLUMNO
								
				SELECT * FROM #T5
				ORDER BY BOLUMNO,DERSAD,KOD


			END

			
			 
			
			DROP TABLE IF EXISTS #T1
			DROP TABLE IF EXISTS #T2
			DROP TABLE IF EXISTS #T3
			DROP TABLE IF EXISTS #T4
			DROP TABLE IF EXISTS #T5

			DROP TABLE IF EXISTS #TT
			DROP TABLE IF EXISTS #KONU
		END

		IF @ISLEM = 3   --ONLİNE TEST RAPOR
		BEGIN
			SELECT ISNULL((
						SELECT DISTINCT
						TCKIMLIKNO  = O.TCKIMLIKNO
						,AD			=K.AD 
						,SOYAD		=K.SOYAD
						,DOGRU		= OTOS.TD
						,YANLIS		= OTOS.TY
						,BOS		= OTOS.TB
						FROM Sinav						S
						JOIN OnlineTestOgrenciSonuc		OTOS ON  OTOS.ID_SINAV	 = S.ID_SINAV
						JOIN v3Ogrenci					O	 ON O.TCKIMLIKNO = OTOS.TCKIMLIKNO
						JOIN v3Kullanici				K	 ON  K.TCKIMLIKNO    = O.TCKIMLIKNO	
						WHERE S.ID_SINAV = @ID_SINAV AND S.DONEM = @DONEM  AND O.ID_SUBE = @ID_SUBE-- AND O.ID_SINIF =@ID_SINIF
				FOR JSON PATH
				),'[]')
			END

			IF @ISLEM = 4 -- ONLİNE TEST SINAV LİSTELE
			BEGIN
				SELECT ISNULL((
					SELECT 
					S.AD
					,S.ID_SINAV
					FROM Sinav				S
					WHERE S.ID_SINAVTURU  = 15 AND ID_KADEME3 = @ID_KADEME3
				FOR JSON PATH
				),'[]')
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
			END CATCH;
	
	END