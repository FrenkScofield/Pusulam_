USE [OkyanusIletisim]
GO
/****** Object:  StoredProcedure [dbo].[sp_SendPushMessage]    Script Date: 10.12.2020 10:21:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[sp_SendPushMessage]

	@TCKIMLIKNO			NVARCHAR(MAX)	= NULL,
	@HEADER				NVARCHAR(MAX)	= NULL,
	@MESAJ				NVARCHAR(MAX)	= NULL,
	@LINK				NVARCHAR(MAX)	= NULL,
	@MENU				NVARCHAR(MAX)	= NULL,
	@LINK_ETIKET		NVARCHAR(MAX)	= NULL,
	@TCKIMLIKNO_KIME	NVARCHAR(MAX)	= NULL,
	@ID_UYGULAMA		INT				= NULL,
	@RESIM_URL			NVARCHAR(MAX)	= NULL,
	@GONDERILDI			BIT				= NULL,
	@MESAJTAM			NVARCHAR(MAX)	= NULL,
	@TOPLUMESAJ			INT             = NULL,
	@ID_BILDIRIM        INT				=0	
AS
BEGIN
		BEGIN		
		
			SET @RESIM_URL		= ISNULL(@RESIM_URL		, '')
			SET @GONDERILDI		= ISNULL(@GONDERILDI	,  1)
			SET @LINK			= ISNULL(@LINK			, '')
			SET @MENU			= ISNULL(@MENU			, '')
			SET @LINK_ETIKET	= ISNULL(@LINK_ETIKET	, '')
			SET @MESAJTAM		= ISNULL(@MESAJTAM		, '')

			INSERT INTO Bildirim(KYE_TCKIMLIKNO, MESAJ, TCKIMLIKNO_KIME, HEADER, LINK, ID_UYGULAMA, RESIM_URL, GONDERILDI, MESAJTAM, LINK_ETIKET)
			
			SELECT 
					 @TCKIMLIKNO
					,@MESAJ
					,@TCKIMLIKNO_KIME
					,@HEADER
					,@LINK
					,@ID_UYGULAMA
					,@RESIM_URL
					,@GONDERILDI
					,@MESAJTAM
					,@LINK_ETIKET

		 SET @ID_BILDIRIM = (SELECT SCOPE_IDENTITY())
			IF @GONDERILDI = 1
			BEGIN

				IF @TOPLUMESAJ > 0
				BEGIN
				INSERT INTO BildirimTopluMesaj (ID_TOPLUMESAJ,ID_BILDIRIM) VALUES(@TOPLUMESAJ,@ID_BILDIRIM)
				END

				SET @MESAJ	= REPLACE(REPLACE(REPLACE(REPLACE(@MESAJ	, '"', ' '), '''', ' '), '’', ''), '&', '')
				SET @HEADER = REPLACE(REPLACE(REPLACE(REPLACE(@HEADER	, '"', ' '), '''', ' '), '’', ''), '&', '')
				SET @LINK	= REPLACE(REPLACE(REPLACE(REPLACE(@LINK		, '"', ' '), '''', ' '), '’', ''), '&', '')

				DECLARE  @JSONDATA NVARCHAR(MAX), @URL NVARCHAR(MAX)

				/* LOOP */
				SELECT FCM_TOKEN, RN = ROW_NUMBER() OVER(ORDER BY FCM_TOKEN)
				INTO #Loop
				FROM LoginLog WITH(NOLOCK) 
				WHERE FCM_TOKEN IS NOT NULL AND FCM_TOKEN <> '' AND LOGOUT = 0 AND KYE_TCKIMLIKNO = @TCKIMLIKNO_KIME AND ID_UYGULAMA = @ID_UYGULAMA

				DECLARE @I INT = 1, @COUNT INT = 0, @FCM_TOKEN NVARCHAR(MAX) = ''
				SELECT @COUNT = COUNT(1) FROM #Loop				

				WHILE @I <= @COUNT
				BEGIN
	
					SELECT
						@FCM_TOKEN = FCM_TOKEN
					FROM #Loop
					WHERE RN = @I

					SELECT @JSONDATA = 
						ISNULL((
							SELECT
								 TCKIMLIKNO_KIME	= @TCKIMLIKNO_KIME
								,MESAJ				= @MESAJ
								,HEADER				= @HEADER
								,LINKMSG			= @LINK
								,MENU				= @MENU
								,ANAHTAR			= dbo.Fn_GetBildirimAnahtar(@ID_UYGULAMA)
								,TOKEN				= @FCM_TOKEN
							FOR JSON PATH
						), '{ "JSONDATA" : [] }')


					Declare @Object as Int;
					Declare @ResponseText as Varchar(8000);
		
					SET @URL = 'http://okyanusiletisim.okyanuskoleji.k12.tr/OkyanusApi/PushMessage/SendPushMessage?jsonData={"JSONDATA": "'+@JSONDATA+'"}'
		

					Exec sp_OACreate 'WinHttp.WinHttpRequest.5.1', @Object OUT;				
					Exec sp_OAMethod @Object, 'open', NULL, 'POST', @URL, 'false'
					Exec sp_OAMethod @Object, 'send'
					Exec sp_OAMethod @Object, 'responseText', @ResponseText OUTPUT
					Exec sp_OADestroy @Object
		
					SET @I = @I + 1
				
				END
				
				DROP TABLE #Loop

			END
		
		END
END


