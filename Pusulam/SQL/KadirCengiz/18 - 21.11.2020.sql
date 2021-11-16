
GO
PRINT N'Disabling all DDL triggers...'
GO
DISABLE TRIGGER ALL ON DATABASE
GO
PRINT N'Creating [dbo].[OnlineDersOgretmenVodafone]...';


GO
CREATE TABLE [dbo].[OnlineDersOgretmenVodafone] (
    [ID_OGRETMENVODAFONE] INT           IDENTITY (1, 1) NOT NULL,
    [TCKIMLIKNO]          VARCHAR (11)  NOT NULL,
    [ID_ODA]              VARCHAR (MAX) NOT NULL,
    [ID_ODAKONTROL]       VARCHAR (MAX) NOT NULL,
    [ODAURL]              VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_OnlineDersOgretmenVodafone] PRIMARY KEY CLUSTERED ([ID_OGRETMENVODAFONE] ASC)
);


GO
PRINT N'Creating [dbo].[OnlineDersSinifVodafone]...';


GO
CREATE TABLE [dbo].[OnlineDersSinifVodafone] (
    [ID_ONLINEDERSSINIFVODAFONE] INT           IDENTITY (1, 1) NOT NULL,
    [ID_SINIF]                   INT           NOT NULL,
    [ID_GUN]                     INT           NOT NULL,
    [DERSNO]                     INT           NOT NULL,
    [DERSAD]                     VARCHAR (MAX) NOT NULL,
    [BASLIK]                     VARCHAR (MAX) NULL,
    [ID_OGRETMENVODAFONE]        INT           NOT NULL,
    [AKTIF]                      BIT           NOT NULL,
    [KYE_TCKIMLIKNO]             VARCHAR (11)  NOT NULL,
    [KYE_TARIH]                  DATETIME      NOT NULL,
    CONSTRAINT [PK_OnlineDersSinifVodafone] PRIMARY KEY CLUSTERED ([ID_ONLINEDERSSINIFVODAFONE] ASC)
);


GO
PRINT N'Creating [dbo].[DF_OnlineDersSinifVodafone_AKTIF]...';


GO
ALTER TABLE [dbo].[OnlineDersSinifVodafone]
    ADD CONSTRAINT [DF_OnlineDersSinifVodafone_AKTIF] DEFAULT ((1)) FOR [AKTIF];


GO
PRINT N'Creating [dbo].[DF_OnlineDersSinifVodafone_KYE_TARIH]...';


GO
ALTER TABLE [dbo].[OnlineDersSinifVodafone]
    ADD CONSTRAINT [DF_OnlineDersSinifVodafone_KYE_TARIH] DEFAULT (getdate()) FOR [KYE_TARIH];


GO
PRINT N'Creating [dbo].[FK_OnlineDersSinifVodafone_OnlineDersSinifVodafone]...';


GO
ALTER TABLE [dbo].[OnlineDersSinifVodafone] WITH NOCHECK
    ADD CONSTRAINT [FK_OnlineDersSinifVodafone_OnlineDersSinifVodafone] FOREIGN KEY ([ID_ONLINEDERSSINIFVODAFONE]) REFERENCES [dbo].[OnlineDersSinifVodafone] ([ID_ONLINEDERSSINIFVODAFONE]);


