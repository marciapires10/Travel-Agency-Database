USE p1g9
GO

CREATE FUNCTION TravelAgency.GetTotalPrice (@PackID INT)
RETURNS DECIMAL

AS
	BEGIN
			DECLARE @total_price DECIMAL
			DECLARE @acc_price DECIMAL
			DECLARE @duration DECIMAL
			DECLARE @acc_price_duration DECIMAL
			DECLARE @flight_price DECIMAL
			DECLARE @transf_price DECIMAL
			DECLARE @sum_total DECIMAL

			SET @acc_price = (SELECT Accommodation.Price
			FROM TravelAgency.Package JOIN TravelAgency.Accommodation ON Package.Acomm_ID = Accommodation.ID
			WHERE Package.ID = @PackID);

			SET @duration = (SELECT Duration FROM TravelAgency.Package WHERE ID = @PackID);

			SET @acc_price_duration = @acc_price * @Duration;

			SET @flight_price = (SELECT SUM(Flight.Price)
			FROM TravelAgency.Package JOIN TravelAgency.Contains_Flight JOIN TravelAgency.Flight ON Contains_Flight.Flight_ID = Flight.ID ON Package.ID = Contains_Flight.Pack_ID
			WHERE Package.ID = @PackID);

			SET @transf_price = (SELECT SUM(Transfer.Price)
			FROM TravelAgency.Package JOIN TravelAgency.Contains_Transf JOIN TravelAgency.Transfer ON Contains_Transf.Transf_ID = Transfer.ID ON Package.ID = Contains_Transf.Pack_ID
			WHERE Package.ID = @PackID);
		
			SET @sum_total = @acc_price_duration + @flight_price + @transf_price;

			RETURN @sum_total;
	END
GO