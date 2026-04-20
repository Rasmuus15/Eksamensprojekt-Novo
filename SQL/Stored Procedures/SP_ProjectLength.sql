USE NovoDatabase
GO

CREATE PROCEDURE SP_ProjectLength
@projectID int

AS
BEGIN 
SELECT * FROM PROJECT_LENGTH
WHERE projectID = @projectID;
END;