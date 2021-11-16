PRINT N'Disabling all DDL triggers...'
GO
DISABLE TRIGGER ALL ON DATABASE
GO
PRINT N'Creating [dbo].[KullaniciSifreEngel]...';


GO
CREATE TABLE [dbo].[KullaniciSifreEngel] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [TCKIMLIKNO] VARCHAR (11) NOT NULL,
    CONSTRAINT [PK_KullaniciSifreEngel] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Altering [dbo].[sp_ApiDisOgrenci]...';


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_ApiDisOgrenci]
@ISLEM			INT,
@AD				VARCHAR(200)=NULL,
@SOYAD			VARCHAR(200)=NULL,
@TCKIMLIKNO		VARCHAR(11)=NULL,
@SINAVID		INT=NULL,
@SUBENO			INT=NULL

AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)

	SET @LOGJSON = (
		SELECT	 ISLEM					= @ISLEM		
				,AD						= @AD			
				,SOYAD					= @SOYAD		
				,TCKIMLIKNO				= @TCKIMLIKNO	
				,SINAVID				= @SINAVID	
				,SUBENO					= @SUBENO						
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)	

	DECLARE @ID_LOG INT = 0

	BEGIN TRY

	EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = NULL, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = NULL, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME


	IF @ID_LOG>1
	BEGIN
		IF @ISLEM=1
			BEGIN

			SELECT TCORJ = K.TCKIMLIKNO, TCYENI = CONVERT(varchar(11),CONVERT(bigint, K.TCKIMLIKNO) + 1 )
						INTO #API_ORJ_KULLANICI
						FROM v3Kullanici K 
						JOIN v3SubeYetki SY ON K.TCKIMLIKNO = SY.TCKIMLIKNO
			
				DECLARE @SINAV_GRUP INT=0
				DECLARE @SINAV_DONEM INT=0
				DECLARE @AKTIF_DONEM INT=(SELECT OkyanusDB.dbo.fn_AktifDonem())

				--Dışarıdan gelen SınavID ye göre Dönem ve Sınıf bilgisi bulunuyor ve değişkenlere atılıyor
				SELECT TOP 1 @SINAV_DONEM = DONEM
							,@SINAV_GRUP = ID_KADEME3 
				FROM Sinav 
				where ID_SINAV =  @SINAVID	

			--Öğrenci DisOgrenci tablsounda varsa DONEM ve ID_KADEME si sınava göre Update ediliyor
			UPDATE  DisOgrenci 
					SET DONEM=@SINAV_DONEM,
						ID_KADEME3=@SINAV_GRUP 
					WHERE TCKIMLIKNO=@TCKIMLIKNO 
					AND
					EXISTS (SELECT TOP 1 1 FROM DisOgrenci WHERE TCKIMLIKNO=@TCKIMLIKNO)
 	
			--Gelen kayıt  TC Kontrolü ile dışÖğrenci tablosuna yazılıyor.
			INSERT INTO DisOgrenci (TCKIMLIKNO,AD,SOYAD,ID_KADEME3,DONEM,EPOSTA,VELI_AD_SOYAD,KYE_TCKIMLIKNO,SUBE_NO)
						SELECT		@TCKIMLIKNO,@AD,@SOYAD,@SINAV_GRUP,@SINAV_DONEM,'','','',@SUBENO
						WHERE	
							NOT EXISTS (SELECT TOP 1 1 FROM DisOgrenci D WHERE D.TCKIMLIKNO = @TCKIMLIKNO AND AKTIF=1)
							AND NOT EXISTS(SELECT TOP 1 1 FROM v3Ogrenci O WHERE O.TCKIMLIKNO=(CONVERT(bigint, @TCKIMLIKNO)-1))

			--Gelen kayıt TCKIMLIKNO ve Aktif Sınav kontrolü ile Online Sınav tablosuna yazılıyor
			--EKLEYEN BILGISINE OKYANUS KOLEJLERİ TCKIMLIKNOSU MANUEL OLARAK VERILDI METE HOCA ILE GORUSULDU
			INSERT INTO Tbl_OnlineSinavOgrenci (ID_SINAV, TCKIMLIKNO, KYE_TCKIMLIKNO)
					SELECT  DISTINCT @SINAVID AS ID_SINAV, @TCKIMLIKNO , '14531453000'
					WHERE 
						NOT EXISTS (SELECT TOP 1 1 FROM Tbl_OnlineSinavOgrenci OS WITH(NOLOCK) WHERE OS.AKTIF = 1 
									AND OS.ID_SINAV = @SINAVID 
									AND  OS.TCKIMLIKNO = @TCKIMLIKNO)
						AND NOT EXISTS(SELECT TOP 1 1 FROM v3Ogrenci O WHERE O.TCKIMLIKNO=(CONVERT(bigint,@TCKIMLIKNO)-1)) 


			INSERT INTO Tbl_OnlineSinavOgrenci (ID_SINAV, TCKIMLIKNO, KYE_TCKIMLIKNO)
					SELECT  DISTINCT @SINAVID AS ID_SINAV, (CONVERT(bigint,@TCKIMLIKNO)-1) , '14531453000'
					WHERE 
						NOT EXISTS (SELECT TOP 1 1 FROM Tbl_OnlineSinavOgrenci OS WITH(NOLOCK) WHERE OS.AKTIF = 1 
									AND OS.ID_SINAV = @SINAVID 
									AND  OS.TCKIMLIKNO = @TCKIMLIKNO)
						AND EXISTS(SELECT TOP 1 1 FROM v3Ogrenci O WHERE O.TCKIMLIKNO=(CONVERT(bigint,@TCKIMLIKNO)-1))

			--SELECT @AD,@SOYAD,@TCKIMLIKNO,@SINAVID,@SUBENO FOR JSON AUTO

			SELECT 'Öğrenci başarıyla kayıt edildi' AS Mesaj FOR JSON PATH

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
	SET NOCOUNT OFF;
END
GO
PRINT N'Reenabling DDL triggers...'
GO
ENABLE TRIGGER [CaptureStoredProcedureChanges] ON DATABASE
GO
PRINT N'Update complete.';


GO
