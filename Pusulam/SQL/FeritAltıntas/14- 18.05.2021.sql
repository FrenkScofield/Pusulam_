

GO
CREATE TABLE [dbo].[OgretmenDegerlendirme] (
    [ID_OGRETMENDEGERLENDIRME] INT          IDENTITY (1, 1) NOT NULL,
    [TC_MUDUR]                 VARCHAR (11) NOT NULL,
    [TC_OGRETMEN]              VARCHAR (11) NOT NULL,
    [ID_KATEGORI]              INT          NOT NULL,
    [ID_SUBE]                  INT          NOT NULL,
    [ID_PERIYOT]               INT          NOT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirme] PRIMARY KEY CLUSTERED ([ID_OGRETMENDEGERLENDIRME] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmeCevap]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmeCevap] (
    [ID_OGRETMENDEGERLENDIRMECEVAP] INT IDENTITY (1, 1) NOT NULL,
    [ID_SORU]                       INT NOT NULL,
    [ID_OGRETMENDEGERLENDIRME]      INT NOT NULL,
    [GUCLUYON]                      BIT NOT NULL,
    [GELISMEALANI]                  BIT NOT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirmeCevap] PRIMARY KEY CLUSTERED ([ID_OGRETMENDEGERLENDIRMECEVAP] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmeKategori]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmeKategori] (
    [ID_DEGERLENDIRMEKATEGORITANIM] INT          IDENTITY (1, 1) NOT NULL,
    [KATEGORI]                      VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirmeKategori] PRIMARY KEY CLUSTERED ([ID_DEGERLENDIRMEKATEGORITANIM] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmeKategoriCevap]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmeKategoriCevap] (
    [ID_OGRETMENDEGERLENDIRMEKATEGORICEVAP]     INT IDENTITY (1, 1) NOT NULL,
    [ID_SORU]                                   INT NOT NULL,
    [ID_OGRETMENDEGERLENDIRME]                  INT NOT NULL,
    [ID_OGRETMENDEGERLENDIRMEKATEGORISORUCEVAP] INT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirmeKategoriCevap] PRIMARY KEY CLUSTERED ([ID_OGRETMENDEGERLENDIRMEKATEGORICEVAP] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmeKategoriSoru]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmeKategoriSoru] (
    [ID_OGRETMENDEGERLENDIRMEKATEGORISORU] INT           IDENTITY (1, 1) NOT NULL,
    [ID_KATEGORI]                          INT           NOT NULL,
    [SORU]                                 VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirmeKategoriSoru] PRIMARY KEY CLUSTERED ([ID_OGRETMENDEGERLENDIRMEKATEGORISORU] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmePeriyot]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmePeriyot] (
    [ID_DEGERLERDIRMEPERIYOD] INT         IDENTITY (1, 1) NOT NULL,
    [DONEM]                   VARCHAR (4) NOT NULL,
    [BASLANGICTARIHI]         DATE        NOT NULL,
    [BITISTARIHI]             DATE        NOT NULL,
    CONSTRAINT [PK_DegerlendirmePeriyod] PRIMARY KEY CLUSTERED ([ID_DEGERLERDIRMEPERIYOD] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenDegerlendirmeSoru]...';


GO
CREATE TABLE [dbo].[OgretmenDegerlendirmeSoru] (
    [ID_SORU] INT           IDENTITY (1, 1) NOT NULL,
    [GRUPADI] VARCHAR (250) NOT NULL,
    [SORU]    VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_OgretmenDegerlendirmeSoru] PRIMARY KEY CLUSTERED ([ID_SORU] ASC)
);


GO
PRINT N'Creating [dbo].[OgretmenKategoriSoruCevap]...';


GO
CREATE TABLE [dbo].[OgretmenKategoriSoruCevap] (
    [ID_OGRETMENKATEGORISORUCEVAP] INT        IDENTITY (1, 1) NOT NULL,
    [ID_SORU]                      INT        NOT NULL,
    [CEVAP]                        NCHAR (10) NOT NULL,
    CONSTRAINT [PK_OgretmenKategoriSoruCevap] PRIMARY KEY CLUSTERED ([ID_OGRETMENKATEGORISORUCEVAP] ASC)
);


GO
PRINT N'Creating [dbo].[sp_Performans]...';


GO
CREATE PROC [dbo].[sp_Performans]
	@ISLEM							INT				= NULL,
	@TCKIMLIKNO						VARCHAR(11)		= NULL,
	@OTURUM							VARCHAR(36)		= NULL,
	@ID_MENU						INT				= NULL,
	@SQLJSON						VARCHAR(MAX)	= NULL,
	@DONEM							VARCHAR(4)		= NULL,
	@BASLANGICTARIH					VARCHAR(MAX)	= NULL,
	@BITISTARIH						VARCHAR(MAX)	= NULL,
	@ID_OGRETMENPERIYOTTANIMLAMA	INT				= NULL,
	@PERFORMANSCEVAP				VARCHAR(MAX)	= NULL,
	--@KATEGORISORU					VARCHAR(MAX)	= NULL,
	@ID_KATEGORI					INT				= NULL,
	@ID_OGRETMEN					INT				= NULL,
	@ID_PERIYOT						INT				= NULL,
	@TC_OGRETMEN					VARCHAR(11)		= NULL,
	@ID_SUBE						INT				= NULL,
	@ID_OGRETMENDEGERLENDIRME		INT				= NULL,
	@KATEGORISORU					INT				= NULL,
    @ID_KULLANICITIPI				INT				= NULL
