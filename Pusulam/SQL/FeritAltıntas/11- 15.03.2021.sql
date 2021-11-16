GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_OZEL];


GO
PRINT N'Dropping [dbo].[DF_Sinav_INTERAKTIFPUANGIZLE]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_INTERAKTIFPUANGIZLE];


GO
PRINT N'Dropping [dbo].[DF_Sinav_OLCEKSINAVI]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_OLCEKSINAVI];


GO
PRINT N'Dropping [dbo].[DF_Sinav_DURUM]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_DURUM];


GO
PRINT N'Dropping [dbo].[DF_Sinav_OLUSTURULMATARIHI]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_OLUSTURULMATARIHI];


GO
PRINT N'Dropping [dbo].[DF_Sinav_AKTIF]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_AKTIF];


GO
PRINT N'Dropping [dbo].[DF_Sinav_GUN_TARIH]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_GUN_TARIH];


GO
PRINT N'Dropping [dbo].[DF_Sinav_BH]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_BH];


GO
PRINT N'Dropping [dbo].[DF_Sinav_CEVAPANAHTARIYUKLEMEKAPAT]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_CEVAPANAHTARIYUKLEMEKAPAT];


GO
PRINT N'Dropping [dbo].[DF_Sinav_ONLINE]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_ONLINE];


GO
PRINT N'Dropping [dbo].[DF_Sinav_YANLISSAYISI]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_YANLISSAYISI];


GO
PRINT N'Dropping [dbo].[DF_Sinav_YUKLEMEKAPAT]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [DF_Sinav_YUKLEMEKAPAT];


GO
PRINT N'Dropping [dbo].[FK_Tbl_OnlineSinavOgrenciOturum_Sinav]...';


GO
ALTER TABLE [dbo].[Tbl_OnlineSinavOgrenciOturum] DROP CONSTRAINT [FK_Tbl_OnlineSinavOgrenciOturum_Sinav];


GO
PRINT N'Dropping [dbo].[FK_SinavOgrenci_Sinav]...';


GO
ALTER TABLE [dbo].[SinavOgrenci] DROP CONSTRAINT [FK_SinavOgrenci_Sinav];


GO
PRINT N'Dropping [dbo].[FK_SinavTYTSecimTurKriter_Sinav]...';


GO
ALTER TABLE [dbo].[SinavTYTSecimTurKriter] DROP CONSTRAINT [FK_SinavTYTSecimTurKriter_Sinav];


GO
PRINT N'Dropping [dbo].[FK_Sinav_SinavTuru]...';


GO
ALTER TABLE [dbo].[Sinav] DROP CONSTRAINT [FK_Sinav_SinavTuru];


GO
PRINT N'Starting rebuilding table [dbo].[Sinav]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Sinav] (
    [ID_SINAV]                  INT           IDENTITY (1, 1) NOT NULL,
    [ID_SINAVTURU]              INT           NOT NULL,
    [AD]                        VARCHAR (200) NOT NULL,
    [KOD]                       VARCHAR (50)  NULL,
    [RAPORBASLIK]               VARCHAR (MAX) NULL,
    [DONEM]                     CHAR (4)      NOT NULL,
    [ID_KADEME3]                INT           NOT NULL,
    [SINAVTARIH]                DATE          NOT NULL,
    [OZEL]                      BIT           CONSTRAINT [DF_Sinav_OZEL] DEFAULT ((0)) NOT NULL,
    [FREKANSHESAPLA]            BIT           NOT NULL,
    [PUANGIZLE]                 BIT           CONSTRAINT [DF_Sinav_INTERAKTIFPUANGIZLE] DEFAULT ((0)) NOT NULL,
    [YUKLEMEKAPAT]              BIT           CONSTRAINT [DF_Sinav_YUKLEMEKAPAT] DEFAULT ((0)) NOT NULL,
    [CEVAPANAHTARIYUKLEMEKAPAT] BIT           CONSTRAINT [DF_Sinav_CEVAPANAHTARIYUKLEMEKAPAT] DEFAULT ((0)) NULL,
    [ONLINESINAV]               BIT           CONSTRAINT [DF_Sinav_ONLINE] DEFAULT ((0)) NOT NULL,
    [SIRALAMAYIGIZLE]           BIT           CONSTRAINT [DF_Sinav_SIRALAMAYIGIZLE] DEFAULT ((0)) NOT NULL,
    [YANLISSAYISI]              TINYINT       CONSTRAINT [DF_Sinav_YANLISSAYISI] DEFAULT ((0)) NOT NULL,
    [OLCEKSINAVI]               BIT           CONSTRAINT [DF_Sinav_OLCEKSINAVI] DEFAULT ((0)) NOT NULL,
    [DURUM]                     TINYINT       CONSTRAINT [DF_Sinav_DURUM] DEFAULT ((0)) NOT NULL,
    [KYE_TCKIMLIKNO]            VARCHAR (11)  NOT NULL,
    [KYE_TARIH]                 DATETIME      CONSTRAINT [DF_Sinav_OLUSTURULMATARIHI] DEFAULT (getdate()) NOT NULL,
    [AKTIF]                     BIT           CONSTRAINT [DF_Sinav_AKTIF] DEFAULT ((1)) NOT NULL,
    [GUN_TARIH]                 DATETIME      CONSTRAINT [DF_Sinav_GUN_TARIH] DEFAULT (getdate()) NOT NULL,
    [GUN_TCKIMLIKNO]            VARCHAR (11)  NULL,
    [DEGERLENDIRILIYOR]         BIT           CONSTRAINT [DF_Sinav_BH] DEFAULT ((0)) NULL,
    [YONERGE_GUID]              VARCHAR (MAX) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Sinav1] PRIMARY KEY CLUSTERED ([ID_SINAV] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Sinav])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Sinav] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Sinav] ([ID_SINAV], [ID_SINAVTURU], [AD], [KOD], [RAPORBASLIK], [DONEM], [ID_KADEME3], [SINAVTARIH], [OZEL], [FREKANSHESAPLA], [PUANGIZLE], [YUKLEMEKAPAT], [CEVAPANAHTARIYUKLEMEKAPAT], [ONLINESINAV], [YANLISSAYISI], [OLCEKSINAVI], [DURUM], [KYE_TCKIMLIKNO], [KYE_TARIH], [AKTIF], [GUN_TARIH], [GUN_TCKIMLIKNO], [DEGERLENDIRILIYOR], [YONERGE_GUID])
        SELECT   [ID_SINAV],
                 [ID_SINAVTURU],
                 [AD],
                 [KOD],
                 [RAPORBASLIK],
                 [DONEM],
                 [ID_KADEME3],
                 [SINAVTARIH],
                 [OZEL],
                 [FREKANSHESAPLA],
                 [PUANGIZLE],
                 [YUKLEMEKAPAT],
                 [CEVAPANAHTARIYUKLEMEKAPAT],
                 [ONLINESINAV],
                 [YANLISSAYISI],
                 [OLCEKSINAVI],
                 [DURUM],
                 [KYE_TCKIMLIKNO],
                 [KYE_TARIH],
                 [AKTIF],
                 [GUN_TARIH],
                 [GUN_TCKIMLIKNO],
                 [DEGERLENDIRILIYOR],
                 [YONERGE_GUID]
        FROM     [dbo].[Sinav]
        ORDER BY [ID_SINAV] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Sinav] OFF;
    END

DROP TABLE [dbo].[Sinav];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Sinav]', N'Sinav';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Sinav1]', N'PK_Sinav', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Tbl_OnlineSinavOgrenciOturum_Sinav]...';


GO
ALTER TABLE [dbo].[Tbl_OnlineSinavOgrenciOturum] WITH NOCHECK
    ADD CONSTRAINT [FK_Tbl_OnlineSinavOgrenciOturum_Sinav] FOREIGN KEY ([ID_SINAV]) REFERENCES [dbo].[Sinav] ([ID_SINAV]);


GO
PRINT N'Creating [dbo].[FK_SinavOgrenci_Sinav]...';


GO
ALTER TABLE [dbo].[SinavOgrenci] WITH NOCHECK
    ADD CONSTRAINT [FK_SinavOgrenci_Sinav] FOREIGN KEY ([ID_SINAV]) REFERENCES [dbo].[Sinav] ([ID_SINAV]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_SinavTYTSecimTurKriter_Sinav]...';


