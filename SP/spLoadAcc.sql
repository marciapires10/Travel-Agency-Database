CREATE PROCEDURE TravelAgency.spLoadAcc

AS
	BEGIN
			SET NOCOUNT ON;
			
			SELECT * FROM TravelAgency.Accommodation
			ORDER BY ID asc
	END

GO