CREATE FUNCTION TravelAgency.AverageScore (@PackID INT) returns DECIMAL

AS
	BEGIN
			DECLARE @avgscore DECIMAL
			SELECT @avgscore = round(avg(cast(Score AS DECIMAL)), 1)
			FROM TravelAgency.Review
			WHERE Pack_ID = @PackID

			RETURN @avgscore

	END

