CREATE PROCEDURE TravelAgency.spLoadPromo

AS
	BEGIN
			SELECT * FROM TravelAgency.Promo
			ORDER BY ID
	END

GO