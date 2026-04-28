USE NovoDatabase;
Go

CREATE PROCEDURE FindEmails
@JobRole NVARCHAR(250)

AS 
BEGIN 
SELECT Email FROM RESOURCE
WHERE JobRole = @JobRole AND Availability = 0
END;