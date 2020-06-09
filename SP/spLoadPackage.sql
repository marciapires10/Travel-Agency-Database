CREATE PROCEDURE TravelAgency.spLoadPackage

AS
	BEGIN
			SET NOCOUNT ON

			SELECT * FROM TravelAgency.Package
			ORDER BY ID
	END

GO