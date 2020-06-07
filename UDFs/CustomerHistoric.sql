CREATE FUNCTION TravelAgency.CustomerHistoric (@CustID int) RETURNS TABLE

AS
	RETURN (SELECT * FROM TravelAgency.Booking
			WHERE TravelAgency.Booking.Cust_ID = @CustID)

SELECT * FROM TravelAgency.CustomerHistoric (1);