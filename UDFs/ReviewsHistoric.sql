CREATE FUNCTION TravelAgency.ReviewsHistoric (@PackID INT) RETURNS TABLE

AS
	RETURN (SELECT * FROM TravelAgency.Review
			WHERE TravelAgency.Review.Pack_ID = @PackID)

