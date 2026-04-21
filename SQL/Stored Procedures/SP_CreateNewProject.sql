USE NovoDatabase
GO
CREATE PROC CreateNewProject 
@ProjectName NVARCHAR(255),
@Complexity NVARCHAR(50)
AS
BEGIN
INSERT INTO PROJECT (ProjectName, Complexity)
VALUES (@ProjectName, @Complexity);

END;