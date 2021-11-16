INSERT INTO MENU (AD,ACIKLAMA,KOD,URL,RESIM,GIZLI,OZEL) VALUES('Şube Yetki','Şube Yetki','022002','#IdariIsler/SubeYetki','icon-user',0,0)

GO
CREATE TABLE [dbo].[BransOgretmen] (
    [ID_BRANSOGRETMEN] INT          IDENTITY (1, 1) NOT NULL,
    [ID_OGRETMEN]      INT          NULL,
    [BRANS]            VARCHAR (50) NULL,
    CONSTRAINT [PK_BransOgretmen] PRIMARY KEY CLUSTERED ([ID_BRANSOGRETMEN] ASC)
);


GO
PRINT N'Creating Synonym [dbo].[Tbl_OnlineSinavOgrenciOturum]...';


GO
CREATE SYNONYM [dbo].[Tbl_OnlineSinavOgrenciOturum] FOR [PusulamSinav].[dbo].[Tbl_OnlineSinavOgrenciOturum];


GO
PRINT N'Creating Function [dbo].[Fn_KullaniciKademeGetir]...';


GO

CREATE FUNCTION [dbo].[Fn_KullaniciKademeGetir](@TC varchar(11))
RETURNS VARCHAR(MAX)
AS	BEGIN
Declare @BILGILER varchar(max)='' 
DECLARE @ID VARCHAR(MAX)=''
SELECT @BILGILER = @BILGILER + K.AD + CASE WHEN KK.XBASE = 1 THEN ' (XBASE)' ELSE '' END+', ', @ID = K.ID_KADEME 
FROM OkyanusDB.dbo.v4KullaniciKademe KK 
INNER JOIN OkyanusDB.dbo.v3Kademe K on K.ID_KADEME = KK.ID_KADEME
WHERE KK.TCKIMLIKNO = @TC
GROUP BY K.AD, K.ID_KADEME, KK.XBASE
ORDER BY K.ID_KADEME
	
Return IsNull(nullif(@BILGILER,''),'')
END
GO
PRINT N'Creating Function [dbo].[Fn_KullaniciTipiGetir]...';


GO


CREATE FUNCTION [dbo].[Fn_KullaniciTipiGetir](@TC varchar(11))
RETURNS VARCHAR(MAX)
AS	BEGIN
Declare @BILGILER varchar(max)='' 
	
SELECT @BILGILER = @BILGILER + kt.[AD]+', ' 
FROM OkyanusDB.dbo.v3Subeyetki s 
INNER JOIN OkyanusDB..v3Sube sb on s.ID_SUBE = sb.ID_SUBE
INNER JOIN OkyanusDB.dbo.v3KullaniciTipi kt on kt.ID_KULLANICITIPI=s.ID_KULLANICITIPI 
WHERE s.TCKIMLIKNO = @TC
GROUP BY kt.[AD]
	
Return IsNull(nullif(@BILGILER,''),'')
END
GO
PRINT N'Creating Function [dbo].[Fn_SubeYetkiGetir]...';


GO


	CREATE FUNCTION [dbo].[Fn_SubeYetkiGetir](@TC varchar(11))
	RETURNS VARCHAR(MAX)
	AS	BEGIN
	Declare @BILGILER varchar(max)='' 
	
	Select @BILGILER = @BILGILER + (sb.AD) +' '+kt.[AD]+' <br>' from OkyanusDB.dbo.v3Subeyetki s 
	inner join OkyanusDB..V3Sube sb on s.ID_SUBE = sb.ID_SUBE
	inner join OkyanusDB.dbo.v3KullaniciTipi kt on kt.ID_KULLANICITIPI=s.ID_KULLANICITIPI 
	where s.TCKIMLIKNO = @TC
	
	Return IsNull(nullif(@BILGILER,''),'')
	END

	-- Select [dbo].[Fn_SubeYetkiGetir]('53110117830')
