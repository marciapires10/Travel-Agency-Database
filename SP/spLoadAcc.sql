CREATE PROCEDURE TravelAgency.spLoadAcc

AS
	BEGIN
			SELECT * FROM TravelAgency.Accommodation
			ORDER BY ID
	END

GO