GO
PRINT N'Creating [dbo].[fn_strToSHA1]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION fn_strToSHA1
(
	@VAL VARCHAR(MAX) = NULL
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	RETURN sys.fn_varbintohexsubstring(0, HashBytes('SHA1', @VAL), 1, 0)

END
GO
PRINT N'Creating [dbo].[sp_HttpGet]...';


GO
-- =============================================
-- Author:		KADİR CENGİZ
-- Create date: 18.11.2020
-- Description:	HTTP GET
-- =============================================
CREATE PROCEDURE sp_HttpGet
	@URL	VARCHAR(MAX)=NULL
	,@RESULT VARCHAR(MAX)=NULL OUTPUT
AS
BEGIN
	
		Declare
		@obj int
		,@hr int
		,@msg varchar(255)
		--,@sUrl VARCHAR(MAX) = 'https://api.hz.web.tv/p201/api/getMeetingInfo?meetingID=201-cl2-d9fb5acd122de60778ad1dc36ad7d6dc98138409&checksum=d74ac973a4af9b4fe568a4c11aff3b3bfdb9ffea'

		 exec @hr = sp_OACreate 'MSXML2.ServerXMLHttp', @obj OUT
		if @hr <> 0 begin Raiserror('sp_OACreate MSXML2.ServerXMLHttp.3.0
		failed', 16,1) return end


		exec @hr = sp_OAMethod @obj, 'open', NULL, 'GET', @URL, false
		if @hr <>0 begin set @msg = 'sp_OAMethod Open failed' goto eh end


		exec @hr = sp_OAMethod @obj, 'setRequestHeader', NULL, 'Content-Type',
		'application/x-www-form-urlencoded'
		if @hr <>0 begin set @msg = 'sp_OAMethod setRequestHeader failed' goto
		eh end


		exec @hr = sp_OAMethod @obj, send, NULL, ''
		if @hr <>0 begin set @msg = 'sp_OAMethod Send failed' goto eh end

		CREATE TABLE #xml(DEGER NVARCHAR(MAX))

		INSERT #xml ( DEGER )
		exec @hr = sp_OAMethod @obj, 'responseText'--, @Reponse OUTPUT

		if @hr <> 0 begin set @msg = 'sp_OAMethod read response failed' goto eh end


		--SET @RESULT = @Reponse
		--DECLARE @RESULT VARCHAR(MAX)
		SELECT @RESULT = DEGER
		FROM #xml
		--SELECT @RESULT
		DROP TABLE #xml
		 exec @hr = sp_OADestroy @obj 

		 RETURN-- @RESULT
		--return @RESULT
		eh:
		exec @hr = sp_OADestroy @obj
		Raiserror(@msg, 16, 1)
		SET @RESULT = @msg
		RETURN --''

END
GO
PRINT N'Creating [dbo].[sp_VodafoneXmlResult]...';


GO
-- =============================================
-- Author:		KADİR CENGİZ
-- Create date: 18.11.2020
-- Description:	[sp_VodafoneXmlResult]
-- =============================================
CREATE PROCEDURE sp_VodafoneXmlResult	
	 @URL		VARCHAR(MAX)= NULL	
	,@RESULT	VARCHAR(MAX)= NULL OUTPUT
AS
BEGIN

	DECLARE @xml VARCHAR(MAX)  
	EXEC dbo.sp_HttpGet @URL = @URL, @RESULT = @xml OUTPUT
	
	DECLARE @idoc int;
	EXEC sp_xml_preparedocument @idoc OUTPUT, @xml;
	
	SET @RESULT = (
		SELECT returncode, meetingID
		FROM OPENXML(@idoc,'/response')
		WITH (	 returncode varchar(50) 'returncode'
				,meetingID	varchar(50) 'meetingID'
		)
		FOR JSON PATH,INCLUDE_NULL_VALUES
	)

	EXEC sp_xml_removedocument @idoc;
	
END
GO
PRINT N'Altering [dbo].[sp_OnlineDers]...';


GO
ALTER procedure [dbo].[sp_OnlineDers]
	@ISLEM						INT				= NULL,
	@TCKIMLIKNO					VARCHAR(11)		= NULL,
	@OTURUM						VARCHAR(36)		= NULL,
	@ID_MENU					INT				= NULL,
	
	@TARIH						NVARCHAR(MAX)	= NULL,
	@TC_OGRENCI					NVARCHAR(11)	= NULL,
	@ID_SINIF					INT				= NULL,
	@ID_KADEME3					INT				= NULL,
	@ID_ONLINEDERSSINIFJITSI	INT				= NULL,
	@EKRANPAYLASIMID			VARCHAR(MAX)	= NULL,
	@TC_OGRETMEN				VARCHAR(MAX)	= NULL,
	@LINK						VARCHAR(MAX)	= NULL,
	@HERKESISUSTUR				VARCHAR(MAX)	= NULL,
	@SQLJSON					NVARCHAR(MAX)	= NULL,
	@ID_SUBE					INT				= NULL,
	@ID_OGRETMENVODAFONE		INT				= NULL,
	@ID_ONLINEDERSSINIFVODAFONE	INT				= NULL

AS
BEGIN
/*

	Değiştiren Adı		: Kadir Cengiz
	Değiştirilen Tarih	: 10.09.2020
	
	--	sp Alter 
		--	ISLEM = 16,17 Add
	--	ÖĞRETMEN ONLINE DERS PROGRAMI, KALDIR

	
	Değiştiren Adı		: Burhan
	Değiştirilen Tarih	: 02.11.2020
	
	--	sp Alter 
		--	ISLEM = 18 Add
	--	ÖĞRETMEN ONLINE ÖĞRETMEN LİSTESİ

	Değiştiren Adı		: Kadir Cengiz
	Değiştirilen Tarih	: 18.11.2020
	
	--	sp Alter 
		--	ISLEM = 19 Add
	--	ÖĞRETMEN ONLINE DERS VODAFONE
*/


		
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 ISLEM						= @ISLEM
				,TCKIMLIKNO					= @TCKIMLIKNO
				,OTURUM						= @OTURUM
				,ID_MENU					= @ID_MENU

				,SQLJSON					= @SQLJSON
				,TARIH						= @TARIH
				,ID_SINIF					= @ID_SINIF
				,ID_KADEME3					= @ID_KADEME3
				,TC_OGRETMEN				= @TC_OGRETMEN
				,TC_OGRENCI					= @TC_OGRENCI
				,LINK						= @LINK
				,HERKESISUSTUR				= @HERKESISUSTUR
				,ID_ONLINEDERSSINIFJITSI	= @ID_ONLINEDERSSINIFJITSI
				,EKRANPAYLASIMID			= @EKRANPAYLASIMID
				,ID_SUBE					= @ID_SUBE
				,ID_ONLINEDERSSINIFVODAFONE	= @ID_ONLINEDERSSINIFVODAFONE
				,ID_OGRETMENVODAFONE		= @ID_OGRETMENVODAFONE
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	--IF @TCKIMLIKNO != '45964597234'
	--BEGIN
	--	RAISERROR ('Lütfen Daha Sonra Tekrar Deneyiniz' , 16,1);
	--	return
	--END
	DECLARE @_ID_LOG INT = 0

	BEGIN TRY    
		DECLARE @_DESTEK_KONTROL INT
		EXEC @_ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
		
		IF @_ID_LOG > 0
		BEGIN
				
			DECLARE @AKTIFDONEM VARCHAR(4) = (SELECT OkyanusDB.dbo.fn_AktifDonem())
			DECLARE @URL	VARCHAR(MAX) = 'https://api.hz.web.tv/p201'
			DECLARE @SECRET VARCHAR(MAX) = 'b547xzsLKBi36FDd3OU7s64dWYByL7hCm2YiI6LMBy'
			DECLARE @RESULT	NVARCHAR(MAX)= NULL


			
			IF @ISLEM = 1	--	GENEL KLASÖR LİSTESİ					
			BEGIN
			
				SELECT ISNULL((
					SELECT DISTINCT 
						 K3.ID_KADEME3
						,K3.AD
						,K2.ID_KADEME2
						,K2.AD
						,LINK	= ISNULL((
							SELECT GK.LINK
							FROM OnlineDersGenelKlasor	GK	
							WHERE	GK.ID_KADEME2	= KB.ID_KADEME2
								AND	GK.ID_KADEME3	= KB.ID_KADEME3
						),'')
					FROM v3Kademe3					K3
					JOIN v3KademeBilgi				KB	ON	KB.ID_KADEME3	= K3.ID_KADEME3
					JOIN v3Kademe2					K2	ON	K2.ID_KADEME2	= KB.ID_KADEME2
					FOR JSON AUTO
				),'[]')

			END

			IF @ISLEM = 2	--	GENEL KLASÖR KAYDET						
			BEGIN

				DELETE FROM OnlineDersGenelKlasor 

				INSERT INTO OnlineDersGenelKlasor (ID_KADEME2, ID_KADEME3, LINK, KYE_TCKIMLIKNO)
				SELECT ID_KADEME2, ID_KADEME3, LINK, @TCKIMLIKNO
				FROM OPENJSON(@SQLJSON)
				WITH(
					ID_KADEME3	INT,
					K2			NVARCHAR(MAX) AS JSON
				) AS KADEME3
				CROSS APPLY OPENJSON(KADEME3.K2) 
				WITH (
					ID_KADEME2	INT,
					LINK		VARCHAR(MAX)
				) KADEME2

			END

			IF @ISLEM = 3	--	TARIH LIST								
			BEGIN
			
				DECLARE @TARIHLIST TABLE (BASTARIH VARCHAR(MAX), BITTARIH VARCHAR(MAX))
				DECLARE @PAZARTESI DATE = DATEADD(week, DATEDIFF(day, 0, getdate())/7, 0)
				
				WHILE (SELECT DATEDIFF(DAY,@PAZARTESI,CAST('15.06.' + CAST(CAST((SELECT OkyanusDB.dbo.fn_AktifDonem()) AS INT) + 1 AS VARCHAR) AS date) )) > 0
				BEGIN				
	
					INSERT INTO @TARIHLIST (BASTARIH, BITTARIH)
					SELECT	BASTARIH = CONVERT(VARCHAR,@PAZARTESI,104),
							BITTARIH = CONVERT(VARCHAR,DATEADD(day,6, @PAZARTESI),104) 
			
					SET @PAZARTESI = DATEADD(WEEK,1,@PAZARTESI)
	
				END

				SELECT ISNULL((
					SELECT BASTARIH, BITTARIH
					FROM @TARIHLIST
					FOR JSON PATH
				),'[]')

			END

			IF @ISLEM = 4	--	SINIF TARIH DERS LINK GÖRÜNTÜLE			
			BEGIN
				SELECT ISNULL((
					SELECT G.ID_GUN
						,G.GUN
						,SA.DERSNO
						,SA.BASSAAT
						,SA.BITSAAT
						,SN = ISNULL((	SELECT 
											 GSN.DERSAD
											,GSN.LINK
											,GSN.SIFRE
										FROM OnlineDersSinif	GSN	
										WHERE	GSN.DERSNO			= SA.DERSNO
											AND GSN.ID_GUN			= G.ID_GUN
											AND GSN.KYE_TCKIMLIKNO	= @TCKIMLIKNO
											AND GSN.ID_SINIF		= @ID_SINIF
											AND GSN.AKTIF			= 1
										FOR JSON PATH
						),'[]')
						,VODAFONE =	ISNULL((SELECT 
												 GSN.DERSAD
												,GSN.ID_ONLINEDERSSINIFVODAFONE
												,O.ODAURL
												,LINK		= @URL	+ '/api/join?'		
																	+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,5,8) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'join' 
																						+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,5,8) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
																						+ @SECRET)												
												,LINKKONTROL= @URL	+ '/api/getMeetingInfo?'
																	+ 'meetingID=' + O.ID_ODAKONTROL
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'getMeetingInfo' 
																						+ 'meetingID=' + O.ID_ODAKONTROL 
																						+ @SECRET)
											FROM OnlineDersSinifVodafone	GSN	
											JOIN OnlineDersOgretmenVodafone O	ON	O.ID_OGRETMENVODAFONE	= GSN.ID_OGRETMENVODAFONE
											JOIN v3Kullanici				K	ON	K.TCKIMLIKNO			= O.TCKIMLIKNO
											WHERE	GSN.DERSNO			= SA.DERSNO
												AND GSN.ID_GUN			= G.ID_GUN
												AND GSN.KYE_TCKIMLIKNO	= @TCKIMLIKNO
												AND GSN.ID_SINIF		= @ID_SINIF
												AND GSN.AKTIF			= 1
											FOR JSON PATH
						),'[]')
						,GENELDRIVEID = ISNULL((
							SELECT TOP 1 right(GK.LINK, charindex('/', reverse(GK.LINK) + '/') - 1)
							FROM OnlineDersGenelKlasor	GK
							JOIN v3SatisTuru			ST	ON	ST.ID_KADEME2	= GK.ID_KADEME2
															AND ST.ID_KADEME3	= GK.ID_KADEME3
							JOIN v3Sinif				SN	ON	SN.ID_SATISTURU	= ST.ID_SATISTURU
							WHERE SN.ID_SINIF = @ID_SINIF
						), '')
					FROM Gun					G
					CROSS APPLY OnlineDersSaat	SA
					ORDER BY G.ID_GUN, SA.DERSNO
					FOR JSON AUTO, INCLUDE_NULL_VALUES
				),'[]')

			END

			IF @ISLEM = 5	--	SINIF TARIH DERS LINK KAYDET			
			BEGIN
							
				UPDATE OnlineDersSinif SET
					AKTIF = 0
				WHERE	ID_SINIF		= @ID_SINIF
					AND	KYE_TCKIMLIKNO	= @TCKIMLIKNO
					AND	AKTIF			= 1

				INSERT INTO OnlineDersSinif (ID_SINIF, ID_GUN, DERSNO, DERSAD, LINK, SIFRE, KYE_TCKIMLIKNO)
				SELECT @ID_SINIF, ID_GUN, DERSNO, DERSAD, LINK, SIFRE, @TCKIMLIKNO
				FROM OPENJSON(@SQLJSON)
				WITH (
					ID_GUN	INT,
					SA		NVARCHAR(MAX) AS JSON
				) AS T
				CROSS APPLY OPENJSON(T.SA) 
				WITH (
					DERSNO	INT,
					SN		NVARCHAR(MAX) AS JSON
				) AS S
				CROSS APPLY OPENJSON(S.SN) 
				WITH (
					DERSAD	VARCHAR(MAX),
					LINK	VARCHAR(MAX),
					SIFRE	VARCHAR(MAX)
				) AS N
				WHERE	DERSAD	IS NOT NULL
					AND LINK	IS NOT NULL
					AND LINK	!= ''
					AND DERSAD	!= ''

			END

			IF @ISLEM = 6	--	ÖĞRENCİ ONLINE DERS PROGRAM				
			BEGIN
				SELECT(
					SELECT (
						SELECT ISNULL((
							SELECT G.ID_GUN
								,G.GUN
								,SA.DERSNO
								,SA.BASSAAT
								,SA.BITSAAT
								,VODAFONE = (
									SELECT		LINK		= @URL	+ '/api/join?'		
																	+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,2,5) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'join' 
																						+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,2,5) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
																						+ @SECRET)												
												,LINKKONTROL= @URL	+ '/api/getMeetingInfo?'
																	+ 'meetingID=' + O.ID_ODAKONTROL
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'getMeetingInfo' 
																						+ 'meetingID=' + O.ID_ODAKONTROL 
																						+ @SECRET)
												,ADSOYAD	= (
													SELECT CONCAT_WS(' ', KO.AD, KO.SOYAD)
													FROM v3Kullanici	KO
													WHERE	KO.TCKIMLIKNO	= SNJ.KYE_TCKIMLIKNO
														AND KO.AKTIF		= 1
												)
												,SNJ.DERSAD
									FROM OnlineDersOgretmenVodafone O
									JOIN OnlineDersSinifVodafone	SNJ	ON	SNJ.DERSNO	 = SA.DERSNO
																		AND SNJ.ID_SINIF = (SELECT TOP 1 ID_SINIF FROM v3Ogrenci WHERE TCKIMLIKNO = @TC_OGRENCI)
																		AND SNJ.ID_GUN	 = G.ID_GUN
																		AND SNJ.AKTIF	 = 1
																		AND SNJ.ID_OGRETMENVODAFONE = O.ID_OGRETMENVODAFONE
									JOIN v3Kullanici				K	ON	K.TCKIMLIKNO = @TC_OGRENCI
									FOR JSON PATH
								)
								,OGRETMENKLASOR = ISNULL((SELECT TOP 1 LINK FROM OnlineDersOgretmenKlasor K WHERE K.KYE_TCKIMLIKNO = SNJV.KYE_TCKIMLIKNO AND K.ID_KADEME3 = (SELECT ID_KADEME3 FROM v3Sinif WHERE ID_SINIF = SNJV.ID_SINIF)), '')
								,SN.DERSAD
								,LINK		= ISNULL(SN.LINK,'')
								,SIFRE		= ISNULL(SN.SIFRE,'')
								,ADSOYAD	= (
									SELECT CONCAT_WS(' ', K.AD, K.SOYAD)
									FROM v3Kullanici	K
									WHERE	K.TCKIMLIKNO	= SN.KYE_TCKIMLIKNO
										AND K.AKTIF			= 1
								)
								,SINIFAD	= ISNULL((
									SELECT S.AD
									FROM OkyanusDB.dbo.vw_Sinif	S
									WHERE S.ID_SINIF	= SN.ID_SINIF
								),'')
							FROM Gun							G
							CROSS APPLY OnlineDersSaat			SA
							LEFT JOIN OnlineDersSinif			SN	ON	SN.DERSNO	= SA.DERSNO
																	AND SN.ID_GUN	= G.ID_GUN
																	AND SN.AKTIF	= 1
																	AND SN.ID_SINIF	= (SELECT TOP 1 ID_SINIF FROM v3Ogrenci WHERE TCKIMLIKNO = @TC_OGRENCI)
							LEFT JOIN OnlineDersSinifVodafone	SNJV	ON	SNJV.DERSNO	 = SA.DERSNO
																	AND SNJV.ID_SINIF = (SELECT TOP 1 ID_SINIF FROM v3Ogrenci WHERE TCKIMLIKNO = @TC_OGRENCI)
																	AND SNJV.ID_GUN	 = G.ID_GUN
																	AND SNJV.AKTIF	 = 1

							
							ORDER BY G.ID_GUN, SA.DERSNO															
							FOR JSON AUTO, INCLUDE_NULL_VALUES
						),'[]') 
					) AS LISTE,
					(	SELECT LINK
						FROM OnlineDersGenelKlasor	GK
						JOIN v3SatisTuru			ST	ON	ST.ID_KADEME2	= GK.ID_KADEME2
														AND ST.ID_KADEME3	= GK.ID_KADEME3
						JOIN v3Sinif				SN	ON	SN.ID_SATISTURU	= ST.ID_SATISTURU
						JOIN v3Ogrenci				O	ON	O.ID_SINIF		= SN.ID_SINIF
						WHERE O.TCKIMLIKNO = @TC_OGRENCI
					) AS GENELKLASOR,
					GENELKLASORID = (
						SELECT TOP 1 right(GK.LINK, charindex('/', reverse(GK.LINK) + '/') - 1)
						FROM OnlineDersGenelKlasor	GK
						JOIN v3SatisTuru			ST	ON	ST.ID_KADEME2	= GK.ID_KADEME2
														AND ST.ID_KADEME3	= GK.ID_KADEME3
						JOIN v3Sinif				SN	ON	SN.ID_SATISTURU	= ST.ID_SATISTURU
						JOIN v3Ogrenci				O	ON	O.ID_SINIF		= SN.ID_SINIF
						WHERE O.TCKIMLIKNO = @TC_OGRENCI
					),
					(	SELECT TOP 1 SUBEAD + '-' + SINIF +'-' + REPLACE(REPLACE(REPLACE( FORMAT(getdate(), 'dd.MM.yyyy HH:mm:ss'),'.','_'),':','_'),' ','_')
						FROM vw_SubeGrupSinif	SN
						JOIN v3Ogrenci			O	ON	O.ID_SINIF		= SN.ID_SINIF
						WHERE O.TCKIMLIKNO = @TC_OGRENCI				
					) AS DOSYAAD
					FOR JSON PATH
				)

			END

			IF @ISLEM = 7	--	SINIF ONLINE DERS PROGRAMI				
			BEGIN
				SELECT (		
				SELECT LISTE = ISNULL((
					SELECT G.ID_GUN
						,G.GUN
						,SA.DERSNO
						,SA.BASSAAT
						,SA.BITSAAT
						,OGRETMENKLASOR = ISNULL((SELECT TOP 1 LINK FROM OnlineDersOgretmenKlasor K WHERE K.KYE_TCKIMLIKNO = SN.KYE_TCKIMLIKNO AND K.ID_KADEME3 = (SELECT ID_KADEME3 FROM v3Sinif WHERE ID_SINIF = @ID_SINIF)), '')
						,SN.DERSAD
						,ODAURL		= IIF( O.TCKIMLIKNO = @TCKIMLIKNO
											,ISNULL(O.ODAURL,'')
											,@URL	+ '/api/getMeetingInfo?'
																	+ 'meetingID=' + O.ID_ODAKONTROL
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'getMeetingInfo' 
																						+ 'meetingID=' + O.ID_ODAKONTROL 
																						+ @SECRET)	
										)
						,LINK		= @URL	+ '/api/join?'		
											+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,5,8) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
											+ '&checksum=' 
											+ dbo.fn_strToSHA1( 'join' 
																+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,5,8) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
																+ @SECRET)
						,LINKKONTROL= @URL	+ '/api/getMeetingInfo?'
																	+ 'meetingID=' + O.ID_ODAKONTROL
																	+ '&checksum=' 
																	+ dbo.fn_strToSHA1( 'getMeetingInfo' 
																						+ 'meetingID=' + O.ID_ODAKONTROL 
																						+ @SECRET)	
						,ADSOYAD	= IIF(K.TCKIMLIKNO IS NOT NULL, CONCAT_WS(' ', K.AD, K.SOYAD),'')
						--(
						--	SELECT CONCAT_WS(' ', K.AD, K.SOYAD)
						--	FROM v3Kullanici	K
						--	WHERE	K.TCKIMLIKNO	= SN.KYE_TCKIMLIKNO
						--		AND K.AKTIF			= 1
						--)
						
						--,VODAFONEID= ISNULL(SNV.ID_ONLINEDERSSINIFVODAFONE,'')
						--,VODAFONEBASLIK= ISNULL(SNV.BASLIK,'')
						--,VODAFONEDERSAD	= ISNULL(SNV.DERSAD,'')
						--,VODAFONEIDODA	= ISNULL(O.ID_ODA,'')
						--,VODAFONESIFRE	= ISNULL(SUBSTRING( O.ID_ODA,2,5),'')
						--,VODAFONELINK= (SELECT 
						--					LINK = @URL + '/api/join?'		
						--								+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,2,5) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
						--								+ '&checksum=' 
						--								+ dbo.fn_strToSHA1( 'join' 
						--													+ 'meetingID=' + O.ID_ODA + '&password=' + SUBSTRING( O.ID_ODA,2,5) + '&fullName=' + dbo.fn_TurkceKarakter(UPPER(K.AD + K.SOYAD)) 
						--													+ @SECRET)
						--					,LINKKONTROL= @URL	+ '/api/getMeetingInfo?'
						--											+ 'meetingID=' + O.ID_ODAKONTROL
						--											+ '&checksum=' 
						--											+ dbo.fn_strToSHA1( 'getMeetingInfo' 
						--																+ 'meetingID=' + O.ID_ODAKONTROL 
						--																+ @SECRET)	
						--				FROM OnlineDersSinifVodafone	GSN	
						--				JOIN OnlineDersOgretmenVodafone O	ON	O.ID_OGRETMENVODAFONE	= GSN.ID_OGRETMENVODAFONE
						--				JOIN v3Kullanici				K	ON	K.TCKIMLIKNO			= @TCKIMLIKNO
						--				WHERE	GSN.DERSNO			= SA.DERSNO
						--					AND GSN.ID_GUN			= G.ID_GUN
						--					AND GSN.KYE_TCKIMLIKNO	= @TCKIMLIKNO
						--					AND GSN.ID_SINIF		= @ID_SINIF
						--					AND GSN.AKTIF			= 1
						--				FOR JSON PATH)
					FROM Gun							G
					CROSS APPLY OnlineDersSaat			SA
					LEFT JOIN OnlineDersSinifVodafone	SN	ON	SN.DERSNO	 = SA.DERSNO
															AND SN.ID_SINIF	 = @ID_SINIF
															AND SN.ID_GUN	 = G.ID_GUN
															AND SN.AKTIF	 = 1
					--LEFT JOIN OnlineDersSinifVodafone	SNJ	ON	SNJ.DERSNO	 = SA.DERSNO
					--										AND SNJ.ID_SINIF = @ID_SINIF
					--										AND SNJ.ID_GUN	 = G.ID_GUN
					--										AND SNJ.AKTIF	 = 1
					LEFT JOIN OnlineDersOgretmenVodafone O	ON	O.ID_OGRETMENVODAFONE = SN.ID_OGRETMENVODAFONE
					LEFT JOIN v3Kullanici				K	ON	K.TCKIMLIKNO = O.TCKIMLIKNO
					ORDER BY G.ID_GUN, SA.DERSNO															
					FOR JSON AUTO, INCLUDE_NULL_VALUES
				),'[]'),
				GENELKLASOR = (
					SELECT TOP 1 LINK
					FROM OnlineDersGenelKlasor	GK
					JOIN v3SatisTuru			ST	ON	ST.ID_KADEME2	= GK.ID_KADEME2
													AND ST.ID_KADEME3	= GK.ID_KADEME3
					JOIN v3Sinif				SN	ON	SN.ID_SATISTURU	= ST.ID_SATISTURU
					WHERE SN.ID_SINIF = @ID_SINIF
				),
				GENELKLASORID = (
					SELECT TOP 1 right(GK.LINK, charindex('/', reverse(GK.LINK) + '/') - 1)
					FROM OnlineDersGenelKlasor	GK
					JOIN v3SatisTuru			ST	ON	ST.ID_KADEME2	= GK.ID_KADEME2
													AND ST.ID_KADEME3	= GK.ID_KADEME3
					JOIN v3Sinif				SN	ON	SN.ID_SATISTURU	= ST.ID_SATISTURU
					WHERE SN.ID_SINIF = @ID_SINIF
				),
				DOSYAAD = (
					SELECT TOP 1 SUBEAD + ' - ' + SINIF + '-' + REPLACE(REPLACE(REPLACE( FORMAT(getdate(), 'dd.MM.yyyy HH:mm:ss'),'.','_'),':','_'),' ','_')
					FROM vw_SubeGrupSinif
					WHERE ID_SINIF = @ID_SINIF
				)
				FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
			END

			IF @ISLEM = 8	--	DERS PROGRAMI RAPOR						
			BEGIN
				SELECT * FROM (
					SELECT	 G.GUN
							,SA.DERSNO
							,SA.BASSAAT
							,SA.BITSAAT
							,DERSAD		= ISNULL((
								SELECT STUFF((
									SELECT '<hr />' +
										CASE	WHEN SN.DERSAD IS NULL THEN '<center>-</center>' 
												ELSE '<div style="font-size:8;margin:8px;">' +
													ISNULL(SN.DERSAD, '') + '</br>' 
													+ ISNULL((
														SELECT CONCAT_WS(' ', K.AD, K.SOYAD)
														FROM v3Kullanici	K
														WHERE	K.TCKIMLIKNO	= SN.KYE_TCKIMLIKNO
															AND K.AKTIF			= 1
													), '') 
													+ '</br><a href="' + ISNULL(SN.LINK,'') + '" target="_blank">' 
													+ (CASE WHEN @ID_SINIF = 0 THEN 'Derse Gitmek İçin Tıklayınız' ELSE SN.LINK END) + '.</a>
													</br> Şifre : '+
													ISNULL(SN.SIFRE,'')
													+'
													</div>'
											END
									FROM OnlineDersSinif	SN	
									WHERE	SN.DERSNO	= SA.DERSNO
									AND SN.ID_GUN	= G.ID_GUN
									AND SN.AKTIF	= 1
									AND (
											(@ID_SINIF = 0 AND SN.ID_SINIF	= (SELECT TOP 1 ID_SINIF FROM v3Ogrenci WHERE TCKIMLIKNO = @TC_OGRENCI)) 
											OR 
											(@ID_SINIF > 0 AND SN.ID_SINIF = @ID_SINIF)
										)
								FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 6, ''))
							,'<center>-</center>')
							
					FROM Gun							G
					CROSS APPLY OnlineDersSaat			SA
					--LEFT JOIN OnlineDersSinif			SN	ON	SN.DERSNO	= SA.DERSNO
					--										AND SN.ID_GUN	= G.ID_GUN
					--										AND SN.AKTIF	= 1
					--										AND (
					--												(@ID_SINIF = 0 AND SN.ID_SINIF	= (SELECT TOP 1 ID_SINIF FROM v3Ogrenci WHERE TCKIMLIKNO = @TC_OGRENCI)) 
					--												OR 
					--												(@ID_SINIF > 0 AND SN.ID_SINIF = @ID_SINIF)
					--											)
				) TABLEX
				PIVOT (
					MAX(DERSAD)
					FOR GUN IN ([PAZARTESİ], [SALI], [ÇARŞAMBA], [PERŞEMBE], [CUMA], [CUMARTESİ], [PAZAR])
				) PIVOTTABLE
				WHERE [PAZARTESİ] IS NOT NULL OR [SALI] IS NOT NULL OR [ÇARŞAMBA] IS NOT NULL OR [PERŞEMBE] IS NOT NULL OR [CUMA] IS NOT NULL OR [CUMARTESİ] IS NOT NULL OR [PAZAR] IS NOT NULL
				ORDER BY DERSNO
			END

			IF @ISLEM = 9	--	SINIF ONLINE DERS EKLE					
			BEGIN			
				IF EXISTS(SELECT * FROM OnlineDersSinifJitsi WHERE ID_SINIF = JSON_VALUE(@SQLJSON, '$.ID_SINIF') AND AKTIF = 1)
				BEGIN
					DECLARE @ADSOYAD VARCHAR(MAX) = (SELECT AD + ' ' + SOYAD FROM OnlineDersSinifJitsi S JOIN OkyanusDB.dbo.v3Kullanici K ON K.TCKIMLIKNO = S.KYE_TCKIMLIKNO WHERE S.ID_SINIF = JSON_VALUE(@SQLJSON, '$.ID_SINIF') AND S.AKTIF = 1)
					DECLARE @HATAMESAJ VARCHAR(MAX) = 'Sınıf için şu anda ' + @ADSOYAD + ' ders vermektedir!'
					EXEC [dbo].[sp_CustomRaiseError] @MESSAGE = @HATAMESAJ, @SEVERITY = 16, @STATE = 1, @ID_LOG = @_ID_LOG, @ID_LOGTUR = 3
				END
				ELSE
				BEGIN
					--UPDATE OnlineDersSinifJitsi SET
					--	AKTIF = 0
					--WHERE	ID_SINIF = JSON_VALUE(@SQLJSON, '$.ID_SINIF')
					--	AND	AKTIF	 = 1
					DECLARE @IDTABLE TABLE (ID INT)
					INSERT INTO OnlineDersSinifJitsi (ID_SINIF, ID_GUN, DERSNO, DERSAD, BASLIK, ODAAD, SIFRE, KYE_TCKIMLIKNO)
					OUTPUT inserted.ID_ONLINEDERSSINIFJITSI INTO @IDTABLE
					SELECT	 JSON_VALUE(@SQLJSON, '$.ID_SINIF')
							,JSON_VALUE(@SQLJSON, '$.ID_GUN')
							,JSON_VALUE(@SQLJSON, '$.DERSNO')
							,JSON_VALUE(@SQLJSON, '$.DERSAD')
							,JSON_VALUE(@SQLJSON, '$.BASLIK')
							,JSON_VALUE(@SQLJSON, '$.ODAAD')
							,JSON_VALUE(@SQLJSON, '$.SIFRE')
							,@TCKIMLIKNO

					SELECT ID FROM @IDTABLE
				END
			END

			IF @ISLEM = 10	--	SINIF ONLINE DERS SİL					
			BEGIN							
				UPDATE OnlineDersSinifJitsi SET
					AKTIF = 0
				WHERE ID_ONLINEDERSSINIFJITSI = @ID_ONLINEDERSSINIFJITSI
			END

			IF @ISLEM = 11	--	SINIF ONLINE EKRAN PAYLAŞIM ID GÜNCELLE	
			BEGIN							
				UPDATE OnlineDersSinifJitsi SET
					EKRANPAYLASIMID = @EKRANPAYLASIMID
				WHERE ID_ONLINEDERSSINIFJITSI = @ID_ONLINEDERSSINIFJITSI
			END

			IF @ISLEM = 12	--	ÖĞRETMEN KADEME3 KLASOR GETİR			
			BEGIN
				SELECT ISNULL(
				(
					SELECT TOP 1 LINK
					FROM OnlineDersOgretmenKlasor
					WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND ID_KADEME3 = @ID_KADEME3
				), '')
			END

			IF @ISLEM = 13	--	ÖĞRETMEN KADEME3 KLASOR KAYDET			
			BEGIN
				DELETE FROM OnlineDersOgretmenKlasor WHERE KYE_TCKIMLIKNO = @TCKIMLIKNO AND ID_KADEME3 = @ID_KADEME3
				INSERT INTO OnlineDersOgretmenKlasor (ID_KADEME3, LINK, KYE_TCKIMLIKNO)
				SELECT @ID_KADEME3, @LINK, @TCKIMLIKNO
			END

			IF @ISLEM = 14	--	SINIF ONLINE DERS HERKESI SUSTUR		
			BEGIN							
				UPDATE OnlineDersSinifJitsi SET
					HERKESISUSTUR = @HERKESISUSTUR
				WHERE ID_ONLINEDERSSINIFJITSI = @ID_ONLINEDERSSINIFJITSI
			END

			IF @ISLEM = 15	--	SINIF ONLINE DERS HERKESI SUSTUR GETİR	
			BEGIN							
				SELECT HERKESISUSTUR FROM OnlineDersSinifJitsi
				WHERE ID_ONLINEDERSSINIFJITSI = @ID_ONLINEDERSSINIFJITSI
			END

			

			IF @ISLEM = 16	--	ÖĞRETMEN ONLINE DERS PROGRAMI			
			BEGIN

				SELECT LISTE = ISNULL((
					SELECT	 G.ID_GUN
							,G.GUN
							,SA.DERSNO
							,SA.BASSAAT
							,SA.BITSAAT
							,SN.DERSAD
							,SINIFAD = (SELECT S.AD FROM V3sinif S WHERE S.ID_SINIF = SN.ID_SINIF)
					FROM Gun							G
					CROSS APPLY OnlineDersSaat			SA
					LEFT JOIN OnlineDersSinif			SN	ON	SN.DERSNO		= SA.DERSNO
															AND KYE_TCKIMLIKNO	= @TC_OGRETMEN
															AND SN.ID_GUN		= G.ID_GUN
															AND SN.AKTIF		= 1
					ORDER BY G.ID_GUN, SA.DERSNO
					FOR JSON AUTO, INCLUDE_NULL_VALUES
				),'[]')
				
				
			END
			IF @ISLEM = 17	--	ÖĞRETMEN ONLINE DERS PROGRAMI KALDIR	
			BEGIN

				UPDATE OnlineDersSinif SET
					AKTIF = 0
				WHERE KYE_TCKIMLIKNO = @TC_OGRETMEN


				
			END
			IF @ISLEM = 18	--	ÖĞRETMEN ONLINE ÖĞRETMEN LİSTESİ		
			BEGIN

				SELECT ISNULL((
					SELECT DISTINCT K.AD,K.SOYAD,K.TCKIMLIKNO 
					FROM OnlineDersSinif	ODS
					JOIN vw_SubeGrupSinif	GS	ON	GS.ID_SINIF	= ODS.ID_SINIF
					JOIN v3Kullanici		K	ON	K.TCKIMLIKNO= ODS.KYE_TCKIMLIKNO
				    WHERE GS.ID_SUBE = @ID_SUBE
						AND  ODS.AKTIF=1
					FOR JSON PATH
				),'[]')


				
			END

			
			
			IF @ISLEM = 19	--	VODAFONE EKLE							
			BEGIN
				DECLARE @URL19		NVARCHAR(MAX) = NULL

				SET @ID_OGRETMENVODAFONE	= (SELECT TOP 1 ID_OGRETMENVODAFONE FROM OnlineDersOgretmenVodafone WHERE TCKIMLIKNO = @TCKIMLIKNO)
				SET @RESULT					= NULL
				DECLARE @HATA			INT	= 0 
				IF @ID_OGRETMENVODAFONE IS NULL
				BEGIN
					WHILE EXISTS (SELECT TOP 1 1 FROM OPENJSON(@RESULT) WITH ( meetingID VARCHAR(100) ) WHERE meetingID IS NULL) OR @RESULT IS NULL
					BEGIN
						DECLARE @ROOMNAME	VARCHAR(20)		= 'okyanuskoleji' -- TCKIMLIKNO
						DECLARE @RNSHA1		VARCHAR(MAX)	= dbo.fn_strToSHA1(@ROOMNAME )
						DECLARE @STR19		NVARCHAR(MAX)	= 'name=' + @ROOMNAME
															+ '&meetingID=' + @RNSHA1
															+ '&attendeePW=' + SUBSTRING( @RNSHA1, 2, 5)
															+ '&moderatorPW=' + SUBSTRING( @RNSHA1, 5, 8)
															+ '&guestPolicy=' + 'ASK_MODERATOR'
						SET @URL19= @URL + '/api/create?' + @STR19 + '&checksum=' + dbo.fn_strToSHA1('create' + @STR19 + @SECRET)

						EXEC sp_VodafoneXmlResult @URL = @URL19, @RESULT = @RESULT OUTPUT
						SET @HATA = @HATA + 1 
						IF @HATA > 2
						BEGIN
							RAISERROR('Oda Oluşturulurken Hata!', 16, 1)
							RETURN
						END
					END
					
					DECLARE @INSERT7 TABLE (ID INT)

					INSERT INTO OnlineDersOgretmenVodafone (TCKIMLIKNO, ID_ODA, ID_ODAKONTROL,ODAURL)
					OUTPUT inserted.ID_OGRETMENVODAFONE INTO @INSERT7
					SELECT TOP 1 @TCKIMLIKNO, dbo.fn_strToSHA1(@ROOMNAME ),meetingID , @URL19
					FROM OPENJSON(@RESULT) 
					WITH ( meetingID VARCHAR(100) )

					SET @ID_OGRETMENVODAFONE = (SELECT TOP 1 ID FROM @INSERT7)
				END

				INSERT INTO OnlineDersSinifVodafone (ID_SINIF, ID_GUN, DERSNO, DERSAD, BASLIK, ID_OGRETMENVODAFONE, KYE_TCKIMLIKNO)
				SELECT	 JSON_VALUE(@SQLJSON, '$.ID_SINIF')
						,JSON_VALUE(@SQLJSON, '$.ID_GUN')
						,JSON_VALUE(@SQLJSON, '$.DERSNO')
						,JSON_VALUE(@SQLJSON, '$.DERSAD')
						,JSON_VALUE(@SQLJSON, '$.BASLIK')
						,@ID_OGRETMENVODAFONE
						,@TCKIMLIKNO

			END

			IF @ISLEM = 20	--	VODAFONE SİL							
			BEGIN

				UPDATE OnlineDersSinifVodafone SET
					AKTIF = 0 
				WHERE ID_ONLINEDERSSINIFVODAFONE = @ID_ONLINEDERSSINIFVODAFONE

			END

			IF @ISLEM = 21	--	VODAFONE ODA AÇ							
			BEGIN

				SET @RESULT					= NULL
				DECLARE @URL21 NVARCHAR(MAX)= (	SELECT TOP 1 O.ODAURL
												FROM OnlineDersOgretmenVodafone O 
												JOIN OnlineDersSinifVodafone	S	ON	S.ID_OGRETMENVODAFONE = O.ID_OGRETMENVODAFONE
												WHERE S.ID_ONLINEDERSSINIFVODAFONE = @ID_ONLINEDERSSINIFVODAFONE
											   )
				EXEC sp_VodafoneXmlResult @URL = @URL21, @RESULT = @RESULT OUTPUT

				SELECT @RESULT

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
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[OnlineDersSinifVodafone] WITH CHECK CHECK CONSTRAINT [FK_OnlineDersSinifVodafone_OnlineDersSinifVodafone];


GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO