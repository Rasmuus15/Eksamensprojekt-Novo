USE NovoDatabase
GO

CREATE PROCEDURE SP_CreateResource
@Email NVARCHAR(20),
@Initials NVARCHAR(4),
@JobRole NVARCHAR(30),
@Availability Bit

AS
BEGIN 

INSERT INTO RESSOURCE (Email, Initials, JobRole, Availability)
Values ('@Email','@Initials', 'JobRole', @Availability);
END;
