CREATE PROCEDURE TravelAgency.spSearchDestination
	@CC_Location VARCHAR(max)

AS

	BEGIN
			SELECT * FROM TravelAgency.Accommodation 
			WHERE CC_Location= @CC_Location

	END

GO