GO
PRINT N'Creating Function [dbo].[fn_Split2]...';


GO

create Function [dbo].[fn_Split2]
(
 @String NVARCHAR (max),
 @Delimiter NVARCHAR (10),
 @Number INT = null
)
returns @RT_ValueTable table (RN INT, [Value] NVARCHAR(max))

BEGIN

	 DECLARE @ValueTable TABLE (RN INT, [Value] NVARCHAR(max))

	 DECLARE @NextString NVARCHAR(max)
	 DECLARE @Pos INT
	 DECLARE @NextPos INT
	 DECLARE @CommaCheck NVARCHAR(1)
	 DECLARE @index	INT
	 
	 --Initialize
	 SET @NextString = ''
	 SET @CommaCheck = right(@String,1) 
	 SET @index = 1
	 
	 --Check for trailing Comma, if not exists, INSERT
	 --if (@CommaCheck <> @Delimiter )
	 SET @String = @String + @Delimiter
	 
	 --Get position of first Comma
	 SET @Pos = charindex(@Delimiter,@String)
	 SET @NextPos = 1
	 
	 --Loop while there is still a comma in the String of levels
	 WHILE (@pos <>  0)  
	 BEGIN
	  SET @NextString = substring(@String,1,(@Pos - 1))
	 
	  IF (@NextString <> '')
	  BEGIN
		INSERT INTO @ValueTable (RN, [Value]) VALUES (@index, @NextString)
		SET @index = @index + 1
	  END
	 
	  SET @String = substring(@String,@pos + len(@Delimiter),len(@String))
	  
	  SET @NextPos = @Pos
	  SET @pos  = charindex(@Delimiter,@String)
	  
	END
 
	IF @Number IS NULL
		INSERT INTO @RT_ValueTable SELECT RN, [Value] FROM @ValueTable
	ELSE
		INSERT INTO @RT_ValueTable SELECT RN, [Value] FROM @ValueTable WHERE RN = @Number
			
 RETURN

END
GO
PRINT N'Creating Function [dbo].[fnSplit2]...';


GO

create function [dbo].[fnSplit2]
(
 @String nvarchar (max),
 @Delimiter nvarchar (10)
)
returns @ValueTable table ([Value] nvarchar(max))

BEGIN
	 declare @NextString nvarchar(max)
	 declare @Pos int
	 declare @NextPos int
	 declare @CommaCheck nvarchar(1)
	 
	 --Initialize
	 set @NextString = ''
	 set @CommaCheck = right(@String,1) 
	 
	 --Check for trailing Comma, if not exists, INSERT
	 --if (@CommaCheck <> @Delimiter )
	 set @String = @String + @Delimiter
	 
	 --Get position of first Comma
	 set @Pos = charindex(@Delimiter,@String)
	 set @NextPos = 1
	 
	 --Loop while there is still a comma in the String of levels
	 while (@pos <>  0)  
	 begin
	  set @NextString = substring(@String,1,(@Pos - 1))
	 
	  insert into @ValueTable ( [Value]) Values (@NextString)
	 
	  set @String = substring(@String,@pos + len(@Delimiter),len(@String))
	  
	  set @NextPos = @Pos
	  set @pos  = charindex(@Delimiter,@String)
	end
 
 return

END
GO
PRINT N'Creating Procedure [dbo].[sp_KullaniciArama]...';


GO
CREATE PROC [dbo].[sp_KullaniciArama]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,		
	@DURUM				INT				= 0,
    @ID_KULLANICI		INT				= 0,
	@ID_KULLANICITIPI	INT				= 0,
	@GRUP				INT				= NULL,
	@DONEMKODS			VARCHAR(50)		= '',
	@ID_SUBE			INT				= 0,
	@ID_DONEM			INT				= 0,
	@ID_OGRENCI			INT				= 0,
	@ID_SINIF			INT				= 0,
	@AD					VARCHAR(50)		= '',
	@TC_KULLANICI		VARCHAR(11)		= '',
	@SOYAD				VARCHAR(50)		= '',
	@SINIFTIP			VARCHAR(6)		= NULL,
	@BRANS				VARCHAR(50)	    = NULL
