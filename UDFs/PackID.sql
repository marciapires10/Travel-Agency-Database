CREATE FUNCTION TravelAgency.GetPackID (@Title VARCHAR(max))
returns INT

AS
	BEGIN
			DECLARE @ID INT
			SET @ID = (Select ID 
			from TravelAgency.Package 
			where Title = @Title)
			RETURN @ID;

	END
go

--Select TravelAgency.GetPackID ('Family trip')