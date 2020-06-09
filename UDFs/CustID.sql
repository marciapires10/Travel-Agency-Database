CREATE FUNCTION TravelAgency.GetCustID (@Email VARCHAR(max))
returns INT

AS
	BEGIN
			DECLARE @ID INT
			SET @ID = (Select CustID 
			from TravelAgency.Customer 
			where Email = @Email)
			RETURN @ID;

	END
go

Select TravelAgency.GetCustID ('marcia.pires@ua.pt')