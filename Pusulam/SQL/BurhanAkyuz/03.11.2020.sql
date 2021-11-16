--Hedef Belirle (Ortaokul) olan menü Yol Haritam olarak güncelle
USE Pusulam
 UPDATE Menu SET AD='Yol Haritam' WHERE ID_MENU=1106


INSERT INTO Menu (AD,ACIKLAMA,KOD,[URL],RESIM,GIZLI,YARDIMHTML,OZEL) VALUES('Veli','Veli öğrenci hedef takibi','066','','icon-user',0,NULL,0) 
INSERT INTO Menu (AD,ACIKLAMA,KOD,[URL],RESIM,GIZLI,YARDIMHTML,OZEL) VALUES('Veli Hedef Takibi','Veli öğrenci hedef takibi','066001','#/Veli/OgrenciHedefTakibi','icon-user',0,NULL,0) 


--INSERT INTO MenuYetki (ID_KULLANICITIPI,ID_MENU,ID_KADEME) VALUES (5,1370,4)
-- INSERT INTO MenuYetki (ID_KULLANICITIPI,ID_MENU,ID_KADEME) VALUES (5,1371,4)

 UPDATE MENU SET [URL] = '#/Veli/Hedefler/OgrenciHedefTakibi' where ID_MENU = 1371