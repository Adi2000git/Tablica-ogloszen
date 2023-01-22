DROP PROCEDURE sp_uzytkownikDelete
GO
CREATE PROCEDURE [dbo].[sp_uzytkownikDelete]
@Id int 
AS
BEGIN
		DELETE FROM Uzytkownik 
		WHERE Id=@Id
END
