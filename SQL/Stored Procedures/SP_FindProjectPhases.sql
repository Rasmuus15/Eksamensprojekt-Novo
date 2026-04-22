USE NovoDatabase
GO

CREATE PROCEDURE SP_FindProjectPhases 

@projectID int

AS
BEGIN 

SELECT * FROM PHASE
Where projectID = @projectID;
END;


 
