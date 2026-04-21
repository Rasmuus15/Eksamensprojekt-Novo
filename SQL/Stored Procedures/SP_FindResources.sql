USE NovoDatabase
GO


CREATE PROCEDURE SP_FindResources
@Availability bit

AS 
BEGIN 

SELECT * FROM RESOURCE
WHERE Availability = @Availability
END;