AS
BEGIN
	BEGIN TRY    
		DECLARE @MSG			VARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT
		DECLARE @_ID_LOG INT = 0
		DECLARE @return_variable INT
		
		DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))

		DECLARE @LOGJSON VARCHAR(MAX)
		SET @LOGJSON = (
			SELECT	 
				ISLEM						= @ISLEM		
				,TCKIMLIKNO					= @TCKIMLIKNO	
				,OTURUM						= @OTURUM		
				,ID_MENU					= @ID_MENU	
				,ID_KULLANICI				= @ID_KULLANICI
				,ID_KULLANICITIPI			= @ID_KULLANICITIPI
				,GRUP						= @GRUP
				,DONEMKODS					= @DONEMKODS
				,ID_SUBE					= @ID_SUBE
				,ID_SINIF					= @ID_SINIF
				,ID_OGRENCI					= @ID_OGRENCI
				,AD							= @AD
				,TC_KULLANICI				= @TC_KULLANICI
				,SOYAD						= @SOYAD
				,SINIFTIP					= @SINIFTIP

			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
		)	
		DECLARE		@DONEMKOD			VARCHAR(5)
		SET		@DONEMKOD		=	(Select DONEM from OkyanusDB.dbo.v3AktifDonem where AKTIF=1 )	
			DECLARE @ID_LOG INT = 0		
			--EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
			SET @ID_LOG = 1
			IF @ID_LOG = 1
			BEGIN
				IF @ISLEM = 1
				BEGIN
					IF @DONEMKODS=''
					BEGIN
					SET @DONEMKODS=@DONEMKOD
					END

					declare @tDURUM				INT					= @DURUM			
					declare @tID_KULLANICI		INT					= @ID_KULLANICI	
					declare @tOTURUM			VARCHAR(50)			= @OTURUM			
					declare @tID_KULLANICITIPI	INT					= @ID_KULLANICITIPI
					declare @tBRANS				VARCHAR(50)			= @BRANS			
					declare @tGRUP				INT					= @GRUP			
					declare @tDONEMKODS			VARCHAR(50)			= @DONEMKODS		
					declare @tID_SUBE			INT					= @ID_SUBE			
					declare @tID_DONEM			INT					= @ID_DONEM		
					declare @tID_OGRENCI		INT					= @ID_OGRENCI		
					declare @tID_SINIF			INT					= @ID_SINIF		
					declare @tAD				VARCHAR(50)			= @AD				
					declare @tTC				VARCHAR(11)			= @TC_KULLANICI				
					declare @tTC_KULLANICI		VARCHAR(11)			= @TC_KULLANICI	
					declare @tSOYAD				VARCHAR(50)			= @SOYAD			
					declare @tSINIFTIP			VARCHAR(6)			= @SINIFTIP	

					IF	@tID_KULLANICITIPI = 4				--Öğrenci Arama
					BEGIN
						
						select distinct  K.ID_KADEME3,K.AD 
						into #OKULONCESIOGRENCI 
						from OkyanusDB.dbo.v3Kademe3		K 
						JOIN OkyanusDB.dbo.v3KademeBilgi	KB	ON KB.ID_KADEME3=K.ID_KADEME3
						WHERE ID_KADEME=2

						select Value GRUP INTO #GRUPLAROGRENCI from dbo.fnSplit2(@GRUP, ',')
						DECLARE @OKULONCESIMI INT=( SELECT COUNT(*) FROM #GRUPLAROGRENCI G JOIN #OKULONCESIOGRENCI O ON O.AD=G.GRUP  )

			
						 SELECT DISTINCT
								 Kul.ID_KULLANICI		ID
								,Kul.Ad					AD
								,Kul.Soyad				SOYAD
								,ISNULL(Kul.TCKIMLIKNO,'') TC
								--,ISNULL(Rsm.FOTOGRAF,Cast('' as image))	FOTOGRAF
			 					,[dbo].[Fn_SubeYetkiGetir](Kul.TCKIMLIKNO) as YetkiliOlduguSubeler
								,ISNULL(S.AD, '') AS SINIF
				

							INTO #TEMPOGRENCI
							FROM OkyanusDB.dbo.v3Kullanici		Kul

						INNER JOIN OkyanusDB.dbo.v3SubeYetki		sy	on	sy.TCKIMLIKNO	= Kul.TCKIMLIKNO and sy.ID_SUBE in (Select ID_SUBE from OkyanusDB.dbo.v3SubeYetki where		TCKIMLIKNO=@TCKIMLIKNO)
						LEFT  JOIN OkyanusDB.dbo.v3Ogrenci			O	ON	O.TCKIMLIKNO	= SY.TCKIMLIKNO
						LEFT  JOIN OkyanusDB.dbo.vw_SinifOgrenci	SO	ON	SO.TCKIMLIKNO	= Kul.TCKIMLIKNO
						LEFT  JOIN OkyanusDB.dbo.vw_v4SinifDetay	SD	ON	SD.ID_SINIF		= SO.ID_SINIF
						LEFT  JOIN OkyanusDB.dbo.v3Sinif			S	ON	S.ID_SINIF		= O.ID_SINIF
						WHERE	
								(@tTC = Kul.TCKIMLIKNO OR @tTC = '')
							AND (@ID_SUBE = 0		OR	sy.ID_SUBE = @ID_SUBE)
							AND (@AD = ''			OR	Kul.Ad like '%' + @AD + '%')
							AND (@SOYAD = ''		OR	Kul.Soyad like '%' + @SOYAD + '%')
							AND ((@OKULONCESIMI=0	and (@GRUP = 0 OR SD.ID_KADEME3 = @GRUP)) or (@OKULONCESIMI=1 and (SD.ID_KADEME3 = (SELECT ID_KADEME3 FROM #OKULONCESIOGRENCI OK WHERE	OK.AD=@GRUP))))
							AND (@ID_SINIF = 0		OR	SD.ID_SINIF = @ID_SINIF)
							AND	AKTIF=1
							AND (@GRUP = 0		OR	SD.ID_KADEME3 = @GRUP)

					
					SELECT ISNULL((
						SELECT ID
						,ADSOYAD = AD + ' ' + SOYAD						
						,TC
						,ISNULL(Rsm.FOTOGRAF,Cast('' as image))	FOTOGRAF
						,YetkiliOlduguSubeler
						,SINIF
						,SIFREDEGISTIRMEYETKI = IIF(EXISTS(SELECT TOP 1 1 FROM Pusulam.dbo.KullaniciSifreEngel E WHERE E.TCKIMLIKNO = TC),1,0)
						FROM #TEMPOGRENCI T
						LEFT  JOIN OkyanusDB.dbo.v3Resim			Rsm	ON	Rsm.TCKIMLIKNO	= T.TC
						FOR JSON PATH
					),'[]')
					END

					ELSE IF @tID_KULLANICITIPI  = 5 -- Veli Arama
					BEGIN
						DROP TABLE IF EXISTS #TABLE
						DROP TABLE IF EXISTS #GRUPLARVELI
						DROP TABLE IF EXISTS #OKULONCESIVELI

						select distinct  K.ID_KADEME3,K.AD 
						into #OKULONCESIVELI 
						from OkyanusDB.dbo.v3Kademe3		K 
						JOIN OkyanusDB.dbo.v3KademeBilgi	KB	ON KB.ID_KADEME3=K.ID_KADEME3
						WHERE ID_KADEME=2

						select Value GRUP INTO #GRUPLARVELI from dbo.fnSplit2(@GRUP, ',')
						DECLARE @_OKULONCESIMI INT=( SELECT COUNT(*) FROM #GRUPLARVELI G JOIN #OKULONCESIVELI O ON O.AD=G.GRUP  )

					

		

						SELECT	 DISTINCT
									K.ID_KULLANICI			ID
								,K.AD					
								,K.SOYAD					
								,ISNULL(K.TCKIMLIKNO,'') TC
						INTO #TABLE
						FROM OkyanusDB.dbo.v3OgrenciTum				O
						JOIN OkyanusDB.DBO.vw_SinifOgrenci			SO	ON	SO.TCKIMLIKNO	= O.TCKIMLIKNO
						JOIN OkyanusDB.DBO.v3OgrenciVeli			V	ON	V.TCKIMLIKNO_OGR	= O.TCKIMLIKNO
						JOIN OkyanusDB.DBO.v3Kullanici				K	ON	K.TCKIMLIKNO	= V.TCKIMLIKNO
						JOIN OkyanusDB.dbo.vw_v4SinifDetay			SD	ON	SD.ID_SINIF		= SO.ID_SINIF	 and SD.DONEM = O.DONEM		
						JOIN OkyanusDB.DBO.Fn_Sube(@TCKIMLIKNO)	KY	ON	KY.ID_SUBE		= SD.ID_SUBE
						WHERE  (@ID_SUBE = 0 OR SD.ID_SUBE = @ID_SUBE)
							AND (SD.ID_SINIF = @ID_SINIF or @ID_SINIF = 0 ) 
							AND (@AD = '' OR K.AD like '%' + @AD + '%')
							AND (@SOYAD = '' OR K.SOYAD like '%' + @SOYAD + '%')
							and (@GRUP = 0 OR SD.ID_KADEME3 = @GRUP)
							--AND (	( @_OKULONCESIMI=0 and (@_GRUP = '0' OR SD.KADEME3 = @_GRUP) )		
							--	or	( @_OKULONCESIMI=1 and (@_GRUP = '0' OR SD.ID_KADEME3 = (SELECT ID_KADEME3 FROM #OKULONCESI WHERE AD=@_GRUP))) )
							and O.DONEM=@DONEMKOD	

						SELECT ISNULL((
						SELECT *
								,ADSOYAD = AD + ' ' + SOYAD
								,Cast('' as image)	 FOTOGRAF
								,[dbo].[Fn_SubeYetkiGetir](TC) as YetkiliOlduguSubeler
 								,OGRLIST = (	SELECT K.ID_KULLANICI, K.TCKIMLIKNO, K.OTURUM, ADSOYAD = K.AD + ' ' + K.SOYAD
												FROM OkyanusDB.DBO.v3OgrenciVeli	V
												JOIN OkyanusDB.DBO.v3Kullanici		K	ON	K.TCKIMLIKNO	= V.TCKIMLIKNO_OGR
												WHERE V.TCKIMLIKNO	= T.TC
												FOR JSON PATH
											)
								,SIFREDEGISTIRMEYETKI = IIF(EXISTS(SELECT TOP 1 1 FROM Pusulam.dbo.KullaniciSifreEngel E WHERE E.TCKIMLIKNO = TC),1,0)
						FROM #TABLE T
						ORDER BY AD, SOYAD
						FOR JSON PATH
					),'[]')

						DROP TABLE IF EXISTS #TABLE
						DROP TABLE IF EXISTS #GRUPLARVELI
						DROP TABLE IF EXISTS #OKULONCESIVELI
					END

					ELSE
					BEGIN
						SELECT distinct 
								Kul.ID_KULLANICI			ID
								,Kul.Ad					AD
								,Kul.Soyad					SOYAD
								,ISNULL(Kul.TCKIMLIKNO,'') TC
								,[dbo].[Fn_KullaniciTipiGetir](Kul.TCKIMLIKNO) as YETKI	
					 			,0							ID_YETKI
					 			,[dbo].[Fn_SubeYetkiGetir](Kul.TCKIMLIKNO)  AS YetkiliOlduguSubeler,
					 			1 as DURUM --techlife kullanıcıları
								,'' as GUID
								,'' as CEP
								,'' as EV
								,Cast(isnull(Kul.DOGUMTARIHI,'') as varchar) as DOGUMTARIHI
								,'' as EMAIL
								,[dbo].[Fn_KullaniciKademeGetir](Kul.TCKIMLIKNO) as KADEME	
							into #temp
							FROM OkyanusDB.dbo.v3Kullanici			Kul
								 JOIN OkyanusDB.dbo.v3SubeYetki		Yetki	ON  Kul.TCKIMLIKNO = Yetki.TCKIMLIKNO   --AND Yetki.ID_SUBE in (Select ID_SUBE from OkyanusDB.dbo.v3SubeYetki where				TCKIMLIKNO=@tTC_KULLANICI)
							LEFT JOIN OkyanusDB.dbo.v3Ogretmen		Ogt		ON	Kul.TCKIMLIKNO = Ogt.TCKIMLIKNO
							LEFT JOIN BransOgretmen					Brn		ON	Brn.ID_OGRETMEN = Ogt.ID_OGRETMEN
						WHERE
							((@tID_SUBE = 0 )OR Yetki.ID_SUBE = @tID_SUBE)
						AND (@tID_KULLANICITIPI = 0 OR Yetki.ID_KULLANICITIPI = @tID_KULLANICITIPI)
						AND (@tAD = '' OR Kul.Ad like '%' + @tAD + '%')
						AND (@tSOYAD = '' OR Kul.Soyad like '%' + @tSOYAD + '%')
						AND (@tBRANS = '0' OR Brn.BRANS = @tBRANS)
						AND Kul.AKTIF=1
						AND (Kul.TCKIMLIKNO like '%'+@tTC+'%' or @tTC='')
						ORDER BY Kul.AD 

						SELECT ISNULL((
							Select T.ID,
							ADSOYAD= T.AD + ' ' + T.SOYAD, 
							T.TC,
							ISNULL((Select top 1 r.FOTOGRAF from OkyanusDB.dbo.v3Resim r where r.TCKIMLIKNO=T.TC),Cast('' as image))	FOTOGRAF,
							T.YETKI,
							T.ID_YETKI,
							T.YetkiliOlduguSubeler,
							T.DURUM,
							T.GUID,
							T.CEP,
							T.EV,
							T.DOGUMTARIHI,
							T.EMAIL,
							T.KADEME,
							--ISNULL((SELECT TOP 1 1 FROM Pusulam.dbo.KullaniciSifreEngel WHERE TCKIMLIKNO=t.TC),0) AS SIFREDEGISTIRMEYETKI,
							SIFREDEGISTIRMEYETKI = IIF(EXISTS(SELECT TOP 1 1 FROM Pusulam.dbo.KullaniciSifreEngel E WHERE E.TCKIMLIKNO = T.TC),1,0),
							OGRLIST = ISNULL((	SELECT K.ID_KULLANICI, K.TCKIMLIKNO, K.OTURUM, ADSOYAD = K.AD + ' ' + K.SOYAD
														FROM OkyanusDB.DBO.v3OgrenciVeli	V
														JOIN OkyanusDB.DBO.v3Kullanici		K	ON	K.TCKIMLIKNO	= V.TCKIMLIKNO_OGR
														WHERE V.TCKIMLIKNO	= T.TC
														FOR JSON PATH
													),'[]')
						FROM #temp T FOR JSON PATH
						),'[]')
						END		
				
					END			
						
				IF @ISLEM = 2     --Öğretmen Branş Listele
				BEGIN		
					SELECT
						ISNULL((
								SELECT DISTINCT
									 DERSAD
								FROM eokul_v2..Ders FOR JSON PATH
							  ),'[]')
					
				END

				IF @ISLEM = 3		-- Yetki Kaldırma
				BEGIN
					IF EXISTS(SELECT TOP 1 1 FROM Pusulam.dbo.KullaniciSifreEngel WHERE TCKIMLIKNO=@TC_KULLANICI)
					BEGIN
						DELETE FROM KullaniciSifreEngel WHERE TCKIMLIKNO=@TC_KULLANICI
					END
					ELSE
					BEGIN
						INSERT INTO KullaniciSifreEngel(TCKIMLIKNO)VALUES(@TC_KULLANICI)
					END
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
PRINT N'Creating Procedure [dbo].[sp_SubeYetki]...';


GO
CREATE PROC [dbo].[sp_SubeYetki]
	@ISLEM				INT				= NULL,
	@TCKIMLIKNO			VARCHAR(11)		= NULL,
	@OTURUM				VARCHAR(36)		= NULL,
	@ID_MENU			INT				= NULL,		
	@DURUM				INT				= 0,
    @ID_KULLANICI		INT				= 0,
	@ID_KULLANICITIPI	INT				= 0,
	@GRUP				INT				= NULL,
	@DONEMKODS			VARCHAR(50)		= '',
	@ID_SUBE				INT			= 0,
	@ID_DONEM			INT				= 0,
	@ID_OGRENCI			INT				= 0,
	@ID_SINIF			INT				= 0,
	@AD					VARCHAR(50)		= '',
	@TC_KULLANICI		VARCHAR(11)		= '',
	@SOYAD				VARCHAR(50)		= '',
	@SINIFTIP			VARCHAR(6)		= NULL,
	@BRANS				INT				= NULL,
	@ID_KADEME			INT				= NULL
AS
BEGIN
	BEGIN TRY    
		DECLARE @MSG			VARCHAR(4000)
		DECLARE @ErrorSeverity	INT
		DECLARE @ErrorState		INT
		DECLARE @_ID_LOG INT = 0
		DECLARE @return_variable INT
		
		DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))

		DECLARE @LOGJSON VARCHAR(MAX)
		SET @LOGJSON = (
			SELECT	 
				ISLEM						= @ISLEM		
				,TCKIMLIKNO					= @TCKIMLIKNO	
				,OTURUM						= @OTURUM		
				,ID_MENU					= @ID_MENU	
				,ID_KULLANICI				= @ID_KULLANICI
				,ID_KULLANICITIPI			= @ID_KULLANICITIPI
				,GRUP						= @GRUP
				,DONEMKODS					= @DONEMKODS
				,ID_SUBE					= @ID_SUBE
				,ID_SINIF					= @ID_SINIF
				,ID_OGRENCI					= @ID_OGRENCI
				,AD							= @AD
				,TC_KULLANICI				= @TC_KULLANICI
				,SOYAD						= @SOYAD
				,SINIFTIP					= @SINIFTIP

			FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
		)	
		DECLARE		@DONEMKOD			VARCHAR(5)
		SET		@DONEMKOD		=	(Select DONEM from OkyanusDB.dbo.v3AktifDonem where AKTIF=1 )	
			DECLARE @ID_LOG INT = 0		
			--EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
			SET @ID_LOG = 1
			IF @ID_LOG = 1
			BEGIN
				IF @ISLEM = 1 -- Kullanıcı Kademe Getir
				BEGIN
					
					SELECT ISNULL((
								SELECT K.ID_KADEME, K.AD, CASE WHEN KK.TCKIMLIKNO IS NULL OR KK.XBASE = 1 THEN 0 ELSE 1 END AS YETKI 
								FROM OkyanusDB.dbo.v3Kademe K
								LEFT JOIN OkyanusDB.dbo.v4KullaniciKademe KK ON KK.ID_KADEME = K.ID_KADEME AND KK.TCKIMLIKNO=@TC_KULLANICI
								ORDER BY K.SIRA
							FOR JSON PATH
					),'[]')
				END

				IF @ISLEM = 2 -- Kullanıcı Kademe Kaydet
				BEGIN
					IF EXISTS (SELECT * FROM OkyanusDB.dbo.v4KullaniciKademe WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_KADEME = @ID_KADEME AND XBASE = 0)
					BEGIN
						DELETE FROM OkyanusDB.dbo.v4KullaniciKademe WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_KADEME = @ID_KADEME
					END
					ELSE IF EXISTS (SELECT * FROM OkyanusDB.dbo.v4KullaniciKademe WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_KADEME = @ID_KADEME AND XBASE = 1)
					BEGIN
						UPDATE OkyanusDB.dbo.v4KullaniciKademe SET XBASE = 0 WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_KADEME = @ID_KADEME
					END
					ELSE
					BEGIN
						INSERT INTO OkyanusDB.dbo.v4KullaniciKademe (TCKIMLIKNO, ID_KADEME, XBASE)
						SELECT @TC_KULLANICI, @ID_KADEME, 0
					END
				END

				IF @ISLEM = 3 -- Kullanıcı tipi Şube Getir
				BEGIN
					SELECT ISNULL((
						SELECT S.ID_SUBE, S.SUBENO, S.AD, YETKI = CASE WHEN MAX(SY.ID_SUBE) IS NULL THEN 0 ELSE 1 END
						FROM OkyanusDB.dbo.v3Sube S
						LEFT JOIN OkyanusDB.dbo.v3SubeYetki SY ON SY.ID_SUBE = S.ID_SUBE AND SY.TCKIMLIKNO = @TC_KULLANICI AND SY.ID_KULLANICITIPI = @ID_KULLANICITIPI
						GROUP BY S.ID_SUBE, S.SUBENO, S.AD
						ORDER BY S.AD
						FOR JSON PATH
					),'[]')
				END

				IF @ISLEM = 4 -- Kullanıcı Tipi Şube Kaydet 
				BEGIN
					IF (@ID_KULLANICITIPI = 1 AND @ID_KULLANICI NOT IN (24549, 66444)) --Admin Yetki Kontrol
					BEGIN
						SELECT 1
					END
					ELSE IF (@ID_KULLANICITIPI IN (4, 5))	--Veli Yetki Kontrol
					BEGIN
						SELECT 2
					END
					ELSE
					BEGIN
					IF EXISTS (SELECT * FROM OkyanusDB.dbo.v3SubeYetki WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_SUBE = @ID_SUBE AND ID_KULLANICITIPI = @ID_KULLANICITIPI)
						BEGIN
							DELETE FROM OkyanusDB.dbo.v3SubeYetki WHERE TCKIMLIKNO = @TC_KULLANICI AND ID_SUBE = @ID_SUBE AND ID_KULLANICITIPI = @ID_KULLANICITIPI 
						END
						ELSE
						BEGIN
							INSERT INTO OkyanusDB.dbo.v3SubeYetki (TCKIMLIKNO, ID_SUBE, ID_KULLANICITIPI, XBASE)
							SELECT @TC_KULLANICI, @ID_SUBE, @ID_KULLANICITIPI, 0
						END
				    END					
					SELECT ISNULL((
						SELECT S.ID_SUBE, S.SUBENO, S.AD, YETKI = CASE WHEN MAX(SY.ID_SUBE) IS NULL THEN 0 ELSE 1 END
						FROM OkyanusDB.dbo.v3Sube S
						LEFT JOIN OkyanusDB.dbo.v3SubeYetki SY ON SY.ID_SUBE = S.ID_SUBE AND SY.TCKIMLIKNO = @TC_KULLANICI AND SY.ID_KULLANICITIPI = @ID_KULLANICITIPI
						GROUP BY S.ID_SUBE, S.SUBENO, S.AD
						ORDER BY S.AD
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
