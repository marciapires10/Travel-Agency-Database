CREATE FUNCTION TravelAgency.GetBookIDs (@BookID INT) RETURNS TABLE

AS
	RETURN (SELECT Pack_ID, Cust_ID FROM TravelAgency.Booking
			WHERE TravelAgency.Booking.ID = @BookID)

--SELECT Pack_ID, Cust_ID From TravelAgency.GetBookIDs (1)
