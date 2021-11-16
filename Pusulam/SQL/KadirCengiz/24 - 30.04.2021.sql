
USE [Pusulam]

SET IDENTITY_INSERT [Menu] ON; 
INSERT INTO Menu (ID_MENU, AD, ACIKLAMA, KOD, URL, RESIM, GIZLI, YARDIMHTML, OZEL )
VALUES (1388, 'Kullanıcı Yetkilendirme Kaldır','Kullanıcı Yetkilendirme Kaldır', '0998004', '#Yetkilendirme/KullaniciYetkilendirmeKaldir','icon-key',0,null,0)
SET IDENTITY_INSERT [Menu] OFF; 


USE [Pusulam]
GO
/****** Object:  StoredProcedure [dbo].[sp___TASLAK_SP]    Script Date: 30.04.2021 14:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_KullaniciMenuYetkiKaldir]
	@ISLEM					INT				= NULL,
	@TCKIMLIKNO				VARCHAR(11)		= NULL,
	@OTURUM					VARCHAR(36)		= NULL,
	@ID_MENU				INT				= NULL,
	
	@TC_KULLANICI			VARCHAR(11)		= NULL,
	@SQLJSON				NVARCHAR(MAX)	= NULL

AS
BEGIN
/*

	Olulturan Adı		: Kadir Cengiz
	Oluşturulma Tarih	: 30.04.2021
	
	--	sp Create

*/	
	
	DECLARE @PROCNAME VARCHAR(MAX) = (SELECT OBJECT_NAME(@@PROCID))
	DECLARE @LOGJSON VARCHAR(MAX)
	SET @LOGJSON = (
		SELECT	 ISLEM					= @ISLEM
				,TCKIMLIKNO				= @TCKIMLIKNO
				,OTURUM					= @OTURUM
				,ID_MENU				= @ID_MENU

				,TC_KULLANICI			= @TC_KULLANICI
				,SQLJSON				= @SQLJSON
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER	   
	)

	DECLARE @ID_LOG INT = 0

	BEGIN TRY
		EXEC @ID_LOG = dbo.sp_OturumKontrolMenuYetkiLog @OTURUM = @OTURUM, @TCKIMLIKNO = @TCKIMLIKNO, @ID_MENU = @ID_MENU, @LOGJSON = @LOGJSON, @ISLEM = @ISLEM, @PROSEDURADI = @PROCNAME
		

		IF @ID_LOG > 0
		BEGIN
			
			IF @ISLEM = 1	--	Kullanıcı Menu Yetki Listesi	
			BEGIN
				
				SELECT (
					SELECT DISTINCT
						 K.TCKIMLIKNO
						,K.AD
						,K.SOYAD
						,K.AKTIF
						,KULLANICITIPILIST	= ( SELECT DISTINCT KT.AD AS KULLANICITIPI
												FROM OkyanusDB..v3SubeYetki	KY
												JOIN OkyanusDB..v3KullaniciTipi		KT	ON	KT.ID_KULLANICITIPI	= KY.ID_KULLANICITIPI
												WHERE KY.TCKIMLIKNO	= MKY.TCKIMLIKNO
												FOR JSON PATH
												)
						,MENULIST			= ( SELECT DISTINCT M.AD AS MENU
												FROM MenuKullaniciYetki	SMY
												JOIN Menu				M	ON	M.ID_MENU = SMY.ID_MENU
												WHERE SMY.TCKIMLIKNO = MKY.TCKIMLIKNO
												FOR JSON PATH
												)
					FROM MenuKullaniciYetki	MKY
					JOIN v3Kullanici		K	ON	K.TCKIMLIKNO	= MKY.TCKIMLIKNO
					WHERE K.AKTIF = 1
					FOR JSON PATH
				)

			END
			IF @ISLEM = 2	--	Kullanıcı Menu Yetki Kaldır		
			BEGIN
				
				DELETE FROM MenuKullaniciYetki
				WHERE TCKIMLIKNO = @TC_KULLANICI

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