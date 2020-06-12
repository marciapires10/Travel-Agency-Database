CREATE PROCEDURE TravelAgency.spLoadBookings

AS
	BEGIN
			SELECT * FROM TravelAgency.Booking
			ORDER BY ID

	END

GO