AS
BEGIN

	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	
	DECLARE @_ID_LOG INT = 0

	BEGIN TRY    
		DECLARE @_DESTEK_KONTROL INT

			IF @DONEM IS NULL OR @DONEM=''
			BEGIN
				SELECT @DONEM= DONEM FROM v3AktifDonem WHERE AKTIF=1
			END
			IF @ISLEM = 0	--	Dönem Listele
			BEGIN
				SELECT * FROM [dbo].[v3AktifDonem] WHERE AKTIF = 1
			END
			IF @ISLEM = 1  -- Periyot Tanımlama
			BEGIN
				DECLARE @PERIYOT INT = NULL
				SELECT @PERIYOT=COUNT(ID_DEGERLERDIRMEPERIYOD)  FROM dbo.OgretmenDegerlendirmePeriyot WHERE DONEM = @DONEM

				IF @PERIYOT >= 2
				BEGIN
					RETURN;
				END
				ELSE
				BEGIN
				    INSERT INTO dbo.OgretmenDegerlendirmePeriyot
				(
				    DONEM,
				    BASLANGICTARIHI,
				    BITISTARIHI
				)
				VALUES
				(   @DONEM,
					CONVERT(date, @BASLANGICTARIH, 104), 
				    CONVERT(date, @BITISTARIH, 104)  
				 )
				 
				SELECT
					ISNULL(
					(
						SELECT 
							 ID_DEGERLERDIRMEPERIYOD
							,DONEM  = (SELECT ACIKLAMA FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							,BASLANGICTARIHI 
							,BITISTARIHI
						FROM dbo.OgretmenDegerlendirmePeriyot 
						WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
						ORDER BY BASLANGICTARIHI 
						FOR JSON PATH
				), '[]')
				END				

			END

			IF @ISLEM = 2  -- Periyot Tanım Listele
			BEGIN
				SELECT
					ISNULL(
					(
						SELECT 
							 ID_DEGERLERDIRMEPERIYOD
							,DONEM  = (SELECT ACIKLAMA FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							,BASLANGICTARIHI 
							,BITISTARIHI
						FROM dbo.OgretmenDegerlendirmePeriyot 
						WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
						ORDER BY BASLANGICTARIHI 
						FOR JSON PATH
				), '[]')
			END

			IF @ISLEM = 3  -- Periyot Tanım Silme
			BEGIN
				DECLARE @KONTROL	INT = NULL
				SELECT @KONTROL =COUNT(ID_OGRETMENDEGERLENDIRME) FROM dbo.OgretmenDegerlendirme WHERE ID_PERIYOT = @ID_OGRETMENPERIYOTTANIMLAMA
				IF	@KONTROL > 0
				BEGIN
					RETURN;
				END
				ELSE
				BEGIN
					DELETE FROM dbo.OgretmenDegerlendirmePeriyot WHERE ID_DEGERLERDIRMEPERIYOD = @ID_OGRETMENPERIYOTTANIMLAMA
				END
			END
			
			IF @ISLEM = 4  -- Müdür Öğretmen Listesi
			BEGIN
				SET @ID_KULLANICITIPI	= 53

				DROP TABLE IF EXISTS  #ID_SINIF

				CREATE TABLE #ID_SINIF(ID_SINIF INT)

				INSERT INTO #ID_SINIF(ID_SINIF)
				SELECT 
					SR.ID_SINIF
				FROM OkyanusDB.dbo.v3SubeYetki SY
				JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = SY.TCKIMLIKNO AND K.AKTIF = 1
				JOIN OkyanusDB.dbo.v3SubeGrupSinif S ON S.ID_SUBE = SY.ID_SUBE
				LEFT JOIN OkyanusDB.dbo.v3SinifPersonel SR ON SR.ID_SINIF = S.ID_SINIF AND SR.TCKIMLIKNO = SY.TCKIMLIKNO AND SR.ID_KULLANICITIPI = @ID_KULLANICITIPI
				WHERE  SR.TCKIMLIKNO = @TCKIMLIKNO

				
					SELECT DISTINCT 
							   do.ID_OGRETMEN,
							   pb.AD+' '+pb.SOYAD as OGRETMENAD
							  -- d.DERSAD					   
						  FROM eokul_v2..DersOgretmen do
					 LEFT JOIN OkyanusDB.dbo.v3Kullanici pb on pb.TCKIMLIKNO=do.TC_OGRETMEN
					 INNER JOIN eokul_v2.dbo.Ders d on d.ID_DERS=do.ID_DERS
					 INNER JOIN OkyanusDB.dbo.v3Ogretmen O ON O.ID_OGRETMEN = do.ID_OGRETMEN
					 LEFT JOIN OKYANUSDB.DBO.V3EgitimTuru e on e.ID_EGITIMTURU=do.ID_EGITIMTURU
					 JOIN #ID_SINIF						  S	ON S.ID_SINIF = do.ID_SINIF
	   					 WHERE  DONEMKOD=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
						   AND d.AKTIF=1
					  ORDER BY OGRETMENAD
			END

			IF @ISLEM = 5 -- Aktif Dönem Periyot Listele
			BEGIN
				SELECT
					 ID_DEGERLERDIRMEPERIYOD
					,TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
				FROM dbo.OgretmenDegerlendirmePeriyot 
				WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
				AND BASLANGICTARIHI <= GETDATE()  AND BITISTARIHI >= GETDATE()
				ORDER BY BASLANGICTARIHI 					
				
			END
			
			IF @ISLEM = 6 -- Kategori Listele
			BEGIN
				SELECT					 
					 ID_DEGERLENDIRMEKATEGORITANIM
					,KATEGORI
				FROM  dbo.OgretmenDegerlendirmeKategori	
				ORDER BY ID_DEGERLENDIRMEKATEGORITANIM		
			END

			IF @ISLEM = 7 -- Performans Soru
			BEGIN
				DECLARE @DEGERCEVAP BIT = 0

				SELECT @TC_OGRETMEN = TCKIMLIKNO FROM OkyanusDB.dbo.v3Ogretmen WHERE ID_OGRETMEN = @ID_OGRETMEN

				SELECT @DEGERCEVAP = IIF(ID_OGRETMENDEGERLENDIRME > 0  ,  1, 0) 
				FROM dbo.OgretmenDegerlendirme 
				WHERE TC_MUDUR = @TCKIMLIKNO AND TC_OGRETMEN = @TC_OGRETMEN


				IF @DEGERCEVAP = 1
				BEGIN
					SELECT
						ISNULL(
						(
							SELECT 
								 ODC.ID_SORU
								,ODS.GRUPADI
								,ODS.SORU
								,GUCLUCEVAP			= IIF(ODC.GUCLUYON > 0  ,  1, 0)
								,GELISTIRMECEVAP	= IIF(ODC.GELISMEALANI > 0  ,  1, 0)
							FROM dbo.OgretmenDegerlendirme			OD 
							JOIN dbo.OgretmenDegerlendirmeCevap		ODC ON ODC.ID_OGRETMENDEGERLENDIRME = OD.ID_OGRETMENDEGERLENDIRME
							JOIN dbo.OgretmenDegerlendirmeSoru		ODS ON ODS.ID_SORU					= ODC.ID_SORU
							WHERE TC_MUDUR = @TCKIMLIKNO AND TC_OGRETMEN = @TC_OGRETMEN
							FOR JSON PATH
					), '[]')
				END
				ELSE
				BEGIN
					SELECT
						ISNULL(
						(
							SELECT 
								 ID_SORU
								,GRUPADI
								,SORU
								,GUCLUCEVAP = CAST(0 AS BIT)
								,GELISTIRMECEVAP = CAST(0 AS BIT)
							FROM  dbo.OgretmenDegerlendirmeSoru  
							FOR JSON PATH
					), '[]')
				END
			END

			IF @ISLEM = 8 -- Kategori Soru
			BEGIN
				DECLARE @ID_DEGERLENDRIME INT = NULL
				SELECT @ID_DEGERLENDRIME = ID_OGRETMENDEGERLENDIRME
				FROM dbo.OgretmenDegerlendirme	
				WHERE TC_MUDUR = @TCKIMLIKNO AND ID_PERIYOT = @ID_PERIYOT AND TC_OGRETMEN =@TC_OGRETMEN

				IF @ID_DEGERLENDRIME >0
				BEGIN
					SELECT
					ISNULL(
					(
						SELECT 
							 ID_OGRETMENDEGERLENDIRMEKATEGORISORU
							,ID_KATEGORI							
							,SORU
							,SECILI_KATEGORI_CEVAP = ODKC.ID_SORU
						FROM  dbo.OgretmenDegerlendirmeKategoriSoru		ODKS
						JOIN dbo.OgretmenDegerlendirmeKategoriCevap		ODKC ON ODKC.ID_SORU= ODKS.ID_OGRETMENDEGERLENDIRMEKATEGORISORU				
						WHERE ODKC.ID_OGRETMENDEGERLENDIRME = @ID_DEGERLENDRIME
						FOR JSON PATH
					), '[]')
				END

				ELSE
				BEGIN
					SELECT
					ISNULL(
					(
						SELECT 
							 ID_OGRETMENDEGERLENDIRMEKATEGORISORU
							,ID_KATEGORI							
							,SORU
							,SECILI_KATEGORI_CEVAP = 0
						FROM  dbo.OgretmenDegerlendirmeKategoriSoru
						FOR JSON PATH
				), '[]')
				END
			    
			END

			IF @ISLEM = 9 -- Değerlendirme Cevap Kaydet
			BEGIN
				DECLARE @DEGER INT = 0

				SELECT @TC_OGRETMEN = TCKIMLIKNO FROM OkyanusDB.dbo.v3Ogretmen WHERE ID_OGRETMEN = @ID_OGRETMEN

				SELECT @DEGER = IIF(ID_OGRETMENDEGERLENDIRME > 0  ,  ID_OGRETMENDEGERLENDIRME, 0) 
				FROM dbo.OgretmenDegerlendirme 
				WHERE TC_MUDUR = @TCKIMLIKNO AND TC_OGRETMEN = @TC_OGRETMEN AND ID_PERIYOT = @ID_PERIYOT

				IF @DEGER >0
				BEGIN
					DELETE FROM dbo.OgretmenDegerlendirmeCevap WHERE ID_OGRETMENDEGERLENDIRME = @DEGER
					
					UPDATE dbo.OgretmenDegerlendirme SET ID_KATEGORI = @ID_KATEGORI WHERE ID_OGRETMENDEGERLENDIRME = @DEGER
					INSERT INTO dbo.OgretmenDegerlendirmeCevap
					(
						ID_SORU,
						ID_OGRETMENDEGERLENDIRME,
						GUCLUYON,
						GELISMEALANI
					)

					SELECT ID_SORU, @DEGER, GUCLUYON, GELISMEALANI FROM OPENJSON(@PERFORMANSCEVAP)
					WITH(
						 ID_SORU			INT				'$.ID_SORU' 
						,GUCLUYON			BIT				'$.GUCLUCEVAP' 
						,GELISMEALANI		BIT				'$.GELISTIRMECEVAP' 
					)

					IF @ID_KATEGORI = 3
					BEGIN
						DELETE FROM OgretmenDegerlendirmeKategoriCevap WHERE ID_OGRETMENDEGERLENDIRME = @DEGER
						INSERT dbo.OgretmenDegerlendirmeKategoriCevap
						(
							ID_SORU,
							ID_OGRETMENDEGERLENDIRME							
						)
						VALUES 
						(
							@KATEGORISORU,
							@DEGER
						)
					END
				END
				ELSE
				BEGIN
					DECLARE @ID table(ID int)

					SELECT @TC_OGRETMEN = TCKIMLIKNO FROM OkyanusDB.dbo.v3Ogretmen WHERE ID_OGRETMEN = @ID_OGRETMEN
					SELECT * FROM dbo.OgretmenDegerlendirmeKategori WHERE ID_DEGERLENDIRMEKATEGORITANIM = @ID_KATEGORI

								SELECT TOP 1
									@ID_SUBE = S.ID_SUBE
									FROM OkyanusDB.dbo.v3SubeYetki SY
									JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = SY.TCKIMLIKNO AND K.AKTIF = 1
									JOIN OkyanusDB.dbo.v3Sube S ON S.ID_SUBE = SY.ID_SUBE
									WHERE K.TCKIMLIKNO = @TCKIMLIKNO
					INSERT INTO dbo.OgretmenDegerlendirme
					(
						TC_MUDUR,
						TC_OGRETMEN,
						ID_KATEGORI,
						ID_SUBE,
						ID_PERIYOT
					)
					OUTPUT inserted.ID_OGRETMENDEGERLENDIRME into @ID 
					VALUES
					(   @TCKIMLIKNO,                 
						@TC_OGRETMEN,                
						@ID_KATEGORI,                
						@ID_SUBE,                    
						@ID_PERIYOT					
						)
					select @ID_OGRETMENDEGERLENDIRME=ID from @ID


					INSERT INTO dbo.OgretmenDegerlendirmeCevap
					(
						ID_SORU,
						ID_OGRETMENDEGERLENDIRME,
						GUCLUYON,
						GELISMEALANI
					)

					SELECT ID_SORU, @ID_OGRETMENDEGERLENDIRME, GUCLUYON, GELISMEALANI FROM OPENJSON(@PERFORMANSCEVAP)
					WITH(
						 ID_SORU			INT				'$.ID_SORU' 
						,GUCLUYON			BIT				'$.GUCLUCEVAP' 
						,GELISMEALANI		BIT				'$.GELISTIRMECEVAP' 
					)
					IF @ID_KATEGORI = 3
					BEGIN
						INSERT dbo.OgretmenDegerlendirmeKategoriCevap
						(
							ID_SORU,
							ID_OGRETMENDEGERLENDIRME							
						)
						VALUES 
						(
							@KATEGORISORU,
							@ID_OGRETMENDEGERLENDIRME
						)
					END
				END
 			END

			IF @ISLEM = 10 -- Değerlendirilenler 
			BEGIN
				SELECT TOP 1 
					@ID_KULLANICITIPI=ID_KULLANICITIPI
				FROM OkyanusDB..vw_KullaniciYetki WHERE TCKIMLIKNO=@TCKIMLIKNO AND ID_KULLANICITIPI=1
				IF @ID_KULLANICITIPI = 1
				BEGIN
				    SELECT
					ISNULL(
					(
					 SELECT 
						 OGRETMEN = K.AD + ' ' + K.SOYAD
						,SUBE	  = S.AD
						,KATEGORI = ODK.KATEGORI
						,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
						,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)
					 FROM  dbo.OgretmenDegerlendirme			OD
					 JOIN  dbo.v3Kullanici						K   ON K.TCKIMLIKNO					     = OD.TC_OGRETMEN
					 JOIN  dbo.v3Sube							S   ON S.ID_SUBE						 = OD.ID_SUBE
					 JOIN  dbo.OgretmenDegerlendirmeKategori	ODK ON ODK.ID_DEGERLENDIRMEKATEGORITANIM = OD.ID_KATEGORI
					 JOIN  dbo.OgretmenDegerlendirmePeriyot		ODP ON ODP.ID_DEGERLERDIRMEPERIYOD		 = OD.ID_PERIYOT
					 WHERE  OD.ID_PERIYOT = @ID_PERIYOT
					 FOR JSON PATH
				), '[]')
				END
				ELSE
				BEGIN
				    SELECT
					ISNULL(
					(
					 SELECT 
						 OGRETMEN = K.AD + ' ' + K.SOYAD
						,SUBE	  = S.AD
						,KATEGORI = ODK.KATEGORI
						,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
						,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)
					 FROM  dbo.OgretmenDegerlendirme			OD
					 JOIN  dbo.v3Kullanici						K   ON K.TCKIMLIKNO					     = OD.TC_OGRETMEN
					 JOIN  dbo.v3Sube							S   ON S.ID_SUBE						 = OD.ID_SUBE
					 JOIN  dbo.OgretmenDegerlendirmeKategori	ODK ON ODK.ID_DEGERLENDIRMEKATEGORITANIM = OD.ID_KATEGORI
					 JOIN  dbo.OgretmenDegerlendirmePeriyot		ODP ON ODP.ID_DEGERLERDIRMEPERIYOD		 = OD.ID_PERIYOT
					 WHERE OD.TC_MUDUR = @TCKIMLIKNO AND OD.ID_PERIYOT = @ID_PERIYOT
					 FOR JSON PATH
				), '[]')
				END
				
			END

			IF @ISLEM = 11 -- Değerlendirilmeyenler
			BEGIN		
				SELECT TOP 1 
					@ID_KULLANICITIPI=ID_KULLANICITIPI
				FROM OkyanusDB..vw_KullaniciYetki WHERE TCKIMLIKNO=@TCKIMLIKNO AND ID_KULLANICITIPI=1
				IF @ID_KULLANICITIPI = 1
				BEGIN
					SELECT
						ISNULL(
						(
						SELECT DISTINCT 
							 OGRETMEN = pb.AD + ' ' + pb.SOYAD
							,SUBE	  = VS.AD
							--,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
							,TARIH	  = (SELECT
											TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
										FROM dbo.OgretmenDegerlendirmePeriyot 
										WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
										AND BASLANGICTARIHI <= GETDATE()  AND BITISTARIHI >= GETDATE()
										)
							--,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)									   
						 FROM eokul_v2..DersOgretmen do
						 LEFT JOIN OkyanusDB.dbo.v3Kullanici		pb ON pb.TCKIMLIKNO		 = do.TC_OGRETMEN
						 LEFT JOIN dbo.v3SubeYetki					SY ON SY.TCKIMLIKNO		 = pb.TCKIMLIKNO
						 LEFT JOIN dbo.v3Sube		 				VS ON VS.ID_SUBE		 = SY.ID_SUBE
						 --INNER JOIN eokul_v2.dbo.Ders			    d  ON d.ID_DERS			 = do.ID_DERS
						 INNER JOIN OkyanusDB.dbo.v3Ogretmen		O  ON O.ID_OGRETMEN		 = do.ID_OGRETMEN
						 LEFT JOIN OKYANUSDB.DBO.V3EgitimTuru	    e  ON e.ID_EGITIMTURU	 = do.ID_EGITIMTURU
	   					 WHERE  DONEMKOD=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							   AND DO.TC_OGRETMEN NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme OD WHERE OD.ID_PERIYOT=@ID_PERIYOT )
						  ORDER BY OGRETMEN
						  FOR JSON PATH
					), '[]')
				END
				ELSE
				BEGIN
					CREATE TABLE #DEGERLENDRILIMEYEN(ID_SINIF INT)

				INSERT INTO #DEGERLENDRILIMEYEN(ID_SINIF)
				SELECT 
					SR.ID_SINIF
				FROM OkyanusDB.dbo.v3SubeYetki SY
				JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = SY.TCKIMLIKNO AND K.AKTIF = 1
				JOIN OkyanusDB.dbo.v3SubeGrupSinif S ON S.ID_SUBE = SY.ID_SUBE
				LEFT JOIN OkyanusDB.dbo.v3SinifPersonel SR ON SR.ID_SINIF = S.ID_SINIF AND SR.TCKIMLIKNO = SY.TCKIMLIKNO AND SR.ID_KULLANICITIPI = 53
				WHERE  SR.TCKIMLIKNO = @TCKIMLIKNO AND SR.TCKIMLIKNO NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme WHERE TC_MUDUR=@TCKIMLIKNO )

				SELECT
						ISNULL(
						(
						 SELECT DISTINCT 
							 OGRETMEN = pb.AD + ' ' + pb.SOYAD
							,SUBE	  = VS.AD
							,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
							,TARIH	  = (SELECT
											TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
										FROM dbo.OgretmenDegerlendirmePeriyot 
										WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
										AND BASLANGICTARIHI <= GETDATE()  AND BITISTARIHI >= GETDATE()
										)
							--,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)									   
						 FROM eokul_v2..DersOgretmen do
						 LEFT JOIN OkyanusDB.dbo.v3Kullanici		pb ON pb.TCKIMLIKNO		 = do.TC_OGRETMEN
						 LEFT JOIN dbo.v3SubeYetki					SY ON SY.TCKIMLIKNO		 = @TCKIMLIKNO
						 LEFT JOIN dbo.v3Sube						VS ON VS.ID_SUBE		 = SY.ID_SUBE
						 INNER JOIN eokul_v2.dbo.Ders			    d  ON d.ID_DERS			 = do.ID_DERS
						 INNER JOIN OkyanusDB.dbo.v3Ogretmen		O  ON O.ID_OGRETMEN		 = do.ID_OGRETMEN
						 LEFT JOIN OKYANUSDB.DBO.V3EgitimTuru	    e  ON e.ID_EGITIMTURU	 = do.ID_EGITIMTURU
						 JOIN #DEGERLENDRILIMEYEN					s  ON s.ID_SINIF		 = do.ID_SINIF
	   						 WHERE  DONEMKOD=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							   AND d.AKTIF=1 AND DO.TC_OGRETMEN NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme OD WHERE OD.TC_MUDUR=@TCKIMLIKNO AND OD.ID_PERIYOT=@ID_PERIYOT )
						  ORDER BY OGRETMEN
						  FOR JSON PATH
					), '[]')
				END
				
				
					
			END

			IF @ISLEM = 12 -- Değerlendirilenler İndir
			BEGIN
				SELECT TOP 1 
					@ID_KULLANICITIPI=ID_KULLANICITIPI
				FROM OkyanusDB..vw_KullaniciYetki WHERE TCKIMLIKNO=@TCKIMLIKNO AND ID_KULLANICITIPI=1
				IF @ID_KULLANICITIPI = 1
				BEGIN
				    SELECT 
						 OGRETMEN = K.AD + ' ' + K.SOYAD
						,SUBE	  = S.AD
						,KATEGORI = ODK.KATEGORI
						,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
						,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)
					 FROM  dbo.OgretmenDegerlendirme			OD
					 JOIN  dbo.v3Kullanici						K   ON K.TCKIMLIKNO					     = OD.TC_OGRETMEN
					 JOIN  dbo.v3Sube							S   ON S.ID_SUBE						 = OD.ID_SUBE
					 JOIN  dbo.OgretmenDegerlendirmeKategori	ODK ON ODK.ID_DEGERLENDIRMEKATEGORITANIM = OD.ID_KATEGORI
					 JOIN  dbo.OgretmenDegerlendirmePeriyot		ODP ON ODP.ID_DEGERLERDIRMEPERIYOD		 = OD.ID_PERIYOT
					 WHERE OD.ID_PERIYOT = @ID_PERIYOT
				END
				ELSE
				BEGIN
				      SELECT 
						 OGRETMEN = K.AD + ' ' + K.SOYAD
						,SUBE	  = S.AD
						,KATEGORI = ODK.KATEGORI
						,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
						,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)
					 FROM  dbo.OgretmenDegerlendirme			OD
					 JOIN  dbo.v3Kullanici						K   ON K.TCKIMLIKNO					     = OD.TC_OGRETMEN
					 JOIN  dbo.v3Sube							S   ON S.ID_SUBE						 = OD.ID_SUBE
					 JOIN  dbo.OgretmenDegerlendirmeKategori	ODK ON ODK.ID_DEGERLENDIRMEKATEGORITANIM = OD.ID_KATEGORI
					 JOIN  dbo.OgretmenDegerlendirmePeriyot		ODP ON ODP.ID_DEGERLERDIRMEPERIYOD		 = OD.ID_PERIYOT
					 WHERE OD.TC_MUDUR = @TCKIMLIKNO AND OD.ID_PERIYOT = @ID_PERIYOT
				END
				
			END

			IF @ISLEM = 13 -- Değerlendirilmeyenler İndir
			BEGIN
				SELECT TOP 1 
					@ID_KULLANICITIPI=ID_KULLANICITIPI
				FROM OkyanusDB..vw_KullaniciYetki WHERE TCKIMLIKNO=@TCKIMLIKNO AND ID_KULLANICITIPI=1
				IF @ID_KULLANICITIPI = 1
				BEGIN
					 SELECT DISTINCT 
							 OGRETMEN = pb.AD + ' ' + pb.SOYAD
							,SUBE	  = VS.AD
							--,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
							,TARIH	  = (SELECT
											TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
										FROM dbo.OgretmenDegerlendirmePeriyot 
										WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
										AND BASLANGICTARIHI <= GETDATE()  AND BITISTARIHI >= GETDATE()
										)
							--,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)									   
						 FROM eokul_v2..DersOgretmen do
						 LEFT JOIN OkyanusDB.dbo.v3Kullanici		pb ON pb.TCKIMLIKNO		 = do.TC_OGRETMEN
						 LEFT JOIN dbo.v3SubeYetki					SY ON SY.TCKIMLIKNO		 = pb.TCKIMLIKNO
						 LEFT JOIN dbo.v3Sube		 				VS ON VS.ID_SUBE		 = SY.ID_SUBE
						 --INNER JOIN eokul_v2.dbo.Ders			    d  ON d.ID_DERS			 = do.ID_DERS
						 INNER JOIN OkyanusDB.dbo.v3Ogretmen		O  ON O.ID_OGRETMEN		 = do.ID_OGRETMEN
						 LEFT JOIN OKYANUSDB.DBO.V3EgitimTuru	    e  ON e.ID_EGITIMTURU	 = do.ID_EGITIMTURU
	   					 WHERE  DONEMKOD=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							   AND DO.TC_OGRETMEN NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme OD WHERE OD.ID_PERIYOT=@ID_PERIYOT )
						  ORDER BY OGRETMEN
				END
				ELSE
			    DROP TABLE IF EXISTS  #DEGERLENDRILIMEYEN1

				CREATE TABLE #DEGERLENDRILIMEYEN1(ID_SINIF INT)

				INSERT INTO #DEGERLENDRILIMEYEN1(ID_SINIF)
				SELECT 
					SR.ID_SINIF
				FROM OkyanusDB.dbo.v3SubeYetki SY
				JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = SY.TCKIMLIKNO AND K.AKTIF = 1
				JOIN OkyanusDB.dbo.v3SubeGrupSinif S ON S.ID_SUBE = SY.ID_SUBE
				LEFT JOIN OkyanusDB.dbo.v3SinifPersonel SR ON SR.ID_SINIF = S.ID_SINIF AND SR.TCKIMLIKNO = SY.TCKIMLIKNO AND SR.ID_KULLANICITIPI = 53
				WHERE  SR.TCKIMLIKNO = @TCKIMLIKNO AND SR.TCKIMLIKNO NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme WHERE TC_MUDUR=@TCKIMLIKNO )

				
				 SELECT DISTINCT 
					 OGRETMEN = pb.AD + ' ' + pb.SOYAD
					,SUBE	  = VS.AD
					,MUDUR    = (SELECT AD + ' ' + SOYAD FROM dbo.v3Kullanici WHERE TCKIMLIKNO = @TCKIMLIKNO) 
					,TARIH	  = (SELECT
									TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
								FROM dbo.OgretmenDegerlendirmePeriyot 
								WHERE  DONEM= (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
								AND BASLANGICTARIHI <= GETDATE()  AND BITISTARIHI >= GETDATE()
								)
					--,TARIH    = CONVERT(VARCHAR,ODP.BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,ODP.BITISTARIHI, 104)									   
				 FROM eokul_v2..DersOgretmen do
				 LEFT JOIN OkyanusDB.dbo.v3Kullanici		pb ON pb.TCKIMLIKNO		 = do.TC_OGRETMEN
				 LEFT JOIN dbo.v3SubeYetki					SY ON SY.TCKIMLIKNO		 = @TCKIMLIKNO
				 LEFT JOIN dbo.v3Sube						VS ON VS.ID_SUBE		 = SY.ID_SUBE
				 INNER JOIN eokul_v2.dbo.Ders			    d  ON d.ID_DERS			 = do.ID_DERS
				 INNER JOIN OkyanusDB.dbo.v3Ogretmen		O  ON O.ID_OGRETMEN		 = do.ID_OGRETMEN
				 LEFT JOIN OKYANUSDB.DBO.V3EgitimTuru	    e  ON e.ID_EGITIMTURU	 = do.ID_EGITIMTURU
				 JOIN #DEGERLENDRILIMEYEN1					s  ON s.ID_SINIF		 = do.ID_SINIF
	   				 WHERE  DONEMKOD=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)
							   AND d.AKTIF=1 AND DO.TC_OGRETMEN NOT IN(SELECT TC_OGRETMEN FROM dbo.OgretmenDegerlendirme OD WHERE OD.TC_MUDUR=@TCKIMLIKNO AND OD.ID_PERIYOT=@ID_PERIYOT )
				  ORDER BY OGRETMEN
						  
			END

			IF @ISLEM = 14 -- Periyot Tarih Listele
			BEGIN
				SELECT
					 ID_DEGERLERDIRMEPERIYOD
					,TARIH = CONVERT(VARCHAR,BASLANGICTARIHI, 104)  + ' - ' + CONVERT(VARCHAR,BITISTARIHI, 104)
				FROM dbo.OgretmenDegerlendirmePeriyot 				  
				ORDER BY BASLANGICTARIHI	
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
GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO


INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Dinleme: Karşısındakini çözüm odaklı, yargılamadan ve sorunu objektif olarak tanımlayabilecek şekilde dinleyebilme' 
)
	
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Ekip arkadaşları ile uyumlu çalışabilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Duygu kontrolü: Öfke, heyecan, korku, üzüntü gibi temel duygularını kontrol edebilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'İşbirliğine açık olma, yardımsever olma' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Geri bildirime, uyarı ve gelişitirme amaçlı yönlendirmelere açık olma' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Veli görüşmelerini yapıcı ve çözüm odaklı sürdürebilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Veli ve öğrenciye karşı özgüvenli duruş sergileyebilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Çatışma ve kriz durumlarını yönetebilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Verilen görevlerin ne olduğunu anlayabilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'İLETİŞİM BECERİLERİ',
    'Pedagojik ilkelere uygun davranabilme' 
)



INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Branşı ile ilgili kazanımları müfredattaki şekliyle öğrencinin seviyesine uygun olarak aktarabilme' 
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Sınıfı (psikologlar için ilgilendiği öğrenci ile görüşme sürecini) etkili şekilde yönetebilme' 
)	
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'İletişimde / derslerde farklı yöntem ve teknikleri kullanabilme'
)	
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Kendini kişisel ve mesleki olarak güncelleyebilme'
)	
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Branşı ile ilgili konularda öğrencilerin ilgi ve yeteneklerine göre geliştirilmesi gereken yönlerini fark edebilme'
)	
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Öğrencinin gelişime açık yönlerini ilgili branşlar ile paylaşma ve işbirliği yapma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Problemlerle ilgili doğru ve etkili çözüm yolları önerebilme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Problemleri doğru teşhis edebilme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Kullanımına sunulan salon, sınıf, akıllı tahta, laboratuvar, alet vb imkanları etkin kullanma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'AKADEMİK VE MESLEKİ BECERİLER',
    'Kazanım eksiklerini gidermeye yönelik çalışmalar yapma'
)


INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Verilen görevi en iyi şekilde yerine getirme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Aldığı işlerin takibini yapabilme.'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Kullanımına sunulan salon, sınıf, akıllı tahta, laboratuvar, alet vb imkanların korunmasını sağlama'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Enerjisini tüm öğrencilerinin gelişimi için eşit kullanma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Verdiği iş/ödev/proje vb çalışmaları zamanında kontrol etme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Öğrencinin gelişime açık yönlerini veli ile paylaşma ve gelişim için destek verme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Mesai saatlerine uygun davranma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Derslerine zamamında girme.'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Nöbet görevini yerine getirme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'SORUMLULUK',
    'Görev motivasyonunun yüksek olması: (zaman zaman konu hakkında negatif konuşmalar yapıyorsa ve isteksizliğini belli ediyorsa Gelişim Alanıdır)'
)

INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Kılık kıyafet konusunda kurum kültürüne uyumlu olma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Kurumun gelişimi için çaba gösterme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Çevresindeki çalışanların kurum hakkında pozitif düşüncelere sahip olmasını sağlama'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'İş ortamında / sosyal ortamda marka değerini destekleyecek söz, tutum ve davranışlarda bulunma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Veli aramalarını zamanında ve düzenli şekilde yapma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Sınıftaki tüm öğrencilerin kayıt dönüşümü için veliler ile görüşme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Öğretmen olarak Okyanus Kolejlerini temsil edebilme'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Velileri tarafından yetkin bir öğretmen olarak algılanma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Öğrenciler tarafından saygın bir öğretmen olarak algılanma'
)
INSERT INTO dbo.OgretmenDegerlendirmeSoru
(
    GRUPADI,
    SORU
)
VALUES
(   'KURUMSAL ÖĞELER',
    'Kurumda çalışyor olmaktan memnun olduğunu söz ya da davranışlarıyla ifade etme.'
)


INSERT INTO dbo.Menu
(
    AD,
    ACIKLAMA,
    KOD,
    URL,
    RESIM,
    GIZLI,
    YARDIMHTML,
    OZEL
)
VALUES
(   'Performans Geliştirme Sistemi',   -- AD - varchar(50)
    'Performans Geliştirme Sistemi',   -- ACIKLAMA - varchar(250)
    '088',   -- KOD - varchar(50)
    '',   -- URL - varchar(250)
    'icon-pencil',   -- RESIM - varchar(50)
    0, -- GIZLI - bit
    NULL,   -- YARDIMHTML - varchar(max)
    0  -- OZEL - bit
    )

	INSERT INTO dbo.Menu
(
    AD,
    ACIKLAMA,
    KOD,
    URL,
    RESIM,
    GIZLI,
    YARDIMHTML,
    OZEL
)
VALUES
(   'Periyot Tanımlama',   -- AD - varchar(50)
    'Periyot Tanımlama',   -- ACIKLAMA - varchar(250)
    '088001',   -- KOD - varchar(50)
    '#Performans/PeriyotTanimlama',   -- URL - varchar(250)
    'icon-plus',   -- RESIM - varchar(50)
    0, -- GIZLI - bit
    NULL,   -- YARDIMHTML - varchar(max)
    0  -- OZEL - bit
    )
	
	
	INSERT INTO	dbo.OgretmenDegerlendirmeKategori
(
    KATEGORI
)
VALUES
('A'
)
INSERT INTO	dbo.OgretmenDegerlendirmeKategori
(
    KATEGORI
)
VALUES
('B'
)
INSERT INTO	dbo.OgretmenDegerlendirmeKategori
(
    KATEGORI
)
VALUES
('C'
)
INSERT INTO	dbo.OgretmenDegerlendirmeKategori
(
    KATEGORI
)
VALUES
('Değerlendirme Dışı'
)

INSERT INTO dbo.OgretmenDegerlendirmeKategoriSoru
(
    ID_KATEGORI,
    SORU
)
VALUES
(   3, 
    'Kesinlikle gelişim gösterir' 
)
INSERT INTO dbo.OgretmenDegerlendirmeKategoriSoru
(
    ID_KATEGORI,
    SORU
)
VALUES
(   3, 
    'Yüksek İhtimalle gelişim Gösterir' 
)
INSERT INTO dbo.OgretmenDegerlendirmeKategoriSoru
(
    ID_KATEGORI,
    SORU
)
VALUES
(   3, 
    'Gelişim Gösterebilir' 
)
INSERT INTO dbo.OgretmenDegerlendirmeKategoriSoru
(
    ID_KATEGORI,
    SORU
)
VALUES
(   3, 
    'Çok zor da olsa gelişim gösterebilir' 
)
INSERT INTO dbo.OgretmenDegerlendirmeKategoriSoru
(
    ID_KATEGORI,
    SORU
)
VALUES
(   3, 
    'Hiçbir zaman gelişim göstermez' 
)
INSERT INTO	dbo.Menu
	(
	    AD,
	    ACIKLAMA,
	    KOD,
	    URL,
	    RESIM,
	    GIZLI,
	    YARDIMHTML,
	    OZEL
	)
	VALUES
	(   'Kategori Değerlendirme',   
	    'Kategori Değerlendirme',   
	    '088002',   
	    '#Performans/KategoriDegerlendirme',   
	    'icon-plus',   
	    0, 
	    null,   
	    0  
	    )

INSERT INTO	dbo.Menu
	(
	    AD,
	    ACIKLAMA,
	    KOD,
	    URL,
	    RESIM,
	    GIZLI,
	    YARDIMHTML,
	    OZEL
	)
	VALUES
	(   'Degerlendirilenler',   
	    'Degerlendirilenler',   
	    '088003',   
	    '#Performans/Degerlendirilenler',   
	    'icon-plus',   
	    0, 
	    null,   
	    0  
	    )
		INSERT INTO	dbo.Menu
	(
	    AD,
	    ACIKLAMA,
	    KOD,
	    URL,
	    RESIM,
	    GIZLI,
	    YARDIMHTML,
	    OZEL
	)
	VALUES
	(   'Degerlendirilmeyenler',   
	    'Degerlendirilmeyenler',   
	    '088004',   
	    '#Performans/Degerlendirilenlermeyen',   
	    'icon-plus',   
	    0, 
	    null,   
	    0  
	    )