GO
ALTER TABLE [dbo].[SinavTYTSecimTurKriter] WITH NOCHECK
    ADD CONSTRAINT [FK_SinavTYTSecimTurKriter_Sinav] FOREIGN KEY ([ID_SINAV]) REFERENCES [dbo].[Sinav] ([ID_SINAV]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Creating [dbo].[FK_Sinav_SinavTuru]...';


GO
ALTER TABLE [dbo].[Sinav] WITH NOCHECK
    ADD CONSTRAINT [FK_Sinav_SinavTuru] FOREIGN KEY ([ID_SINAVTURU]) REFERENCES [dbo].[SinavTuru] ([ID_SINAVTURU]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
PRINT N'Refreshing [dbo].[vw_SinavDersOturum]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[vw_SinavDersOturum]';


GO
PRINT N'Refreshing [dbo].[vw_SinavOgrenciBolum]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[vw_SinavOgrenciBolum]';


GO
PRINT N'Refreshing [dbo].[vw_SinavOgrenciSoru]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[vw_SinavOgrenciSoru]';


GO
PRINT N'Altering [dbo].[sp_Sinav]...';


GO
ALTER procedure [dbo].[sp_Sinav]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@TC_OGRENCI			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	@DONEM				char(4)			= NULL,
	@OGRENCIDONEM		char(4)			= NULL,
	@SINAVSORUISLEM		BIT				= 0,
	@ID_KADEME3			INT				= NULL,
	@ID_SINAVTURU		INT				= NULL,
	@ID_SINAVTURUS		VARCHAR(MAX)	= NULL,
	@KADEME				INT				= NULL,
	@ID_SINAV			INT				= NULL,
	@ID_SINAVDERS		INT				= NULL,
	@TAKMAAD			VARCHAR(MAX)	= NULL,
	@ID_DERS			INT				= NULL,
	@ID_SUBE			INT				= NULL,
	@ID_SUBES			NVARCHAR(MAX)	= NULL,
	@DOSYAADI			varchar(100)	= NULL,
	@DOSYAGUID			varchar(50)		= NULL,
	@ID_SINAVDOSYA		int				= NULL,
	@SINAVOTURUM		tinyint			= NULL,
	@UNITEKOD			VARCHAR(50)		= NULL,
	@SQLJSON			NVARCHAR(MAX)	= NULL,
	@TCLIST				VARCHAR(MAX)	= NULL,
	@ID_SINAVDERSLIST	VARCHAR(MAX)	= NULL,
	@DURUM				VARCHAR(MAX)	= NULL,
	@TARIH				VARCHAR(MAX)	= NULL,
	@DURUMDEGER			BIT				= NULL,
	@ESKIDONEMSINIF		BIT				= 0,
	@ID_TYTSECIMTUR		INT				= NULL,
	@BARAJ				VARCHAR(MAX)	= NULL,
	@ID_SORUBANKASI		int				= NULL,
	@KOD				VARCHAR(MAX)	= NULL,
	@MAIL				BIT				= 0,
	@GUID				VARCHAR(MAX)    = NULL

AS
BEGIN
	BEGIN TRY    
		DECLARE @MSG			VARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT
		
		DECLARE @return_variable INT
		EXEC @return_variable = [dbo].[sp_OturumKontrol] @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU
	
		IF @return_variable = 1
		BEGIN
/*

	Değiştiren Adı		: Kadir Cengiz
	Değiştirilen Tarih	: 29.09.2020
	
	--	sp UPDATE
	--	ISLEM 48 ADD 

*/
			--IF (SELECT COUNT(*) FROM SinavDegerlendir WHERE AKTIF = 1 AND ID_SINAV = @ID_SINAV) > 0
			--BEGIN
			--	RAISERROR ('Sınav değerlendirme işlemi yapılıyor daha sonra tekrar deneyiniz.' , -- Message text.
			--		16, -- Severity.
			--		1 -- State.
			--		);
			--END
			--ELSE
			--BEGIN

			DECLARE @ONLINESINAV BIT = NULL			
			DECLARE @cols2		NVARCHAR(MAX)
			DECLARE @query		NVARCHAR(MAX)

			BEGIN
				SELECT ID_KADEME INTO #KADEMELER FROM v3KullaniciKademe WHERE TCKIMLIKNO=@TCKIMLIKNO
				DECLARE @AKTIFDONEM VARCHAR(4)=(	SELECT  DONEM FROM v3AktifDonem WHERE AKTIF=1)

				DECLARE  @OZELSINAVGOR BIT=0
				IF EXISTS ( SELECT TOP 1 1 FROM OkyanusDB.DBO.v3SubeYetki WHERE TCKIMLIKNO= @TCKIMLIKNO AND ID_KULLANICITIPI IN (1,60) )
				BEGIN
					SET @OZELSINAVGOR=1
				END
			
				DECLARE @ID_KADEME3ESKI INT  = @ID_KADEME3

					IF @DONEM IS NULL OR @DONEM=''
					BEGIN
						SELECT @DONEM= DONEM FROM v3AktifDonem WHERE AKTIF=1
					END

					IF NOT EXISTS ( SELECT TOP 1 1 FROM v3AktifDonem WHERE DONEM = @OGRENCIDONEM)
					BEGIN
						SET @OGRENCIDONEM = (SELECT DONEM FROM v3AktifDonem WHERE AKTIF=1)
					END 

					DECLARE @DONEMFARKI INT = CAST(@OGRENCIDONEM AS INT ) - CAST (@DONEM AS INT)
					IF  (@DONEMFARKI<>0) AND (SELECT @ESKIDONEMSINIF ) = 0
					BEGIN
						DECLARE @SIRA INT = (SELECT SIRA FROM OkyanusDB.DBO.v3Kademe3 WHERE ID_KADEME3 = @ID_KADEME3)
						SELECT @ID_KADEME3ESKI=ID_KADEME3 FROM OkyanusDB.DBO.v3Kademe3 WHERE SIRA = @SIRA- @DONEMFARKI
					END
			END

				IF @ISLEM = 0	--	Dönem Listele
				BEGIN
					SELECT * FROM [dbo].[v3AktifDonem] order by DONEM desc
				END
				IF @ISLEM = 1	--	Sınav Türü Listele
				BEGIN
					IF 4 IN (SELECT ID_KULLANICITIPI FROM OkyanusDB.dbo.v3SubeYetki WHERE TCKIMLIKNO=@TCKIMLIKNO)	--	ÖĞRENCİ İSE YALNIZCA GİRDİĞİ SINAV TÜRLERİ
					BEGIN	
						SELECT DISTINCT ST.ID_SINAVTURU, ST.AD, ST.KISAAD FROM SinavOgrenci SO
						INNER JOIN Sinav S ON S.ID_SINAV=SO.ID_SINAV
						INNER JOIN SinavTuru ST ON ST.ID_SINAVTURU=S.ID_SINAVTURU
						WHERE SO.TCKIMLIKNO = @TCKIMLIKNO
					END
					ELSE
					BEGIN
						SELECT ID_SINAVTURU, AD, KISAAD FROM SinavTuru where AKTIF=1
					END
				END
				IF @ISLEM = 2	--	Sınav Grup Listele
				BEGIN
					select distinct st.ID_KADEME3, k.AD
					from		  [dbo].[vw_SubeGrupSinif]	sgs
					Inner Join	  [dbo].[v3Sinif]			s		on s.ID_SINIF		=	sgs.ID_SINIF
					Inner Join	  [dbo].[v3Donem]			d		on d.ID_DONEM		=	s.ID_DONEM
					Inner Join	  [dbo].[v3AktifDonem]		ad		on ad.DONEM			=	d.KOD			and ad.AKTIF=1
					Inner Join	  [dbo].[v3SatisTuru]		st		on st.ID_SATISTURU	=	s.ID_SATISTURU
					Inner Join	  [dbo].[v3Kademe3]			k		on k.ID_KADEME3		=	st.ID_KADEME3
					where	(@ID_SUBES IS NULL OR @ID_SUBES='[]' OR sgs.ID_SUBE in (select value from openjson( @ID_SUBES)))  AND
							(@ID_SUBE IS NULL OR @ID_SUBE=0 OR @ID_SUBE=sgs.ID_SUBE) AND
							((@KADEME IS NULL AND st.ID_KADEME IN (SELECT ID_KADEME FROM #KADEMELER)) OR (@KADEME IS NOT NULL AND st.ID_KADEME=@KADEME))
					--ORDER BY k.SIRA

				END
				IF @ISLEM = 3	--	Sınav Ders Listele
				BEGIN
					SELECT (
						SELECT 
						KADEME		AS 'id',
						KADEME		AS 'text', 
						(
							SELECT 
							ID_DERS AS 'id', 
							AD		AS 'text'
							FROM [dbo].[v3SinavDers] sd 
							WHERE sd.ID_KADEME3=xTable.ID_KADEME3 
							ORDER BY sd.AD
							FOR JSON PATH
						) AS 'children'
						from
						(
							SELECT k3.AD AS 'KADEME', k3.ID_KADEME3 AS 'ID_KADEME3'
							FROM		[dbo].[v3SinavDers]		sd
							INNER JOIN	[dbo].[v3Kademe3]		k3	ON	k3.ID_KADEME3 = sd.ID_KADEME3
							GROUP BY k3.AD, k3.ID_KADEME3
						) AS xTable
						ORDER BY ID_KADEME3
						FOR JSON PATH
					) AS 'JSON'
				END
				IF @ISLEM = 4	--	Sınav Ekle
				BEGIN
					BEGIN TRANSACTION [Tran4]
					BEGIN TRY    
						---------
						--SINAV--
						---------
						DECLARE @ID_SINAVLIST TABLE (ID_SINAV INT)
						INSERT INTO [dbo].[Sinav]
								   ([ID_SINAVTURU] ,[AD] ,[KOD] ,[RAPORBASLIK] ,[DONEM] ,[ID_KADEME3] ,[SINAVTARIH] ,[OZEL] ,[FREKANSHESAPLA] ,[PUANGIZLE] ,[YUKLEMEKAPAT] ,[CEVAPANAHTARIYUKLEMEKAPAT],ONLINESINAV,[YANLISSAYISI], [OLCEKSINAVI], [KYE_TCKIMLIKNO],YONERGE_GUID,SIRALAMAYIGIZLE)
						OUTPUT INSERTED.ID_SINAV INTO @ID_SINAVLIST(ID_SINAV)
						select		[ID_SINAVTURU] ,[AD] ,[KOD] ,[RAPORBASLIK] ,(select DONEM from [dbo].[v3AktifDonem] where AKTIF=1) as [DONEM],[ID_KADEME3] ,convert(datetime, [SINAVTARIH], 103) ,[OZEL] ,[FREKANSHESAPLA] ,CASE WHEN (SELECT COUNT(1) FROM SinavPuanTuru WHERE ID_SINAVTURU=J.[ID_SINAVTURU])>0 THEN [PUANGIZLE] ELSE 1 END ,[YUKLEMEKAPAT] ,[CEVAPANAHTARIYUKLEMEKAPAT],ONLINESINAV ,[YANLISSAYISI] ,[OLCEKSINAVI] ,@TCKIMLIKNO ,@GUID,[SIRALAMAYIGIZLE]
						from OPENJSON(@SQLJSON)
						with
						(
							[ID_SINAVTURU] [int],
							[AD] [varchar](50),
							[KOD] [varchar](50),
							[RAPORBASLIK] [varchar](MAX),
							[ID_KADEME3] [int],
							[SINAVTARIH] [varchar](50),
							[OZEL] [bit],
							[FREKANSHESAPLA] [bit],
							[PUANGIZLE] [bit],
							[YUKLEMEKAPAT] [bit],
							[CEVAPANAHTARIYUKLEMEKAPAT] [bit],
							[ONLINESINAV] [bit],
							[YANLISSAYISI] [tinyint],
							[OLCEKSINAVI] [bit],
							[SIRALAMAYIGIZLE] [bit]
						) AS J

						SET @ID_SINAV = (select TOP 1 ID_SINAV from @ID_SINAVLIST)
						
						--SET @ONLINESINAV = IIF (EXISTS((SELECT TOP 1 1 FROM Sinav S WHERE ID_SINAV = (select ID_SINAV from @ID_SINAVLIST) AND S.ONLINESINAV = 1)),1, 0 )
						-------------
						--SINAVDERS--
						-------------
						DECLARE @SINAVDERS TABLE (ID_SINAVDERS INT,ID_DERS varchar(20), BOLUMNO int)
						INSERT INTO [dbo].[SinavDers]
								   ([ID_SINAV]
								   ,[BOLUMNO]
								   ,[ID_DERS]
								   ,[TAKMAAD])
						output INSERTED.ID_SINAVDERS, inserted.ID_DERS, inserted.BOLUMNO into @SINAVDERS(ID_SINAVDERS, ID_DERS, BOLUMNO)
						select @ID_SINAV as [ID_SINAV], SIRA as [BOLUMNO], ID_DERS=id, CASE WHEN TAKMAAD='' THEN text ELSE TAKMAAD END as TAKMAAD from OPENJSON(@SQLJSON,'$.JSONDERS')
						with
						(
							id varchar(20),
							SIRA int,
							text varchar(max),
							TAKMAAD varchar(max)
						)
						------------
						--KITAPCIK--
						------------
						DECLARE @KITAPCIKSAYISI int = (select KITAPCIKSAYISI from OPENJSON(@SQLJSON)with(KITAPCIKSAYISI int))

						--IF @ONLINESINAV = 1
						--	SET @KITAPCIKSAYISI = 1



						CREATE TABLE #HARFLER (AD char(1))
						DECLARE @asciiCode INT= 65 WHILE @asciiCode <= 90 BEGIN
							INSERT  #HARFLER (AD) SELECT  CHAR(@asciiCode)
							SELECT  @asciiCode = @asciiCode + 1
						END

						DECLARE @ID_SINAVKITAPCIK TABLE (ID_SINAVKITAPCIK INT, AD CHAR(1))
						INSERT INTO [dbo].[SinavKitapcik]
								   ([ID_SINAV]
								   ,[AD])
						output INSERTED.ID_SINAVKITAPCIK, INSERTED.AD into @ID_SINAVKITAPCIK(ID_SINAVKITAPCIK, AD)
						select top(@KITAPCIKSAYISI)@ID_SINAV as [ID_SINAV], AD from #HARFLER ORDER BY AD
						drop table #HARFLER
						------------------
						--SINAVANAHTAR A--
						------------------
						INSERT INTO [dbo].[SinavAnahtar]
								   ([ID_SINAVKITAPCIK]
								   ,[ID_SINAVDERS]
								   ,[ID_SINAVSORUTURU]
								   ,[SORUNO_A]
								   ,[CEVAP]
								   ,[OPTIKKARAKTERSAYISI]
								   ,[KOD]
								   ,[KATSAYI]
								   ,[SORUNO]
								   ,[ID_BILGI]
								   ,[ID_BILISSELSUREC]
								   ,ID_SORUBANKASI)
						SELECT
							(select ID_SINAVKITAPCIK from @ID_SINAVKITAPCIK WHERE AD='A'),
							sd.ID_SINAVDERS as ID_SINAVDERS,
							JSON_Value (s.value, '$.SORUTURU') as ID_SINAVSORUTURU,
							JSON_Value (s.value, '$.SORUNO') as SORUNOA, 
							CEVAP = CASE	WHEN SBC.ID_CEVAP	IS NULL THEN JSON_Value (s.value, '$.ACEVAP')
											WHEN SBC.CEVAPNO = 1		THEN 'A'
											WHEN SBC.CEVAPNO = 2		THEN 'B'
											WHEN SBC.CEVAPNO = 3		THEN 'C'
											WHEN SBC.CEVAPNO = 4		THEN 'D'
											WHEN SBC.CEVAPNO = 5		THEN 'E'
										END,
							--JSON_Value (s.value, '$.ACEVAP') as CEVAP,
							JSON_Value (s.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI,
							JSON_Value (s.value, '$.KOD') as KOD,
							CONVERT(float,REPLACE(JSON_Value (s.value, '$.KATSAYI'),',','.')) as KATSAYI,
							JSON_Value (s.value, '$.SORUNO') as SORUNO ,
							JSON_Value (s.value, '$.ID_BILGI') as ID_BILGI ,
							JSON_Value (s.value, '$.ID_BILISSELSUREC') as ID_BILISSELSUREC,
							SSD.ID_SORU
						FROM OPENJSON (@SQLJSON, '$.JSONDERS') as d
						CROSS APPLY OPENJSON (d.value, '$.SORULIST') as s
						INNER JOIN @SINAVDERS sd on sd.ID_DERS= JSON_Value (d.value, '$.id') and sd.BOLUMNO=JSON_Value(d.value, '$.SIRA')						
						LEFT JOIN SoruBankasi.dbo.Soru SS ON SS.ID_SORU = JSON_Value (s.value, '$.ID_SORUBANKASI') 
						LEFT JOIN Sinav JS ON JS.ID_SINAV = @ID_SINAV
						LEFT JOIN SoruBankasi.dbo.SoruDetay SSD ON SSD.ID_SORU = SS.ID_SORU AND JS.ID_KADEME3 = SSD.ID_SINAVGRUP AND SSD.UNITE = JSON_Value (s.value, '$.KOD')  AND SSD.SILINDI = 0
						LEFT JOIN SoruBankasi.dbo.Cevap		SBC	ON	SBC.ID_SORU	= SSD.ID_SORU AND DEGER = '1'


						DELETE from @ID_SINAVKITAPCIK where ID_SINAVKITAPCIK=(select ID_SINAVKITAPCIK from @ID_SINAVKITAPCIK WHERE AD='A')--A kitapçığı siliniyor

						----------------------
						--SINAVANAHTAR DIGER--
						----------------------
						declare @ID int=(select min(ID_SINAVKITAPCIK) from @ID_SINAVKITAPCIK)
						while @ID is not null
						begin
							INSERT INTO [dbo].[SinavAnahtar]
									   ([ID_SINAVKITAPCIK]
									   ,[ID_SINAVDERS]
									   ,[ID_SINAVSORUTURU]
									   ,[SORUNO_A]
									   ,[CEVAP]
									   ,[OPTIKKARAKTERSAYISI]
									   ,[KOD]
									   ,[KATSAYI]
									   ,[SORUNO]
									   ,[ID_BILGI]
									   ,[ID_BILISSELSUREC]
									   ,ID_SORUBANKASI)
							select 
								ID_SINAVKITAPCIK = @ID,
								sd.ID_SINAVDERS as ID_SINAVDERS,
								JSON_Value (s.value, '$.SORUTURU') as ID_SINAVSORUTURU,
								JSON_Value (s.value, '$.SORUNO') as SORUNOA,
								JSON_Value (s.value, '$.ACEVAP') as CEVAP,
								JSON_Value (s.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI,
								JSON_Value (s.value, '$.KOD') as KOD,
								CONVERT(float,REPLACE(JSON_Value (s.value, '$.KATSAYI'),',','.')) as KATSAYI,
								JSON_Value (k.value, '$.SIRA') as SORUNO,  
								JSON_Value (s.value, '$.ID_BILGI') as ID_BILGI ,
								JSON_Value (s.value, '$.ID_BILISSELSUREC') as ID_BILISSELSUREC,
								SSD.ID_SORU
							FROM OPENJSON (@SQLJSON, '$.JSONDERS') as d
							CROSS APPLY OPENJSON (d.value, '$.SORULIST') as s
							CROSS APPLY OPENJSON (s.value, '$.KARSILIKLIST') as k
							INNER JOIN @SINAVDERS sd on sd.ID_DERS= JSON_Value (d.value, '$.id') and sd.BOLUMNO=JSON_Value(d.value, '$.SIRA')
							LEFT JOIN SoruBankasi.dbo.Soru SS ON SS.ID_SORU = JSON_Value (s.value, '$.ID_SORUBANKASI') 
							LEFT JOIN Sinav JS ON JS.ID_SINAV = @ID_SINAV 
							LEFT JOIN SoruBankasi.dbo.SoruDetay SSD ON SSD.ID_SORU = SS.ID_SORU AND JS.ID_KADEME3 = SSD.ID_SINAVGRUP AND SSD.UNITE = JSON_Value (s.value, '$.KOD')  AND SSD.SILINDI = 0
							where JSON_Value (k.value, '$.KITAPCIK')=(select AD from SinavKitapcik where ID_SINAVKITAPCIK = @ID)

							select @ID = min(ID_SINAVKITAPCIK) from @ID_SINAVKITAPCIK where ID_SINAVKITAPCIK > @ID
						end
						select ID_SINAV = @ID_SINAV
						COMMIT TRANSACTION [Tran4]
					END TRY
					BEGIN CATCH
						ROLLBACK TRANSACTION [Tran4]

						SELECT 
							@ErrorSeverity	= ERROR_SEVERITY(),
							@ErrorState		= ERROR_STATE()
							
						SELECT @MSG = 'Kayıt işlemi sırasında sistemsel bir hata meydana geldi! Lütfen daha sonra tekrar deneyiniz...'
			
						RAISERROR (@MSG , -- Message text.
									@ErrorSeverity, -- Severity.
									@ErrorState -- State.
									);
					END CATCH;
				END
				IF @ISLEM = 5	--	Sınav Listele
				BEGIN
					SELECT * FROM
					(
						SELECT DISTINCT 
							S.ID_SINAV
							,KOD
							,S.AD
							,S.RAPORBASLIK
							,K3.AD AS GRUP
							,S.ID_KADEME3
							,Convert(datetime, SINAVTARIH, 104) AS SINAVTARIH
							,DURUM
							,AKTIF 
							,(CASE WHEN  SK.ID_SINAV IS NULL THEN 0 ELSE 1 END ) B_KITAPCIK
							,S.OZEL AS OZEL
							,S.FREKANSHESAPLA AS FREKANSHESAPLA
							,S.PUANGIZLE AS PUANGIZLE
							,S.YUKLEMEKAPAT AS YUKLEMEKAPAT
							,S.CEVAPANAHTARIYUKLEMEKAPAT AS CEVAPANAHTARIYUKLEMEKAPAT
							,S.ONLINESINAV
							,(SELECT COUNT(1) FROM (SELECT TCKIMLIKNO FROM SinavOgrenci WHERE ID_SINAV = S.ID_SINAV GROUP BY TCKIMLIKNO) XTABLE ) AS KATILIM
						FROM Sinav S
						INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.ID_KADEME3=S.ID_KADEME3
						LEFT JOIN SinavKitapcik SK ON SK.ID_SINAV=S.ID_SINAV AND SK.AD='B'
						WHERE DONEM = @DONEM and ID_SINAVTURU = @ID_SINAVTURU and S.ID_KADEME3 = @ID_KADEME3ESKI and S.AKTIF = 1
						AND (S.OZEL=0 OR @OZELSINAVGOR=1)
					)XTABLE
					ORDER BY Convert(datetime, SINAVTARIH, 104) DESC
				END
				IF @ISLEM = 6	--	Sınav Bilgi Getir
				BEGIN

					--SELECT SD.ID_DERS, A.KOD, A.AD
					--INTO #SinavDers FROM SinavDers	SD
					----CROSS APPLY [dbo].[fn_DersUniteListe](SD.ID_DERS, NULL)	DU
					--INNER JOIN v3SinavDersUnite		A	ON A.ID_DERS = SD.ID_DERS
					--WHERE SD.ID_SINAV = @ID_SINAV

					DECLARE @ID_SINAVKITAPCIKX INT = (select  top 1  ID_SINAVKITAPCIK from SinavKitapcik where ID_SINAV=@ID_SINAV and AD='A')

					Select (
						Select 
							ID_SINAVTURU, 
							s.AD as AD, 
							(select count(1) from SinavKitapcik where ID_SINAV=@ID_SINAV) as KITAPCIKSAYISI, 
							s.KOD, 
							s.RAPORBASLIK,
							ID_KADEME3, 
							Convert(varchar, SINAVTARIH, 103) as SINAVTARIH,
							OZEL,
							FREKANSHESAPLA,
							PUANGIZLE,
							YUKLEMEKAPAT,
							CEVAPANAHTARIYUKLEMEKAPAT,
							ONLINESINAV,
							YANLISSAYISI,
							OLCEKSINAVI,
							s.YONERGE_GUID as YONERGE,
							JSONDERS.ID_DERS as id,
							JSONDERS.ID_SINAVDERS as id_sinavders,
							JSONDERS.TAKMAAD as text,
							(select count(1)/(select count(1) from SinavKitapcik where ID_SINAV=@ID_SINAV) from SinavAnahtar sa where ID_SINAVDERS=JSONDERS.ID_SINAVDERS) as SORUSAYISI,
							JSONDERS.BOLUMNO as SIRA,
							SORULIST.SORUNO_A as SORUNO,
							SORULIST.ID_SINAVSORUTURU as SORUTURU,
							SORULIST.CEVAP as ACEVAP,
							SORULIST.KOD,
							ISNULL(CAST(SORULIST.ID_SORUBANKASI AS VARCHAR), '') AS ID_SORUBANKASI,
							ISNULL(SORULIST.ID_BILGI, 0) AS ID_BILGI,
							ISNULL(SORULIST.ID_BILISSELSUREC, 0) AS ID_BILISSELSUREC, 
							UNITE = ISNULL(SD.AD, ''), 
							SORULIST.OPTIKKARAKTERSAYISI as KARAKTERSAYISI,
							SORULIST.KATSAYI,
							(select (select AD from SinavKitapcik where ID_SINAVKITAPCIK=ssa.ID_SINAVKITAPCIK) as KITAPCIK
								, SORUNO as SIRA 
							from SinavAnahtar ssa 
							where ID_SINAVKITAPCIK in (	select ID_SINAVKITAPCIK 
														from SinavKitapcik 
														where	ID_SINAV=@ID_SINAV 
															and AD<>'A') 
								and SORUNO_A=SORULIST.SORUNO 
								and ID_SINAVDERS=JSONDERS.ID_SINAVDERS 
							for json path) AS KARSILIKLIST
							,s.DONEM
						from	   Sinav			s
						inner join SinavDers		JSONDERS		on	JSONDERS.ID_SINAV		= s.ID_SINAV 
						LEFT  join SinavAnahtar		SORULIST		on	SORULIST.ID_SINAVDERS	= JSONDERS.ID_SINAVDERS and SORULIST.ID_SINAVKITAPCIK=@ID_SINAVKITAPCIKX
						LEFT  JOIN v3SinavDersUnite	SD				ON	SD.KOD					= SORULIST.KOD
						where s.ID_SINAV = @ID_SINAV
						order by BOLUMNO,SORULIST.SORUNO_A
					for json auto) as J

					--DROP TABLE #SinavDers

				END
				IF @ISLEM = 7	--	Sınav Güncelle
				BEGIN					
					BEGIN TRANSACTION [Tran7]
					BEGIN TRY    
						---------
						--SINAV--
						---------
						UPDATE S
							
						SET
							 [ID_SINAVTURU] = J.ID_SINAVTURU
							,[AD] = J.AD
							,[KOD] = J.KOD
							,[RAPORBASLIK] = J.RAPORBASLIK
							,[ID_KADEME3] = IIF(J.ID_KADEME3 = 0 , S.ID_KADEME3, J.ID_KADEME3)
							,[SINAVTARIH] = convert(datetime, J.SINAVTARIH, 103)
							,[OZEL] = J.OZEL
							,[FREKANSHESAPLA] = J.FREKANSHESAPLA
							,[PUANGIZLE] = CASE WHEN (SELECT COUNT(1) FROM SinavPuanTuru WHERE ID_SINAVTURU=J.ID_SINAVTURU)>0 THEN J.[PUANGIZLE] ELSE 1 END
							,[YUKLEMEKAPAT] = J.YUKLEMEKAPAT
							,[CEVAPANAHTARIYUKLEMEKAPAT]  = J.CEVAPANAHTARIYUKLEMEKAPAT
							,[ONLINESINAV] = J.[ONLINESINAV]
							,[YANLISSAYISI] = J.YANLISSAYISI
							,[OLCEKSINAVI] = J.OLCEKSINAVI
							,[GUN_TCKIMLIKNO] = @TCKIMLIKNO
							,[GUN_TARIH] = GETDATE()
							,[YONERGE_GUID] = @GUID
						FROM [dbo].[Sinav] S
						JOIN OPENJSON(@SQLJSON)
							with
							(
								[ID_SINAVTURU] [int],
								[AD] [varchar](50),
								[KOD] [varchar](50),
								[RAPORBASLIK] [varchar](MAX),
								[ID_KADEME3] int,
								[SINAVTARIH] [varchar](50),
								[OZEL] [bit],
								[FREKANSHESAPLA] [bit],
								[PUANGIZLE] [bit],
								[YUKLEMEKAPAT] [bit],
								[CEVAPANAHTARIYUKLEMEKAPAT]  [bit],
								[ONLINESINAV]  [bit],
								[YANLISSAYISI] [tinyint],
								[OLCEKSINAVI] [bit]
							) as J ON 1 = 1
						WHERE S.ID_SINAV = @ID_SINAV
							
						SET @ONLINESINAV = (SELECT TOP 1 ONLINESINAV FROM Sinav S WHERE ID_SINAV = @ID_SINAV )
						---------------
						--SORUBANKASI--
						---------------
						SELECT SI.ID_SORUILISKI, SD.ID_SINAV, SD.ID_DERS, SORUNO_A
						INTO #TEMPSORUILISKI
						FROM SinavAnahtar SA
						INNER JOIN SinavKitapcik SK ON SK.ID_SINAVKITAPCIK = SA.ID_SINAVKITAPCIK
						INNER JOIN SinavDers SD ON SD.ID_SINAVDERS = SA.ID_SINAVDERS
						INNER JOIN SoruBankasi.dbo.SoruIliski SI ON SI.ID_SINAVANAHTAR = SA.ID_SINAVANAHTAR
						WHERE SK.ID_SINAV = @ID_SINAV AND SK.AD = 'A'

						IF @ONLINESINAV = 1
							SELECT	 SA.ID_SINAVANAHTAR
									,OS.ID_ONLINESINAVOGRENCICEVAP
									,SA.SORUNO_A
									,SD.BOLUMNO
									,OS.ID_CEVAP
							INTO #SINAVANAHTAR
							FROM SinavDers						SD
							JOIN SinavAnahtar					SA	ON	SA.ID_SINAVDERS		= SD.ID_SINAVDERS
							JOIN Tbl_OnlineSinavOgrenciCevap	OS	ON	OS.ID_SINAVANAHTAR	= SA.ID_SINAVANAHTAR
							WHERE SD.ID_SINAV	= @ID_SINAV

						---------
						--Silme--
						---------
						DELETE sa
						FROM SinavAnahtar sa
						INNER JOIN SinavKitapcik sk
							ON sk.ID_SINAVKITAPCIK=sa.ID_SINAVKITAPCIK
						WHERE sk.ID_SINAV=@ID_SINAV

						DELETE sk
						FROM SinavKitapcik sk
						WHERE sk.ID_SINAV=@ID_SINAV

						--SINAV KRİTER
						SELECT SNK.*, SNKD.ID_SINAVDERS, SD.TAKMAAD, SD.ID_DERS
						INTO #TempSinavNetKriterDers
						FROM [Pusulam].[dbo].[SinavNetKriter] SNK
						JOIN [Pusulam].[dbo].[SinavNetKriterDers] SNKD ON SNKD.ID_SINAVNETKRITER = SNK.ID_SINAVNETKRITER
						JOIN [Pusulam].[dbo].[SinavDers] SD ON SD.ID_SINAVDERS = SNKD.ID_SINAVDERS
						WHERE SD.ID_SINAV = @ID_SINAV

						SELECT DISTINCT ID_SINAVNETKRITER, NET, ID_SINAVPUANTURU
						INTO #TempSinavNetKriter
						FROM #TempSinavNetKriterDers

						SELECT *
						INTO #TempSinavTYTSecimTurKriter
						FROM [Pusulam].[dbo].[SinavTYTSecimTurKriter]
						WHERE ID_SINAV = @ID_SINAV

						--TempSinavOptikDers
						SELECT sd.ID_SINAV,sd.ID_DERS,BASLANGIC,OPTIKKARAKTERSAYISI,OTURUM,TAKMAAD INTO #TempSinavOptikDers 
						FROM SinavOptikDers sod
						INNER JOIN SinavDers sd on sd.ID_SINAVDERS=sod.ID_SINAVDERS
						WHERE sd.ID_SINAV=@ID_SINAV						

						DELETE sod
						FROM SinavOptikDers sod
						INNER JOIN SinavDers sd on sd.ID_SINAVDERS=sod.ID_SINAVDERS
						WHERE sd.ID_SINAV=@ID_SINAV

						--TempSinavKatsayi
						SELECT sd.ID_SINAV,sd.TAKMAAD,ID_SINAVPUANTURU,KATSAYI INTO #TempSinavKatsayi
						FROM SinavKatsayi sk
						INNER JOIN SinavDers sd on sd.ID_SINAVDERS=sk.ID_SINAVDERS
						WHERE sd.ID_SINAV=@ID_SINAV

						DELETE sk
						FROM SinavKatsayi sk
						INNER JOIN SinavDers sd on sd.ID_SINAVDERS=sk.ID_SINAVDERS
						WHERE sd.ID_SINAV=@ID_SINAV

						DELETE sd
						FROM SinavDers sd
						WHERE sd.ID_SINAV=@ID_SINAV

						DELETE soc
						FROM SinavOgrenciCevap soc
						INNER JOIN SinavOgrenciCevapBolum socb on socb.ID_SINAVOGRENCICEVAPBOLUM=soc.ID_SINAVOGRENCICEVAPBOLUM
						INNER JOIN SinavOgrenci so on so.ID_SINAVOGRENCI=socb.ID_SINAVOGRENCI
						WHERE so.ID_SINAV=@ID_SINAV

						DELETE socb
						FROM SinavOgrenciCevapBolum socb
						INNER JOIN SinavOgrenci so on so.ID_SINAVOGRENCI=socb.ID_SINAVOGRENCI
						WHERE so.ID_SINAV=@ID_SINAV

						DELETE sos
						FROM SinavOgrenciSonuc sos
						INNER JOIN SinavOgrenci so on so.ID_SINAVOGRENCI=sos.ID_SINAVOGRENCI
						WHERE so.ID_SINAV=@ID_SINAV

						DELETE sop
						FROM SinavOgrenciPuan sop					
						WHERE sop.ID_SINAV=@ID_SINAV

						DELETE sot
						FROM SinavOgrenciToplam sot
						WHERE sot.ID_SINAV=@ID_SINAV

						DELETE so
						FROM SinavOgrenci so
						WHERE so.ID_SINAV=@ID_SINAV

						-------------
						--SINAVDERS--
						-------------
						DECLARE @USINAVDERS TABLE (ID_SINAVDERS INT,ID_DERS varchar(20), BOLUMNO int)
						INSERT INTO [dbo].[SinavDers]
									([ID_SINAV]
									,[BOLUMNO]
									,[ID_DERS]
									,[TAKMAAD])
						output INSERTED.ID_SINAVDERS, inserted.ID_DERS, inserted.BOLUMNO into @USINAVDERS(ID_SINAVDERS, ID_DERS, BOLUMNO)
						select @ID_SINAV as [ID_SINAV], SIRA as [BOLUMNO], ID_DERS=id, CASE WHEN TAKMAAD='' THEN text ELSE TAKMAAD END as TAKMAAD 
						from OPENJSON(@SQLJSON,'$.JSONDERS')
						with
						(
							id varchar(20),
							SIRA int,
							text varchar(max),
							TAKMAAD varchar(max)
						)

						--------------------------------------------
						--EŞLEŞEN OPTİK VE KATSAYILARI GERİ KAYDET--
						--------------------------------------------
						INSERT INTO [dbo].[SinavOptikDers]
									([ID_SINAVDERS]
									,[BASLANGIC]
									,[OPTIKKARAKTERSAYISI]
									,[OTURUM])
								SELECT SD.ID_SINAVDERS, TSOD.BASLANGIC, TSOD.OPTIKKARAKTERSAYISI,OTURUM FROM #TempSinavOptikDers TSOD
								INNER JOIN SinavDers SD ON SD.ID_SINAV=TSOD.ID_SINAV AND SD.ID_DERS=TSOD.ID_DERS AND SD.TAKMAAD=TSOD.TAKMAAD
						DROP TABLE #TempSinavOptikDers

						INSERT INTO [dbo].[SinavKatsayi]
									([ID_SINAV]
									,[ID_SINAVPUANTURU]
									,[ID_SINAVDERS]
									,[KATSAYI])
								SELECT TSK.ID_SINAV, TSK.ID_SINAVPUANTURU, SD.ID_SINAVDERS, TSK.KATSAYI FROM #TempSinavKatsayi TSK
								INNER JOIN SinavDers SD ON SD.ID_SINAV=TSK.ID_SINAV AND SD.TAKMAAD=TSK.TAKMAAD
						DROP TABLE #TempSinavKatsayi

						--------------------------------------------
						--EŞLEŞEN KRİTERLERİ GERİ KAYDET--
						--------------------------------------------
						DECLARE @ID_SINAVNETKRITERTABLE7 TABLE (ID_SINAVNETKRITER INT, ID_SINAVNETKRITERESKI INT)
						MERGE INTO SinavNetKriter 
						USING #TempSinavNetKriter AS TEMP ON 1 = 0
						WHEN NOT MATCHED THEN
						INSERT ([NET] ,[ID_SINAVPUANTURU])
						VALUES (TEMP.NET, TEMP.ID_SINAVPUANTURU)
						OUTPUT TEMP.ID_SINAVNETKRITER, inserted.ID_SINAVNETKRITER
						INTO @ID_SINAVNETKRITERTABLE7 (ID_SINAVNETKRITERESKI, ID_SINAVNETKRITER);

						INSERT INTO SinavNetKriterDers (ID_SINAVNETKRITER, ID_SINAVDERS)
						SELECT DISTINCT ID_SNK.ID_SINAVNETKRITER, SD.ID_SINAVDERS
						FROM @ID_SINAVNETKRITERTABLE7 ID_SNK
						JOIN #TempSinavNetKriterDers SNKD ON SNKD.ID_SINAVNETKRITER = ID_SNK.ID_SINAVNETKRITERESKI
						JOIN SinavDers SD ON SD.ID_SINAV = @ID_SINAV AND SD.ID_DERS = SNKD.ID_DERS AND SD.TAKMAAD = SNKD.TAKMAAD

						INSERT INTO SinavTYTSecimTurKriter (ID_SINAV, ID_TYTSECIMTUR, PUAN)
						SELECT DISTINCT ID_SINAV, ID_TYTSECIMTUR, PUAN
						FROM #TempSinavTYTSecimTurKriter
						
						DROP TABLE #TempSinavNetKriter
						DROP TABLE #TempSinavNetKriterDers
						DROP TABLE #TempSinavTYTSecimTurKriter

						------------
						--KITAPCIK--
						------------
						DECLARE @UKITAPCIKSAYISI int = (select KITAPCIKSAYISI from OPENJSON(@SQLJSON)with(KITAPCIKSAYISI int))
												
						--IF @ONLINESINAV = 1
						--	SET @UKITAPCIKSAYISI = 1

						CREATE TABLE #UHARFLER (AD char(1))
						DECLARE @UasciiCode INT= 65 WHILE @UasciiCode <= 90 BEGIN
							INSERT  #UHARFLER (AD) SELECT  CHAR(@UasciiCode)
							SELECT  @UasciiCode = @UasciiCode + 1
						END

						DECLARE @UID_SINAVKITAPCIK TABLE (ID_SINAVKITAPCIK INT, AD CHAR(1))
						INSERT INTO [dbo].[SinavKitapcik]
									([ID_SINAV]
									,[AD])
						output INSERTED.ID_SINAVKITAPCIK, inserted.AD into @UID_SINAVKITAPCIK(ID_SINAVKITAPCIK, AD)
						select top(@UKITAPCIKSAYISI)@ID_SINAV as [ID_SINAV], AD from #UHARFLER ORDER BY AD
						drop table #UHARFLER

						------------------
						--SINAVANAHTAR A--
						------------------
						INSERT INTO [dbo].[SinavAnahtar]
									([ID_SINAVKITAPCIK]
									,[ID_SINAVDERS]
									,[ID_SINAVSORUTURU]
									,[SORUNO_A]
									,[CEVAP]
									,[OPTIKKARAKTERSAYISI]
									,[KOD]
									,[KATSAYI]
									,[SORUNO]
									,[ID_BILGI]
									,[ID_BILISSELSUREC]
									,[ID_SORUBANKASI])
						SELECT
							(select ID_SINAVKITAPCIK from @UID_SINAVKITAPCIK WHERE AD = 'A'),
							sd.ID_SINAVDERS as ID_SINAVDERS,
							JSON_Value (s.value, '$.SORUTURU') as ID_SINAVSORUTURU,
							JSON_Value (s.value, '$.SORUNO') as SORUNOA, 
							
							CEVAP = CASE	WHEN SBC.ID_CEVAP	IS NULL THEN JSON_Value (s.value, '$.ACEVAP')
											WHEN SBC.CEVAPNO = 1		THEN 'A'
											WHEN SBC.CEVAPNO = 2		THEN 'B'
											WHEN SBC.CEVAPNO = 3		THEN 'C'
											WHEN SBC.CEVAPNO = 4		THEN 'D'
											WHEN SBC.CEVAPNO = 5		THEN 'E'
										END,
							--JSON_Value (s.value, '$.ACEVAP') as CEVAP,
							JSON_Value (s.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI,
							JSON_Value (s.value, '$.KOD') as KOD,
							CONVERT(float,REPLACE(JSON_Value (s.value, '$.KATSAYI'),',','.')) as KATSAYI,
							JSON_Value (s.value, '$.SORUNO') as SORUNO,  
							JSON_Value (s.value, '$.ID_BILGI') as ID_BILGI,
							JSON_Value (s.value, '$.ID_BILISSELSUREC') as ID_BILISSELSUREC,
							SSD.ID_SORU
						FROM OPENJSON (@SQLJSON, '$.JSONDERS') as d
						CROSS APPLY OPENJSON (d.value, '$.SORULIST') as s
						INNER JOIN @USINAVDERS sd on sd.ID_DERS= JSON_Value (d.value, '$.id') and sd.BOLUMNO=JSON_Value(d.value, '$.SIRA')
						LEFT JOIN SoruBankasi.dbo.Soru SS ON SS.ID_SORU = JSON_Value (s.value, '$.ID_SORUBANKASI') 
						LEFT JOIN Sinav JS ON JS.ID_SINAV = @ID_SINAV 
						LEFT JOIN SoruBankasi.dbo.SoruDetay SSD ON SSD.ID_SORU = SS.ID_SORU AND JS.ID_KADEME3 = SSD.ID_SINAVGRUP AND SSD.UNITE = JSON_Value (s.value, '$.KOD')  AND SSD.SILINDI = 0
						LEFT JOIN SoruBankasi.dbo.Cevap		SBC	ON	SBC.ID_SORU	= SSD.ID_SORU AND DEGER = '1'
						
						DELETE from @UID_SINAVKITAPCIK where ID_SINAVKITAPCIK=(select ID_SINAVKITAPCIK from @UID_SINAVKITAPCIK WHERE AD = 'A')--A kitapçığı siliniyor

						----------------------
						--SINAVANAHTAR DIGER--
						----------------------
						declare @UID int=(select min(ID_SINAVKITAPCIK) from @UID_SINAVKITAPCIK)
						while @UID is not null
						begin
							INSERT INTO [dbo].[SinavAnahtar]
										([ID_SINAVKITAPCIK]
										,[ID_SINAVDERS]
										,[ID_SINAVSORUTURU]
										,[SORUNO_A]
										,[CEVAP]
										,[OPTIKKARAKTERSAYISI]
										,[KOD]
										,[KATSAYI]
										,[SORUNO]
										,[ID_BILGI]
										,[ID_BILISSELSUREC]
										,[ID_SORUBANKASI])
							select 
								ID_SINAVKITAPCIK = @UID,
								sd.ID_SINAVDERS as ID_SINAVDERS,
								JSON_Value (s.value, '$.SORUTURU') as ID_SINAVSORUTURU,
								JSON_Value (s.value, '$.SORUNO') as SORUNOA,
								JSON_Value (s.value, '$.ACEVAP') as CEVAP,
								JSON_Value (s.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI,
								JSON_Value (s.value, '$.KOD') as KOD,
								CONVERT(float,REPLACE(JSON_Value (s.value, '$.KATSAYI'),',','.')) as KATSAYI,
								JSON_Value (k.value, '$.SIRA') as SORUNO,  
								JSON_Value (s.value, '$.ID_BILGI') as ID_BILGI,
								JSON_Value (s.value, '$.ID_BILISSELSUREC') as ID_BILISSELSUREC,
								SSD.ID_SORU
							FROM OPENJSON (@SQLJSON, '$.JSONDERS') as d
							CROSS APPLY OPENJSON (d.value, '$.SORULIST') as s
							CROSS APPLY OPENJSON (s.value, '$.KARSILIKLIST') as k
							INNER JOIN @USINAVDERS sd on sd.ID_DERS= JSON_Value (d.value, '$.id') and sd.BOLUMNO=JSON_Value(d.value, '$.SIRA')
							LEFT JOIN SoruBankasi.dbo.Soru SS ON SS.ID_SORU = JSON_Value (s.value, '$.ID_SORUBANKASI') 
							LEFT JOIN Sinav JS ON JS.ID_SINAV = @ID_SINAV 
							LEFT JOIN SoruBankasi.dbo.SoruDetay SSD ON SSD.ID_SORU = SS.ID_SORU AND JS.ID_KADEME3 = SSD.ID_SINAVGRUP AND SSD.UNITE = JSON_Value (s.value, '$.KOD') AND SSD.SILINDI = 0
							where JSON_Value (k.value, '$.KITAPCIK')=(select AD from SinavKitapcik where ID_SINAVKITAPCIK = @UID)

							select @UID = min(ID_SINAVKITAPCIK) from @UID_SINAVKITAPCIK where ID_SINAVKITAPCIK > @UID
						END
						
						IF @ONLINESINAV = 1
						BEGIN
							UPDATE OS SET
								OS.ID_SINAVANAHTAR	= SA.ID_SINAVANAHTAR
							FROM SinavDers						SD
							JOIN SinavAnahtar					SA	ON	SA.ID_SINAVDERS					= SD.ID_SINAVDERS
							JOIN #SINAVANAHTAR					T	ON	T.BOLUMNO						= SD.BOLUMNO
																	AND T.SORUNO_A						= SA.SORUNO_A
							JOIN SoruBankasi.dbo.SoruDetay		SBD	ON	SBD.ID_SORU						= SA.ID_SORUBANKASI
							JOIN SoruBankasi.dbo.Cevap			C	ON	C.ID_CEVAP						= T.ID_CEVAP
																	AND C.ID_SORU						= SBD.ID_SORU
							JOIN Tbl_OnlineSinavOgrenciCevap	OS	ON	OS.ID_ONLINESINAVOGRENCICEVAP	= T.ID_ONLINESINAVOGRENCICEVAP
																	AND OS.ID_SINAVANAHTAR				= T.ID_SINAVANAHTAR
							WHERE SD.ID_SINAV = @ID_SINAV
						END
			
						DELETE FROM Tbl_OnlineSinavOgrenciCevap
						WHERE ID_ONLINESINAVOGRENCICEVAP NOT IN ( 
							SELECT OC.ID_ONLINESINAVOGRENCICEVAP
							FROM SinavAnahtar					SA
							JOIN SoruBankasi.dbo.Cevap			C	ON	C.ID_SORU			= SA.ID_SORUBANKASI
							JOIN Tbl_OnlineSinavOgrenciCevap	OC	ON	OC.ID_SINAVANAHTAR	= SA.ID_SINAVANAHTAR
																	AND OC.ID_CEVAP			= C.ID_CEVAP
							)

						UPDATE Sinav SET DURUM = 0 WHERE ID_SINAV = @ID_SINAV	--	SINAV DURUMU BEKLEMEDEYE ALINIYOR

						---------------
						--SORUBANKASI--
						---------------
						UPDATE SI SET SI.ID_SINAVANAHTAR = SA.ID_SINAVANAHTAR
						FROM SoruBankasi.dbo.SoruIliski SI
						INNER JOIN #TEMPSORUILISKI T ON T.ID_SORUILISKI = SI.ID_SORUILISKI
						INNER JOIN SinavDers SD ON SD.ID_SINAV = T.ID_SINAV AND SD.ID_DERS = T.ID_DERS
						INNER JOIN SinavKitapcik SK ON SK.ID_SINAV = T.ID_SINAV AND SK.AD = 'A'
						INNER JOIN SinavAnahtar SA ON SA.ID_SINAVKITAPCIK = SK.ID_SINAVKITAPCIK AND SA.ID_SINAVDERS = SD.ID_SINAVDERS AND SA.SORUNO_A = T.SORUNO_A

						DROP TABLE IF EXISTS #TEMPSORUILISKI
						COMMIT TRANSACTION [Tran7]
					END TRY
					BEGIN CATCH
						ROLLBACK TRANSACTION [Tran7]

						SELECT 
							@ErrorSeverity	= ERROR_SEVERITY(),
							@ErrorState		= ERROR_STATE()
							
						SELECT @MSG = 'Güncelleme işlemi sırasında sistemsel bir hata meydana geldi! Lütfen daha sonra tekrar deneyiniz...'
			
						RAISERROR (@MSG , -- Message text.
									@ErrorSeverity, -- Severity.
									@ErrorState -- State.
									);
					END CATCH;
				END
				IF @ISLEM = 8	--	Sınav Ders Konu Listele
				BEGIN
				
					EXEC [dbo].[sp_DersUniteGetir] @ID_DERS = @ID_DERS

					--SELECT ID_DERS, KOD, AD, SECILEBILIR FROM [dbo].[fn_DersUniteListe](@ID_DERS, NULL)			
				
				END
				IF @ISLEM = 9	--	Sınav Katsayı Listele
				BEGIN
					SELECT
					(
						SELECT	 ID_SINAV = @ID_SINAV
								,SPT.ID_SINAVPUANTURU
								,PUANTURU = SPT.AD
								,KATSAYILIST = ISNULL(
								(
									SELECT	 SK.ID_SINAVKATSAYI
											,S.ID_SINAV
											,SPT.ID_SINAVPUANTURU
											,SD.ID_SINAVDERS
											,DERS = SD.TAKMAAD
											,ISNULL(CONVERT(DECIMAL(18,10 ), SK.KATSAYI), 0) AS KATSAYI 
									FROM SinavDers SD
									INNER JOIN Sinav S on S.ID_SINAV = SD.ID_SINAV
									LEFT JOIN SinavKatsayi SK on SK.ID_SINAV = S.ID_SINAV AND SK.ID_SINAVDERS = SD.ID_SINAVDERS
									WHERE s.ID_SINAV = @ID_SINAV AND (sk.ID_SINAVPUANTURU IS NULL OR SK.ID_SINAVPUANTURU=SPT.ID_SINAVPUANTURU) 
									ORDER BY SD.BOLUMNO 
									FOR JSON PATH, INCLUDE_NULL_VALUES
								), '[]')
								,KRITERLIST = ISNULL(
								(
									SELECT	 ID_SINAVPUANTURU	= SPT.ID_SINAVPUANTURU
											,NET				= SNK.NET
											,ID_SINAVDERSLIST	= dbo.ufnToRawJsonArray((SELECT ID_SINAVDERS FROM SinavNetKriterDers SNKD WHERE SNKD.ID_SINAVNETKRITER = SNK.ID_SINAVNETKRITER FOR JSON PATH), 'ID_SINAVDERS')
									FROM SinavNetKriter SNK
									WHERE SNK.ID_SINAVPUANTURU = SPT.ID_SINAVPUANTURU
									AND SNK.ID_SINAVNETKRITER IN (SELECT ID_SINAVNETKRITER FROM SinavNetKriterDers WHERE ID_SINAVDERS IN (SELECT ID_SINAVDERS FROM SinavDers WHERE ID_SINAV = @ID_SINAV))
									FOR JSON PATH
								), '[]')
								,SINAVDERSLIST = ISNULL(
								(
									SELECT	ID_SINAVDERS, AD = TAKMAAD
									FROM SinavDers
									WHERE ID_SINAV = @ID_SINAV
									FOR JSON PATH
								), '[]')
								,STP.ID_SINAVTABANPUAN AS ID_SINAVTABANPUAN
								,ISNULL(STP.TABANPUAN, 0) AS TABANPUAN
						FROM SinavPuanTuru SPT
						LEFT JOIN SinavTabanPuan STP ON STP.ID_SINAVPUANTURU = SPT.ID_SINAVPUANTURU AND STP.ID_SINAV = @ID_SINAV
						WHERE ID_SINAVTURU = (SELECT ID_SINAVTURU FROM Sinav WHERE ID_SINAV = @ID_SINAV)
						FOR JSON PATH, INCLUDE_NULL_VALUES
					) AS RESULT
				END
				IF @ISLEM = 10	--	Sınav Katsayı - Taban Puan Keydet
				BEGIN
					--TABAN PUAN INSERT
					DELETE FROM SinavTabanPuan WHERE ID_SINAV=@ID_SINAV
					INSERT INTO [dbo].[SinavTabanPuan]
								([ID_SINAV]
								,[ID_SINAVPUANTURU]
								,[TABANPUAN])
					SELECT
							JSON_Value (j.value, '$.ID_SINAV') as ID_SINAV,
							JSON_Value (j.value, '$.ID_SINAVPUANTURU') as ID_SINAVPUANTURU, 
							ISNULL(JSON_Value (j.value, '$.TABANPUAN'),0) as TABANPUAN
					FROM OPENJSON (@SQLJSON) as j
					--WHERE JSON_Value (j.value, '$.ID_SINAVTABANPUAN') is null

					--TABAN PUAN UPDATE
					--UPDATE [dbo].[SinavTabanPuan]
					--SET	[TABANPUAN] = JSON_Value (j.value, '$.TABANPUAN')
					--FROM
					--	[dbo].[SinavTabanPuan] stp
					--	INNER JOIN OPENJSON (@SQLJSON) j
					--		ON JSON_Value (j.value, '$.ID_SINAVTABANPUAN') = stp.ID_SINAVTABANPUAN
					--WHERE
					--	JSON_Value (j.value, '$.ID_SINAVTABANPUAN') IS NOT NULL

					--KATSAYI INSERT
				

					DELETE FROM [SinavKatsayi] WHERE ID_SINAV=@ID_SINAV
					INSERT INTO [dbo].[SinavKatsayi]
								([ID_SINAV]
								,[ID_SINAVPUANTURU]
								,[ID_SINAVDERS]
								,[KATSAYI])
					SELECT	DISTINCT
							JSON_Value (j.value, '$.ID_SINAV') as ID_SINAV,
							JSON_Value (j.value, '$.ID_SINAVPUANTURU') as ID_SINAVPUANTURU, 
							JSON_Value (j.value, '$.ID_SINAVDERS') as ID_SINAVDERS,
							ISNULL(REPLACE(JSON_Value (j.value, '$.KATSAYI'),',','.'),0.0) as KATSAYI
					FROM OPENJSON(@SQLJSON) as parent
					CROSS APPLY OPENJSON (parent.value, '$.KATSAYILIST') as j
					--WHERE JSON_Value (j.value, '$.KATSAYI') IS NOT NULL


					--KRİTER--
					DECLARE @ID_SINAVNETKRITERTABLE TABLE (ID_SINAVNETKRITER INT, GUID VARCHAR(MAX))

					DELETE SNK 
					FROM [SinavNetKriter] SNK
					JOIN SinavNetKriterDers SNKD ON SNKD.ID_SINAVNETKRITER = SNK.ID_SINAVNETKRITER
					WHERE SNKD.ID_SINAVDERS IN (SELECT ID_SINAVDERS FROM SinavDers WHERE ID_SINAV = @ID_SINAV)

					SELECT	DISTINCT
							ISNULL(REPLACE(JSON_Value (j.value, '$.NET'),',','.'),0.0) as NET,
							JSON_Value (j.value, '$.ID_SINAVPUANTURU') as ID_SINAVPUANTURU,
							JSON_Value (j.value, '$.GUID') as GUID
					INTO #SINAVNETKRITER
					FROM OPENJSON(@SQLJSON) as parent
					CROSS APPLY OPENJSON (parent.value, '$.KRITERLIST') as j

					SELECT	 GUID = JSON_Value (KRITERLIST.value, '$.GUID')
							,ID_SINAVDERS = ID_SINAVDERSLIST.value
					INTO #SINAVNETKRITERDERS
					FROM OPENJSON(@SQLJSON) J
					CROSS APPLY OPENJSON (J.value, '$.KRITERLIST') as KRITERLIST
					CROSS APPLY OPENJSON (KRITERLIST.value, '$.ID_SINAVDERSLIST') as ID_SINAVDERSLIST

					MERGE INTO SinavNetKriter 
					USING #SINAVNETKRITER AS TEMP ON 1 = 0
					WHEN NOT MATCHED THEN
					INSERT ([NET] ,[ID_SINAVPUANTURU])
					VALUES (TEMP.NET, TEMP.ID_SINAVPUANTURU)
					OUTPUT TEMP.GUID, inserted.ID_SINAVNETKRITER
					INTO @ID_SINAVNETKRITERTABLE (GUID, ID_SINAVNETKRITER);

					INSERT INTO SinavNetKriterDers (ID_SINAVNETKRITER, ID_SINAVDERS)
					SELECT ID_SINAVNETKRITER, SNKD.ID_SINAVDERS
					FROM @ID_SINAVNETKRITERTABLE ID_SNK
					JOIN #SINAVNETKRITERDERS SNKD ON SNKD.GUID = ID_SNK.GUID

					DROP TABLE IF EXISTS #SINAVNETKRITER
					DROP TABLE IF EXISTS #SINAVNETKRITERDERS
					--KRİTER--

					--KATSAYI UPDATE
					--UPDATE [dbo].[SinavKatsayi]
					--SET [KATSAYI] = k.KATSAYI
					--FROM
					--	[dbo].[SinavKatsayi] sk
					--	INNER JOIN (SELECT JSON_Value (j.value, '$.ID_SINAVKATSAYI') as ID_SINAVKATSAYI, REPLACE(JSON_Value (j.value, '$.KATSAYI'),',','.') as KATSAYI FROM OPENJSON(@SQLJSON) as parent CROSS APPLY OPENJSON (parent.value, '$.KATSAYILIST') as j) k on k.ID_SINAVKATSAYI=sk.ID_SINAVKATSAYI
					--WHERE k.ID_SINAVKATSAYI IS NOT NULL and k.KATSAYI IS NOT NULL
				END
				IF @ISLEM = 11	--	Sınav Optik Listele
				BEGIN
					SELECT [ID_SINAVOPTIKSABIT] as ID	
						  ,ILISKI=so.ID_SINAVOPTIK
						  ,[BASLANGIC]
						  ,[OPTIKKARAKTERSAYISI] as KARAKTERSAYISI
						  ,TUR=1
						  ,AD=so.AD
						  ,OTURUM=1
						  ,BOLUMNO=0
					  INTO #TEMPOPTIK
					  FROM [SinavOptik]				so
					  LEFT JOIN [SinavOptikSabit]	sos on sos.ID_SINAVOPTIK=so.ID_SINAVOPTIK and sos.ID_SINAV=@ID_SINAV
					  ORDER BY sos.ID_SINAVOPTIKSABIT
			
					SELECT [ID_SINAVOPTIKDERS] as ID
						  ,sd.[ID_SINAVDERS] as ILISKI
						  ,[BASLANGIC]
						  ,[OPTIKKARAKTERSAYISI] as KARAKTERSAYISI
						  ,TUR=2
						  ,AD=sd.TAKMAAD
						  ,ISNULL(sod.OTURUM,1) AS OTURUM
						  ,sd.BOLUMNO
					  INTO #TEMPOPTIK2
					  FROM [SinavDers]				sd
					  LEFT JOIN [SinavOptikDers]	sod on sod.ID_SINAVDERS=sd.ID_SINAVDERS
					  WHERE sd.ID_SINAV=@ID_SINAV

					 SELECT *  FROM #TEMPOPTIK
					 UNION
					 SELECT * FROM #TEMPOPTIK2
					 ORDER BY BOLUMNO
			 
					 DROP TABLE IF EXISTS #TEMPOPTIK
					 DROP TABLE IF EXISTS #TEMPOPTIK2
				END
				IF @ISLEM = 12	--	Sınav Optik Kaydet
				BEGIN
					--OPTIK SABIT INSERT
					DELETE FROM [SinavOptikSabit] WHERE ID_SINAV=@ID_SINAV
					INSERT INTO [dbo].[SinavOptikSabit]
							   ([ID_SINAV]
							   ,[ID_SINAVOPTIK]
							   ,[BASLANGIC]
							   ,[OPTIKKARAKTERSAYISI])
					SELECT
							@ID_SINAV as ID_SINAV,
							JSON_Value (j.value, '$.ILISKI') as ID_SINAVOPTIK, 
							JSON_Value (j.value, '$.BASLANGIC') as BASLANGIC, 
							JSON_Value (j.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI
					FROM OPENJSON (@SQLJSON) as j
					WHERE JSON_Value (j.value, '$.TUR') = 1 --and (JSON_Value (j.value, '$.ID') IS NULL or JSON_Value (j.value, '$.ID') = 0)

					--OPTIK SABIT UPDATE
					--UPDATE [dbo].[SinavOptikSabit]
					--SET	[BASLANGIC] = JSON_Value (j.value, '$.BASLANGIC'), [OPTIKKARAKTERSAYISI] = JSON_Value (j.value, '$.KARAKTERSAYISI')
					--FROM OPENJSON (@SQLJSON) as j
					--WHERE
					--	JSON_Value (j.value, '$.TUR') = 1 
					--	and (JSON_Value (j.value, '$.ID') IS NOT NULL 
					--	and JSON_Value (j.value, '$.ID') > 0)
					--	and ID_SINAVOPTIKSABIT=JSON_Value (j.value, '$.ID')

					--OPTIK DERS INSERT
					DELETE SOD FROM [SinavOptikDers] SOD INNER JOIN SinavDers SD ON SD.ID_SINAVDERS=SOD.ID_SINAVDERS WHERE SD.ID_SINAV=@ID_SINAV
					INSERT INTO [dbo].[SinavOptikDers]
							   ([ID_SINAVDERS]
							   ,[BASLANGIC]
							   ,[OPTIKKARAKTERSAYISI]
							   ,[OTURUM])
					SELECT
							JSON_Value (j.value, '$.ILISKI') as ID_SINAVDERS, 
							JSON_Value (j.value, '$.BASLANGIC') as BASLANGIC, 
							JSON_Value (j.value, '$.KARAKTERSAYISI') as OPTIKKARAKTERSAYISI,
							JSON_Value (j.value, '$.OTURUM') as OTURUM
					FROM OPENJSON (@SQLJSON) as j
					WHERE JSON_Value (j.value, '$.TUR') = 2 --and (JSON_Value (j.value, '$.ID') IS NULL or JSON_Value (j.value, '$.ID') = 0)

					--OPTIK DERS UPDATE
					--UPDATE [dbo].[SinavOptikDers]
					--SET	[BASLANGIC] = JSON_Value (j.value, '$.BASLANGIC'), [OPTIKKARAKTERSAYISI] = JSON_Value (j.value, '$.KARAKTERSAYISI'),
					--	[OTURUM] = JSON_Value (j.value, '$.OTURUM')
					--FROM OPENJSON (@SQLJSON) as j
					--WHERE
					--	JSON_Value (j.value, '$.TUR') = 2 
					--	and (JSON_Value (j.value, '$.ID') IS NOT NULL 
					--	and JSON_Value (j.value, '$.ID') > 0)
					--	and ID_SINAVOPTIKDERS=JSON_Value (j.value, '$.ID')
				END
				IF @ISLEM = 13	--	Sınav Dosya Listele
				BEGIN
					SELECT 
						 sd.ID_SINAVDOSYA
						,sd.AD
						,k.AD AS KAD
						,k.SOYAD AS KSOYAD
						,sd.KYE_TARIH 
						,ISNULL(s.AD,'') SUBEAD
						,sn.ONLINESINAV
						,GUID = sd.GUID + IIF (CHARINDEX('.', SD.AD) > 0 , SUBSTRING( SD.AD , LEN(SD.AD) - CHARINDEX('.', REVERSE(SD.AD)) + 1 , CHARINDEX('.', REVERSE(SD.AD)) ) , '')
						,KATILIM	= IIF(SN.ONLINESINAV = 1 AND  SD.AD = 'ONLINE'
										,(	SELECT COUNT(DISTINCT TCKIMLIKNO) 
											FROM Tbl_OnlineSinavOgrenci		 OS 
											JOIN Tbl_OnlineSinavOgrenciCevap C	ON	C.ID_ONLINESINAVOGRENCI = OS.ID_ONLINESINAVOGRENCI 
																				AND C.AKTIF					= 1 
											WHERE	OS.ID_SINAV = SN.ID_SINAV 
												AND OS.AKTIF	= 1)
										,(SELECT COUNT(t.LINE) FROM SinavOptikTemp t WHERE t.ID_SINAVDOSYA=sd.ID_SINAVDOSYA)
										)
						,(SELECT COUNT(1) FROM [dbo].[fn_TcEslesmeGetir](sn.ID_KADEME3,SD.ID_SINAVDOSYA,sn.ID_SINAV,cast(@SINAVOTURUM as int),0)) SORUNLUTC
						--,(SELECT COUNT(1) FROM [dbo].[fn_TcEslesmeGetir](sn.ID_KADEME3,SD.ID_SINAVDOSYA,sn.ID_SINAV,@SINAVOTURUM,1))TEKRARLAYANTC
						,sn.DURUM
						,sn.YUKLEMEKAPAT
					FROM		SinavDosya		sd
					INNER JOIN	Sinav			sn	on sn.ID_SINAV	= sd.ID_SINAV 
					INNER JOIN	v3Kullanici		k	on k.TCKIMLIKNO	= sd.KYE_TCKIMLIKNO
					LEFT  JOIN	v3Sube			s	on s.ID_SUBE	= sd.ID_SUBE
					where sd.ID_SINAV=@ID_SINAV AND sd.AKTIF=1 AND (@ID_SUBE=0 OR sd.ID_SUBE=@ID_SUBE) AND (@SINAVOTURUM = 0 OR sd.OTURUM = @SINAVOTURUM)
				END
				IF @ISLEM = 14	--	Sınav Dosya Kaydet
				BEGIN

					 IF EXISTS (SELECT TOP 1 1 FROM SinavDosya WHERE ID_SINAV=@ID_SINAV AND AD=@DOSYAADI AND AKTIF=1)
					 BEGIN
						SELECT @MSG = 'DAHA ÖNCE AYNI İSİMLİ DOSYA KAYDETTİNİZ. KONTROL EDİNİZ!'
			
						RAISERROR (@MSG , -- Message text.
							16, -- Severity.
							1-- State.
							);					
					 END
					 ELSE
					 BEGIN
						DECLARE @ID_SINAVDOSYAx TABLE (ID_SINAVDOSYA int)
						INSERT INTO [dbo].[SinavDosya]
									([ID_SINAV]
									,[ID_SUBE]
									,[AD]
									,[GUID]
									,[KYE_TCKIMLIKNO]
									,[OTURUM])
									OUTPUT INSERTED.ID_SINAVDOSYA into @ID_SINAVDOSYAx
								VALUES
									(@ID_SINAV,@ID_SUBE,@DOSYAADI,@DOSYAGUID,@TCKIMLIKNO,@SINAVOTURUM)
						DECLARE @IDX int=(select top 1 ID_SINAVDOSYA from @ID_SINAVDOSYAx)
						DECLARE	@return_value int
			
						--UPDATE Sinav SET DURUM=3 WHERE ID_SINAV=@ID_SINAV	--	SINAV DURUMU BEKLEMEDEYE ALINIYOR
			
						--EXEC	@return_value = [dbo].[sp_OptikIslemleri]
						--		@ID_SINAV = @ID_SINAV,
						--		@ID_SINAVDOSYA = @IDX,
						--		@ID_SUBE=@ID_SUBE,
						--		@ISLEM = 0

						--UPDATE Sinav SET DURUM=0 WHERE ID_SINAV=@ID_SINAV

						DECLARE @SQLDOSYA		VARCHAR(max)	= NULL
						DECLARE @YOLDOSYA		VARCHAR(max)	= NULL
						DECLARE @GUIDDOSYA		VARCHAR(max)	= NULL

						IF @IDX IS NOT NULL AND NOT EXISTS(SELECT TOP 1 1 FROM SinavOptikTemp WHERE ID_SINAVDOSYA=@IDX)
						BEGIN
				
							PRINT '2: ' + CONVERT( VARCHAR(24), GETDATE(), 121)

							DECLARE @PAYLASIMYOL VARCHAR(MAX) = '\\172.18.194.207\$SinavOptik\'

							IF HOST_NAME() = 'KDCENGIZPC'	-- LOCALDEN İSE KENDİ SINAVDOSYA YA 
								SET @PAYLASIMYOL = '\\100.100.15.144\$SinavOptik\'
								

							SET @GUIDDOSYA = (SELECT GUID FROM SinavDosya where ID_SINAVDOSYA=@IDX)
							create table #tmp (
								line nvarchar(max)
							);
							BEGIN TRY  
								SET @YOLDOSYA = @PAYLASIMYOL+@GUIDDOSYA+'.txt'
								SET @SQLDOSYA=
								'BULK INSERT #tmp
								FROM '''+@YOLDOSYA+'''
								WITH (FIELDTERMINATOR = '','',CODEPAGE=''ACP'')'
								EXEC(@SQLDOSYA) 
							END TRY  
							BEGIN CATCH  
								SET @YOLDOSYA = @PAYLASIMYOL+@GUIDDOSYA+'.dat'
								SET @SQLDOSYA=
								'BULK INSERT #tmp
								FROM '''+@YOLDOSYA+'''
								WITH (FIELDTERMINATOR = '','',CODEPAGE=''ACP'')'
								EXEC(@SQLDOSYA) 
							END CATCH 
				
							INSERT INTO SinavOptikTemp (LINE,ID_SINAVDOSYA)
							SELECT line, @IDX FROM #tmp
							WHERE line IS NOT NULL
							DROP TABLE IF EXISTS #tmp

							PRINT '3: ' + CONVERT( VARCHAR(24), GETDATE(), 121)

						END	

						SELECT DISTINCT OTURUM INTO #OTURUMLARDOSYAYUKLE FROM SinavDosya WHERE ID_SINAV  = @ID_SINAV
						DECLARE @HATALIDOSYAYUKLE BIT=0
						WHILE (SELECT COUNT(1) FROM #OTURUMLARDOSYAYUKLE)>0
						BEGIN 
							DECLARE @SUB_OTURUMDOSYAYUKLE INT = (SELECT TOP 1 OTURUM FROM #OTURUMLARDOSYAYUKLE)
				
							IF  ((SELECT COUNT(1) TE_TC FROM [dbo].[fn_TcEslesmeGetir](NULL,NULL,@ID_SINAV,@SUB_OTURUMDOSYAYUKLE,1)) > 0)
							BEGIN
								--SELECT @MSG = 'TEKRARLANAN TC OLDUĞUNDAN DEĞERLENDİRME YAPILAMAZ KONTROL EDİNİZ!'
			
								--RAISERROR (@MSG , -- Message text.
								--	16, -- Severity.
								--	1-- State.
								--	);
								SET @HATALIDOSYAYUKLE = 1
							END
				
							DELETE FROM #OTURUMLARDOSYAYUKLE WHERE OTURUM = @SUB_OTURUMDOSYAYUKLE
						END

						IF @HATALIDOSYAYUKLE = 0
						BEGIN
							UPDATE Sinav SET DURUM = 1, DEGERLENDIRILIYOR = 0 where ID_SINAV = @ID_SINAV -- SIRAYA ALINDI
			
							INSERT INTO [dbo].[SinavDegerlendir]
									([ID_SINAV]
									,[KYE_TCKIMLIKNO])
								VALUES
									(@ID_SINAV
									,@TCKIMLIKNO)
						END

						SELECT @IDX
					END
				END
				IF @ISLEM = 15	--	Sınav Dosya Sil
				BEGIN
					UPDATE SinavDosya set AKTIF=0, SIL_TCKIMLIKNO=@TCKIMLIKNO, SIL_TARIH=getdate() where ID_SINAVDOSYA=@ID_SINAVDOSYA
					DELETE FROM SinavOgrenci WHERE ID_SINAVDOSYA=@ID_SINAVDOSYA
					DELETE FROM SinavOptikTemp WHERE ID_SINAVDOSYA=@ID_SINAVDOSYA
				END
				IF @ISLEM = 16	--	Optik Girildi Mi
				BEGIN
					IF((select count(1) from SinavOptikDers sod inner join SinavDers sd on sd.ID_SINAVDERS=sod.ID_SINAVDERS where sd.ID_SINAV=@ID_SINAV)
					+(select count(1) from SinavOptikSabit where ID_SINAV=@ID_SINAV)
					=(select count(1) from SinavOptik)+(select count(1) from SinavDers where ID_SINAV=@ID_SINAV))
					BEGIN
						select 1
					END
					ELSE
					BEGIN
						select 0
					END 
				END
				IF @ISLEM = 17	--	Sınav Değerlendir
				BEGIN
					SELECT DISTINCT OTURUM INTO #OTURUMLAR FROM SiNAVDOSYA WHERE ID_SINAV=@ID_SINAV
					DECLARE @HATALI BIT=0
					WHILE (SELECT COUNT(1) FROM #OTURUMLAR)>0
					BEGIN 
				
						DECLARE @SUB_OTURUM INT =(SELECT TOP 1 OTURUM FROM #OTURUMLAR)
				
						IF  ((SELECT COUNT(1) TE_TC FROM [dbo].[fn_TcEslesmeGetir](NULL,NULL,@ID_SINAV,@SUB_OTURUM,1))>0)
						BEGIN
							SELECT @MSG = 'TEKRARLANAN TC OLDUĞUNDAN DEĞERLENDİRME YAPILAMAZ KONTROL EDİNİZ!'
			
							RAISERROR (@MSG , -- Message text.
								16, -- Severity.
								1-- State.
								);
							SET @HATALI=1

						END
				
						DELETE FROM #OTURUMLAR WHERE OTURUM=@SUB_OTURUM
					END





					IF  (@HATALI=0)			
					BEGIN

					
						IF	 EXISTS(SELECT TOP 1 1 FROM Sinav WHERE ID_SINAV = @ID_SINAV AND ONLINESINAV = 1) 
						 AND NOT EXISTS(SELECT TOP 1 1 FROM SinavDosya WHERE ID_SINAV = @ID_SINAV)
						BEGIN
								INSERT INTO SinavDosya (AD, GUID, ID_SINAV, ID_SUBE, KYE_TCKIMLIKNO)
								VALUES ('ONLINE', '', @ID_SINAV, 0, @TCKIMLIKNO)
						END

						UPDATE Sinav SET DURUM = 1, DEGERLENDIRILIYOR = 0 where ID_SINAV = @ID_SINAV -- SIRAYA ALINDI
			
						INSERT INTO [dbo].[SinavDegerlendir]
								   ([ID_SINAV]
								   ,[KYE_TCKIMLIKNO])
							 VALUES
								   (@ID_SINAV
								   ,@TCKIMLIKNO)
					END
				END
				IF @ISLEM = 18	--	Sınav Puan Türü (@ID_SINAVTURU)
				BEGIN
					--SELECT		S.ID_SINAV, KOD, S.AD, ID_KADEME3, Convert(varchar, SINAVTARIH, 103) as SINAVTARIH, DURUM 
					--FROM		Sinav			S
					--INNER JOIN	SinavOgrenci	SO	ON SO.ID_SINAV=S.ID_SINAV
					--WHERE		SO.TCKIMLIKNO	= @TC_OGRENCI	AND S.DONEM	= @DONEM
					--ORDER BY	S.SINAVTARIH

					SELECT		DISTINCT PT.ID_SINAVPUANTURU,PT.AD,PT.KOD
					FROM		SinavPuanTuru	PT
					INNER JOIN	Sinav			S	ON PT.ID_SINAVTURU=S.ID_SINAVTURU
					WHERE		S.ID_SINAVTURU=@ID_SINAVTURU
					ORDER BY	PT.ID_SINAVPUANTURU 


				END
				IF @ISLEM = 19	--	Sınav Listele by Ogrenci
				BEGIN
					select * from
					(
						Select distinct s.ID_SINAV, KOD, s.AD, ID_KADEME3, Convert(varchar, SINAVTARIH, 104) as SINAVTARIH, DURUM, st.AD as TUR
						from Sinav			s
						join SinavTuru		st	on st.ID_SINAVTURU=s.ID_SINAVTURU
						join SinavOgrenci	so	on s.ID_SINAV=so.ID_SINAV	and (@TC_OGRENCI IS NULL OR TCKIMLIKNO=@TC_OGRENCI)
						where s.AKTIF=1 and s.DURUM=2  
							and (OZEL=0 OR @OZELSINAVGOR=1)
							and ((@DONEM='0' and DONEM=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)) or DONEM=@DONEM) 
							and (cast(@ID_SINAVTURU as int)=0 or s.ID_SINAVTURU=cast(@ID_SINAVTURU as int))
					) as XTABLE
					order by cast(SINAVTARIH as datetime) desc
				END
				IF @ISLEM = 20	--	Ünite Ara
				BEGIN			
					--select AD from dbo.[fn_DersUniteListe](@ID_DERS,@UNITEKOD)

					SELECT AD FROM v3SinavDersUnite
					WHERE KOD = @UNITEKOD

				END
				IF @ISLEM = 21  --  SINAV AKTIF PASIF
				BEGIN
					UPDATE Sinav 
					SET AKTIF=CASE WHEN AKTIF=1 THEN 0 ELSE 1 END
					WHERE ID_SINAV=@ID_SINAV
					select AKTIF from Sinav WHERE ID_SINAV=@ID_SINAV
				END 
				IF @ISLEM = 22  --  Sınav Dosya İçerik
				BEGIN

					declare @SQL varchar(max),@cols varchar(max)--,@ID_SINAVDOSYA int=196,@ID_SINAV int=43
					
					DROP TABLE IF EXISTS #TEMP
					DROP TABLE IF EXISTS ##T1
					
					select @ID_SINAV=ID_SINAV FROM SinavDosya WHERE ID_SINAVDOSYA=@ID_SINAVDOSYA

					SELECT 
						 DEGER	= CAST( RTRIM(SUBSTRING(LINE,BASLANGIC , OPTIKKARAKTERSAYISI )) AS varchar(MAX))
						,SO.KOLONADI
						,ID_SINAVOPTIKTEMP
						,SIRA = SO.ID_SINAVOPTIK 
					INTO #TEMP
					FROM SinavOptikTemp		SOT
					JOIN SinavOptikSabit	SOS ON	SOS.ID_SINAV=@ID_SINAV and SOS.OPTIKKARAKTERSAYISI>0
					JOIN SinavOptik			SO	ON	SO.ID_SINAVOPTIK=SOS.ID_SINAVOPTIK
					where ID_SINAVDOSYA=@ID_SINAVDOSYA
					UNION ALL
					select			
						 RTRIM(SUBSTRING(LINE, sod.BASLANGIC, sod.OPTIKKARAKTERSAYISI)) DEGER
						,sd.TAKMAAD KOLONADI
						,ID_SINAVOPTIKTEMP	
						,SIRA = 100 + CAST(SD.BOLUMNO AS INT)
					from SinavOptikTemp			sot
					inner join	SinavDers		sd	on	sd.ID_SINAV=@ID_SINAV
					inner join	SinavOptikDers	sod	on	sod.ID_SINAVDERS=sd.ID_SINAVDERS
					where ID_SINAVDOSYA=@ID_SINAVDOSYA


					SET @cols = STUFF((SELECT DISTINCT ',' + QUOTENAME(c.KOLONADI) 
											FROM #TEMP c
											FOR XML PATH(''), TYPE
											).value('.', 'NVARCHAR(MAX)') 
										,1,1,'')

					SET @SQL='  SELECT * INTO ##T1  FROM ( SELECT DEGER,KOLONADI,ID_SINAVOPTIKTEMP FROM #TEMP ) AS x 
								PIVOT  (MAX(DEGER) FOR KOLONADI IN ('+@cols+')) AS p'
					EXECUTE(@SQL)

					select(
						select * from(
							select 
							(
								select * from ##T1
								FOR JSON AUTO
							) as t1	
							,(
								SELECT KOLONADI FROM #TEMP GROUP BY KOLONADI,SIRA ORDER BY SIRA
								FOR JSON AUTO
							) as t2					
						) as tablo FOR JSON AUTO
					) as tt

					DROP TABLE IF EXISTS #TEMP
					DROP TABLE IF EXISTS ##T1


				END 
				IF @ISLEM = 23  --  Sınav Dosya İçerik Düzenle
				BEGIN
					--DECLARE @SQLJSON VARCHAR(MAX)='[{"ID_SINAVOPTIKTEMP":37314,"KOLONADI":"AD","DEGER":"ZİLAN     KARAYİĞİTAS"},{"ID_SINAVOPTIKTEMP":37326,"KOLONADI":"KITAPCIK","DEGER":"A"},{"ID_SINAVOPTIKTEMP":37315,"KOLONADI":"KITAPCIK","DEGER":"A"}]'
					DROP TABLE IF EXISTS #TEMP2
					SELECT
						JSON_Value (j.value, '$.ID_SINAVOPTIKTEMP') as ID_SINAVOPTIKTEMP,
						JSON_Value (j.value, '$.KOLONADI') as KOLONADI, 
						JSON_Value (j.value, '$.DEGER') as DEGER
					INTO #TEMP2
					FROM OPENJSON (@SQLJSON) as j
			
					SELECT @ID_SINAVDOSYA=ID_SINAVDOSYA,@ID_SINAV=ID_SINAV  FROM SinavDosya WHERE ID_SINAVDOSYA =(SELECT ID_SINAVDOSYA FROM SinavOptikTemp WHERE ID_SINAVOPTIKTEMP=(SELECT TOP 1 ID_SINAVOPTIKTEMP FROM #TEMP2))
			
					WHILE ((SELECT COUNT(DISTINCT KOLONADI) FROM #TEMP2)>0)
					BEGIN
						UPDATE	SOT
						SET		
								sot.LINE = STUFF(LINE,SOS.BASLANGIC,SOS.OPTIKKARAKTERSAYISI,LEFT( upper(DEGER)+SPACE(SOS.OPTIKKARAKTERSAYISI),SOS.OPTIKKARAKTERSAYISI))
						FROM	SinavOptikTemp		SOT
						JOIN	#TEMP2				T	ON	T.ID_SINAVOPTIKTEMP	= SOT.ID_SINAVOPTIKTEMP
						JOIN	SinavOptik			SO	ON	SO.KOLONADI			= T.KOLONADI
						JOIN	SinavDosya			SD	ON	SD.ID_SINAVDOSYA	= SOT.ID_SINAVDOSYA
						JOIN	SinavOptikSabit		SOS	ON	SOS.ID_SINAVOPTIK	= SO.ID_SINAVOPTIK
														AND SOS.ID_SINAV		= SD.ID_SINAV
						WHERE	T.KOLONADI=(SELECT TOP 1 KOLONADI FROM #TEMP2 ORDER BY KOLONADI)

						DELETE FROM #TEMP2 WHERE KOLONADI=(SELECT TOP 1 KOLONADI FROM #TEMP2 ORDER BY KOLONADI)
					END
			
					DROP TABLE IF EXISTS #TEMP2			

					UPDATE Sinav SET DURUM=0 WHERE ID_SINAV=@ID_SINAV	--	SINAV DURUMU BEKLEMEDEYE ALINIYOR
			
					--EXEC	@return_value = [dbo].[sp_OptikIslemleri]
					--		@ID_SINAV = @ID_SINAV,
					--		@ID_SINAVDOSYA = @ID_SINAVDOSYA,
					--		@ISLEM = 0
			
					select 1
				END 
				IF @ISLEM = 24  --  Öğrenci Listele by ID_SINAV
				BEGIN
					DROP TABLE IF EXISTS #T1

					SELECT	DISTINCT SO.TCKIMLIKNO
							,ISNULL(VK.AD,SO.AD) AD
							,ISNULL(VK.SOYAD,SO.SOYAD) SOYAD
							,ISNULL(VS.SUBEAD,'') SUBEAD
							,ISNULL(VS.SINIF,'') SINIF
					 INTO #T1
					 FROM SinavOgrenci		SO
				LEFT JOIN V3OGRENCi			VO	ON	SO.TCKIMLIKNO	= VO.TCKIMLIKNO
				LEFT JOIN V3Kullanici		VK	ON	VK.TCKIMLIKNO	= SO.TCKIMLIKNO
												AND VK.AKTIF		= 1
				LEFT JOIN vw_SubeGrupSinif	VS	ON	VS.ID_SINIF		= VO.ID_SINIF
					WHERE							SO.ID_SINAV		= @ID_SINAV

					select(
						select * from(
							select 
							(
								select DISTINCT TCKIMLIKNO,AD,SOYAD,SUBEAD,SINIF from #T1
								FOR JSON AUTO
							) as t1								
						) as tablo FOR JSON AUTO
					) as tt
			
						DROP TABLE IF EXISTS #T1
				END 
				IF @ISLEM = 25  --  Sınav Özellikleri
				BEGIN
						SELECT 
							 SD.TAKMAAD 
							,SA.SORUNO_A
							,SA.CEVAP
							,SORUNO= CASE WHEN (SELECT 1 FROM SinavKitapcik WHERE ID_SINAV=@ID_SINAV AND AD='B') > 0 THEN CONVERT(varchar(20), SA.SORUNO) ELSE '---'  END
							,SA.KOD
							,BILGI = ISNULL(B.AD, '')
							,BILISSELSUREC = ISNULL(BS.AD, '')
							,SA.ID_SORUBANKASI

							,KAZANIM= CASE WHEN LEN(SA.KOD) >0 THEN (select top 1 AD from v3SinavDersUnite DK where DK.KOD=SA.KOD								) ELSE '' END
							,UNITE	= CASE WHEN LEN(SA.KOD) >3 THEN (select top 1 AD from v3SinavDersUnite DK where DK.KOD=SUBSTRING(SA.KOD ,1,LEN(SA.KOD)-3)	) ELSE '' END
							,KONU	= CASE WHEN LEN(SA.KOD) >6 THEN (select top 1 AD from v3SinavDersUnite DK where DK.KOD=SUBSTRING(SA.KOD ,1,LEN(SA.KOD)-6)	) ELSE '' END
					
							,SD.ID_SINAVDERS
							,S.ID_SINAV
							,ISNULL(SOD.OTURUM,1) OTURUM
							,VD.ACIKLAMA DONEM
							,VK.AD GRUP
							,S.AD SINAVAD
							,convert(varchar(15), S.SINAVTARIH, 104) SINAVTARIH
						FROM	Sinav				S
						JOIN	SinavKitapcik		SK	ON	SK.ID_SINAV			= S.ID_SINAV
														AND SK.AD				= IIF (S.ONLINESINAV = 1 ,'A','A')
						JOIN	SinavAnahtar		SA	ON	SA.ID_SINAVKITAPCIK	= SK.ID_SINAVKITAPCIK
						JOIN	SinavDers			SD	ON	SD.ID_SINAVDERS		= SA.ID_SINAVDERS
					LEFT JOIN	SinavOptikDers		SOD	ON	SOD.ID_SINAVDERS	= SD.ID_SINAVDERS
						JOIN	V3AKTiFDONEM		VD	ON	VD.DONEM			= S.DONEM
						JOIN	V3Kademe3			VK	ON	VK.ID_KADEME3		= S.ID_KADEME3		
					LEFT JOIN	Bilgi				B	ON	B.ID_BILGI			= SA.ID_BILGI
					LEFT JOIN	BilisselSurec		BS	ON	BS.ID_BILISSELSUREC	= SA.ID_BILISSELSUREC
						WHERE S.ID_SINAV=@ID_SINAV
						ORDER BY OTURUM,SORUNO_A,SD.BOLUMNO
				END 
				IF @ISLEM = 26  --  Sınav Derslerini Listele
				BEGIN	
					select(
						select * from(
							select 
							(
								SELECT	DISTINCT SD.ID_SINAVDERS, SD.ID_DERS,SD.TAKMAAD DERSAD,BOLUMNO
								FROM	SinavDers	SD	
								WHERE 	SD.ID_SINAV=@ID_SINAV
								ORDER BY BOLUMNO
								FOR JSON AUTO
							) as t1								
						) as tablo FOR JSON AUTO
					) as tt
				END 
				IF @ISLEM = 27  --  Sınav Dersler Sorularını Listele
				BEGIN
					--SELECT	SORUNO,SORUNO_A,CEVAP,SK.AD
					--FROM	SinavDers		SD	
					--JOIN	SinavAnahtar	SA	ON	SA.ID_SINAVDERS	= SD.ID_SINAVDERS
					--JOIN	SinavKitapcik	SK	ON	SK.ID_SINAV		= S.ID_SINAV
					--						AND SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK	
					--WHERE	SD.ID_SINAVDERS = @ID_SINAVDERS		

						DROP TABLE IF EXISTS ##TEMP

						SET @cols2 = STUFF((SELECT distinct ',' + QUOTENAME(SK.AD) 
									FROM SinavDers		SD	
									JOIN	SinavAnahtar	SA	ON	SA.ID_SINAVDERS	= SD.ID_SINAVDERS
									JOIN	SinavKitapcik	SK	ON	SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK	
									WHERE	SD.ID_SINAVDERS = @ID_SINAVDERS
									FOR XML PATH(''), TYPE
									).value('.', 'NVARCHAR(MAX)') 
								,1,1,'')

								--SORUNO_A,CEVAP,A,B
								--@SINAVSORUISLEM =1 ise soru işlem tablosuna eklenenler listelenmeyecek 
						set @query = 'SELECT * INTO ##TEMP FROM
						(
							SELECT	SORUNO,SORUNO_A,CEVAP,SK.AD
							FROM	SinavDers		SD	
							JOIN	SinavAnahtar	SA	ON	SA.ID_SINAVDERS	= SD.ID_SINAVDERS
							JOIN	SinavKitapcik	SK	ON	SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK	
							WHERE	SD.ID_SINAVDERS = '+CAST(@ID_SINAVDERS AS varchar(MAX))+'
							and ('+CAST(@SINAVSORUISLEM AS varchar(MAX))+'=0 OR SA.ID_SINAVANAHTAR not in (select distinct ID_SINAVANAHTAR from SinavSoruIslem))
						) AS xTablo
						PIVOT( MAX(SORUNO) FOR AD IN (' + @cols2 + '))AS p'

						execute(@query)

						select(
							select * from(
								select 
								(
									select * from ##TEMP
									ORDER BY SORUNO_A
									FOR JSON AUTO
								) as t1	,	
								(
									SELECT	DISTINCT SK.AD
									FROM	SinavDers		SD	
									JOIN	SinavAnahtar	SA	ON	SA.ID_SINAVDERS		= SD.ID_SINAVDERS
									JOIN	SinavKitapcik	SK	ON	SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK	
									WHERE	SD.ID_SINAVDERS		=	@ID_SINAVDERS
									FOR JSON AUTO
								) as t2							
							) as tablo FOR JSON AUTO
						) as tt


						DROP TABLE IF EXISTS ##TEMP

				END 
				IF @ISLEM = 28	--	Sınav Listele Pasif Dahil
				BEGIN
					SELECT DISTINCT 
						S.ID_SINAV
						,KOD
						,S.AD
						,K3.AD AS GRUP
						,S.ID_KADEME3
						,Convert(varchar, SINAVTARIH, 103) AS SINAVTARIH
						,DURUM
						,AKTIF 
						,(CASE WHEN  SK.ID_SINAV IS NULL THEN 0 ELSE 1 END ) B_KITAPCIK
						,S.OZEL AS OZEL
						,S.FREKANSHESAPLA AS FREKANSHESAPLA
						,S.PUANGIZLE AS PUANGIZLE
						,S.YUKLEMEKAPAT AS YUKLEMEKAPAT
						,S.[CEVAPANAHTARIYUKLEMEKAPAT]  AS [CEVAPANAHTARIYUKLEMEKAPAT] 
						,S.[ONLINESINAV]  AS [ONLINESINAV] 
						,(SELECT COUNT(1) FROM (SELECT TCKIMLIKNO FROM SinavOgrenci WHERE ID_SINAV = S.ID_SINAV GROUP BY TCKIMLIKNO) XTABLE ) AS KATILIM
						,(SELECT COUNT(DISTINCT TCKIMLIKNO) FROM SinavOgrenciHariciPuanSiralama WHERE ID_SINAV = S.ID_SINAV) AS HARICIOGRSAY
					FROM Sinav S
					INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.ID_KADEME3=S.ID_KADEME3
					LEFT JOIN SinavKitapcik SK ON SK.ID_SINAV=S.ID_SINAV AND SK.AD='B'
					WHERE DONEM = @DONEM and ID_SINAVTURU = @ID_SINAVTURU and S.ID_KADEME3 = @ID_KADEME3
				END
				IF @ISLEM = 29	--	Öğrenci Sil
				BEGIN
					SELECT SO.ID_SINAVOGRENCI, SOT.ID_SINAVOPTIKTEMP 
					INTO #OGRENCIDELETE
					FROM SinavOgrenci SO
					INNER JOIN SinavOptikTemp SOT ON SOT.ID_SINAVOPTIKTEMP = SO.ID_SINAVOPTIKTEMP
					WHERE SO.ID_SINAV = @ID_SINAV AND SO.TCKIMLIKNO IN (SELECT VALUE FROM OPENJSON(@TCLIST))
			
					DELETE FROM SinavOgrenci WHERE ID_SINAVOGRENCI IN (SELECT ID_SINAVOGRENCI FROM #OGRENCIDELETE)
					DELETE FROM SinavOptikTemp WHERE ID_SINAVOPTIKTEMP IN (SELECT ID_SINAVOPTIKTEMP FROM #OGRENCIDELETE)

					DROP TABLE #OGRENCIDELETE

					SELECT 1
				END
				IF @ISLEM = 30	--	Öğrenci Sil
				BEGIN
					UPDATE SinavDers SET TAKMAAD=@TAKMAAD WHERE ID_SINAVDERS=@ID_SINAVDERS
					SELECT 1
				END			
				IF @ISLEM = 31  --  SınavTürü Derslerini Listele
				BEGIN	
					select(
						select * from(
							select 
							(
								SELECT DISTINCT	D.ID_DERS,D.DERSAD AD,D.ID_DERS ID
								FROM	SinavDers			SD	
								JOIN	Sinav				S	ON	S.ID_SINAV	= SD.ID_SINAV
								JOIN	eokul_v2.dbo.Ders	D	ON  D.ID_DERS	= SD.ID_DERS 
																AND D.ID_KADEME3= S.ID_KADEME3
								WHERE	S.ID_SINAVTURU	= @ID_SINAVTURU 
									AND S.ID_KADEME3	= @ID_KADEME3ESKI		
									AND (S.DONEM		= @DONEM		OR @DONEM = '' )
								FOR JSON AUTO
							) as t1								
						) as tablo FOR JSON AUTO
					) as tt
				END 
				IF @ISLEM = 32	--	Sınav Listele Değerlendirilmiş
				BEGIN
					SELECT * FROM
					(
						SELECT DISTINCT 
							S.ID_SINAV
							,KOD
							,S.AD
							,K3.AD AS GRUP
							,S.ID_KADEME3
							,Convert(varchar, SINAVTARIH, 104) AS SINAVTARIH
							,DURUM
							,AKTIF 
							,(CASE WHEN  SK.ID_SINAV IS NULL THEN 0 ELSE 1 END ) B_KITAPCIK
							,S.OZEL AS OZEL
							,S.FREKANSHESAPLA AS FREKANSHESAPLA
							,S.PUANGIZLE AS PUANGIZLE
							,S.YUKLEMEKAPAT AS YUKLEMEKAPAT
							,S.[CEVAPANAHTARIYUKLEMEKAPAT]  AS [CEVAPANAHTARIYUKLEMEKAPAT] 
							,S.[ONLINESINAV]  AS [ONLINESINAV] 
							,(SELECT COUNT(1) FROM (SELECT TCKIMLIKNO FROM SinavOgrenci WHERE ID_SINAV = S.ID_SINAV GROUP BY TCKIMLIKNO) XTABLE ) AS KATILIM
						FROM Sinav S
						INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.ID_KADEME3=S.ID_KADEME3
						LEFT JOIN SinavKitapcik SK ON SK.ID_SINAV=S.ID_SINAV AND SK.AD='B'
						WHERE DONEM = @DONEM and ID_SINAVTURU = @ID_SINAVTURU and S.ID_KADEME3 = @ID_KADEME3 and S.AKTIF = 1 and S.DURUM=2
							and (OZEL=0 OR @OZELSINAVGOR=1)
					)XTABLE
					ORDER BY Convert(datetime, SINAVTARIH, 104) DESC
				END
				IF @ISLEM = 33	--	Sınav Türü Listele Gruba Göre
				BEGIN
					SELECT DISTINCT ST.ID_SINAVTURU, ST.AD, ST.KISAAD FROM  Sinav S 
					INNER JOIN SinavTuru ST ON ST.ID_SINAVTURU=S.ID_SINAVTURU
					WHERE S.ID_KADEME3 = @ID_KADEME3ESKI AND S.AKTIF=1 AND S.DURUM=2
							and (OZEL=0 OR @OZELSINAVGOR=1)
				END
				IF @ISLEM = 34	--	Sınav Türü Listele Gruba Göre
				BEGIN
					IF (@ID_KADEME3ESKI IS NULL OR @ID_KADEME3ESKI='') AND (@TC_OGRENCI IS NOT NULL AND @TC_OGRENCI != '')
					BEGIN
						SELECT @ID_KADEME3ESKI=S.ID_KADEME3
						FROM OkyanusDB.DBO.v3Ogrenci		O
						JOIN OkyanusDB.DBO.v3SubeGrupSinif	S	ON	S.ID_SINIF=O.ID_SINIF 
						WHERE TCKIMLIKNO=@TC_OGRENCI
					END

					SELECT DISTINCT ST.ID_SINAVTURU, ST.AD, ST.KISAAD 
					FROM  Sinav S 
					INNER JOIN SinavTuru ST ON ST.ID_SINAVTURU=S.ID_SINAVTURU
					WHERE S.ID_KADEME3 = @ID_KADEME3ESKI AND S.AKTIF=1 AND S.DURUM=2 AND S.DONEM=@DONEM
							and (OZEL=0 OR @OZELSINAVGOR=1)
				END
				IF @ISLEM = 35	--	Sınav Listele by Grup
				BEGIN
					select * from
					(
						Select distinct s.ID_SINAV, KOD, s.AD, ID_KADEME3, Convert(varchar, SINAVTARIH, 104) as SINAVTARIH, DURUM, st.AD as TUR
						from Sinav			s
						join SinavTuru		st	on st.ID_SINAVTURU=s.ID_SINAVTURU
						join SinavOgrenci	so	on s.ID_SINAV=so.ID_SINAV
						where s.AKTIF=1 and s.DURUM=2 and s.ID_KADEME3=@ID_KADEME3ESKI
							and (OZEL=0 OR @OZELSINAVGOR=1)
							and ((@DONEM='0' and DONEM=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)) or DONEM=@DONEM) 
							and (cast(@ID_SINAVTURU as int)=0 or s.ID_SINAVTURU=cast(@ID_SINAVTURU as int))
					) as XTABLE
					order by cast(SINAVTARIH as date) desc
				END
				IF @ISLEM = 36	--	Sınav Listele by Grup - ÇOKLU
				BEGIN
					select * from
					(
						Select distinct s.ID_SINAV, KOD, s.AD, ID_KADEME3, Convert(varchar, SINAVTARIH, 104) as SINAVTARIH, DURUM, st.AD as TUR
						from Sinav			s
						join SinavTuru		st	on st.ID_SINAVTURU=s.ID_SINAVTURU
						join SinavOgrenci	so	on s.ID_SINAV=so.ID_SINAV
						where s.AKTIF=1 and s.DURUM=2 and s.ID_KADEME3=@ID_KADEME3ESKI
							and (OZEL=0 OR @OZELSINAVGOR=1)
							and ((@DONEM='0' and DONEM=(SELECT DONEM FROM OkyanusDB.dbo.v3AktifDonem WHERE AKTIF=1)) or DONEM=@DONEM) 
							and (S.ID_SINAVTURU IN (SELECT value FROM OPENJSON  ( @ID_SINAVTURUS)))
					) as XTABLE
					order by TUR, cast(SINAVTARIH as date) desc
				END
				IF @ISLEM = 37	--	Sınav Özellik Güncelle
				BEGIN
					DECLARE @SQL37 VARCHAR(MAX) ='
						UPDATE Sinav
						SET '+@DURUM+'='+CAST(@DURUMDEGER AS varchar)+'
						WHERE ID_SINAV='+CAST(@ID_SINAV AS varchar)+'
					'
					PRINT (@SQL37)
					EXEC (@SQL37)

					SELECT 1
					
				
				END
				IF @ISLEM = 38	--	Sınav Listele KADEME DONEM
				BEGIN

					SELECT * FROM
					(
						SELECT DISTINCT 
							S.ID_SINAV
							,KOD
							,S.AD
							,K3.AD AS GRUP
							,S.ID_KADEME3
							,Convert(varchar, SINAVTARIH, 104) AS SINAVTARIH
							,DURUM
							,AKTIF 
							,(CASE WHEN  SK.ID_SINAV IS NULL THEN 0 ELSE 1 END ) B_KITAPCIK
							,S.OZEL AS OZEL
							,S.FREKANSHESAPLA AS FREKANSHESAPLA
							,S.PUANGIZLE AS PUANGIZLE
							,S.YUKLEMEKAPAT AS YUKLEMEKAPAT
							,S.CEVAPANAHTARIYUKLEMEKAPAT AS CEVAPANAHTARIYUKLEMEKAPAT
							,S.ONLINESINAV AS ONLINESINAV
							,(SELECT COUNT(1) FROM (SELECT TCKIMLIKNO FROM SinavOgrenci WHERE ID_SINAV = S.ID_SINAV GROUP BY TCKIMLIKNO) XTABLE ) AS KATILIM
							,(SELECT COUNT(DISTINCT TCKIMLIKNO) FROM SinavOgrenciHariciPuanSiralama WHERE ID_SINAV = S.ID_SINAV) AS HARICIOGRSAY
						FROM Sinav S
						INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.ID_KADEME3=S.ID_KADEME3
						LEFT JOIN SinavKitapcik SK ON SK.ID_SINAV=S.ID_SINAV AND SK.AD IN ('A','B')
						WHERE DONEM = @DONEM and ID_SINAVTURU = @ID_SINAVTURU and S.ID_KADEME3 = @ID_KADEME3 and S.AKTIF = 1
						AND (S.OZEL=0 OR @OZELSINAVGOR=1)
					)XTABLE
					ORDER BY Convert(datetime, SINAVTARIH, 104) DESC
				END
				IF @ISLEM = 39	--	sınav ders liste
				BEGIN
					SELECT   SD.*
							,SA.SORUNO
							,SA.CEVAP
							,SA.KOD
							,(SELECT SORUNO FROM SinavAnahtar GSA JOIN SinavKitapcik GSK ON GSK.ID_SINAVKITAPCIK = GSA.ID_SINAVKITAPCIK WHERE GSA.ID_SINAVDERS = SD.ID_SINAVDERS AND GSA.SORUNO_A = SA.SORUNO AND GSK.AD = 'B' ) B_KARSILIK	
							,(SELECT SORUNO FROM SinavAnahtar GSA JOIN SinavKitapcik GSK ON GSK.ID_SINAVKITAPCIK = GSA.ID_SINAVKITAPCIK WHERE GSA.ID_SINAVDERS = SD.ID_SINAVDERS AND GSA.SORUNO_A = SA.SORUNO AND GSK.AD = 'C' ) C_KARSILIK	
							,(SELECT SORUNO FROM SinavAnahtar GSA JOIN SinavKitapcik GSK ON GSK.ID_SINAVKITAPCIK = GSA.ID_SINAVKITAPCIK WHERE GSA.ID_SINAVDERS = SD.ID_SINAVDERS AND GSA.SORUNO_A = SA.SORUNO AND GSK.AD = 'D' ) D_KARSILIK		
							,SA.ID_BILGI
							,SA.ID_BILISSELSUREC
							,SA.ID_SORUBANKASI
					FROM Sinav							S
					JOIN SinavDers						SD	ON	SD.ID_SINAV			= S.ID_SINAV
					JOIN OPENJSON (@ID_SINAVDERSLIST)	DL	ON	DL.VALUE			= SD.ID_SINAVDERS
					JOIN SinavAnahtar					SA	ON	SA.ID_SINAVDERS		= SD.ID_SINAVDERS 
					JOIN SinavKitapcik					SK	ON	SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK
															AND	SK.AD				= 'A'
					WHERE	S.ID_SINAV	= @ID_SINAV
					GROUP BY SD.ID_SINAVDERS,BOLUMNO,ID_DERS,TAKMAAD,SD.ID_SINAV,SA.CEVAP,SA.KOD,SA.SORUNO,SA.ID_BILGI,SA.ID_BILISSELSUREC	,SA.ID_SORUBANKASI	
					ORDER BY BOLUMNO,SORUNO
				END
				IF @ISLEM = 40	--	CEVAP ANAHTARI EXCEL YUKLE
				BEGIN
					--declare @SQLJSON varchar(max)= '[{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"1","CEVAP":"B","B_KARSILIK":"20","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"2","CEVAP":"E","B_KARSILIK":"19","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"3","CEVAP":"E","B_KARSILIK":"18","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"4","CEVAP":"A","B_KARSILIK":"17","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL107001007"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"5","CEVAP":"C","B_KARSILIK":"16","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"6","CEVAP":"D","B_KARSILIK":"15","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL103001019"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"7","CEVAP":"A","B_KARSILIK":"14","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110001001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"8","CEVAP":"B","B_KARSILIK":"13","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL107001005"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"9","CEVAP":"D","B_KARSILIK":"12","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL110002001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"10","CEVAP":"D","B_KARSILIK":"11","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL103001008"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"11","CEVAP":"C","B_KARSILIK":"10","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL107002011"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"12","CEVAP":"E","B_KARSILIK":"9","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL111003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"13","CEVAP":"D","B_KARSILIK":"8","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL111003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"14","CEVAP":"A","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL111003001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"15","CEVAP":"E","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL112001001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"16","CEVAP":"B","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL112001001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"17","CEVAP":"B","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL107002011"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"18","CEVAP":"C","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL105009001"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"19","CEVAP":"A","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL105001007"},{"ID_SINAVDERS":"19944","TAKMAAD":"Türk Dili Edebiyatı","SORUNO":"20","CEVAP":"C","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİL107001007"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"1","CEVAP":"E","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK316003006"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"2","CEVAP":"D","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK316003003"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"3","CEVAP":"E","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK315002010"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"4","CEVAP":"A","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK314003001"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"5","CEVAP":"C","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK317003001"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"6","CEVAP":"B","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK313004001"},{"ID_SINAVDERS":"19945","TAKMAAD":"Tarih","SORUNO":"7","CEVAP":"D","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"İNK315005001"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"1","CEVAP":"D","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ423002003"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"2","CEVAP":"D","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ401002001"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"3","CEVAP":"B","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ416005001"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"4","CEVAP":"C","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ423002003"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"5","CEVAP":"A","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ423002003"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"6","CEVAP":"E","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ406002001"},{"ID_SINAVDERS":"19946","TAKMAAD":"Coğrafya","SORUNO":"7","CEVAP":"A","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"COĞ406002001"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"1","CEVAP":"C","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN804001002"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"2","CEVAP":"A","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN804004004"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"3","CEVAP":"D","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN806006001"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"4","CEVAP":"C","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN806008002"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"5","CEVAP":"B","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN805004001"},{"ID_SINAVDERS":"19947","TAKMAAD":"Din Kült. Ve A. B.","SORUNO":"6","CEVAP":"E","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"DİN502002001"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"1","CEVAP":"D","B_KARSILIK":"20","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201005002"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"2","CEVAP":"B","B_KARSILIK":"19","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201005004"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"3","CEVAP":"C","B_KARSILIK":"18","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201005002"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"4","CEVAP":"E","B_KARSILIK":"17","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201005002"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"5","CEVAP":"D","B_KARSILIK":"16","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201006004"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"6","CEVAP":"D","B_KARSILIK":"15","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201006003"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"7","CEVAP":"E","B_KARSILIK":"14","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT203002005"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"8","CEVAP":"E","B_KARSILIK":"13","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT206001001"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"9","CEVAP":"C","B_KARSILIK":"12","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001005"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"10","CEVAP":"A","B_KARSILIK":"11","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001004"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"11","CEVAP":"E","B_KARSILIK":"10","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202006001"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"12","CEVAP":"A","B_KARSILIK":"9","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT206001002"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"13","CEVAP":"B","B_KARSILIK":"8","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202006001"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"14","CEVAP":"B","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202006007"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"15","CEVAP":"E","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001026"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"16","CEVAP":"A","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202003004"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"17","CEVAP":"B","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT201010001"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"18","CEVAP":"B","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001026"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"19","CEVAP":"C","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001026"},{"ID_SINAVDERS":"19948","TAKMAAD":"Matematik","SORUNO":"20","CEVAP":"D","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"MAT202001026"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"1","CEVAP":"C","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ607011006"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"2","CEVAP":"C","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ603003001"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"3","CEVAP":"A","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ606002003"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"4","CEVAP":"B","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ608002003"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"5","CEVAP":"D","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ607004001"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"6","CEVAP":"B","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ604001002"},{"ID_SINAVDERS":"19949","TAKMAAD":"Fizik","SORUNO":"7","CEVAP":"B","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"FİZ607011006"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"1","CEVAP":"A","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM701003005"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"2","CEVAP":"D","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM703003001"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"3","CEVAP":"E","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM703005006"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"4","CEVAP":"B","B_KARSILIK":"7","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM702003004"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"5","CEVAP":"C","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM705008002"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"6","CEVAP":"A","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM703005006"},{"ID_SINAVDERS":"19950","TAKMAAD":"Kimya","SORUNO":"7","CEVAP":"E","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"KİM701006001"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"1","CEVAP":"D","B_KARSILIK":"3","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY806001001"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"2","CEVAP":"C","B_KARSILIK":"5","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY806001005"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"3","CEVAP":"A","B_KARSILIK":"2","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY807002001"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"4","CEVAP":"E","B_KARSILIK":"6","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY804002005"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"5","CEVAP":"E","B_KARSILIK":"4","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY810001001"},{"ID_SINAVDERS":"19951","TAKMAAD":"Biyoloji","SORUNO":"6","CEVAP":"D","B_KARSILIK":"1","C_KARSILIK":"","D_KARSILIK":"","KOD":"BİY810001004"}]'
				
					DROP TABLE IF EXISTS #SQLJSON
					DROP TABLE IF EXISTS #BKONTROL
					DROP TABLE IF EXISTS #CKONTROL
					DROP TABLE IF EXISTS #DKONTROL


					SELECT ID_SINAVDERS, TAKMAAD, SORUNO, CEVAP, B_KARSILIK,C_KARSILIK,D_KARSILIK, KOD, ID_BILGI, ID_BILISSELSUREC, ID_SORUBANKASI
					INTO #SQLJSON 
					FROM OPENJSON(@SQLJSON)
					with
					(
						ID_SINAVDERS [int],
						TAKMAAD [varchar](MAX),
						SORUNO [int],
						CEVAP [varchar](MAX),
						B_KARSILIK [int],
						C_KARSILIK [int],
						D_KARSILIK [int],
						KOD [varchar](MAX),
						ID_BILGI INT,
						ID_BILISSELSUREC INT,
						ID_SORUBANKASI INT
					) AS J
				
					DECLARE @RESULT BIT = 0
				
					--SELECT SA.CEVAP,SA.SORUNO_A,SA.KOD, '' AS SS, J.SORUNO,J.CEVAP,J.KOD,J.B_KARSILIK,J.C_KARSILIK,J.D_KARSILIK
				
					IF NOT EXISTS (SELECT TOP 1 1 FROM #SQLJSON WHERE CEVAP NOT IN ('A','B','C','D','E','F'))
					BEGIN

						UPDATE  SA
						SET
							 SA.CEVAP				= J.CEVAP
							,SA.KOD					= J.KOD
							,SA.ID_BILGI			= J.ID_BILGI 
							,SA.ID_BILISSELSUREC	= J.ID_BILISSELSUREC 
							,SA.ID_SORUBANKASI		= SSD.ID_SORU --J.ID_SORUBANKASI
						FROM SinavDers					SD	
						JOIN #SQLJSON					J	ON	J.ID_SINAVDERS		= SD.ID_SINAVDERS
						JOIN SinavAnahtar				SA	ON	SA.ID_SINAVDERS		= SD.ID_SINAVDERS
															AND SA.SORUNO_A			= J.SORUNO 
						JOIN SinavKitapcik				SK	ON	SK.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK 
															AND	SK.AD				= 'A'
					LEFT JOIN SoruBankasi.dbo.Soru		SS	ON	SS.ID_SORU			= J.ID_SORUBANKASI
					LEFT JOIN Sinav						JS	ON	JS.ID_SINAV			= SD.ID_SINAV
					LEFT JOIN SoruBankasi.dbo.SoruDetay SSD ON	SSD.ID_SORU			= SS.ID_SORU
															AND JS.ID_KADEME3		= SSD.ID_SINAVGRUP
															AND SSD.SILINDI			= 0
															AND SSD.UNITE			= J.KOD
						WHERE SD.ID_SINAV = @ID_SINAV

						BEGIN	-- KITAPÇIK YANLIŞ KONTROL
					
							SELECT 
									S.SORUNO,
									G.B_KARSILIK,
									G.ID_SINAVDERS,
									COUNT(G.B_KARSILIK) SAY
							INTO #BKONTROL
							FROM #SQLJSON S
					   LEFT JOIN #SQLJSON G	ON	S.SORUNO		= G.B_KARSILIK 
											AND S.ID_SINAVDERS	= G.ID_SINAVDERS
							GROUP BY 
									S.SORUNO,
									G.B_KARSILIK,
									G.ID_SINAVDERS
							HAVING COUNT(G.B_KARSILIK)=0

				
							SELECT 
									S.SORUNO,
									G.C_KARSILIK,
									G.ID_SINAVDERS,
									COUNT(G.C_KARSILIK) SAY
							INTO #CKONTROL
							FROM #SQLJSON S
					   LEFT JOIN #SQLJSON G	ON	S.SORUNO		= G.C_KARSILIK 
											AND S.ID_SINAVDERS	= G.ID_SINAVDERS
							GROUP BY 
									S.SORUNO,
									G.C_KARSILIK,
									G.ID_SINAVDERS
							HAVING COUNT(G.C_KARSILIK)=0

				
							SELECT 
								S.SORUNO,
								G.D_KARSILIK,
								G.ID_SINAVDERS,
								COUNT(G.D_KARSILIK) SAY
						INTO #DKONTROL
						FROM #SQLJSON S
				   LEFT JOIN #SQLJSON G	ON	S.SORUNO		= G.D_KARSILIK 
										AND S.ID_SINAVDERS	= G.ID_SINAVDERS
						GROUP BY 
								S.SORUNO,
								G.D_KARSILIK,
								G.ID_SINAVDERS
						HAVING COUNT(G.D_KARSILIK)=0

						END
					
						IF NOT EXISTS (SELECT * FROM #BKONTROL)
						BEGIN
							--SELECT SA.SORUNO,SA.CEVAP,SA.KOD,SA.SORUNO_A,J.SORUNO,J.CEVAP,J.KOD,J.B_KARSILIK
							UPDATE	SA
							SET		SA.SORUNO	= J.B_KARSILIK
							FROM	SinavAnahtar		SA
							JOIN	SinavKitapcik		KB	ON	KB.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK AND KB.AD = 'B' 
							JOIN	#SQLJSON			J	ON	J.ID_SINAVDERS		= SA.ID_SINAVDERS 
															AND J.SORUNO			= SA.SORUNO_A

						END		
					
						IF NOT EXISTS (SELECT * FROM #CKONTROL)
						BEGIN
							--SELECT SA.SORUNO,SA.CEVAP,SA.KOD,SA.SORUNO_A,J.SORUNO,J.CEVAP,J.KOD,J.C_KARSILIK
							UPDATE	SA
							SET		SA.SORUNO	= J.C_KARSILIK
							FROM SinavAnahtar		SA
							JOIN SinavKitapcik		KB	ON	KB.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK AND KB.AD = 'C' 
							JOIN #SQLJSON			J	ON	J.ID_SINAVDERS		= SA.ID_SINAVDERS 
														AND J.SORUNO			= SA.SORUNO_A

						END		
					
						IF NOT EXISTS (SELECT * FROM #DKONTROL)
						BEGIN
							
							--SELECT SA.SORUNO,SA.CEVAP,SA.KOD,SA.SORUNO_A,J.SORUNO,J.CEVAP,J.KOD,J.D_KARSILIK
							UPDATE	SA
							SET		SA.SORUNO	= J.D_KARSILIK
							FROM SinavAnahtar		SA
							JOIN SinavKitapcik		KB	ON	KB.ID_SINAVKITAPCIK	= SA.ID_SINAVKITAPCIK AND KB.AD = 'D' 
							JOIN #SQLJSON			J	ON	J.ID_SINAVDERS		= SA.ID_SINAVDERS 
														AND J.SORUNO			= SA.SORUNO_A
						END

					SET @RESULT = 1

				END
				

					SELECT @RESULT


					DROP TABLE IF EXISTS #SQLJSON
					DROP TABLE IF EXISTS #BKONTROL
					DROP TABLE IF EXISTS #CKONTROL
					DROP TABLE IF EXISTS #DKONTROL


				END
				IF @ISLEM = 41	--	Sınav/Yazılı Listele Tarihe Göre
				BEGIN

						SELECT ID_SINAV,S.AD,K.ID_KADEME3, K.AD KADEME3, S.ID_SINAVTURU, ST.KISAAD SINAVTURU
						INTO #SINAVLISTESI
						FROM Sinav		S
						JOIN v3Kademe3	K	ON	K.ID_KADEME3	= S.ID_KADEME3
						JOIN SinavTuru	ST	ON	ST.ID_SINAVTURU	= S.ID_SINAVTURU
						WHERE	S.SINAVTARIH	= @TARIH
							AND S.DONEM			= @DONEM
							AND (K.ID_KADEME3	= @ID_KADEME3 OR @ID_KADEME3 = 0 OR @ID_KADEME3 IS NULL )
							AND	S.AKTIF			= 1
					UNION ALL
						SELECT ID_YAZILI,Y.AD,K.ID_KADEME3, K.AD KADEME3, 0 ID_SINAVTURU,'YZL' SINAVTURU
						FROM Yazili		Y
						JOIN v3Kademe3	K	ON	K.ID_KADEME3	= Y.ID_KADEME3
						WHERE	Y.YAZILITARIH	= @TARIH
							AND Y.DONEM			= @DONEM
							AND (K.ID_KADEME3	= @ID_KADEME3 OR @ID_KADEME3 = 0 OR @ID_KADEME3 IS NULL )
							AND	Y.AKTIF			= 1
					ORDER BY ID_KADEME3 

					SELECT (
						SELECT *
						FROM #SINAVLISTESI
						for json path, INCLUDE_NULL_VALUES 
					)AS SINAVLISTESI

					DROP TABLE #SINAVLISTESI					

				END
				IF @ISLEM = 42	--	Bursluluk Dosya Liste
				BEGIN

					SELECT ISNULL(
						(	SELECT *
							FROM BurslulukDosya
							WHERE (DONEM = @DONEM OR ((@DONEM IS NULL OR @DONEM = '') AND (@DONEM =@AKTIFDONEM )))
								AND AKTIF = 1
							for json auto
						)
					,'[]')AS DOSYALISTESI

				END
				IF @ISLEM = 43	--	Sınav Listele Multi Sınav Turu
				BEGIN
					SELECT * FROM
					(
						SELECT DISTINCT 
							S.ID_SINAV
							,KOD
							,S.AD
							,K3.AD AS GRUP
							,S.ID_KADEME3
							,Convert(varchar, SINAVTARIH, 104) AS SINAVTARIH
							,DURUM
							,AKTIF 
							,(CASE WHEN  SK.ID_SINAV IS NULL THEN 0 ELSE 1 END ) B_KITAPCIK
							,S.OZEL AS OZEL
							,S.FREKANSHESAPLA AS FREKANSHESAPLA
							,S.PUANGIZLE AS PUANGIZLE
							,S.YUKLEMEKAPAT AS YUKLEMEKAPAT
							,S.[CEVAPANAHTARIYUKLEMEKAPAT]  AS [CEVAPANAHTARIYUKLEMEKAPAT] 
							,S.ONLINESINAV  AS ONLINESINAV 
							,(SELECT COUNT(1) FROM (SELECT TCKIMLIKNO FROM SinavOgrenci WHERE ID_SINAV = S.ID_SINAV GROUP BY TCKIMLIKNO) XTABLE ) AS KATILIM
						FROM Sinav S
						INNER JOIN OkyanusDB.dbo.v3Kademe3 K3 ON K3.ID_KADEME3=S.ID_KADEME3
						LEFT JOIN SinavKitapcik SK ON SK.ID_SINAV=S.ID_SINAV AND SK.AD='B'
						WHERE DONEM = @DONEM 
						and S.ID_KADEME3 = @ID_KADEME3ESKI 
						and (S.ID_SINAVTURU IN (SELECT value FROM OPENJSON  ( @ID_SINAVTURUS)))
						and S.AKTIF = 1 
						and S.DURUM = 2
						and (OZEL = 0 OR @OZELSINAVGOR=1)
					)XTABLE
					ORDER BY Convert(datetime, SINAVTARIH, 104) DESC
				END
				IF @ISLEM = 44	--	AYT için TYT Seçim Türü
				BEGIN
					SELECT 
					(
						SELECT DISTINCT TST.ID_TYTSECIMTUR, TST.AD, CASE WHEN STST.ID_SINAV IS NULL THEN 0 ELSE 1 END SELECTED, ISNULL(STST.PUAN, 0) AS PUAN
						FROM TYTSecimTur TST
						LEFT JOIN SinavTYTSecimTurKriter STST ON STST.ID_TYTSECIMTUR = TST.ID_TYTSECIMTUR AND STST.ID_SINAV = @ID_SINAV
						FOR JSON PATH
					)
				END
				IF @ISLEM = 45	--	AYT için TYT Seçim Kriter Kaydet
				BEGIN
					DELETE FROM SinavTYTSecimTurKriter WHERE ID_SINAV = @ID_SINAV

					INSERT INTO SinavTYTSecimTurKriter (ID_SINAV, ID_TYTSECIMTUR, PUAN)
					VALUES (@ID_SINAV, @ID_TYTSECIMTUR, ISNULL(REPLACE(@BARAJ,',','.'),0.0))
				END
				IF @ISLEM = 46	--	SORU BANKASI SORU LISTESI
				BEGIN
					DECLARE		@DISPLAYLENGTH		INT				= 0,
								@DISPLAYSTART		INT				= 0,
								@WHERECLAUSE		VARCHAR(MAX)	= '',
								@ORDERBYCLAUSE		VARCHAR(MAX)	= '',
								@FILTER				VARCHAR(MAX)	= '',
								@TABLENAME			VARCHAR(MAX)	= 'vw_Soru',
								@COLUMNNAME			VARCHAR(MAX)	= '',
								@COLUMNS			VARCHAR(MAX)	= ''	

					SET @COLUMNNAME		= (SELECT COLUMN_NAME FROM SoruBankasi.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TABLENAME AND TABLE_SCHEMA='dbo' AND ORDINAL_POSITION = (SELECT CASE WHEN JSON_Value (VALUE, '$.column') = '0' THEN '1' ELSE JSON_Value (VALUE, '$.column') END FROM OPENJSON(@SQLJSON,'$.order')))

					SET @ORDERBYCLAUSE	= (SELECT ' ORDER BY '+ @COLUMNNAME + ' ' + J.TUR FROM (SELECT JSON_Value (value, '$.column') AS SIRA, JSON_Value (value, '$.dir') AS TUR FROM OPENJSON(@SQLJSON,'$.order')) J)
					SET @WHERECLAUSE	= (SELECT value FROM OPENJSON(@SQLJSON, '$.search') WITH (value VARCHAR(MAX)))
					SET @DISPLAYSTART	= (SELECT start FROM OPENJSON(@SQLJSON) WITH (start INT)) + 1
					SET @DISPLAYLENGTH	= (SELECT length FROM OPENJSON(@SQLJSON) WITH (length INT)) - 1

					IF	@WHERECLAUSE=''
					BEGIN
						SET @WHERECLAUSE = '
							WHERE	SILINDI			= 0  
								AND ID_HAREKETDURUM	= 13 
								AND ID_SORUTUR		= 4								
								AND (SNV.ID_SINAV	= ' + CAST(@ID_SINAV AS VARCHAR) + ' OR ' + CAST(@ID_SINAV AS VARCHAR) + ' = 0)
								AND (	(	(''' + @KOD + ''' IS NULL OR ''' + @KOD + ''' = '''') 
										AND X.ID_DERS	= ' + CAST(@ID_DERS AS VARCHAR) + '
										)
									OR ((''' + @KOD + ''' IS not NULL and ''' + @KOD + ''' != '''') and (X.UNITE LIKE ''%' + @KOD + '%''))
								)'								
								--AND SNV.ID_SINAV	= ' + CAST(@ID_SINAV AS VARCHAR) + '
								--AND UNITE			= ''' + @KOD +''''
					END
					ELSE
					BEGIN
						SET @COLUMNS = ''
				
						SET @COLUMNS = '(U.AD LIKE ''%' + @WHERECLAUSE + '%'' OR ' + (SELECT ' X.' + COLUMN_NAME + ' LIKE ''%' + @WHERECLAUSE + '%'' OR' AS [text()] 
											FROM SoruBankasi.INFORMATION_SCHEMA.COLUMNS 
											WHERE	TABLE_NAME		= @TABLENAME 
												AND TABLE_SCHEMA	= 'dbo'
											FOR XML PATH ('')) + ')'

						SET @COLUMNS = SUBSTRING(@COLUMNS, 1, LEN(@COLUMNS)-3) + ') '
						SET @WHERECLAUSE = 'WHERE ' + @COLUMNS

						SET @WHERECLAUSE = @WHERECLAUSE + '	
								AND	X.SILINDI			= 0  
								AND X.ID_HAREKETDURUM	= 13 
								AND X.ID_SORUTUR		= 4
								AND SNV.ID_SINAV		= ' + CAST(@ID_SINAV AS VARCHAR) + ' 
								AND (''' + @KOD + ''' IS NULL OR ''' + @KOD + ''' = '''' OR X.UNITE LIKE ''%' + @KOD + '%'') '
					END

					DECLARE @LINK VARCHAR(MAX)=  'SoruBankasi/'
					SET @QUERY='
								SELECT * INTO #TEMP FROM   
								(
									SELECT ROW_NUMBER() OVER(' + @ORDERBYCLAUSE + ') AS RowNumber, * 
									FROM(
										SELECT DISTINCT X.*,UNITEKOD = U.KOD, UNITEAD = U.AD
										FROM ' + @TABLENAME + ' X
										JOIN Sinav				SNV	ON	SNV.ID_KADEME3	= X.ID_SINAVGRUP
										JOIN OkyanusDB.dbo.v3SinavDersUnite U ON U.KOD	= X.UNITE
										' + @WHERECLAUSE + '
									) 
								RawResults) DATA 


								DECLARE @DATA VARCHAR(MAX)=(
										SELECT	RowNumber
												,ID_SORU
												,UNITEKOD
												,UNITEAD
												,	''Soru Türü: '' + ISNULL(SORUTUR, '''') + ''<br />'' +
													''Grup: '' + ISNULL(SINAVGRUP, '''') + ''<br />'' +
													''Ders: '' + ISNULL(DERSAD, '''') + ''<br />'' +
													''Zorluk: '' + ISNULL(ZORLUKTUR, '''') + ''<br />'' +
													''Durum: '' + ISNULL(HAREKETDURUM, '''') + ''<br />'' +
													''Yazar: '' + ISNULL(KULLANICIAD, '''') + ''<br />'' +
													''Tarih: '' + CONVERT(VARCHAR, KYE_TARIH, 104)+ ''<br />'' +
													''Kod: '' + ISNULL(UNITEKOD, '''')  AS SORUBILGILERI 
												,SORUHTML	= (SELECT '''+ @LINK +''' + SORUHTML FROM SoruBankasi.dbo.SoruHtml WHERE ID_SORU = S.ID_SORU AND ID_TASLAKTUR=20) 
												,CEVAP		= (SELECT CEVAPNO,'''+ @LINK +''' + CEVAPHTML AS CEVAPHTML, DEGER FROM SoruBankasi.dbo.CEVAP WHERE ID_SORU = S.ID_SORU ORDER BY ID_CEVAP FOR JSON PATH, INCLUDE_NULL_VALUES) 
												,ID_SORUTUR
												,USTSORUHTML= CASE	WHEN ID_USTSORU IS NOT NULL THEN (SELECT SORUHTML FROM SoruBankasi.dbo.SoruHtml WHERE ID_SORU = S.ID_USTSORU AND ID_TASLAKTUR=20) 
																	ELSE '''' 
																END 
												,COZUM		= (SELECT COZUMHTML, ID_COZUMTUR FROM SoruBankasi.dbo.SoruCozum WHERE ID_SORU = S.ID_SORU ORDER BY SIRA FOR JSON PATH, INCLUDE_NULL_VALUES)
												,KULLANIM	= (	SELECT SN.AD, CONVERT(VARCHAR, SN.KYE_TARIH, 104) AS KYE_TARIH, SS.KITAPCIK, SS.SORUNO FROM SoruBankasi.dbo.SinavSoru SS
																	INNER JOIN SoruBankasi.dbo.Sinav SN ON SN.ID_SINAV = SS.ID_SINAV
																	WHERE SN.SILINDI = 0 AND SS.ID_SORU = S.ID_SORU 
																	ORDER BY SS.KITAPCIK
																	FOR JSON PATH, INCLUDE_NULL_VALUES)
												,ID_HAREKETDURUM
												,ALTSORU = (SELECT SUBS.ID_SORU, SORUTUR, DERSAD, ZORLUKTUR FROM '+@TABLENAME+' SUBS
															WHERE SUBS.ID_USTSORU = S.ID_SORU
															FOR JSON PATH, INCLUDE_NULL_VALUES)
												,SORUTUR, DERSAD, ZORLUKTUR
												,SORUGECMIS = ISNULL((
													SELECT DISTINCT SN.ID_SINAV, SN.DONEM, SN.AD SINAVAD, ST.KISAAD SINAVTURU, SN.SINAVTARIH
													FROM Pusulam.dbo.Sinav			SN
													JOIN Pusulam.dbo.SinavTuru		ST	ON	ST.ID_SINAVTURU	= SN.ID_SINAVTURU
													JOIN Pusulam.dbo.SinavDers		SD	ON	SD.ID_SINAV		= SN.ID_SINAV
													JOIN Pusulam.dbo.SinavAnahtar	SA	ON	SA.ID_SINAVDERS	= SD.ID_SINAVDERS
													WHERE SA.ID_SORUBANKASI = S.ID_SORU
													ORDER BY SN.DONEM DESC, SN.SINAVTARIH
													FOR JSON PATH										
												),''[]'')
												,ID_SORUDUZEY
										FROM #TEMP S
										WHERE RowNumber BETWEEN '+CONVERT(VARCHAR(MAX), @DISPLAYSTART)+' AND '+CONVERT(VARCHAR(MAX), (@DISPLAYSTART + @DISPLAYLENGTH))+ 'FOR JSON AUTO, INCLUDE_NULL_VALUES
								)

								SELECT(
									SELECT (SELECT draw FROM OPENJSON(''' + @SQLJSON + ''') WITH(draw INT)) AS draw, 
									(SELECT COUNT(1) FROM ' + @TABLENAME + ') AS recordsTotal, 
									(SELECT COUNT(1) FROM #TEMP) AS recordsFiltered, 
									@DATA AS data FOR JSON PATH, INCLUDE_NULL_VALUES
								) AS JSONDATA

								DROP TABLE #TEMP
					'
					print @QUERY
					EXECUTE (@QUERY)
				

				END
				IF @ISLEM = 47	--	SORU BANKASI ID KONTROL
				BEGIN
					SELECT ISNULL((
							SELECT S.ID_SORU, UNITEKOD = U.KOD, UNITEAD = U.AD, S.ID_SORUDUZEY, CEVAP = (SELECT CASE CEVAPNO WHEN 1 THEN 'A' WHEN 2 THEN 'B' WHEN 3 THEN 'C' WHEN 4 THEN 'D' WHEN 5 THEN 'E' ELSE '' END FROM SoruBankasi.dbo.CEVAP WHERE ID_SORU = S.ID_SORU AND DEGER = 1)
												,SORUGECMIS = ISNULL((
													SELECT DISTINCT SN.ID_SINAV, SN.DONEM, SN.AD SINAVAD, ST.KISAAD SINAVTURU, SN.SINAVTARIH
													FROM Pusulam.dbo.Sinav			SN
													JOIN Pusulam.dbo.SinavTuru		ST	ON	ST.ID_SINAVTURU	= SN.ID_SINAVTURU
													JOIN Pusulam.dbo.SinavDers		SD	ON	SD.ID_SINAV		= SN.ID_SINAV
													JOIN Pusulam.dbo.SinavAnahtar	SA	ON	SA.ID_SINAVDERS	= SD.ID_SINAVDERS
													WHERE SA.ID_SORUBANKASI = S.ID_SORU
													ORDER BY SN.DONEM DESC, SN.SINAVTARIH
													FOR JSON PATH										
												),'[]')
							FROM vw_Soru						S 
							JOIN Sinav							SNV	ON	SNV.ID_KADEME3	= S.ID_SINAVGRUP
							JOIN OkyanusDB.dbo.v3SinavDersUnite	U	ON	U.KOD			= S.UNITE
							WHERE	S.ID_SORU		= @ID_SORUBANKASI
								AND	S.SILINDI		= 0
								AND SNV.ID_SINAV	= @ID_SINAV
								AND S.ID_SORUTUR	= 4
								AND	(@UNITEKOD	IS NULL OR @UNITEKOD ='' OR S.UNITE = @UNITEKOD)
							FOR JSON PATH
					),'[]')
				END
				IF @ISLEM = 48	--	ONLINE SINAV OPTIK HAZIRLAMA
				BEGIN
					
					DROP TABLE IF EXISTS #SinavOptikSabit
					DROP TABLE IF EXISTS #SinavOptikDers
					DROP TABLE IF EXISTS #OGRENCILIST
					DROP TABLE IF EXISTS #LINE

					SELECT	 SOS.ID_SINAVOPTIK
							,SOS.BASLANGIC
							,SOS.OPTIKKARAKTERSAYISI
					INTO #SinavOptikSabit
					FROM Sinav				S
					JOIN SinavOptikSabit	SOS	ON	SOS.ID_SINAV	= S.ID_SINAV
					WHERE S.ID_SINAV = @ID_SINAV


					SELECT	 SD.BOLUMNO
							,SD.ID_SINAVDERS
							,SOD.OTURUM
							,SOD.BASLANGIC
							,SOD.OPTIKKARAKTERSAYISI
					INTO #SinavOptikDers
					FROM Sinav			S
					JOIN SinavDers		SD	ON	SD.ID_SINAV		= S.ID_SINAV
					JOIN SinavOptikDers	SOD	ON	SOD.ID_SINAVDERS= SD.ID_SINAVDERS
					WHERE	S.ID_SINAV	= @ID_SINAV
						--AND SOD.OTURUM	= @SINAVOTURUM
					ORDER BY BASLANGIC


					SELECT DISTINCT SO.TCKIMLIKNO
					INTO #OGRENCILIST
					FROM Tbl_OnlineSinavOgrenci			SO
					JOIN Tbl_OnlineSinavOgrenciCevap	SOC	ON	SOC.ID_ONLINESINAVOGRENCI	= SO.ID_ONLINESINAVOGRENCI
					WHERE	SO.ID_SINAV = @ID_SINAV
						AND SO.AKTIF	= 1
						AND SOC.AKTIF	= 1


					--DECLARE @BOSLUKSAYISI	INT	= (SELECT TOP 1 BASLANGIC + OPTIKKARAKTERSAYISI FROM #SinavOptikSabit ORDER BY BASLANGIC DESC)
					DECLARE @BOSLUKSAYISI	INT	= (SELECT TOP 1 BASLANGIC FROM #SinavOptikDers ORDER BY BASLANGIC)
					DECLARE @TCSIRASI		INT	= (SELECT BASLANGIC FROM #SinavOptikSabit WHERE ID_SINAVOPTIK = 1)
					DECLARE @KITAPCIKSIRASI	INT	= (SELECT BASLANGIC FROM #SinavOptikSabit WHERE ID_SINAVOPTIK = 4)

					SELECT LINE = STUFF(dbo.fn_BoslukBirak(@TCSIRASI - 1) + SO.TCKIMLIKNO + dbo.fn_BoslukBirak(@BOSLUKSAYISI - (@TCSIRASI + 11)), @KITAPCIKSIRASI, 1, 'A')
						,SOD.BASLANGIC
						,TEST = IIF( SOD.OTURUM = @SINAVOTURUM, (ISNULL(REPLACE(STUFF((		
									SELECT IIF( C.CEVAPNO IS NOT NULL
												,  CASE WHEN C.CEVAPNO = 1 THEN 'A'
														WHEN C.CEVAPNO = 2 THEN 'B'
														WHEN C.CEVAPNO = 3 THEN 'C'
														WHEN C.CEVAPNO = 4 THEN 'D'
														WHEN C.CEVAPNO = 5 THEN 'E'
														ELSE '-'
													END
												, '-')
									FROM SinavAnahtar						SA	
									LEFT JOIN Tbl_OnlineSinavOgrenciCevap	OC	ON	OC.ID_ONLINESINAVOGRENCI= SO.ID_ONLINESINAVOGRENCI
																				AND SA.ID_SINAVANAHTAR		= OC.ID_SINAVANAHTAR
																				AND OC.AKTIF				= 1
									LEFT JOIN SoruBankasi..Cevap			C	ON	C.ID_SORU				= SA.ID_SORUBANKASI
																				AND C.ID_CEVAP				= OC.ID_CEVAP				
									WHERE	SA.ID_SINAVDERS	= SOD.ID_SINAVDERS
										AND SA.SORUNO_A		= SA.SORUNO			--	SADECE KİTAPÇIK A OLANLAR
									ORDER BY SA.SORUNO_A
									FOR XML PATH('')
							), 1, 0, ''), '-', ' '), dbo.fn_BoslukBirak(SOD.OPTIKKARAKTERSAYISI)))
						,dbo.fn_BoslukBirak(SOD.OPTIKKARAKTERSAYISI))
					INTO #LINE
					FROM Tbl_OnlineSinavOgrenci	SO
					JOIN #OGRENCILIST			OL	ON	OL.TCKIMLIKNO	= SO.TCKIMLIKNO
					JOIN SinavDers				SD	ON	SD.ID_SINAV		= SO.ID_SINAV
					JOIN #SinavOptikDers		SOD	ON	SOD.BOLUMNO		= SD.BOLUMNO
					WHERE	SO.ID_SINAV = @ID_SINAV
						AND SO.AKTIF	= 1
	
					DECLARE @cols48 NVARCHAR(MAX) = NULL
					DECLARE @sels48 NVARCHAR(MAX) = NULL
					DECLARE @sql48 NVARCHAR(MAX) = NULL

					SET @cols48 = STUFF((	SELECT ',' +  QUOTENAME( C.BASLANGIC)  FROM #SinavOptikDers c ORDER BY C.BASLANGIC FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)')  ,1,1,'')
					SET @sels48 = STUFF((	SELECT '+' +  QUOTENAME( C.BASLANGIC)  FROM #SinavOptikDers c ORDER BY C.BASLANGIC FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)')  ,1,1,'')	
					--SET @sql48 = '
					--	SELECT LINE = LINE + '+@sels48+'
					--	FROM #LINE
					--	PIVOT ( MAX(TEST) FOR BASLANGIC IN(' + @cols48 + ') ) P
					--	ORDER BY LINE
					--'

					
					SET @sql48 = '
						DROP TABLE IF EXISTS #TEMP
						SELECT LINE = LINE + '+@sels48+' , RN = ROW_NUMBER() OVER(ORDER BY LINE)
						INTO #TEMP
						FROM #LINE
						PIVOT ( MAX(TEST) FOR BASLANGIC IN(' + @cols48 + ') ) P
						ORDER BY LINE

						DECLARE @TEKSATIR NVARCHAR(MAX) = ''''
						IF '+CAST(@MAIL AS VARCHAR)+' = CAST(1 AS BIT)
						BEGIN
							SELECT REPLACE(LINE,'' '',''&nbsp;''),RN FROM #TEMP ORDER BY RN
						END
						ELSE
						BEGIN
							WHILE EXISTS(SELECT TOP 1 1 FROM #TEMP)
							BEGIN
								DECLARE @RN INT = (SELECT TOP 1 RN FROM #TEMP ORDER BY RN)
								SET @TEKSATIR += (SELECT LINE + IIF(EXISTS(SELECT TOP 1 1 FROM #TEMP WHERE RN != @RN) , CHAR(13) + CHAR(10), '''') FROM #TEMP WHERE RN = @RN) 
								DELETE FROM #TEMP WHERE RN = @RN
							END
							SELECT @TEKSATIR
						END
					'

					EXEC (@sql48)

					
					DROP TABLE IF EXISTS #SinavOptikSabit
					DROP TABLE IF EXISTS #SinavOptikDers
					DROP TABLE IF EXISTS #OGRENCILIST
					DROP TABLE IF EXISTS #LINE

				END
				IF @ISLEM = 49  --  SINAV DÜZENLE YETKİ KONTROL 
				BEGIN

					-- BUNU SINAV DÜZENLEMEYE KOPYALA
					--EXEC sp_Sinav @TCKIMLIKNO = @TCKIMLIKNO , @OTURUM = @OTURUM, @ID_MENU = @ID_MENU, @ISLEM = 49, @ID_SINAV = @ID_SINAV


					--'YETKİNİZ YOK'	0
					--'SINAV BAŞLAMADI' 1
					--'SINAV BAŞLADI'	2
					DECLARE @SONUC INT = 1
					IF EXISTS (SELECT TOP 1 1 FROM Sinav WHERE ID_SINAV = @ID_SINAV AND ONLINESINAV = 1)
					BEGIN
						IF	EXISTS( SELECT VALUE FROM fn_Split((SELECT DEGER FROM ParametreDeger WHERE ID_PARAMETREDEGER = 4), ',', NULL ) WHERE VALUE = @TCKIMLIKNO)
						BEGIN					
					
							-- sınav başladı
							-- tc yetkisi var mı 

							IF EXISTS (	SELECT TOP 1 1 
										FROM Sinav S 
										JOIN Tbl_OnlineSinavDetay		D	ON	D.ID_SINAV				= S.ID_SINAV 
																			AND D.AKTIF					= 1
										JOIN Tbl_OnlineSinavDetayOturum	DO	ON	DO.ID_ONLINESINAVDETAY	= D.ID_ONLINESINAVDETAY
										WHERE	S.ID_SINAV		= @ID_SINAV 
											AND S.ONLINESINAV	= 1 
											AND DO.BASTARIH		< GETDATE()
										)
							BEGIN
								SET @SONUC = 2
							END
						END
						ELSE
						BEGIN
							SET @SONUC = 0
						END
					END

					SELECT @SONUC
				END

				IF @ISLEM = 50  -- YÖNERGE SİL
				BEGIN
					UPDATE Sinav SET YONERGE_GUID =NULL WHERE ID_SINAV=@ID_SINAV
				END	

				--51 NUMARALI ISLEM FERITTEN GELECEK O NEDENLE 52 DEN DEVAM EDILDI

				IF @ISLEM=52 --SINAV DEGERLENDIRME LOG 
				BEGIN

					SELECT ISNULL((
						SELECT   
							 CONVERT(VARCHAR, D.KYE_TARIH, 104 ) 	AS DegerlendirmeTarihi
							,DL.ID_SINAV	AS SinavID
							,K.AD+K.SOYAD	AS Degerlendiren
						FROM SinavDegerlendirLog	AS DL
						JOIN SinavDegerlendir		AS  D ON D.ID_SINAVDEGERLENDIR=DL.ID_SINAVDEGERLENDIR
						JOIN v3Kullanici			AS  K ON K.TCKIMLIKNO=D.KYE_TCKIMLIKNO
						WHERE DL.ID_SINAV=@ID_SINAV 
						FOR JSON PATH
					),'[]')

				END

				-- GENEL 
				DROP TABLE IF EXISTS #KADEMELER
				DROP TABLE IF EXISTS #OTURUMLAR
			--END

			
		END
	
		--COMMIT TRANSACTION [Tran1]
	END TRY
	BEGIN CATCH
		--ROLLBACK TRANSACTION [Tran1]
		IF @ISLEM IN (14, 17)	--	DOSYA YÜKLEME
		BEGIN
			UPDATE Sinav			SET DURUM = 0, DEGERLENDIRILIYOR = 0	WHERE ID_SINAV	= @ID_SINAV
		END

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
PRINT N'Altering [dbo].[sp_OgrenciIzleme]...';


GO
ALTER procedure [dbo].[sp_OgrenciIzleme]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@TC_OGRENCI			VARCHAR(11)		= NULL,
	@TC_OGRETMEN		VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@IL					VARCHAR(36)		= NULL,
	@ILCE				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,
	@DONEM				char(4)			= NULL,
	@DONEMSINAV			char(4)			= NULL,
	@ID_KADEME3			VARCHAR(MAX)	= NULL,
	@ID_SINAVTURU		VARCHAR(MAX)	= NULL,
	--@KADEME				INT			= NULL,
	@ID_SINAV			INT				= NULL,
	@ID_DERS			INT				= NULL,
	@ID_SUBE			INT				= NULL,
	@ID_SINIF			INT				= NULL,
	@DOSYAADI			varchar(100)	= NULL,
	@DOSYAGUID			varchar(50)		= NULL,
	@ID_SINAVDOSYA		int				= NULL,
	@DERSAD				VARCHAR(MAX)	= NULL,
	@SQLJSON			NVARCHAR(MAX)	= NULL,
	@ID_SUBEs			VARCHAR(MAX)	= NULL,
	@ID_SINIFs			VARCHAR(MAX)	= NULL,
	@ID_DERSs			VARCHAR(MAX)	= NULL
AS
BEGIN
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	
			ISLEM						= @ISLEM			
			,TCKIMLIKNO					= @TCKIMLIKNO		
			,TC_OGRENCI					= @TC_OGRENCI		
			,TC_OGRETMEN				= @TC_OGRETMEN	
			,OTURUM						= @OTURUM			
			,IL							= @IL				
			,ILCE						= @ILCE			
			,ID_MENU					= @ID_MENU		
			,DONEM						= @DONEM			
			,DONEMSINAV					= @DONEMSINAV		
			,ID_KADEME3					= @ID_KADEME3		
			,ID_SINAVTURU				= @ID_SINAVTURU	
			,ID_SINAV					= @ID_SINAV		
			,ID_DERS					= @ID_DERS		
			,ID_SUBE					= @ID_SUBE		
			,ID_SINIF					= @ID_SINIF		
			,DOSYAADI					= @DOSYAADI		
			,DOSYAGUID					= @DOSYAGUID		
			,ID_SINAVDOSYA				= @ID_SINAVDOSYA	
			,DERSAD						= @DERSAD			
			,SQLJSON				 	= @SQLJSON		
			,ID_SUBEs					= @ID_SUBEs		
			,ID_SINIFs					= @ID_SINIFs		
			,ID_DERSs					= @ID_DERSs		
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0
-----------------------------------------------------------------------------------------------
----------------Net sayıları sınavların soru sayılarına göre normalize ediliyor----------------

-----------INTSNV1037	ENDEKSLEME KAPATILIP NORMAL NETLERİ VE AĞIRLIKLI ORT EKLENDİ-----------
-----------------------------------------------------------------------------------------------


--	http://pusulam/Rapor.aspx?rapor=Sinav.OgrenciIzlemePivot&raporTur=xls&p=32051057542;8A65CF46-5A8A-40D0-95F2-D8F3F5FED778;2017;18;2;[11];[101672];[847,850,851]
--	http://pusulam/Rapor.aspx?rapor=Sinav.OgrenciIzlemePivot&raporTur=xls&p=32051057542;FD2297C4-AF4C-42A6-B6A3-7E0510FB7995;2017;14;8;[427];[102195];[144]

--	EXEC [sp_OgrenciIzleme] @ISLEM=2,@TCKIMLIKNO='32051057542',@OTURUM='DD159E6F-D2B5-4A0A-8404-3191C46C529E',@DONEM='2018',@DONEMSINAV='2018',@ID_KADEME3=14,@ID_SINAVTURU=8,@ID_SUBEs='[0,427,289,652,11,385,236,690,411,134,123,598,580,702,581,368,576,200,335,442,594,730,447,448,706,444,446,443,309,256,430]',@ID_SINIFs='[0,108378,108377,106603,106604,106578,106580,106582,106646,106435,106427,107264,107270,107266,107271,107272,106136,109352,106310,106311,106312,106358,106360,106361,106819,106820,106821,106549,106550,106218,106219,106898,106899,106701,106700,106699,106366,106367,107502,107501,107500,107163,107164,107695,108862,108531,108534,108533,107396,107397,107497,106517,106518,106519,106520,106804,106805,106806,106807,106808,106834,108404,108410,108412,106478,106479,106480,107365,107366,107358,108941]',@ID_DERSs='[0,141,144,145,146,830,1110]'


--	EXEC [sp_OgrenciIzleme] @ISLEM=2,@TCKIMLIKNO='32051057542',@OTURUM='43EFA6B0-F85F-43CA-9092-16C1FF8C1403',@DONEM='2018',@DONEMSINAV='2018',@ID_KADEME3=15,@ID_SINAVTURU=5,@ID_SUBEs='[0,427,289,652,11,385,236,690,411,134,123,598,580,702,581,368,576,200,335,442,594,730,447,448,706,444,446,443,309,256,430]',@ID_SINIFs='[0,108614,106609,106611,106643,106644,106645,106642,106664,107214,107210,107209,107211,108612,107251,107253,106137,106138,106139,106140,106765,106299,106300,106335,106334,106333,106822,106823,106824,106825,106551,106552,106553,107548,107549,107552,107561,106688,106689,106368,106369,107513,107505,107504,107503,107179,107180,107181,108550,108551,108552,106161,107496,107354,106854,106853,106852,106851,108341,108342,108343,106481,106482,106483,106484,107368,107376,107374,108942]',@ID_DERSs='[0,804]'

BEGIN TRY    
	EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
 				
	IF @ID_LOG > 1
	BEGIN
		
		DECLARE  @OZELSINAVGOR BIT=0
		IF EXISTS ( SELECT TOP 1 1 FROM OkyanusDB.DBO.v3SubeYetki WHERE TCKIMLIKNO= @TCKIMLIKNO AND ID_KULLANICITIPI IN (1,60) ) -- ADMİN VEYA ÖD İSE ÖZEL SINAVLARI GÖRECEK
		BEGIN
			SET @OZELSINAVGOR=1
		END

		
				DECLARE @DONEMFARKI INT = CAST(@DONEM AS INT ) - CAST (@DONEMSINAV AS INT)
				IF  (@DONEMFARKI<>0) 
				BEGIN
					DECLARE @SIRA INT = (SELECT SIRA FROM OkyanusDB.DBO.v3Kademe3 WHERE ID_KADEME3 = @ID_KADEME3)
					SELECT @ID_KADEME3=ID_KADEME3 FROM OkyanusDB.DBO.v3Kademe3 WHERE SIRA = @SIRA- @DONEMFARKI
				END

		IF @ISLEM = 1 OR @ISLEM = 2
		BEGIN
			
			

			DROP TABLE IF EXISTS #TempNet
			DROP TABLE IF EXISTS #TempNet2
			DROP TABLE IF EXISTS #NETLISTE
			DROP TABLE IF EXISTS ##PivotsNet

			DROP TABLE IF EXISTS #TempPuan
			DROP TABLE IF EXISTS ##PivotsPuan
		
			Declare @SQL		as varchar(max)
				,	@Columns	as varchar(max)
				,	@Columns2	as varchar(max)
				,	@Columns3	as varchar(max)
				,	@Columns4	as varchar(max)
				,	@Columns5	as varchar(max)
				,	@Columns6	as varchar(max)

		--DECLARE  @DERSAD varchar(max)='Tümü'		
		--		,@ID_SUBEs		VARCHAR(MAX)= '[427,11]'
		--		,@ID_SINIFs		VARCHAR(MAX)= '[0,101671,101679,101672,101676,101680]'
		--		,@DONEM			VARCHAR(MAX)= '2017'
		--		,@ID_SINAVTURU	INT			= 3
		--		,@ID_KADEME3	INT			= 18

			
			SELECT  DISTINCT	SO.TCKIMLIKNO,SO.AD + ' ' + SO.SOYAD ADSOYAD, S.ID_SINAV, S.AD SINAVAD,S.SINAVTARIH, SOS.ID_SINAVPUANTURU, PT.AD PUANTURU,SOS.PUAN, O.ID_SUBE,O.ID_SINIF,VS.AD SUBEAD,VSN.AD SINIFAD
			INTO	#TempPuan
			FROM	Sinav					S
			JOIN	SinavOgrenci			SO	ON	SO.ID_SINAV			= S.ID_SINAV
			JOIN	v3OgrenciTum			O	ON	O.TCKIMLIKNO		= SO.TCKIMLIKNO
			JOIN	v3Sube					VS	ON	VS.ID_SUBE			= O.ID_SUBE
			JOIN	v3SinifTum				VSN	ON	VSN.ID_SINIF		= O.ID_SINIF 
			JOIN	v3Donem					D	ON	D.ID_DONEM			= VSN.ID_DONEM
			JOIN	SinavOgrenciSonuc		SOS	ON	SOS.ID_SINAVOGRENCI	= SO.ID_SINAVOGRENCI
			JOIN	SinavPuanTuru			PT	ON	PT.ID_SINAVPUANTURU	= SOS.ID_SINAVPUANTURU
			WHERE	S.DURUM			= 2 
				AND S.AKTIF			= 1
				AND (OZEL=0 OR @OZELSINAVGOR=1)
				AND S.PUANGIZLE		= 0
				AND S.ID_SINAVTURU	= @ID_SINAVTURU
				AND S.ID_KADEME3	= @ID_KADEME3
				AND S.DONEM			= @DONEMSINAV	
				AND D.KOD			= @DONEM			
				AND (O.ID_SUBE		IN	(SELECT Value FROM OPENJSON(@ID_SUBEs))		OR	@ID_SUBEs	= '[]')
				AND (O.ID_SINIF		IN	(SELECT Value FROM OPENJSON(@ID_SINIFs))	OR	@ID_SINIFs	= '[]')				
			ORDER BY SO.TCKIMLIKNO
					
			INSERT INTO #TempPuan 
			SELECT TCKIMLIKNO, ADSOYAD, 9999, 'Ağırlıklı Ort',DATEADD (YEAR,1, GETDATE()), ID_SINAVPUANTURU, PUANTURU, AVG(PUAN), ID_SUBE, ID_SINIF, SUBEAD, SINIFAD FROM #TempPuan 			
			GROUP BY TCKIMLIKNO, ADSOYAD, ID_SINAVPUANTURU, PUANTURU,  ID_SUBE, ID_SINIF, SUBEAD, SINIFAD 
			
			SELECT  SO.TCKIMLIKNO,SO.AD + ' ' + SO.SOYAD ADSOYAD, S.ID_SINAV, S.AD SINAVAD,S.SINAVTARIH, S.ID_SINAVTURU,SD.ID_SINAVDERS ,SD.TAKMAAD, DOGRU, YANLIS, BOS, NET
			, YUZDENET,O.ID_SUBE,O.ID_SINIF,VS.AD SUBEAD,VSN.AD SINIFAD
			INTO	#TempNet
			FROM	Sinav					S
			JOIN	SinavOgrenci			SO	ON	SO.ID_SINAV			= S.ID_SINAV
			JOIN	v3OgrenciTum			O	ON	O.TCKIMLIKNO		= SO.TCKIMLIKNO
			JOIN	v3Sube					VS	ON	VS.ID_SUBE			= O.ID_SUBE
			JOIN	v3SinifTum				VSN	ON	VSN.ID_SINIF		= O.ID_SINIF
			JOIN	v3Donem					DN	ON	DN.ID_DONEM			= VSN.ID_DONEM
			JOIN	SinavOgrenciCevapBolum	B	ON	B.ID_SINAVOGRENCI	= SO.ID_SINAVOGRENCI
			JOIN	SinavDers				SD	ON	SD.ID_SINAVDERS		= B.ID_SINAVDERS
			JOIN	eokul_v2.dbo.Ders		D	ON	D.ID_DERS			= SD.ID_DERS
			WHERE	S.DURUM			= 2 
				AND S.AKTIF			= 1
				AND (OZEL=0 OR @OZELSINAVGOR=1)
				AND S.ID_SINAVTURU	= @ID_SINAVTURU
				AND S.ID_KADEME3	= @ID_KADEME3
				AND S.DONEM			= @DONEMSINAV
				AND DN.KOD			= @DONEM	
				AND (D.ID_DERS		IN	(SELECT Value FROM OPENJSON(@ID_DERSs))		OR	@ID_DERSs	= '[]')
				AND (O.ID_SUBE		IN	(SELECT Value FROM OPENJSON(@ID_SUBEs))		OR	@ID_SUBEs	= '[]')
				AND (O.ID_SINIF		IN	(SELECT Value FROM OPENJSON(@ID_SINIFs))	OR	@ID_SINIFs	= '[]')				
			ORDER BY S.SINAVTARIH
			
			SELECT TAKMAAD,MAX(SS) SS 
			INTO #TempNet2 
			FROM (
				SELECT	 DISTINCT 
						 ID_SINAVDERS
						,TAKMAAD
						,ID_SINAV
						--,SUM(DOGRU+YANLIS+BOS) SS	
						,(select max(sa.SORUNO_A) from SinavAnahtar sa where sa.ID_SINAVDERS = t.ID_SINAVDERS ) SS
				FROM #TempNet t
				--GROUP BY  TCKIMLIKNO,ID_SINAV, TAKMAAD,ID_SINAVDERS		
				) AS TEMP2
			GROUP BY  TAKMAAD

			SELECT DISTINCT TCKIMLIKNO, ADSOYAD, ID_SINAV, SINAVAD, ID_SINAVTURU,ID_SINAVDERS ,T.TAKMAAD, DOGRU, YANLIS, BOS, NET, YUZDENET,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD,SINAVTARIH
				,G.SS
				,NETORT = NET --CAST((CAST(NET AS DECIMAL(8,2))/nullif(SUM(DOGRU+YANLIS+BOS),0) * G.SS) AS DECIMAL(8,2))
			INTO #NETLISTE
			FROM #TempNet T
			JOIN #TempNet2 G ON G.TAKMAAD=T.TAKMAAD
			--GROUP BY TCKIMLIKNO, ADSOYAD, ID_SINAV, SINAVAD, ID_SINAVTURU,ID_SINAVDERS ,T.TAKMAAD, DOGRU, YANLIS, BOS, NET, YUZDENET,ID_SUBE,ID_SINIF,G.SS,SINIFAD,SUBEAD,SINAVTARIH
		UNION ALL			
			SELECT TCKIMLIKNO, ADSOYAD, 0, 'Ağırlıklı Ort', ID_SINAVTURU,0,T.TAKMAAD, 0, 0, 0, 0, 0,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD,DATEADD(YEAR,1,  GETDATE())
				,G.SS
				,NETORT	= CAST((CAST(SUM(NET) AS DECIMAL(8,2))/nullif(SUM(DOGRU+YANLIS+BOS),0)*G.SS	) AS DECIMAL(8,2)) 
			FROM #TempNet T
			JOIN #TempNet2 G ON G.TAKMAAD=T.TAKMAAD
			GROUP BY TCKIMLIKNO, ADSOYAD,  ID_SINAVTURU,T.TAKMAAD, ID_SUBE,ID_SINIF,G.SS,SINIFAD,SUBEAD


PRINT 1
			IF NOT EXISTS (SELECT * FROM #NETLISTE)
			BEGIN
				SELECT SIRA=NULL, TCKIMLIKNO=NULL, ADSOYAD=NULL, ID_SINAVTURU=NULL,TAKMAAD=NULL,ID_SUBE=NULL,ID_SINIF=NULL,SINIFAD=NULL,SUBEAD=NULL, SS=NULL INTO ##PivotsNet				
			END	
			
				
			IF NOT EXISTS (SELECT * FROM #TempPuan)
			BEGIN				
				SELECT TCKIMLIKNO=NULL, ADSOYAD=NULL, ID_SINAV=NULL, SINAVAD=NULL,SINAVTARIH=NULL, ID_SINAVPUANTURU=NULL, PUANTURU=NULL, PUAN=NULL, ID_SUBE=NULL, ID_SINIF=NULL, SUBEAD=NULL, SINIFAD=NULL INTO ##PivotsPuan				
				delete from ##PivotsPuan
			END		
			
PRINT 3
		--	Select distinct ID_SINAV,SINAVTARIH from #NETLISTE 
				-- PIVOT 
				BEGIN
					Select @Columns=Coalesce(@Columns+',','')+QUOTENAME(ID_SINAV) 
					from 
						( Select distinct ID_SINAV,SINAVTARIH from #NETLISTE  ) as b 
					ORDER BY SINAVTARIH 
PRINT 4

					Select @Columns2=Coalesce(@Columns2+',','')+'ISNULL(CAST(MAX(P2.'+QUOTENAME(ID_SINAV)+') AS VARCHAR),''-'') AS ' +QUOTENAME(ID_SINAV) 
					from 
						( Select distinct ID_SINAV,SINAVTARIH from #NETLISTE  ) as b 				
					ORDER BY SINAVTARIH 

					Select @Columns3=Coalesce(@Columns3+',','')+'ISNULL(CAST(SUM(CAST( CASE WHEN  P2.'+QUOTENAME(ID_SINAV)+' = ''-'' THEN NULL ELSE P2.'+QUOTENAME(ID_SINAV)+' END AS DECIMAL(8,2)) ) AS VARCHAR),''-'') AS ' +QUOTENAME(ID_SINAV) 
					from 
						( Select distinct ID_SINAV,SINAVTARIH from #NETLISTE  ) as b 				
					ORDER BY SINAVTARIH 

			
					Select @Columns4=Coalesce(@Columns4+',','')+QUOTENAME(ID_SINAV) 
					from 
						( Select distinct ID_SINAV,SINAVTARIH from #TempPuan  ) as b 
					ORDER BY SINAVTARIH 
			
					--Select @Columns5=Coalesce(@Columns5+',','')+'ISNULL(CAST(MAX(CAST( CASE WHEN P2.'+QUOTENAME(ID_SINAV)+' = ''-'' THEN NULL ELSE P2.'+QUOTENAME(ID_SINAV)+' END AS DECIMAL(8,2) ) AS VARCHAR),''-'') AS ' +QUOTENAME(ID_SINAV) 
					--from 
					--	( Select distinct ID_SINAV,SINAVTARIH from #TempPuan  ) as b 				
					--ORDER BY SINAVTARIH 
						
					Select @Columns5=Coalesce(@Columns5+',','')+'ISNULL(CAST(MAX(P2.'+QUOTENAME(ID_SINAV)+') AS VARCHAR),''-'') AS ' +QUOTENAME(ID_SINAV) 
					from 
						( Select distinct ID_SINAV,SINAVTARIH from #TempPuan  ) as b 				
					ORDER BY SINAVTARIH 

					
PRINT @Columns	
PRINT @Columns2


					Set @SQL='	

					Select 								 
						0 SIRA, TCKIMLIKNO, ADSOYAD, ID_SINAVTURU,TAKMAAD,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD, SS,'+@Columns2+' 
					into ##PivotsNet
					from (
						Select 
							TCKIMLIKNO, ADSOYAD, ID_SINAV, SINAVAD, ID_SINAVTURU,ID_SINAVDERS ,TAKMAAD, DOGRU, YANLIS, BOS, NET, YUZDENET,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD,SINAVTARIH,SS,NETORT
						from #NETLISTE
					) AS S			
					PIVOT (MAX(NETORT) FOR ID_SINAV IN('+@Columns+')) as P2
					GROUP BY TCKIMLIKNO, ADSOYAD, ID_SINAVTURU, TAKMAAD,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD, SS

					INSERT INTO ##PivotsNet
					SELECT	1 SIRA, TCKIMLIKNO, ADSOYAD, ID_SINAVTURU, ''TOPLAM'' TAKMAAD,ID_SUBE,ID_SINIF,SINIFAD,SUBEAD, SUM(SS) SS,'+@Columns3+'  
					FROM	##PivotsNet P2
					GROUP BY SIRA, TCKIMLIKNO, ADSOYAD, ID_SINAVTURU, ID_SUBE,ID_SINIF,SINIFAD,SUBEAD
'
				PRINT @SQL
				EXEC (@SQL)
			SET @SQL = '
					Select
						TCKIMLIKNO, ADSOYAD, ID_SINAVPUANTURU, PUANTURU,  ID_SUBE, ID_SINIF, SINIFAD, SUBEAD,'+@Columns5+' 
					into ##PivotsPuan
					from (
						Select 
							TCKIMLIKNO, ADSOYAD, ID_SINAV, SINAVAD,SINAVTARIH, ID_SINAVPUANTURU, PUANTURU, PUAN, ID_SUBE, ID_SINIF, SUBEAD, SINIFAD
						from #TempPuan
					) AS S			
					PIVOT (MAX(PUAN) FOR ID_SINAV IN('+@Columns4+')) as P2
					GROUP BY TCKIMLIKNO, ADSOYAD, ID_SINAVPUANTURU, PUANTURU,  ID_SUBE, ID_SINIF, SUBEAD, SINIFAD
				'
				PRINT @SQL
				EXEC (@SQL)
		
				--SELECT * FROM ##PivotsNet
				--ORDER BY ADSOYAD,TAKMAAD
		
				--SELECT * FROM ##PivotsPuan
				--ORDER BY ADSOYAD,PUANTURU

			END

		SELECT TOP 1 ST.KISAAD STKISAAD,K.AD KADEME3,D.ACIKLAMA DONEM
		INTO #BASLIK
		FROM ##PivotsNet	PT
		JOIN SinavTuru		ST ON ST.ID_SINAVTURU	= @ID_SINAVTURU
		JOIN V3KADEME3		K  ON K.ID_KADEME3		= @ID_KADEME3
		JOIN v3AktifDonem	D  ON D.DONEM			= @DONEM	

			IF @ISLEM = 1 -- JSON SEND
			BEGIN
		
					select(
							select * from(
								select 
								(					
									SELECT * FROM ##PivotsNet
									ORDER BY ADSOYAD,SIRA,TAKMAAD
									FOR JSON AUTO
								) as TblNet,
								(									
									Select distinct ID_SINAV,SINAVAD,SINAVTARIH from #NETLISTE order by SINAVTARIH		
									FOR JSON AUTO
								) as TblNetSinav,
								(					
									SELECT * FROM ##PivotsPuan
									ORDER BY ADSOYAD,PUANTURU
									FOR JSON AUTO
								) as TblPuan,
								(	
									Select distinct ID_SINAV,SINAVAD,SINAVTARIH from #TempPuan order by SINAVTARIH		
									FOR JSON AUTO
								) as TblPuanSinav,
								(	
									Select STKISAAD,KADEME3,DONEM from #BASLIK
									FOR JSON AUTO
								) as TblBaslik
												
							) as tablo FOR JSON AUTO
						) as tt
			END
			IF @ISLEM = 2 -- DATASET SEND
			BEGIN
				SELECT * 
				FROM ##PivotsNet
				ORDER BY ADSOYAD, SIRA, TAKMAAD

				;WITH SIRA AS (
				SELECT ID_SINAV,SIRA = CASE WHEN ID_SINAV = 0 THEN 'ORT' ELSE CAST(ROW_NUMBER() OVER (ORDER BY SINAVTARIH) AS VARCHAR) END FROM #NETLISTE GROUP BY ID_SINAV,SINAVTARIH
				)
				Select distinct 
					 N.ID_SINAV
					,SINAVAD =    '('
								+ RIGHT( '000' +  S.SIRA,3 )
								+ ')'
								+ SINAVAD
					,SINAVTARIH 
				from #NETLISTE	N
				JOIN SIRA		S	ON	S.ID_SINAV = N.ID_SINAV
				order by SINAVTARIH		
		
				SELECT * FROM ##PivotsPuan
				ORDER BY ADSOYAD,PUANTURU
				
				Select distinct ID_SINAV,SINAVAD,SINAVTARIH from #TempPuan order by SINAVTARIH		
				
				Select STKISAAD,KADEME3,DONEM from #BASLIK

			END

	
			DROP TABLE IF EXISTS #TempNet
			DROP TABLE IF EXISTS #TempNet2
			DROP TABLE IF EXISTS #NETLISTE
			DROP TABLE IF EXISTS ##PivotsNet

			DROP TABLE IF EXISTS #TempPuan
			DROP TABLE IF EXISTS ##PivotsPuan

			DROP TABLE IF EXISTS #BASLIK

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
GO
PRINT N'Altering [dbo].[sp_SinavSonuclari]...';


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
					,S.SIRALAMAYIGIZLE

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
				,S.SIRALAMAYIGIZLE

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
GO
PRINT N'Refreshing [dbo].[fn_SinavOgrenciToplamDYBN]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[fn_SinavOgrenciToplamDYBN]';


GO
PRINT N'Refreshing [dbo].[sp_Ogrenci]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_Ogrenci]';


GO
PRINT N'Refreshing [dbo].[sp_FiltreEk]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_FiltreEk]';


GO
PRINT N'Refreshing [dbo].[sp_Sinav2]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_Sinav2]';


GO
PRINT N'Refreshing [dbo].[sp_ApiDisOgrenci]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_ApiDisOgrenci]';


GO
PRINT N'Refreshing [dbo].[sp_AraKarne]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_AraKarne]';


GO
PRINT N'Refreshing [dbo].[sp_AraKarneCoklu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_AraKarneCoklu]';


GO
PRINT N'Refreshing [dbo].[sp_BurslulukOgrenciIslemleri]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_BurslulukOgrenciIslemleri]';


GO
PRINT N'Refreshing [dbo].[sp_CevapAnahtariDuzenle]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_CevapAnahtariDuzenle]';


GO
PRINT N'Refreshing [dbo].[sp_DenemeSinavRaporlari]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_DenemeSinavRaporlari]';


GO
PRINT N'Refreshing [dbo].[sp_DersCalismaProgrami]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_DersCalismaProgrami]';


GO
PRINT N'Refreshing [dbo].[sp_DisOgrenci]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_DisOgrenci]';


GO
PRINT N'Refreshing [dbo].[sp_DSNetPuanOrtalamalariOS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_DSNetPuanOrtalamalariOS]';


GO
PRINT N'Refreshing [dbo].[sp_Etut]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_Etut]';


GO
PRINT N'Refreshing [dbo].[sp_FrekansOgrenciHesapla]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_FrekansOgrenciHesapla]';


GO
PRINT N'Refreshing [dbo].[sp_FrekansSinavOgrenci]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_FrekansSinavOgrenci]';


GO
PRINT N'Refreshing [dbo].[sp_FrekansSistem]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_FrekansSistem]';


GO
PRINT N'Refreshing [dbo].[sp_GelisimRaporu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GelisimRaporu]';


GO
PRINT N'Refreshing [dbo].[sp_GelisimRaporuLS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GelisimRaporuLS]';


GO
PRINT N'Refreshing [dbo].[sp_GelisimRaporuOgrenciBelge]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GelisimRaporuOgrenciBelge]';


GO
PRINT N'Refreshing [dbo].[sp_GelisimRaporuOO]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GelisimRaporuOO]';


GO
PRINT N'Refreshing [dbo].[sp_GenelSinavRaporu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GenelSinavRaporu]';


GO
PRINT N'Refreshing [dbo].[sp_GenelSoruAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_GenelSoruAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_KarmaListe]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_KarmaListe]';


GO
PRINT N'Refreshing [dbo].[sp_KarmaListe3]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_KarmaListe3]';


GO
PRINT N'Refreshing [dbo].[sp_KonuAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_KonuAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_KonuAnaliziCoklu]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_KonuAnaliziCoklu]';


GO
PRINT N'Refreshing [dbo].[sp_LUSRapor]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_LUSRapor]';


GO
PRINT N'Refreshing [dbo].[sp_OdulListesi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OdulListesi]';


GO
PRINT N'Refreshing [dbo].[sp_OgrenciHedef]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OgrenciHedef]';


GO
PRINT N'Refreshing [dbo].[sp_OgrenciHedefOO]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OgrenciHedefOO]';


GO
PRINT N'Refreshing [dbo].[sp_OgrenciMaddeAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OgrenciMaddeAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_OgrenciRaporlari]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OgrenciRaporlari]';


GO
PRINT N'Refreshing [dbo].[sp_OkulNetPuanOrtalamalari]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OkulNetPuanOrtalamalari]';


GO
PRINT N'Refreshing [dbo].[sp_OkulRapor]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OkulRapor]';


GO
PRINT N'Refreshing [dbo].[sp_OnlineSinavDurumGuncelle]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OnlineSinavDurumGuncelle]';


GO
PRINT N'Refreshing [dbo].[sp_OptikYukle]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OptikYukle]';


GO
PRINT N'Refreshing [dbo].[sp_SinavHariciPuanSira]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavHariciPuanSira]';


GO
PRINT N'Refreshing [dbo].[sp_SinavIstatistik]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavIstatistik]';


GO
PRINT N'Refreshing [dbo].[sp_SinavKatilim]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavKatilim]';


GO
PRINT N'Refreshing [dbo].[sp_SinavKonuAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavKonuAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_SinavListele]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavListele]';


GO
PRINT N'Refreshing [dbo].[sp_SinifDersAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinifDersAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_SinifMaddeAnalizi]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinifMaddeAnalizi]';


GO
PRINT N'Refreshing [dbo].[sp_SinifNetPuanOrtalamalari]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinifNetPuanOrtalamalari]';


GO
PRINT N'Refreshing [dbo].[sp_SoruMaddeAnaliziOS]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SoruMaddeAnaliziOS]';


GO
PRINT N'Refreshing [dbo].[sp_SorunBildir]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SorunBildir]';


GO
PRINT N'Refreshing [dbo].[sp_TopluSinavSonuclari]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_TopluSinavSonuclari]';


GO
PRINT N'Refreshing [dbo].[sp_v2FrekansKampus]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_v2FrekansKampus]';


GO
PRINT N'Refreshing [dbo].[sp_v2FrekansOgrenciHesapla]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_v2FrekansOgrenciHesapla]';


GO
PRINT N'Refreshing [dbo].[sp_v2FrekansSinif]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_v2FrekansSinif]';


GO
PRINT N'Refreshing [dbo].[sp_v2FrekansSistem]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_v2FrekansSistem]';


GO
PRINT N'Refreshing [dbo].[sp_v2FrekansSistemEksikKazanim]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_v2FrekansSistemEksikKazanim]';


GO
PRINT N'Refreshing [dbo].[sp_OnlineSinavMailYolla]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OnlineSinavMailYolla]';


GO
PRINT N'Refreshing [dbo].[sp_OptikIslemleriOnline]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OptikIslemleriOnline]';


GO
PRINT N'Refreshing [dbo].[sp_OptikIslemleri]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OptikIslemleri]';


GO
PRINT N'Refreshing [dbo].[sp_OptikIslemleriEski]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_OptikIslemleriEski]';


GO
PRINT N'Refreshing [dbo].[sp_SinavDegerlendir]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavDegerlendir]';


GO
PRINT N'Refreshing [dbo].[sp_SinavDegerlendirTumSinavlar]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[sp_SinavDegerlendirTumSinavlar]';


GO
PRINT N'Checking existing data against newly created constraints';



GO
ALTER TABLE [dbo].[Tbl_OnlineSinavOgrenciOturum] WITH CHECK CHECK CONSTRAINT [FK_Tbl_OnlineSinavOgrenciOturum_Sinav];

ALTER TABLE [dbo].[SinavOgrenci] WITH CHECK CHECK CONSTRAINT [FK_SinavOgrenci_Sinav];

ALTER TABLE [dbo].[SinavTYTSecimTurKriter] WITH CHECK CHECK CONSTRAINT [FK_SinavTYTSecimTurKriter_Sinav];

ALTER TABLE [dbo].[Sinav] WITH CHECK CHECK CONSTRAINT [FK_Sinav_SinavTuru];


GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO
