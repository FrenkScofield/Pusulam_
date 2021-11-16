

GO
CREATE TABLE [dbo].[EOgrenmeBilgi] (
    [ID_EOGRENME]    INT          IDENTITY (1, 1) NOT NULL,
    [ID_KADEME3]     INT          NOT NULL,
    [ID_DERS]        INT          NOT NULL,
    [ID_KONU]        INT          NOT NULL,
    [DONEM]          VARCHAR (4)  NOT NULL,
    [KYE_TCKIMLIKNO] VARCHAR (11) NOT NULL,
    [KYE_TARIH]      DATE         NOT NULL,
    CONSTRAINT [PK_EOgrenmeBilgi] PRIMARY KEY CLUSTERED ([ID_EOGRENME] ASC)
);


GO
PRINT N'Creating Table [dbo].[EOgrenmeDetay]...';


GO
CREATE TABLE [dbo].[EOgrenmeDetay] (
    [ID_EOGRENMEDETAY] INT           IDENTITY (1, 1) NOT NULL,
    [ID_EOGRENMETUR]   INT           NOT NULL,
    [ID_EOGRENMEBILGI] INT           NOT NULL,
    [ID_SEVIYE]        INT           NOT NULL,
    [ACIKLAMA]         VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_EOgrenmeBilgiDetay] PRIMARY KEY CLUSTERED ([ID_EOGRENMEDETAY] ASC)
);


GO
PRINT N'Creating Table [dbo].[EOgrenmeDosya]...';


GO
CREATE TABLE [dbo].[EOgrenmeDosya] (
    [ID_EOGRENMEDOSYA] INT           IDENTITY (1, 1) NOT NULL,
    [ID_EOGRENMEDETAY] INT           NOT NULL,
    [DOSYA_AD]         VARCHAR (250) NOT NULL,
    [DOSYA_GUID]       VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_EOgrenmeDosya] PRIMARY KEY CLUSTERED ([ID_EOGRENMEDOSYA] ASC)
);


GO
PRINT N'Creating Table [dbo].[EOgrenmeSeviye]...';


GO
CREATE TABLE [dbo].[EOgrenmeSeviye] (
    [ID_EOGRENMESEVIYE] INT           IDENTITY (1, 1) NOT NULL,
    [ID_EOGRENMETUR]    INT           NOT NULL,
    [AD]                VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_EOgrenmeSeviye] PRIMARY KEY CLUSTERED ([ID_EOGRENMESEVIYE] ASC)
);


GO
PRINT N'Creating Table [dbo].[EOgrenmeTur]...';


GO
CREATE TABLE [dbo].[EOgrenmeTur] (
    [ID_EOGRENMETUR] INT           IDENTITY (1, 1) NOT NULL,
    [AD]             VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_EOgrenmeTur] PRIMARY KEY CLUSTERED ([ID_EOGRENMETUR] ASC)
);


GO
PRINT N'Creating Procedure [dbo].[sp_Eogrenme]...';


GO
CREATE PROC [dbo].[sp_Eogrenme]
	@ISLEM							INT				= NULL,
	@TCKIMLIKNO						VARCHAR(11)		= NULL,
	@OTURUM							VARCHAR(36)		= NULL,
	@ID_MENU						INT				= NULL,
	@SQLJSON						VARCHAR(MAX)	= NULL,
	@DONEM							VARCHAR(4)		= NULL,
	@ID_KADEME3						INT				= NULL,
	@ID_DERS						INT				= NULL,	
	@DOSYALIST						VARCHAR(MAX)	= NULL,
	@BASKAN							BIT				= NULL,
	@OGRENCI						BIT				= NULL,
	@ID_KONU						INT				= NULL,
	@ID_EOGRENME					INT				= NULL,
	@ID_EOGRENMEDETAY				INT				= NULL,
	@ID_EOGRENMEDOSYA				INT				= NULL
	
	
AS
BEGIN
	BEGIN TRY    
		DECLARE @MSG			VARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT
		DECLARE @_ID_LOG INT = 0
		DECLARE @return_variable INT
		EXEC @return_variable = [dbo].[sp_OturumKontrol] @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU
		DECLARE @AKTIFDONEM VARCHAR(4) = (SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF = 1)

		

		IF @return_variable = 1
		BEGIN
			IF @ISLEM = 1  --Kullanıcı Tip Getir
			BEGIN
			    SELECT ISNULL((
					SELECT DISTINCT
					    OGRENCI = ISNULL((SELECT TOP 1 1 FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULLANICITIPI = 4),0)
					   ,BASKAN	 = ISNULL((SELECT TOP 1 1 FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULLANICITIPI IN(27,59) ),0)
					   ,ID_KADEME3 = (SELECT TOP 1 ID_SINIF FROM OkyanusDB..vw_SinifOgrenci WHERE TCKIMLIKNO = @TCKIMLIKNO ORDER BY 1 DESC)
					 FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO
					FOR JSON AUTO
				),'[]')
			END
			
			IF @ISLEM = 2  -- Sinif listele
			BEGIN
				SELECT DISTINCT
						    @OGRENCI = ISNULL((SELECT TOP 1 1 FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO AND ID_KULLANICITIPI = 4),0)
						   ,@BASKAN	 = ISNULL((SELECT TOP 1 1  FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO AND (ID_KULLANICITIPI = 27 OR ID_KULLANICITIPI = 59)),0)
				FROM OkyanusDB.dbo.vw_KullaniciYetki WHERE TCKIMLIKNO = @TCKIMLIKNO
				IF @OGRENCI = 1
				BEGIN 
					SELECT 
						 ID_KADEME3
						,AD =KADEME3
					FROM OkyanusDB..vw_OgrenciDetayTum 
					WHERE TCKIMLIKNO = @TCKIMLIKNO AND DONEM = @AKTIFDONEM
				END
				IF @BASKAN = 1 
				BEGIN
					select distinct 					   
					   SGS.ID_KADEME3					  
					   ,AD = SGS.KADEME3
					  from eokul_v2.dbo.DersOgretmen do
					 left join OkyanusDB.dbo.v3Kullanici pb on pb.TCKIMLIKNO=do.TC_OGRETMEN
					 inner join eokul_v2.dbo.Ders   d on d.ID_DERS=do.ID_DERS
					 INNER JOIN OkyanusDB.dbo.v3Ogretmen O ON O.ID_OGRETMEN = do.ID_OGRETMEN
					 left join OKYANUSDB.DBO.V3EgitimTuru e on e.ID_EGITIMTURU=do.ID_EGITIMTURU
					 JOIN OkyanusDB.dbo.vw_SubeGrupSinif SGS ON SGS.ID_SINIF = DO.ID_SINIF
	   					 WHERE 	DONEMKOD = @AKTIFDONEM
						 --pb.TCKIMLIKNO = @TCKIMLIKNO
						   AND d.AKTIF=1
						   AND SGS.ID_KADEME3 >9 AND  SGS.ID_KADEME3 < 19 
					ORDER BY ID_KADEME3
				END
				
			END

			IF @ISLEM = 3  --Kazanım Getir
			BEGIN
			
				
					SELECT	
						 D1.ID_DERS
						,D1.ID_DERSUNITE
						,D1.KOD
						,D1.AD						
					FROM v3SinavDersUnite		D1
					inner join v3SinavDers		SD  ON SD.ID_DERS = D1.ID_DERS
					WHERE SD.ID_KADEME3=@ID_KADEME3 AND  D1.ID_DERS = @ID_DERS AND D1.AKTIF=1  

					AND  LEN(SUBSTRING(D1.KOD, PATINDEX('%[0-9]%', D1.KOD), LEN(D1.KOD)))=6
					
			END

			IF @ISLEM = 4 -- Ders Getir
			BEGIN 
				SELECT 
					ID_DERS
				   ,AD=DERSAD
				FROM eokul_v2.dbo.Ders
				WHERE ID_KADEME3 = @ID_KADEME3 AND DERSAD IN ('Matematik','Türkçe','Din Kült. Ve A. B.','Fen ve Teknoloji','Sosyal Bilgiler') AND AKTIF = 1 				
			END

			IF @ISLEM = 5 -- KONTENİR 
			BEGIN
				DECLARE @KONU BIT = 0
				SELECT TOP 1 @KONU = 1 FROM EOgrenmeBilgi WHERE ID_KONU = @ID_KONU
				IF @KONU = 1 
				BEGIN
					SELECT ISNULL((
					SELECT 
						BASLIK		= ET.AD
					   ,SEVIYE		= (SELECT ISNULL ((SELECT  ES.AD,ES.ID_EOGRENMESEVIYE, ED.ACIKLAMA,ED.ID_EOGRENMEDETAY,EB.ID_EOGRENME
														FROM EOgrenmeBilgi				EB
														JOIN EOgrenmeDetay				ED  ON ED.ID_EOGRENMEBILGI   = EB.ID_EOGRENME
														JOIN EOgrenmeSeviye				ES  ON ES.ID_EOGRENMESEVIYE  = ED.ID_SEVIYE
														WHERE ED.ID_EOGRENMETUR = ET.ID_EOGRENMETUR AND EB.ID_KONU   = @ID_KONU
														FOR JSON PATH ),'[]'))
					   ,ID			= ET.ID_EOGRENMETUR
					FROM EOgrenmeTur					ET 
					FOR JSON PATH
					),'[]')
				END
				IF @KONU = 0
				BEGIN
					SELECT ISNULL((
					SELECT 
						BASLIK		= ET.AD
					   ,SEVIYE		= (SELECT ISNULL ((SELECT DISTINCT ES.AD,ES.ID_EOGRENMESEVIYE ,ACIKLAMA = '' FROM EOgrenmeSeviye ES WHERE ES.ID_EOGRENMETUR = ET.ID_EOGRENMETUR FOR JSON PATH ),'[]'))
					   ,ID			= ET.ID_EOGRENMETUR
					FROM EOgrenmeTur					ET
					FOR JSON PATH
					),'[]')
				END
				
			END

			IF @ISLEM = 6 -- Kaydet
			BEGIN
				DECLARE  @EogrenmeBilgiId TABLE (ID_EOGRENME INT)

				INSERT INTO EOgrenmeBilgi (ID_DERS,ID_KADEME3,ID_KONU,DONEM,KYE_TARIH,KYE_TCKIMLIKNO)
				OUTPUT INSERTED.ID_EOGRENME INTO @EogrenmeBilgiId(ID_EOGRENME)
				VALUES(@ID_DERS,@ID_KADEME3,@ID_KONU,@AKTIFDONEM,GETDATE(),@TCKIMLIKNO)
				
				SET @ID_EOGRENME = (SELECT TOP 1 ID_EOGRENME FROM @EogrenmeBilgiId)

				 INSERT INTO EOgrenmeDetay(ID_EOGRENMEBILGI,ID_EOGRENMETUR,ID_SEVIYE,ACIKLAMA)
				 SELECT @ID_EOGRENME ,ID_TUR,ID_SEVIYE,ICERIK
				 FROM OPENJSON(@SQLJSON)
				 WITH
				 (
				 ICERIK			VARCHAR(MAX),
				 ID_SEVIYE		INT,
				 ID_TUR			INT
				 ) AS J
				
				SELECT 
				ISNULL((
					SELECT 
					    ED.ID_EOGRENMEDETAY
					   ,ED.ID_EOGRENMETUR
					   ,ED.ID_SEVIYE
					FROM EOgrenmeDetay			ED		
					WHERE ED.ID_EOGRENMEBILGI = @ID_EOGRENME
					FOR JSON AUTO
				),'[]')
			END

			IF @ISLEM = 7 -- Dosya Kaydet 
			BEGIN
				INSERT INTO EOgrenmeDosya(DOSYA_AD,DOSYA_GUID,ID_EOGRENMEDETAY)
				SELECT AD,GUID,@ID_EOGRENMEDETAY
				FROM OPENJSON(@DOSYALIST,'$.DOSYA')
					WITH(
						GUID	VARCHAR(MAX),
						AD		VARCHAR(MAX)

					) 
			END

			IF @ISLEM = 8 -- Güncelle
			BEGIN
		
				 UPDATE ED
				 SET ED.ACIKLAMA = J.ICERIK
				 FROM EOgrenmeDetay AS ED
				 JOIN OPENJSON(@SQLJSON)
				 WITH(
					ICERIK				VARCHAR(MAX),
					ID_SEVIYE			INT,
					ID_TUR				INT,
					ID_EOGRENMEDETAY	INT
				 ) AS J ON J.ID_EOGRENMEDETAY = ED.ID_EOGRENMEDETAY AND J.ID_SEVIYE = ED.ID_SEVIYE AND J.ID_TUR = ED.ID_EOGRENMETUR

				
				SELECT 
				ISNULL((
					SELECT 
					    ED.ID_EOGRENMEDETAY
					   ,ED.ID_EOGRENMETUR
					   ,ED.ID_SEVIYE
					FROM EOgrenmeDetay			ED		
					WHERE ED.ID_EOGRENMEBILGI = @ID_EOGRENME
					FOR JSON AUTO
				),'[]')
			END
			
			IF @ISLEM = 9 -- Dosya Detay Gör
			BEGIN
				SELECT ISNULL((
					SELECT 
						ED.DOSYA_AD
					   ,ED.DOSYA_GUID
					   ,ED.ID_EOGRENMEDOSYA
					FROM EOgrenmeDosya	ED
					WHERE ID_EOGRENMEDETAY = @ID_EOGRENMEDETAY
					FOR JSON PATH
				), '[]')
			END
			
			IF @ISLEM = 10 -- Dosya Sil
			BEGIN

				SELECT @ID_EOGRENMEDETAY = ID_EOGRENMEDETAY  FROM EOgrenmeDosya WHERE ID_EOGRENMEDOSYA = @ID_EOGRENMEDOSYA
				DELETE FROM EOgrenmeDosya WHERE ID_EOGRENMEDOSYA = @ID_EOGRENMEDOSYA

				SELECT ISNULL((
					SELECT 
						ED.DOSYA_AD
					   ,ED.DOSYA_GUID
					   ,ED.ID_EOGRENMEDOSYA
					FROM EOgrenmeDosya	ED
					WHERE ID_EOGRENMEDETAY = @ID_EOGRENMEDETAY
					FOR JSON PATH
				), '[]')
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
GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO
